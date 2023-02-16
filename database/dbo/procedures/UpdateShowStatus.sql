CREATE PROCEDURE dbo.UpdateShowStatus
	@ShowId INT,
	@IsEnabled BIT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @ret INT = 0;

	UPDATE dbo.Shows 
	SET 
		IsEnabled = @IsEnabled,
		UpdateDate = SYSDATETIME()
	WHERE Id = @ShowId;

	IF @@ROWCOUNT = 0
	BEGIN
		SET @ret = 1;
	END

	EXEC dbo.GetShows @ShowId = @ShowId;
	RETURN @ret;
END