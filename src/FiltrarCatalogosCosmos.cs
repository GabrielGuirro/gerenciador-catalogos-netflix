using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Documents.Client;
using System.Linq;
using System.Threading.Tasks;

public static class FiltrarCatalogosCosmos
{
    [FunctionName("FiltrarCatalogosCosmos")]
    public static async Task Run(
        [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req,
        [CosmosDB(
            databaseName: "NetflixCatalog",
            collectionName: "Catalogos",
            ConnectionStringSetting = "CosmosDBConnection")] DocumentClient client,
        ILogger log)
    {
        var genero = req.Query["genero"];
        var ano = req.Query["ano"];
        
        var collectionUri = UriFactory.CreateDocumentCollectionUri("NetflixCatalog", "Catalogos");

        var query = client.CreateDocumentQuery<dynamic>(collectionUri)
                          .Where(c => (string)c.genero == genero || (string)c.ano == ano)
                          .AsEnumerable();

        var result = query.ToList();
        
        return new OkObjectResult(result);
    }
}
