CREATE TABLE [dbo].[Product] (
    [ProductId]     INT             IDENTITY (1, 1) NOT NULL,
    [ProductName]   NVARCHAR (50)   NULL,
    [Description]   NVARCHAR (MAX)  NULL,
    [Price]         DECIMAL (18, 2) NULL,
    [NumberOfItems] INT             NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([ProductId] ASC)
);

