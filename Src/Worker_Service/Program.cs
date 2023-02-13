using Serilog;
using Serilog.Events;
using Worker_Service;


Log.Logger = new LoggerConfiguration()
       .MinimumLevel.Debug()
       .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
       .Enrich.FromLogContext()
       .WriteTo.File(@"D:\soheyl\Git\GitProject\Worker_Service_Dotnet\Src\Worker_Service\bin\Release\net7.0\publish\Logfile.txt")
       .CreateLogger();
try
{

    Log.Information("App Strating up");
}
catch (Exception ex)
{
    Log.Fatal(ex, ex.Message);

    throw;
}
finally
{
    Log.CloseAndFlush();
}

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .UseSerilog()
    .Build();

host.Run();
