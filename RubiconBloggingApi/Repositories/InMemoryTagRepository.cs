using RubiconBloggingApi.Models;
using System;
using System.Collections.Generic;

namespace RubiconBloggingApi.Repositories
{
    public class InMemoryTagRepository : ITagRepository
    {
        static List<Tag> Tags = new List<Tag> { new Tag { Id = 1, Name = "Lifestyle" },
                                                new Tag { Id = 2, Name = "Tech" },
                                                new Tag { Id = 3, Name = "Automotive" },
                                                new Tag { Id = 4, Name = "Beauty" },
                                                new Tag { Id = 5, Name = "Sports" },
                                                new Tag { Id = 6, Name = "Travel" } };

        public List<Tag> GetTags()
        {
            return Tags;
        }
    }
}
