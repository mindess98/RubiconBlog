CREATE PROCEDURE [dbo].[UpdatePost]
	@slug varchar(50),
	@newslug varchar(50),
	@title nvarchar(100),
	@description nvarchar(250),
	@body text
AS
	UPDATE Posts
	SET
		Slug = @newslug,
		Title = @title,
		[Description] = @description,
		Body = @body,
		UpdatedAt = GETDATE()
	WHERE
		Slug = @slug

RETURN @@ROWCOUNT
