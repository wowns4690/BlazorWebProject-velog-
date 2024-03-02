







using BlazorWebProject.Model;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace BlazorWebProject.Service
{
    public class EmployeeService
    {
        private CosmosService cosmosService;
        private JwtService jwtService;

        public EmployeeService(CosmosService cosmosService, JwtService jwtService)
        {
            this.cosmosService = cosmosService;
            this.jwtService = jwtService;
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
            return mappedEmployees.OrderBy(employee => employee.EmployeeId).ToList();
        }

        //직원 id값중 가장 큰 값을 찾아오는 Method
        public async Task<int> EmployeeFindIdMax()
        {
            var container = cosmosService.GetContainer("Employee");
            var query = "SELECT TOP 1 * FROM c ORDER BY c.EmployeeId DESC";
            var iterator = container.GetItemQueryIterator<EmployeeModel>(query);
            var emp = new EmployeeModel();
            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                var employees = response.ToList();
                if (employees.Any())
                {
                    emp = employees.First();
                }
                else
                {
                    emp.EmployeeId = "0";
                }
            }

            return int.Parse(emp.EmployeeId);
        }

        //직원을 추가하는 Method
        public async Task EmployeeAdd(EmployeeModel emp)
        {
            int maxId = await EmployeeFindIdMax();
            maxId++;
            emp.EmployeeId = maxId.ToString();
            var container = cosmosService.GetContainer("Employee");
            await container.CreateItemAsync(emp);
        }

        //직원을 삭제하는 Method
        public async Task EmployeeDelete(EmployeeModel emp)
        {
            var container = cosmosService.GetContainer("Employee");
            await container.DeleteItemAsync<EmployeeModel>(emp.id, new PartitionKey(emp.pk));
        }

        //특정 직원을 조회하는 Method
        public async Task<EmployeeModel> GetEmployeeById(EmployeeModel emp)
        {
            var container = cosmosService.GetContainer("Employee");
            var response = await container.ReadItemAsync<EmployeeModel>(emp.id, new PartitionKey(emp.pk));
            return response.Resource;
        }

        //직원을 수정하는 Method
        public async Task EmployeeUpdate(EmployeeModel emp)
        {
            var container = cosmosService.GetContainer("Employee");
            await container.UpsertItemAsync<EmployeeModel>(emp);
        }

        //직원 로그인시 아이디와 패스워드 확인 Method
        public async Task<string> Login(EmployeeModel emp)
        {
            var container = cosmosService.GetContainer("Employee");
            var query = $"SELECT * FROM C WHERE C.RegisterId = '{emp.RegisterId}'";
            QueryDefinition queryDefinition = new QueryDefinition(query);
            FeedIterator<EmployeeModel> resultSetIterator = container.GetItemQueryIterator<EmployeeModel>(queryDefinition);
            var response = await resultSetIterator.ReadNextAsync();
            var DbEmployee = response.Resource.FirstOrDefault<EmployeeModel>();
            if (DbEmployee != null)
            {
                if (DbEmployee.Password == emp.Password)
                {
                    var token = await jwtService.GenerateJwtTokenAsync(emp.RegisterId);
                    return token;
                }
            }
            return "";
        }

    }
}
