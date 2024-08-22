using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp.Discounts
{
    public class DiscountFunction
    {
        private readonly ILogger<DiscountFunction> _logger;

        public DiscountFunction(ILogger<DiscountFunction> logger)
        {
            _logger = logger;
        }

        [Function("GetDiscount")]
        public IActionResult GetDiscount([HttpTrigger(AuthorizationLevel.Anonymous, "get" )] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }

        [Function("CreateDiscount")]
        public IActionResult CreateDiscount([HttpTrigger(AuthorizationLevel.Anonymous, "post" )] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }

        [Function("UpdateDiscount")]
        public IActionResult UpdateDiscount([HttpTrigger(AuthorizationLevel.Anonymous, "put" )] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }

        [Function("DeleteDiscount")]
        public IActionResult DeleteDiscount([HttpTrigger(AuthorizationLevel.Anonymous, "delete" )] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
