using BlazorWebProject.Controller;
using BlazorWebProject.Model;

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
            var response = await httpClient.GetFromJsonAsync<List<EmployeeModel>>(baseUrl);
            return response ?? new();
        }
    }
}
