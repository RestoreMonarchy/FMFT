CREATE PROCEDURE dbo.UpdateUserCulture
	@UserId INT,
	@CultureId INT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @currentCultureId INT;
	
	IF NOT EXISTS(SELECT * FROM dbo.Users WHERE Id = @UserId)
	BEGIN
		RETURN 1;
	END

	SELECT @currentCultureId = CultureId FROM dbo.Users WHERE Id = @UserId;

	IF @currentCultureId = @CultureId
	BEGIN
		RETURN 2;
	END

	UPDATE dbo.Users SET CultureId = @CultureId WHERE Id = @UserId;
	
	RETURN 0;
END
