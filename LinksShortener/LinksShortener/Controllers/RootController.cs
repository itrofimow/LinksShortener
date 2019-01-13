using System.Threading.Tasks;
using LinksShortener.Core;
using Microsoft.AspNetCore.Mvc;

namespace LinksShortener.Controllers
{
    public class RootController : ControllerBase
    {
        private readonly ILinksService _linksService;

        public RootController(ILinksService linksService)
        {
            _linksService = linksService;
        }

        [Route("{uri}")]
        public async Task<IActionResult> FollowLink(string uri)
        {
            var redirectUrl = await _linksService.Hit(uri);

            return Redirect(redirectUrl);
        }
    }
}