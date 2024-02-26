using EmployeeRegistration.Application.Model;
using EmployeeRegistration.Core.Entitites.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRegistration.Application.Interface
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<EmployeeDTO>> GetEmployees(string name,int pageNumber,int pageSize);

        public Task CreateEmployee(Employee employee);
    }
}
