using BlazorWebProject.Model;
using System.Net.Http;

namespace BlazorWebProject.Service
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //로그인 요청을 하는 Method
        public async Task<string> Login(EmployeeLoginModel emp)
        {
            const string baseUrl = "api/auth/login";

            var response = await _httpClient.PostAsJsonAsync(baseUrl, emp);

            if (!response.IsSuccessStatusCode)
            {
                return "";
            }   

            var token = await response.Content.ReadAsStringAsync();

            return token;
        }
    }
}
