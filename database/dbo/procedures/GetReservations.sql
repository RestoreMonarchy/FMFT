CREATE PROCEDURE dbo.GetReservations
	@ReservationId CHAR(8) = NULL,
    @ShowId INT = NULL,
    @UserId INT = NULL,
    @OrderId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
	SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    IF @ShowId IS NOT NULL AND @UserId IS NOT NULL
    BEGIN
        SELECT r.*, s.*, u.*, au.*, rd.*, rs.*, s2.*
        FROM dbo.Reservations r
        JOIN dbo.Shows s ON s.Id = r.ShowId
        LEFT JOIN dbo.Users u ON u.Id = r.UserId
        LEFT JOIN dbo.Users au ON au.Id = r.AdminUserId
        LEFT JOIN dbo.ReservationDetails rd ON rd.ReservationId = r.Id
        LEFT JOIN dbo.ReservationSeats rs ON rs.ReservationId = r.Id
        LEFT JOIN dbo.Seats s2 ON s2.Id = rs.SeatId
        WHERE r.ShowId = @ShowId AND r.UserId = @UserId
        ORDER BY r.Id, rs.Id;
    END
    ELSE IF @ReservationId IS NOT NULL
    BEGIN
        SELECT r.*, s.*, u.*, au.*, rd.*, rs.*, s2.*
        FROM dbo.Reservations r
        JOIN dbo.Shows s ON s.Id = r.ShowId
        LEFT JOIN dbo.Users u ON u.Id = r.UserId
        LEFT JOIN dbo.Users au ON au.Id = r.AdminUserId
        LEFT JOIN dbo.ReservationDetails rd ON rd.ReservationId = r.Id
        LEFT JOIN dbo.ReservationSeats rs ON rs.ReservationId = r.Id
        LEFT JOIN dbo.Seats s2 ON s2.Id = rs.SeatId
        WHERE r.Id = @ReservationId
        ORDER BY r.Id, rs.Id;
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
        WHERE r.ShowId = @ShowId
        ORDER BY r.Id, rs.Id;
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
        WHERE r.UserId = @UserId
        ORDER BY r.Id, rs.Id;
    END
    ELSE IF @OrderId IS NOT NULL
    BEGIN
        SELECT r.*, s.*, u.*, au.*, rd.*, rs.*, s2.*
        FROM dbo.Reservations r
        JOIN dbo.Shows s ON s.Id = r.ShowId
        LEFT JOIN dbo.Users u ON u.Id = r.UserId
        LEFT JOIN dbo.Users au ON au.Id = r.AdminUserId
        LEFT JOIN dbo.ReservationDetails rd ON rd.ReservationId = r.Id
        LEFT JOIN dbo.ReservationSeats rs ON rs.ReservationId = r.Id
        LEFT JOIN dbo.Seats s2 ON s2.Id = rs.SeatId
        WHERE r.OrderId = @OrderId
        ORDER BY r.Id, rs.Id;
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
        LEFT JOIN dbo.Seats s2 ON s2.Id = rs.SeatId
        ORDER BY r.Id, rs.Id;
    END;
    RETURN 0;
END
