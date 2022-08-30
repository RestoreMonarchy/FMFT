CREATE PROCEDURE dbo.UpdateUserRole
	@UserId INT,
	@Role INT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @currentRole INT;
	
	IF NOT EXISTS(SELECT * FROM dbo.Users WHERE Id = @UserId)
	BEGIN
		RETURN 1;
	END

	SELECT @currentRole = [Role] FROM dbo.Users WHERE Id = @UserId;

	IF @currentRole = @Role
	BEGIN
		RETURN 2;
	END

	UPDATE dbo.Users SET [Role] = @Role WHERE Id = @UserId;
	
	RETURN 0;
END
