using FunctionApp.Catalogs.Commands.Products;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionApp.Catalogs
{
    public class CatalogFunction
    {
        private readonly ILogger<CatalogFunction> _logger;
        private readonly ISender _sender;

        public CatalogFunction(ILogger<CatalogFunction> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        [Function("CreateProduct")]
        public async Task<IActionResult> CreateProductAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "product")] HttpRequest req)
        {
            _logger.LogInformation("Create new product triggered...");
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var reqProduct = JsonConvert.DeserializeObject<CreateProductCommand>(requestBody);
                if (reqProduct is null)
                {
                    return new BadRequestObjectResult("Not found: request body");
                }
                var result = _sender.Send(reqProduct);
                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                var error = $"GetTrips failed: {e.Message}";
                _logger.LogError(error);
                return new BadRequestObjectResult(error);
            }
        }

        //TODO do this new method later
        [Function("GetProductById")]
        public async Task<IActionResult> GetProductByIdAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "product/{id}")] HttpRequest req, Guid id)
        {
            _logger.LogInformation("Create new product triggered...");
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var reqProduct = JsonConvert.DeserializeObject<CreateProductCommand>(requestBody);
                if (reqProduct is null)
                {
                    return new BadRequestObjectResult("Not found: request body");
                }
                var result = _sender.Send(reqProduct);
                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                var error = $"GetTrips failed: {e.Message}";
                _logger.LogError(error);
                return new BadRequestObjectResult(error);
            }
        }
    }
}
