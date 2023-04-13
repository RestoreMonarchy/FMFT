CREATE TABLE dbo.ReservationDetails
(
	Id INT IDENTITY(1,1) NOT NULL,
	ReservationId CHAR(8) NOT NULL CONSTRAINT FK_ReservationDetails_ReservationId FOREIGN KEY REFERENCES dbo.Reservations(Id) ON DELETE CASCADE,
	Email NVARCHAR(255) NULL,
	FirstName NVARCHAR(255) NULL,
	LastName NVARCHAR(255) NULL,
	CONSTRAINT PK_ReservationDetails PRIMARY KEY (Id)
)
