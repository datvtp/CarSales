USE [master]
GO
/****** Object:  Database [CarManagement]    Script Date: 4/5/2020 2:17:35 PM ******/
CREATE DATABASE [CarManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CarManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\CarManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CarManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\CarManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [CarManagement] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CarManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CarManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CarManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CarManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CarManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CarManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [CarManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CarManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CarManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CarManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CarManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CarManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CarManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CarManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CarManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CarManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CarManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CarManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CarManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CarManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CarManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CarManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CarManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CarManagement] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CarManagement] SET  MULTI_USER 
GO
ALTER DATABASE [CarManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CarManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CarManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CarManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CarManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CarManagement] SET QUERY_STORE = OFF
GO
USE [CarManagement]
GO
/****** Object:  Table [dbo].[Account_Roles]    Script Date: 4/5/2020 2:17:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account_Roles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Account_Roles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Account_Status]    Script Date: 4/5/2020 2:17:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account_Status](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Account_Status] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 4/5/2020 2:17:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[UserID] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Fullname] [nvarchar](100) NOT NULL,
	[Phone] [varchar](50) NOT NULL,
	[Account_Role_ID] [int] NOT NULL,
	[Account_Status_ID] [int] NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Car_Categories]    Script Date: 4/5/2020 2:17:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Car_Categories](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Car_Categories] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Car_Fuels]    Script Date: 4/5/2020 2:17:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Car_Fuels](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Car_Fuels] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Car_Manufacturers]    Script Date: 4/5/2020 2:17:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Car_Manufacturers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Car_Manufacturers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Car_Status]    Script Date: 4/5/2020 2:17:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Car_Status](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Car_Status] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Car_Tranmissions]    Script Date: 4/5/2020 2:17:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Car_Tranmissions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Car_Tranmissions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Car_Types]    Script Date: 4/5/2020 2:17:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Car_Types](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Car_Types] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cars]    Script Date: 4/5/2020 2:17:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cars](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Model_Name] [nvarchar](100) NOT NULL,
	[Price] [float] NOT NULL,
	[Produced_Year] [int] NOT NULL,
	[Accquired_Date] [datetime] NOT NULL,
	[Engine] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Manufacturer_ID] [int] NOT NULL,
	[Tranmission_ID] [int] NOT NULL,
	[Type_ID] [int] NOT NULL,
	[Category_ID] [int] NOT NULL,
	[Fuel_ID] [int] NOT NULL,
	[Status_ID] [int] NOT NULL,
 CONSTRAINT [PK_Cars] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 4/5/2020 2:17:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Fullname] [nvarchar](100) NOT NULL,
	[Phone] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice_Details]    Script Date: 4/5/2020 2:17:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice_Details](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Car_ID] [int] NOT NULL,
	[Invoice_ID] [int] NOT NULL,
	[Unit_Price] [float] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_Invoice_Details] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoices]    Script Date: 4/5/2020 2:17:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoices](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date_Of_Purcharse] [datetime] NOT NULL,
	[Customer_ID] [int] NOT NULL,
 CONSTRAINT [PK_Invoices] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account_Roles] ON 

INSERT [dbo].[Account_Roles] ([ID], [Description]) VALUES (1, N'Supervisor')
INSERT [dbo].[Account_Roles] ([ID], [Description]) VALUES (2, N'Staff')
SET IDENTITY_INSERT [dbo].[Account_Roles] OFF
SET IDENTITY_INSERT [dbo].[Account_Status] ON 

INSERT [dbo].[Account_Status] ([ID], [Description]) VALUES (1, N'Active')
INSERT [dbo].[Account_Status] ([ID], [Description]) VALUES (2, N'Deleted')
SET IDENTITY_INSERT [dbo].[Account_Status] OFF
INSERT [dbo].[Accounts] ([UserID], [Password], [Fullname], [Phone], [Account_Role_ID], [Account_Status_ID]) VALUES (N'binz', N'1', N'Binz', N'0977544551', 2, 2)
INSERT [dbo].[Accounts] ([UserID], [Password], [Fullname], [Phone], [Account_Role_ID], [Account_Status_ID]) VALUES (N'dat', N'1', N'Đạt Văn', N'0376710815', 1, 1)
INSERT [dbo].[Accounts] ([UserID], [Password], [Fullname], [Phone], [Account_Role_ID], [Account_Status_ID]) VALUES (N'duyen', N'1', N'Thùy Duyên', N'0914628302', 2, 1)
INSERT [dbo].[Accounts] ([UserID], [Password], [Fullname], [Phone], [Account_Role_ID], [Account_Status_ID]) VALUES (N'thang', N'1', N'Thắng Hoàng', N'0963492364', 1, 1)
SET IDENTITY_INSERT [dbo].[Car_Categories] ON 

INSERT [dbo].[Car_Categories] ([ID], [Description]) VALUES (1, N'Luxury')
INSERT [dbo].[Car_Categories] ([ID], [Description]) VALUES (2, N'Sport')
INSERT [dbo].[Car_Categories] ([ID], [Description]) VALUES (3, N'SUV')
INSERT [dbo].[Car_Categories] ([ID], [Description]) VALUES (4, N'Family')
INSERT [dbo].[Car_Categories] ([ID], [Description]) VALUES (5, N'Sedan')
INSERT [dbo].[Car_Categories] ([ID], [Description]) VALUES (6, N'5 banh')
SET IDENTITY_INSERT [dbo].[Car_Categories] OFF
SET IDENTITY_INSERT [dbo].[Car_Fuels] ON 

INSERT [dbo].[Car_Fuels] ([ID], [Description]) VALUES (1, N'Gasoline')
INSERT [dbo].[Car_Fuels] ([ID], [Description]) VALUES (2, N'Diesel')
SET IDENTITY_INSERT [dbo].[Car_Fuels] OFF
SET IDENTITY_INSERT [dbo].[Car_Manufacturers] ON 

INSERT [dbo].[Car_Manufacturers] ([ID], [Name]) VALUES (1, N'Vinfast')
INSERT [dbo].[Car_Manufacturers] ([ID], [Name]) VALUES (2, N'Audi')
INSERT [dbo].[Car_Manufacturers] ([ID], [Name]) VALUES (3, N'Mercedes')
INSERT [dbo].[Car_Manufacturers] ([ID], [Name]) VALUES (4, N'BMW')
INSERT [dbo].[Car_Manufacturers] ([ID], [Name]) VALUES (5, N'Lexus')
SET IDENTITY_INSERT [dbo].[Car_Manufacturers] OFF
SET IDENTITY_INSERT [dbo].[Car_Status] ON 

INSERT [dbo].[Car_Status] ([ID], [Description]) VALUES (1, N'Selling')
INSERT [dbo].[Car_Status] ([ID], [Description]) VALUES (2, N'Deleted')
SET IDENTITY_INSERT [dbo].[Car_Status] OFF
SET IDENTITY_INSERT [dbo].[Car_Tranmissions] ON 

INSERT [dbo].[Car_Tranmissions] ([ID], [Description]) VALUES (1, N'Automatic')
INSERT [dbo].[Car_Tranmissions] ([ID], [Description]) VALUES (2, N'Manual')
INSERT [dbo].[Car_Tranmissions] ([ID], [Description]) VALUES (3, N'CVT')
SET IDENTITY_INSERT [dbo].[Car_Tranmissions] OFF
SET IDENTITY_INSERT [dbo].[Car_Types] ON 

INSERT [dbo].[Car_Types] ([ID], [Description]) VALUES (1, N'New')
INSERT [dbo].[Car_Types] ([ID], [Description]) VALUES (2, N'Nearly New')
INSERT [dbo].[Car_Types] ([ID], [Description]) VALUES (3, N'Used')
SET IDENTITY_INSERT [dbo].[Car_Types] OFF
SET IDENTITY_INSERT [dbo].[Cars] ON 

INSERT [dbo].[Cars] ([ID], [Model_Name], [Price], [Produced_Year], [Accquired_Date], [Engine], [Quantity], [Manufacturer_ID], [Tranmission_ID], [Type_ID], [Category_ID], [Fuel_ID], [Status_ID]) VALUES (1, N'VinFast Lux A2.0', 50000, 2019, CAST(N'2020-03-29T00:00:00.000' AS DateTime), 400, 46, 1, 1, 2, 5, 1, 1)
INSERT [dbo].[Cars] ([ID], [Model_Name], [Price], [Produced_Year], [Accquired_Date], [Engine], [Quantity], [Manufacturer_ID], [Tranmission_ID], [Type_ID], [Category_ID], [Fuel_ID], [Status_ID]) VALUES (3, N'VinFast Lux SA2.0', 70000, 2019, CAST(N'2020-03-29T00:00:00.000' AS DateTime), 450, 29, 1, 1, 1, 3, 1, 1)
INSERT [dbo].[Cars] ([ID], [Model_Name], [Price], [Produced_Year], [Accquired_Date], [Engine], [Quantity], [Manufacturer_ID], [Tranmission_ID], [Type_ID], [Category_ID], [Fuel_ID], [Status_ID]) VALUES (4, N'Mercedes AMG GT', 100000, 2020, CAST(N'2020-03-29T00:00:00.000' AS DateTime), 640, 18, 3, 1, 1, 2, 1, 1)
INSERT [dbo].[Cars] ([ID], [Model_Name], [Price], [Produced_Year], [Accquired_Date], [Engine], [Quantity], [Manufacturer_ID], [Tranmission_ID], [Type_ID], [Category_ID], [Fuel_ID], [Status_ID]) VALUES (7, N'GLC 350', 40000, 2019, CAST(N'2020-04-02T23:54:01.060' AS DateTime), 350, 20, 3, 1, 1, 5, 1, 1)
SET IDENTITY_INSERT [dbo].[Cars] OFF
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([ID], [Fullname], [Phone], [Email]) VALUES (1, N'Thùy Duyên', N'0963492364', N'duyen123@gmail.com')
INSERT [dbo].[Customers] ([ID], [Fullname], [Phone], [Email]) VALUES (2, N'Toàn', N'0977544551', N'toanhoang@gmail.com')
SET IDENTITY_INSERT [dbo].[Customers] OFF
SET IDENTITY_INSERT [dbo].[Invoice_Details] ON 

INSERT [dbo].[Invoice_Details] ([ID], [Car_ID], [Invoice_ID], [Unit_Price], [Quantity]) VALUES (1, 1, 1, 50000, 1)
INSERT [dbo].[Invoice_Details] ([ID], [Car_ID], [Invoice_ID], [Unit_Price], [Quantity]) VALUES (2, 4, 2, 100000, 2)
INSERT [dbo].[Invoice_Details] ([ID], [Car_ID], [Invoice_ID], [Unit_Price], [Quantity]) VALUES (3, 1, 3, 50000, 3)
INSERT [dbo].[Invoice_Details] ([ID], [Car_ID], [Invoice_ID], [Unit_Price], [Quantity]) VALUES (4, 3, 4, 70000, 1)
SET IDENTITY_INSERT [dbo].[Invoice_Details] OFF
SET IDENTITY_INSERT [dbo].[Invoices] ON 

INSERT [dbo].[Invoices] ([ID], [Date_Of_Purcharse], [Customer_ID]) VALUES (1, CAST(N'2020-03-04T14:24:09.193' AS DateTime), 1)
INSERT [dbo].[Invoices] ([ID], [Date_Of_Purcharse], [Customer_ID]) VALUES (2, CAST(N'2020-02-04T14:26:06.433' AS DateTime), 1)
INSERT [dbo].[Invoices] ([ID], [Date_Of_Purcharse], [Customer_ID]) VALUES (3, CAST(N'2020-04-04T14:26:28.963' AS DateTime), 2)
INSERT [dbo].[Invoices] ([ID], [Date_Of_Purcharse], [Customer_ID]) VALUES (4, CAST(N'2020-04-04T19:59:44.103' AS DateTime), 2)
SET IDENTITY_INSERT [dbo].[Invoices] OFF
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Account_Roles] FOREIGN KEY([Account_Role_ID])
REFERENCES [dbo].[Account_Roles] ([ID])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Account_Roles]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Account_Status] FOREIGN KEY([Account_Status_ID])
REFERENCES [dbo].[Account_Status] ([ID])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Account_Status]
GO
ALTER TABLE [dbo].[Cars]  WITH CHECK ADD  CONSTRAINT [FK_Cars_Car_Categories] FOREIGN KEY([Category_ID])
REFERENCES [dbo].[Car_Categories] ([ID])
GO
ALTER TABLE [dbo].[Cars] CHECK CONSTRAINT [FK_Cars_Car_Categories]
GO
ALTER TABLE [dbo].[Cars]  WITH CHECK ADD  CONSTRAINT [FK_Cars_Car_Fuels] FOREIGN KEY([Fuel_ID])
REFERENCES [dbo].[Car_Fuels] ([ID])
GO
ALTER TABLE [dbo].[Cars] CHECK CONSTRAINT [FK_Cars_Car_Fuels]
GO
ALTER TABLE [dbo].[Cars]  WITH CHECK ADD  CONSTRAINT [FK_Cars_Car_Manufacturers] FOREIGN KEY([Manufacturer_ID])
REFERENCES [dbo].[Car_Manufacturers] ([ID])
GO
ALTER TABLE [dbo].[Cars] CHECK CONSTRAINT [FK_Cars_Car_Manufacturers]
GO
ALTER TABLE [dbo].[Cars]  WITH CHECK ADD  CONSTRAINT [FK_Cars_Car_Status] FOREIGN KEY([Status_ID])
REFERENCES [dbo].[Car_Status] ([ID])
GO
ALTER TABLE [dbo].[Cars] CHECK CONSTRAINT [FK_Cars_Car_Status]
GO
ALTER TABLE [dbo].[Cars]  WITH CHECK ADD  CONSTRAINT [FK_Cars_Car_Tranmissions] FOREIGN KEY([Tranmission_ID])
REFERENCES [dbo].[Car_Tranmissions] ([ID])
GO
ALTER TABLE [dbo].[Cars] CHECK CONSTRAINT [FK_Cars_Car_Tranmissions]
GO
ALTER TABLE [dbo].[Cars]  WITH CHECK ADD  CONSTRAINT [FK_Cars_Car_Types] FOREIGN KEY([Type_ID])
REFERENCES [dbo].[Car_Types] ([ID])
GO
ALTER TABLE [dbo].[Cars] CHECK CONSTRAINT [FK_Cars_Car_Types]
GO
ALTER TABLE [dbo].[Invoice_Details]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Details_Cars] FOREIGN KEY([Car_ID])
REFERENCES [dbo].[Cars] ([ID])
GO
ALTER TABLE [dbo].[Invoice_Details] CHECK CONSTRAINT [FK_Invoice_Details_Cars]
GO
ALTER TABLE [dbo].[Invoice_Details]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Details_Invoices] FOREIGN KEY([Invoice_ID])
REFERENCES [dbo].[Invoices] ([ID])
GO
ALTER TABLE [dbo].[Invoice_Details] CHECK CONSTRAINT [FK_Invoice_Details_Invoices]
GO
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [FK_Invoices_Customers] FOREIGN KEY([Customer_ID])
REFERENCES [dbo].[Customers] ([ID])
GO
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [FK_Invoices_Customers]
GO
USE [master]
GO
ALTER DATABASE [CarManagement] SET  READ_WRITE 
GO
