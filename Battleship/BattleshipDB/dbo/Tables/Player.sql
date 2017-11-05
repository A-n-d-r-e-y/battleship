CREATE TABLE [dbo].[Player] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (50)  NOT NULL,
    [Email]     NVARCHAR (100) NOT NULL,
    [Stamp]     ROWVERSION     NOT NULL,
    [CreatedOn] DATETIME       CONSTRAINT [DF_Player_CreatedOn] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Player] PRIMARY KEY CLUSTERED ([Id] ASC)
);

