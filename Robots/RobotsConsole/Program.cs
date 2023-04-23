using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Robots.Model;
using Robots.Services;

var configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();

var serviceProvider = new ServiceCollection()
    .AddSingleton<ITableTopService, TableTopService>()
    .AddSingleton<ICommandService, CommandService>()
    .Configure<GridOptions>(options => configuration.GetSection("GridOptions").Bind(options))
    .BuildServiceProvider();

var commandService = serviceProvider.GetService<ICommandService>();
commandService?.RunCommandLoop();