using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RubiconBloggingApi.Models;
using RubiconBloggingApi.ResponseObjects;
using RubiconBloggingApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RubiconBloggingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private IPostService postService;
        public PostsController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpGet]
        public GetPostsResponse Get()
        {
            var posts = postService.GetPosts();
            return new GetPostsResponse
            {
                BlogPosts = posts,
                PostsCount = posts.Count()
            };
        }

        [HttpGet]
        [Route("/{**slug}")]
        public Post GetPost([FromRoute]string slug)
        {
            return postService.GetPost(slug);
        }

        [HttpDelete]
        public bool Delete(string slug)
        {
            return postService.DeletePost(slug);
        }

        [HttpPost]
        public string Create([FromBody]Post post)
        {
            return postService.CreatePost(post);
        }

        [HttpPut]
        public string Update(string slug, [FromBody]Post post)
        {
            return postService.UpdatePost(slug, post);
        }
    }
}
