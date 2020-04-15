CREATE PROCEDURE [dbo].[GetPosts]
AS

	SELECT p.*, t.*
	FROM Posts p
	LEFT JOIN PostsTags pt ON p.Slug = pt.PostSlug
	LEFT JOIN Tags t ON pt.TagId = t.Id

RETURN 0
