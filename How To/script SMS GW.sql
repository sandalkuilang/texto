USE [SMSGW]
GO
/****** Object:  Table [dbo].[WeeklyTrigger]    Script Date: 04/17/2015 20:04:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WeeklyTrigger](
	[ID] [varchar](10) NOT NULL,
	[RecursEvery] [int] NOT NULL,
	[Sunday] [int] NOT NULL,
	[Monday] [int] NOT NULL,
	[Tuesday] [int] NOT NULL,
	[Wednesday] [int] NOT NULL,
	[Thursday] [int] NOT NULL,
	[Friday] [int] NOT NULL,
	[Saturday] [int] NOT NULL,
 CONSTRAINT [PK_WeeklyTrigger] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ResultWorkItem]    Script Date: 04/17/2015 20:04:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ResultWorkItem](
	[SeqNbr] [varchar](20) NOT NULL,
	[Response] [text] NULL,
	[CreatedDate] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Quiz]    Script Date: 04/17/2015 20:04:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Quiz](
	[Group] [int] NOT NULL,
	[Keyword] [varchar](1000) NOT NULL,
	[Response] [varchar](1000) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QueueWorkItem]    Script Date: 04/17/2015 20:04:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QueueWorkItem](
	[SeqNbr] [varchar](20) NOT NULL,
	[Command] [varchar](max) NOT NULL,
	[ScheduleID] [varchar](20) NOT NULL,
	[Created] [datetime] NOT NULL,
	[Enabled] [bit] NOT NULL,
	[RecursPoint] [datetime] NULL,
	[LastExecuted] [datetime] NULL,
	[NextExecuted] [datetime] NULL,
	[Status] [varchar](1) NOT NULL,
 CONSTRAINT [PK_QueueWorkItem] PRIMARY KEY CLUSTERED 
(
	[SeqNbr] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PrefixPhoneName]    Script Date: 04/17/2015 20:04:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PrefixPhoneName](
	[ID] [varchar](3) NOT NULL,
	[PrefixPhoneName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_PrefixPhoneName] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PhoneNumber]    Script Date: 04/17/2015 20:04:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PhoneNumber](
	[IDContact] [varchar](20) NOT NULL,
	[IDPrefixPhoneName] [varchar](3) NOT NULL,
	[PhoneNumber] [varchar](50) NOT NULL,
 CONSTRAINT [PK_PhoneNumber] PRIMARY KEY CLUSTERED 
(
	[IDContact] ASC,
	[IDPrefixPhoneName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Outbox]    Script Date: 04/17/2015 20:04:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Outbox](
	[Sender] [varchar](30) NOT NULL,
	[SeqNbr] [varchar](20) NOT NULL,
	[Receiver] [varchar](870) NOT NULL,
	[Message] [varchar](max) NOT NULL,
	[Time] [datetime] NOT NULL,
	[SenderOperator] [varchar](30) NOT NULL,
	[DeviceResponse] [varchar](500) NULL,
	[NetworkStatus] [varchar](100) NULL,
	[Error] [varchar](200) NULL,
 CONSTRAINT [PK_Outbox_1] PRIMARY KEY CLUSTERED 
(
	[SeqNbr] ASC,
	[Receiver] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MonthlyTrigger]    Script Date: 04/17/2015 20:04:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MonthlyTrigger](
	[ID] [varchar](10) NOT NULL,
	[Days] [varchar](50) NOT NULL,
	[January] [int] NOT NULL,
	[February] [int] NOT NULL,
	[March] [int] NOT NULL,
	[April] [int] NOT NULL,
	[May] [int] NOT NULL,
	[June] [int] NOT NULL,
	[July] [int] NOT NULL,
	[August] [int] NOT NULL,
	[September] [int] NOT NULL,
	[October] [int] NOT NULL,
	[November] [int] NOT NULL,
	[December] [int] NOT NULL,
 CONSTRAINT [PK_MonthlyTrigger] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MessageStatus]    Script Date: 04/17/2015 20:04:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MessageStatus](
	[Phonenumber] [varchar](870) NOT NULL,
	[SeqNbr] [varchar](20) NOT NULL,
	[Status] [varchar](1) NOT NULL,
	[Source] [varchar](10) NOT NULL,
 CONSTRAINT [PK_MessageStatus_1] PRIMARY KEY CLUSTERED 
(
	[Phonenumber] ASC,
	[SeqNbr] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Inbox]    Script Date: 04/17/2015 20:04:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Inbox](
	[Sender] [varchar](30) NOT NULL,
	[SeqNbr] [varchar](20) NOT NULL,
	[Message] [varchar](max) NOT NULL,
	[Time] [datetime] NOT NULL,
	[ReceiverOperator] [varchar](30) NOT NULL,
	[IsRead] [int] NULL,
 CONSTRAINT [PK_Inbox_1] PRIMARY KEY CLUSTERED 
(
	[Sender] ASC,
	[SeqNbr] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DailyTrigger]    Script Date: 04/17/2015 20:04:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DailyTrigger](
	[ID] [varchar](10) NOT NULL,
	[RecursEvery] [int] NOT NULL,
 CONSTRAINT [PK_DailyTrigger] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ContactName]    Script Date: 04/17/2015 20:04:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ContactName](
	[ID] [varchar](20) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Note] [varchar](2000) NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Answer]    Script Date: 04/17/2015 20:04:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Answer](
	[KeyID] [varchar](100) NOT NULL,
	[Description] [varchar](3000) NOT NULL,
 CONSTRAINT [PK_Answer] PRIMARY KEY CLUSTERED 
(
	[KeyID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
