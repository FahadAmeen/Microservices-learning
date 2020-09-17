using System;

namespace EventBusRabbitMQ.Events
{
    public class BasketCheckoutEvent
    {
        public Guid RequestId { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Country { get; set; }
    }
}
