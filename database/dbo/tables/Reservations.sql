CREATE TABLE dbo.Reservations
(
	Id CHAR(8) NOT NULL,
	SecretCode UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_Reservations_SecretCode DEFAULT(NEWID()),
	ShowId INT NOT NULL CONSTRAINT FK_Reservations_ShowId FOREIGN KEY REFERENCES dbo.Shows(Id),
	UserId INT NULL CONSTRAINT FK_Reservations_UserId FOREIGN KEY REFERENCES dbo.Users(Id),
	[Status] INT NOT NULL CONSTRAINT DF_Reservations_Status DEFAULT 0,
	IsCanceled BIT NOT NULL CONSTRAINT DF_Reservations_IsCanceled DEFAULT 0,
	CreateDate DATETIME2(0) NOT NULL CONSTRAINT DF_Reservations_CreateDate DEFAULT SYSDATETIME(),
	UpdateStatusDate DATETIME2(0) NULL,
	AdminUserId INT NULL CONSTRAINT FK_Reservations_AdminUserId FOREIGN KEY REFERENCES dbo.Users(Id),
	CONSTRAINT PK_Reservations PRIMARY KEY (Id),
	CONSTRAINT UK_Reservations_SecretCode UNIQUE (SecretCode)
)
