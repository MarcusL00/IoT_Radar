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

public:
  Radar(int motor_pin, Sensor& sensor_1, Sensor& sensor_2, MQTT& mqtt)
      : motor(motor_pin), sensor_1(sensor_1), sensor_2(sensor_2), mqtt(mqtt)
  {
  }

public:
  void Init()
  {
    motor.Init();
  }

public:
  void Scan()
  {
    for (int pos = 0; pos <= 180; pos += 1)
    { 
      if(pos % 10 == 0 && pos != 0) {
        mqtt.EnsureConnectivity();
      }

      ReadDistanceForSensors(pos);
    }

    for (int pos = 180; pos >= 0; pos -= 1)
    {
      if(pos % 10 == 0 && pos != 0) {
        mqtt.EnsureConnectivity();
      }

      ReadDistanceForSensors(pos);
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

      // Create a nested object for sensor data
      JsonObject sensors = doc.createNestedObject("sensors");
      sensors["sensor_1_distance"] = sensor_1_distance;
      sensors["sensor_2_distance"] = sensor_2_distance;
      sensors["unit"] = "cm";

      // Rotation stays at the root level
      doc["rotation"] = rotation;

      // Serialize JSON to a string
      static char buffer[256];   // static so it persists after function returns
      serializeJson(doc, buffer);

      return buffer;
    }
};