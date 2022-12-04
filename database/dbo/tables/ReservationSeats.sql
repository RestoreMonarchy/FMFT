CREATE TABLE dbo.ReservationSeats
(
	Id INT IDENTITY(1, 1) NOT NULL CONSTRAINT PK_ReservationSeats PRIMARY KEY,
	SecretCode UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_ReservationSeats_SecretCode DEFAULT(NEWID()),
	ReservationId CHAR(8) NOT NULL CONSTRAINT FK_ReservationSeats_ReservationId FOREIGN KEY REFERENCES dbo.Reservations(Id),
	SeatId INT NOT NULL CONSTRAINT FK_ReservationSeats_SeatId FOREIGN KEY REFERENCES dbo.Seats(Id),
	CONSTRAINT UK_ReservationSeats UNIQUE (SecretCode)
);