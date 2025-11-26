
class Sensor{
  int trig_Pin;
  int echo_Pin;


  public:
    Sensor(int trig_Pin, int echo_Pin) : trig_Pin(trig_Pin), echo_Pin(echo_Pin)  { }

  public:
    

  void setup(){
    pinMode(trig_Pin, OUTPUT);  
    pinMode(echo_Pin, INPUT);  
  }
  public:
    long getDistance() {
    // Trigger ultrasonic pulse
    digitalWrite(trigPin, LOW);
    delayMicroseconds(2);
    digitalWrite(trigPin, HIGH);
    delayMicroseconds(10);
    digitalWrite(trigPin, LOW);

    // Measure echo time
    long duration = pulseIn(echoPin, HIGH);

    // Convert to distance (cm)
    long distance = duration * 0.034 / 2;
    return distance;
}
};