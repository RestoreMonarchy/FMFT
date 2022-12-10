CREATE TABLE dbo.ReservationDetails
(
	ReservationId CHAR(8) NOT NULL CONSTRAINT FK_ReservationDetails_ReservationId FOREIGN KEY REFERENCES dbo.Reservations(Id) ON DELETE CASCADE,
	Email NVARCHAR(255) NULL,
	FirstName NVARCHAR(255) NULL,
	LastName NVARCHAR(255) NULL,
	CONSTRAINT PK_ReservationDetails PRIMARY KEY (ReservationId)
)
