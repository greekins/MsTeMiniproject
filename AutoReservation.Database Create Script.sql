PRINT N'Creating Tables';
PRINT N'-------------------';
PRINT N'';

PRINT N'Creating Database...';

IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'AutoReservation')
BEGIN
	USE [master]

	-- Kill all Connections to Database
	DECLARE @Spid 					INT,
			@SpidCurrentConnection	INT,
			@Sql 					NVARCHAR(MAX),
			@DatabaseName			NVARCHAR(MAX)

	SET @DatabaseName			= DB_NAME()
	SET @SpidCurrentConnection	= @@SPID

	DECLARE CurrentProcess CURSOR FOR 
		SELECT 		p.SPID 
		FROM 		sys.databases		db 
		INNER JOIN 	sys.sysprocesses 	p	ON db.database_id = p.dbid
		WHERE 		db.name = @DatabaseName 
			AND 	p.SPID > 50 
			AND		p.SPID <> @SpidCurrentConnection

	OPEN CurrentProcess FETCH NEXT FROM CurrentProcess INTO @Spid 
	WHILE @@FETCH_STATUS = 0 
	BEGIN 
		SET @Sql = 'KILL ' + CONVERT(NVARCHAR(30), @Spid) 
		PRINT @Sql 
		EXECUTE(@Sql) 
	FETCH NEXT FROM CurrentProcess INTO @Spid END CLOSE CurrentProcess DEALLOCATE CurrentProcess

	-- Drop Database
	DROP DATABASE AutoReservation
END

CREATE DATABASE AutoReservation

GO

USE [AutoReservation]


PRINT N'Creating dbo.Reservation...';

IF EXISTS(SELECT name FROM sys.tables WHERE name = 'Reservation')
DROP TABLE [dbo].[Reservation]
CREATE TABLE [dbo].[Reservation] (
    [Id]      INT      IDENTITY (1, 1) NOT NULL,
    [AutoId]  INT      NOT NULL,
    [KundeId] INT      NOT NULL,
    [Von]     DATETIME NOT NULL,
    [Bis]     DATETIME NOT NULL
);


PRINT N'Creating dbo.Auto...';

IF EXISTS(SELECT name FROM sys.tables WHERE name = 'Auto')
DROP TABLE [dbo].[Auto]
CREATE TABLE [dbo].[Auto] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Marke]      NVARCHAR (20) NOT NULL,
    [AutoKlasse] INT           NOT NULL,
    [Tagestarif] INT           NOT NULL,
    [Basistarif] INT           NULL
);


PRINT N'Creating dbo.Kunde...';

IF EXISTS(SELECT name FROM sys.tables WHERE name = 'Kunde')
DROP TABLE [dbo].[Kunde]
CREATE TABLE [dbo].[Kunde] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Nachname]     NVARCHAR (20) NOT NULL,
    [Vorname]      NVARCHAR (20) NOT NULL,
    [Geburtsdatum] DATETIME      NOT NULL
) ON [PRIMARY];


PRINT N'Creating dbo.PK_Auto...';

ALTER TABLE [dbo].[Auto]
    ADD CONSTRAINT [PK_Auto] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY];

PRINT N'Creating dbo.PK_Kunde...';

ALTER TABLE [dbo].[Kunde]
    ADD CONSTRAINT [PK_Kunde] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY];

PRINT N'Creating dbo.PK_Reservation...';

ALTER TABLE [dbo].[Reservation]
    ADD CONSTRAINT [PK_Reservation] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY];

PRINT N'Creating dbo.FK_Reservation_Auto...';

ALTER TABLE [dbo].[Reservation]
    ADD CONSTRAINT [FK_Reservation_Auto] FOREIGN KEY ([AutoId]) REFERENCES [dbo].[Auto] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION;

PRINT N'Creating dbo.FK_Reservation_Kunde...';

ALTER TABLE [dbo].[Reservation]
    ADD CONSTRAINT [FK_Reservation_Kunde] FOREIGN KEY ([KundeId]) REFERENCES [dbo].[Kunde] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION;
	
PRINT N'Generating Test Data'

SET IDENTITY_INSERT Auto ON
INSERT INTO Auto (Id, Marke, AutoKlasse, Tagestarif, Basistarif)
    SELECT 1, 'Fiat Punto', 2, 50, 0 UNION
    SELECT 2, 'VW Golf', 1, 120, 0 UNION
    SELECT 3, 'Audi S6', 0, 180, 50
SET IDENTITY_INSERT Auto OFF

SET IDENTITY_INSERT Kunde ON
INSERT INTO Kunde (Id, Nachname, Vorname, Geburtsdatum)
    SELECT 1, 'Nass', 'Anna', '1961-05-05 00:00:00' UNION
    SELECT 2, 'Beil', 'Timo', '1980-09-09 00:00:00' UNION
    SELECT 3, 'Pfahl', 'Martha', '1950-07-03 00:00:00' UNION
    SELECT 4, 'Zufall', 'Rainer', '1944-11-11 00:00:00'
SET IDENTITY_INSERT Kunde OFF
	
SET IDENTITY_INSERT Reservation ON
INSERT INTO Reservation (Id, AutoId, KundeId, Von, Bis)				
   SELECT 1, 1, 1, '2020-01-10 00:00:00', '2020-01-20 00:00:00' UNION				
   SELECT 2, 2, 2, '2020-01-10 00:00:00', '2020-01-20 00:00:00' UNION				
   SELECT 3, 3, 3, '2020-01-10 00:00:00', '2020-01-20 00:00:00'				
SET IDENTITY_INSERT Reservation OFF
				
PRINT N'';
PRINT N'-------------------';
PRINT N'Script end...';