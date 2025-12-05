using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Subscribing;
using System;
using System.Threading.Tasks;

namespace Radar_Frontend.Components.Utilities.Services
{
    public class MqttService
    {
        private readonly IMqttClient _mqttClient;
        private bool _subscribed = false;

        public event Func<string, Task>? OnMessageReceived;

        public MqttService()
        {
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

            // Register the handler ONCE in the constructor
            _mqttClient.UseApplicationMessageReceivedHandler(async e =>
            {
                var topic = e.ApplicationMessage.Topic;
                var payload = e.ApplicationMessage.ConvertPayloadToString();

                Console.WriteLine($"MQTT message received on topic '{topic}': {payload}");

                var handler = OnMessageReceived;
                if (handler != null)
                {
                    foreach (var d in handler.GetInvocationList())
                    {
                        try
                        {
                            var func = (Func<string, Task>)d;
                            await func(payload);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"MQTT handler exception: {ex.Message}");
                        }
                    }
                }
            });
        }

        public async Task ConnectAsync()
        {
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer("mosquitto", 1883)
                // Consider removing CleanSession if you want broker to remember subscriptions
                .WithCleanSession()
                .Build();

            try
            {
                await _mqttClient.ConnectAsync(options);
                Console.WriteLine("MQTT connected.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MQTT connection failed: {ex.Message}");
            }
        }

        public async Task SubscribeAsync(string topic = "radar/distance")
        {
            if (_mqttClient?.IsConnected == true && !_subscribed)
            {
                await _mqttClient.SubscribeAsync(
                    new MqttClientSubscribeOptionsBuilder()
                        .WithTopicFilter(f => f.WithTopic(topic))
                        .Build()
                );

                _subscribed = true;
                Console.WriteLine($"Subscribed to topic '{topic}'.");
            }
            else if (_subscribed)
            {
                Console.WriteLine($"Already subscribed to topic '{topic}', skipping duplicate subscription.");
            }
        }
    }
}
