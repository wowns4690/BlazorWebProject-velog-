using BlazorWebProject.Model;
using Microsoft.Azure.Cosmos;

namespace BlazorWebProject.InterFace
{
    public interface IEmployeeService
    {
        Task<List<EmployeeModel>> GetEmployeeList(); //직원 목록을 불러오는 Method
        Task<List<DepartmentModel>> GetDepartmentList(); //부서 목록을 불러오는 Method
        Task<List<EmployeeModel>> EmployeeDepartmentMapper(); //직원 목록 부서id를 참조하여 부서명을 불러오는 Method
        Task<int> EmployeeFindIdMax(); //직원 id값중 가장 큰 값을 찾아오는 Method
        Task EmployeeAdd(EmployeeModel emp); //직원을 추가하는 Method
        Task EmployeeDelete(EmployeeModel emp); //직원을 삭제하는 Method
        Task<EmployeeModel> GetEmployeeById(EmployeeModel emp); //특정 직원을 조회하는 Method
        Task EmployeeUpdate(EmployeeModel emp); //직원을 수정하는 Method
        Task<EmployeeModel?> Login(EmployeeLoginModel emp); //직원 로그인시 아이디와 패스워드 확인 Method
    }
}
