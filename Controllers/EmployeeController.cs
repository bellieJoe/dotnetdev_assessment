using Microsoft.AspNetCore.Mvc;

namespace dotnetdev_assessment.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
