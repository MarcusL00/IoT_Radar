using MQTTnet;
using MQTTnet.Client;

namespace Radar_Frontend.Components.Utilities.Services
{
    public class MqttService
    {
        private IMqttClient _mqttClient;
        public event Func<string, Task>? OnMessageReceived;

        public async Task ConnectAsync()
        {
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

            var options = new MQTTnet.Client.Options.MqttClientOptionsBuilder()
                .WithTcpServer("mosquitto", 1883)
                .WithCleanSession()
                .Build();

            // Handle incoming messages and await any async handlers
            _mqttClient.UseApplicationMessageReceivedHandler(async e =>
            {
                var topic = e.ApplicationMessage.Topic;
                var payload = e.ApplicationMessage.ConvertPayloadToString();
                var handler = OnMessageReceived;
                if (handler != null)
                {
                    // Invoke each subscriber and await it to ensure UI updates occur in order
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

            try
            {
                await _mqttClient.ConnectAsync(options);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MQTT connection failed: {ex.Message}");
            }
        }

        public async Task SubscribeAsync(string topic = "radar/distance")
        {
            if (_mqttClient?.IsConnected == true)
            {
                await _mqttClient.SubscribeAsync(
                    new MQTTnet.Client.Subscribing.MqttClientSubscribeOptionsBuilder()
                        .WithTopicFilter(f => f.WithTopic(topic))
                        .Build()
                );
            }
        }
    }
}
