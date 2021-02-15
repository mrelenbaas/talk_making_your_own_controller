//#include <ESP8266WiFi.h>
//#include <WiFiUdp.h>
#include "WiFi.h"
#include "AsyncUDP.h"

#ifndef STASSID
#define STASSID "NETGEAR08"
#define STAPSK  "largeskates190"
#endif

const char * ssid = "NETGEAR08";
const char * password = "largeskates190";
AsyncUDP udp;
unsigned int localPort = 50007;      // local port to listen on
int pin = 14;
int buzzer = 4;
int upPin = 13;
int downPin = 12;
int leftPin = 27;
int rightPin = 15;
int up;
int down;
int left;
int right;
char upText[] = "up000,";
char downText[] = "down0,";
char leftText[] = "left0,";
char rightText[] = "right,";
char blankText[] = "000000";
int upSize = 6;
int downSize = 6;
int leftSize = 6;
int rightSize = 6;
char middleText[] = "middle";

// buffers for receiving and sending data
//char packetBuffer[UDP_TX_PACKET_MAX_SIZE + 1]; //buffer to hold incoming packet,
char  ReplyBuffer[] = "acknowledged\r\n";       // a string to send back
char messageBuffer[] = "feather\r\n";
char OFF[] = "off";
int OFF_SIZE = 3;
char ON[] = "on";
int ON_SIZE = 2;
char original[] = "feather,00000000000000000000000000000000000000\r\n";
char output[] = "feather,00000000000000000000000000000000000000\r\n";
int i_start = 8;
const int SIZE = 48;
bool toggle = false;
bool firstLoop = true;

int JoyStick_X = A2; //x
int JoyStick_Y = A4; //y
int JoyStick_Z = 21; //key
const int ANALOG_LOWER = 800;
const int ANALOG_UPPER = 2800;

/*
  int redpin = 14; // 11; //select the pin for the red LED
  int bluepin = 12; // 10; // select the pin for the  blue LED
  int greenpin = 13; // 9;// select the pin for the green LED
  int val;
*/



WiFiUDP Udp;

void setup() {
  Serial.begin(9600);
  while (!Serial);
  pinMode(pin, OUTPUT);
  digitalWrite(pin, HIGH);
  pinMode(buzzer, OUTPUT);
  pinMode(upPin, INPUT_PULLUP);
  pinMode(downPin, INPUT_PULLUP);
  pinMode(leftPin, INPUT_PULLUP);
  pinMode(rightPin, INPUT_PULLUP);
  pinMode(JoyStick_X, INPUT);
  pinMode(JoyStick_Y, INPUT);
  pinMode(JoyStick_Z, INPUT_PULLUP);


  WiFi.mode(WIFI_STA);
  WiFi.begin(ssid, password);
  if (WiFi.waitForConnectResult() != WL_CONNECTED) {
    Serial.println("WiFi Failed");
    while (1) {
      delay(1000);
    }
  }
  if (udp.connect(IPAddress(192, 168, 1, 3), 50007)) {
    Serial.println("UDP connected");
    udp.onPacket([](AsyncUDPPacket packet) {
      Serial.print("UDP Packet Type: ");
      Serial.print(packet.isBroadcast() ? "Broadcast" : packet.isMulticast() ? "Multicast" : "Unicast");
      Serial.print(", From: ");
      Serial.print(packet.remoteIP());
      Serial.print(":");
      Serial.print(packet.remotePort());
      Serial.print(", To: ");
      Serial.print(packet.localIP());
      Serial.print(":");
      Serial.print(packet.localPort());
      Serial.print(", Length: ");
      Serial.print(packet.length());
      Serial.print(", Data: ");
      Serial.write(packet.data(), packet.length());
      Serial.println();
      //reply to the client
      packet.printf("Got %u bytes of data", packet.length());
    });
    //Send unicast
    udp.print("Hello Server!");
  }
}

void loop() {
  up = !digitalRead(upPin);
  down = !digitalRead(downPin);
  left = !digitalRead(leftPin);
  right = !digitalRead(rightPin);

  int x, y, z;
  x = analogRead(JoyStick_X);
  y = analogRead(JoyStick_Y);
  z = digitalRead(JoyStick_Z);
  //Serial.println(x);
  Serial.print(x);
  Serial.print(",");
  Serial.print(y);
  Serial.print(",");
  Serial.println(z);

  //if (z == 0)
  //{
  //  udp.broadcastTo(button, 50007);
  //  delay(10);
  //}

  /*
  Serial.print(up);
  Serial.print(", ");
  Serial.print(down);
  Serial.print(", ");
  Serial.print(left);
  Serial.print(", ");
  Serial.println(right);
  */

  if (up || (y < ANALOG_LOWER))
  {
    for (int i = i_start, j = 0; j < upSize; ++i, ++j)
    {
      output[i] = upText[j];
    }
  }
  else
  {
    for (int i = i_start, j = 0; j < upSize; ++i, ++j)
    {
      output[i] = blankText[j];
    }
  }
  if (down || (y > ANALOG_UPPER))
  {
    for (int i = (i_start + 6), j = 0; j < downSize; ++i, ++j)
    {
      output[i] = downText[j];
    }
  }
  else
  {
    for (int i = (i_start + 6), j = 0; j < downSize; ++i, ++j)
    {
      output[i] = blankText[j];
    }
  }
  if (left || (x < ANALOG_LOWER))
  {
    for (int i = (i_start + (6 * 2)), j = 0; j < leftSize; ++i, ++j)
    {
      output[i] = leftText[j];
    }
  }
  else
  {
    for (int i = (i_start + (6 * 2)), j = 0; j < leftSize; ++i, ++j)
    {
      output[i] = blankText[j];
    }
  }
  if (right || (x > ANALOG_UPPER))
  {
    for (int i = (i_start + (6 * 3)), j = 0; j < rightSize; ++i, ++j)
    {
      output[i] = rightText[j];
    }
  }
  else
  {
    for (int i = (i_start + (6 * 3)), j = 0; j < rightSize; ++i, ++j)
    {
      output[i] = blankText[j];
    }
  }
  
  if (z == 0)
  {
    for (int i = (i_start + (6 * 4)), j = 0; j < rightSize; ++i, ++j)
    {
      output[i] = middleText[j];
    }
  }
  else
  {
    for (int i = (i_start + (6 * 4)), j = 0; j < rightSize; ++i, ++j)
    {
      output[i] = blankText[j];
    }
  }
  
  udp.broadcastTo(output, 50007);
  delay(10);

  for (int i = 0; i < SIZE; i++)
  {
    output[i] = original[i];
  }
}
