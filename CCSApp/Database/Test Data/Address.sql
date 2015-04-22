USE [Imagis]
GO

set IDENTITY_INSERT Address ON
GO

DELETE FROM Address
GO

INSERT INTO Address ([AddressID],[StreetAddress1],[StreetAddress2],[CityID],[StateID],[ZipID]) VALUES
(2, '123', NULL, 2,1,2),
(3,'514 24th Street',NULL,3,1,3),
(4,'495 N Harrison Blvd.',NULL,3,1,3),
(5,'2780 N. HWY 89',NULL,4,1,4),
(6,'1333 N. Meridian Ave.',NULL,5,2,5),
(7,'876 S 175 W',NULL,6,1,6)GO

set IDENTITY_INSERT Address OFF
GO

-- *********************************************************************
USE Master
GO