//clock output
const int clocksource = 13;
//Input pins color coded
const int red = 0;
const int orange = 1;
const int yellow = 2;
const int green = 3;
const int blue = 4;
const int violet = 5;
#define KEYREPEAT 100  // milliseconds
#define KEYDELAY 200 // delay from first to second character

const int pinLEDOutput = 11;

//Variables for the states of the inputs
byte inputs[] = {red, orange, yellow, green, blue, violet};

int keys[] = {KEY_1, KEY_2, KEY_3, KEY_4, KEY_5, KEY_6}; //changed short to int
#define NUMINPUTS sizeof(inputs)

typedef void KeyFunction_t(uint8_t  c);
void myset_key1(uint8_t c)
{
  Keyboard.set_key1(c);
}

void myset_key2(uint8_t c)
{
  Keyboard.set_key2(c);
}

void myset_key3(uint8_t c)
{
  Keyboard.set_key3(c);
}

void myset_key4(uint8_t c)
{
  Keyboard.set_key4(c);
}

void myset_key5(uint8_t c)
{
  Keyboard.set_key5(c);
}

void myset_key6(uint8_t c)
{
  Keyboard.set_key6(c);
}
KeyFunction_t* buttonActive[NUMINPUTS];
KeyFunction_t* keyList[] = {myset_key6, myset_key5, myset_key4, myset_key3, myset_key2, myset_key1};
int            keySlot = sizeof(keyList) / sizeof(KeyFunction_t*);

void setup() {
  // put your setup code here, to run once:

  //Setup the pin modes.
  pinMode( pinLEDOutput, OUTPUT );
  
  //Special for the Teensy is the INPUT_PULLUP
  //It enables a pullup resitor on the pin.
  for (byte i=0; i< NUMINPUTS; i++) {
    pinMode(inputs[i], INPUT_PULLUP);
}

void loop() {
  // put your main code here, to run repeatedly:

//  //debugging the start button...
  digitalWrite ( pinLEDOutput, digitalRead(red));
  
  //Delay to avoid 'twitchiness' and bouncing inputs
  //due to too fast of sampling.
  //As said above, there is a better way to do this
  //than delay the whole MCU.
  delay(cintMouseDelay);
}

//Function to process the buttons from the SNES controller
void fcnProcessButtons()
{ 
  bool keysPressed = false; 
  bool keysReleased = false;
  
  // run through all the buttons
  for (byte i = 0; i < NUMBUTTONS; i++) {
    
    // are any of them pressed?
    if (! digitalRead(buttons[i])) 
    {                              //this button is pressed
      keysPressed = true;
      if (!buttonActive[i])        //was it pressed before?
        activateButton(i);            //no - activate the keypress
    }
    else
    {                              //this button is not pressed
      if (buttonActive[i]) {        //was it pressed before?
        releaseButton(i);            //yes - release the keypress
        keysReleased = true;
      }
    }
  }
  
  if (keysPressed || keysReleased)
    Keyboard.send_now();            //update all the keypresses

}

void activateButton(byte index)
{
  if (keySlot)      //any key slots left?
  {
    keySlot--;                                //Push the keySlot stack
    buttonActive[index] = keyList[keySlot];   //Associate the keySlot function pointer with the button
    (*keyList[keySlot])(keys[index]);         //Call the key slot function to set the key value 
  }
}

void releaseButton(byte index)
{
  keyList[keySlot] = buttonActive[index];    //retrieve the keySlot function pointer
  buttonActive[index] = 0;                   //mark the button as no longer pressed
  (*keyList[keySlot])(0);                    //release the key slot
  keySlot++;                                 //pop the keySlot stack
}


