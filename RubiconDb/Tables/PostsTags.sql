CREATE TABLE [dbo].[PostsTags]
(
	[PostId] INT NOT NULL , 
    [TagId] INT NOT NULL, 
    PRIMARY KEY ([TagId], [PostId]), 
    CONSTRAINT [FK_PostsTags_Posts] FOREIGN KEY ([PostId]) REFERENCES [Posts]([Id]), 
    CONSTRAINT [FK_PostsTags_Tags] FOREIGN KEY ([TagId]) REFERENCES [Tags]([Id])
)

GO

CREATE INDEX [IX_PostsTags_PostSlug] ON [dbo].[PostsTags] ([PostId])

GO

CREATE INDEX [IX_PostsTags_TagId] ON [dbo].[PostsTags] ([TagId])
