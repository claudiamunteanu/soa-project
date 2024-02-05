using Confluent.Kafka;

namespace KafkaProducer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string bootstrapServers = "localhost:9092";
            string topic = "tutorial";

            var producerConfig = new ProducerConfig
            {
                BootstrapServers = bootstrapServers,
            };

            using (var producer = new ProducerBuilder<Null, string>(producerConfig).Build())
            {
                // Produce a message to the topic
                var message = new Message<Null, string> { Value = "Hello, Kafka!" };
                var deliveryReport = await producer.ProduceAsync(topic, message);

                // Print delivery report details
                Console.WriteLine($"Delivered message to {deliveryReport.TopicPartitionOffset}");
            }
        }
    }
}
