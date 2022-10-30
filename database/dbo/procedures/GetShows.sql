CREATE PROCEDURE dbo.GetShows
	@ShowId INT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	IF @ShowId IS NOT NULL
	BEGIN
		SELECT s.*, r.SeatId FROM dbo.Shows s
		LEFT JOIN dbo.Reservations r ON r.ShowId = s.Id AND r.IsCanceled = 0
		WHERE s.Id = @ShowId;
	END
	ELSE
	BEGIN
		SELECT s.*, r.SeatId FROM dbo.Shows s
		LEFT JOIN dbo.Reservations r ON r.ShowId = s.Id AND r.IsCanceled = 0;
	END;

	RETURN 0;
END
