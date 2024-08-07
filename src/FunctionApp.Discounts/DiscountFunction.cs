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

        [Function("DiscountFunction")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
