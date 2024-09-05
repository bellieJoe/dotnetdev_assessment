
using dotnetdev_assessment.Models.ViewModels;
using dotnetdev_assessment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnetdev_assessment.Controllers
{
    [Route("auth")]
    [Authorize]
    public class AuthController(IAuthService auth) : Controller
    {
        private readonly IAuthService _authService = auth;

        [HttpGet("login")]
        public IActionResult Login() { return View(); }

        [HttpPost("login")]
        public IActionResult Login(AuthLoginViewModel loginForm) 
        {
            //if (!ModelState.IsValid)
            //{
            //    TempData["ErrorMessage"] = "The Inputs are invalid.";
            //    return Unauthorized();
            //}
            //string? token = _authService.Authenticate(loginForm);
            //if (String.IsNullOrEmpty(token))
            //{
            //    TempData["ErrorMessage"] = "Unauthorized Credentials";
            //    return Unauthorized();
            //}
            //return Ok(token); 
            return Ok();
        }

        [Route("signup")]
        public IActionResult SignUp() { return View(); }

        
    }
}
