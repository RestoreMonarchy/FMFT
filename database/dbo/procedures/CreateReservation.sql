CREATE PROCEDURE dbo.CreateReservation
	@ShowId INT,
	@UserId INT,
	@Seats VARCHAR(8000),
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
	DECLARE @seatsTable TABLE(SeatId INT NOT NULL PRIMARY KEY);
	DECLARE @setsCount INT;
	DECLARE @validatedSetsCount INT;

	INSERT INTO @seatsTable (SeatId) 
	SELECT [Value] 
	FROM STRING_SPLIT(@Seats, ',');

	SET @setsCount = @@ROWCOUNT;

	SELECT @validatedSetsCount = COUNT(*)
	FROM dbo.Shows s
	JOIN dbo.Auditoriums a ON a.Id = s.AuditoriumId
	JOIN dbo.Seats t ON t.AuditoriumId = a.Id
	JOIN @seatsTable t2 ON t2.SeatId = t.Id
	WHERE s.Id = @ShowId;

	IF @setsCount = 0 OR @setsCount > @validatedSetsCount
	BEGIN
		PRINT FORMATMESSAGE('Seats provided are invalid. @setsCount = %d, @validatedSetsCount = %d', @setsCount, @validatedSetsCount);
		SET @ret = 3;
	END;

	IF @ret = 0 
		AND EXISTS(
			SELECT * 
			FROM dbo.Reservations r 
			JOIN dbo.ReservationSeats rs ON rs.ReservationId = r.Id 
			JOIN @seatsTable st ON st.SeatId = rs.SeatId
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
	BEGIN		
		
		EXEC dbo.GetNewReservationId @Id = @reservationId OUTPUT;

		IF @isExternalTransaction = 0
			BEGIN TRAN;

		INSERT INTO dbo.Reservations (Id, ShowId, UserId, OrderId)
		VALUES (@reservationId, @ShowId, @UserId, @OrderId);

		INSERT INTO dbo.ReservationSeats (ReservationId, SeatId)
		SELECT @reservationId, SeatId FROM @seatsTable;

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
