USE TrainingDB
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PRODUCT')
BEGIN
CREATE TABLE [PRODUCT](
 [Id] uniqueidentifier NOT NULL,
 [Name] [nvarchar](50) NOT NULL,
 [Description] [nvarchar](MAX) NULL,
 [ImageFile] [varchar](200) NULL,
 [Price] [decimal](10,2) NULL,
 CONSTRAINT PK_PRODUCT PRIMARY KEY (Id, Name)
)
END
ELSE
BEGIN
    PRINT 'The table "ProjectStatus" already exists.'
END
GO