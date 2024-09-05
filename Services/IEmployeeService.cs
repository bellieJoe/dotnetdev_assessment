using dotnetdev_assessment.Models.Entities;
using dotnetdev_assessment.Models.FormModels;

namespace dotnetdev_assessment.Services
{
    public interface IEmployeeService
    {
        public bool Insert(AddEmployee employee);

        public IEnumerable<Employee> GetAll();

        public Employee? GetById(int id);

        public bool UpdateById(EditEmployee employee);

        public bool DeleteById(Employee employee);
    }
}
