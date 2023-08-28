using Microsoft.AspNetCore.Mvc;

namespace NZWalks.Web.Controllers
{
    public class RegionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
