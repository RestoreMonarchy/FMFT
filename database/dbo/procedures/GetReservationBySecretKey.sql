CREATE PROCEDURE dbo.GetReservationBySecretKey 
	@SecretCode UNIQUEIDENTIFIER,
	@ReservationSeatId INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @reservationId CHAR(8);

	SELECT 
		@ReservationSeatId = Id,
		@reservationId = ReservationId
	FROM dbo.ReservationItems
	WHERE SecretCode = @SecretCode;

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
