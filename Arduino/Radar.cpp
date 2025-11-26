#include <Servo.h>

Servo myServo;
int servoPin = 2;

void setup()
{
    myServo.attach(servoPin);
}

void loop()
{
    for (int pos = 0; pos <= 180; pos += 1)
    { // move slowly
        myServo.write(pos);
        delay(10); // smaller delay = faster movement
    }
    delay(2000); // pause 2 seconds

    for (int pos = 180; pos >= 0; pos -= 1)
    {
        myServo.write(pos);
        delay(10);
    }
    delay(2000);
}
