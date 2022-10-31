CREATE PROCEDURE dbo.ConfirmUserEmail
	@UserId INT,
	@ConfirmSecret UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON; 
	SET XACT_ABORT ON;

	DECLARE @userIdFromConfirmSecret INT;
	DECLARE @isEmailConfirmed BIT;

	SELECT
		@userIdFromConfirmSecret = Id,
		@isEmailConfirmed = IsEmailConfirmed
	 FROM dbo.Users
	 WHERE ConfirmEmailSecret = @ConfirmSecret;

	 IF @userIdFromConfirmSecret <> @UserId OR @userIdFromConfirmSecret IS NULL
	 BEGIN
		RETURN 1;
	 END;

	 IF @isEmailConfirmed = 1
	 BEGIN
		RETURN 2;
	 END;

	UPDATE dbo.Users 
	SET IsEmailConfirmed = 1
	WHERE Id = @UserId;

	RETURN 0;

END;
