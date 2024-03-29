﻿CREATE PROCEDURE dbo.CreateReservation
	@ShowId INT,
	@UserId INT,
	@Items VARCHAR(8000),
	@Email NVARCHAR(255) = NULL,
	@FirstName NVARCHAR(255) = NULL,
	@LastName NVARCHAR(255) = NULL,
	@OrderId INT = NULL,
	@ReturnReservations BIT = 1,
	@DisallowMultipleReservationsForOneShowFromOneUser BIT = 1
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @isExternalTransaction BIT = @@TRANCOUNT;
	DECLARE @ret INT = 0;
	DECLARE @reservationId CHAR(8) = '--------';
	DECLARE @itemsCount INT; 
	DECLARE @seatItemsCount INT;
	DECLARE @validatedSetsCount INT;
	DECLARE @status TINYINT = CASE WHEN @OrderId IS NULL THEN 1 ELSE 0 END;

	DECLARE @ItemsTable TABLE (ShowProductId INT NOT NULL, SeatId INT NULL, IsBulk INT NOT NULL);

	INSERT INTO @ItemsTable (ShowProductId, SeatId, IsBulk) 
	SELECT ShowProductId, SeatId, CASE WHEN SeatId IS NULL THEN 1 ELSE 0 END
	FROM OPENJSON ( @Items )  
	WITH (   
		ShowProductId INT '$.ShowProductId',
		SeatId INT '$.SeatId'
	);

	SET @itemsCount = @@ROWCOUNT;

	SELECT @seatItemsCount = COUNT(*) FROM @ItemsTable WHERE SeatId IS NOT NULL;

	SELECT @validatedSetsCount = COUNT(*)
	FROM dbo.Shows s
	JOIN dbo.Auditoriums a ON a.Id = s.AuditoriumId
	JOIN dbo.Seats t ON t.AuditoriumId = a.Id
	JOIN @ItemsTable t2 ON t2.SeatId = t.Id
	WHERE s.Id = @ShowId;

	IF @seatItemsCount > @validatedSetsCount
	BEGIN
		PRINT FORMATMESSAGE('Seats provided are invalid. @itemsCount = %d, @validatedSetsCount = %d', @itemsCount, @validatedSetsCount);
		SET @ret = 3;
	END;

	IF @ret = 0 
		AND EXISTS(
			SELECT * 
			FROM dbo.Reservations r 
			JOIN dbo.ReservationItems ri ON ri.ReservationId = r.Id 
			JOIN @ItemsTable st ON st.SeatId = ri.SeatId
			WHERE r.ShowId = @ShowId
			AND r.IsValid = 1
		)
	BEGIN
		PRINT 'One or more seats is already reserved';
		SET @ret = 1;
	END;

	IF @ret = 0 
		AND @DisallowMultipleReservationsForOneShowFromOneUser = 1 
		AND EXISTS(SELECT * FROM dbo.Reservations WHERE UserId = @UserId AND ShowId = @ShowId AND IsValid = 1)
	BEGIN
		PRINT 'There are already reservations for this show from this user';
		SET @ret = 2;
	END;

	IF @ret = 0 
		AND EXISTS(SELECT ShowProductId, SeatId
			FROM @ItemsTable
			WHERE SeatId IS NOT NULL 
			GROUP BY ShowProductId, SeatId HAVING COUNT(*) > 1)
	BEGIN
		PRINT 'There are some duplicated seat for this reservation request';
		SET @ret = 4;
	END;

	IF @ret = 0 
		AND EXISTS(SELECT * FROM @ItemsTable i JOIN dbo.ShowProducts s ON s.Id = i.ShowProductId WHERE s.IsBulk = 0 AND i.SeatId IS NULL)
	BEGIN
		PRINT 'There are some non-bulk show products with eampty SeatId';
		SET @ret = 5;
	END;
	
	IF @ret = 0
		AND EXISTS(SELECT * FROM @ItemsTable i 
		LEFT JOIN dbo.Shows s ON s.Id = @ShowId
		LEFT JOIN dbo.Seats t ON t.Id = i.SeatId AND t.AuditoriumId = s.AuditoriumId
		WHERE i.SeatId IS NOT NULL
		AND t.Id IS NULL)
	BEGIN
		PRINT 'One or more seatIds are for a different show than specified';
		SET @ret = 6;
	END;

	IF @ret = 0
		AND EXISTS(
			SELECT * FROM @ItemsTable WHERE IsBulk = 1
		)
	BEGIN
		
		WITH CTE_ReservationBulkProducts AS (
			SELECT ShowProductId, Qty = COUNT(*) 
			FROM @ItemsTable 
			WHERE IsBulk = 1
			GROUP BY ShowProductId
		), 
		CTE_ShowBulkProducts AS (
			SELECT 
				ShowProductId = sp.Id, 
				AlreadyReservedQty = t.AlreadyReservedQty,
				MaxQty = sp.Quantity
			FROM dbo.ShowProducts sp
			OUTER APPLY (SELECT AlreadyReservedQty = COUNT(*) 
				FROM dbo.ReservationItems ri
					JOIN dbo.Reservations r ON r.Id = ri.ReservationId
				WHERE r.IsValid = 1
				AND ri.ShowProductId = sp.Id) AS t
			WHERE sp.IsBulk = 1
			AND sp.ShowId = @ShowId
		)
		SELECT rbp.ShowProductId, sbp.MaxQty, rbp.Qty, AlreadyReservedQty = ISNULL(sbp.AlreadyReservedQty, 0)
		INTO #BulkShowProductIdsOverbooked
		FROM CTE_ReservationBulkProducts rbp 
		JOIN CTE_ShowBulkProducts sbp ON sbp.ShowProductId = rbp.ShowProductId
		WHERE rbp.Qty + ISNULL(sbp.AlreadyReservedQty, 0) > sbp.MaxQty;

		IF EXISTS(SELECT * FROM #BulkShowProductIdsOverbooked)
		BEGIN
			PRINT 'One or more bulk products are overbooked';
			SET @ret = 7;
		END;
	END;

	IF @ret = 0
	BEGIN		
		
		EXEC dbo.GetNewReservationId @Id = @reservationId OUTPUT;

		IF @isExternalTransaction = 0
			BEGIN TRAN;

		INSERT INTO dbo.Reservations (Id, ShowId, UserId, OrderId, [Status])
		VALUES (@reservationId, @ShowId, @UserId, @OrderId, @status);

		INSERT INTO dbo.ReservationItems (ReservationId, SeatId, ShowProductId)
		SELECT @reservationId, SeatId, ShowProductId FROM @itemsTable;

		IF @UserId IS NULL
		BEGIN
			INSERT INTO dbo.ReservationDetails (ReservationId, Email, FirstName, LastName)
			VALUES (@reservationId, @Email, @FirstName, @LastName);
		END

		IF @isExternalTransaction = 0		
			COMMIT;
	END;

	IF @ReturnReservations = 1
		EXEC dbo.GetReservations @ReservationId = @reservationId;

	RETURN @ret;
END;
