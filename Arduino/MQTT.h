#include <PubSubClient.h>

class MQTT {
  WiFiClient wifiClient;
  PubSubClient client;

  const char* server_hostname;
  int server_port;

  unsigned long lastReconnectAttempt = 0;

  public:
    MQTT(const char* server_hostname, int server_port)
      : client(wifiClient) {
      this->server_hostname = server_hostname;
      this->server_port = server_port;
    }

  public:
    void Connect() {
      client.setServer(server_hostname, server_port);
    }
  
  public:
    void EnsureConnectivity() {
      while (!client.connected()) {
        Serial.print("Attempting MQTT connection...");
        if (client.connect("ArduinoClient")) {
          Serial.println("connected");
          client.subscribe("radar/distance"); 
        } else {
          Serial.print("failed, rc=");
          Serial.print(client.state());
          Serial.println(" try again in 5 seconds");
          delay(5000);
        }
      }
      client.loop(); // Keep alive
    }

    public:
      void PublishMessage(const char* message) {
        const char* topic = "radar/distance";
        Serial.println(message);
        client.publish(topic, message);
      }
};