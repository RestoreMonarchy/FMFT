﻿CREATE TABLE dbo.Users
(
	Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Users PRIMARY KEY,
	Email NVARCHAR(320) NOT NULL,
	FirstName NVARCHAR(255) NOT NULL,
	LastName NVARCHAR(255) NULL,
	Role INT NOT NULL CONSTRAINT DF_Users_Role DEFAULT 0,
	PasswordHash NVARCHAR(128) NULL,
	IsPasswordEnabled AS CONVERT(BIT,CASE WHEN PasswordHash IS NOT NULL THEN 1 ELSE 0 END),
	CultureId INT NOT NULL CONSTRAINT DF_Users_CultureId DEFAULT 0,
	ConfirmEmailSecret UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_Users_ConfirmEmailSecret DEFAULT NEWID(),
	IsEmailConfirmed BIT NOT NULL CONSTRAINT DF_Users_IsEmailConfirmed DEFAULT 0,
	ConfirmEmailSendDate DATETIME2(0) NULL,
	CreateDate DATETIME2(0) NOT NULL CONSTRAINT DF_Users_CreateDate DEFAULT SYSDATETIME(),
	CONSTRAINT UK_Users_Email UNIQUE (Email)
);