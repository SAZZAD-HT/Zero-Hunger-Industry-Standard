
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/11/2023 20:35:16
-- Generated from EDMX file: C:\Users\TRIFO\source\repos\Zero Hunger Industry Standard\Zero Hunger Industry Standard\Models\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [db_aa135e_ibos1234];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[tblLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblLog];
GO
IF OBJECT_ID(N'[dbo].[tblNgo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblNgo];
GO
IF OBJECT_ID(N'[dbo].[TblRestrurantHeader]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TblRestrurantHeader];
GO
IF OBJECT_ID(N'[dbo].[TblRestrurantRow]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TblRestrurantRow];
GO
IF OBJECT_ID(N'[dbo].[tblUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblUser];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'tblLogs'
CREATE TABLE [dbo].[tblLogs] (
    [intAutoId] int IDENTITY(1,1) NOT NULL,
    [intRestrurantId] bigint  NULL,
    [intRowid] bigint  NULL,
    [IntUserID] bigint  NULL
);
GO

-- Creating table 'TblRestrurantHeaders'
CREATE TABLE [dbo].[TblRestrurantHeaders] (
    [intRestrurantId] bigint IDENTITY(1,1) NOT NULL,
    [strRestrurantName] nvarchar(100)  NOT NULL,
    [strRestrurantType] nvarchar(100)  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'TblRestrurantRows'
CREATE TABLE [dbo].[TblRestrurantRows] (
    [intRowId] bigint IDENTITY(1,1) NOT NULL,
    [strFood] nvarchar(100)  NOT NULL,
    [StrFoodType] nvarchar(100)  NOT NULL,
    [intRestrurantId] bigint  NOT NULL,
    [intEmployeeId] bigint  NULL,
    [IsCollectReqest] bit  NULL,
    [DteCollectionTimeLeft] datetime  NULL,
    [IsActive] bit  NULL
);
GO

-- Creating table 'tblUsers'
CREATE TABLE [dbo].[tblUsers] (
    [intUserID] bigint IDENTITY(1,1) NOT NULL,
    [strUserType] bigint  NOT NULL,
    [strUserName] bigint  NOT NULL,
    [IsActive] bit  NOT NULL,
    [intNgoID] bigint  NOT NULL,
    [strUserMail] nvarchar(500)  NULL,
    [strPassword] nvarchar(500)  NULL
);
GO

-- Creating table 'tblNgoes'
CREATE TABLE [dbo].[tblNgoes] (
    [intNgoId] bigint IDENTITY(1,1) NOT NULL,
    [strNgoName] nvarchar(50)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [intAutoId] in table 'tblLogs'
ALTER TABLE [dbo].[tblLogs]
ADD CONSTRAINT [PK_tblLogs]
    PRIMARY KEY CLUSTERED ([intAutoId] ASC);
GO

-- Creating primary key on [intRestrurantId] in table 'TblRestrurantHeaders'
ALTER TABLE [dbo].[TblRestrurantHeaders]
ADD CONSTRAINT [PK_TblRestrurantHeaders]
    PRIMARY KEY CLUSTERED ([intRestrurantId] ASC);
GO

-- Creating primary key on [intRowId] in table 'TblRestrurantRows'
ALTER TABLE [dbo].[TblRestrurantRows]
ADD CONSTRAINT [PK_TblRestrurantRows]
    PRIMARY KEY CLUSTERED ([intRowId] ASC);
GO

-- Creating primary key on [intUserID] in table 'tblUsers'
ALTER TABLE [dbo].[tblUsers]
ADD CONSTRAINT [PK_tblUsers]
    PRIMARY KEY CLUSTERED ([intUserID] ASC);
GO

-- Creating primary key on [intNgoId] in table 'tblNgoes'
ALTER TABLE [dbo].[tblNgoes]
ADD CONSTRAINT [PK_tblNgoes]
    PRIMARY KEY CLUSTERED ([intNgoId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------