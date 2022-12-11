﻿CREATE PROCEDURE dbo.CreateReservation
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

	DECLARE @ret INT = 0;
	DECLARE @id CHAR(8) = '--------';
	DECLARE @seatsTable TABLE(SeatId INT NOT NULL PRIMARY KEY);

	INSERT INTO @seatsTable (SeatId) SELECT VALUE FROM STRING_SPLIT(@Seats, ',');

	IF @@ROWCOUNT = 0
		SET @ret = 3;

	IF EXISTS(
		SELECT * 
		FROM dbo.Reservations r 
		JOIN dbo.ReservationSeats rs ON rs.ReservationId = r.Id 
		JOIN @seatsTable st ON st.SeatId = rs.SeatId
		WHERE r.ShowId = @ShowId AND r.IsCanceled = 0
		)
	BEGIN
		SET @ret = 1;
	END;
		

	IF EXISTS(SELECT * FROM dbo.Reservations WHERE UserId = @UserId AND ShowId = @ShowId AND IsCanceled = 0)
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
