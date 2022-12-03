CREATE TABLE dbo.ReservationSeats
(
	Id INT IDENTITY(1, 1) NOT NULL CONSTRAINT PK_ReservationSeats PRIMARY KEY,
	ReservationId CHAR(8) NOT NULL CONSTRAINT FK_ReservationSeats_ReservationId FOREIGN KEY REFERENCES dbo.Reservations(Id),
	SeatId INT NOT NULL CONSTRAINT FK_ReservationSeats_SeatId FOREIGN KEY REFERENCES dbo.Seats(Id)
);