USE [EcommerceDb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CUSTOMERS](
	[CUSTOMERID] [nvarchar](10) NOT NULL,
	[FIRSTNAME] [nvarchar](50) NULL,
	[LASTNAME] [nvarchar](50) NULL,
	[EMAIL] [nvarchar](50) NULL,
	[HOUSENO] [nvarchar](50) NULL,
	[STREET] [nvarchar](50) NULL,
	[TOWN] [nvarchar](50) NULL,
	[POSTCODE] [nvarchar](10) NULL,
 CONSTRAINT [PK__CUSTOMER__61DBD78883467B11] PRIMARY KEY CLUSTERED 
(
	[CUSTOMERID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO