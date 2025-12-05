#include "Motor.h"
#include "Sensor.h"
#include <Servo.h>
#include <ArduinoJson.h>

class Radar
{
  Motor motor;

  Sensor& sensor_1;
  Sensor& sensor_2;

  MQTT& mqtt;
  WiFiConnection& wiFiConnection;

  TimeHandler& timeHandler;

  char* unique_identifier;

public:
  Radar(int motor_pin, Sensor& sensor_1, Sensor& sensor_2, MQTT& mqtt, WiFiConnection& wiFiConnection, TimeHandler& timeHandler)
      : motor(motor_pin), sensor_1(sensor_1), sensor_2(sensor_2), mqtt(mqtt), wiFiConnection(wiFiConnection), timeHandler(timeHandler)
    {
    }

public:
  void Init(char* mac_address)
  {
    unique_identifier = mac_address;

    timeHandler.Init();
    motor.Init();
  }

public:
  void Scan()
  {
    for (int pos = 0; pos <= 180; pos += 1)
    { 
      if(pos % 10 == 0 && pos != 0) {
        wiFiConnection.EnsureConnectivity();
        mqtt.EnsureConnectivity();
        ReadDistanceForSensors(pos);
      }
      delay(100);
    }

    for (int pos = 180; pos >= 0; pos -= 1)
    {
      if(pos % 10 == 0 && pos != 0) {
        wiFiConnection.EnsureConnectivity();
        mqtt.EnsureConnectivity();
        ReadDistanceForSensors(pos);
      }
      delay(100);
    }
  }

  private:
    void ReadDistanceForSensors(int position) {
      long sensor_1_distance = sensor_1.getDistance();
      long sensor_2_distance = sensor_2.getDistance();
       
      motor.TurnDegrees(position);
      
      char* message = ConvertSensorDataToJson(position, sensor_1_distance, sensor_2_distance);
      mqtt.PublishMessage(message); 
    }

  private:
    char* ConvertSensorDataToJson(int rotation, int sensor_1_distance, int sensor_2_distance) {
      StaticJsonDocument<200> doc;

      JsonObject sensor_1 = doc.createNestedObject("sensor_1");
      sensor_1["distance"] = sensor_1_distance;
      sensor_1["unit"] = "cm";

      JsonObject sensor_2 = doc.createNestedObject("sensor_2");
      sensor_2["distance"] = sensor_2_distance;
      sensor_2["unit"] = "cm";

      doc["rotation"] = rotation;
      doc["radar_id"] = unique_identifier;
      doc["timestamp"] = timeHandler.GetCurrentUnixTime();

      // Serialize JSON to a string
      static char buffer[256];   // static so it persists after function returns
      serializeJson(doc, buffer);

      return buffer;
    }
};