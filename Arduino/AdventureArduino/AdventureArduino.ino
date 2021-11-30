#include <SPI.h>
#include <MFRC522.h>
#define SS_PIN 10
#define RST_PIN 9
MFRC522 rfid(SS_PIN, RST_PIN);

int joystickX = A4;
int joystickY = A5;
int SW = 5;
String nuid, estadoB;

unsigned long myTime;
unsigned long myTimeA;
String entrada = "";
bool flag;
bool recogio;
int boton = 2;
int led = 6;
int buzzer = 8;

void setup()
{
  Serial.begin(9600);
  
  SPI.begin(); // Init SPI bus
  rfid.PCD_Init(); // Init MFRC522
  
  pinMode(led, OUTPUT);
  pinMode(buzzer, OUTPUT);
  pinMode(joystickX, INPUT);
  pinMode(joystickY, INPUT);
  pinMode(boton, INPUT);
  pinMode(SW, INPUT);
  digitalWrite(SW, HIGH);
  digitalWrite(buzzer, HIGH);
}

void loop ()
{
  myTime = millis();
  if (digitalRead(buzzer) == LOW)
  {     
    if (myTime - myTimeA >= 100)
    {  
      digitalWrite(buzzer, HIGH);    
    }
  }  
  
  int yPin = analogRead(joystickX);
  int xPin = analogRead(joystickY);
  int zPin = digitalRead(SW);
  estadoB = digitalRead(boton);
  
  Serial.print(xPin); Serial.print("/");
  Serial.print(yPin); Serial.print("/");
  Serial.print(zPin); Serial.print("/");
  Serial.print(estadoB); Serial.print("/");
  Serial.println(nuid);

  nuid = "";
  
  while(Serial.available())
  {  
    char caracter = (char)Serial.read();
    entrada += caracter;
    if (caracter == '\n')
    {
      flag = true;
    }
  }
  if (flag)
  { 
    entrada.trim();
    analogWrite(led, entrada.toInt());
    digitalWrite(buzzer, LOW);
    myTimeA = millis();   
    entrada = "";
    flag = false;
  }

  if ( ! rfid.PICC_IsNewCardPresent())
    return;
    
  if ( ! rfid.PICC_ReadCardSerial())
    return;
    
  printHex(rfid.uid.uidByte, rfid.uid.size);
  
  rfid.PICC_HaltA();
  rfid.PCD_StopCrypto1();
}

void printHex(byte *buffer, byte bufferSize)
{
  for (byte i = 0; i < bufferSize; i++)
  {
    nuid = nuid + String(buffer[i], HEX);
  }
}
