-- ######################### --
-- START OF DATABASE --
-- ######################### --
    
    USE master;

    DROP DATABASE IF EXISTS ServitaeARDE;
    CREATE DATABASE ServitaeARDE;
    GO

    USE ServitaeARDE;

    ALTER AUTHORIZATION ON DATABASE::ServitaeARDE TO SA;
    GO

-- ######################### --
-- STORED PROCEDURES --
-- ######################### --

-- function that duplicates this database into a backup database, executable by the admin only

    CREATE PROCEDURE DatabaseBackup
    AS
        USE master
        DROP DATABASE IF EXISTS BackupServitae;
        CREATE DATABASE BackupServitae;
        USE BackupServitae;
        SELECT * into BackupServitae.dbo.Date FROM ServitaeARDE.dbo.Date;
        SELECT * into BackupServitae.dbo.Location FROM ServitaeARDE.dbo.Location;
        SELECT * into BackupServitae.dbo.LatLng FROM SertvitaeARDE.dbo.LatLng;
        SELECT * into BackupServitae.dbo.User FROM ServitaeARDE.dbo.User;
        SELECT * into BackupServitae.dbo.Password FROM ServitaeARDE.dbo.Password;
        SELECT * into BackupServitae.dbo.RecoveryPass FROM ServitaeARDE.dbo.RecoveryPass;
        SELECT * into BackupServitae.dbo.TeamMember FROM ServitaeARDE.dbo.TeamMember;
        SELECT * into BackupServitae.dbo.ProjectControls FROM ServitaeARDE.dbo.ProjectControls;
        SELECT * into BackupServitae.dbo.ProjectLeader FROM ServitaeARDE.dbo.ProjectLeader;
        SELECT * into BackupServitae.dbo.Authenticator FROM ServitaeARDE.dbo.Authenticator;
        SELECT * into BackupServitae.dbo.ARDE FROM ServitaeARDE.dbo.ARDE;
        SELECT * into BackupServitae.dbo.Project FROM ServitaeARDE.dbo.Project;
        SELECT * into BackupServitae.dbo.Observation FROM ServitaeARDE.dbo.Observation;    
    GO;

-- ######################### --
-- Triggers --
-- ######################### --

    -- trigger that clears the recovery passwords after 10 days, this will help with storage costs
-- trigger that executes every morning, looks into RecoverPass table for a User with more than 10 records and deletes 80% of the least most recent records      



-- ######################### --
-- DROP TABLES -- 
-- ######################### --

    DROP TABLE IF EXISTS Observation;
    DROP TABLE IF EXISTS Project;
    DROP TABLE IF EXISTS ARDE;
    DROP TABLE IF EXISTS Authenticator;
    DROP TABLE IF EXISTS Researcher;
    DROP TABLE IF EXISTS ProjectLeader;
    DROP TABLE IF EXISTS ProjectControls;
    DROP TABLE IF EXISTS TeamMember;
    DROP TABLE IF EXISTS RecoveryPass;
    DROP TABLE IF EXISTS Password;
    DROP TABLE IF EXISTS User;
    DROP TABLE IF EXISTS LatLng;
    DROP TABLE IF EXISTS Location;
    DROP TABLE IF EXISTS Date;


-- ######################### --
-- CREATE TABLES --
-- ######################### --


 -- Create Date
    CREATE TABLE Date(
        DateKey int PRIMARY KEY identity(1, 1) NOT NULL,
        DateStamp datetime NOT NULL,
        LastUpdated int references Date(DateKey) NOT NULL,
        LastUpdatedBy nvarchar(30) NOT NULL        
    );

    -- Create Location
    CREATE TABLE Location(
        LocationID int PRIMARY KEY identity(1, 1) NOT NULL,
        StreetAddress nvarchar(50) NOT NULL,
        City nvarchar(30) NOT NULL,
        State nvarchar(2),
        Country nvarchar(3) NOT NULL,
        PostalCode nvarchar(10) NOT NULL,
        LastUpdated int references Date(DateKey) NOT NULL,
        LastUpdatedBy nvarchar(30) NOT NULL
    );
    
    -- Create User
    CREATE TABLE User(
        UserID int PRIMARY KEY identity(1, 1) NOT NULL,
        FirstName nvarchar(30) NOT NULL, 
        MiddleName nvarchar(30),
        LastName nvarchar(30) NOT NULL,
        Phone nvarchar(15),
        Email nvarchar(30) NOT NULL,
        AddressID int references Location(LocationID) NOT NULL,
        Address2ID int references Location(LocationID),
        ActiveUser Boolean NOT NULL,
        LastUpdated int references Date(DateKey) NOT NULL,
        LastUdpdatedBy int references User(UserID) NOT NULL
    );
    
    -- Create LatLng
    CREATE TABLE LatLng(
        LatLngID int PRIMARY KEY identity(1, 1) NOT NULL,
        Latitude nvarchar(30) NOT NULL,
        Longitude nvarchar(30) NOT NULL,
        LastUpdated int references Date(DateKey) NOT NULL,
        LastUpdatedBy int references User(UserID) NOT NULL
    );
    
    -- Create Password
    CREATE TABLE Password(
        UserID int PRIMARY KEY references User(UserID) NOT NULL,
        Email nvarchar(30) NOT NULL,
        PasswordHash nvarchar(256) NOT NULL,
        LastUpdated int references Date(DateKey) NOT NULL,
        LastUdpdatedBy int references User(UserID) NOT NULL 
    );
    
    -- Create Recovery Password
    CREATE TABLE RecoveryPass(
        UserID int references User(UserID) NOT NULL,
        RandomHash nvarchar(256) NOT NULL,
        DateStamp nvarchar(30) NOT NULL,
        LastUpdated int references Date(DateKey) NOT NULL,
        LastUdpdatedBy int references User(UserID) NOT NULL,
        CONSTRAINT PRIMARY KEY (UserID, RandomHash)
    );
    
    -- Create Team Member
    CREATE TABLE TeamMember(
        TeamMemberID int PRIMARY KEY identity(1, 1) NOT NULL,
        UserID int references User(UserID) NOT NULL,
        TemporaryAccessKey nvarchar(256) NOT NULL,
        LastUpdated int references Date(DateKey) NOT NULL,
        LastUdpdatedBy int references User(UserID) NOT NULL
    );
    
    -- Create Project Controls
    
    -- Create Project Leader
    CREATE TABLE ProjectLeader(
        ProjectLeadID int PRIMARY KEY identity(1, 1) NOT NULL,
        UserID int references User(UserID) NOT NULL,
        BoxAccessKey nvarchar(256) NOT NULL,
        ProjectControlsID int references ProjectControl(ProjectControlsID) NOT NULL,
        LastUpdated int references Date(DateKey) NOT NULL,
        LastUdpdatedBy int references User(UserID) NOT NULL
    );
    
    -- Create Authenticator
    CREATE TABLE Authenticator(
        AuthenticatorID int PRIMARY KEY identity(1, 1) NOT NULL,
        UserID int references User(UserID) NOT NULL,
        VerificationCredentials nvarchar(256) NOT NULL,
        LastUpdated int references Date(DateKey) NOT NULL,
        LastUdpdatedBy int references User(UserID) NOT NULL
    );
    
    -- Create ARDE
    CREATE TABLE ARDE(
        BoxSerialID int PRIMARY KEY identity(1, 1) NOT NULL,
        UserID int references User(UserID) NOT NULL,
        DeviceNickname nvarchar(30) NOT NULL,
        BatteryStatus nvarchar(30),
        HasAnimal Boolean,
        IsActiveBox Boolean,
        Bluetooth Boolean,
        Wifi Boolean,
        Mode nvarchar(30),
        IntervalDelay nvarchar(30),
        NumberImages int,
        WeightUnit nvarchar(10),
        Media nvarchar(256),
        PurchaseDate int references Date(DateKey) NOT NULL,
        PurchaseHistory nvarchar(256),
        LatLngID int references LatLng(LatLngID),
        LastUpdated int references Date(DateKey),
        LastUdpdatedBy int references User(UserID)
    );
    
    -- Create Project
    CREATE TABLE Project(
        ProjectID int PRIMARY KEY identity(1, 1) NOT NULL,
        BoxSerialID int references ARDE(BoxSerialID) NOT NULL,
        UserID int references User(UserID) NOT NULL,
        ProjectLeader int references ProjectLeader(ProjectLeaderID) NOT NULL,
        ProjectTitle nvarchar(30) NOT NULL,
        ProjectUserLimit int DEFAULT 1,
        ProjectDescription nvarchar(256) NOT NULL,
        LastUpdated int references Date(DateKey) NOT NULL,
        LastUdpdatedBy int references User(UserID) NOT NULL
    );
    
    -- Create Observation
    CREATE TABLE Observation(
        RecordNum int PRIMARY KEY identity(1, 1) NOT NULL,
        ProjectID int references Project(ProjectID) NOT NULL,
        UserID int references User(UserID) NOT NULL,
        BoxSerialID int references ARDE(BoxSerialID) NOT NULL,
        Genus nvarchar(30),
        Species nvarchar(30),
        CommonName nvarchar(30),
        Size double,
        Weight double,
        Temperature double,
        Humidity double,
        Media nvarchar(256) NOT NULL,
-- indicates whether an authenticator has validated the data yet
-- FALSE if they have not, TRUE if they have
        ValidateStatus Boolean DEFAULT False,
        DateTimeArrival int references Date(DateKey) NOT NULL,
        DateTimeRelease int references Date(DateKey) NOT NULL,
        LocationID int references Location(LocationID) NOT NULL,
        UploadDateTime int references Date(DateKey) NOT NULL,
        LastUpdated int references Date(DateKey) NOT NULL,
        LastUdpdatedBy int references User(UserID) NOT NULL
    );
    

-- ######################### --
-- INSERT STATEMENTS --
-- ######################### --


-- ######################### --
-- RESEEDS --
-- ######################### --

    -- example of how to reseed an identity value
    -- DBCC CHECKIDENT (Date, RESEED, 0);
