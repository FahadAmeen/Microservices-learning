using System;
using System.Text;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WebApplication2.API.Repositories.Interfaces;

namespace WebApplication2.API.RabbitMQ
{
    public class EventBusRabbitMQConsumer
    {
        private readonly IRabbitMQConnection _connection;

        public EventBusRabbitMQConsumer(IRabbitMQConnection connection, IOrderingRepository repository)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public void Consume()
        {
            var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: EventBusConstants.BasketCheckoutQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            //Create event when something receive
            consumer.Received += ReceivedEvent;

            channel.BasicConsume(queue: EventBusConstants.BasketCheckoutQueue, autoAck: true, consumer: consumer);
        }

        private  void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            if (e.RoutingKey == EventBusConstants.BasketCheckoutQueue)
            {
                var message = Encoding.UTF8.GetString(e.Body.Span);
                var basketCheckoutEvent = JsonConvert.DeserializeObject<BasketCheckoutEvent>(message);

                // EXECUTION : Call Internal Checkout Operation
                // var command = _mapper.Map<CheckoutOrderCommand>(basketCheckoutEvent);
                // var result = await _mediator.Send(command);
            }
        }

        public void Disconnect()
        {
            _connection.Dispose();
        }
    }
}
