# Arduino

To make the Arduino function correctly, you will need to create a config file called "Config.h".

The file should have this format:
```
// Wi-Fi
#define WIFI_SSID "WIFI SSID"
#define WIFI_PASSWORD "WIFI PASSWORD"

// MQTT
#define MQTT_SERVER "IP ADDRESS"
#define MQTT_PORT 1883

// Ultrasonic distance sensor
#define DISTANCE_SENSOR_TRIG_PIN_1 10
#define DISTANCE_SENSOR_ECHO_PIN_1 11

#define DISTANCE_SENSOR_TRIG_PIN_2 5
#define DISTANCE_SENSOR_ECHO_PIN_2 6

#define MOTOR_PIN 2
```

Change the values so they fit your project.