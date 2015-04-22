USE [Imagis]
GO

set IDENTITY_INSERT FoodIn ON
GO

DELETE FROM FoodIn
GO

INSERT INTO FoodIn ([FoodInID],[TimeStamp],[Weight],[Count],[FoodSourceID],[FoodCategoryID],[USDAID]) VALUES
(1,41743,1.00,1,2,2,NULL),
(2,41762,1.00,2,7,5,NULL),
(3,41755,1.00,1,2,4,NULL),
(4,41799,1.00,1,3,2,NULL),
(5,41809,1.00,2,3,4,NULL)



GO

set IDENTITY_INSERT FoodIn OFF
GO

-- *********************************************************************
USE Master
GO