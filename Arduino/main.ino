#include "Config.h"
#include "WiFiConnection.h"
#include "MQTT.h"
#include "Radar.h"

WiFiConnection wiFiConnection(WIFI_SSID, WIFI_PASSWORD);
MQTT mqtt(MQTT_SERVER, MQTT_PORT);

Radar radar(MOTOR_PIN, DISTANCE_SENSOR_TRIG_PIN, DISTANCE_SENSOR_ECHO_PIN);

unsigned long last_sensor_read;

void setup()
{ 
  Serial.begin(9600);

  wiFiConnection.Connect();
  mqtt.Connect();

  radar.Init(); 
}

void loop()
{
  unsigned long runtime = millis();

  if (runtime - last_sensor_read >= 200)
  {
    radar.Scan();
    last_sensor_read = runtime;
  }

  wiFiConnection.EnsureConnectivity(runtime);
  mqtt.EnsureConnectivity();
}
