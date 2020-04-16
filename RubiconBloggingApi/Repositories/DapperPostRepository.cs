using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using RubiconBloggingApi.Models;

namespace RubiconBloggingApi.Repositories
{
    public class DapperPostRepository : IPostRepository
    {
        private IConfiguration configuration;
        public DapperPostRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string CreatePost(Post post)
        {
            using (IDbConnection connection = new SqlConnection(GetDefaultConnectionString()))
            {
                string insertPost = @"INSERT INTO Posts (Slug, Title, [Description], Body) 
                                      VALUES (@Slug, @Title, @Description, @Body);
                                      SELECT SCOPE_IDENTITY();";
                var parameters = new { Slug = post.Slug, Title = post.Title, Description = post.Description, Body = post.Body };

                var id = connection.Query<int>(insertPost, parameters).SingleOrDefault();

                string insertTag = @"INSERT INTO Tags (Name) VALUES (@Name); SELECT SCOPE_IDENTITY();";
                var ids = new List<int>();
                foreach (var tag in post?.TagList)
                {
                    var paras = new { Name = tag };

                    ids.Add(connection.Query<int>(insertTag, paras).SingleOrDefault());
                }

                string insertPostTag = @"INSERT INTO PostsTags (PostId, TagId) VALUES (@PostId, @TagId); SELECT @@ROWCOUNT;";

                int insertedPostTags = 0;

                foreach(var tagId in ids)
                {
                    if (tagId < 1)
                        continue;

                    var pars = new { PostId = id, TagId = tagId };

                    insertedPostTags += connection.Query<int>(insertPostTag, pars).SingleOrDefault();
                }

                 return post.Slug;
            }
        }

        public bool DeletePost(string slug)
        {
            using (IDbConnection connection = new SqlConnection(GetDefaultConnectionString()))
            {
                string sql = @"DELETE FROM Posts WHERE Slug = @Slug; SELECT @@ROWCOUNT;";
                var parameters = new { Slug = slug };

                var removedRows = connection.Query<int>(sql, parameters);

                return removedRows.SingleOrDefault() == 1;
            }
        }

        public FullPost GetPost(string slug)
        {
            using (IDbConnection connection = new SqlConnection(GetDefaultConnectionString()))
            {
                string sql = @"SELECT * FROM Posts p
                               LEFT JOIN PostsTags pt ON pt.PostId = p.Id
                               LEFT JOIN Tags t ON pt.TagId = t.Id
                               WHERE p.Slug = @Slug";
                var parameters = new { Slug = slug };

                var tagsDict = new Dictionary<int, FullPost>();

                var post = connection.Query<FullPost, Tag, FullPost>(sql,
                    (post, tag) =>
                    {
                        FullPost postEntry;

                        if (!tagsDict.TryGetValue(post.Id, out postEntry))
                        {
                            postEntry = post;
                            postEntry.TagList = new List<string>();
                            tagsDict.Add(postEntry.Id, postEntry);
                        }

                        if (!(tag is null))
                        {
                            postEntry.TagList.Add(tag.Name);
                        }

                        return postEntry;
                    }, parameters);

                return tagsDict.SingleOrDefault().Value;
            }
        }

        public List<FullPost> GetPosts(string tag)
        {
            using (IDbConnection connection = new SqlConnection(GetDefaultConnectionString()))
            {
                string sql;
                
                if(!string.IsNullOrEmpty(tag))
                {
                    sql = @"SELECT pos.*, tg.*
	                           FROM (SELECT p.Id
	                                 FROM Posts p
	                                 LEFT JOIN PostsTags pt ON p.Id = pt.PostId
	                                 LEFT JOIN Tags t ON pt.TagId = t.Id
                                     WHERE t.Name = @Tag) ps
                               JOIN Posts pos ON pos.Id = ps.Id
	                           LEFT JOIN PostsTags pot ON pos.Id = pot.PostId
	                           LEFT JOIN Tags tg ON pot.TagId = tg.Id
                               ORDER BY pos.CreatedAt DESC;";
                } 
                else
                {
                    sql = @"SELECT pos.*, tg.*
	                        FROM Posts pos
	                        LEFT JOIN PostsTags pt ON pos.Id = pt.PostId
	                        LEFT JOIN Tags tg ON pt.TagId = tg.Id
                            ORDER BY pos.CreatedAt DESC;";
                }
                
                
                var parameters = new { Tag = tag };

                var tagsDict = new Dictionary<int, FullPost>();

                var posts = connection.Query<FullPost, Tag, FullPost>(sql,
                    (post, tag) => 
                    {
                        FullPost postEntry;

                        if(!tagsDict.TryGetValue(post.Id, out postEntry))
                        {
                            postEntry = post;
                            postEntry.TagList = new List<string>();
                            tagsDict.Add(postEntry.Id, postEntry);
                        }

                        if(!(tag is null))
                        {
                            postEntry.TagList.Add(tag.Name);
                        }

                        return postEntry;
                    }, parameters).Distinct().ToList();
                
                return posts;
            }
        }

        public string UpdatePost(string slug, Post post)
        {
            using (IDbConnection connection = new SqlConnection(GetDefaultConnectionString()))
            {
                string sql = @"UPDATE Posts
                               SET
                                    Slug = @Slug,
                                    Title = @Title,
                                    [Description] = @Description,
                                    Body = @Body,
                                    UpdatedAt = GETDATE()
                               WHERE
                                    Slug = @OldSlug;
                               SELECT @@ROWCOUNT;";
                var parameters = new { OldSlug = slug, Slug = post.Slug, Title = post.Title, Description = post.Description, Body = post.Body };

                var rows = connection.Query<int>(sql, parameters);

                if (rows.SingleOrDefault() == 1)
                    return post.Slug;
                return "";
            }
        }

        private string GetDefaultConnectionString()
        {
            return configuration.GetConnectionString("DefaultConnection");
        }
    }
}
