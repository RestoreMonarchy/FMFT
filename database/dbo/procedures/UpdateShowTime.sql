CREATE PROCEDURE dbo.UpdateShowTime
	@ShowId INT,
	@StartDateTime DATETIME2(0),
	@EndDateTime DATETIME2(0)
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @ret INT = 0;

	UPDATE dbo.Shows 
	SET 
		StartDateTime = @StartDateTime,
		EndDateTime = @EndDateTime,
		UpdateDate = SYSDATETIME()
	WHERE Id = @ShowId
	AND (StartDateTime <> @StartDateTime
	OR EndDateTime <> @EndDateTime);

	IF @@ROWCOUNT = 0 AND NOT EXISTS(SELECT * FROM dbo.Shows WHERE Id = @ShowId)
	BEGIN
		SET @ret = 1;
	END

	EXEC dbo.GetShows @ShowId = @ShowId;
	RETURN @ret;
END
