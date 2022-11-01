CREATE PROCEDURE dbo.CreateResetPasswordRequest
	@Email NVARCHAR(320),
	@ExpireDate DATETIME2(0)
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;
		
	DECLARE @userId INT;
	DECLARE @ret INT = 0;

	SELECT @userId = Id
	FROM dbo.Users
	WHERE Email = @Email;

	IF @@ROWCOUNT = 0
	BEGIN
		PRINT FORMATMESSAGE(N'Could not find user %s', @Email);
		SET @ret = 1;
	END
	ELSE IF 4 < (SELECT COUNT(*) FROM dbo.ResetPasswordRequests WHERE UserId = @userId AND IsExpired = 0 AND IsReset = 0) 
	BEGIN

		PRINT FORMATMESSAGE(N'There are already 5 active reset password requests for the user %s', @Email);
		SET @ret = 2;
	END
	ELSE IF EXISTS(SELECT * FROM dbo.Users WHERE Id = @userId AND PasswordHash IS NULL)
	BEGIN
		PRINT FORMATMESSAGE(N'There''s no password authentication enabled for the user %s', @Email);
		SET @ret = 3;
	END
	ELSE
	BEGIN
		INSERT INTO dbo.ResetPasswordRequests (UserId, ExpireDate) 
		VALUES (@userId, @ExpireDate);
	END;

	SELECT 
		r.*, 
		u.* 
	FROM dbo.ResetPasswordRequests r 
	JOIN dbo.Users u ON u.Id = r.UserId 
	WHERE r.Id = SCOPE_IDENTITY() 
	AND @ret = 0;

	RETURN @ret;

END;
