using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BlazorWebProject.Model;
using BlazorWebProject.Service;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace BlazorWebProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly EmployeeService _employeeService;


        public AuthController(IConfiguration configuration, EmployeeService employeeService)
        {
            _configuration = configuration;
            _employeeService = employeeService;
        }

        //로그인 Method
        [HttpPost("login")]
        public async Task<ActionResult<string>> SignInAsync([FromBody] EmployeeLoginModel emp)
        {
            var cosmosEmployee = await _employeeService.Login(emp);

            if (cosmosEmployee is null)
            {
                return Forbid();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, emp.Id),
                new Claim(ClaimTypes.Name, cosmosEmployee.Name),
                new Claim(ClaimTypes.Gender, cosmosEmployee.Gender),
                new Claim(ClaimTypes.Role, cosmosEmployee.DepartmentId)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties
            {
                IsPersistent = false,
                
            });

            return Ok("success");
        }



        [HttpPost("logout")]
        public async void SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
