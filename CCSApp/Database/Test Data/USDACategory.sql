USE [Imagis]
GO

set IDENTITY_INSERT USDACategory ON
GO

DELETE FROM USDACategory
GO

INSERT INTO USDACategory ([USDAID],[Description],[USDANumber],[CaseWeight]) VALUES
(1,'USDA Beans','1234','20'),
(2,'USDA Soup','5678','30'),
(3,'USDA Peas','9012','35'),
(4,'USDA Corn','3456','25'),
(5,'USDA Tuna','7890','15')



GO

set IDENTITY_INSERT USDACategory OFF
GO

-- *********************************************************************
USE Master
GO