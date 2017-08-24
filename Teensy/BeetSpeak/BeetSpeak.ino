//clock output
const int clocksource = 13;
//Input pins color coded
const int red = 0;
const int orange = 1;
const int yellow = 2;
const int green = 3;
const int blue = 7;
const int violet = 8;

//Variables for the states of the inputs
byte inputs[] = {  red, orange, yellow, green, blue, violet};

short keys[] = {KEY_1, KEY_2, KEY_3, KEY_4, KEY_5, KEY_6};
#define NUMINPUTS sizeof(inputs)

void setup() {
  // put your setup code here, to run once:
  pinMode(ledPin, OUTPUT);
  
  //Special for the Teensy is the INPUT_PULLUP
  //It enables a pullup resitor on the pin.
  for (byte i=0; i< NUMBUTTONS; i++) {
    pinMode(buttons[i], INPUT_PULLUP);
}

void loop() {
  // put your main code here, to run repeatedly:

}
