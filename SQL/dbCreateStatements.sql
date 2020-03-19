DDL Diagram
Example table below to use:

CREATE TABLE Pass(
UserID int FOREIGN KEY references Person(UserID) NOT NULL, 
Email nvarchar(30) NOT NULL,
PasswordHash nvarchar(256) NOT NULL,
PRIMARY KEY (UserID)
);

--#########################--
-- START OF DATABASE --
--#########################--
	
    USE master;

    DROP DATABASE IF EXISTS ServitaeARDE;
    CREATE DATABASE ServitaeARDE;
    GO

    USE ServitaeARDE;

    ALTER AUTHORIZATION ON DATABASE::ServitaeARDE TO SA;
    GO

--#########################--
-- STORED PROCEDURES --
--#########################--


--#########################--
-- DROP TABLES -- 
--#########################--


--#########################--
--CREATE TABLES --
--#########################--


--#########################--
-- INSERT STATEMENTS --
--#########################--


--#########################--
-- RESEEDS --
--#########################--

