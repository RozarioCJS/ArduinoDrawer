const int SW_pin = 2; // digital pin connected to switch output
const int X_pin = 0; // analog pin connected to X output
const int Y_pin = 1; // analog pin connected to Y output

int vertValue,horzValue;

void setup() {
  pinMode(X_pin, INPUT);
  pinMode(Y_pin, INPUT);
  pinMode(SW_pin, INPUT);
  digitalWrite(SW_pin, HIGH);
  Serial.begin(9600);
}

void loop() {
  horzValue = analogRead(X_pin);
  vertValue = analogRead(Y_pin);
  Serial.print(digitalRead(SW_pin));
  Serial.print(",");
  Serial.print(horzValue/2);
  Serial.print(",");
  Serial.print(vertValue/2);
  Serial.print("\n");
}
