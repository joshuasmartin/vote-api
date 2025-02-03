CREATE TABLE [dbo].[Users] (
    [Id]               INT              IDENTITY (1, 1) NOT NULL,
    [CreatedAt]        DATETIME         NOT NULL,
    [EmailAddress]     VARCHAR (128)    NOT NULL,
    [Name]             NVARCHAR (128)   NOT NULL,
    [PasswordSalt]     VARCHAR (128)    NULL,
    [PasswordHash]     VARCHAR (128)    NULL,
    [IsDeleted]        BIT              NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id])
);
GO

CREATE TABLE [dbo].[Subjects] (
    [Id]                    INT                 IDENTITY (1, 1) NOT NULL,
    [CreatedAt]             DATETIME            NOT NULL,
    [Name]                  VARCHAR (128)       NOT NULL,
    [ShortName]             VARCHAR (128)       NOT NULL,
    [Type]                  VARCHAR (32)        NOT NULL,
    [DiversityScore]        TINYINT             NOT NULL,
    [UnionScore]            TINYINT             NOT NULL,
    [EnvironmentalScore]    TINYINT             NOT NULL,
    [LobbyingScore]         TINYINT             NOT NULL,
    CONSTRAINT [PK_Subjects] PRIMARY KEY CLUSTERED ([Id])
);
GO

CREATE TABLE [dbo].[Scores] (
    [Id]                INT                 IDENTITY (1, 1) NOT NULL,
    [CreatedAt]         DATETIME            NOT NULL,
    [SourceUrl]         VARCHAR (MAX)       NOT NULL,
    [Headline]          VARCHAR (MAX)       NOT NULL,
    [Number]            TINYINT             NOT NULL,
    [Topic]             VARCHAR (32)        NOT NULL,
    [IsApproved]        BIT                 NOT NULL,
    [CreatedByUserId]   INT                 NOT NULL,
    [SubjectId]         INT                 NOT NULL,
    CONSTRAINT [PK_Scores] PRIMARY KEY CLUSTERED ([Id])
);
GO
