USE [Example]
/****** Object:  Table [dbo].[Manager]    Script Date: 04/05/2016 22:59:07 ******/
SET IDENTITY_INSERT [dbo].[Manager] ON
INSERT [dbo].[Manager] ([Id], [LoginName], [UserName], [Password], [RoleId], [Cellphone], [Email], [IsEnable], [IsSys], [CreatedTime], [Version]) VALUES (5, N'admin', N'阿德曼', N'admin', 1, N'110', N'4062499@qq.com', 1, 1, CAST(0x0000A41300000000 AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Manager] OFF
