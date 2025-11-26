#include "Motor.h"
#include "Sensor.h"
#include <Servo.h>

class Radar
{
  Motor motor;
  Sensor sensor;

public:
  Radar(int motorPin, int echoPin, int triggerPin)
      : motor(motorPin), sensor(triggerPin, echoPin)
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
      motor.TurnDegrees(pos);
      delay(10); // smaller delay = faster movement
    }

    for (int pos = 180; pos >= 0; pos -= 1)
    {
      motor.TurnDegrees(pos);
      delay(10);
    }
  }
};