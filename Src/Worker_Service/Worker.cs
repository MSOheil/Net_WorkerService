namespace Worker_Service;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }
    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Hello from stop");
        return base.StopAsync(cancellationToken);
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            RabbitMqOperation.RabbitMqOperation.RabbitConsumer();
            _logger.LogInformation("Hello from starting");
            await Task.Delay(1000, stoppingToken);
        }
    }
}
