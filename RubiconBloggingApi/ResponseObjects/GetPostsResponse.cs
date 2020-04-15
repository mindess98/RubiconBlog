using RubiconBloggingApi.Models;
using System.Collections.Generic;

namespace RubiconBloggingApi.ResponseObjects
{
    public class GetPostsResponse
    {
        public List<Post> BlogPosts { get; set; }
        public int PostsCount { get; set; }
    }
}
