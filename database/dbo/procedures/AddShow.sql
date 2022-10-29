﻿CREATE PROCEDURE dbo.AddShow
	@PublicId VARCHAR(255),
	@Name NVARCHAR(255),
	@Description NVARCHAR(4000),
	@StartDateTime DATETIME2,
	@EndDateTime DATETIME2,
	@AuditoriumId INT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @ret INT = 0;
	DECLARE @id INT = 0;

	IF NOT EXISTS(SELECT * FROM dbo.Auditoriums WHERE Id = @AuditoriumId)
		SET @ret = 1;

	IF @ret = 0
	BEGIN
		INSERT INTO dbo.Shows (PublicId, Name, Description, StartDateTime, EndDateTime, AuditoriumId)
		VALUES (@PublicId, @Name, @Description, @StartDateTime, @EndDateTime, @AuditoriumId);

		SET @id = SCOPE_IDENTITY();
	END;
	
	EXEC dbo.GetShows @ShowId = @id;

	RETURN @ret;
END;
