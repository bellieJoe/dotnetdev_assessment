using Microsoft.AspNetCore.Mvc;

namespace dotnetdev_assessment.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View("~/Pages/Index.cshtml");
        }
    }
}
