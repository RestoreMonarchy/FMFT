CREATE TABLE dbo.Seats
(
	Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Seats PRIMARY KEY,
	[Row] SMALLINT NOT NULL,
	Number SMALLINT NOT NULL,
	AuditoriumId INT NOT NULL CONSTRAINT FK_Seats_AuditoriumId FOREIGN KEY REFERENCES dbo.Auditoriums(Id),
	CONSTRAINT UK_Seats_RowNumberAuditoriumId UNIQUE ([Row], Number, AuditoriumId)
)
