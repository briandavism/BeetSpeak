/* Beet Speak Keyboard Mode
   You must select Keyboard from the "Tools > USB Type" menu
   Pin Key
    0-5:ROYGBV
    6:Clock Input
    7:Encoder Push
   13:LED Clock Out
   14:Encoder Pin1
   15:Encoder Pin3
*/

#include <Bounce.h>
#include <Encoder.h>

  // Creates Bounce objects for each button.  The Bounce object
  // automatically deals with contact chatter or "bounce", and
  // it makes detecting changes very simple.
  Bounce red = Bounce(0, 10);  // 10 = 10 ms debounce time
  Bounce orange = Bounce(1, 10);  // which is appropriate for
  Bounce yellow = Bounce(2, 10);  // most mechanical pushbuttons
  Bounce green = Bounce(3, 10);  // if a button is too "sensitive"
  Bounce blue = Bounce(4, 10);  // to rapid touch, you can
  Bounce violet = Bounce(5, 10);  // increase this time.
  Bounce clkIn = Bounce(6, 10);

  // Teensy 3.x / Teensy LC have the LED on pin 13
  const int ledPin = 13;

  // Encoder 
  Encoder knob(14,15);
  Bounce knobPush = Bounce(7, 10);

void setup() {
  // Configure the pins for input mode with pullup resistors.
  // The pushbuttons connect from each pin to ground.  When
  // the button is pressed, the pin reads LOW because the button
  // shorts it to ground.  When released, the pin reads HIGH
  // because the pullup resistor connects tB9 release

  // the chip.  LOW for "on", and HIGH for "off" may seem
  // backwards, but using the on-chip pullup resistors is very
  // convenient.  The scheme is called "active low", and it's
  // very commonly used in electronics... so much that the chip
  // has built-in pullup resistors!
  pinMode(0, INPUT_PULLUP);
  pinMode(1, INPUT_PULLUP);
  pinMode(2, INPUT_PULLUP);
  pinMode(3, INPUT_PULLUP);
  pinMode(4, INPUT_PULLUP);
  pinMode(5, INPUT_PULLUP);
  pinMode(6, INPUT_PULLUP);
  pinMode(7, INPUT_PULLUP);
   
  // initialize the digital pin as an output.
  pinMode(ledPin, OUTPUT);

  Serial.begin(9600);
  Serial.println("Knob Encoder Test");
}

long knobPosition = -999;

void loop() {
  long newKnob;
  newKnob = knob.read();
  if (newKnob != knobPosition) {
    Serial.print("Knob = ");
    Serial.print(newKnob);
    Serial.println();
    knobPosition = newKnob;
  }
  // if a character is sent from the serial monitor,
  // reset both back to zero.
  if (Serial.available()) {
    Serial.read();
    Serial.println("Reset both knob to zero");
    knob.write(0);
  }
  
  // Update all the buttons.  There should not be any long
  // delays in loop(), so this runs repetitively at a rate
  // faster than the buttons could be pressed and released.
  red.update();
  orange.update();
  yellow.update();
  green.update();
  blue.update();
  violet.update();
  clkIn.update();
  knobPush.update();

  // Check each button for "falling" edge.
  // Type a message on the Keyboard when each button presses
  // Update the Joystick buttons only upon changes.
  // falling = high (not pressed - voltage from pullup resistor)
  //           to low (pressed - button connects pin to ground)
  if (red.fallingEdge()) {
    Keyboard.println("R");
  }
  if (orange.fallingEdge()) {
    Keyboard.println("O");
  }
  if (yellow.fallingEdge()) {
    Keyboard.println("Y");
  }
  if (green.fallingEdge()) {
    Keyboard.println("G");
  }
  if (blue.fallingEdge()) {
    Keyboard.println("B");
  }
  if (violet.fallingEdge()) {
    Keyboard.println("V");
  }

  // Check each button for "rising" edge
  // Type a message on the Keyboard when each button releases.
  // For many types of projects, you only care when the button
  // is pressed and the release isn't needed.
  // rising = low (pressed - button connects pin to ground)
  //          to high (not pressed - voltage from pullup resistor)
  if (red.risingEdge()) {
    Keyboard.println("r");
  }
  if (orange.risingEdge()) {
    Keyboard.println("o");
  }
  if (yellow.risingEdge()) {
    Keyboard.println("y");
  }
  if (green.risingEdge()) {
    Keyboard.println("g");
  }
  if (blue.risingEdge()) {
    Keyboard.println("b");
  }
  if (violet.risingEdge()) {
    Keyboard.println("v");
  }
}

