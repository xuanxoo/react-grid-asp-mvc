CREATE TABLE [dbo].[FundValues] (
    [Id]     INT             IDENTITY (1, 1) NOT NULL,
    [FundId] INT             NOT NULL,
    [Date]   DATETIME        NOT NULL,
    [Value]  DECIMAL (18, 6) NOT NULL,
    CONSTRAINT [PK_FundValues] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FundValues_Fund] FOREIGN KEY ([FundId]) REFERENCES [dbo].[Fund] ([Id])
);

