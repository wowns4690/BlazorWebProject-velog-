using BlazorWebProject.Model;
using Microsoft.Azure.Cosmos;

namespace BlazorWebProject.Service
{
    public class EmployeeService
    {
        private CosmosService cosmosService;

        public EmployeeService(CosmosService cosmosService)
        {
            this.cosmosService = cosmosService;
        }

        //직원 목록을 불러오는 Method
        public async Task<List<EmployeeModel>> GetEmployeeList()
        {
            var container = cosmosService.GetContainer("Employee");
            var query = "SELECT * FROM c";
            var iterator = container.GetItemQueryIterator<EmployeeModel>(new QueryDefinition(query));
            var result = new List<EmployeeModel>();

            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                result.AddRange(response.ToList());
            }
            return result;
        }
    }
}
