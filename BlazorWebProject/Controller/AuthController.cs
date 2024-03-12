using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BlazorWebProject.Model;
using BlazorWebProject.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorWebProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly EmployeeService _employeeService;
        private readonly NavigationManager _navigationManager;



        public AuthController(IConfiguration configuration, EmployeeService employeeService, NavigationManager navigationManager)
        {
            _configuration = configuration;
            _employeeService = employeeService;
            _navigationManager = navigationManager;
            
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

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(principal), new AuthenticationProperties
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
