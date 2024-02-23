using BlazorWebProject.Model;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Azure.Cosmos;

namespace BlazorWebProject.Service
{
    public class EmployeeService
    {
        private CosmosService cosmosService;

        public EmployeeService(CosmosService cosmosService)
        {
            this.cosmosService = cosmosService;
        }

        //직원 목록을 불러오는 Method
        public async Task<List<EmployeeModel>> GetEmployeeList()
        {
            var container = cosmosService.GetContainer("Employee");
            var query = "SELECT * FROM c";
            var iterator = container.GetItemQueryIterator<EmployeeModel>(new QueryDefinition(query));
            var result = new List<EmployeeModel>();

            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                result.AddRange(response.ToList());
            }
            return result;
        }

        //부서 목록을 불러오는 Method
        public async Task<List<DepartmentModel>> GetDepartmentList()
        {
            var container = cosmosService.GetContainer("Department");
            var query = "SELECT * FROM C";
            var iterator = container.GetItemQueryIterator<DepartmentModel>(new QueryDefinition(query));
            var result = new List<DepartmentModel>();

            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                result.AddRange(response.ToList());
            }
            return result;
        }

        //직원 목록 부서id를 참조하여 부서명을 불러오는 Method
        public async Task<List<EmployeeModel>> EmployeeDepartmentMapper() 
        {
            var employees = await GetEmployeeList();
            var departments = await GetDepartmentList();

            var mappedEmployees = employees.Select(e =>
            {
                var matchingDepartment = departments.FirstOrDefault(d => d.DepartmentId == e.DepartmentId);
                if (matchingDepartment != null)
                {
                    e.DepartmentName = matchingDepartment.name;
                }
                return e;
            });


            return mappedEmployees.OrderBy(employee => employee.Id).ToList();

        }
    }
}
