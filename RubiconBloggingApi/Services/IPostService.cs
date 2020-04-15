using RubiconBloggingApi.Models;
using System.Collections.Generic;

namespace RubiconBloggingApi.Services
{
    public interface IPostService
    {
        Post GetPost(string slug);
        List<Post> GetPosts();
        bool DeletePost(string slug);
        string UpdatePost(string slug, Post post);
        string CreatePost(Post post);
    }
}
