CREATE PROCEDURE dbo.UpdateShow
	@Id INT,
	@Name NVARCHAR(255),
	@Description NVARCHAR(4000),
	@StartDateTime DATETIME2(0),
	@EndDateTime DATETIME2(0),
	@AuditoriumId INT,
	@ThumbnailMediaId UNIQUEIDENTIFIER,
	@SellStartDateTime DATETIME2(0),
	@IsEnabled BIT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @ret INT = 0;

	IF NOT EXISTS(SELECT * FROM dbo.Auditoriums WHERE Id = @AuditoriumId)
		SET @ret = 1;

	IF NOT EXISTS(SELECT * FROM dbo.Shows WHERE Id = @Id)
		SET @ret = 2;

	IF @ret = 0
	BEGIN
		UPDATE dbo.Shows 
		SET 
			[Name] = @Name, 
			[Description] = @Description, 
			StartDateTime = @StartDateTime, 
			EndDateTime = @EndDateTime,
			AuditoriumId = @AuditoriumId,
			ThumbnailMediaId = @ThumbnailMediaId,
			SellStartDateTime = @SellStartDateTime,
			IsEnabled = @IsEnabled,
			UpdateDate = SYSDATETIME()
		WHERE Id = @Id
		AND ([Name] <> @Name
		OR ISNULL([Description], '') <> ISNULL(@Description, '')
		OR StartDateTime <> @StartDateTime
		OR EndDateTime <> @EndDateTime
		OR AuditoriumId <> @AuditoriumId
		OR (ThumbnailMediaId <> @ThumbnailMediaId 
			OR ThumbnailMediaId IS NULL AND @ThumbnailMediaId IS NOT NULL
			OR ThumbnailMediaId IS NOT NULL AND @ThumbnailMediaId IS NULL)
		OR SellStartDateTime <> @SellStartDateTime
		OR IsEnabled <> @IsEnabled);
	END;

	EXEC dbo.GetShows @ShowId = @Id;

	RETURN @ret;
END;
