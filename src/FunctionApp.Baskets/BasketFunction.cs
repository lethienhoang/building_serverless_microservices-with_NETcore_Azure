using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp.Baskets
{
    public class BasketFunction
    {
        private readonly ILogger<BasketFunction> _logger;

        public BasketFunction(ILogger<BasketFunction> logger)
        {
            _logger = logger;
        }

        [Function("BasketFunction")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
