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
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

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


        private readonly ILogger<PostsController> _logger;

        public PostsController(ILogger<PostsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Post> Get()
        {
            return Posts;
        }
    }
}
