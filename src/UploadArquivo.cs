using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.Storage;

public static class UploadArquivo
{
    [FunctionName("UploadArquivo")]
    public static async Task Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req,
        [Blob("catalogos", FileAccess.Write)] CloudBlobContainer blobContainer,
        ILogger log)
    {
        // Leitura do arquivo enviado no corpo da requisição
        var file = req.Form.Files[0];
        
        if (file != null)
        {
            var blob = blobContainer.GetBlockBlobReference(file.FileName);
            using (var stream = file.OpenReadStream())
            {
                await blob.UploadFromStreamAsync(stream);
            }

            log.LogInformation($"Arquivo {file.FileName} carregado com sucesso.");
            return new OkObjectResult($"Arquivo {file.FileName} carregado com sucesso.");
        }
        else
        {
            return new BadRequestObjectResult("Nenhum arquivo foi enviado.");
        }
    }
}
