USE master
GO

DECLARE @SQL nvarchar(1000);
IF EXISTS (SELECT 1 FROM sys.databases WHERE name = N'RoyalParking')
BEGIN
    SET @SQL = N'USE RoyalParking;

                 ALTER DATABASE RoyalParking SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                 USE master;

                 DROP DATABASE RoyalParking;';
    EXEC (@SQL);
END;

CREATE DATABASE RoyalParking
GO

USE RoyalParking
GO

CREATE TABLE [User] (
	Id INT IDENTITY(1,1) NOT NULL,
	Username VARCHAR(50) NOT NULL,
	PasswordHash VARCHAR(200) NOT NULL,
	Salt VARCHAR(200) NOT NULL,
	FirstName VARCHAR(255) NOT NULL,
	LastName VARCHAR(255) NOT NULL,
	Email VARCHAR(255) NOT NULL,
	Phone CHAR(10) NOT NULL,
	CreateDate DATETIME NOT NULL,
	UpdateDate DATETIME NULL,
	CONSTRAINT PK_User PRIMARY KEY(Id),
	CONSTRAINT UC_User_Username UNIQUE(Username),
	CONSTRAINT UC_User_Email UNIQUE(Email),
	CONSTRAINT UC_User_Phone UNIQUE(Phone),
)
GO

CREATE TABLE Customer (
	Id INT IDENTITY(1,1) NOT NULL,
	UserId INT NOT NULL,
	CONSTRAINT PK_Customer PRIMARY KEY(Id),
	CONSTRAINT FK_Customer_User FOREIGN KEY(UserId) REFERENCES [User](Id),
	CONSTRAINT UC_Customer_UserId UNIQUE(UserId),
)
GO

CREATE TABLE Valet (
	Id INT IDENTITY(1,1) NOT NULL,
	UserId INT NOT NULL,
	CONSTRAINT PK_Valet PRIMARY KEY(Id),
	CONSTRAINT FK_Valet_User FOREIGN KEY(UserId) REFERENCES [User](Id),
	CONSTRAINT UC_Valet_UserId UNIQUE(UserId),
)
GO

CREATE TABLE Rate (
	Id INT IDENTITY(1,1) NOT NULL,
	WeekdayRate DECIMAL(13,2) NOT NULL,
	SaturdaySundayRate DECIMAL(13,2) NOT NULL,
	SpecialEventRate DECIMAL(13,2) NOT NULL,
	CONSTRAINT PK_Rate PRIMARY KEY(Id),
) 
GO

INSERT INTO Rate(WeekdayRate,SaturdaySundayRate,SpecialEventRate) VALUES(7,9,14);

CREATE TABLE ParkingSpot (
	Id INT IDENTITY(1,1) NOT NULL,
	[Number] VARCHAR(50) NOT NULL,
	CONSTRAINT PK_ParkingSpot PRIMARY KEY(Id),
	CONSTRAINT UC_ParkingSpot_Number UNIQUE([Number]),
)
GO

INSERT INTO ParkingSpot([Number]) VALUES('A1');
INSERT INTO ParkingSpot([Number]) VALUES('A2');
INSERT INTO ParkingSpot([Number]) VALUES('A3');
INSERT INTO ParkingSpot([Number]) VALUES('A4');
INSERT INTO ParkingSpot([Number]) VALUES('A5');
INSERT INTO ParkingSpot([Number]) VALUES('A6');
INSERT INTO ParkingSpot([Number]) VALUES('A7');
INSERT INTO ParkingSpot([Number]) VALUES('A8');
INSERT INTO ParkingSpot([Number]) VALUES('A9');
INSERT INTO ParkingSpot([Number]) VALUES('A10');
INSERT INTO ParkingSpot([Number]) VALUES('A11');
INSERT INTO ParkingSpot([Number]) VALUES('A12');
INSERT INTO ParkingSpot([Number]) VALUES('B1');
INSERT INTO ParkingSpot([Number]) VALUES('B2');
INSERT INTO ParkingSpot([Number]) VALUES('B3');
INSERT INTO ParkingSpot([Number]) VALUES('B4');
INSERT INTO ParkingSpot([Number]) VALUES('B5');
INSERT INTO ParkingSpot([Number]) VALUES('B6');
INSERT INTO ParkingSpot([Number]) VALUES('B7');
INSERT INTO ParkingSpot([Number]) VALUES('B8');
INSERT INTO ParkingSpot([Number]) VALUES('B9');
INSERT INTO ParkingSpot([Number]) VALUES('B10');
INSERT INTO ParkingSpot([Number]) VALUES('B11');
INSERT INTO ParkingSpot([Number]) VALUES('B12');
INSERT INTO ParkingSpot([Number]) VALUES('C1');
INSERT INTO ParkingSpot([Number]) VALUES('C2');
INSERT INTO ParkingSpot([Number]) VALUES('C3');
INSERT INTO ParkingSpot([Number]) VALUES('C4');
INSERT INTO ParkingSpot([Number]) VALUES('C5');
INSERT INTO ParkingSpot([Number]) VALUES('C6');
INSERT INTO ParkingSpot([Number]) VALUES('C7');
INSERT INTO ParkingSpot([Number]) VALUES('C8');
INSERT INTO ParkingSpot([Number]) VALUES('C9');
INSERT INTO ParkingSpot([Number]) VALUES('C10');
INSERT INTO ParkingSpot([Number]) VALUES('C11');
INSERT INTO ParkingSpot([Number]) VALUES('C12');

CREATE TABLE Vehicle (
	Id INT IDENTITY(1,1) NOT NULL,
	CustomerId INT NOT NULL,
	Make VARCHAR(100) NOT NULL,
	Model VARCHAR(100) NOT NULL,
	[Year] CHAR(4) NOT NULL,
	LicensePlate CHAR(6) NOT NULL,
	StateLicensedIn CHAR(2) NOT NULL,
	Color VARCHAR(100) NOT NULL,
	CreateDate DATETIME NOT NULL,
	UpdateDate DATETIME NULL,
	CONSTRAINT PK_Vehicle PRIMARY KEY(Id),
	CONSTRAINT FK_Vehicle_Customer FOREIGN KEY(CustomerId) REFERENCES Customer(Id),
	CONSTRAINT UC_Vehicle_License UNIQUE(LicensePlate, StateLicensedIn),
)
GO

CREATE TABLE ParkingStatus (
	Id INT IDENTITY(1,1) NOT NULL,
	[Status] VARCHAR(20) NOT NULL 
	CONSTRAINT PK_ParkingStatus PRIMARY KEY(Id),
	CONSTRAINT UC_ParkingStatus_Status UNIQUE([Status]),
)
GO

INSERT INTO ParkingStatus([Status]) VALUES('ParkingRequested');
INSERT INTO ParkingStatus([Status]) VALUES('VehicleParked');
INSERT INTO ParkingStatus([Status]) VALUES('PickupRequested');
INSERT INTO ParkingStatus([Status]) VALUES('VehiclePickedUp');

CREATE TABLE Discount (
	Id INT IDENTITY(1,1) NOT NULL,
	[Type]	VARCHAR(255) NOT NULL,
	[Description] VARCHAR(255) NOT NULL,
	Multiplier DECIMAL(13,2) NOT NULL,
	CONSTRAINT PK_Discount PRIMARY KEY(Id),
	CONSTRAINT UC_Discount_Type UNIQUE([Type]),
)
GO

INSERT INTO Discount([Type],[Description],Multiplier) VALUES('Military','Discount for active and former military',0.85);
INSERT INTO Discount([Type],[Description],Multiplier) VALUES('Senior','Discount for seniors aged 55+',0.9);
INSERT INTO Discount([Type],[Description],Multiplier) VALUES('Student','Discount for college students, student ID required',0.93);

CREATE TABLE ParkingSlip (
	Id INT IDENTITY(1,1) NOT NULL,
	VehicleId INT NOT NULL,
	ValetId INT NULL,
	ParkingSpotId INT NULL,
	ParkingStatusId INT NOT NULL,
	RateId INT NOT NULL,
	DiscountId INT NULL,
	TimeIn DATETIME NOT NULL,
	[TimeOut] DATETIME NULL,
	AmountDue DECIMAL(13,2) NULL,
	AmountPaid DECIMAL(13,2) NULL,
	CreateDate DATETIME NOT NULL,
	UpdateDate DATETIME NULL,
	SavedDetails VARCHAR(2500) NULL,
	CONSTRAINT PK_ParkingSlip PRIMARY KEY(Id),
	CONSTRAINT FK_ParkingSlip_Vehicle FOREIGN KEY(VehicleId) REFERENCES Vehicle(Id),
	CONSTRAINT FK_ParkingSlip_Valet FOREIGN KEY(ValetId) REFERENCES Valet(Id),
	CONSTRAINT FK_ParkingSlip_ParkingSpot FOREIGN KEY(ParkingSpotId) REFERENCES ParkingSpot(Id),
	CONSTRAINT FK_ParkingSlip_ParkingStatus FOREIGN KEY(ParkingStatusId) REFERENCES ParkingStatus(Id),
	CONSTRAINT FK_ParkingSlip_Rate FOREIGN KEY(RateId) REFERENCES Rate(Id),
	CONSTRAINT FK_ParkingSlip_Discount FOREIGN KEY(DiscountId) REFERENCES Discount(Id),
)
GO

