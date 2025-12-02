
using MQTTnet;
using MQTTnet.Client;
using radar_frontend.Models;
using System.Text.Json;
namespace radar_frontend.Components.Services

{
    public class MqttService
    {
        private IMqttClient _mqttClient;
        public event Action<string>? OnMessageReceived;

        internal List<RadarData> radarDataList = new List<RadarData>();

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

                RadarData radarData = JsonSerializer.Deserialize<RadarData>(payload);
                radarDataList.Add(radarData);

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

        public List<RadarData> GetRadarData()
        {
            return radarDataList;
        }
    }
}