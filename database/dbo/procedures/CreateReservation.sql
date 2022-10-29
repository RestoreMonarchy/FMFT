CREATE PROCEDURE dbo.CreateReservation
	@ShowId INT,
	@SeatId INT,
	@UserId INT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @ret INT = 0;
	DECLARE @id CHAR(8) = '--------';

	IF EXISTS(SELECT * FROM dbo.Reservations WHERE ShowId = @ShowId AND SeatId = @SeatId)
		SET @ret = 1;

	IF EXISTS(SELECT * FROM dbo.Reservations WHERE UserId = @UserId AND ShowId = @ShowId)
		SET @ret = 2;

			
	IF @ret = 0
	BEGIN		
		
		EXEC dbo.GetNewReservationId @Id = @id OUTPUT;

		INSERT INTO dbo.Reservations (Id, ShowId, SeatId, UserId)
		VALUES (@id, @ShowId, @SeatId, @UserId);

	END;

	EXEC dbo.GetReservations @ReservationId = @id;

	RETURN @ret;
END;
