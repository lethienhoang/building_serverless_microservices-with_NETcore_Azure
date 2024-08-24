using FunctionApp.Discounts.Commands;
using FunctionApp.Discounts.Queries;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionApp.Discounts
{
    public class DiscountFunction
    {
        private readonly ILogger<DiscountFunction> _logger;
        private readonly ISender _sender;
        public DiscountFunction(ILogger<DiscountFunction> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        [Function("GetDiscount")]
        public IActionResult GetDiscount([HttpTrigger(AuthorizationLevel.Anonymous, "get" )] HttpRequest req)
        {
            _logger.LogInformation("get collection of discount triggered...");
            try
            {
                var result = _sender.Send(new GetDiscountQuery()).Result;
                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                var error = $"GetDiscount failed: {e.Message}";
                _logger.LogError(error);
                return new BadRequestObjectResult(error);
            }
            return new OkObjectResult("Welcome to Azure Functions!");
        }

        [Function("CreateDiscount")]
        public async Task<IActionResult> CreateDiscount([HttpTrigger(AuthorizationLevel.Anonymous, "post" )] HttpRequest req)
        {
            _logger.LogInformation("Create new discount triggered...");
            try
            {
                var stringBody = await new StreamReader(req.Body).ReadToEndAsync();
                var reqDiscountCommand = JsonConvert.DeserializeObject<CreateDiscountCommand>(stringBody);
                if (reqDiscountCommand is null)
                {
                    throw new Exception("Invalid request object.");
                }

                var result = _sender.Send(reqDiscountCommand).Result;
                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                var error = $"CreateDiscount failed: {e.Message}";
                _logger.LogError(error);
                return new BadRequestObjectResult(error);
            }
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
