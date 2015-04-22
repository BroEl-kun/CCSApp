USE [Imagis]
GO

set IDENTITY_INSERT Zipcode ON
GO

DELETE FROM Zipcode
GO

INSERT INTO Zipcode([ZipID],[ZipCode]) VALUES
(2, '84404'),
(3,'84401'),
(4,'84414'),
(5,'73101'),
(6,'84010')



GO

set IDENTITY_INSERT Zipcode OFF
GO

-- *********************************************************************
USE Master
GO