#include "Config.h"
#include "Radar.h"

Radar radar(2, 3, 4);

void setup()
{
    radar.Init();
}

void loop()
{
    radar.Scan();
}
