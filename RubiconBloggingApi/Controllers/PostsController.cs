using Microsoft.AspNetCore.Mvc;
using RubiconBloggingApi.Models;
using RubiconBloggingApi.ResponseObjects;
using RubiconBloggingApi.Services;
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
        public GetPostsResponse Get([FromQuery] string tag)
        {
            var posts = postService.GetPosts(tag);
            return new GetPostsResponse
            {
                BlogPosts = posts,
                PostsCount = posts.Count()
            };
        }

        [HttpGet]
        [Route("{slug}")]
        public Post GetPost(string slug)
        {
            return postService.GetPost(slug);
        }

        [HttpDelete]
        [Route("{slug}")]
        public bool Delete(string slug)
        {
            return postService.DeletePost(slug);
        }

        [HttpPost]
        public Post Create([FromBody]Post post)
        {
            var slug = postService.CreatePost(post);

            return postService.GetPost(slug);
        }

        [HttpPut]
        [Route("{slug}")]
        public Post Update(string slug, [FromBody]Post post)
        {
            var newSlug = postService.UpdatePost(slug, post);

            return postService.GetPost(newSlug);
        }
    }
}
