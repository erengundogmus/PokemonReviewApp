using PokemonWorkerService;
using Serilog;

string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
string logFilePath = @"C:\PokemonLogs\worker_log_.txt";

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSerilog();

builder.Services.AddHttpClient<PokemonApiClient>(client =>
{
    var baseUrl = builder.Configuration.GetSection("PokemonApiSettings:BaseUrl").Value;
    if (!string.IsNullOrEmpty(baseUrl))
    {
        client.BaseAddress = new Uri(baseUrl);
    }
});

builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "Pokemon API Worker Service";
});

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();