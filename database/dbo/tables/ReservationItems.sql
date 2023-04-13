CREATE TABLE dbo.ReservationItems
(
	Id INT IDENTITY(1, 1) NOT NULL CONSTRAINT PK_ReservationItems PRIMARY KEY,
	SecretCode UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_ReservationItems_SecretCode DEFAULT(NEWID()),
	ReservationId CHAR(8) NOT NULL CONSTRAINT FK_ReservationItems_ReservationId FOREIGN KEY REFERENCES dbo.Reservations(Id),
	SeatId INT NULL CONSTRAINT FK_ReservationItems_SeatId FOREIGN KEY REFERENCES dbo.Seats(Id),
	ShowProductId INT NOT NULL CONSTRAINT FK_ReservationItems_ShowProductId FOREIGN KEY REFERENCES dbo.ShowProducts(Id),
	IsScanned AS CONVERT(BIT, CASE WHEN ScanDate IS NULL THEN 0 ELSE 1 END),
	ScanDate DATETIME2(0) NULL,
	CONSTRAINT UK_ReservationItems UNIQUE (SecretCode)
);