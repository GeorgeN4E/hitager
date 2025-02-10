/*
 * (c) Janne Kivijakola
 * janne@kivijakola.fi
 *
 * Modified for the Raspberry Pi Pico (Arduino IDE) by [Your Name]
 *
 * NOTES:
 * - AVR-specific headers and registers have been removed.
 * - _delay_ms() and _delay_us() calls have been replaced by delay() and delayMicroseconds().
 * - The pulse–measurement code that originally used Timer1 now uses micros().
 * - The clock output configuration (originally using Timer2 registers) is commented and left
 *   as a placeholder. You will need to set up a 4MHz PWM (or similar) if required.
 */

#include <Arduino.h>

// Define pins
#if defined(__AVR_ATmega328P__)
const int CLKOUT = 3;
#elif defined(__AVR_ATmega2560__)
const int CLKOUT = 9;
#else
// For the Pico (or other boards), choose an appropriate pin for the clock output.
const int CLKOUT = 3; // Adjust as needed.
#endif

const int SCK_pin = 6;
const int dout_pin = 7;
const int din_pin = 2;   // Note: din_pin must support external interrupts!
const int test_pin = 4;

const char hitagerVersion[] = {"211"};  // Major Version, Minor Version, Fix

// Since we no longer use AVR timers, define dummy macros:
#define START_TIMER  lastTime = micros();
#define STOP_TIMER   // no operation

byte sampcont = 0x17;

unsigned int isrtimes[400];
unsigned int *isrtimes_ptr = isrtimes;
volatile int bitsCnt = 0;
volatile int isrCnt = 0;
volatile int capturedone = 0;
unsigned long starttime = 0;
int rfoffset = 2;
unsigned char AbicPhaseMeas;
int debug = 0;
int decodemode = 0;
int delay_1 = 20;
int delay_0 = 14;
int delay_p = 5;
int hysteresis = 1;

/* ABIC Settings */
static union{
  struct {
    unsigned char filter_l:1;
    unsigned char filter_h:1;
    unsigned char gain:2;
    unsigned char page_nr:2;
    unsigned char SetPageCmd:2;
  };
  unsigned char byteval;
} AbicConf_Page0;

static union{
  struct {
    unsigned char txdis:1;
    unsigned char hysteresis:1;
    unsigned char pd:1;
    unsigned char pd_mode:1;
    unsigned char page_nr:2;    
    unsigned char SetPageCmd:2;
  };
  unsigned char byteval;
} AbicConf_Page1;

static union{
  struct {
    unsigned char freeze:2;
    unsigned char acqamp:1;
    unsigned char threset:1;
    unsigned char page_nr:2;    
    unsigned char SetPageCmd:2;
  };
  unsigned char byteval;
} AbicConf_Page2;

//volatile unsigned long vvdiDelay = 7768;
volatile unsigned long vvdiDelay = 6982;

// Global variable for timing (replacing the AVR Timer1 counter)
volatile unsigned long lastTime = 0;

void setup() {
  Serial.begin(115200);
  while (!Serial) {
    ; // wait for serial port to connect if needed
  }

  AbicConf_Page0.SetPageCmd = 1;
  AbicConf_Page1.SetPageCmd = 1;
  AbicConf_Page2.SetPageCmd = 1;
  
  AbicConf_Page0.gain = 2;
  AbicConf_Page0.filter_h = 1;
  AbicConf_Page0.filter_l = 1;
  AbicConf_Page1.hysteresis = 0;
  AbicConf_Page1.page_nr = 1;
  AbicConf_Page2.page_nr = 2;
  
  // Configure the clock output pin.
  pinMode(CLKOUT, OUTPUT);
  pinMode(test_pin, OUTPUT);
  // ***** IMPORTANT *****
  // The AVR code configures Timer2 to output a 4MHz clock.
  // On the Pico you must configure PWM (or similar) to get this frequency.
  // For now, we simply set the CLKOUT pin LOW as a placeholder.
  digitalWrite(CLKOUT, LOW);

  // Configure I/O pins.
  pinMode(SCK_pin, OUTPUT);
  pinMode(dout_pin, OUTPUT);
  pinMode(din_pin, INPUT);
  
  digitalWrite(SCK_pin, HIGH);
  digitalWrite(dout_pin, HIGH);
  digitalWrite(din_pin, HIGH); 

  Serial.println("Init done");

  writePCF7991Reg(0x40, 8); // wake up
  AbicConf_Page1.txdis = 0;
  writePCF7991Reg(AbicConf_Page1.byteval, 8); // RF on
  byte readval = 0;
  for (int i = 2; i < 9; i++) {
    readval = readPCF7991Reg(i);
    printRegister(i, readval);
  }

  Serial.println("set sampling time");
  int samplingT = (1 << 7) | (0x3f & sampcont);
  readval = readPCF7991Reg(samplingT);
  printRegister(samplingT, readval);

  Serial.println("done registers");
}

void loop() {
  while (Serial.available() < 1) {
    delay(1);
  }
  byte command = Serial.read();
  switch (command) {
    case 'a': {
      Serial.println("adapt offset");
      rfoffset = serialToByte();
      Serial.print("RESP:\n");
      Serial.print(rfoffset, HEX);
      Serial.println("\nEOF");
      break;
    }
    
    case 'b': {
      Serial.println("decoding mode");
      decodemode = serialToByte();
      Serial.print("RESP:\n");
      Serial.print(decodemode, HEX);
      Serial.println("\nEOF");
      break;
    }

    case 'c': {
      Serial.println("Pulse width delay adjust");
      delay_p = serialToByte() & 0xff;
      Serial.print("RESP:\n");
      Serial.print(delay_p, HEX);
      Serial.println("\nEOF");
      break;
    }
    
    case 'd': {
      Serial.println("debug mode");
      debug = serialToByte();
      Serial.print("RESP:\n");
      Serial.print(debug, HEX);
      Serial.println("\nEOF");
      break;
    }

    case 'f': {
      AbicConf_Page1.txdis = 1;
      writePCF7991Reg(AbicConf_Page1.byteval, 8); // RF off
      Serial.println("RFOFF");
      break;
    }
    
    case 'g': {
      Serial.println("Gain adjust");
      AbicConf_Page0.gain = serialToByte() & 0x3;
      writePCF7991Reg(AbicConf_Page0.byteval, 8);
      Serial.print("RESP:\n");
      Serial.print(AbicConf_Page0.gain, HEX);
      Serial.println("\nEOF");
      break;
    }
    
    case 'h': {
      Serial.println("Hysteresis");
      AbicConf_Page1.hysteresis = serialToByte() & 0x1;
      Serial.print("RESP:\n");
      Serial.print(AbicConf_Page1.hysteresis, HEX);
      Serial.println("\nEOF");
      break;
    }
    
    case 'i': {
      Serial.println("transfer");
      byte cmdlength = serialToByte();
      byte authcmd[300] = {0};
      if (cmdlength > 1 && cmdlength < 200) {
        for (int i = 0; i < (cmdlength + 7) / 8; i++) {
          authcmd[i] = serialToByte();
        }
      }
      communicateTag(authcmd, cmdlength);
      Serial.println("EOF");
      break;
    }
    
    case 'o': {
      AbicConf_Page1.txdis = 0;
      writePCF7991Reg(AbicConf_Page0.byteval, 8);
      writePCF7991Reg(AbicConf_Page2.byteval, 8);
      writePCF7991Reg(AbicConf_Page1.byteval, 8); // RF on
      adapt(rfoffset);
      Serial.println("RFON");
      break;
    }

    case 'p': {
      unsigned char PageNr = Serial.read();
      if (PageNr > '3' || PageNr < '0') {
        Serial.println("Page Nr. not within permitted range\nERROR");
      } else {
        Serial.print("Read ABIC config Page");
        Serial.write(PageNr);
        Serial.println(":");
        PageNr -= '0'; // Convert character to number
        byte PageData = readPCF7991Reg(0b00000100 | PageNr);
        Serial.print("RESP:");
        Serial.print(PageData, HEX);
        Serial.println("\nEOF");
      }      
      break;
    }
    
    case 'q': {
      Serial.println("Super chip init");
      writePCF7991Reg(0x50 | ((hysteresis & 0x1) << 1), 8); // RF on
      delayMicroseconds(350);
      delayMicroseconds(2500);
      superInit();
      break;
    }

    case 'r': {
      byte cmdlength = serialToByte();
      byte cmd[300] = {0};
      if (cmdlength > 1 && cmdlength < 200) {
        for (int i = 0; i < (cmdlength * 2 + 3); i++) {
          cmd[i] = serialToByte();
        }
      }
      writePCF7991Reg(0x51, 8); // RF off
      delayMicroseconds(1000);
      writePCF7991Reg(0x50 | ((hysteresis & 0x1) << 1), 8); // RF on
      delayMicroseconds(350);
        
      digitalWrite(SCK_pin, LOW);
      writePCF7991Reg(0xe0, 3);
           
      unsigned int startDelay = cmd[0];
      startDelay <<= 8;
      startDelay |= cmd[1];
      
      for (unsigned int d = 0; d < startDelay; d++) {
          delayMicroseconds(100);
      }
      digitalWrite(SCK_pin, HIGH);
      delayMicroseconds(70);
      digitalWrite(SCK_pin, LOW);
      writePCF7991Reg(0x10, 8);

      for (int i = 0; i < cmdlength; i++) {
          digitalWrite(dout_pin, HIGH);
          for (int d = 0; d < cmd[i * 2 + 2]; d++) {
              delayMicroseconds(9);
          }
          digitalWrite(dout_pin, LOW);
          for (int d = 0; d < cmd[i * 2 + 3]; d++) {
              delayMicroseconds(9);
          }
      }
      
      digitalWrite(dout_pin, HIGH);
      for (int d = 0; d < cmd[cmdlength * 2 + 2]; d++) {
          delayMicroseconds(9);
      }
      digitalWrite(dout_pin, LOW);
      delayMicroseconds(3000);
      
      readTagResp();
      processManchester();
      
      Serial.println("\nEOF");
      break;
    }

    case 's': {
      while (!Serial.available()) { }
      unsigned char PageData = (Serial.read() & 0b00111111);
      Serial.print("Write ABIC Page");
      Serial.write('0' + ((PageData >> 4) & 0b00000011));
      Serial.print(" 0x");
      Serial.print((PageData & 0b00001111), HEX);
      Serial.println();

      // Copy the received target config to the local config.
      switch (PageData & 0b00110000) {
        case 0:
          AbicConf_Page0.byteval = ((PageData & 0b00111111) | 0b01000000);
          break;
        case 0b00010000:
          AbicConf_Page1.byteval = ((PageData & 0b00111111) | 0b01000000);
          break;
        case 0b00100000:
          AbicConf_Page2.byteval = ((PageData & 0b00111111) | 0b01000000);
          break;
      }
      
      writePCF7991Reg(PageData | 0b01000000, 8);
      Serial.println("EOF");
      break;
    }
    
    case 't': {
      tester();
      Serial.println("\nEOF");
      break;
    }
    
    case 'v': {
      Serial.print("Hitager version:");
      Serial.print(hitagerVersion);
      Serial.println("\nEOF");
      break;
    }

    case 'w': {
      Serial.println("Set VVDI delay");
      byte delays[3] = {0};
      for (int i = 1; i >= 0; i--) {
        delays[i] = serialToByte();
      }
      int mydelay = *((int *)delays);
      Serial.println(mydelay);
      vvdiDelay += mydelay;
      Serial.println(vvdiDelay);
      Serial.println("EOF");
      break;
    }

    case 'x': {
      Serial.println("Pulse 1 delay adjust");
      delay_1 = serialToByte() & 0xff;
      Serial.print("RESP:\n");
      Serial.print(delay_1, HEX);
      Serial.println("\nEOF");
      break;
    }
    
    case 'z': {
      Serial.println("Pulse 0 delay adjust");
      delay_0 = serialToByte() & 0xff;
      Serial.print("RESP:\n");
      Serial.print(delay_0, HEX);
      Serial.println("\nEOF");
      break;
    }
  }
  while (Serial.available() > 0) {
    byte dummyread = Serial.read();
  }
}

void superInit() {
  byte authcmd[] = {0x58, 0x48, 0x4F, 0x52, 0x53, 0x45};
  byte authcmd2[] = {0x07, 0x2F};
  byte authcmd3[] = {0x73, 0x72};
  
  writeToTag(authcmd, 0x30);
  writePCF7991Reg(0xe0, 3);
  delayMicroseconds(20000);
  digitalWrite(SCK_pin, HIGH);
  writeToTag(authcmd2, 0x10);
  writePCF7991Reg(0xe0, 3);
  
  for (unsigned long d = 0; d < vvdiDelay; d++)
    delayMicroseconds(2);
  
  digitalWrite(SCK_pin, HIGH);
  communicateTag(authcmd3, 0x10);
  Serial.println("\nEOF");
}

byte serialToByte() {
  byte retval = 0;
  while (Serial.available() < 2) { }
  for (int i = 0; i < 2; i++) {
    retval <<= 4;
    byte raw = Serial.read();
    if (raw >= '0' && raw <= '9')
      retval |= raw - '0';
    else if (raw >= 'A' && raw <= 'F')
      retval |= raw - 'A' + 10;
    else if (raw >= 'a' && raw <= 'f')
      retval |= raw - 'a' + 10;
  }
  return retval;
}

void printRegister(byte addr, byte val) {
  Serial.print("Read addr 0x");
  Serial.print(addr, HEX);
  Serial.print(" value 0x");
  Serial.print(val, HEX);
  Serial.println("");
}

void tester() {
  isrCnt = 0;
  START_TIMER;
  attachInterrupt(digitalPinToInterrupt(din_pin), pin_ISR, CHANGE);
  byte phase = readPCF7991Reg(0x08);
  STOP_TIMER;
  Serial.print("Measured phase:");
  Serial.println(phase, HEX);
  Serial.print("ISRcnt:");
  Serial.println(isrCnt, HEX);
  detachInterrupt(digitalPinToInterrupt(din_pin));
}

byte readPCF7991Reg(byte addr) {
  byte readval = 0;
  writePCF7991Reg(addr, 8);
  delayMicroseconds(500);
  readval = readPCF991Response();
  return readval;
}

void adapt(int offset) {
  byte phase = readPCF7991Reg(0x08);  // Read phase
  Serial.print("Measured phase:");
  Serial.println(phase, HEX);
  byte samplingT = (0x3f & (2 * phase + offset));
  Serial.print("adapt samplingT:");
  Serial.println(samplingT, HEX);
  byte readval = readPCF7991Reg((1 << 7) | samplingT); // Set Sampling Time + cmd
  Serial.print("adapt readval:");
  Serial.println(readval, HEX);
}

byte readPCF991Response() {
  byte _receive = 0;
  for (int i = 0; i < 8; i++) {
    digitalWrite(SCK_pin, HIGH);
    delayMicroseconds(50);
    bitWrite(_receive, 7 - i, digitalRead(din_pin));
    digitalWrite(SCK_pin, LOW);
    delayMicroseconds(50);
  }
  return _receive;
}

void writeToTag(byte *data, int bits) {
  int bytes = bits / 8;
  int rembits = bits % 8;
  int bytBits = 8;
  
  digitalWrite(SCK_pin, LOW);
  writePCF7991Reg(0x19, 8);
  digitalWrite(dout_pin, LOW);
  delayMicroseconds(20);
  digitalWrite(dout_pin, HIGH);
  delayMicroseconds(20);
  digitalWrite(dout_pin, LOW);
  
  for (int by = 0; by <= bytes; by++) {
    if (by == bytes)
      bytBits = rembits;
    else
      bytBits = 8;
      
    for (int i = 0; i < bytBits; i++) {
      if (bitRead(data[by], 7 - i)) {
        for (int j = 0; j < delay_1; j++)
          delayMicroseconds(10);
        digitalWrite(dout_pin, HIGH);
        delayMicroseconds(10);
        digitalWrite(dout_pin, LOW);
      } else {
        for (int j = 0; j < delay_0; j++)
          delayMicroseconds(10);
        digitalWrite(dout_pin, HIGH);
        delayMicroseconds(10);
        digitalWrite(dout_pin, LOW);
      }
    }
  }
  
  // End of transmission
  if (decodemode == 0)
    delayMicroseconds(1200);
  else
    delayMicroseconds(400); // For biphase decode, a shorter delay is used.
}

void readTagResp() {
  writePCF7991Reg(0xe0, 3);
  START_TIMER;
  // Reset pulse counters and timing variables
  isrCnt = 0;
  capturedone = 0;
  bitsCnt = 0;
  
  // Set the starting time for pulse measurement
  lastTime = micros();
  attachInterrupt(digitalPinToInterrupt(din_pin), pin_ISR, CHANGE);
  
  // Instead of using a hardware timer overflow, wait a fixed period.
  delay(10); // Adjust this delay as necessary to capture all pulses
  
  // Mimic the original “last pulse” adjustment.
  if (isrCnt < 400 && isrCnt > 3) {
    isrtimes_ptr[isrCnt - 1] = isrtimes_ptr[isrCnt - 2] + 201;
    isrCnt++;
  }
  
  STOP_TIMER;
  detachInterrupt(digitalPinToInterrupt(din_pin));
}

void communicateTag(byte *tagcmd, unsigned int cmdLengt) {
  isrtimes_ptr = isrtimes;
  writeToTag(tagcmd, cmdLengt);
  readTagResp();
  Serial.print("ISRcnt:");
  Serial.println(isrCnt, HEX);
  if (debug) {
    for (int s = 0; s < isrCnt; s++) {
      Serial.print(isrtimes[s]);
      Serial.print(", ");
    }
    Serial.println();
  }
  if (decodemode == 0)
    processManchester();
  else
    processcdp();
}

void writePCF7991Reg(byte _send, uint8_t bits) {
  pinMode(dout_pin, OUTPUT);
  digitalWrite(dout_pin, LOW);
  delayMicroseconds(50);
  digitalWrite(SCK_pin, HIGH);
  delayMicroseconds(50);
  digitalWrite(dout_pin, HIGH);
  delayMicroseconds(50);
  digitalWrite(SCK_pin, LOW);
  
  for (uint8_t i = 0; i < bits; i++) {
    delayMicroseconds(50);
    digitalWrite(dout_pin, bitRead(_send, 7 - i));
    delayMicroseconds(50);
    digitalWrite(SCK_pin, HIGH);
    delayMicroseconds(50);
    digitalWrite(SCK_pin, LOW);
  }
}

void pin_ISR() {
  // Use micros() to compute the elapsed time since the last interrupt.
  unsigned long currentTime = micros();
  unsigned int travelTime = (unsigned int)(currentTime - lastTime);
  lastTime = currentTime;
  
  // Mark the edge type: for a rising edge, clear the LSB; for falling, set it.
  if (digitalRead(din_pin)) {
    travelTime &= ~1;  // Rising edge: ensure even value.
  } else {
    travelTime |= 1;   // Falling edge: mark with odd value.
  }
  
  isrtimes_ptr[isrCnt] = travelTime;
  if (isrCnt < 400)
    isrCnt++;
}

void processManchester() {
  int bitcount = 0;
  int bytecount = 0;
  int mybytes[10] = {0};
  int lead = 0;
  int errorCnt = 0;
  int state = 1;
  int start = 0;
  int pulsetime_fil = 0;
  
  for (start = 0; start < 10; start++) {
    if (isrtimes_ptr[start] < 55)
      break;
  }
  start += 3;
  
  // Adapt the filtered pulse time using the first few pulses.
  int pulsetime_accum = 0;
  for (uint8_t i = start + 1; i < start + 4; i++) {
      pulsetime_accum += isrtimes_ptr[i];
  }
  pulsetime_fil = pulsetime_accum / 4;
  
  if ((isrtimes_ptr[start] & 1) == 0) {
    start--;
    pulsetime_fil = fir_filter(pulsetime_fil, isrtimes_ptr[start]);
  }
  
  for (int i = start; i < isrCnt; i++) {
    int pulsetime_thresh = 55;
    int travelTime = isrtimes_ptr[i];
    
    if (travelTime & 1) {  // high pulse
      if (travelTime > pulsetime_thresh) {
        if (state) {
          state = 1;
          if (lead < 4) {
            lead++;
          } else {
            mybytes[bytecount] |= (1 << (7 - bitcount++));
          }
        } else {
          Serial.print("X");
          if (bytecount < 1)
            errorCnt++;
        }
      } else {
        pulsetime_fil = fir_filter(pulsetime_fil, travelTime);
        if (state) {
          state = 0;
          if (lead < 4)
            lead++;
          else {
            mybytes[bytecount] |= (1 << (7 - bitcount++));
          }
        } else {
          state++;
        }
      }
    } else {
      if (travelTime > pulsetime_thresh) {
        if (state) {
          state = 1;
          bitcount++;
        } else {
          Serial.print("X");
          if (bytecount < 1)
            errorCnt++;
        }
      } else {
        pulsetime_fil = fir_filter(pulsetime_fil, travelTime);
        if (state) {
          state = 0;
          bitcount++;
        } else {
          state++;
        }
      }
    }
    
    if (bitcount > 7) {
      bitcount = 0;
      bytecount++;
    }
    if (travelTime > 80) {
      if (bitcount > 0)
        bytecount++;
      break;
    }
  }
  
  char hash[20];
  Serial.print("\nRESP:");
  if (errorCnt || bytecount == 0)
    Serial.println("NORESP");
  else {
    for (int s = 0; s < bytecount && s < 10; s++) {
      sprintf(hash, "%02X", mybytes[s]);
      Serial.print(hash);
    }
  }
  Serial.println();
}

void processcdp() {
  int bitcount = 0;
  int bytecount = 0;
  int mybytes[10] = {0};
  int lead = 0;
  int errorCnt = 0;
  int state = 1;
  int start = 0;
  
  start += 6;
  if ((isrtimes_ptr[start] & 1) == 0)
    start++;
  
  for (int i = start; i < isrCnt; i++) {
    int travelTime = isrtimes_ptr[i];
    if (travelTime > 45) {
      mybytes[bytecount] |= (1 << (7 - bitcount++));
    } else {
      bitcount++;
      i++;
    }
    if (bitcount > 7) {
      bitcount = 0;
      bytecount++;
    }
    if (travelTime > 200) {
      if (bitcount > 0)
        bytecount++;
      break;
    }
  }
  
  char hash[20];
  Serial.print("\nRESP:");
  if (errorCnt || bytecount == 0)
    Serial.println("NORESP");
  else {
    for (int s = 0; s < bytecount && s < 10; s++) {
      sprintf(hash, "%02X", mybytes[s]);
      Serial.print(hash);
    }
  }
  Serial.println();
}

unsigned int fir_filter(unsigned int pulse_fil_in, unsigned int current_pulse) {
  unsigned int pulse_fil_out;
  if (((int)pulse_fil_in - (int)current_pulse) > 3)
    pulse_fil_out = pulse_fil_in - 1;
  else if (((int)current_pulse - (int)pulse_fil_in) > 3)
    pulse_fil_out = pulse_fil_in + 1;
  else
    pulse_fil_out = pulse_fil_in;
  return pulse_fil_out;
}
