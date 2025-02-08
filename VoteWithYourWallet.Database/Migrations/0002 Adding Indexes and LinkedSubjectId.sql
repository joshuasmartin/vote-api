ALTER TABLE [dbo].[Subjects]
ADD [LinkedSubjectId] INT NULL;
GO

CREATE INDEX [IX_Subjects_LinkedSubjectId]
    ON [dbo].[Subjects] ([LinkedSubjectId]);
GO

CREATE INDEX [IX_Subjects_ShortName]
    ON [dbo].[Subjects] ([ShortName]);
GO

CREATE INDEX [IX_Subjects_Type]
    ON [dbo].[Subjects] ([Type])
    INCLUDE ([Name]);
GO

CREATE INDEX [IX_Scores_SubjectId]
    ON [dbo].[Scores] ([SubjectId])
    INCLUDE ([Topic], [IsApproved], [CreatedByUserId]);
GO
