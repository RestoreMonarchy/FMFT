CREATE PROCEDURE dbo.ResetPassword
	@SecretKey UNIQUEIDENTIFIER,
	@NewPassword NVARCHAR(255)
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @id INT;
	DECLARE @userId INT;
	DECLARE @isReset BIT;
	DECLARE @isExpired BIT;

	SELECT 
		@id = Id,
		@userId = UserId,
		@isReset = IsReset,
		@isExpired = IsExpired
	FROM dbo.ResetPasswordRequests
	WHERE SecretKey = @SecretKey;

	IF @@ROWCOUNT = 0
	BEGIN
		PRINT FORMATMESSAGE('Reset password request not found for the request key %s', CAST(@SecretKey AS VARCHAR(36)));
		RETURN 1;
	END;

	IF @isReset = 1
	BEGIN
		PRINT FORMATMESSAGE('Password has already been reset for the request key %s', CAST(@SecretKey AS VARCHAR(36)));
		RETURN 2;
	END;

	IF @isExpired = 1
	BEGIN
		PRINT FORMATMESSAGE('Password reset request %s expired', CAST(@SecretKey AS VARCHAR(36)));
		RETURN 3;
	END;

	IF ISNULL(@NewPassword, '') = ''
	BEGIN
		PRINT 'New password cannot be empty';
		RETURN 4;
	END;

	BEGIN TRAN;
	
	UPDATE dbo.Users 
	SET PasswordHash = @NewPassword
	WHERE Id = @userId;

	UPDATE dbo.ResetPasswordRequests
	SET ResetDate = SYSDATETIME()
	WHERE Id = @id;

	COMMIT;

	RETURN 0;
END;