using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RubiconBloggingApi.Models;
using RubiconBloggingApi.Services;

namespace RubiconBloggingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private ITagService tagService;
        public TagsController(ITagService tagService)
        {
            this.tagService = tagService;
        }

        [HttpGet]
        public List<string> Get()
        {
            return tagService.GetTags();
        }
    }
}