USE [GibddDB2]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 17.03.2021 22:52:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArticleOffenses]    Script Date: 17.03.2021 22:52:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticleOffenses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Number] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Penalty] [nvarchar](max) NULL,
 CONSTRAINT [PK_ArticleOffenses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarDrivers]    Script Date: 17.03.2021 22:52:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarDrivers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IsCarOwner] [bit] NOT NULL,
	[FIO] [nvarchar](max) NULL,
	[VehicleId] [int] NULL,
 CONSTRAINT [PK_CarDrivers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarOwners]    Script Date: 17.03.2021 22:52:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarOwners](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FIO] [nvarchar](max) NULL,
	[PassportData] [bigint] NOT NULL,
 CONSTRAINT [PK_CarOwners] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 17.03.2021 22:52:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoryDriverLicense]    Script Date: 17.03.2021 22:52:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryDriverLicense](
	[CategoriesId] [int] NOT NULL,
	[DriverLicensesId] [int] NOT NULL,
 CONSTRAINT [PK_CategoryDriverLicense] PRIMARY KEY CLUSTERED 
(
	[CategoriesId] ASC,
	[DriverLicensesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Colors]    Script Date: 17.03.2021 22:52:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Colors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Colors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DriverLicenses]    Script Date: 17.03.2021 22:52:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DriverLicenses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Number] [bigint] NOT NULL,
	[ReleaseDate] [datetime2](7) NOT NULL,
	[IsInvalid] [bit] NOT NULL,
	[CarOwnerId] [int] NOT NULL,
 CONSTRAINT [PK_DriverLicenses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Models]    Script Date: 17.03.2021 22:52:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Models](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Models] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Offenses]    Script Date: 17.03.2021 22:52:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Offenses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[SumPenalty] [float] NOT NULL,
	[CarDriverId] [int] NULL,
 CONSTRAINT [PK_Offenses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TakeStroke]    Script Date: 17.03.2021 22:52:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TakeStroke](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TakeDate] [datetime2](7) NOT NULL,
	[ReturnDate] [datetime2](7) NOT NULL,
	[DriverLicenseId] [int] NULL,
 CONSTRAINT [PK_TakeStroke] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VehicleOffenses]    Script Date: 17.03.2021 22:52:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VehicleOffenses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Penalty] [float] NOT NULL,
	[TakeLicenseTime] [int] NOT NULL,
	[CarDriverId] [int] NULL,
	[ArticleOffenseId] [int] NULL,
	[CarOwnerId] [int] NULL,
 CONSTRAINT [PK_VehicleOffenses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehicles]    Script Date: 17.03.2021 22:52:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RegistrationNumber] [nvarchar](max) NULL,
	[ModelId] [int] NULL,
	[ColorId] [int] NULL,
	[MaintenanceDate] [datetime2](7) NULL,
	[MaintenanceSuccess] [bit] NOT NULL,
	[CarOwnerId] [int] NULL,
	[DriverLicenseId] [int] NULL,
	[CategoryId] [int] NULL,
 CONSTRAINT [PK_Vehicles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[DriverLicenses] ADD  DEFAULT ((0)) FOR [CarOwnerId]
GO
ALTER TABLE [dbo].[CarDrivers]  WITH CHECK ADD  CONSTRAINT [FK_CarDrivers_Vehicles_VehicleId] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[Vehicles] ([Id])
GO
ALTER TABLE [dbo].[CarDrivers] CHECK CONSTRAINT [FK_CarDrivers_Vehicles_VehicleId]
GO
ALTER TABLE [dbo].[CategoryDriverLicense]  WITH CHECK ADD  CONSTRAINT [FK_CategoryDriverLicense_Categories_CategoriesId] FOREIGN KEY([CategoriesId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CategoryDriverLicense] CHECK CONSTRAINT [FK_CategoryDriverLicense_Categories_CategoriesId]
GO
ALTER TABLE [dbo].[CategoryDriverLicense]  WITH CHECK ADD  CONSTRAINT [FK_CategoryDriverLicense_DriverLicenses_DriverLicensesId] FOREIGN KEY([DriverLicensesId])
REFERENCES [dbo].[DriverLicenses] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CategoryDriverLicense] CHECK CONSTRAINT [FK_CategoryDriverLicense_DriverLicenses_DriverLicensesId]
GO
ALTER TABLE [dbo].[DriverLicenses]  WITH CHECK ADD  CONSTRAINT [FK_DriverLicenses_CarOwners_CarOwnerId] FOREIGN KEY([CarOwnerId])
REFERENCES [dbo].[CarOwners] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DriverLicenses] CHECK CONSTRAINT [FK_DriverLicenses_CarOwners_CarOwnerId]
GO
ALTER TABLE [dbo].[Offenses]  WITH CHECK ADD  CONSTRAINT [FK_Offenses_CarDrivers_CarDriverId] FOREIGN KEY([CarDriverId])
REFERENCES [dbo].[CarDrivers] ([Id])
GO
ALTER TABLE [dbo].[Offenses] CHECK CONSTRAINT [FK_Offenses_CarDrivers_CarDriverId]
GO
ALTER TABLE [dbo].[TakeStroke]  WITH CHECK ADD  CONSTRAINT [FK_TakeStroke_DriverLicenses_DriverLicenseId] FOREIGN KEY([DriverLicenseId])
REFERENCES [dbo].[DriverLicenses] ([Id])
GO
ALTER TABLE [dbo].[TakeStroke] CHECK CONSTRAINT [FK_TakeStroke_DriverLicenses_DriverLicenseId]
GO
ALTER TABLE [dbo].[VehicleOffenses]  WITH CHECK ADD  CONSTRAINT [FK_VehicleOffenses_ArticleOffenses_ArticleOffenseId] FOREIGN KEY([ArticleOffenseId])
REFERENCES [dbo].[ArticleOffenses] ([Id])
GO
ALTER TABLE [dbo].[VehicleOffenses] CHECK CONSTRAINT [FK_VehicleOffenses_ArticleOffenses_ArticleOffenseId]
GO
ALTER TABLE [dbo].[VehicleOffenses]  WITH CHECK ADD  CONSTRAINT [FK_VehicleOffenses_CarDrivers_CarDriverId] FOREIGN KEY([CarDriverId])
REFERENCES [dbo].[CarDrivers] ([Id])
GO
ALTER TABLE [dbo].[VehicleOffenses] CHECK CONSTRAINT [FK_VehicleOffenses_CarDrivers_CarDriverId]
GO
ALTER TABLE [dbo].[VehicleOffenses]  WITH CHECK ADD  CONSTRAINT [FK_VehicleOffenses_CarOwners_CarOwnerId] FOREIGN KEY([CarOwnerId])
REFERENCES [dbo].[CarOwners] ([Id])
GO
ALTER TABLE [dbo].[VehicleOffenses] CHECK CONSTRAINT [FK_VehicleOffenses_CarOwners_CarOwnerId]
GO
ALTER TABLE [dbo].[Vehicles]  WITH CHECK ADD  CONSTRAINT [FK_Vehicles_CarOwners_CarOwnerId] FOREIGN KEY([CarOwnerId])
REFERENCES [dbo].[CarOwners] ([Id])
GO
ALTER TABLE [dbo].[Vehicles] CHECK CONSTRAINT [FK_Vehicles_CarOwners_CarOwnerId]
GO
ALTER TABLE [dbo].[Vehicles]  WITH CHECK ADD  CONSTRAINT [FK_Vehicles_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Vehicles] CHECK CONSTRAINT [FK_Vehicles_Categories_CategoryId]
GO
ALTER TABLE [dbo].[Vehicles]  WITH CHECK ADD  CONSTRAINT [FK_Vehicles_Colors_ColorId] FOREIGN KEY([ColorId])
REFERENCES [dbo].[Colors] ([Id])
GO
ALTER TABLE [dbo].[Vehicles] CHECK CONSTRAINT [FK_Vehicles_Colors_ColorId]
GO
ALTER TABLE [dbo].[Vehicles]  WITH CHECK ADD  CONSTRAINT [FK_Vehicles_DriverLicenses_DriverLicenseId] FOREIGN KEY([DriverLicenseId])
REFERENCES [dbo].[DriverLicenses] ([Id])
GO
ALTER TABLE [dbo].[Vehicles] CHECK CONSTRAINT [FK_Vehicles_DriverLicenses_DriverLicenseId]
GO
ALTER TABLE [dbo].[Vehicles]  WITH CHECK ADD  CONSTRAINT [FK_Vehicles_Models_ModelId] FOREIGN KEY([ModelId])
REFERENCES [dbo].[Models] ([Id])
GO
ALTER TABLE [dbo].[Vehicles] CHECK CONSTRAINT [FK_Vehicles_Models_ModelId]
GO
