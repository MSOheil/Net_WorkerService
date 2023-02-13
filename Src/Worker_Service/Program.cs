using Serilog;
using Serilog.Events;
using Worker_Service;


Log.Logger = new LoggerConfiguration()
       .MinimumLevel.Debug()
       .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
       .Enrich.FromLogContext()
       .WriteTo.File(Directory.GetCurrentDirectory() + @"Logfile.txt")
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
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .UseSerilog()
    .Build();

host.Run();
