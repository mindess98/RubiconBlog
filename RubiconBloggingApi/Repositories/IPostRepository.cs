using RubiconBloggingApi.Models;
using System.Collections.Generic;

namespace RubiconBloggingApi.Repositories
{
    public interface IPostRepository
    {
        FullPost GetPost(string slug);
        List<FullPost> GetPosts(string tag);
        bool DeletePost(string slug);
        string UpdatePost(string slug, Post post);
        string CreatePost(Post post);
    }
}
