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
                string sql = @"INSERT INTO Posts (Slug, Title, [Description], Body) 
                               VALUES (@Slug, @Title, @Description, @Body);
                               SELECT @@ROWCOUNT;";
                var parameters = new { Slug = post.Slug, Title = post.Title, Description = post.Description, Body = post.Body };

                var rows = connection.Query<int>(sql, parameters);

                if(rows.SingleOrDefault() == 1)
                    return post.Slug;
                return "";
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

        public Post GetPost(string slug)
        {
            using (IDbConnection connection = new SqlConnection(GetDefaultConnectionString()))
            {
                string sql = @"SELECT * FROM Posts WHERE Slug = @Slug";
                var parameters = new { Slug = slug };

                var post = connection.Query<Post>(sql, parameters);

                return post.SingleOrDefault();
            }
        }

        public List<Post> GetPosts()
        {
            using (IDbConnection connection = new SqlConnection(GetDefaultConnectionString()))
            {
                string sql = @"SELECT p.*, t.*
	                           FROM Posts p
	                           LEFT JOIN PostsTags pt ON p.Slug = pt.PostSlug
	                           LEFT JOIN Tags t ON pt.TagId = t.Id";

                var posts = connection.Query<Post, Tag, Post>(sql, 
                    (post, tag) => 
                    {
                        if(!(tag is null))
                        {
                            if(post.TagList is null)
                                post.TagList = new List<string>();
                            post.TagList.Add(tag.Name);
                        }

                        return post;
                    }, splitOn: "Slug").Distinct().ToList();

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
                                    UpdatedAt = GETDATE();
                               WHERE
                                    Slug = @OldSlug
                               SELECT @@ROWCOUNT;";
                var parameters = new { OldSlug = slug, Slug = post.Slug, Title = post.Title, Description = post.Description, Body = post.Body };

                var rows = connection.Query<int>(sql);

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
