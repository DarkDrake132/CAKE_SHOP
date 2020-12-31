CREATE DATABASE CakeShop
go
USE [CakeShop]
GO
/****** Object:  Table [dbo].[CAKE]    Script Date: 12/28/2020 12:57:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAKE](
	[ID] [int] NOT NULL,
	[C_NAME] [nvarchar](max) NULL,
	[INFO] [nvarchar](max) NULL,
	[TYPEID] [int] NULL,
	[PRICE] [int] NULL,
	[IMG] [nvarchar](max) NULL,
	[VISIBLE] [int] NULL default 1,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CAKE_TYPE]    Script Date: 12/28/2020 12:57:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAKE_TYPE](
	[ID] [int] NOT NULL,
	[C_NAME] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RECEIPT]    Script Date: 12/28/2020 12:57:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RECEIPT](
	[ID] [int] NOT NULL,
	[INPUTDATE] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RECEIPT_DETAIL]    Script Date: 12/28/2020 12:57:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RECEIPT_DETAIL](
	[RECEIPT_ID] [int] NOT NULL,
	[SERIAL] [int] NOT NULL,
	[CAKE_ID] [int] NULL,
	[TYPEID] [int] NULL,
	[AMOUNT] [int] NULL,
	[TOTAL] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[SERIAL] ASC,
	[RECEIPT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[CAKE] ([ID], [C_NAME], [INFO], [TYPEID], [PRICE], [IMG]) VALUES (1, N'Bánh Sô-cô-la', N'Bánh Sô-cô-la', 1, 180000, N'Best-Birthday-Cake-with-milk-chocolate-buttercream-SQUARE.jpg')
INSERT [dbo].[CAKE] ([ID], [C_NAME], [INFO], [TYPEID], [PRICE], [IMG]) VALUES (2, N'Bánh Dâu', N'Bánh Dâu', 1, 240000, N'download.jpg')
INSERT [dbo].[CAKE] ([ID], [C_NAME], [INFO], [TYPEID], [PRICE], [IMG]) VALUES (3, N'Tiramisu', N'Tiramisu', 3, 90000, N'20186171820-tiramisu-16k.jpg')
INSERT [dbo].[CAKE] ([ID], [C_NAME], [INFO], [TYPEID], [PRICE], [IMG]) VALUES (4, N'The Bear', N'The Bear', 3, 135000, N'2018620155259-the-bear-16k.jpg')
INSERT [dbo].[CAKE] ([ID], [C_NAME], [INFO], [TYPEID], [PRICE], [IMG]) VALUES (5, N'Bánh bao nhân đậu đỏ', N'Bánh bao nhân đậu đỏ', 2, 12000, N'banh-bao-nhan-dau-do-13555.jpg')
INSERT [dbo].[CAKE] ([ID], [C_NAME], [INFO], [TYPEID], [PRICE], [IMG]) VALUES (6, N'Bánh bao mè', N'Bánh bao mè', 2, 19000, N'banh-bao-me-13557.jpg')
GO
INSERT [dbo].[CAKE_TYPE] ([ID], [C_NAME]) VALUES (1, N'Bánh kem')
INSERT [dbo].[CAKE_TYPE] ([ID], [C_NAME]) VALUES (2, N'Bánh bao')
INSERT [dbo].[CAKE_TYPE] ([ID], [C_NAME]) VALUES (3, N'Donut')
GO
INSERT [dbo].[RECEIPT] ([ID], [INPUTDATE]) VALUES (1, CAST(N'2020-01-05' AS Date))
INSERT [dbo].[RECEIPT] ([ID], [INPUTDATE]) VALUES (2, CAST(N'2020-01-23' AS Date))
INSERT [dbo].[RECEIPT] ([ID], [INPUTDATE]) VALUES (3, CAST(N'2020-01-31' AS Date))
INSERT [dbo].[RECEIPT] ([ID], [INPUTDATE]) VALUES (4, CAST(N'2020-03-24' AS Date))
INSERT [dbo].[RECEIPT] ([ID], [INPUTDATE]) VALUES (5, CAST(N'2020-06-09' AS Date))
INSERT [dbo].[RECEIPT] ([ID], [INPUTDATE]) VALUES (6, CAST(N'2020-10-16' AS Date))
INSERT [dbo].[RECEIPT] ([ID], [INPUTDATE]) VALUES (7, CAST(N'2020-12-14' AS Date))
GO
INSERT [dbo].[RECEIPT_DETAIL] ([RECEIPT_ID], [SERIAL], [CAKE_ID], [TYPEID], [AMOUNT], [TOTAL]) VALUES (1, 1, 1, 1, 3, 540000)
INSERT [dbo].[RECEIPT_DETAIL] ([RECEIPT_ID], [SERIAL], [CAKE_ID], [TYPEID], [AMOUNT], [TOTAL]) VALUES (2, 1, 5, 2, 5, 60000)
INSERT [dbo].[RECEIPT_DETAIL] ([RECEIPT_ID], [SERIAL], [CAKE_ID], [TYPEID], [AMOUNT], [TOTAL]) VALUES (3, 1, 2, 1, 1, 240000)
INSERT [dbo].[RECEIPT_DETAIL] ([RECEIPT_ID], [SERIAL], [CAKE_ID], [TYPEID], [AMOUNT], [TOTAL]) VALUES (4, 1, 4, 3, 2, 270000)
INSERT [dbo].[RECEIPT_DETAIL] ([RECEIPT_ID], [SERIAL], [CAKE_ID], [TYPEID], [AMOUNT], [TOTAL]) VALUES (5, 1, 3, 3, 1, 90000)
INSERT [dbo].[RECEIPT_DETAIL] ([RECEIPT_ID], [SERIAL], [CAKE_ID], [TYPEID], [AMOUNT], [TOTAL]) VALUES (6, 1, 6, 2, 1, 19000)
INSERT [dbo].[RECEIPT_DETAIL] ([RECEIPT_ID], [SERIAL], [CAKE_ID], [TYPEID], [AMOUNT], [TOTAL]) VALUES (7, 1, 6, 2, 1, 19000)
INSERT [dbo].[RECEIPT_DETAIL] ([RECEIPT_ID], [SERIAL], [CAKE_ID], [TYPEID], [AMOUNT], [TOTAL]) VALUES (1, 2, 2, 1, 2, 480000)
INSERT [dbo].[RECEIPT_DETAIL] ([RECEIPT_ID], [SERIAL], [CAKE_ID], [TYPEID], [AMOUNT], [TOTAL]) VALUES (5, 2, 1, 1, 1, 180000)
INSERT [dbo].[RECEIPT_DETAIL] ([RECEIPT_ID], [SERIAL], [CAKE_ID], [TYPEID], [AMOUNT], [TOTAL]) VALUES (7, 2, 3, 3, 1, 90000)
GO
ALTER TABLE [dbo].[CAKE]  WITH CHECK ADD FOREIGN KEY([TYPEID])
REFERENCES [dbo].[CAKE_TYPE] ([ID])
GO
ALTER TABLE [dbo].[RECEIPT_DETAIL]  WITH CHECK ADD FOREIGN KEY([CAKE_ID])
REFERENCES [dbo].[CAKE] ([ID])
GO
ALTER TABLE [dbo].[RECEIPT_DETAIL]  WITH CHECK ADD FOREIGN KEY([RECEIPT_ID])
REFERENCES [dbo].[RECEIPT] ([ID])
GO
ALTER TABLE [dbo].[RECEIPT_DETAIL]  WITH CHECK ADD FOREIGN KEY([TYPEID])
REFERENCES [dbo].[CAKE_TYPE] ([ID])
GO
