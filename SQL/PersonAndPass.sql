--Create a database named "PBKDF2"
DROP TABLE IF EXISTS recovery;
DROP TABLE IF EXISTS Pass;
DROP TABLE IF EXISTS Person;

CREATE TABLE Person(
UserID int IDENTITY (1,1) NOT NULL,
Email nvarchar(30) NOT NULL,
Address nvarchar(20) NOT NULL,
Address2 nvarchar(30),
City nvarchar (20) NOT NULL,
State nvarchar (20),
Zip nvarchar (20)
PRIMARY KEY (UserID));

CREATE TABLE Pass(
UserID int FOREIGN KEY references Person(UserID) NOT NULL,
Email nvarchar(30) NOT NULL,
PasswordHash nvarchar(256) NOT NULL,
PRIMARY KEY (UserID));

CREATE TABLE recovery (
UserID int FOREIGN KEY references Pass(UserID) NOT NULL,
randomHash nvarchar(50) NOT NULL,
dateStamp nvarchar(20) NOT NULL,
Primary Key (UserID, randomHash)
);
