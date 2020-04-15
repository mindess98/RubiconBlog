using RubiconBloggingApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RubiconBloggingApi.Services
{
    public class TagService : ITagService
    {
        private ITagRepository tagRepository;
        public TagService(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }
        public List<string> GetTags()
        {
            return tagRepository.GetTags().Select(x => x.Name).ToList();
        }
    }
}
