using BlazorWebProject.Model;
using BlazorWebProject.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlazorWebProject.Controller
{
    [Authorize]
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

        //부서 목록 Method
        [HttpGet]
        [Route("departmentList")]
        public async Task<ActionResult<List<DepartmentModel>>> GetDepartmentList()
        {
            var departmentList = await employeeService.GetDepartmentList();
            if (departmentList.Count == 0)
            {
                return Ok(new List<EmployeeModel>());
            }
            return Ok(departmentList);
        }

        //직원 추가 Method
        [HttpPost]
        [Route("employeeAdd")]
        public async Task<ActionResult> EmployeeAdd([FromBody]EmployeeModel emp)
        {
            await employeeService.EmployeeAdd(emp);
            return Created();
        }

        //직원 삭제 Method
        [HttpPost]
        [Route("employeeDelete")]
        public async Task<ActionResult> EmployeeDelete([FromBody]EmployeeModel emp)
        {
            await employeeService.EmployeeDelete(emp);
            return NoContent();
        }

        //특정 직원을 조회하는 Method
        [HttpPost]
        [Route("employeeById")]
        public async Task<ActionResult<EmployeeModel>> GetEmployeeById([FromBody]EmployeeModel emp)
        {
            var response = await employeeService.GetEmployeeById(emp);
            return Ok(response);
        }

        //직원 정보를 업데이트 하는 Method
        [HttpPost]
        [Route("employeeUpdate")]
        public async Task<ActionResult> EmployeeUpdate([FromBody]EmployeeModel emp)
        {
            await employeeService.EmployeeUpdate(emp);
            return NoContent();
        }
    }
}
