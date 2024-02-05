using Confluent.Kafka;

namespace KafkaConsumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Kafka consumer configuration
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "groupId",
            };

            // Create a consumer instance
            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                // Subscribe to a Kafka topic
                consumer.Subscribe("tutorial");

                Console.WriteLine("Consumer started. Press Ctrl+C to exit.");

                // Start consuming messages
                try
                {
                    while (true)
                    {
                        // Consume a message
                        var consumeResult = consumer.Consume();

                        // Check if the message was consumed successfully
                        if (consumeResult != null)
                        {
                            Console.WriteLine();
                            Console.WriteLine(consumeResult.Message.Value);
                            Console.WriteLine();
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    // Graceful shutdown if cancellation is requested
                }
                finally
                {
                    consumer.Close();
                }
            }
        }
    }
}
