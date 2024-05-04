USE [master]
GO
/****** Object:  Database [LungMed]    Script Date: 04.05.2024 08:59:41 ******/
CREATE DATABASE [LungMed]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LungMed', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\LungMed.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LungMed_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\LungMed_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [LungMed] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LungMed].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LungMed] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LungMed] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LungMed] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LungMed] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LungMed] SET ARITHABORT OFF 
GO
ALTER DATABASE [LungMed] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LungMed] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LungMed] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LungMed] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LungMed] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LungMed] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LungMed] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LungMed] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LungMed] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LungMed] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LungMed] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LungMed] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LungMed] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LungMed] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LungMed] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LungMed] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LungMed] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LungMed] SET RECOVERY FULL 
GO
ALTER DATABASE [LungMed] SET  MULTI_USER 
GO
ALTER DATABASE [LungMed] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LungMed] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LungMed] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LungMed] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LungMed] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [LungMed] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'LungMed', N'ON'
GO
ALTER DATABASE [LungMed] SET QUERY_STORE = OFF
GO
USE [LungMed]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 04.05.2024 08:59:41 ******/
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
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 04.05.2024 08:59:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 04.05.2024 08:59:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 04.05.2024 08:59:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 04.05.2024 08:59:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 04.05.2024 08:59:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 04.05.2024 08:59:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[CreatedById] [nvarchar](max) NULL,
	[CreatedOn] [datetime2](7) NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[RoleId] [nvarchar](450) NULL,
	[DoctorId] [int] NULL,
	[PatientId] [int] NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 04.05.2024 08:59:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doctor]    Script Date: 04.05.2024 08:59:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Doctor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HealthCard]    Script Date: 04.05.2024 08:59:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HealthCard](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Medicines] [nvarchar](max) NULL,
	[Diseases] [nvarchar](max) NULL,
	[Allergies] [nvarchar](max) NULL,
	[BleedingDisorders] [nvarchar](max) NULL,
	[Pregnancy] [bit] NULL,
	[PregnancyWeek] [int] NULL,
	[Date] [datetime2](7) NOT NULL,
	[PatientId] [int] NOT NULL,
 CONSTRAINT [PK_HealthCard] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 04.05.2024 08:59:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[PersonalNumber] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NOT NULL,
	[BirhtDate] [datetime2](7) NOT NULL,
	[Sex] [int] NOT NULL,
	[DoctorId] [int] NOT NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'00000000000000_CreateIdentitySchema', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240503115632_InitialCreate', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240503132023_AddingUsers', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240503133312_FixingUsers', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240503141808_AddingTables', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240503195041_AddingRowsInApplicationUser', N'8.0.4')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'0695d69f-ceb8-4319-a8f4-61f1d9e4291a', N'Administrator', N'ADMINISTRATOR', N'b4f541ed-d661-4082-820c-9fd4932c55d6')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'0b17b658-538a-4fdb-b12c-6350572fa269', N'Pacjent', N'PACJENT', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'af1cf5f7-6c72-41c2-9a85-fef04e9ecd51', N'Lekarz', N'LEKARZ', NULL)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'313fc2dc-9274-4aab-a7ec-cd92b0155031', N'0695d69f-ceb8-4319-a8f4-61f1d9e4291a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'30148440-6ad0-433b-b4e1-9cc18e06a219', N'0b17b658-538a-4fdb-b12c-6350572fa269')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'49760748-04c0-484e-85a5-42b4579e0d02', N'0b17b658-538a-4fdb-b12c-6350572fa269')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'519b38e3-a722-4c1b-916d-0e841052b5e7', N'af1cf5f7-6c72-41c2-9a85-fef04e9ecd51')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b194b5f5-87f5-471c-b2cb-04ad16d5244c', N'af1cf5f7-6c72-41c2-9a85-fef04e9ecd51')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [CreatedById], [CreatedOn], [FirstName], [LastName], [RoleId], [DoctorId], [PatientId]) VALUES (N'30148440-6ad0-433b-b4e1-9cc18e06a219', N'zuzajac@gmail.com', N'ZUZAJAC@GMAIL.COM', N'zuzajac@gmail.com', N'ZUZAJAC@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEAU/9s6Fxc/qOFbIk37qYC6gqc2qU7WSCFDWELVvTbIXjGzRVZNzXbkkA367nruMeA==', N'BBWM7HUS2MWM2H2MIMBLDMDDV53VKEGT', N'0b84c073-dbea-4960-b043-505a090ec28c', N'533232393', 0, 0, NULL, 1, 0, N'Admin', CAST(N'2024-05-04T05:41:49.8146959' AS DateTime2), N'Zuzanna', N'Zajonc', N'0b17b658-538a-4fdb-b12c-6350572fa269', NULL, 2)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [CreatedById], [CreatedOn], [FirstName], [LastName], [RoleId], [DoctorId], [PatientId]) VALUES (N'313fc2dc-9274-4aab-a7ec-cd92b0155031', N'kamillewandowski@gmail.com', N'KAMILLEWANDOWSKI@GMAIL.COM', N'kamillewandowski@gmail.com', N'KAMILLEWANDOWSKI@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEBO2DGb1S08syRe284rB0KVE2SJpVf24WdjBqPSiF04OcCGvlcr3lVaijeATDZ6mjg==', N'246MSJTSFPQZWXYP3ODSAYJDEBXNMNSH', N'65ccef1d-6f31-4f57-8f29-a545ccc217cf', N'123456788', 0, 0, NULL, 1, 0, N'Admin', CAST(N'2024-05-03T15:44:50.9072580' AS DateTime2), N'Kamil', N'Lewandowski', N'0695d69f-ceb8-4319-a8f4-61f1d9e4291a', NULL, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [CreatedById], [CreatedOn], [FirstName], [LastName], [RoleId], [DoctorId], [PatientId]) VALUES (N'49760748-04c0-484e-85a5-42b4579e0d02', N'mariannatybura@gmail.com', N'MARIANNATYBURA@GMAIL.COM', N'mariannatybura@gmail.com', N'MARIANNATYBURA@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAECcEtstbMqj163osKQE8c96ItR8qRa1+Ep1O6Qeu/Ropt1uHYTUZq5DyHvfugXMDTw==', N'N5LC736BIKKYURNLXCGD3GWVY5LRKRYX', N'f7c47b2a-d62a-4e3e-946d-d4040698e83b', N'502253737', 1, 0, NULL, 1, 0, N'Admin', CAST(N'2024-05-04T05:57:10.8007738' AS DateTime2), N'Marianna', N'Tybura', N'0b17b658-538a-4fdb-b12c-6350572fa269', NULL, 1)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [CreatedById], [CreatedOn], [FirstName], [LastName], [RoleId], [DoctorId], [PatientId]) VALUES (N'519b38e3-a722-4c1b-916d-0e841052b5e7', N'ewalis@gmail.com', N'EWALIS@GMAIL.COM', N'ewalis@gmail.com', N'EWALIS@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEDGr9zF25QpTOraZfmtKJvMCIc9Iciac2BdryIBxByEKduBnhBGg+n72zIhq9FYknw==', N'GPQYQEOHRBHSLWLGICEC3Z2HPS5LBN6A', N'548a0bf7-7650-427a-a9f2-eb9e4bd0c441', N'111444555', 1, 0, NULL, 1, 0, N'Admin', CAST(N'2024-05-04T05:55:30.9079001' AS DateTime2), N'Ewa', N'Lisa', N'af1cf5f7-6c72-41c2-9a85-fef04e9ecd51', 3, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [CreatedById], [CreatedOn], [FirstName], [LastName], [RoleId], [DoctorId], [PatientId]) VALUES (N'b194b5f5-87f5-471c-b2cb-04ad16d5244c', N'dominikaszulc@gmail.com', N'DOMINIKASZULC@GMAIL.COM', N'dominikaszulc@gmail.com', N'DOMINIKASZULC@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEETmQqKn2vtAr7aDv9MqAUVoZQplLvwmH5O686mWUCvUXBQiAjwZH2NDsOmLSo0SyQ==', N'IATZT7KAAN4BZ6EP3GTORGUGBFLC3FHZ', N'de96f28c-310e-4911-b328-ef56013969c1', N'222111444', 1, 0, NULL, 1, 0, N'Admin', CAST(N'2024-05-03T22:16:14.9675955' AS DateTime2), N'Dominika', N'Szulc', N'af1cf5f7-6c72-41c2-9a85-fef04e9ecd51', 2, NULL)
GO
INSERT [dbo].[AspNetUserTokens] ([UserId], [LoginProvider], [Name], [Value]) VALUES (N'313fc2dc-9274-4aab-a7ec-cd92b0155031', N'[AspNetUserStore]', N'AuthenticatorKey', N'4DRD2HGFTJPZYE4CVNB43POHGZ75KBBN')
GO
SET IDENTITY_INSERT [dbo].[Doctor] ON 

INSERT [dbo].[Doctor] ([Id], [FirstName], [LastName], [PhoneNumber]) VALUES (1, N'Grażyna', N'Jankowska', N'123456789')
INSERT [dbo].[Doctor] ([Id], [FirstName], [LastName], [PhoneNumber]) VALUES (2, N'Dominika', N'Szulc', N'222111444')
INSERT [dbo].[Doctor] ([Id], [FirstName], [LastName], [PhoneNumber]) VALUES (3, N'Ewa', N'Lisa', N'111444555')
SET IDENTITY_INSERT [dbo].[Doctor] OFF
GO
SET IDENTITY_INSERT [dbo].[HealthCard] ON 

INSERT [dbo].[HealthCard] ([Id], [Medicines], [Diseases], [Allergies], [BleedingDisorders], [Pregnancy], [PregnancyWeek], [Date], [PatientId]) VALUES (1, N'Bufomix Easyhaler - 1 inhalation twice a day', N'Asthma', N'Grasses and pollen', N'-', 0, 0, CAST(N'2024-05-03T00:00:00.0000000' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[HealthCard] OFF
GO
SET IDENTITY_INSERT [dbo].[Patient] ON 

INSERT [dbo].[Patient] ([Id], [FirstName], [LastName], [PersonalNumber], [PhoneNumber], [BirhtDate], [Sex], [DoctorId]) VALUES (1, N'Marianna', N'Tybura', N'11111111111', N'502253737', CAST(N'2002-01-01T00:00:00.0000000' AS DateTime2), 1, 1)
INSERT [dbo].[Patient] ([Id], [FirstName], [LastName], [PersonalNumber], [PhoneNumber], [BirhtDate], [Sex], [DoctorId]) VALUES (2, N'Zuzanna', N'Zajonc', N'11122233300', N'222555888', CAST(N'2002-01-01T00:00:00.0000000' AS DateTime2), 1, 2)
SET IDENTITY_INSERT [dbo].[Patient] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 04.05.2024 08:59:41 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 04.05.2024 08:59:41 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 04.05.2024 08:59:41 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 04.05.2024 08:59:41 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 04.05.2024 08:59:41 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 04.05.2024 08:59:41 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AspNetUsers_DoctorId]    Script Date: 04.05.2024 08:59:41 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUsers_DoctorId] ON [dbo].[AspNetUsers]
(
	[DoctorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AspNetUsers_PatientId]    Script Date: 04.05.2024 08:59:41 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUsers_PatientId] ON [dbo].[AspNetUsers]
(
	[PatientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUsers_RoleId]    Script Date: 04.05.2024 08:59:41 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUsers_RoleId] ON [dbo].[AspNetUsers]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 04.05.2024 08:59:41 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HealthCard_PatientId]    Script Date: 04.05.2024 08:59:41 ******/
CREATE NONCLUSTERED INDEX [IX_HealthCard_PatientId] ON [dbo].[HealthCard]
(
	[PatientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Patient_DoctorId]    Script Date: 04.05.2024 08:59:41 ******/
CREATE NONCLUSTERED INDEX [IX_Patient_DoctorId] ON [dbo].[Patient]
(
	[DoctorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUsers_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
GO
ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_AspNetUsers_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUsers_Doctor_DoctorId] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Doctor] ([Id])
GO
ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_AspNetUsers_Doctor_DoctorId]
GO
ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUsers_Patient_PatientId] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patient] ([Id])
GO
ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_AspNetUsers_Patient_PatientId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[HealthCard]  WITH CHECK ADD  CONSTRAINT [FK_HealthCard_Patient_PatientId] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patient] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HealthCard] CHECK CONSTRAINT [FK_HealthCard_Patient_PatientId]
GO
ALTER TABLE [dbo].[Patient]  WITH CHECK ADD  CONSTRAINT [FK_Patient_Doctor_DoctorId] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Doctor] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Patient] CHECK CONSTRAINT [FK_Patient_Doctor_DoctorId]
GO
USE [master]
GO
ALTER DATABASE [LungMed] SET  READ_WRITE 
GO
