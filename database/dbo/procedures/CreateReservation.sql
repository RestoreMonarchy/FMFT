CREATE PROCEDURE dbo.CreateReservation
	@ShowId INT,
	@SeatId INT,
	@UserId INT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	--DECLARE @auditoriumId INT;
	
	--IF NOT EXISTS(SELECT * FROM dbo.Seats)

	--SELECT @auditoriumId = AuditoriumId FROM dbo.Shows WHERE Id = @ShowId;

	--IF NOT EXISTS(SELECT * FROM dbo.Seats WHERE Id = @SeatId AND AuditoriumId = @auditoriumId)
	--	RETURN 1;

	IF EXISTS(SELECT * FROM dbo.Reservations WHERE ShowId = @ShowId AND SeatId = @SeatId)
		RETURN 1;

	IF EXISTS(SELECT * FROM dbo.Reservations WHERE UserId = @UserId AND ShowId = @ShowId)
		RETURN 2;

	INSERT INTO dbo.Reservations (ShowId, SeatId, UserId)
	VALUES (@ShowId, @SeatId, @UserId);

	SELECT r.*, s.*, se.*, u.* 
	FROM dbo.Reservations r
	JOIN dbo.Shows s ON s.Id = r.ShowId
	JOIN dbo.Seats se ON se.Id = r.SeatId
	JOIN dbo.Users u ON u.Id = r.UserId
	WHERE r.Id = SCOPE_IDENTITY();

	RETURN 0;
END
