using RubiconBloggingApi.Models;
using RubiconBloggingApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var x = postRepository.GetPost(slug);

            return new Post
            {
                Slug = x.Slug,
                Title = x.Title,
                Description = x.Description,
                Body = x.Body,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                TagList = x.TagList
            };
        }

        public List<Post> GetPosts(string tag)
        {
            return postRepository.GetPosts(tag).Select(x => new Post
            {
                Slug = x.Slug,
                Title = x.Title,
                Description = x.Description,
                Body = x.Body,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                TagList = x.TagList
            }).ToList();
        }

        public string UpdatePost(string slug, Post post)
        {
            if (post.Slug.Length > 150 || post.Title.Length > 100 || post.Description.Length > 250 || post.Body.Length > 2147483647)
                return "";

            post.Slug = MakeSlugOutOfTitle(post.Title);

            return postRepository.UpdatePost(slug, post);
        }

        public string CreatePost(Post post)
        {
            if (post.Slug.Length > 150 || post.Title.Length > 100 || post.Description.Length > 250 || post.Body.Length > 2147483647)
                return "";

            post.Slug = MakeSlugOutOfTitle(post.Title);

            return postRepository.CreatePost(post);
        }

        private string MakeSlugOutOfTitle(string title)
        {
            var titleWords = title.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var slug = titleWords[0];
            for(int i = 1; i < titleWords.Length; i++)
            {
                slug += ("-" + titleWords[i]);
            }

            return slug;
        }
    }
}
