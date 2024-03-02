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

namespace BlazorWebProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly Service.EmployeeService _employeeService;
        private readonly Service.JwtService _jwtService;

        public AuthController(IConfiguration configuration, EmployeeService employeeService, JwtService jwtService)
        {
            _configuration = configuration;
            _employeeService = employeeService;
            _jwtService = jwtService;
        }

        //로그인 Method
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] EmployeeModel emp)
        {
            var token = await _employeeService.Login(emp);
            if (token.IsNullOrEmpty())
            {
                return Unauthorized();
            }
            else
            {
                return Ok(token);
            }
        }
    }
}
