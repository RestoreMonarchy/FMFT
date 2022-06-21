CREATE PROCEDURE dbo.CreateReservation
	@ShowId INT,
	@SeatId INT,
	@UserId INT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF EXISTS(SELECT * FROM dbo.Reservations WHERE ShowId = @ShowId AND SeatId = @SeatId)
		RETURN 1;

	IF EXISTS(SELECT * FROM dbo.Reservations WHERE UserId = @UserId AND ShowId = @ShowId)
		RETURN 2;

	INSERT INTO dbo.Reservations (ShowId, SeatId, UserId)
	VALUES (@ShowId, @SeatId, @UserId);

	RETURN 0;
END
