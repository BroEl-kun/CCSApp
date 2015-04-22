USE [Imagis]
GO

set IDENTITY_INSERT FoodSourceType ON
GO

DELETE FROM FoodSourceType
GO

INSERT INTO FoodSourceType ([FoodSourceTypeID],[FoodSourceType]) VALUES
(3,'In-Kind(Non-Tax)'),
(4,'In-Kind(Taxable)'),
(5,'Given'),
(6,'Found'),
(7,'Borrowed')




GO

set IDENTITY_INSERT FoodSourceType OFF
GO

-- *********************************************************************
USE Master
GO