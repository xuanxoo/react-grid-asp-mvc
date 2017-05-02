CREATE TABLE [dbo].[Fund] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (35) NOT NULL,
    [Description] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Found] PRIMARY KEY CLUSTERED ([Id] ASC)
);

