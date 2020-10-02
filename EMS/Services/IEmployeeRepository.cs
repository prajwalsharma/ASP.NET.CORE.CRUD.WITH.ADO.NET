using EMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Services
{
    interface IEmployeeRepository
    {
        public Result SaveEmployee(Employee employee);
        public List<Employee> GetEmployees();
        public Employee GetEmployee(int empid);
        public Result DeleteEmployee(int empid);
        public Result UpdateEmployee(Employee employee);
    }
}
