CREATE PROCEDURE dbo.CreateOrder @Order NVARCHAR(max)
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;
	
	DECLARE @isExternalTransaction BIT = @@TRANCOUNT;
	DECLARE @userId INT;
	DECLARE @amount DECIMAL(9,2);
	DECLARE @currency CHAR(3);
	DECLARE @paymentMethod VARCHAR(255);
	DECLARE @paymentProvider TINYINT;
	DECLARE @expireDate DATETIME2(0);
	DECLARE @orderItemsJSON NVARCHAR(max);
	DECLARE @seatIdsJSON NVARCHAR(max);

	DECLARE @OrderItems TABLE (
		ShowProductId INT NOT NULL, 
		Quantity INT NOT NULL, 
		Price DECIMAL(9,2) NOT NULL, 
		ShowId INT NULL, 
		ShowEndDateTime DATETIME2(0) NULL);

	DECLARE @Seats TABLE (RowNum INT NOT NULL, SeatId INT NOT NULL, ShowProductId INT NULL, ShowId INT NULL);

	DECLARE @orderId INT;
	DECLARE @showId INT;
	DECLARE @seatIds VARCHAR(8000);
	DECLARE @ret INT = 0;
	DECLARE @retCreateReservation INT;

	DECLARE @sumOrderQuantity INT;
	DECLARE @sumOrderNotBulkQuantity INT;
	DECLARE @reservedSeatsCount INT;
	DECLARE @sumOrderItemsAmount DECIMAL(9,2);
	DECLARE @completedShowId INT;

	SELECT 
		@userId = UserId,
		@amount = Amount,
		@currency = Currency,
		@paymentMethod = PaymentMethod,
		@paymentProvider = PaymentProvider,
		@expireDate = [ExpireDate],
		@orderItemsJSON = [Items],
		@seatIdsJSON = SeatIds 
	FROM OPENJSON ( @Order )  
	WITH (   
		UserId INT '$.UserId',
		Amount DECIMAL(9,2) '$.Amount',
		Currency CHAR(3) '$.Currency',
		PaymentMethod VARCHAR(255) '$.PaymentMethod',
		PaymentProvider TINYINT '$.PaymentProvider',
		[ExpireDate] DATETIME2(0) '$.ExpireDate',
		[Items] NVARCHAR(MAX) AS JSON, 
		SeatIds NVARCHAR(MAX) AS JSON
	 );

	INSERT INTO @OrderItems (ShowProductId, Quantity, Price)
	SELECT 
		ShowProductId, 
		Quantity, 
		Price
	FROM OPENJSON ( @orderItemsJSON )  
	WITH (   
		ShowProductId INT '$.ShowProductId',
		Quantity INT '$.Quantity',
		Price DECIMAL(9,2) '$.Price'
	);

	INSERT INTO @Seats (RowNum, SeatId)
	SELECT 
		rownum = ROW_NUMBER() OVER(ORDER BY SeatId), 
		SeatId
	FROM OPENJSON ( @seatIdsJSON )  
	WITH ( SeatId INT '$');
	
	SET @reservedSeatsCount = @@ROWCOUNT;

	SELECT @sumOrderNotBulkQuantity = SUM(oi.Quantity)
	FROM @OrderItems oi
	JOIN dbo.ShowProducts sp ON sp.Id = oi.ShowProductId
	WHERE sp.IsBulk = 0;

	IF @sumOrderNotBulkQuantity <> @reservedSeatsCount
	BEGIN 
		PRINT FORMATMESSAGE('Sum of not bulk order items quantity %d does not match reserved sets count %d', @sumOrderNotBulkQuantity, @reservedSeatsCount);
		SET @ret = 101;
	END; 

	SELECT @sumOrderQuantity = SUM(Quantity)
	FROM @OrderItems;

	IF @sumOrderQuantity > 100
	BEGIN 
		PRINT FORMATMESSAGE('Sum of order quantity %d is too big. It must not exceed 100 for a single order', @sumOrderQuantity);
		SET @ret = 102;
	END;

	UPDATE o
	SET o.ShowId = s.Id,
		o.ShowEndDateTime = s.EndDateTime
	FROM @OrderItems o
	JOIN dbo.ShowProducts p ON p.Id = o.ShowProductId
	JOIN dbo.Shows s ON s.Id = p.ShowId;


	IF EXISTS (SELECT * FROM @OrderItems WHERE ShowId IS NULL)
	BEGIN 
		PRINT 'Invalid value of ShowProductId';
		SET @ret = 103;
	END; 

	IF NOT ISNULL(@amount, 0) > 0
	BEGIN
		PRINT 'Order amount must be greater than 0';
		SET @ret = 104;
	END;

	IF EXISTS (SELECT * FROM @OrderItems oi JOIN dbo.Shows s ON s.Id = oi.ShowId WHERE s.IsEnabled = 0)
	BEGIN
		PRINT 'One or more shows are disabled';
		SET @ret = 110;
	END;

	IF EXISTS (SELECT * FROM @OrderItems oi JOIN dbo.Shows s ON s.Id = oi.ShowId WHERE s.SellStartDateTime > SYSDATETIME())
	BEGIN
		PRINT 'One or more shows have not started selling';
		SET @ret = 111;
	END;

	SELECT @sumOrderItemsAmount = SUM(Quantity * Price)	FROM @OrderItems;

	IF @amount <> @sumOrderItemsAmount
	BEGIN 
		PRINT FORMATMESSAGE('Order amount %s does not match order amount calculated as sum of items %s', 
							FORMAT(@amount, 'N', 'en-us'), 
							FORMAT(@sumOrderItemsAmount, 'N', 'en-us'));
		SET @ret = 105;
	END;

	SELECT TOP (1) 
		@completedShowId = ShowId 
	FROM @OrderItems 
	WHERE ShowEndDateTime < SYSDATETIME();

	IF @completedShowId IS NOT NULL
	BEGIN
		PRINT FORMATMESSAGE('Show Id %d is from the past', @completedShowId);
		SET @ret = 106;
	END;

	IF @ret = 0
	BEGIN

		WITH CTE_Tally AS (
			SELECT * FROM dbo.Tally WHERE N <= @sumOrderQuantity
		), CTE_OrderItemsSplit AS (
			SELECT 
				RowNum = ROW_NUMBER() OVER(ORDER BY o.ShowProductId), 
				o.* 
			FROM @OrderItems o
			JOIN dbo.Tally t ON t.N <= o.Quantity
		)
		UPDATE s
		SET 
			s.ShowProductId = o.ShowProductId,
			s.ShowId = o.ShowId
		FROM @Seats s
		JOIN CTE_OrderItemsSplit o ON o.RowNum = s.RowNum;

		IF @isExternalTransaction = 0
			BEGIN TRAN; 

		INSERT INTO dbo.Orders (UserId, Amount, Currency, PaymentMethod, PaymentProvider, [ExpireDate])
		VALUES (@UserId, @Amount, @Currency, @PaymentMethod, @PaymentProvider, @ExpireDate);
		
		SET @orderId = SCOPE_IDENTITY();

		INSERT INTO dbo.OrderItems (OrderId, ShowProductId, Price, Quantity)
		SELECT 
			@OrderId, 
			ShowProductId, 
			Price, 
			Quantity
		FROM @OrderItems;

		SET @showId = 0;

		WHILE 1=1
		BEGIN
		
			SELECT @showId = MIN(ShowId)
			FROM @Seats
			WHERE ShowId > @showId;

			IF @showId IS NULL
				BREAK;

			SELECT @seatIds = STRING_AGG(SeatId, ',')
			FROM @Seats
			WHERE ShowId = @showId;

			EXEC @retCreateReservation = dbo.CreateReservation
				@ShowId = @showId,
				@UserId = @userId,
				@Seats = @seatIds,
				@OrderId = @orderId,
				@ReturnReservations = 0,
				@DisallowMultipleReservationsForOneShowFromOneUser = 0;

			IF @retCreateReservation <> 0
			BEGIN
				SET @ret = @retCreateReservation;
				BREAK;
			END;
		END;

		IF @isExternalTransaction = 0 AND @@TRANCOUNT > 0
		BEGIN
			IF @ret = 0 
				COMMIT;
			ELSE 
				ROLLBACK;
		END;
	END; -- IF @ret = 0

	IF @OrderId IS NULL
		SET @OrderId = -1;

	EXEC dbo.GetOrders @OrderId = @orderId;

	RETURN @ret;

END;
GO
/*
-- EXECUTE EXAMPLE:

DECLARE @Order NVARCHAR(MAX) = N'{  
    "UserId":1,  
    "Amount":146.12, 
	"Currency":"PLN",
	"PaymentMethod":"1",
	"PaymentProvider": 1,
	"ExpireDate":"2023-01-17T20:01:12",
    "Items": [{  
		"ShowProductId":1,
		"Price":100.12,  
		"Quantity":1
    },
	{  
		"ShowProductId":2,
		"Price":23.00,  
		"Quantity":2
    }],
	"SeatIds": [47,48,49]
  }';
  DECLARE @ret INT;
  EXEC @ret = dbo.CreateOrder @Order = @Order;
  SELECT @ret;

*/