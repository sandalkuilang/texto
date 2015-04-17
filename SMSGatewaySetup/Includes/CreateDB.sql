USE [master]
GO
/****** Object:  Database [SMSGW]    Script Date: 03/18/2014 18:16:52 ******/
CREATE DATABASE [SMSGW] ON  PRIMARY 
( NAME = N'SMSGW_Data', FILENAME = N'c:\program files\microsoft sql server\mssql10_50.mssqlserver\mssql\data\SMSGW_Data.mdf' , SIZE = 10240KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SMSGW_Log', FILENAME = N'c:\program files\microsoft sql server\mssql10_50.mssqlserver\mssql\data\SMSGW_Log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SMSGW] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SMSGW].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SMSGW] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [SMSGW] SET ANSI_NULLS OFF
GO
ALTER DATABASE [SMSGW] SET ANSI_PADDING OFF
GO
ALTER DATABASE [SMSGW] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [SMSGW] SET ARITHABORT OFF
GO
ALTER DATABASE [SMSGW] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [SMSGW] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [SMSGW] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [SMSGW] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [SMSGW] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [SMSGW] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [SMSGW] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [SMSGW] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [SMSGW] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [SMSGW] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [SMSGW] SET  ENABLE_BROKER
GO
ALTER DATABASE [SMSGW] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [SMSGW] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [SMSGW] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [SMSGW] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [SMSGW] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [SMSGW] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [SMSGW] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [SMSGW] SET  READ_WRITE
GO
ALTER DATABASE [SMSGW] SET RECOVERY FULL
GO
ALTER DATABASE [SMSGW] SET  MULTI_USER
GO
ALTER DATABASE [SMSGW] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [SMSGW] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'SMSGW', N'ON'
GO
USE [SMSGW]
GO
/****** Object:  Table [dbo].[Outbox]    Script Date: 03/18/2014 18:16:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Outbox](
	[Sender] [varchar](30) NOT NULL,
	[SeqNbr] [int] NOT NULL,
	[Receiver] [varchar](30) NOT NULL,
	[Message] [varchar](2000) NOT NULL,
	[Time] [datetime] NOT NULL,
	[SenderOperator] [varchar](30) NOT NULL,
	[DeviceResponse] [varchar](500) NULL,
	[NetworkStatus] [varchar](100) NULL,
 CONSTRAINT [PK_Outbox_1] PRIMARY KEY CLUSTERED 
(
	[SeqNbr] ASC,
	[Receiver] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Inbox]    Script Date: 03/18/2014 18:16:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Inbox](
	[Sender] [varchar](30) NOT NULL,
	[SeqNbr] [int] NOT NULL,
	[Message] [varchar](2000) NOT NULL,
	[Time] [datetime] NOT NULL,
	[ReceiverOperator] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Inbox_1] PRIMARY KEY CLUSTERED 
(
	[Sender] ASC,
	[SeqNbr] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
