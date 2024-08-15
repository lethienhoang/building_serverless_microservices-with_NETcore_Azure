using FunctionApp.Catalogs.Commands.Products;
using FunctionApp.Catalogs.Queries.Products;
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
                var error = $"CreateProduct failed: {e.Message}";
                _logger.LogError(error);
                return new BadRequestObjectResult(error);
            }
        }

        //TODO do this new method later
        [Function("GetProductById")]
        public IActionResult GetProductById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "product/{id}")] HttpRequest req, Guid id)
        {
            _logger.LogInformation("Get product by id triggered...");
            try
            {
                var result = _sender.Send(new GetProductByIdQuery(id));
                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                var error = $"GetProductById failed: {e.Message}";
                _logger.LogError(error);
                return new BadRequestObjectResult(error);
            }
        }

        [Function("GetAllProducts")]
        public IActionResult GetAllProducts([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "product/?pageNumber={pageNumber}&pageSize={pageSize}")] HttpRequest req, int pageNumber = 1, int pageSize = 10)
        {
            _logger.LogInformation("Get all product with paging triggered...");
            try
            {
                var result = _sender.Send(new GetProductsQuery(pageNumber, pageSize));
                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                var error = $"GetAllProducts failed: {e.Message}";
                _logger.LogError(error);
                return new BadRequestObjectResult(error);
            }
        }
    }
}
