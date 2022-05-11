/****** Object:  Table [dbo].[Customer]    Script Date: 11/05/2022 10:35:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nchar](50) NOT NULL,
	[Surname] [nchar](50) NOT NULL,
	[PolicyReference] [nchar](10) NOT NULL,
	[DateOfBirth] [date] NULL,
	[Email] [nchar](255) NULL
) ON [PRIMARY]
GO

