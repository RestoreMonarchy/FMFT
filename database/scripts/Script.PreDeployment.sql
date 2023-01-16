IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
	WHERE TABLE_NAME = 'Reservations'
	AND TABLE_SCHEMA = 'dbo' 
	AND COLUMN_NAME = 'IsCanceled') 
BEGIN
	BEGIN TRAN;
	EXEC ('
		UPDATE dbo.Reservations SET Status = 3 WHERE IsCanceled = 1;
		UPDATE dbo.Reservations SET Status = 1 WHERE IsCanceled = 0;

		ALTER TABLE dbo.Reservations DROP CONSTRAINT DF_Reservations_IsCanceled;
		ALTER TABLE dbo.Reservations DROP COLUMN IsCanceled;

		ALTER TABLE dbo.Reservations DROP CONSTRAINT DF_Reservations_Status;
		ALTER TABLE dbo.Reservations ALTER COLUMN [Status] TINYINT NOT NULL;
		ALTER TABLE dbo.Reservations ADD CONSTRAINT DF_Reservations_Status DEFAULT 0 FOR [Status];
	');

	COMMIT;
END;