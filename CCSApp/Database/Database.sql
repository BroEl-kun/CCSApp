IF EXISTS (SELECT * FROM sysdatabases WHERE name='Imagis')
DROP DATABASE Imagis
GO

CREATE DATABASE [Imagis]
GO

USE [Imagis]
GO
/****** Object:  User [ImagisDB]    Script Date: 4/21/2013 6:48:34 PM ******/
CREATE USER [ImagisDB] FOR LOGIN [ImagisDB] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [ImagisDB]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [ImagisDB]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [ImagisDB]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [ImagisDB]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [ImagisDB]
GO
ALTER ROLE [db_datareader] ADD MEMBER [ImagisDB]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [ImagisDB]
GO
ALTER ROLE [db_denydatareader] ADD MEMBER [ImagisDB]
GO
ALTER ROLE [db_denydatawriter] ADD MEMBER [ImagisDB]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 4/21/2013 6:48:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Address](
	[AddressID] [smallint] IDENTITY(1,1) NOT NULL,
	[StreetAddress1] [varchar](50) NULL,
	[StreetAddress2] [varchar](50) NULL,
	[CityID] [smallint] NULL,
	[StateID] [smallint] NULL,
	[ZipID] [smallint] NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[AddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Adjustment]    Script Date: 4/21/2013 6:48:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Adjustment](
	[AdjustmentID] [int] IDENTITY(1,1) NOT NULL,
	[Weight] [decimal](9, 2) NULL,
	[FoodCategory] [varchar](30) NULL,
	[Location] [varchar](25) NULL,
	[isUSDA] [bit] NULL,
	[USDANumber] [varchar](20) NULL,
	[Cases] [smallint] NULL,
	[AuditID] [smallint] NOT NULL,
 CONSTRAINT [PK_Adjustment] PRIMARY KEY CLUSTERED 
(
	[AdjustmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Agency]    Script Date: 4/21/2013 6:48:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Agency](
	[AgencyID] [smallint] IDENTITY(1,1) NOT NULL,
	[AgencyName] [nvarchar](150) NOT NULL,
	[AddressID] [smallint] NULL,
 CONSTRAINT [PK_Agency] PRIMARY KEY CLUSTERED 
(
	[AgencyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Audit]    Script Date: 4/21/2013 6:48:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Audit](
	[AuditID] [smallint] IDENTITY(1,1) NOT NULL,
	[Date] [smalldatetime] NOT NULL,
	[UserID] [smallint] NOT NULL,
 CONSTRAINT [PK_Audit] PRIMARY KEY CLUSTERED 
(
	[AuditID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[City]    Script Date: 4/21/2013 6:48:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[City](
	[CityID] [smallint] IDENTITY(1,1) NOT NULL,
	[CityName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[CityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Container]    Script Date: 4/21/2013 6:48:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Container](
	[ContainerID] [smallint] IDENTITY(1,1) NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[BinNumber] [smallint] NOT NULL,
	[Weight] [decimal](9, 2) NOT NULL,
	[isUSDA] [bit] NULL,
	[USDAID] [smallint] NULL,
	[FoodCategoryID] [smallint] NULL,
	[LocationID] [smallint] NOT NULL,
	[Cases] [smallint] NULL,
	[FoodSourcesTypeID] [smallint] NULL,
 CONSTRAINT [PK_Container] PRIMARY KEY CLUSTERED 
(
	[ContainerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DistributionType]    Script Date: 4/21/2013 6:48:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DistributionType](
	[DistributionTypeID] [smallint] IDENTITY(1,1) NOT NULL,
	[DistributionType] [varchar](30) NOT NULL,
	[ToClients] [bit] NOT NULL,
 CONSTRAINT [PK_DistributionType] PRIMARY KEY CLUSTERED 
(
	[DistributionTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ErrorLog]    Script Date: 4/21/2013 6:48:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ErrorLog](
	[ErrorLogID] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [varchar](255) NOT NULL,
	[TimeStamp] [datetime] NOT NULL,
	[FunctionName] [varchar](255) NOT NULL,
	[LineNumber] [varchar](255) NOT NULL,
	[ErrorText] [varchar](max) NOT NULL,
 CONSTRAINT [PK_ErrorLog] PRIMARY KEY CLUSTERED 
(
	[ErrorLogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FoodCategory]    Script Date: 4/21/2013 6:48:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FoodCategory](
	[FoodCategoryID] [smallint] IDENTITY(1,1) NOT NULL,
	[CategoryType] [varchar](30) NOT NULL,
	[Perishable] [bit] NOT NULL,
	[NonFood] [bit] NOT NULL,
	[CaseWeight] [decimal](7, 2) NULL,
 CONSTRAINT [PK_FoodCategory] PRIMARY KEY CLUSTERED 
(
	[FoodCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FoodIn]    Script Date: 4/21/2013 6:48:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodIn](
	[FoodInID] [smallint] IDENTITY(1,1) NOT NULL,
	[TimeStamp] [smalldatetime] NOT NULL,
	[Weight] [decimal](9, 2) NOT NULL,
	[Count] [smallint] NULL,
	[FoodSourceID] [smallint] NOT NULL,
	[FoodCategoryID] [smallint] NULL,
	[USDAID] [smallint] NULL,
 CONSTRAINT [PK_FoodIn] PRIMARY KEY CLUSTERED 
(
	[FoodInID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FoodOut]    Script Date: 4/21/2013 6:48:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodOut](
	[DistributionID] [smallint] IDENTITY(1,1) NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[TimeStamp] [smalldatetime] NOT NULL,
	[Weight] [float] NOT NULL,	
	[BinNumber] [smallint] NULL,
	[FoodSourceTypeID] [smallint] NOT NULL,
	[DistributionTypeID] [smallint] NOT NULL,
	[Count] [smallint] NULL,
	[USDAID] [smallint] NULL,
	[FoodCategoryID] [smallint] NULL,
	[AgencyID] [smallint] NULL,
 CONSTRAINT [PK_FoodOut] PRIMARY KEY CLUSTERED 
(
	[DistributionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FoodSource]    Script Date: 4/21/2013 6:48:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FoodSource](
	[FoodSourceID] [smallint] IDENTITY(1,1) NOT NULL,
	[Source] [varchar](30) NOT NULL,
	[StoreID] [varchar](10) NULL,
	[FoodSourceTypeID] [smallint] NOT NULL,
	[AddressID] [smallint] NULL,
 CONSTRAINT [PK_FoodSource] PRIMARY KEY CLUSTERED 
(
	[FoodSourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FoodSourceType]    Script Date: 4/21/2013 6:48:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FoodSourceType](
	[FoodSourceTypeID] [smallint] IDENTITY(1,1) NOT NULL,
	[FoodSourceType] [varchar](50) NOT NULL,
 CONSTRAINT [PK_FoodSourceType] PRIMARY KEY CLUSTERED 
(
	[FoodSourceTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Location]    Script Date: 4/21/2013 6:48:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Location](
	[LocationID] [smallint] IDENTITY(1,1) NOT NULL,
	[RoomName] [varchar](25) NOT NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Log]    Script Date: 4/21/2013 6:48:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Log](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[Date] [datetime] NOT NULL,
	[UserID] [smallint] NOT NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[State]    Script Date: 4/21/2013 6:48:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[State](
	[StateID] [smallint] IDENTITY(1,1) NOT NULL,
	[StateFullName] [varchar](50) NULL,
	[StateShortName] [char](2) NOT NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[StateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Template]    Script Date: 4/21/2013 6:48:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Template](
	[TemplateID] [int] IDENTITY(1,1) NOT NULL,
	[TemplateName] [varchar](100) NOT NULL,
	[Data] [xml] NOT NULL,
	[TemplateType] [smallint] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[LastUpdatedBy] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Template] PRIMARY KEY CLUSTERED 
(
	[TemplateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[USDACategory]    Script Date: 4/21/2013 6:48:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[USDACategory](
	[USDAID] [smallint] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[USDANumber] [varchar](20) NULL,
	[CaseWeight] [decimal](9, 2) NULL,
 CONSTRAINT [PK_USDACategory] PRIMARY KEY CLUSTERED 
(
	[USDAID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 4/21/2013 6:48:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [smallint] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](25) NOT NULL,
	[LastName] [varchar](25) NOT NULL,
	[UserName] [varchar](25) NOT NULL,
	[Admin] [bit] NOT NULL,
	[Password] [varchar](max) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Zipcode]    Script Date: 4/21/2013 6:48:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Zipcode](
	[ZipID] [smallint] IDENTITY(1,1) NOT NULL,
	[ZipCode] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Zipcode] PRIMARY KEY CLUSTERED 
(
	[ZipID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_City] FOREIGN KEY([CityID])
REFERENCES [dbo].[City] ([CityID])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_City]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_State] FOREIGN KEY([StateID])
REFERENCES [dbo].[State] ([StateID])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_State]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_Zipcode] FOREIGN KEY([ZipID])
REFERENCES [dbo].[Zipcode] ([ZipID])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_Zipcode]
GO
ALTER TABLE [dbo].[Adjustment]  WITH CHECK ADD  CONSTRAINT [FK_Adjustment_Audit] FOREIGN KEY([AuditID])
REFERENCES [dbo].[Audit] ([AuditID])
GO
ALTER TABLE [dbo].[Adjustment] CHECK CONSTRAINT [FK_Adjustment_Audit]
GO
ALTER TABLE [dbo].[Agency]  WITH CHECK ADD  CONSTRAINT [FK_Agency_Address] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Address] ([AddressID])
GO
ALTER TABLE [dbo].[Agency] CHECK CONSTRAINT [FK_Agency_Address]
GO
ALTER TABLE [dbo].[Audit]  WITH CHECK ADD  CONSTRAINT [FK_Audit_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Audit] CHECK CONSTRAINT [FK_Audit_User]
GO
ALTER TABLE [dbo].[Container]  WITH CHECK ADD  CONSTRAINT [FK_Container_FoodCategory] FOREIGN KEY([FoodCategoryID])
REFERENCES [dbo].[FoodCategory] ([FoodCategoryID])
GO
ALTER TABLE [dbo].[Container] CHECK CONSTRAINT [FK_Container_FoodCategory]
GO
ALTER TABLE [dbo].[Container]  WITH CHECK ADD  CONSTRAINT [FK_Container_FoodSourceType] FOREIGN KEY([FoodSourcesTypeID])
REFERENCES [dbo].[FoodSourceType] ([FoodSourceTypeID])
GO
ALTER TABLE [dbo].[Container] CHECK CONSTRAINT [FK_Container_FoodSourceType]
GO
ALTER TABLE [dbo].[Container]  WITH CHECK ADD  CONSTRAINT [FK_Container_Location] FOREIGN KEY([LocationID])
REFERENCES [dbo].[Location] ([LocationID])
GO
ALTER TABLE [dbo].[Container] CHECK CONSTRAINT [FK_Container_Location]
GO
ALTER TABLE [dbo].[Container]  WITH CHECK ADD  CONSTRAINT [FK_Container_USDACategory] FOREIGN KEY([USDAID])
REFERENCES [dbo].[USDACategory] ([USDAID])
GO
ALTER TABLE [dbo].[Container] CHECK CONSTRAINT [FK_Container_USDACategory]
GO
ALTER TABLE [dbo].[FoodIn]  WITH CHECK ADD  CONSTRAINT [FK_FoodIn_FoodCategory] FOREIGN KEY([FoodCategoryID])
REFERENCES [dbo].[FoodCategory] ([FoodCategoryID])
GO
ALTER TABLE [dbo].[FoodIn] CHECK CONSTRAINT [FK_FoodIn_FoodCategory]
GO
ALTER TABLE [dbo].[FoodIn]  WITH CHECK ADD  CONSTRAINT [FK_FoodIn_FoodSource] FOREIGN KEY([FoodSourceID])
REFERENCES [dbo].[FoodSource] ([FoodSourceID])
GO
ALTER TABLE [dbo].[FoodIn] CHECK CONSTRAINT [FK_FoodIn_FoodSource]
GO
ALTER TABLE [dbo].[FoodIn]  WITH CHECK ADD  CONSTRAINT [FK_FoodIn_USDACategory] FOREIGN KEY([USDAID])
REFERENCES [dbo].[USDACategory] ([USDAID])
GO
ALTER TABLE [dbo].[FoodIn] CHECK CONSTRAINT [FK_FoodIn_USDACategory]
GO
ALTER TABLE [dbo].[FoodOut]  WITH CHECK ADD  CONSTRAINT [FK_FoodOut_Agency] FOREIGN KEY([AgencyID])
REFERENCES [dbo].[Agency] ([AgencyID])
GO
ALTER TABLE [dbo].[FoodOut] CHECK CONSTRAINT [FK_FoodOut_Agency]
GO
ALTER TABLE [dbo].[FoodOut]  WITH CHECK ADD  CONSTRAINT [FK_FoodOut_DistributionType] FOREIGN KEY([DistributionTypeID])
REFERENCES [dbo].[DistributionType] ([DistributionTypeID])
GO
ALTER TABLE [dbo].[FoodOut] CHECK CONSTRAINT [FK_FoodOut_DistributionType]
GO
ALTER TABLE [dbo].[FoodOut]  WITH CHECK ADD  CONSTRAINT [FK_FoodOut_FoodCategory] FOREIGN KEY([FoodCategoryID])
REFERENCES [dbo].[FoodCategory] ([FoodCategoryID])
GO
ALTER TABLE [dbo].[FoodOut] CHECK CONSTRAINT [FK_FoodOut_FoodCategory]
GO
ALTER TABLE [dbo].[FoodOut]  WITH CHECK ADD  CONSTRAINT [FK_FoodOut_FoodSourceType] FOREIGN KEY([FoodSourceTypeID])
REFERENCES [dbo].[FoodSourceType] ([FoodSourceTypeID])
GO
ALTER TABLE [dbo].[FoodOut] CHECK CONSTRAINT [FK_FoodOut_FoodSourceType]
GO
ALTER TABLE [dbo].[FoodOut]  WITH CHECK ADD  CONSTRAINT [FK_FoodOut_USDACategory] FOREIGN KEY([USDAID])
REFERENCES [dbo].[USDACategory] ([USDAID])
GO
ALTER TABLE [dbo].[FoodOut] CHECK CONSTRAINT [FK_FoodOut_USDACategory]
GO
ALTER TABLE [dbo].[FoodSource]  WITH CHECK ADD  CONSTRAINT [FK_FoodSource_Address] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Address] ([AddressID])
GO
ALTER TABLE [dbo].[FoodSource] CHECK CONSTRAINT [FK_FoodSource_Address]
GO
ALTER TABLE [dbo].[FoodSource]  WITH CHECK ADD  CONSTRAINT [FK_FoodSource_FoodSourceType] FOREIGN KEY([FoodSourceTypeID])
REFERENCES [dbo].[FoodSourceType] ([FoodSourceTypeID])
GO
ALTER TABLE [dbo].[FoodSource] CHECK CONSTRAINT [FK_FoodSource_FoodSourceType]
GO
ALTER TABLE [dbo].[Log]  WITH CHECK ADD  CONSTRAINT [FK_Log_Log] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Log] CHECK CONSTRAINT [FK_Log_Log]
GO


/***** Default Database Data *****/
INSERT INTO [dbo].[User] VALUES ('CCS', 'Admin', 'admin', 1, '123')

INSERT INTO [dbo].[City] VALUES ('Salt Lake City')
INSERT INTO [dbo].[State] VALUES ('Utah', 'UT')
INSERT INTO [dbo].[Zipcode] VALUES ('84119')
INSERT INTO [dbo].[Address] VALUES ('3150 South 900 West', NULL, 1, 1, 1)
INSERT INTO [dbo].[FoodSourceType] VALUES ('USDA')
INSERT INTO [dbo].[FoodSourceType] VALUES ('Grocery Rescue')
INSERT INTO [dbo].[FoodSource] VALUES ('USDA', NULL, 1, 1)

INSERT INTO [dbo].[DistributionType] VALUES ('Spoiled', 0)
INSERT INTO [dbo].[DistributionType] VALUES ('Transferred', 0)
INSERT INTO [dbo].[DistributionType] VALUES ('On The Line', 0)
GO

/***** Test Data *****/
set IDENTITY_INSERT City ON
INSERT INTO City ([CityID],[CityName]) VALUES
(2, 'West Haven'),
(3,'Ogden'),
(4,'Pleasant View'),
(5,'Oklahoma City'),
(6,'Bountiful')
set IDENTITY_INSERT City OFF

set IDENTITY_INSERT Zipcode ON
INSERT INTO Zipcode([ZipID],[ZipCode]) VALUES
(2, '84404'),
(3,'84401'),
(4,'84414'),
(5,'73101'),
(6,'84010')
set IDENTITY_INSERT Zipcode OFF

set IDENTITY_INSERT State ON
INSERT INTO State([StateID],[StateFullName],[StateShortName]) VALUES
(2, 'Oklahoma', 'OK')
set IDENTITY_INSERT State OFF

set IDENTITY_INSERT Address ON
INSERT INTO Address ([AddressID],[StreetAddress1],[StreetAddress2],[CityID],[StateID],[ZipID]) VALUES
(2, '123', NULL, 2,1,2),
(3,'514 24th Street',NULL,3,1,3),
(4,'495 N Harrison Blvd.',NULL,3,1,3),
(5,'2780 N. HWY 89',NULL,4,1,4),
(6,'1333 N. Meridian Ave.',NULL,5,2,5),
(7,'876 S 175 W',NULL,6,1,6)
set IDENTITY_INSERT Address OFF

set IDENTITY_INSERT FoodSourceType ON
INSERT INTO FoodSourceType ([FoodSourceTypeID],[FoodSourceType]) VALUES
(3,'In-Kind(Non-Tax)'),
(4,'In-Kind(Taxable)'),
(5,'Given'),
(6,'Found'),
(7,'Borrowed')
set IDENTITY_INSERT FoodSourceType OFF

set IDENTITY_INSERT FoodSource ON
INSERT INTO FoodSource ([FoodSourceID],[Source],[StoreID],[FoodSourceTypeID],[AddressID]) VALUES
(2, 'St. Marys Church', NULL, 2, 2),
(3,'St. Josephs Parishioners',NULL,4,3),
(4,'St. James Parish',NULL,4,4),
(5,'Pepsi of Ogden',NULL,3,5),
(6,'Feed the Children',NULL,3,6),
(7,'Nate',NULL,5,7)
set IDENTITY_INSERT FoodSource OFF

set IDENTITY_INSERT Location ON
INSERT INTO Location ([LocationID],[RoomName]) VALUES
(1,'(NONE)'),
(2,'Kitchen'),
(3,'Living Room'),
(4,'War Room'),
(5,'USDA Room')
set IDENTITY_INSERT Location OFF

set IDENTITY_INSERT FoodCategory ON
INSERT INTO FoodCategory ([FoodCategoryID],[CategoryType],[Perishable],[NonFood],[CaseWeight]) VALUES
(1,'Beans','False','False',20),
(2,'Soup','False','False',35),
(3,'Peas','False','False',25),
(4,'Corn','False','False',30),
(5,'Tuna','False','False',20)
set IDENTITY_INSERT FoodCategory OFF

set IDENTITY_INSERT FoodOut ON
INSERT INTO FoodOut ([DistributionID],[DateCreated],[TimeStamp],[Weight],[BinNumber],[FoodSourceTypeID],[DistributionTypeID],[Count],[USDAID],[FoodCategoryID],[AgencyID]) VALUES
(1,41821.5,41822.5678125,150,1234,2,3,5,null,4,null),
(2,41821.5,41823.5678124421,100,2222,2,1,15,null,2,null),
(3,41821.5,41824.5678124421,110,0,4,3,22,null,1,null),
(4,41821.5,41825.5678124421,200,9873,3,2,17,null,3,null),
(5,41821.5,41826.5678124421,105,4413,4,3,6,null,5,null),
(6,41821.5,41827.5678124421,111,4891,5,3,30,null,5,null),
(7,41821.5,41828.5678124421,102,4321,1,2,25,null,2,null),
(8,41821.5,41829.5678124421,100,0,6,3,18,null,1,null),
(9,41821.5,41830.5678124421,115,0,1,3,20,null,4,null),
(10,41821.5,41831.5678124421,50,6378,1,2,5,null,5,null),
(11,41821.5,41832.5678124421,75,1111,1,3,20,null,1,null),
(12,41821.5,41833.5678124421,90,5346,6,2,15,null,4,null),
(13,41821.5,41834.5678124421,100,0,3,2,35,null,4,null),
(14,41821.5,41835.5678124421,88,9835,5,3,27,null,3,null),
(15,41821.5,41836.5678124421,40,7201,2,3,11,null,3,null),
(16,41821.5,41837.5678124421,34,1001,3,3,8,null,4,null),
(17,41821.5,41838.5678124421,77,0,6,1,22,null,5,null),
(18,41821.5,41839.5678124421,45,0,1,3,10,null,2,null),
(19,41821.5,41840.5678124421,86,2796,1,3,23,null,4,null),
(20,41821.5,41841.5678124421,66,8411,3,3,18,null,3,null),
(21,41821.5,41842.5678124421,70,7632,2,2,30,null,2,null),
(22,41821.5,41843.5678124421,110,9051,4,3,40,null,2,null),
(23,41821.5,41844.5678124421,85,1963,4,2,35,null,1,null),
(24,41821.5,41845.5678124421,105,0,6,3,37,null,1,null),
(25,41821.5,41846.5678124421,200,0,4,3,50,null,3,null)
set IDENTITY_INSERT FoodOut OFF

set IDENTITY_INSERT USDACategory ON
INSERT INTO USDACategory ([USDAID],[Description],[USDANumber],[CaseWeight]) VALUES
(1,'USDA Beans','1234','20'),
(2,'USDA Soup','5678','30'),
(3,'USDA Peas','9012','35'),
(4,'USDA Corn','3456','25'),
(5,'USDA Tuna','7890','15')
set IDENTITY_INSERT USDACategory OFF

set IDENTITY_INSERT Container ON
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
set IDENTITY_INSERT Container OFF

set IDENTITY_INSERT FoodIn ON
INSERT INTO FoodIn ([FoodInID],[TimeStamp],[Weight],[Count],[FoodSourceID],[FoodCategoryID],[USDAID]) VALUES
(1,41743,1.00,1,2,2,NULL),
(2,41762,1.00,2,7,5,NULL),
(3,41755,1.00,1,2,4,NULL),
(4,41799,1.00,1,3,2,NULL),
(5,41809,1.00,2,3,4,NULL)
set IDENTITY_INSERT FoodIn OFF

GO
