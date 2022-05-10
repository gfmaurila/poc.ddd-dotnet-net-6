USE [UserAPI]
GO
/****** Object:  Table [dbo].[User]    Script Date: 25/04/2022 23:40:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [varchar](80) NOT NULL,
	[email] [varchar](180) NOT NULL,
	[password] [varchar](1000) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [name], [email], [password]) VALUES (31, N'string', N'string@teste.com', N'@G147a23')
INSERT [dbo].[User] ([Id], [name], [email], [password]) VALUES (33, N'string', N'strings@teste.com', N'string')
INSERT [dbo].[User] ([Id], [name], [email], [password]) VALUES (34, N'string', N'string@teste.com', N'string')
INSERT [dbo].[User] ([Id], [name], [email], [password]) VALUES (35, N'string', N'string@tessssste.com', N'string')
INSERT [dbo].[User] ([Id], [name], [email], [password]) VALUES (36, N'stringteste', N'string@teste123tGOeste.com', N'string@teste')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
