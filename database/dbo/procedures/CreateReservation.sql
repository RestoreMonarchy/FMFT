CREATE PROCEDURE dbo.CreateReservation
	@ShowId INT,
	@UserId INT,
	@Seats VARCHAR(8000),
	@Email NVARCHAR(255) NULL,
	@FirstName NVARCHAR(255) NULL,
	@LastName NVARCHAR(255) NULL
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @validStatuses TABLE([Status] TINYINT);
	DECLARE @ret INT = 0;
	DECLARE @id CHAR(8) = '--------';
	DECLARE @seatsTable TABLE(SeatId INT NOT NULL PRIMARY KEY);


	-- Add Pending and Ok to valid statuses
	INSERT INTO @validStatuses VALUES (0), (1);

	INSERT INTO @seatsTable (SeatId) SELECT VALUE FROM STRING_SPLIT(@Seats, ',');

	IF @@ROWCOUNT = 0
		SET @ret = 3;
	
	-- some seat is already reserved
	IF EXISTS(
		SELECT * 
		FROM dbo.Reservations r 
		JOIN dbo.ReservationSeats rs ON rs.ReservationId = r.Id 
		JOIN @seatsTable st ON st.SeatId = rs.SeatId
		WHERE r.ShowId = @ShowId AND r.[Status] IN (SELECT Status FROM @validStatuses)
		)
	BEGIN
		SET @ret = 1;
	END;
		

	IF EXISTS(SELECT * FROM dbo.Reservations WHERE UserId = @UserId AND ShowId = @ShowId AND [Status] IN (SELECT Status FROM @validStatuses))
		SET @ret = 2;
			
	IF @ret = 0
	BEGIN		
		
		EXEC dbo.GetNewReservationId @Id = @id OUTPUT;

		BEGIN TRAN;

		INSERT INTO dbo.Reservations (Id, ShowId, UserId)
		VALUES (@id, @ShowId, @UserId);

		INSERT INTO dbo.ReservationSeats (ReservationId, SeatId)
		SELECT @id, SeatId FROM @seatsTable;

		IF @UserId IS NULL
		BEGIN
			INSERT INTO dbo.ReservationDetails (ReservationId, Email, FirstName, LastName)
			VALUES (@id, @Email, @FirstName, @LastName);
		END
		
		COMMIT;
	END;

	EXEC dbo.GetReservations @ReservationId = @id;

	RETURN @ret;
END;
