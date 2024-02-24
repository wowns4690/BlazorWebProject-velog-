using BlazorWebProject.Controller;
using BlazorWebProject.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;
using System.Xml.Linq;

namespace BlazorWebProject.Service
{
    public class ClientService
    {
        private HttpClient httpClient;

        public ClientService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        //직원 목록 Method
        public async Task<List<EmployeeModel>> GetEmployeeList()
        {
            string baseUrl = "api/Employee/employeeList";
            //var response = await httpClient.GetFromJsonAsync<List<EmployeeModel>>(baseUrl);
            var response = await httpClient.GetStringAsync(baseUrl);
            if (string.IsNullOrEmpty(response))
            {
                return new();
            }
            else
            {
                var option = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                List<EmployeeModel> employeeList = JsonSerializer.Deserialize<List<EmployeeModel>>(response, option) ?? new();
                return employeeList ?? new();
            }
        }

        //부서 목록 Method
        public async Task<List<DepartmentModel>> GetDepartmentList()
        {
            string baseUrl = "api/Employee/departmentList";
            var response = await httpClient.GetFromJsonAsync<List<DepartmentModel>>(baseUrl);
            Console.WriteLine(response);
            return response ?? new();
        }

        //직원을 추가하는 method
        public async Task EmployeeAdd(EmployeeModel emp)
        {
            string baseUrl = "api/Employee/employeeAdd";
            await httpClient.PostAsJsonAsync(baseUrl, emp);
        }

        //직원 삭제 Method
        public async Task EmployeeDelete(EmployeeModel emp)
        {
            string baseUrl = "api/Employee/employeeDelete";
            await httpClient.PostAsJsonAsync(baseUrl, emp);
        }
    }
}
