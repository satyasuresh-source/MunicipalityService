IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Frequencies] (
    [FrequencyId] int NOT NULL IDENTITY,
    [Value] nvarchar(max) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Frequencies] PRIMARY KEY ([FrequencyId])
);

GO

CREATE TABLE [Municipalities] (
    [MunicipalityId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Municipalities] PRIMARY KEY ([MunicipalityId])
);

GO

CREATE TABLE [Taxes] (
    [TaxId] nvarchar(450) NOT NULL,
    [TaxValue] nvarchar(max) NOT NULL,
    [MunicipalityId] int NOT NULL,
    [FrequencyId] int NOT NULL,
    CONSTRAINT [PK_Taxes] PRIMARY KEY ([TaxId]),
    CONSTRAINT [FK_Taxes_Frequencies_FrequencyId] FOREIGN KEY ([FrequencyId]) REFERENCES [Frequencies] ([FrequencyId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Taxes_Municipalities_MunicipalityId] FOREIGN KEY ([MunicipalityId]) REFERENCES [Municipalities] ([MunicipalityId]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Taxes_FrequencyId] ON [Taxes] ([FrequencyId]);

GO

CREATE INDEX [IX_Taxes_MunicipalityId] ON [Taxes] ([MunicipalityId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201025223932_InitialCreate', N'3.1.9');

GO

