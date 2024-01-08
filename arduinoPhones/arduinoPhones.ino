int led1 = 2; //further leds are offset from here

int button1 = 8; //further buttons are offset from here

int phoneCount = 5; //if you adjust this, adjust the array under as well
bool buttonStates[] = {0, 0, 0, 0, 0};

void setup() 
{
  for(int i = 0; i < phoneCount; i++){
    pinMode(button1 + i, INPUT);
    buttonStates[i] = digitalRead(button1 + i);
  }
  for(int i = 0; i < phoneCount; i++){
    pinMode(led1 + i, OUTPUT);
    digitalWrite(led1 + i, LOW);
  }
  Serial.begin(9600);
}


void loop()
{
  //check if any button states have changed
  for(int i = 0; i < phoneCount; i++){
    bool newState = digitalRead(button1 + i);
    if(newState != buttonStates[i]){
      send_command(button1 + i, newState);
      buttonStates[i] = newState;
    }
  }

  //check for new commands
  if(Serial.available()){
    byte receivedData = Serial.read();
    receive_command(receivedData);
  }
}

void receive_command(byte data){
  bool lightSet = bitRead(data, 7);
  bitWrite(data, 7, 0);
  int led = data;
  digitalWrite(led, lightSet);
}

void send_command(int button, bool state){
  byte command = button;
  bitWrite(command, 7, state);
  Serial.write(command);
}
