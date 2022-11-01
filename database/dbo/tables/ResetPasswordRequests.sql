CREATE TABLE dbo.ResetPasswordRequests
(
	Id INT NOT NULL IDENTITY (1, 1) CONSTRAINT PK_ResetPasswordRequests PRIMARY KEY,
	UserId INT NOT NULL CONSTRAINT FK_ResetPasswordRequests_UserId FOREIGN KEY REFERENCES dbo.Users(Id),
	SecretKey UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_ResetPasswordRequests_SecretKey DEFAULT (NEWID()),
	ExpireDate DATETIME2(0) NOT NULL,
	IsExpired AS CAST(IIF(ExpireDate > SYSDATETIME(), 0, 1) AS BIT),
	ResetDate DATETIME2(0) NULL,
	IsReset AS CAST(IIF(ResetDate IS NULL, 0, 1) AS BIT),
	CreateDate DATETIME2(0) NOT NULL CONSTRAINT DF_ResetPasswordRequests_CreateDate DEFAULT (SYSDATETIME()),
	CONSTRAINT UK_ResetPasswordRequests UNIQUE (SecretKey)
);
