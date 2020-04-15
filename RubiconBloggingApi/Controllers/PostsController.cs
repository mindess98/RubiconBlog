using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RubiconBloggingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RubiconBloggingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        static List<Tag> Tags = new List<Tag> { new Tag { Id = 1, Name = "Lifestyle" },
                                                new Tag { Id = 2, Name = "Tech" },
                                                new Tag { Id = 3, Name = "Automotive" },
                                                new Tag { Id = 4, Name = "Beauty" },
                                                new Tag { Id = 5, Name = "Sports" },
                                                new Tag { Id = 6, Name = "Travel" } };

        static List<Post> Posts = new List<Post>
        {
            new Post
            {
                Slug = "lorem-ipsum-dolor-sit-amet",
                Title = "Lorem ipsum dolor sit amet",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                Tags = Tags.Where(x => x.Id % 2 == 0).Select(x => x.Name).ToList(),
                CreatedAt = DateTime.Now,
                UpdatedAt = null
            },
            new Post
            {
                Slug = "lorem-ipsum-dolor-sit-amet-1",
                Title = "Lorem ipsum dolor sit amet",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                Tags = Tags.Where(x => x.Id % 2 == 1).Select(x => x.Name).ToList(),
                CreatedAt = DateTime.Now,
                UpdatedAt = null
            },
            new Post
            {
                Slug = "lorem-ipsum-dolor-sit-amet-2",
                Title = "Lorem ipsum dolor sit amet",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                Tags = Tags.Where(x => x.Id > 2).Select(x => x.Name).ToList(),
                CreatedAt = DateTime.Now,
                UpdatedAt = null
            }
        };

        public PostsController()
        {
        }

        [HttpGet]
        public IEnumerable<Post> Get()
        {
            return Posts;
        }

        [HttpDelete]
        public bool Delete(string slug)
        {
            var postToRemove = Posts.Where(x => x.Slug == slug).SingleOrDefault();
            if (postToRemove != null) 
            {
                Posts.Remove(postToRemove);
                return true;
            }
            return false;
        }

        [HttpPost]
        public string Create([FromBody]Post post)
        {
            var slug = MakeSlugOutOfTitle(post.Title);
            post.Slug = slug;
            Posts.Add(post);

            return slug;
        }

        [HttpPut]
        public string Update(string slug, [FromBody]Post post)
        {
            var existingPost = Posts.Where(x => x.Slug == slug).SingleOrDefault();

            if(existingPost != null)
            {
                var title = post.Title ?? existingPost.Title;
                var updatedPost = new Post
                {
                    Slug = MakeSlugOutOfTitle(title),
                    Title = title,
                    Description = post.Description ?? existingPost.Description,
                    Body = post.Body ?? existingPost.Body,
                    CreatedAt = existingPost.CreatedAt,
                    UpdatedAt = DateTime.Now,
                    Tags = post.Tags
                };

                var i = Posts.IndexOf(existingPost);
                Posts[i] = updatedPost;

                return updatedPost.Slug;
            }

            return "";
        }

        private string MakeSlugOutOfTitle(string title)
        {
            var titleWords = title.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return titleWords.Aggregate("", (x, y) => x + "-" + y);
        }
    }
}
