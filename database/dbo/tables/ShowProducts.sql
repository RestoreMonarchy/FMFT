﻿CREATE TABLE dbo.ShowProducts
(
	Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_ShowProducts PRIMARY KEY,
	ShowId INT NOT NULL CONSTRAINT FK_ShowProducts_ShowId FOREIGN KEY REFERENCES dbo.Shows(Id),
	[Name] NVARCHAR(255) NOT NULL,
	Price DECIMAL(9,2) NOT NULL,
	IsBulk BIT NOT NULL CONSTRAINT DF_ShowProducts_IsBulk DEFAULT 0,
	Quantity SMALLINT NOT NULL CONSTRAINT DF_ShowProducts_Quantity DEFAULT 0,
	IsEnabled BIT NOT NULL
) 
