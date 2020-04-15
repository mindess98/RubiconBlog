using RubiconBloggingApi.Models;
using System.Collections.Generic;

namespace RubiconBloggingApi.Repositories
{
    public interface IPostRepository
    {
        Post GetPost(string slug);
        List<Post> GetPosts();
    }
}
