class Sensor
{
  int trig_pin;
  int echo_pin;

public:
  Sensor(int trig_pin, int echo_pin) : trig_pin(trig_pin), echo_pin(echo_pin)
  {
    pinMode(trig_pin, OUTPUT);
    pinMode(echo_pin, INPUT);
  }

public:
  long getDistance()
  {
    // Trigger ultrasonic pulse
    digitalWrite(trig_pin, LOW);
    delayMicroseconds(2);
    digitalWrite(trig_pin, HIGH);
    delayMicroseconds(10);
    digitalWrite(trig_pin, LOW);

    // Measure echo time with timeout
    long duration = pulseIn(echo_pin, HIGH, 30000); // 30 ms = ~5m range

    if (duration == 0) {
      return -1; // no echo detected
    }

    // Convert to distance (cm)
    long distance = (duration * 0.034) / 2.0;
    return distance;
  }
};
