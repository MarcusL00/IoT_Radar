#include "Config.h"
#include "WiFiConnection.h"
#include "MQTT.h"
#include "TimeHandler.h"
#include "Radar.h"

WiFiConnection wiFiConnection(WIFI_SSID, WIFI_PASSWORD);
MQTT mqtt(MQTT_SERVER, MQTT_PORT);

TimeHandler timeHandler;

Sensor sensor_1(DISTANCE_SENSOR_TRIG_PIN_1, DISTANCE_SENSOR_ECHO_PIN_1);
Sensor sensor_2(DISTANCE_SENSOR_TRIG_PIN_2, DISTANCE_SENSOR_ECHO_PIN_2);

Radar radar(MOTOR_PIN, sensor_1, sensor_2, mqtt, timeHandler);

unsigned long last_sensor_read;

void setup()
{ 
  Serial.begin(9600);

  wiFiConnection.Connect();
  mqtt.Connect();

  char* mac_address = wiFiConnection.GetMacAddress();
  radar.Init(mac_address); 
}

void loop() {
  unsigned long runtime = millis();

  if (runtime - last_sensor_read >= 200)
  {
    radar.Scan();
    last_sensor_read = runtime;
  }

  wiFiConnection.EnsureConnectivity(runtime);
}
