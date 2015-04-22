USE [Imagis]
GO

set IDENTITY_INSERT FoodSource ON
GO

DELETE FROM FoodSource
GO

INSERT INTO FoodSource ([FoodSourceID],[Source],[StoreID],[FoodSourceTypeID],[AddressID]) VALUES
(2, 'St. Marys Church', NULL, 2, 2),
(3,'St. Josephs Parishioners',NULL,4,3),
(4,'St. James Parish',NULL,4,4),
(5,'Pepsi of Ogden',NULL,3,5),
(6,'Feed the Children',NULL,3,6),
(7,'Nate',NULL,5,7)





GO

set IDENTITY_INSERT FoodSource OFF
GO

-- *********************************************************************
USE Master
GO