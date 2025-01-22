using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging/
using Microsoft.Azure.Documents.Client;
using System.Linq;
using System.Threading.Tasks;

public static class ListarCatalogosCosmos
{
    [FunctionName("ListarCatalogosCosmos")]
    public static async Task Run(
        [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req,
        [CosmosDB(
            databaseName: "NetflixCatalog",
            collectionName: "Catalogos",
            ConnectionStringSetting = "CosmosDBConnection")] DocumentClient client,
        ILogger log)
    {
        var collectionUri = UriFactory.CreateDocumentCollectionUri("NetflixCatalog", "Catalogos");

        var query = client.CreateDocumentQuery<dynamic>(collectionUri)
                          .AsEnumerable();

        var result = query.ToList();

        return new OkObjectResult(result);
    }
}
