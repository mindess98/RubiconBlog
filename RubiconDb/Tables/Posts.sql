CREATE TABLE [dbo].[Posts]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Slug] VARCHAR(150) NOT NULL UNIQUE, 
    [Title] NVARCHAR(100) NOT NULL, 
    [Description] NVARCHAR(250) NOT NULL, 
    [Body] TEXT NOT NULL, 
    [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(), 
    [UpdatedAt] DATETIME NULL 
)

GO

CREATE INDEX [IX_Posts_Slug] ON [dbo].[Posts] ([Slug])
