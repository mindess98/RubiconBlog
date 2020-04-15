CREATE TABLE [dbo].[Posts]
(
	[Slug] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [Title] NVARCHAR(100) NOT NULL, 
    [Description] NVARCHAR(250) NOT NULL, 
    [Body] TEXT NOT NULL, 
    [CreatedAt] DATETIME NOT NULL, 
    [UpdatedAt] DATETIME NULL
)
