CREATE PROCEDURE dbo.GetShows
	@ShowId INT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	IF @ShowId IS NOT NULL
	BEGIN
		SELECT s.*, rs.SeatId 
		FROM dbo.Shows s 
		LEFT JOIN dbo.Reservations r ON r.ShowId = s.Id 
		JOIN dbo.ReservationSeats rs ON rs.ReservationId  = r.Id
		WHERE s.Id = @ShowId;
	END
	ELSE
	BEGIN
		SELECT s.*, rs.SeatId 
		FROM dbo.Shows s 
		LEFT JOIN dbo.Reservations r ON r.ShowId = s.Id 
		JOIN dbo.ReservationSeats rs ON rs.ReservationId  = r.Id;
	END;

	RETURN 0;
END
