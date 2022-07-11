CREATE PROCEDURE dbo.CreateReservation
	@ShowId INT,
	@SeatId INT,
	@UserId INT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @ret INT = 0;
	DECLARE @id INT;

	IF EXISTS(SELECT * FROM dbo.Reservations WHERE ShowId = @ShowId AND SeatId = @SeatId)
		SET @ret = 1;

	IF EXISTS(SELECT * FROM dbo.Reservations WHERE UserId = @UserId AND ShowId = @ShowId)
		SET @ret = 2;

	IF @ret = 0
	BEGIN
		INSERT INTO dbo.Reservations (ShowId, SeatId, UserId)
		VALUES (@ShowId, @SeatId, @UserId);

		SET @id = SCOPE_IDENTITY();
	END;

	SELECT r.*, s.*, se.*, u.* 
	FROM dbo.Reservations r
	JOIN dbo.Shows s ON s.Id = r.ShowId
	JOIN dbo.Seats se ON se.Id = r.SeatId
	JOIN dbo.Users u ON u.Id = r.UserId
	WHERE r.Id = @id;

	RETURN @ret;
END;
