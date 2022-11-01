CREATE PROCEDURE dbo.UpdateUserPassword
	@UserId INT,
	@PasswordHash NVARCHAR(128)
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF NOT EXISTS(SELECT * FROM dbo.Users WHERE Id = @UserId AND PasswordHash IS NOT NULL)
	BEGIN
		RETURN 1;
	END;

	UPDATE dbo.Users SET PasswordHash = @PasswordHash WHERE Id = @UserId;

	RETURN 0;
END