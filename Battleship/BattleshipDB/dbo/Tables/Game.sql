CREATE TABLE [dbo].[Game] (
    [Id]        INT              IDENTITY (1, 1) NOT NULL,
    [Guid]      UNIQUEIDENTIFIER CONSTRAINT [DF_Game_GameGuid] DEFAULT (newid()) NOT NULL,
    [Name]      NVARCHAR (50)    NOT NULL,
    [SessionId] INT              NOT NULL,
    [Stamp]     ROWVERSION       NOT NULL,
    [CreatedOn] DATETIME         CONSTRAINT [DF_Game_DateTimeCreated] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Game] PRIMARY KEY CLUSTERED ([Id] ASC)
);

