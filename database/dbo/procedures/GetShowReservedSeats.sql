CREATE PROCEDURE dbo.GetShowReservedSeats
	@ShowId INT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @ret INT = 0;

	IF EXISTS(SELECT * FROM dbo.Shows WHERE Id = @ShowId)
		SET @ret = 1;

	SELECT r.SeatId FROM dbo.Shows s 
	LEFT JOIN dbo.Reservations r ON r.ShowId = s.Id
	WHERE s.Id = @ShowId;

	RETURN @ret;
END