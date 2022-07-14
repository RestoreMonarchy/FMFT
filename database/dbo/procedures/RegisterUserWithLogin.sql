CREATE PROCEDURE dbo.RegisterUserWithLogin
	@Email NVARCHAR(320),
	@FirstName NVARCHAR(255),
	@LastName NVARCHAR(255),
	@Role VARCHAR(255),
	@ProviderName NVARCHAR(255),
	@ProviderKey NVARCHAR(255),
	@IsEmailConfirmed BIT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF EXISTS(SELECT * FROM dbo.Users WHERE Email = @Email)
	BEGIN
		RETURN 1;
	END

	IF EXISTS(SELECT * FROM dbo.UserLogins WHERE ProviderName = @ProviderName AND ProviderKey = @ProviderKey)
	BEGIN
		RETURN 2;
	END

	BEGIN TRAN;

	INSERT INTO dbo.Users (Email, FirstName, LastName, Role, IsEmailConfirmed)
	VALUES (@Email, @FirstName, @LastName, @Role, @IsEmailConfirmed);

	DECLARE @userId INT = SCOPE_IDENTITY();

	INSERT INTO dbo.UserLogins (UserId, ProviderName, ProviderKey)
	VALUES (@userId, @ProviderName, @ProviderKey);

	COMMIT;

	SELECT * FROM dbo.Users WHERE Id = @userId;

	RETURN 0;
END
