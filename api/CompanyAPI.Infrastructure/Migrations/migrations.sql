IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'CompanyApi')
BEGIN
CREATE DATABASE [CompanyApi]

END
GO
    USE [CompanyApi]
GO


IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250523124659_Initial'
)
BEGIN
    CREATE TABLE [Company] (
        [Id] int NOT NULL IDENTITY,
        [Isin] nvarchar(12) NOT NULL,
        [Name] nvarchar(255) NOT NULL,
        [Exchange] nvarchar(255) NOT NULL,
        [Ticker] nvarchar(255) NOT NULL,
        [Website] nvarchar(255) NULL,
        CONSTRAINT [PK_Company] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250523124659_Initial'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Exchange', N'Isin', N'Name', N'Ticker', N'Website') AND [object_id] = OBJECT_ID(N'[Company]'))
        SET IDENTITY_INSERT [Company] ON;
    EXEC(N'INSERT INTO [Company] ([Id], [Exchange], [Isin], [Name], [Ticker], [Website])
    VALUES (1, N''NASDAQ'', N''US0378331005'', N''Apple Inc.'', N''AAPL'', N''http://www.apple.com''),
    (2, N''Pink Sheets'', N''US1104193065'', N''British Airways Plc'', N''BAIRY'', NULL),
    (3, N''Euronext Amsterdam'', N''NL0000009165'', N''Heineken NV'', N''HEIA'', NULL),
    (4, N''Tokyo Stock Exchange'', N''JP3866800000'', N''Panasonic Corp'', N''6752'', N''http://www.panasonic.co.jp''),
    (5, N''Deutsche Börse'', N''DE000PAH0038'', N''Porsche Automobil'', N''PAH3'', N''https://www.porsche.com/'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Exchange', N'Isin', N'Name', N'Ticker', N'Website') AND [object_id] = OBJECT_ID(N'[Company]'))
        SET IDENTITY_INSERT [Company] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250523124659_Initial'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250523124659_Initial', N'8.0.16');
END;
GO

COMMIT;
GO

