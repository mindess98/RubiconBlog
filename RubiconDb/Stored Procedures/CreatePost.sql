CREATE PROCEDURE [dbo].[CreatePost]
	@slug varchar(50),
	@title nvarchar(100),
	@description nvarchar(250),
	@body text
AS

	INSERT INTO Posts (Slug, Title, [Description], Body, CreatedAt)
	VALUES (@slug, @title, @description, @body, GETDATE())

RETURN @@ROWCOUNT
