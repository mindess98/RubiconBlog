using RubiconBloggingApi.Models;
using RubiconBloggingApi.Repositories;
using System.Collections.Generic;

namespace RubiconBloggingApi.Services
{
    public class PostService : IPostService
    {
        private IPostRepository postRepository;
        public PostService(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        public bool DeletePost(string slug)
        {
            return postRepository.DeletePost(slug);
        }

        public Post GetPost(string slug)
        {
            return postRepository.GetPost(slug);
        }

        public List<Post> GetPosts()
        {
            return postRepository.GetPosts();
        }

        public string UpdatePost(string slug, Post post)
        {
            return postRepository.UpdatePost(slug, post);
        }

        public string CreatePost(Post post)
        {
            return postRepository.CreatePost(post);
        }
    }
}
