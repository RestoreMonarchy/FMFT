CREATE PROCEDURE dbo.GetReservations
	@ReservationId CHAR(8) = NULL,
    @ShowId INT = NULL,
    @UserId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
	SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    IF @ReservationId IS NOT NULL
    BEGIN
        SELECT r.*, s.*, u.*, au.*, rd.*, rs.*, s2.*
        FROM dbo.Reservations r
        JOIN dbo.Shows s ON s.Id = r.ShowId
        LEFT JOIN dbo.Users u ON u.Id = r.UserId
        LEFT JOIN dbo.Users au ON au.Id = r.AdminUserId
        LEFT JOIN dbo.ReservationDetails rd ON rd.ReservationId = r.Id
        LEFT JOIN dbo.ReservationSeats rs ON rs.ReservationId = r.Id
        LEFT JOIN dbo.Seats s2 ON s2.Id = rs.SeatId
        WHERE r.Id = @ReservationId;
    END
    ELSE IF @ShowId IS NOT NULL
    BEGIN
        SELECT r.*, s.*, u.*, au.*, rd.*, rs.*, s2.*
        FROM dbo.Reservations r
        JOIN dbo.Shows s ON s.Id = r.ShowId
        LEFT JOIN dbo.Users u ON u.Id = r.UserId
        LEFT JOIN dbo.Users au ON au.Id = r.AdminUserId
        LEFT JOIN dbo.ReservationDetails rd ON rd.ReservationId = r.Id
        LEFT JOIN dbo.ReservationSeats rs ON rs.ReservationId = r.Id
        LEFT JOIN dbo.Seats s2 ON s2.Id = rs.SeatId
        WHERE r.ShowId = @ShowId;
    END
    ELSE IF @UserId IS NOT NULL
    BEGIN
        SELECT r.*, s.*, u.*, au.*, rd.*, rs.*, s2.*
        FROM dbo.Reservations r
        JOIN dbo.Shows s ON s.Id = r.ShowId
        LEFT JOIN dbo.Users u ON u.Id = r.UserId
        LEFT JOIN dbo.Users au ON au.Id = r.AdminUserId
        LEFT JOIN dbo.ReservationDetails rd ON rd.ReservationId = r.Id
        LEFT JOIN dbo.ReservationSeats rs ON rs.ReservationId = r.Id
        LEFT JOIN dbo.Seats s2 ON s2.Id = rs.SeatId
        WHERE r.UserId = @UserId;
    END
    ELSE
    BEGIN
        SELECT r.*, s.*, u.*, au.*, rd.*, rs.*, s2.*
        FROM dbo.Reservations r
        JOIN dbo.Shows s ON s.Id = r.ShowId
        LEFT JOIN dbo.Users u ON u.Id = r.UserId
        LEFT JOIN dbo.Users au ON au.Id = r.AdminUserId
        LEFT JOIN dbo.ReservationDetails rd ON rd.ReservationId = r.Id
        LEFT JOIN dbo.ReservationSeats rs ON rs.ReservationId = r.Id
        LEFT JOIN dbo.Seats s2 ON s2.Id = rs.SeatId;
    END;
    RETURN 0;
END
