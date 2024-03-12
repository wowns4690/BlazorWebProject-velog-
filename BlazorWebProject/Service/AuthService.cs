using BlazorWebProject.Model;
using Microsoft.IdentityModel.Logging;
using System.Net.Http;
using System.Text.RegularExpressions;

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

            string cookies = response.Headers.GetValues("Set-Cookie").First();
            string pattern = @"auth=([^;]+)";
            Match match = Regex.Match(cookies, pattern);
            return match.Groups[1].Value;
        }

        //로그아웃 Method
        public async Task Logout()
        {
            const string baseUrl = "api/auth/logout";
            await _httpClient.PostAsync(baseUrl, null);
        }

    }
}
