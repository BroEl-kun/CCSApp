USE [Imagis]
GO

set IDENTITY_INSERT Container ON
GO

DELETE FROM Container
GO

INSERT INTO Container ([ContainerID],[DateCreated],[BinNumber],[Weight],[isUSDA],[USDAID],[FoodCategoryID],[LocationID],[Cases],[FoodSourcesTypeID]) VALUES
(1,41814.5,9462,150,'FALSE',null,5,2,5,2),
(2,41815.5,2322,100,'FALSE',null,2,2,15,2),
(3,41816.5,4983,110,'FALSE',null,4,1,22,2),
(4,41817.5,6243,200,'TRUE',2,null,5,17,1),
(5,41818.5,1132,105,'TRUE',3,null,5,6,1),
(6,41819.5,4981,111,'FALSE',null,1,3,30,2),
(7,41820.5,2431,102,'TRUE',1,null,5,25,1),
(8,41821.5,5985,100,'FALSE',null,2,2,18,2),
(9,41822.5,6386,115,'FALSE',null,3,1,20,2),
(10,41823.5,1538,55,'TRUE',2,null,2,13,2)


GO

set IDENTITY_INSERT Container OFF
GO

-- *********************************************************************
USE Master
GO