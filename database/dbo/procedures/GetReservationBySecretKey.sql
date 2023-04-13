CREATE PROCEDURE dbo.GetReservationBySecretKey 
	@SecretCode UNIQUEIDENTIFIER,
	@ReservationItemId INT OUTPUT,
	@ScanDate DATETIME2(0) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @reservationId CHAR(8);

	SELECT 
		@ReservationItemId = Id,
		@reservationId = ReservationId
	FROM dbo.ReservationItems
	WHERE SecretCode = @SecretCode;	

	IF @ReservationItemId IS NOT NULL
	BEGIN
		SELECT 
			@ScanDate = ScanDate
		FROM dbo.ReservationItems 
		WHERE Id = @ReservationItemId;

		-- mark reservation item (ticket) as scanned
		IF @ScanDate IS NULL
		BEGIN
			UPDATE dbo.ReservationItems 
			SET ScanDate = SYSDATETIME()
			WHERE Id = @ReservationItemId;
		END
	END

	IF @reservationId IS NULL
	BEGIN

		SELECT @reservationId = Id
		FROM dbo.Reservations
		WHERE SecretCode = @SecretCode;

	END;

	IF @reservationId IS NULL
	BEGIN
		EXEC dbo.GetReservations @ReservationId = '--------';

		RETURN 1;
	END;

	EXEC dbo.GetReservations @ReservationId = @reservationId;
	
	RETURN 0;

END;
