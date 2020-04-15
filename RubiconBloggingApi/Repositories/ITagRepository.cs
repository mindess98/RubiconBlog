using RubiconBloggingApi.Models;
using System.Collections.Generic;

namespace RubiconBloggingApi.Repositories
{
    public interface ITagRepository
    {
        List<Tag> GetTags();
    }
}
