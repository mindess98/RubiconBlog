using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RubiconBloggingApi.Models;
using System.Collections.Generic;

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

        private readonly ILogger<PostsController> _logger;

        public PostsController(ILogger<PostsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Post> Get()
        {
            
            return new 
            
        }
    }
}
