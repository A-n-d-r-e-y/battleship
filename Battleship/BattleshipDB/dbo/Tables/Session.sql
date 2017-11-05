CREATE TABLE [dbo].[Session] (
    [Id]            INT        IDENTITY (1, 1) NOT NULL,
    [GameId]        INT        NOT NULL,
    [HostPlayerId]  INT        NOT NULL,
    [GuestPlayerId] INT        NULL,
    [Stamp]         ROWVERSION NOT NULL,
    [CreatedOn]     DATETIME   CONSTRAINT [DF_Session_CreatedOn] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Session_Game] FOREIGN KEY ([GameId]) REFERENCES [dbo].[Game] ([Id]),
    CONSTRAINT [FK_Session_Player] FOREIGN KEY ([HostPlayerId]) REFERENCES [dbo].[Player] ([Id]),
    CONSTRAINT [FK_Session_Player1] FOREIGN KEY ([GuestPlayerId]) REFERENCES [dbo].[Player] ([Id])
);

