CREATE PROCEDURE [dbo].[UpdateReservationStatus]
	@ReservationId INT,
    @ReservationStatus INT,
    @UpdateStatusDate DATETIME2(0),
    @AdminUserId INT NULL
AS
BEGIN
    SET NOCOUNT ON;
	SET XACT_ABORT ON;
	
    DECLARE @ret INT = 0;

    IF NOT EXISTS (SELECT * FROM dbo.Reservations WHERE Id = @ReservationId)
        SET @ret = 1;

    IF @ret = 0
    BEGIN
        UPDATE dbo.Reservations 
        SET Status = @ReservationStatus, 
        UpdateStatusDate = @UpdateStatusDate,
        AdminUserId = @AdminUserId
        WHERE Id = @ReservationId;
    END;

    EXEC dbo.GetReservations @ReservationId = @ReservationId;    
    RETURN @ret;
END