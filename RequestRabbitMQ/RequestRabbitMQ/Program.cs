using RabbitMQ.Client;
using System;
using System.Text;

namespace RequestRabbitMQ
{
    class Program
    {
        static void Main(string[] args)
        {
            string UserName = "guest";
            string Password = "guest";
            string HostName = "localhost";

            var connectionFactory =new RabbitMQ.Client.ConnectionFactory()
            {
                UserName = UserName,
                Password = Password,
                HostName = HostName
            };

            var connection = connectionFactory.CreateConnection();
            var model = connection.CreateModel();

            Console.WriteLine("Creating Exchange...");

            model.ExchangeDeclare("jewelExchange", ExchangeType.Direct);

            model.QueueDeclare("jewelQueue", true, false, false, null);
            Console.WriteLine("Creating Queue");

            model.QueueBind("jewelQueue", "jewelExchange", "jewelexchange_key");
            Console.WriteLine("Creating Binding");

            var properties = model.CreateBasicProperties();
            properties.Persistent = false;

            byte[] messagebuffer = Encoding.Default.GetBytes("Direct Message");

            model.BasicPublish("jewelExchange", "jewelexchange_key", properties, messagebuffer);
            Console.WriteLine("Message Sent");

            Console.ReadLine();

        }
    }
}
