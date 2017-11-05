CREATE TABLE [dbo].[Ship] (
    [Id]           INT        IDENTITY (1, 1) NOT NULL,
    [GameId]       INT        NOT NULL,
    [PlayerId]     INT        NOT NULL,
    [PositionX]    INT        NOT NULL,
    [PositionY]    INT        NOT NULL,
    [Length]       INT        NOT NULL,
    [IsHorizontal] BIT        NOT NULL,
    [Stamp]        ROWVERSION NOT NULL,
    [CreatedOn]    DATETIME   CONSTRAINT [DF_Ship_CreatedOn] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Ship] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Ship_Game] FOREIGN KEY ([GameId]) REFERENCES [dbo].[Game] ([Id]),
    CONSTRAINT [FK_Ship_Player] FOREIGN KEY ([PlayerId]) REFERENCES [dbo].[Player] ([Id])
);

