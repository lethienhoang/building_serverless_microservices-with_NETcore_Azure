
using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;

public class ExceptionLoggingMiddleware : IFunctionsWorkerMiddleware
{
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            List<string> trace = new();
            Exception? tracer = ex;

            while (tracer != null)
            {
                trace.Add(tracer!.Message);
                tracer = tracer!.InnerException;
            }

            var httpReqData = await context.GetHttpRequestDataAsync();
            if (httpReqData != null)
            {
                var newhttpResData = httpReqData.CreateResponse(HttpStatusCode.InternalServerError);
                await newhttpResData.WriteAsJsonAsync(new
                {
                    success = false,
                    errors = JsonSerializer.Serialize(trace.ToArray())
                }, newhttpResData.StatusCode);

                context.GetInvocationResult().Value = newhttpResData;

            }
        }
    }
}
