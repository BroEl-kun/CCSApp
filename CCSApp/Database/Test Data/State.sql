USE [Imagis]
GO

set IDENTITY_INSERT State ON
GO

DELETE FROM State
GO

INSERT INTO State ([StateID],[StateFullName],[StateShortName]) VALUES
(2,'Alabama','AL'),
(3,'Arizona','AZ'),
(4,'Arkansas','AR'),
(5,'California','CA'),
(6,'Colorado','CO'),
(7,'Connecticut','CT'),
(8,'Delaware','DE'),
(9,'Florida','FL'),
(10,'Georgia','GA'),
(11,'Idaho','ID'),
(12,'Illinois','IL'),
(13,'Indiana','IN'),
(14,'Iowa','IA'),
(15,'Kansas','KS'),
(16,'Kentucky','KY'),
(17,'Louisiana','LA'),
(18,'Maine','ME'),
(19,'Maryland','MD'),
(20,'Massachusetts','MA'),
(21,'Michigan','MI'),
(22,'Minnesota','MN'),
(23,'Mississippi','MS'),
(24,'Missouri','MO'),
(25,'Montana','MT'),
(26,'Nebraska','NE'),
(27,'Nevada','NV'),
(28,'New Hampshire','NH'),
(29,'New Jersey','NJ'),
(30,'New Mexico','NM'),
(31,'New York','NY'),
(32,'North Carolina','NC'),
(33,'North Dakota','ND'),
(34,'Ohio','OH'),
(35,'Oklahoma','OK'),
(36,'Oregon','OR'),
(37,'Pennsylvania','PA'),
(38,'Rhode Island','RI'),
(39,'South Carolina','SC'),
(40,'South Dakota','SD'),
(41,'Tennessee','TN'),
(42,'Texas','TX'),
(43,'Vermont','VT'),
(44,'Virginia','VA'),
(45,'Washington','WA'),
(46,'West Virginia','WV'),
(47,'Wisconsin','WI'),
(48,'Wyoming','WY')

GO

set IDENTITY_INSERT State OFF
GO

-- *********************************************************************
USE Master
GO