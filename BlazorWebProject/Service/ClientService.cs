using BlazorWebProject.Controller;
using BlazorWebProject.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Xml.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BlazorWebProject.Service
{
    public class ClientService
    {
        private HttpClient httpClient;
        private HttpContext? httpContext;

        public ClientService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            this.httpClient = httpClient;
            this.httpContext = httpContextAccessor.HttpContext;
        }

        //직원 목록 Method
        public async Task<List<EmployeeModel>> GetEmployeeList()
        {
            const string baseUrl = "api/Employee/employeeList";

            var cookie = httpContext?.Request.Cookies["auth"];
            httpClient.DefaultRequestHeaders.Add("Cookie", $"auth={cookie}");

            var response = await httpClient.GetStringAsync(baseUrl);

            if (string.IsNullOrEmpty(response))
            {
                return new();
            }
            else
            {
                List<EmployeeModel> employeeList = JsonSerializer.Deserialize<List<EmployeeModel>>(response) ?? new();
                return employeeList ?? new();
            }
        }

        //부서 목록 Method
        public async Task<List<DepartmentModel>> GetDepartmentList()
        {
            const string baseUrl = "api/Employee/departmentList";
            var cookie = httpContext?.Request.Cookies["auth"];
            httpClient.DefaultRequestHeaders.Add("Cookie", $"auth={cookie}");
            var response = await httpClient.GetFromJsonAsync<List<DepartmentModel>>(baseUrl);
            return response ?? new();
        }

        //직원을 추가하는 method
        public async Task EmployeeAdd(EmployeeModel emp)
        {
            const string baseUrl = "api/Employee/employeeAdd";
            var cookie = httpContext?.Request.Cookies["auth"];
            httpClient.DefaultRequestHeaders.Add("Cookie", $"auth={cookie}");
            await httpClient.PostAsJsonAsync(baseUrl, emp);
        }

        //직원 삭제 Method
        public async Task EmployeeDelete(EmployeeModel emp)
        {
            const string baseUrl = "api/Employee/employeeDelete";
            var cookie = httpContext?.Request.Cookies["auth"];
            httpClient.DefaultRequestHeaders.Add("Cookie", $"auth={cookie}");
            await httpClient.PostAsJsonAsync(baseUrl, emp);
        }

        //특정 직원을 조회하는 Method
        public async Task<EmployeeModel> GetEmployeeById(EmployeeModel emp)
        {
            const string baseUrl = "api/Employee/employeeById";
            var cookie = httpContext?.Request.Cookies["auth"];
            httpClient.DefaultRequestHeaders.Add("Cookie", $"auth={cookie}");
            var response = await httpClient.PostAsJsonAsync<EmployeeModel>(baseUrl, emp);
            var responseContent = await response.Content.ReadAsStringAsync();
            var employee = JsonSerializer.Deserialize<EmployeeModel>(responseContent);
            return employee ?? new();
        }

        //직원 정보를 수정하는 Method
        public async Task EmployeeUpdate(EmployeeModel emp)
        {
            var cookie = httpContext?.Request.Cookies["auth"];
            httpClient.DefaultRequestHeaders.Add("Cookie", $"auth={cookie}");
            const string baseUrl = "api/Employee/employeeUpdate";
            await httpClient.PostAsJsonAsync(baseUrl, emp);
        }

    }
}
