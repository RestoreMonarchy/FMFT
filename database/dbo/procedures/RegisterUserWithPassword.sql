CREATE PROCEDURE dbo.RegisterUserWithPassword
	@Email NVARCHAR(320),
	@FirstName NVARCHAR(255),
	@LastName NVARCHAR(255),	
	@Role VARCHAR(255),
	@PasswordHash NVARCHAR(128)
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF EXISTS(SELECT * FROM dbo.Users WHERE Email = @Email)
		RETURN 1;

	INSERT INTO dbo.Users (Email, FirstName, LastName, Role, PasswordHash)
	VALUES (@Email, @FirstName, @LastName, @Role, @PasswordHash);

	SELECT * FROM dbo.Users WHERE Id = SCOPE_IDENTITY();

	RETURN 0;
END