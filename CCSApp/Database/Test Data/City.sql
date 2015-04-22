USE [Imagis]
GO

set IDENTITY_INSERT City ON
GO

DELETE FROM City
GO

INSERT INTO City ([CityID],[CityName]) VALUES
(2, 'West Haven'),
(3,'Ogden'),
(4,'Pleasant View'),
(5,'Oklahoma City'),
(6,'Bountiful')



GO

set IDENTITY_INSERT City OFF
GO

-- *********************************************************************
USE Master
GO