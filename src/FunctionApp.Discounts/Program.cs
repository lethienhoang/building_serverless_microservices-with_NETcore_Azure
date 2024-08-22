using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureAppConfiguration((context, builder) =>
    {
        builder.SetBasePath(context.HostingEnvironment.ContentRootPath)
        .AddJsonFile("local.settings.json", optional: true, reloadOnChange: false)
        .AddEnvironmentVariables();
    })
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.addDbContext<AppDbContext>(option =>
        {
            option.UseSqlServer(Configuration.GetConnectionString("DbConnectionString").ToString());
        });
    })
    .Build();

host.Run();
