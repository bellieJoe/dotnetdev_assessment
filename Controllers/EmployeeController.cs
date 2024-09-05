using dotnetdev_assessment.Models.FormModels;
using dotnetdev_assessment.Models.Entities;
using dotnetdev_assessment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace dotnetdev_assessment.Controllers
{
    [Route("employees")]
    public class EmployeeController(IEmployeeService employeeService) : Controller
    {
        private readonly IEmployeeService _employeeService = employeeService;

        [Route("")]
        [Authorize]
        public IActionResult Index()
        {
            return View(_employeeService.GetAll());
        }

        [HttpGet("add")]
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(AddEmployee employee)
        {
            if(!ModelState.IsValid)
                return UnprocessableEntity("Invalid Data");

            if (!_employeeService.Insert(employee))
                return BadRequest();

            return Created();
        }

        [HttpGet("edit/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id) 
        {
            Employee? employee = _employeeService.GetById(id);
            if(employee == null)
                return NotFound();
            return View(employee);
        }

        [HttpPut("edit/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(EditEmployee employee, int id) 
        {
            employee.Id = id;
            if(!ModelState.IsValid)
                return UnprocessableEntity();
            if(!_employeeService.UpdateById(employee))
                return BadRequest();
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id) 
        {
            Employee? employee = _employeeService.GetById(id);
            if (employee == null)
                return NotFound();
            if (!_employeeService.DeleteById(employee))
                return BadRequest();
            return Ok();
        }
    }
}
