CREATE TABLE dbo.Reservations
(
	Id CHAR(8) NOT NULL,
	SecretCode UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_Reservations_SecretCode DEFAULT(NEWID()),
	ShowId INT NOT NULL CONSTRAINT FK_Reservations_ShowId FOREIGN KEY REFERENCES dbo.Shows(Id),
	UserId INT NULL CONSTRAINT FK_Reservations_UserId FOREIGN KEY REFERENCES dbo.Users(Id),
	OrderId INT NULL CONSTRAINT FK_Reservations_OrderId FOREIGN KEY REFERENCES dbo.Orders(Id),
	[Status] TINYINT NOT NULL CONSTRAINT DF_Reservations_Status DEFAULT 0,
	IsValid AS CONVERT(BIT,CASE [Status] WHEN (0) THEN (1) WHEN (1) THEN (1) ELSE (0) END),
	CreateDate DATETIME2(0) NOT NULL CONSTRAINT DF_Reservations_CreateDate DEFAULT SYSDATETIME(),
	UpdateStatusDate DATETIME2(0) NULL,
	AdminUserId INT NULL CONSTRAINT FK_Reservations_AdminUserId FOREIGN KEY REFERENCES dbo.Users(Id),
	CONSTRAINT PK_Reservations PRIMARY KEY (Id),
	CONSTRAINT UK_Reservations_SecretCode UNIQUE (SecretCode),
	INDEX IDX_UserId (UserId),
	INDEX IDX_CreateDate (CreateDate),
	INDEX IDX_OrderId (OrderId),
	INDEX IDX_ShowId (ShowId)
)
