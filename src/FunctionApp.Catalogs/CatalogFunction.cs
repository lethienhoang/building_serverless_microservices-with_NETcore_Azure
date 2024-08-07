using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp.Catalogs
{
    public class CatalogFunction
    {
        private readonly ILogger<CatalogFunction> _logger;

        public CatalogFunction(ILogger<CatalogFunction> logger)
        {
            _logger = logger;
        }

        [Function("CatalogFunction")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
