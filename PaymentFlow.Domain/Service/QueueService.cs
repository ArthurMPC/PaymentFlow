using Newtonsoft.Json;
using PaymentFlow.Domain.Interfaces;
using PaymentFlow.Domain.Interfaces.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentFlow.Domain.Services
{
    public class QueueService : IQueueService
    {
        public int GetMessagesCount(string queueName)
        {
            //TODO: Hostname no config
            ConnectionFactory factory = new RabbitMQ.Client.ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };

            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel modelChannel = connection.CreateModel())
                {
                    modelChannel.QueueBind(queue: queueName, exchange: "amq.direct", routingKey: queueName);

                    int messageCount = int.Parse(modelChannel.MessageCount(queue: queueName).ToString());

                    return messageCount;
                }
            }
        }

        public T DequeueMessage<T>(string queueName)
        {
            //TODO: Hostname no config
            ConnectionFactory factory = new RabbitMQ.Client.ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };

            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel modelChannel = connection.CreateModel())
                {
                    modelChannel.QueueBind(queue: queueName, exchange: "amq.direct", routingKey: queueName);

                    BasicGetResult message = modelChannel.BasicGet(queue: queueName, autoAck: true);

                    string messageBody = Encoding.UTF8.GetString(message.Body, 0, message.Body.Length);

                    T objectMessage = JsonConvert.DeserializeObject<T>(messageBody);

                    return objectMessage;
                }
            }
        }

        public void QueueMessage(string queueName, object messageContent)
        {
            //TODO: Hostname no config
            ConnectionFactory factory = new RabbitMQ.Client.ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };

            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel modelChannel = connection.CreateModel())
                {
                    modelChannel.QueueBind(queue: queueName, exchange: "amq.direct", routingKey: queueName);

                    byte[] messageBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messageContent));

                    IBasicProperties basicProp = modelChannel.CreateBasicProperties();
                    basicProp.ContentType = "application/json";
                    basicProp.DeliveryMode = 2;

                    modelChannel.BasicPublish(exchange: "",
                        routingKey: queueName,
                        basicProperties: basicProp,
                        body: messageBytes
                        );
                }
            }
        }
    }
}
