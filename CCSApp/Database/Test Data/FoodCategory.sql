USE [Imagis]
GO

set IDENTITY_INSERT FoodCategory ON
GO

DELETE FROM FoodCategory
GO

INSERT INTO FoodCategory ([FoodCategoryID],[CategoryType],[Perishable],[NonFood],[CaseWeight]) VALUES
(1,'Beans','False','False',20),
(2,'Soup','False','False',35),
(3,'Peas','False','False',25),
(4,'Corn','False','False',30),
(5,'Tuna','False','False',20)


GO

set IDENTITY_INSERT FoodCategory OFF
GO

-- *********************************************************************
USE Master
GO