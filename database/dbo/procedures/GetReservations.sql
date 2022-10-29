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
        SELECT r.*, s.*, se.*, u.*, au.* 
        FROM dbo.Reservations r
        JOIN dbo.Shows s ON s.Id = r.ShowId
        JOIN dbo.Seats se ON se.Id = r.SeatId
        JOIN dbo.Users u ON u.Id = r.UserId
        LEFT JOIN dbo.Users au ON au.Id = r.AdminUserId
        WHERE r.Id = @ReservationId;
    END
    ELSE IF @ShowId IS NOT NULL
    BEGIN
        SELECT r.*, s.*, se.*, u.*, au.* 
        FROM dbo.Reservations r
        JOIN dbo.Shows s ON s.Id = r.ShowId
        JOIN dbo.Seats se ON se.Id = r.SeatId
        JOIN dbo.Users u ON u.Id = r.UserId
        LEFT JOIN dbo.Users au ON au.Id = r.AdminUserId
        WHERE r.ShowId = @ShowId;
    END
    ELSE IF @UserId IS NOT NULL
    BEGIN
        SELECT r.*, s.*, se.*, u.*, au.* 
        FROM dbo.Reservations r
        JOIN dbo.Shows s ON s.Id = r.ShowId
        JOIN dbo.Seats se ON se.Id = r.SeatId
        JOIN dbo.Users u ON u.Id = r.UserId
        LEFT JOIN dbo.Users au ON au.Id = r.AdminUserId
        WHERE r.UserId = @UserId;
    END
    ELSE
    BEGIN
        SELECT r.*, s.*, se.*, u.*, au.* 
        FROM dbo.Reservations r
        JOIN dbo.Shows s ON s.Id = r.ShowId
        JOIN dbo.Seats se ON se.Id = r.SeatId
        JOIN dbo.Users u ON u.Id = r.UserId
        LEFT JOIN dbo.Users au ON au.Id = r.AdminUserId;
    END;
    RETURN 0;
END
