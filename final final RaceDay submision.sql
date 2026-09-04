--CREATE DATABASE RaceDay;

--CREATE TABLE ORGANISERS(
--OrganiserID INT NOT NULL PRIMARY KEY,
--OrganiserName VARCHAR(30) NOT NULL,
--OrganiserSurname VARCHAR(30) NOT NULL,
--OrganiserAddress VARCHAR(60) NOT NULL
--);

--CREATE TABLE PARTICIPANTS(
--ParticipantID INT NOT NULL PRIMARY KEY,
--ParticipantName VARCHAR(30) NOT NULL,
--ParticipantSurname VARCHAR(30) NOT NULL,
--ParticipantEmail VARCHAR(30),
--ParticipantContact INT NOT NULL
--);

--CREATE TABLE EVENTS(
--EventID INT NOT NULL PRIMARY KEY,
--EventName VARCHAR(80) NOT NULL,
--[Description] VARCHAR(100) NOT NULL,
--[Date] VARCHAR(9) NOT NULL,
--[Location] VARCHAR(40) NOT NULL,
--Distance VARCHAR(5000) NOT NULL,
--EventType VARCHAR(30),
--OrganiserID INT NOT NULL,

--FOREIGN KEY (OrganiserID) REFERENCES ORGANISERS(OrganiserID)
--);



--CREATE TABLE CATEGORIES(
--CategoryID INT NOT NULL PRIMARY KEY,
--CategoryName VARCHAR(30) NOT NULL,
--Age INT NOT NULL,
--OrganiserID INT NOT NULL,
--EventID INT NOT NULL,
SELECT * FROM CATEGORIES;

--FOREIGN KEY (OrganiserID) REFERENCES ORGANISERS(OrganiserID),
--FOREIGN KEY (EventID) REFERENCES EVENTS(EventID)
--);

--CREATE TABLE ENROLMENT(
--EnrolmentID INT NOT NULL PRIMARY KEY,
--EventID INT NOT NULL,
--ParticipantID INT NOT NULL,
--OrganiserID INT NOT NULL,
--CategoryID INT NOT NULL,
--EnrolDate VARCHAR(9) NOT NULL,

--FOREIGN KEY (EventID) REFERENCES EVENTS(EventID),
--FOREIGN KEY (ParticipantID) REFERENCES PARTICIPANTS(ParticipantID),
--FOREIGN KEY (OrganiserID) REFERENCES ORGANISERS(OrganiserID),
--FOREIGN KEY (CategoryID) REFERENCES CATEGORIES(CategoryID)
--);

--CREATE TABLE RESULTS(
--ResultID INT NOT NULL PRIMARY KEY,
--Finish_time VARCHAR(20) NOT NULL,
--Finishing_Positions INT NOT NULL,
--EnrolmentID INT NOT NULL,

--FOREIGN KEY (EnrolmentID) REFERENCES ENROLMENT(EnrolmentID)
--);

--CREATE TABLE HISTORY(
--HistoryID INT NOT NULL PRIMARY KEY,
--ResultID INT NOT NULL,
--ParticipantID INT NOT NULL,
--ViewDate VARCHAR(9) NOT NULL,

--FOREIGN KEY (ResultID) REFERENCES RESULTS(ResultID),
--FOREIGN KEY (ParticipantID) REFERENCES PARTICIPANTS(ParticipantID)
--);

--INSERT INTO ORGANISERS
--(OrganiserID,OrganiserName,OrganiserSurname,OrganiserAddress)
--VALUES
--(1,'Junior','Kgatla','4972 Mathibestad'),
--(2,'Orapeleng','Rakgekola','4973 Mathibestad');
--SELECT * FROM ORGANISERS;

--INSERT INTO PARTICIPANTS
--(ParticipantID,ParticipantName,ParticipantSurname,ParticipantEmail,ParticipantContact)
--VALUES
--(1,'Chalton','Modika','CharlesModika@gmail.com',0768832210),
--(2,'Charllot','lesenya','lesenya@gmail.com',0768543210);
--SELECT * FROM PARTICIPANTS;

--INSERT INTO EVENTS
--(EventID,EventName,[Description],[Date],[Location],Distance,EventType,OrganiserID)
--VALUES
--(1,'Pretoria City Run','20KM Run','03/10/26','Pretoria CBD','20KM','Running',2),
--(2,'Hammanskraal Fun Run','5KM Fun Run','15/10/26','Hammanskraal Sports Ground','5KM','Fun Run',1),
--(3,'Tshwane Marathon','15KM Marathon','23/10/26','Soshanghuve Giant Stadium','15KM','Marathon',2);
--(10001,'Pretoria City Run','20KM Run','03/10/26','Pretoria CBD','20KM','Running',2),
--(10002,'Hammanskraal Fun Run','5KM Fun Run','15/10/26','Hammanskraal Sports Ground','5KM','Fun Run',1),
--(10003,'Tshwane Marathon','15KM Marathon','23/10/26','Soshanghuve Giant Stadium','15KM','Marathon',2);
--SELECT * FROM EVENTS;

--INSERT INTO CATEGORIES
--(CategoryID, CategoryName,OrganiserID,EventID,Age)
--VALUES
--(1,'Senior',2,1,'18+'),
--(2,'Junior',1,2,'Under 20'),
--(3,'Senior',2,3,'Above 18');
--SELECT * FROM CATEGORIES;

--INSERT INTO ENROLMENT
--(EnrolmentID,EventID,ParticipantID,OrganiserID,CategoryID,EnrolDate)
--VALUES
--(1,3,1,2,3,'10/10/26');
--SELECT * FROM ENROLMENT;