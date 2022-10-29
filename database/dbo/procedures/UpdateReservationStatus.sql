CREATE PROCEDURE [dbo].[UpdateReservationStatus]
	@ReservationId CHAR(8),
    @ReservationStatus INT,
    @UpdateStatusDate DATETIME2(0),
    @AdminUserId INT NULL
AS
BEGIN
    SET NOCOUNT ON;
	SET XACT_ABORT ON;
	
    DECLARE @ret INT = 0;
    DECLARE @id CHAR(8) = '--------';

    IF NOT EXISTS (SELECT * FROM dbo.Reservations WHERE Id = @ReservationId)
        SET @ret = 1;

    IF @ret = 0
    BEGIN
        SET @id = @ReservationId;

        UPDATE dbo.Reservations 
        SET Status = @ReservationStatus, 
        UpdateStatusDate = @UpdateStatusDate,
        AdminUserId = @AdminUserId
        WHERE Id = @id;
    END;

    EXEC dbo.GetReservations @ReservationId = @id;    
    RETURN @ret;
END