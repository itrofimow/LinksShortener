using Microsoft.AspNetCore.Mvc;

namespace LinksShortener.Controllers
{
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            HttpContext.Request.Headers.Remove("If-Modified-Since");

            return File("~/index.html", "text/html");
        }
    }
}