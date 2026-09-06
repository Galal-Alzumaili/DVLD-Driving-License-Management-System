USE [master]
GO
/****** Object:  Database [DVLD] ******/
CREATE DATABASE [DVLD]
 CONTAINMENT = NONE
 ON  PRIMARY
( NAME = N'DVLD', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQL\DATA\DVLD.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON
( NAME = N'DVLD_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQL\DATA\DVLD_log.ldf' , SIZE = 139264KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DVLD] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DVLD].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DVLD] SET ANSI_NULLS OFF
GO
ALTER DATABASE [DVLD] SET ANSI_PADDING OFF
GO
ALTER DATABASE [DVLD] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [DVLD] SET ARITHABORT OFF
GO
ALTER DATABASE [DVLD] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [DVLD] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [DVLD] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [DVLD] SET RECOVERY FULL
GO
ALTER DATABASE [DVLD] SET  MULTI_USER
GO
USE [DVLD]
GO

/* =========================================================
   TABLES
   ========================================================= */

/****** Table: People ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[People](
	[PersonID] [int] IDENTITY(1,1) NOT NULL,
	[NationalNo] [nvarchar](20) NOT NULL,
	[FirstName] [nvarchar](20) NOT NULL,
	[SecondName] [nvarchar](20) NOT NULL,
	[ThirdName] [nvarchar](20) NULL,
	[LastName] [nvarchar](20) NOT NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[Gendor] [tinyint] NOT NULL,
	[Address] [nvarchar](500) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[NationalityCountryID] [int] NOT NULL,
	[ImagePath] [nvarchar](250) NULL,
 CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED ([PersonID] ASC)
) ON [PRIMARY]
GO

/****** Table: DetainedLicenses ******/
CREATE TABLE [dbo].[DetainedLicenses](
	[DetainID] [int] IDENTITY(1,1) NOT NULL,
	[LicenseID] [int] NOT NULL,
	[DetainDate] [smalldatetime] NOT NULL,
	[FineFees] [smallmoney] NOT NULL,
	[CreatedByUserID] [int] NOT NULL,
	[IsReleased] [bit] NOT NULL,
	[ReleaseDate] [smalldatetime] NULL,
	[ReleasedByUserID] [int] NULL,
	[ReleaseApplicationID] [int] NULL,
 CONSTRAINT [PK_DetainedLicenses] PRIMARY KEY CLUSTERED ([DetainID] ASC)
) ON [PRIMARY]
GO

/****** Table: Licenses ******/
CREATE TABLE [dbo].[Licenses](
	[LicenseID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[DriverID] [int] NOT NULL,
	[LicenseClass] [int] NOT NULL,
	[IssueDate] [datetime] NOT NULL,
	[ExpirationDate] [datetime] NOT NULL,
	[Notes] [nvarchar](500) NULL,
	[PaidFees] [smallmoney] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IssueReason] [tinyint] NOT NULL,
	[CreatedByUserID] [int] NOT NULL,
 CONSTRAINT [PK_Licenses] PRIMARY KEY CLUSTERED ([LicenseID] ASC)
) ON [PRIMARY]
GO

/****** Table: Drivers ******/
CREATE TABLE [dbo].[Drivers](
	[DriverID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	[CreatedByUserID] [int] NOT NULL,
	[CreatedDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Drivers_1] PRIMARY KEY CLUSTERED ([DriverID] ASC)
) ON [PRIMARY]
GO

/****** View: DetainedLicenses_View ******/
CREATE VIEW [dbo].[DetainedLicenses_View]
AS
SELECT        dbo.DetainedLicenses.DetainID, dbo.DetainedLicenses.LicenseID, dbo.DetainedLicenses.DetainDate, dbo.DetainedLicenses.IsReleased, dbo.DetainedLicenses.FineFees, dbo.DetainedLicenses.ReleaseDate,
                         dbo.People.NationalNo, dbo.People.FirstName + ' ' + dbo.People.SecondName + ' ' + ISNULL(dbo.People.ThirdName, ' ') + ' ' + dbo.People.LastName AS FullName, dbo.DetainedLicenses.ReleaseApplicationID
FROM            dbo.People INNER JOIN
                         dbo.Drivers ON dbo.People.PersonID = dbo.Drivers.PersonID INNER JOIN
                         dbo.Licenses ON dbo.Drivers.DriverID = dbo.Licenses.DriverID RIGHT OUTER JOIN
                         dbo.DetainedLicenses ON dbo.Licenses.LicenseID = dbo.DetainedLicenses.LicenseID
GO

/****** Table: Applications ******/
CREATE TABLE [dbo].[Applications](
	[ApplicationID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicantPersonID] [int] NOT NULL,
	[ApplicationDate] [datetime] NOT NULL,
	[ApplicationTypeID] [int] NOT NULL,
	[ApplicationStatus] [tinyint] NOT NULL,
	[LastStatusDate] [datetime] NOT NULL,
	[PaidFees] [smallmoney] NOT NULL,
	[CreatedByUserID] [int] NOT NULL,
 CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED ([ApplicationID] ASC)
) ON [PRIMARY]
GO

/****** Table: LocalDrivingLicenseApplications ******/
CREATE TABLE [dbo].[LocalDrivingLicenseApplications](
	[LocalDrivingLicenseApplicationID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[LicenseClassID] [int] NOT NULL,
 CONSTRAINT [PK_DrivingLicsenseApplications] PRIMARY KEY CLUSTERED ([LocalDrivingLicenseApplicationID] ASC)
) ON [PRIMARY]
GO

/****** View: LocalDrivingLicenseFullApplications_View ******/
CREATE VIEW [dbo].[LocalDrivingLicenseFullApplications_View]
AS
SELECT        dbo.Applications.ApplicationID, dbo.Applications.ApplicantPersonID, dbo.Applications.ApplicationDate, dbo.Applications.ApplicationTypeID, dbo.Applications.ApplicationStatus, dbo.Applications.LastStatusDate,
                         dbo.Applications.PaidFees, dbo.Applications.CreatedByUserID, dbo.LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID, dbo.LocalDrivingLicenseApplications.LicenseClassID
FROM            dbo.Applications INNER JOIN
                         dbo.LocalDrivingLicenseApplications ON dbo.Applications.ApplicationID = dbo.LocalDrivingLicenseApplications.ApplicationID
GO

/****** Table: TestAppointments ******/
CREATE TABLE [dbo].[TestAppointments](
	[TestAppointmentID] [int] IDENTITY(1,1) NOT NULL,
	[TestTypeID] [int] NOT NULL,
	[LocalDrivingLicenseApplicationID] [int] NOT NULL,
	[AppointmentDate] [smalldatetime] NOT NULL,
	[PaidFees] [smallmoney] NOT NULL,
	[CreatedByUserID] [int] NOT NULL,
	[IsLocked] [bit] NOT NULL,
	[RetakeTestApplicationID] [int] NULL,
 CONSTRAINT [PK_TestAppointments] PRIMARY KEY CLUSTERED ([TestAppointmentID] ASC)
) ON [PRIMARY]
GO

/****** Table: Tests ******/
CREATE TABLE [dbo].[Tests](
	[TestID] [int] IDENTITY(1,1) NOT NULL,
	[TestAppointmentID] [int] NOT NULL,
	[TestResult] [bit] NOT NULL,
	[Notes] [nvarchar](500) NULL,
	[CreatedByUserID] [int] NOT NULL,
 CONSTRAINT [PK_Tests] PRIMARY KEY CLUSTERED ([TestID] ASC)
) ON [PRIMARY]
GO

/****** Table: LicenseClasses ******/
CREATE TABLE [dbo].[LicenseClasses](
	[LicenseClassID] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [nvarchar](50) NOT NULL,
	[ClassDescription] [nvarchar](500) NOT NULL,
	[MinimumAllowedAge] [tinyint] NOT NULL,
	[DefaultValidityLength] [tinyint] NOT NULL,
	[ClassFees] [smallmoney] NOT NULL,
 CONSTRAINT [PK_LicenseClasses] PRIMARY KEY CLUSTERED ([LicenseClassID] ASC)
) ON [PRIMARY]
GO

/****** View: LocalDrivingLicenseApplications_View ******/
CREATE VIEW [dbo].[LocalDrivingLicenseApplications_View]
AS
SELECT        dbo.LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID, dbo.LicenseClasses.ClassName, dbo.People.NationalNo, dbo.People.FirstName + ' ' + dbo.People.SecondName + ' ' + ISNULL(dbo.People.ThirdName, '')
                         + ' ' + dbo.People.LastName AS FullName, dbo.Applications.ApplicationDate,
                             (SELECT        COUNT(dbo.TestAppointments.TestTypeID) AS PassedTestCount
                               FROM            dbo.Tests INNER JOIN
                                                         dbo.TestAppointments ON dbo.Tests.TestAppointmentID = dbo.TestAppointments.TestAppointmentID
                               WHERE        (dbo.TestAppointments.LocalDrivingLicenseApplicationID = dbo.LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID) AND (dbo.Tests.TestResult = 1)) AS PassedTestCount,
                         CASE WHEN Applications.ApplicationStatus = 1 THEN 'New' WHEN Applications.ApplicationStatus = 2 THEN 'Cancelled' WHEN Applications.ApplicationStatus = 3 THEN 'Completed' END AS Status
FROM            dbo.LocalDrivingLicenseApplications INNER JOIN
                         dbo.Applications ON dbo.LocalDrivingLicenseApplications.ApplicationID = dbo.Applications.ApplicationID INNER JOIN
                         dbo.LicenseClasses ON dbo.LocalDrivingLicenseApplications.LicenseClassID = dbo.LicenseClasses.LicenseClassID INNER JOIN
                         dbo.People ON dbo.Applications.ApplicantPersonID = dbo.People.PersonID
GO

/****** Table: TestTypes ******/
CREATE TABLE [dbo].[TestTypes](
	[TestTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TestTypeTitle] [nvarchar](100) NOT NULL,
	[TestTypeDescription] [nvarchar](500) NOT NULL,
	[TestTypeFees] [smallmoney] NOT NULL,
 CONSTRAINT [PK_TestTypes] PRIMARY KEY CLUSTERED ([TestTypeID] ASC)
) ON [PRIMARY]
GO

/****** View: TestAppointments_View ******/
CREATE VIEW [dbo].[TestAppointments_View]
AS
SELECT        dbo.TestAppointments.TestAppointmentID, dbo.TestAppointments.LocalDrivingLicenseApplicationID, dbo.TestTypes.TestTypeTitle, dbo.LicenseClasses.ClassName, dbo.TestAppointments.AppointmentDate,
                         dbo.TestAppointments.PaidFees, dbo.People.FirstName + ' ' + dbo.People.SecondName + ' ' + ISNULL(dbo.People.ThirdName, '') + ' ' + dbo.People.LastName AS FullName, dbo.TestAppointments.IsLocked
FROM            dbo.TestAppointments INNER JOIN
                         dbo.TestTypes ON dbo.TestAppointments.TestTypeID = dbo.TestTypes.TestTypeID INNER JOIN
                         dbo.LocalDrivingLicenseApplications ON dbo.TestAppointments.LocalDrivingLicenseApplicationID = dbo.LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID INNER JOIN
                         dbo.Applications ON dbo.LocalDrivingLicenseApplications.ApplicationID = dbo.Applications.ApplicationID INNER JOIN
                         dbo.People ON dbo.Applications.ApplicantPersonID = dbo.People.PersonID INNER JOIN
                         dbo.LicenseClasses ON dbo.LocalDrivingLicenseApplications.LicenseClassID = dbo.LicenseClasses.LicenseClassID
GO

/****** View: Drivers_View ******/
CREATE VIEW [dbo].[Drivers_View]
AS
SELECT        dbo.Drivers.DriverID, dbo.Drivers.PersonID, dbo.People.NationalNo, dbo.People.FirstName + ' ' + dbo.People.SecondName + ' ' + ISNULL(dbo.People.ThirdName, '') + ' ' + dbo.People.LastName AS FullName,
                         dbo.Drivers.CreatedDate,
                             (SELECT        COUNT(LicenseID) AS NumberOfActiveLicenses
                               FROM            dbo.Licenses
                               WHERE        (IsActive = 1) AND (DriverID = dbo.Drivers.DriverID)) AS NumberOfActiveLicenses
FROM            dbo.Drivers INNER JOIN
                         dbo.People ON dbo.Drivers.PersonID = dbo.People.PersonID
GO

/****** Table: ApplicationTypes ******/
CREATE TABLE [dbo].[ApplicationTypes](
	[ApplicationTypeID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationTypeTitle] [nvarchar](150) NOT NULL,
	[ApplicationFees] [smallmoney] NOT NULL,
 CONSTRAINT [PK_ApplicationTypes] PRIMARY KEY CLUSTERED ([ApplicationTypeID] ASC)
) ON [PRIMARY]
GO

/****** Table: Countries ******/
CREATE TABLE [dbo].[Countries](
	[CountryID] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__Countrie__10D160BFDBD6933F] PRIMARY KEY CLUSTERED ([CountryID] ASC)
) ON [PRIMARY]
GO

/****** Table: InternationalLicenses ******/
CREATE TABLE [dbo].[InternationalLicenses](
	[InternationalLicenseID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[DriverID] [int] NOT NULL,
	[IssuedUsingLocalLicenseID] [int] NOT NULL,
	[IssueDate] [smalldatetime] NOT NULL,
	[ExpirationDate] [smalldatetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedByUserID] [int] NOT NULL,
 CONSTRAINT [PK_InternationalLicenses] PRIMARY KEY CLUSTERED ([InternationalLicenseID] ASC)
) ON [PRIMARY]
GO

/****** Table: Users ******/
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	[UserName] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](20) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserID] ASC)
) ON [PRIMARY]
GO

/* =========================================================
   DEFAULT CONSTRAINTS
   ========================================================= */
ALTER TABLE [dbo].[Applications] ADD  CONSTRAINT [DF_Applications_ApplicationStatus]  DEFAULT ((1)) FOR [ApplicationStatus]
GO
ALTER TABLE [dbo].[ApplicationTypes] ADD  CONSTRAINT [DF_ApplicationTypes_Fees]  DEFAULT ((0)) FOR [ApplicationFees]
GO
ALTER TABLE [dbo].[DetainedLicenses] ADD  CONSTRAINT [DF_DetainedLicenses_IsReleased]  DEFAULT ((0)) FOR [IsReleased]
GO
ALTER TABLE [dbo].[LicenseClasses] ADD  CONSTRAINT [DF_LicenseClasses_Age]  DEFAULT ((18)) FOR [MinimumAllowedAge]
GO
ALTER TABLE [dbo].[LicenseClasses] ADD  CONSTRAINT [DF_LicenseClasses_DefaultPeriodLength]  DEFAULT ((1)) FOR [DefaultValidityLength]
GO
ALTER TABLE [dbo].[LicenseClasses] ADD  CONSTRAINT [DF_LicenseClasses_ClassFees]  DEFAULT ((0)) FOR [ClassFees]
GO
ALTER TABLE [dbo].[Licenses] ADD  CONSTRAINT [DF_Licenses_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Licenses] ADD  CONSTRAINT [DF_Licenses_IssueReason]  DEFAULT ((1)) FOR [IssueReason]
GO
ALTER TABLE [dbo].[People] ADD  CONSTRAINT [DF_People_Gendor]  DEFAULT ((0)) FOR [Gendor]
GO
ALTER TABLE [dbo].[TestAppointments] ADD  CONSTRAINT [DF_TestAppointments_AppointmentLocked]  DEFAULT ((0)) FOR [IsLocked]
GO

/* =========================================================
   FOREIGN KEYS
   ========================================================= */
ALTER TABLE [dbo].[Applications]  WITH CHECK ADD  CONSTRAINT [FK_Applications_ApplicationTypes] FOREIGN KEY([ApplicationTypeID]) REFERENCES [dbo].[ApplicationTypes] ([ApplicationTypeID])
GO
ALTER TABLE [dbo].[Applications] CHECK CONSTRAINT [FK_Applications_ApplicationTypes]
GO
ALTER TABLE [dbo].[Applications]  WITH CHECK ADD  CONSTRAINT [FK_Applications_People] FOREIGN KEY([ApplicantPersonID]) REFERENCES [dbo].[People] ([PersonID])
GO
ALTER TABLE [dbo].[Applications] CHECK CONSTRAINT [FK_Applications_People]
GO
ALTER TABLE [dbo].[Applications]  WITH CHECK ADD  CONSTRAINT [FK_Applications_Users] FOREIGN KEY([CreatedByUserID]) REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Applications] CHECK CONSTRAINT [FK_Applications_Users]
GO
ALTER TABLE [dbo].[DetainedLicenses]  WITH CHECK ADD  CONSTRAINT [FK_DetainedLicenses_Applications] FOREIGN KEY([ReleaseApplicationID]) REFERENCES [dbo].[Applications] ([ApplicationID])
GO
ALTER TABLE [dbo].[DetainedLicenses] CHECK CONSTRAINT [FK_DetainedLicenses_Applications]
GO
ALTER TABLE [dbo].[DetainedLicenses]  WITH CHECK ADD  CONSTRAINT [FK_DetainedLicenses_Licenses] FOREIGN KEY([LicenseID]) REFERENCES [dbo].[Licenses] ([LicenseID])
GO
ALTER TABLE [dbo].[DetainedLicenses] CHECK CONSTRAINT [FK_DetainedLicenses_Licenses]
GO
ALTER TABLE [dbo].[DetainedLicenses]  WITH CHECK ADD  CONSTRAINT [FK_DetainedLicenses_Users] FOREIGN KEY([CreatedByUserID]) REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[DetainedLicenses] CHECK CONSTRAINT [FK_DetainedLicenses_Users]
GO
ALTER TABLE [dbo].[DetainedLicenses]  WITH CHECK ADD  CONSTRAINT [FK_DetainedLicenses_Users1] FOREIGN KEY([ReleasedByUserID]) REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[DetainedLicenses] CHECK CONSTRAINT [FK_DetainedLicenses_Users1]
GO
ALTER TABLE [dbo].[Drivers]  WITH CHECK ADD  CONSTRAINT [FK_Drivers_People] FOREIGN KEY([PersonID]) REFERENCES [dbo].[People] ([PersonID])
GO
ALTER TABLE [dbo].[Drivers] CHECK CONSTRAINT [FK_Drivers_People]
GO
ALTER TABLE [dbo].[Drivers]  WITH CHECK ADD  CONSTRAINT [FK_Drivers_Users] FOREIGN KEY([CreatedByUserID]) REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Drivers] CHECK CONSTRAINT [FK_Drivers_Users]
GO
ALTER TABLE [dbo].[InternationalLicenses]  WITH CHECK ADD  CONSTRAINT [FK_InternationalLicenses_Applications] FOREIGN KEY([ApplicationID]) REFERENCES [dbo].[Applications] ([ApplicationID])
GO
ALTER TABLE [dbo].[InternationalLicenses] CHECK CONSTRAINT [FK_InternationalLicenses_Applications]
GO
ALTER TABLE [dbo].[InternationalLicenses]  WITH CHECK ADD  CONSTRAINT [FK_InternationalLicenses_Drivers] FOREIGN KEY([DriverID]) REFERENCES [dbo].[Drivers] ([DriverID])
GO
ALTER TABLE [dbo].[InternationalLicenses] CHECK CONSTRAINT [FK_InternationalLicenses_Drivers]
GO
ALTER TABLE [dbo].[InternationalLicenses]  WITH CHECK ADD  CONSTRAINT [FK_InternationalLicenses_Licenses] FOREIGN KEY([IssuedUsingLocalLicenseID]) REFERENCES [dbo].[Licenses] ([LicenseID])
GO
ALTER TABLE [dbo].[InternationalLicenses] CHECK CONSTRAINT [FK_InternationalLicenses_Licenses]
GO
ALTER TABLE [dbo].[InternationalLicenses]  WITH CHECK ADD  CONSTRAINT [FK_InternationalLicenses_Users] FOREIGN KEY([CreatedByUserID]) REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[InternationalLicenses] CHECK CONSTRAINT [FK_InternationalLicenses_Users]
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD  CONSTRAINT [FK_Licenses_Applications] FOREIGN KEY([ApplicationID]) REFERENCES [dbo].[Applications] ([ApplicationID])
GO
ALTER TABLE [dbo].[Licenses] CHECK CONSTRAINT [FK_Licenses_Applications]
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD  CONSTRAINT [FK_Licenses_Drivers] FOREIGN KEY([DriverID]) REFERENCES [dbo].[Drivers] ([DriverID])
GO
ALTER TABLE [dbo].[Licenses] CHECK CONSTRAINT [FK_Licenses_Drivers]
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD  CONSTRAINT [FK_Licenses_LicenseClasses] FOREIGN KEY([LicenseClass]) REFERENCES [dbo].[LicenseClasses] ([LicenseClassID])
GO
ALTER TABLE [dbo].[Licenses] CHECK CONSTRAINT [FK_Licenses_LicenseClasses]
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD  CONSTRAINT [FK_Licenses_Users] FOREIGN KEY([CreatedByUserID]) REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Licenses] CHECK CONSTRAINT [FK_Licenses_Users]
GO
ALTER TABLE [dbo].[LocalDrivingLicenseApplications]  WITH CHECK ADD  CONSTRAINT [FK_DrivingLicsenseApplications_Applications] FOREIGN KEY([ApplicationID]) REFERENCES [dbo].[Applications] ([ApplicationID])
GO
ALTER TABLE [dbo].[LocalDrivingLicenseApplications] CHECK CONSTRAINT [FK_DrivingLicsenseApplications_Applications]
GO
ALTER TABLE [dbo].[LocalDrivingLicenseApplications]  WITH CHECK ADD  CONSTRAINT [FK_DrivingLicsenseApplications_LicenseClasses] FOREIGN KEY([LicenseClassID]) REFERENCES [dbo].[LicenseClasses] ([LicenseClassID])
GO
ALTER TABLE [dbo].[LocalDrivingLicenseApplications] CHECK CONSTRAINT [FK_DrivingLicsenseApplications_LicenseClasses]
GO
ALTER TABLE [dbo].[People]  WITH CHECK ADD  CONSTRAINT [FK_People_Countries1] FOREIGN KEY([NationalityCountryID]) REFERENCES [dbo].[Countries] ([CountryID])
GO
ALTER TABLE [dbo].[People] CHECK CONSTRAINT [FK_People_Countries1]
GO
ALTER TABLE [dbo].[TestAppointments]  WITH CHECK ADD  CONSTRAINT [FK_TestAppointments_Applications] FOREIGN KEY([RetakeTestApplicationID]) REFERENCES [dbo].[Applications] ([ApplicationID])
GO
ALTER TABLE [dbo].[TestAppointments] CHECK CONSTRAINT [FK_TestAppointments_Applications]
GO
ALTER TABLE [dbo].[TestAppointments]  WITH CHECK ADD  CONSTRAINT [FK_TestAppointments_LocalDrivingLicenseApplications] FOREIGN KEY([LocalDrivingLicenseApplicationID]) REFERENCES [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID])
GO
ALTER TABLE [dbo].[TestAppointments] CHECK CONSTRAINT [FK_TestAppointments_LocalDrivingLicenseApplications]
GO
ALTER TABLE [dbo].[TestAppointments]  WITH CHECK ADD  CONSTRAINT [FK_TestAppointments_TestTypes] FOREIGN KEY([TestTypeID]) REFERENCES [dbo].[TestTypes] ([TestTypeID])
GO
ALTER TABLE [dbo].[TestAppointments] CHECK CONSTRAINT [FK_TestAppointments_TestTypes]
GO
ALTER TABLE [dbo].[TestAppointments]  WITH CHECK ADD  CONSTRAINT [FK_TestAppointments_Users] FOREIGN KEY([CreatedByUserID]) REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[TestAppointments] CHECK CONSTRAINT [FK_TestAppointments_Users]
GO
ALTER TABLE [dbo].[Tests]  WITH CHECK ADD  CONSTRAINT [FK_Tests_TestAppointments] FOREIGN KEY([TestAppointmentID]) REFERENCES [dbo].[TestAppointments] ([TestAppointmentID])
GO
ALTER TABLE [dbo].[Tests] CHECK CONSTRAINT [FK_Tests_TestAppointments]
GO
ALTER TABLE [dbo].[Tests]  WITH CHECK ADD  CONSTRAINT [FK_Tests_Users] FOREIGN KEY([CreatedByUserID]) REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Tests] CHECK CONSTRAINT [FK_Tests_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_People] FOREIGN KEY([PersonID]) REFERENCES [dbo].[People] ([PersonID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_People]
GO

/* =========================================================
   COLUMN DOCUMENTATION (extended properties)
   ========================================================= */
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-New 2-Cancelled 3-Completed' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Applications', @level2type=N'COLUMN',@level2name=N'ApplicationStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Minmum age allowed to apply for this license' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LicenseClasses', @level2type=N'COLUMN',@level2name=N'MinimumAllowedAge'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'How many years the licesnse will be valid.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LicenseClasses', @level2type=N'COLUMN',@level2name=N'DefaultValidityLength'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-FirstTime, 2-Renew, 3-Replacement for Damaged, 4- Replacement for Lost.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Licenses', @level2type=N'COLUMN',@level2name=N'IssueReason'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 Male , 1 Femail' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'People', @level2type=N'COLUMN',@level2name=N'Gendor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 - Fail 1-Pass' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tests', @level2type=N'COLUMN',@level2name=N'TestResult'
GO

/* =========================================================
   LOOKUP / REFERENCE DATA
   These are fixed values the application depends on
   (dropdown lists, fee schedules, etc.). No transactional
   or personal sample data (People, Users, Applications,
   Licenses, Drivers, Tests...) is included here on purpose —
   create your own test data through the application itself.
   ========================================================= */

SET IDENTITY_INSERT [dbo].[ApplicationTypes] ON
INSERT [dbo].[ApplicationTypes] ([ApplicationTypeID], [ApplicationTypeTitle], [ApplicationFees]) VALUES (1, N'New Local Driving License Service', 15.0000)
INSERT [dbo].[ApplicationTypes] ([ApplicationTypeID], [ApplicationTypeTitle], [ApplicationFees]) VALUES (2, N'Renew Driving License Service', 7.0000)
INSERT [dbo].[ApplicationTypes] ([ApplicationTypeID], [ApplicationTypeTitle], [ApplicationFees]) VALUES (3, N'Replacement for a Lost Driving License', 10.0000)
INSERT [dbo].[ApplicationTypes] ([ApplicationTypeID], [ApplicationTypeTitle], [ApplicationFees]) VALUES (4, N'Replacement for a Damaged Driving License', 5.0000)
INSERT [dbo].[ApplicationTypes] ([ApplicationTypeID], [ApplicationTypeTitle], [ApplicationFees]) VALUES (5, N'Release Detained Driving Licsense', 15.0000)
INSERT [dbo].[ApplicationTypes] ([ApplicationTypeID], [ApplicationTypeTitle], [ApplicationFees]) VALUES (6, N'New International License', 51.0000)
INSERT [dbo].[ApplicationTypes] ([ApplicationTypeID], [ApplicationTypeTitle], [ApplicationFees]) VALUES (7, N'Retake Test', 5.0000)
SET IDENTITY_INSERT [dbo].[ApplicationTypes] OFF
GO

SET IDENTITY_INSERT [dbo].[LicenseClasses] ON
INSERT [dbo].[LicenseClasses] ([LicenseClassID], [ClassName], [ClassDescription], [MinimumAllowedAge], [DefaultValidityLength], [ClassFees]) VALUES (1, N'Class 1 - Small Motorcycle', N'It allows the driver to drive small motorcycles, It is suitable for motorcycles with small capacity and limited power.', 18, 5, 15.0000)
INSERT [dbo].[LicenseClasses] ([LicenseClassID], [ClassName], [ClassDescription], [MinimumAllowedAge], [DefaultValidityLength], [ClassFees]) VALUES (2, N'Class 2 - Heavy Motorcycle License', N'Heavy Motorcycle License (Large Motorcycle License)', 21, 5, 30.0000)
INSERT [dbo].[LicenseClasses] ([LicenseClassID], [ClassName], [ClassDescription], [MinimumAllowedAge], [DefaultValidityLength], [ClassFees]) VALUES (3, N'Class 3 - Ordinary driving license', N'Ordinary driving license (car licence)', 18, 10, 20.0000)
INSERT [dbo].[LicenseClasses] ([LicenseClassID], [ClassName], [ClassDescription], [MinimumAllowedAge], [DefaultValidityLength], [ClassFees]) VALUES (4, N'Class 4 - Commercial', N'Commercial driving license (taxi/limousine)', 21, 10, 200.0000)
INSERT [dbo].[LicenseClasses] ([LicenseClassID], [ClassName], [ClassDescription], [MinimumAllowedAge], [DefaultValidityLength], [ClassFees]) VALUES (5, N'Class 5 - Agricultural', N'Agricultural and work vehicles used in farming or construction, (tractors / tillage machinery)', 21, 10, 50.0000)
INSERT [dbo].[LicenseClasses] ([LicenseClassID], [ClassName], [ClassDescription], [MinimumAllowedAge], [DefaultValidityLength], [ClassFees]) VALUES (6, N'Class 6 - Small and medium bus', N'Small and medium bus license', 21, 10, 250.0000)
INSERT [dbo].[LicenseClasses] ([LicenseClassID], [ClassName], [ClassDescription], [MinimumAllowedAge], [DefaultValidityLength], [ClassFees]) VALUES (7, N'Class 7 - Truck and heavy vehicle', N'Truck and heavy vehicle license', 21, 10, 300.0000)
SET IDENTITY_INSERT [dbo].[LicenseClasses] OFF
GO

SET IDENTITY_INSERT [dbo].[TestTypes] ON
INSERT [dbo].[TestTypes] ([TestTypeID], [TestTypeTitle], [TestTypeDescription], [TestTypeFees]) VALUES (1, N'Vision Test', N'This assesses the applicant''s visual acuity to ensure they have sufficient vision to drive safely.', 10.0000)
INSERT [dbo].[TestTypes] ([TestTypeID], [TestTypeTitle], [TestTypeDescription], [TestTypeFees]) VALUES (2, N'Written (Theory) Test', N'This test assesses the applicant''s knowledge of traffic rules, road signs, and driving regulations. It typically consists of multiple-choice questions, and the applicant must select the correct answer(s). The written test aims to ensure that the applicant understands the rules of the road and can apply them in various driving scenarios.', 20.0000)
INSERT [dbo].[TestTypes] ([TestTypeID], [TestTypeTitle], [TestTypeDescription], [TestTypeFees]) VALUES (3, N'Practical (Street) Test', N'This test evaluates the applicant''s driving skills and ability to operate a motor vehicle safely on public roads. A licensed examiner accompanies the applicant in the vehicle and observes their driving performance.', 35.0000)
SET IDENTITY_INSERT [dbo].[TestTypes] OFF
GO

SET IDENTITY_INSERT [dbo].[Countries] ON
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (1, N'Afghanistan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (2, N'Albania')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (3, N'Algeria')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (4, N'Andorra')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (5, N'Angola')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (6, N'Antigua and Barbuda')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (7, N'Argentina')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (8, N'Armenia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (9, N'Austria')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (10, N'Azerbaijan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (11, N'Bahrain')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (12, N'Bangladesh')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (13, N'Barbados')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (14, N'Belarus')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (15, N'Belgium')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (16, N'Belize')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (17, N'Benin')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (18, N'Bhutan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (19, N'Bolivia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (20, N'Bosnia and Herzegovina')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (21, N'Botswana')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (22, N'Brazil')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (23, N'Brunei')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (24, N'Bulgaria')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (25, N'Burkina Faso')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (26, N'Burundi')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (27, N'Cabo Verde')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (28, N'Cambodia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (29, N'Cameroon')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (30, N'Canada')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (31, N'Central African Republic')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (32, N'Chad')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (33, N'Channel Islands')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (34, N'Chile')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (35, N'China')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (36, N'Colombia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (37, N'Comoros')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (38, N'Congo')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (39, N'Costa Rica')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (40, N'Côte d''Ivoire')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (41, N'Croatia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (42, N'Cuba')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (43, N'Cyprus')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (44, N'Czech Republic')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (45, N'Denmark')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (46, N'Djibouti')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (47, N'Dominica')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (48, N'Dominican Republic')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (49, N'DR Congo')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (50, N'Ecuador')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (51, N'Egypt')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (52, N'El Salvador')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (53, N'Equatorial Guinea')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (54, N'Eritrea')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (55, N'Estonia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (56, N'Eswatini')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (57, N'Ethiopia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (58, N'Faeroe Islands')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (59, N'Finland')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (60, N'France')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (61, N'French Guiana')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (62, N'Gabon')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (63, N'Gambia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (64, N'Georgia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (65, N'Germany')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (66, N'Ghana')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (67, N'Gibraltar')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (68, N'Greece')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (69, N'Grenada')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (70, N'Guatemala')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (71, N'Guinea')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (72, N'Guinea-Bissau')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (73, N'Guyana')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (74, N'Haiti')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (75, N'Holy See')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (76, N'Honduras')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (77, N'Hong Kong')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (78, N'Hungary')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (79, N'Iceland')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (80, N'India')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (81, N'Indonesia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (82, N'Iran')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (83, N'Iraq')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (84, N'Ireland')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (85, N'Isle of Man')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (86, N'Israel')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (87, N'Italy')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (88, N'Jamaica')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (89, N'Japan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (90, N'Jordan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (91, N'Kazakhstan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (92, N'Kenya')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (93, N'Kuwait')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (94, N'Kyrgyzstan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (95, N'Laos')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (96, N'Latvia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (97, N'Lebanon')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (98, N'Lesotho')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (99, N'Liberia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (100, N'Libya')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (101, N'Liechtenstein')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (102, N'Lithuania')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (103, N'Luxembourg')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (104, N'Macao')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (105, N'Madagascar')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (106, N'Malawi')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (107, N'Malaysia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (108, N'Maldives')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (109, N'Mali')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (110, N'Malta')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (111, N'Mauritania')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (112, N'Mauritius')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (113, N'Mayotte')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (114, N'Mexico')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (115, N'Moldova')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (116, N'Monaco')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (117, N'Mongolia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (118, N'Montenegro')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (119, N'Morocco')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (120, N'Mozambique')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (121, N'Myanmar')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (122, N'Namibia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (123, N'Nepal')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (124, N'Netherlands')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (125, N'Nicaragua')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (126, N'Niger')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (127, N'Nigeria')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (128, N'North Korea')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (129, N'North Macedonia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (130, N'Norway')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (131, N'Oman')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (132, N'Pakistan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (133, N'Panama')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (134, N'Paraguay')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (135, N'Peru')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (136, N'Philippines')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (137, N'Poland')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (138, N'Portugal')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (139, N'Qatar')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (140, N'Réunion')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (141, N'Romania')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (142, N'Russia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (143, N'Rwanda')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (144, N'Saint Helena')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (145, N'Saint Kitts and Nevis')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (146, N'Saint Lucia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (147, N'Saint Vincent and the Grenadines')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (148, N'San Marino')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (149, N'Sao Tome & Principe')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (150, N'Saudi Arabia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (151, N'Senegal')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (152, N'Serbia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (153, N'Seychelles')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (154, N'Sierra Leone')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (155, N'Singapore')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (156, N'Slovakia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (157, N'Slovenia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (158, N'Somalia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (159, N'South Africa')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (160, N'South Korea')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (161, N'South Sudan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (162, N'Spain')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (163, N'Sri Lanka')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (164, N'State of Palestine')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (165, N'Sudan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (166, N'Suriname')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (167, N'Sweden')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (168, N'Switzerland')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (169, N'Syria')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (170, N'Taiwan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (171, N'Tajikistan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (172, N'Tanzania')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (173, N'Thailand')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (174, N'The Bahamas')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (175, N'Timor-Leste')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (176, N'Togo')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (177, N'Trinidad and Tobago')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (178, N'Tunisia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (179, N'Turkey')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (180, N'Turkmenistan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (181, N'Uganda')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (182, N'Ukraine')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (183, N'United Arab Emirates')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (184, N'United Kingdom')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (185, N'United States')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (186, N'Uruguay')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (187, N'Uzbekistan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (188, N'Venezuela')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (189, N'Vietnam')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (190, N'Western Sahara')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (191, N'Yemen')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (192, N'Zambia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (193, N'Zimbabwe')
SET IDENTITY_INSERT [dbo].[Countries] OFF
GO

/* =========================================================
   DEFAULT ADMIN ACCOUNT
   A single starter Person + User record so the application
   has a way to log in on first run. Change this password
   immediately after your first login.
   Username: admin | Password: 1234
   ========================================================= */

INSERT INTO [dbo].[People]
	([NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath])
VALUES
	(N'ADMIN001', N'Admin', N'Admin', NULL, N'Admin', CAST(N'1990-01-01' AS DateTime), 0, N'N/A', N'0000000000', N'admin@example.com', 90, NULL)
GO

DECLARE @AdminPersonID INT = SCOPE_IDENTITY();

INSERT INTO [dbo].[Users]
	([PersonID], [UserName], [Password], [IsActive])
VALUES
	(@AdminPersonID, N'admin', N'1234', 1)
GO

/* =========================================================
   DEFAULT ADMIN ACCOUNT
   Creates a single Person + User record so the application
   has a working login on first run. Change this password
   immediately after your first login.
   ========================================================= */

SET IDENTITY_INSERT [dbo].[People] ON
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath])
VALUES (1, N'0000000000', N'Admin', N'Admin', NULL, N'Admin', CAST(N'1990-01-01T00:00:00.000' AS DateTime), 0, N'N/A', N'0000000000', NULL, 90, NULL)
SET IDENTITY_INSERT [dbo].[People] OFF
GO

SET IDENTITY_INSERT [dbo].[Users] ON
INSERT [dbo].[Users] ([UserID], [PersonID], [UserName], [Password], [IsActive])
VALUES (1, 1, N'admin', N'1234', 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO

USE [master]
GO
ALTER DATABASE [DVLD] SET  READ_WRITE
GO
