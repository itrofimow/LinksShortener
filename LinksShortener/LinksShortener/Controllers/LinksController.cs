using System.Collections.Generic;
using System.Threading.Tasks;
using LinksShortener.Core;
using Microsoft.AspNetCore.Mvc;

namespace LinksShortener.Controllers
{
    [Route("/api/links")]
    public class LinksController : ControllerBase
    {
        private readonly ILinksService _linksService;

        public LinksController(ILinksService linksService)
        {
            _linksService = linksService;
        }

        [HttpPost]
        public Task<Link> CreateLink([FromBody] Link link)
        {
            return _linksService.Create(link);
        }

        [HttpGet]
        [Route("all")]
        public Task<List<Link>> GetAll()
        {
            return _linksService.GetAll();
        }

        [HttpGet]
        [Route("owner/{ownerId}")]
        public Task<List<Link>> GetAllByOwner(string ownerId)
        {
            return _linksService.GetAllByOwner(ownerId);
        }
    }
}