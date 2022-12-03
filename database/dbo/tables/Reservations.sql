CREATE TABLE dbo.Reservations
(
	Id CHAR(8) NOT NULL,
	ShowId INT NOT NULL CONSTRAINT FK_Reservations_ShowId FOREIGN KEY REFERENCES dbo.Shows(Id),
	SeatId INT NULL CONSTRAINT FK_Reservations_SeatId FOREIGN KEY REFERENCES dbo.Seats(Id),
	UserId INT NOT NULL CONSTRAINT FK_Reservations_UserId FOREIGN KEY REFERENCES dbo.Users(Id),
	[Status] INT NOT NULL CONSTRAINT DF_Reservations_Status DEFAULT 0,
	IsCanceled BIT NOT NULL CONSTRAINT DF_Reservations_IsCanceled DEFAULT 0,
	CreateDate DATETIME2(0) NOT NULL CONSTRAINT DF_Reservations_CreateDate DEFAULT SYSDATETIME(),
	UpdateStatusDate DATETIME2(0) NULL,
	AdminUserId INT NULL CONSTRAINT FK_Reservations_AdminUserId FOREIGN KEY REFERENCES dbo.Users(Id),
	CONSTRAINT PK_Reservations PRIMARY KEY (Id),
	INDEX UI_Reservations_SeatShowId UNIQUE (ShowId, SeatId) WHERE IsCanceled = 0
)
