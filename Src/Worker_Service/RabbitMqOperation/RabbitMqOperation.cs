using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Worker_Service.RabbitMqOperation;

public class RabbitMqOperation
{

    public static void RabbitConsumer(string message = "hello")
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            Console.WriteLine(" [*] Waiting for logs");

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] {0}", message);
            };

            channel.BasicConsume("b", autoAck: true, consumer: consumer);
        }
    }
}
