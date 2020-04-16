﻿CREATE TABLE [dbo].[PostsTags]
(
	[PostSlug] VARCHAR(50) NOT NULL , 
    [TagId] INT NOT NULL, 
    PRIMARY KEY ([TagId], [PostSlug]), 
    CONSTRAINT [FK_PostsTags_Posts] FOREIGN KEY ([PostSlug]) REFERENCES [Posts]([Slug]), 
    CONSTRAINT [FK_PostsTags_Tags] FOREIGN KEY ([TagId]) REFERENCES [Tags]([Id])
)

GO

CREATE INDEX [IX_PostsTags_PostSlug] ON [dbo].[PostsTags] ([PostSlug])

GO

CREATE INDEX [IX_PostsTags_TagId] ON [dbo].[PostsTags] ([TagId])
