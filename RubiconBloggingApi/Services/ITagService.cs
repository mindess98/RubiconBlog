using System.Collections.Generic;

namespace RubiconBloggingApi.Services
{
    public interface ITagService
    {
        List<string> GetTags();
    }
}