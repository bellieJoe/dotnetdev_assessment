using Microsoft.AspNetCore.Mvc;

namespace dotnetdev_assessment.Controllers
{
    [Route("errors")]
    public class ErrorController : Controller
    {
        [Route("unauthorized")]
        public IActionResult UnauthorizedView()
        {
            Response.StatusCode = 401;
            return View();
        }

        [Route("forbidden")]
        public IActionResult ForbiddenView()
        {
            Response.StatusCode = 403;
            return View();
        }
    }
}
