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
		ShowEndDateTime DATETIME2(0) NULL,
		SeatIds NVARCHAR(MAX) NULL);

	DECLARE @Seats TABLE (SeatId INT NULL, ShowProductId INT NULL, ShowId INT NULL, Quantity INT);

	DECLARE @orderId INT;
	DECLARE @showId INT;
	DECLARE @itemsJSON VARCHAR(8000);
	DECLARE @ret INT = 0;
	DECLARE @retCreateReservation INT;

	DECLARE @sumOrderQuantity INT;
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
		@orderItemsJSON = [Items]
	FROM OPENJSON ( @Order )  
	WITH (   
		UserId INT '$.UserId',
		Amount DECIMAL(9,2) '$.Amount',
		Currency CHAR(3) '$.Currency',
		PaymentMethod VARCHAR(255) '$.PaymentMethod',
		PaymentProvider TINYINT '$.PaymentProvider',
		[ExpireDate] DATETIME2(0) '$.ExpireDate',
		[Items] NVARCHAR(MAX) AS JSON
	 );

	INSERT INTO @OrderItems (ShowProductId, Quantity, Price, SeatIds)
	SELECT 
		ShowProductId, 
		Quantity, 
		Price,
		SeatIds
	FROM OPENJSON( @orderItemsJSON )  
	WITH (   
		ShowProductId INT '$.ShowProductId',
		Quantity INT '$.Quantity',
		Price DECIMAL(9,2) '$.Price',
		SeatIds NVARCHAR(MAX) AS JSON
	);

	INSERT INTO @Seats (SeatId, ShowProductId, ShowId, Quantity)
	SELECT 
		s.SeatId,
		i.ShowProductId,
		sp.ShowId,
		i.Quantity
	FROM @OrderItems i
	JOIN ShowProducts sp ON sp.Id = i.ShowProductId
	OUTER APPLY (SELECT SeatId FROM OPENJSON( i.SeatIds ) WITH ( SeatId INT '$')) s(SeatId)
	
	SET @reservedSeatsCount = @@ROWCOUNT;
	
	INSERT INTO @Seats(SeatId, ShowProductId, ShowId, Quantity)
	SELECT SeatId, ShowProductId, ShowId, Quantity
	FROM @Seats s
	JOIN dbo.Tally t ON t.N < s.Quantity
	WHERE s.SeatId IS NULL
	AND s.Quantity > 1

	SET @reservedSeatsCount += @@ROWCOUNT;

	SELECT @sumOrderQuantity = SUM(Quantity)
	FROM @OrderItems;

	IF @sumOrderQuantity <> @reservedSeatsCount
	BEGIN 
		PRINT FORMATMESSAGE('Sum of order quantity %d does not match reserved sets count %d', @sumOrderQuantity, @reservedSeatsCount);
		SET @ret = 101;
	END; 

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

			-- N'[{"ShowProductId":10},{"ShowProductId":2,"SeatId":125}]',

			SET @itemsJSON = (
				SELECT ShowProductId, SeatId
				FROM @Seats
				WHERE  ShowId = @showId
				FOR JSON PATH
			);

			EXEC @retCreateReservation = dbo.CreateReservation
				@ShowId = @showId,
				@UserId = @userId,
				@Items = @itemsJSON,
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
-- EXECUTE EXAMPLE:
/*

DECLARE @Order NVARCHAR(MAX) = N'{  
    "UserId":1,  
    "Amount":176.12, 
	"Currency":"PLN",
	"PaymentMethod":"1",
	"PaymentProvider": 1,
	"ExpireDate":"2023-01-17T20:01:12",
    "Items": [
	{  
		"ShowProductId":3,
		"Price":100.12,  
		"Quantity":1,
		"SeatIds": [47]
    },
	{  
		"ShowProductId":3,
		"Price":23.00,
		"Quantity":2,
		"SeatIds": [48,49]
    },
	{
		"ShowProductId":10,
		"Price": 10.00,
		"Quantity": 3,
		"SeatIds": []
	}]
  }';
DECLARE @ret INT;
EXEC @ret = dbo.CreateOrder @Order = @Order;
SELECT @ret;

*/
