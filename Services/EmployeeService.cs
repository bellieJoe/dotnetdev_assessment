using dotnetdev_assessment.Models.Entities;
using dotnetdev_assessment.Models.FormModels;

namespace dotnetdev_assessment.Services
{
    public class EmployeeService(ApplicationDbContext context) : IEmployeeService
    {
        private readonly ApplicationDbContext _context = context;

        public bool DeleteById(Employee employee)
        {
            _context.Employees.Remove(employee);
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<Employee> GetAll()
        {
            return [.. _context.Employees];
        }

        public Employee? GetById(int id)
        {
            return _context.Employees.FirstOrDefault(e => e.Id == id);
        }

        public bool Insert(AddEmployee employee)
        {
            _context.Employees.Add(new Employee
            {
                Username = employee.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(employee.Password),
                Name = employee.Name,
                Department = employee.Department,
                IsAdmin = employee.IsAdmin,
                Position = employee.Position,
            });
            return _context.SaveChanges() > 0;
        }

        public bool UpdateById(EditEmployee employee)
        {
            Employee? toUpdate = _context.Employees.Find(employee.Id);
            toUpdate!.Name = employee.Name;
            toUpdate!.Department = employee.Department;
            toUpdate!.Username = employee.Username;
            toUpdate!.IsAdmin = employee.IsAdmin;
            toUpdate!.Position = employee.Position;
            _context.Employees.Update(toUpdate);
            return _context.SaveChanges() > 0;
        }
    }
}
