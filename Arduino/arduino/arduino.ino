void setup() {
  // put your setup code here, to run once:
  pinMode(13, OUTPUT);
  pinMode(6, INPUT);
  pinMode(7, INPUT);
  
  Serial.begin(9600);
}

int incomingByte = 0;
void loop() {
  // put your main code here, to run repeatedly:
  if (Serial.available() > 0) {
    // считываем входящий байт:
    incomingByte = Serial.read();
    // показываем, что именно мы получили:
    if(incomingByte == 48){
      digitalWrite(13, LOW);
      Serial.print("LOW\n");
    }
    if(incomingByte == 49){
      digitalWrite(13, HIGH);
      Serial.print("HIGH\n");
    }
  }
  if(digitalRead(7) == HIGH){
    digitalWrite(13, HIGH);
    Serial.print("HIGH\n");
  }
  if(digitalRead(6) == HIGH){
    digitalWrite(13, LOW);
    Serial.print("LOW\n");
  }
}


void serialEvent(){
  byte r = Serial.read();
  if (r == 0) digitalWrite(13, LOW);
  if (r == 1) digitalWrite(13, HIGH);
}

