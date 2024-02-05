# Simple Kafka tutorial in .NET using Docker
This is a tutorial for creating a simple producer and consumer with Kafka and .NET, with Kafka being hosted in Docker in a container.

## 1. Create `docker-compose.yml` file
```
version: "2"
services:
  zookeeper:
    image: confluentinc/cp-zookeeper:6.2.0
    hostname: zookeeper
    container_name: zookeeper
    ports:
      - "22181:2181"
    environment:
      ZOOKEEPER_CLIENT_PORT: 2181
      ZOOKEEPER_TICK_TIME: 2000
  kafka:
    image: confluentinc/cp-kafka:6.2.0
    hostname: kafka
    container_name: kafka
    depends_on:
      - zookeeper
    ports:
      - "29092:29092"
      - "9092:9092"
      - "9101:9101"
    environment:
      KAFKA_BROKER_ID: 1
      KAFKA_ZOOKEEPER_CONNECT: 'zookeeper:2181'
      KAFKA_LISTENER_SECURITY_PROTOCOL_MAP: PLAINTEXT:PLAINTEXT,PLAINTEXT_HOST:PLAINTEXT
      KAFKA_ADVERTISED_LISTENERS: PLAINTEXT://kafka:29092,PLAINTEXT_HOST://localhost:9092
      KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR: 1
      KAFKA_TRANSACTION_STATE_LOG_MIN_ISR: 1
      KAFKA_TRANSACTION_STATE_LOG_REPLICATION_FACTOR: 1
      KAFKA_GROUP_INITIAL_REBALANCE_DELAY_MS: 0
      KAFKA_JMX_PORT: 9101
      KAFKA_JMX_HOSTNAME: localhost
```

Kafka relies on Zookeeper as a distributed coordination service to manage various aspects of its operations. We tell Kafka to be open for listeners on `kafka:29092` for internal listeners and on `localhost:9092`. Because in our tutorial we don't run the producer inside the same network as Kafka, we will use `localhost:9092`.

## 2. Create the containers
Inside the folder where you created the `docker-compose.yml` file, open a terminal and run the `docker-compose up -d` to create the containers.

## 3. Create the kafka topic
Before we create our consumer and producer, we need to create a topic for them to communicate with. We will do that from the container's bash. Inside the terminal:
1. run `docker exec -it kafka bash` to open a bash with the container.
2. run `kafka-topics --create --topic <your_topic_name> --bootstrap-server localhost:9092` to create the topic.
3. You can check your topic by running `kafka-topics --describe --topic <your_topic_name> --bootstrap-server localhost:9092`

## 4. Create Producer and Consumer applications
Open Visual Studio and create two Console App in C#, one for the producer and one for the consumer.

![image](https://github.com/claudiamunteanu/soa-project/assets/79506727/71ac55d6-ba82-478e-8fc9-c678eedcec06)

## 5. Install Kafka package
For each of the Producer and Consumer, install the `Confluent.Kafka` package: `Tools -> NuGet Package Manager -> Manage NuGet Packages for Solution`

![image](https://github.com/claudiamunteanu/soa-project/assets/79506727/ba7ca710-588d-4d9f-98aa-60cf90eb24b1)

## 6. Write consumer
Inside the `Program.cs` file in the Consumer application:

```
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
```

## 7. Write Producer
Inside the `Program.cs` file in the Producer application:

```
using Confluent.Kafka;

namespace KafkaProducer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Kafka producer configuration
            string bootstrapServers = "localhost:9092";
            string topic = "tutorial";

            var producerConfig = new ProducerConfig
            {
                BootstrapServers = bootstrapServers,
            };

            // Create a producer instance
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
```

## 8. Run the applications
First, run the Consumer to start listening to messages. If everything works correctly and it connects to the Kafka container, it should look something like this: 

![image](https://github.com/claudiamunteanu/soa-project/assets/79506727/f0042bfc-5650-477c-b37e-bcf507891d31)

Now run the Producer. It should stop fairly quick, but if everything went well, the applications should display the appropriate messages, and the Consumer should display the received message from the Producer.

| Consumer | Producer |
|---|---|
| ![image](https://github.com/claudiamunteanu/soa-project/assets/79506727/0bf4f44a-5cc1-40f7-8cef-c6892a742e3c) | ![image](https://github.com/claudiamunteanu/soa-project/assets/79506727/ba8570e3-0f78-4a78-9882-aaf352f26aed) |

## 9. Congratulations!
You successfully communicated through Kafka in .NET!
