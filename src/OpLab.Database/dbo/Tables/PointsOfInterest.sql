CREATE TABLE [dbo].[PointsOfInterest] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)  NULL,
    [Description] NVARCHAR (150) NULL,
    [CityId]      INT            NOT NULL,
    CONSTRAINT [PK_PointsOfInterest] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PointsOfInterest_Cities] FOREIGN KEY ([CityId]) REFERENCES [dbo].[Cities] ([Id])
);

