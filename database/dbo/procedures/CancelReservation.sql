CREATE PROCEDURE dbo.CancelReservation
	@ReservationId CHAR(8)
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @canceledReservationStatus TINYINT = 3;

	DECLARE @isCanceled BIT;
	DECLARE @retValue INT = 0;
	DECLARE @resId CHAR(8) = '--------';
	
	SELECT
		@resId = Id, 
		@isCanceled = CASE [Status] WHEN @canceledReservationStatus THEN 1 ELSE 0 END
	FROM dbo.Reservations 
	WHERE Id = @ReservationId;

	IF @resId = '--------'
	BEGIN
		-- reservation doesn't exist
		SET @retValue = 1;
	END
	ELSE IF @isCanceled = 1
	BEGIN
		-- reservation was already canceled
		SET @retValue = 2;
	END;

	IF @retValue = 0
	BEGIN
		UPDATE dbo.Reservations 
		SET 
			[Status] = @canceledReservationStatus,
			UpdateStatusDate = SYSDATETIME()
		WHERE Id = @ReservationId;
	END;

	EXEC dbo.GetReservations @ReservationId = @resId;
	RETURN @retValue;
END