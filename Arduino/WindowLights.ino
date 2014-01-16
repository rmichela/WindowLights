#include "Tlc5940.h"

void setup() {
  Serial.begin(9600);
  Tlc.init();
  Tlc.clear();
  Tlc.update();
}

uint8_t frameBufferOffset = 0;

void loop() {
  while (Serial.available()) {
    // Pause reading from the serial stream until the XLAT ISR has finished writing to the TLC
    while(tlc_needXLAT);
    // Read a byte from the serial stream and update the frame buffer
    tlc_GSData[frameBufferOffset++] = (char)Serial.read(); 
    if (frameBufferOffset == NUM_TLCS * 24) {
      frameBufferOffset = 0;
      Tlc.update();
    }
  }
}
