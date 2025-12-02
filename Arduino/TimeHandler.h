#include <WiFiUdp.h>
#include <NTPClient.h>

class TimeHandler {
  WiFiUDP ntpUDP;
  NTPClient timeClient;
  
  public:
    // Constructor: initialize NTPClient with ntpUDP
    TimeHandler() 
      : timeClient(ntpUDP, "pool.ntp.org", 0, 60000) 
    {
      timeClient.begin();
    }
    
  public:
    long GetCurrentUnixTime() {
      if (WiFi.status() != WL_CONNECTED) {
        return 0;
      }
      timeClient.update();
      return timeClient.getEpochTime();
    }
};
