CREATE PROCEDURE [dbo].[UpdateReservationStatus]
	@ReservationId INT,
    @ReservationStatus INT,
    @UpdateStatusDate DATETIME2(0),
    @AdminUserId INT NULL
AS
BEGIN
    SET NOCOUNT ON;
	SET XACT_ABORT ON;
	

    UPDATE dbo.Reservations 
    SET Status = @ReservationStatus, 
    UpdateStatusDate = @UpdateStatusDate,
    AdminUserId = @AdminUserId
    WHERE Id = @ReservationId;

    EXEC dbo.GetReservations @ReservationId = @ReservationId;    
    RETURN 0;
END