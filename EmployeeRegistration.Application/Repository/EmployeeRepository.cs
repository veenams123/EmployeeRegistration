using Dapper;
using EmployeeRegistration.Application.Interface;
using EmployeeRegistration.Application.Model;
using EmployeeRegistration.Core.Entitites.Employee;
using EmployeeRegistration.Infrastructure.DataContext;
using System.Data;

namespace EmployeeRegistration.Application.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DapperContext _context;

        public EmployeeRepository(DapperContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Method for creating the employee
        /// </summary>
        /// <returns></returns>
        public async Task CreateEmployee(Employee employee)
        {

            var procedureName = "InsertEmployeeData";
            var parameters = new DynamicParameters();
            parameters.Add("@FirstName", employee.FirstName, DbType.String);
            parameters.Add("@LastName", employee.LastName, DbType.String);
            parameters.Add("@Email", employee.Email, DbType.String);
            parameters.Add("@DOB", employee.DOB, DbType.DateTime);
            parameters.Add("@DepartmentId", employee.DepartmentId, DbType.Int32);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteScalarAsync<Employee>
                     (procedureName, parameters, commandType: CommandType.StoredProcedure);

            }
        }
        /// <summary>
        /// Method for getting all the Employee
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public async Task<IEnumerable<EmployeeDTO>> GetEmployees(string? Name, int pageNumber, int pageSize)
        {
            var procedureName = "GetEmployees";
            var parameters = new DynamicParameters();
            parameters.Add("@Name", Name, DbType.String, ParameterDirection.Input);
            parameters.Add("@PageNumber", pageNumber, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PageSize", pageSize, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var company = await connection.QueryAsync<EmployeeDTO>
                    (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return company.ToList();
            }
        }
    }
}
