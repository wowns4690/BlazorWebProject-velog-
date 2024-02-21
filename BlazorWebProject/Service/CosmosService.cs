using Microsoft.Azure.Cosmos;
using BlazorWebProject.Model;

namespace BlazorWebProject.Service
{
    public class CosmosService
    {
        private readonly CosmosClient _cosmosClient;
        private readonly string? _databaseName;

        public CosmosService(IConfiguration configuration)
        {
            // appsettings.json 파일에서 Cosmos DB 연결 정보 읽기
            var cosmosSettings = configuration.GetSection("CosmosDb");
            var accountEndpoint = cosmosSettings.GetValue<string>("AccountEndpoint");
            var accountKey = cosmosSettings.GetValue<string>("AccountKey");
            _databaseName = cosmosSettings.GetValue<string>("DatabaseName");
            

            // Cosmos DB 클라이언트 초기화
            _cosmosClient = new CosmosClient(accountEndpoint, accountKey);
        }

        public Container GetContainer(string _containerName)
        {
            var container = _cosmosClient.GetContainer(_databaseName, _containerName);
            return container;
        }

    }
}
