USE [Imagis]
GO

set IDENTITY_INSERT Location ON
GO

DELETE FROM Location
GO

INSERT INTO Location ([LocationID],[RoomName]) VALUES
(1,'(NONE)'),
(2,'Kitchen'),
(3,'Living Room'),
(4,'War Room'),
(5,'USDA Room')

GO

set IDENTITY_INSERT Location OFF
GO

-- *********************************************************************
USE Master
GO