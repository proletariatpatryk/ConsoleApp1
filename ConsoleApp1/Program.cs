using ConsoleApp1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

var serviceProvider = new ServiceCollection()
    .AddSqlServer<BridgeContext>(config.GetConnectionString("POS"))
    .AddScoped<FinancialsService>()
    .BuildServiceProvider();

var service = serviceProvider.GetRequiredService<FinancialsService>();

await service.ProcessAsync(2);
