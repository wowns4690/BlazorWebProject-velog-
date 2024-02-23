using BlazorWebProject.Model;
using BlazorWebProject.Service;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private EmployeeService employeeService;
        public EmployeeController(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        //직원 목록 Method
        [HttpGet]
        [Route("employeeList")]
        public async Task<ActionResult<List<EmployeeModel>>> GetEmployeeList()
        {

            var employeeList = await employeeService.EmployeeDepartmentMapper();
            if(employeeList.Count == 0)
            {
                return NoContent();
            }
            return Ok(employeeList);
        }
    }
}
