CREATE PROCEDURE dbo.CancelReservation
	@ReservationId CHAR(8)
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @isCanceled BIT;
	DECLARE @retValue INT = 0;
	DECLARE @resId CHAR(8) = '--------';
	

	SELECT
		@resId = Id, 
		@isCanceled = IsCanceled 
	FROM dbo.Reservations 
	WHERE Id = @ReservationId

	IF @resId = '--------'
	BEGIN
		-- reservation doesn't exist
		SET @retValue = 1;
	END
	ELSE IF @isCanceled = 1
	BEGIN
		-- reservation was already canceled
		SET @retValue = 2;
	END

	IF @retValue = 0
	BEGIN
		UPDATE dbo.Reservations 
		SET IsCanceled = 1 
		WHERE Id = @ReservationId;
	END

	EXEC dbo.GetReservations @ReservationId = @resId;
	RETURN @retValue;
END
