CREATE PROCEDURE dbo.GetUserByLogin
	@ProviderName NVARCHAR(255),
	@ProviderKey NVARCHAR(255)
AS
BEGIN
	
	SELECT 
		u.* 
	FROM dbo.Users u
	JOIN dbo.UserLogins ul ON u.Id = ul.UserId
	WHERE ul.ProviderName = @ProviderName 
	AND ul.ProviderKey = @ProviderKey;

	RETURN 0;
END		
