﻿CREATE TABLE [dbo].[Tags]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL
)

GO

CREATE INDEX [IX_Tags_Id] ON [dbo].[Tags] ([Id])
