#include "Motor.h"
#include "Sensor.h"
#include <Servo.h>

class Radar
{
  Motor motor;
  Sensor sensor;

public:
  Radar(int motor_pin, int trigger_pin, int echo_pin)
      : motor(motor_pin), sensor(trigger_pin, echo_pin)
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
    { // move slowly
      long distance = sensor.getDistance();
      delay(500); // smaller delay = faster movement
      motor.TurnDegrees(pos);
      Serial.println(distance);
    }

    for (int pos = 180; pos >= 0; pos -= 1)
    {
      long distance = sensor.getDistance();
      delay(500); // smaller delay = faster movement
      motor.TurnDegrees(pos);
      Serial.println(distance);
    }
  }
};