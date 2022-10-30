CREATE PROCEDURE dbo.CancelReservation
	@ReservationId CHAR(8)
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	UPDATE dbo.Reservations 
	SET IsCanceled = 1 
	WHERE Id = @ReservationId;

	EXEC dbo.GetReservations @ReservationId = @ReservationId;
	RETURN 0;
END
