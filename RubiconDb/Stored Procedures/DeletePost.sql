CREATE PROCEDURE [dbo].[DeletePost]
	@slug varchar(50)
AS

	DELETE FROM Posts
	WHERE Slug = @slug

RETURN @@ROWCOUNT
