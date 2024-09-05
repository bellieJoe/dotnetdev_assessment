using dotnetdev_assessment.Models.FormModels;
using dotnetdev_assessment.Models.Entities;
using dotnetdev_assessment.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnetdev_assessment.Controllers
{
    [Route("employees")]
    public class EmployeeController(IEmployeeService employeeService) : Controller
    {
        private readonly IEmployeeService _employeeService = employeeService;
        [Route("")]
        public IActionResult Index()
        {
            return View(_employeeService.GetAll());
        }

        [HttpGet("add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost("add")]
        public IActionResult Add(AddEmployee employee)
        {
            if(!ModelState.IsValid)
                return UnprocessableEntity("Invalid Data");

            if (!_employeeService.Insert(employee))
                return BadRequest();

            return Created();
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id) 
        {
            Employee? employee = _employeeService.GetById(id);
            if(employee == null)
                return NotFound();
            return View(employee);
        }

        [HttpPost("edit/{id}")]
        public IActionResult Edit(EditEmployee employee, int id) 
        {
            employee.Id = id;
            if(!ModelState.IsValid)
                return UnprocessableEntity();
            if(!_employeeService.UpdateById(employee))
                return BadRequest();
            return RedirectToAction("Index");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id) 
        {
            Employee? employee = _employeeService.GetById(id);
            if (employee == null)
                return NotFound();
            if (!_employeeService.DeleteById(employee))
                return BadRequest();
            return RedirectToAction("Index");
        }
    }
}
