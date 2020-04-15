using RubiconBloggingApi.Models;
using System.Collections.Generic;

namespace RubiconBloggingApi.Repositories
{
    public interface IPostRepository
    {
        Post GetPost(string slug);
        List<Post> GetPosts();
        bool DeletePost(string slug);
        string UpdatePost(string slug, Post post);
        string CreatePost(Post post);
    }
}
