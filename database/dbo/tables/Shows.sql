﻿CREATE TABLE dbo.Shows
(
	Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Shows PRIMARY KEY,
	[Name] NVARCHAR(255) NOT NULL,
	[Description] NVARCHAR(4000) NULL,
	StartDateTime DATETIME2(0) NOT NULL,
	EndDateTime DATETIME2(0) NOT NULL,
	AuditoriumId INT NOT NULL CONSTRAINT FK_Shows_AuditoriumId FOREIGN KEY REFERENCES dbo.Auditoriums(Id),
	CreateDate DATETIME2(0) NOT NULL CONSTRAINT DF_Shows_CreateDate DEFAULT SYSDATETIME()
)
