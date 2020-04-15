CREATE PROCEDURE [dbo].[GetPost]
	@slug varchar(50)
AS
	SELECT * 
	FROM Posts p
	LEFT JOIN PostsTags pt ON p.Slug = pt.PostSlug
	LEFT JOIN Tags t ON pt.TagId = t.Id
	WHERE Slug = @slug

RETURN 0
