using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost" };

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "hello",
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

var consumidor = new EventingBasicConsumer(channel);

consumidor.Received += (modelo, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine("[x] Recebido {0}", message);
};

channel.BasicConsume(queue: "hello",
                     autoAck: true,
                     consumer: consumidor);


Console.WriteLine("Pressione [enter] para sair.");
Console.ReadLine();


