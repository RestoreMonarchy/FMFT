CREATE PROCEDURE dbo.GetShows
	@ShowId INT = NULL,
	@Expired BIT = 1,
	@Disabled BIT = 1
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	IF @ShowId IS NOT NULL
	BEGIN
		SELECT s.*, ri.SeatId 
		FROM dbo.Shows s 
		LEFT JOIN dbo.Reservations r ON r.ShowId = s.Id AND r.IsValid = 1
		LEFT JOIN dbo.ReservationItems ri ON ri.ReservationId = r.Id
		WHERE s.Id = @ShowId
		AND (@Expired = 1 OR s.EndDateTime > SYSDATETIME())
		AND (@Disabled = 1 OR s.IsEnabled = 1);
	END
	ELSE
	BEGIN
		SELECT s.*, ri.SeatId 
		FROM dbo.Shows s 
		LEFT JOIN dbo.Reservations r ON r.ShowId = s.Id AND r.IsValid = 1
		LEFT JOIN dbo.ReservationItems ri ON ri.ReservationId = r.Id
		WHERE (@Expired = 1 OR s.EndDateTime > SYSDATETIME())
		AND (@Disabled = 1 OR s.IsEnabled = 1);
	END;

	RETURN 0;
END
