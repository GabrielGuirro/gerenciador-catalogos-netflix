using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

public static class SalvarNoCosmosDB
{
    [FunctionName("SalvarNoCosmosDB")]
    public static async Task Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req,
        [CosmosDB(
            databaseName: "NetflixCatalog",
            collectionName: "Catalogos",
            ConnectionStringSetting = "CosmosDBConnection")] IAsyncCollector<dynamic> catalogoItems,
        ILogger log)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var catalogoItem = JsonConvert.DeserializeObject(requestBody);

        await catalogoItems.AddAsync(catalogoItem);

        log.LogInformation($"Catálogo salvo no Cosmos DB.");
        return new OkObjectResult("Catálogo salvo com sucesso.");
    }
}
