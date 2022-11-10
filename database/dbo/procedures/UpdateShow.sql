﻿CREATE PROCEDURE dbo.UpdateShow
	@Id INT,
	@Name NVARCHAR(255),
	@Description NVARCHAR(4000),
	@StartDateTime DATETIME2,
	@EndDateTime DATETIME2,
	@AuditoriumId INT,
	@ThumbnailMediaId UNIQUEIDENTIFIER
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
			ThumbnailMediaId = @ThumbnailMediaId
		WHERE Id = @Id;
	END;

	EXEC dbo.GetShows @ShowId = @Id;

	RETURN @ret;
END;
