int led1 = 2;  //further leds are offset from here

int button1 = 8;  //further buttons are offset from here

bool buttonState;

void setup() {
  pinMode(button1, INPUT);
  buttonState = digitalRead(button1);
  pinMode(led1, OUTPUT);
  digitalWrite(led1, LOW);
  Serial.begin(9600);
}

void loop() {
  bool newState = digitalRead(button1);
  if (newState != buttonState) {
    buttonState = newState;
    send_command(button1, newState);
  }

  //check for new commands
  if (Serial.available()) {
    char receivedData = Serial.read();
    receive_command(receivedData);
  }
}

void receive_command(char data) {
  if (data == '0') {
    digitalWrite(led1, LOW);
  }
  if (data == '1') {
    digitalWrite(led1, HIGH);
  }
}

void send_command(int button, bool state) {
  byte command = button;
  bitWrite(command, 7, state);
  Serial.write(command);
}
