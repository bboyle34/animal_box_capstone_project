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
    DROP TABLE IF EXISTS Location;
    DROP TABLE IF EXISTS Date;

-- ######################### --
-- CREATE TABLES --
-- ######################### --

    -- Create Date
    CREATE TABLE Date(
        DateKey int PRIMARY KEY identity(1, 1),
        FullDateAlternateKey nvarchar(30),
        DateStamp datetime,
        DayOfMonth nvarchar(30),
        MonthOfYear nvarchar(30),
        CalendarYear nvarchar(30),
        HourOfDay nvarchar(30),
        MinuteOfHour nvarchar(30),
        SeconfOfMinute nvarchar(30),
        LastUpdated int references Date(DateKey) NOT NULL,
        -- LastUpdatedBy nvarchar(30)        
    );
    
        -- Create Location
    CREATE TABLE Location(
        LocationID int PRIMARY KEY identity(1, 1),
        StreetAddress nvarchar(30),
        City nvarchar(30),
        State nvarchar(30),
        Country nvarchar(30),
        Zip nvarchar(30),
        Region nvarchar(30),
        Latitude nvarchar(30),
        Longitude nvarchar(30),
        LastUpdated int references Date(DateKey),
        -- LastUpdatedBy int references User(UserID)
    );
    
    -- Create User
    CREATE TABLE User(
        UserID int PRIMARY KEY identity(1, 1),
        FirstName nvarchar(30), 
        MiddleName nvarchar(30),
        LastName nvarchar(30),
        Phone nvarchar(30),
        Email nvarchar(30),
        AddressID int references Location(LocationID),
        Address2ID int references Location(LocationID),\
        ActiveUser Boolean,
        LastUpdated int references Date(DateKey),
        LastUdpdatedBy int references User(UserID)
    );
    
    -- Create Password
    CREATE TABLE Password(
        UserID int PRIMARY KEY references User(UserID) NOT NULL,
        Email nvarchar(30),
        PasswordHash nvarchar(256),
        LastUpdated int references Date(DateKey),
        LastUdpdatedBy int references User(UserID) 
    );
    
    -- Create Recovery Password
    CREATE TABLE RecoveryPass(
        UserID int references User(UserID) NOT NULL,
        RandomHash nvarchar(256),
        DateStamp nvarchar(30),
        LastUpdated int references Date(DateKey),
        LastUdpdatedBy int references User(UserID),
        CONSTRAINT PRIMARY KEY (UserID, RandomHash)
    );
    
    -- Create Team Member
    CREATE TABLE TeamMember(
        TeamMemberID int PRIMARY KEY identity(1, 1),
        UserID int references User(UserID) NOT NULL,
        TemporaryAccessKey nvarchar(256) NOT NULL,
        LastUpdated int references Date(DateKey),
        LastUdpdatedBy int references User(UserID)
    );
    
    -- Create Project Controls
    
    -- Create Project Leader
    CREATE TABLE ProjectLeader(
        ProjectLeadID int PRIMARY KEY identity(1, 1),
        UserID int references User(UserID) NOT NULL,
        BoxAccessKey nvarchar(256) NOT NULL,
        ProjectControlsID int references ProjectControl(ProjectControlsID),
        LastUpdated int references Date(DateKey),
        LastUdpdatedBy int references User(UserID)
    );
    
    -- Create Authenticator
    CREATE TABLE Authenticator(
        AuthenticatorID int PRIMARY KEY identity(1, 1),
        UserID int references User(UserID) NOT NULL,
        VerificationCredentials nvarchar(256),
        LastUpdated int references Date(DateKey),
        LastUdpdatedBy int references User(UserID)
    );
    
    -- Create ARDE
    CREATE TABLE ARDE(
        BoxSerialID int PRIMARY KEY identity(1, 1),
        UserID int references User(UserID) NOT NULL,
        DeviceNickname nvarchar(30),
        BatteryStatus nvarchar(30),
        HasAnimal Boolean,
        IsActiveBox Boolean,
        Bluetooth nvarchar(30),
        Wifi nvarchar(30),
        Mode nvarchar(30),
        IntervalDelay nvarchar(30),
        NumberImages nvarchar(30),
        WeightUNit nvarchar(30),
        Media nvarchar(30)
        PurchaseDate int references Date(DateKey),
        PurchaseHistory nvarchar(256),
        LocationID int references Location(LocationID),
        LastUpdated int references Date(DateKey),
        LastUdpdatedBy int references User(UserID)
    );
    
    -- Create Project
    CREATE TABLE Project(
        ProjectID int PRIMARY KEY identity(1, 1),
        BoxSerialID int references ARDE(BoxSerialID) NOT NULL,
        UserID int references User(UserID) NOT NULL,
        ProjectLeader int references ProjectLeader(ProjectLeaderID) NOT NULL,
        ProjectTitle nvarchar(30),
        ProjectUserLimit int,
        ProjectDescription nvarchar(256),
        LastUpdated int references Date(DateKey),
        LastUdpdatedBy int references User(UserID)
    );
    
    -- Create Observation
    CREATE TABLE Observation(
        RecordNum int PRIMARY KEY identity(1, 1),
        ProjectID int references Project(ProjectID) NOT NULL,
        UserID int references User(UserID) NOT NULL,
        BoxSerialID int references ARDE(BoxSerialID) NOT NULL,
        Genus nvarchar(30),
        Species nvarchar(30),
        CommonName nvarchar(30),
        Size decimal,
        Weight decimal,
        Temperature decimal,
        Humidity decimal,
        Media nvarchar(30),
        ValidateStatus Boolean,
        DateTimeArrival int references Date(DateKey),
        DateTimeRelease int references Date(DateKey),
        LocationID int references Location(LocationID),
        UploadDateTime int references Date(DateKey),
        LastUpdated int references Date(DateKey),
        LastUdpdatedBy int references User(UserID)
    );

    

-- ######################### --
-- INSERT STATEMENTS --
-- ######################### --


-- ######################### --
-- RESEEDS --
-- ######################### --

    -- example of how to reseed an identity value
    -- DBCC CHECKIDENT (Date, RESEED, 0);
