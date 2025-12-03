#include <WiFiS3.h>

class WiFiConnection {
  const char* ssid;
  const char* password;
  
  unsigned long lastWiFiCheck = 0;

  public:
    WiFiConnection(const char* ssid, const char* password) {
      this->ssid = ssid;
      this->password = password;
    }

    public:
      void Connect() {
        Serial.print("Connecting to Wi-Fi: ");
        Serial.println(ssid);

        WiFi.begin(ssid, password);

        unsigned long start = millis();
        while (WiFi.status() != WL_CONNECTED && millis() - start < 15000) {
          delay(500);
          Serial.print(".");
        }

        if (WiFi.status() == WL_CONNECTED) {
          Serial.println("\nWiFi connected!");
          Serial.print("IP Address: ");
          Serial.println(WiFi.localIP());
        } else {
          Serial.println("\nWiFi connection failed.");
        }
      }

  public:
    void EnsureConnectivity() {
      if (WiFi.status() != WL_CONNECTED) {
        Serial.println("WiFi lost, reconnecting...");
        Connect();
      }
    }

  public:
    char* GetMacAddress() {
      static char macStr[18];
      byte mac[6];
      WiFi.macAddress(mac);
    
      snprintf(macStr, sizeof(macStr), "%02X:%02X:%02X:%02X:%02X:%02X",
               mac[0], mac[1], mac[2], mac[3], mac[4], mac[5]);
    
      return macStr; 
    }
};
