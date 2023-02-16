CREATE PROCEDURE dbo.UpdateShowSellingDetails
	@ShowId INT,
	@SellStartDateTime DATETIME2(0)
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @ret INT = 0;

	UPDATE dbo.Shows 
	SET 
		SellStartDateTime = @SellStartDateTime,
		UpdateDate = SYSDATETIME()
	WHERE Id = @ShowId;

	IF @@ROWCOUNT = 0
	BEGIN
		SET @ret = 1;
	END

	EXEC dbo.GetShows @ShowId = @ShowId;
	RETURN @ret;
END