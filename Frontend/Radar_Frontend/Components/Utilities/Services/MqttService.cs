using MQTTnet;
using MQTTnet.Client;

namespace Radar_Frontend.Components.Utilities.Services
{
    public class MqttService
    {

        private IMqttClient _mqttClient;
        public event Action<string>? OnMessageReceived;

        public async Task ConnectAsync()
        {
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

            var options = new MQTTnet.Client.Options.MqttClientOptionsBuilder()
                .WithTcpServer("mosquitto", 1883)
                .WithCleanSession()
                .Build();

            // Handle incoming messages
            _mqttClient.UseApplicationMessageReceivedHandler(e =>
            {
                var topic = e.ApplicationMessage.Topic;
                var payload = e.ApplicationMessage.ConvertPayloadToString();
                OnMessageReceived?.Invoke(payload);
                return Task.CompletedTask;
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
                await _mqttClient.SubscribeAsync(topic);
            }
        }
    }
}
