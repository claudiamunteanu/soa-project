using Confluent.Kafka;
using Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MonitoringService
    {
        private readonly ProducerConfig _producerConfig;
        private readonly string _orderTopic = "monitoring";

        public MonitoringService()
        {
            _producerConfig = new ProducerConfig
            {
                BootstrapServers = "kafka:29092",
                //BootstrapServers = "localhost:29092",
            };
        }

        public async Task MonitorEvent(string monitoredEvent)
        {
            using (var producer = new ProducerBuilder<Null, string>(_producerConfig).Build())
            {

                var message = new Message<Null, string>
                {
                    Value = monitoredEvent
                };

                await producer.ProduceAsync(_orderTopic, message);
            }
        }
    }
}
