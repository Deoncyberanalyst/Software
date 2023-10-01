// C++ code
const int LED_R = 11;
const int LED = 13;
const int LED_G = 12;
const int LED_R2 = 10;
const int TMP = A1;
const int BUTTON = 3;
const int PIR = 2;
const int IR = 4;
volatile bool pirState = 0;
volatile bool ledState = 0;
volatile bool ledState2 = 0;

const int tl_load = 0;
uint16_t tl_comp = 31250;
//Max value of Timer0 is 65,536, so this compare match value is valid.

//unint64_t means,
//u = unsigned, meaning it can only represent positive values, or zero
//int means integer
//32 refers to the bits. It means that this integer will 
//take up 32 bits of memory. And therefore the max value it can
//hold is (2^32)

void setup()
{
    pinMode(LED, OUTPUT);
    pinMode(LED_G, OUTPUT);
    pinMode(LED_R, OUTPUT);
    pinMode(TMP, INPUT);
    pinMode(BUTTON, INPUT);
    pinMode(LED_R2, OUTPUT);
    pinMode(PIR, INPUT);
    Serial.begin(9600);

    attachInterrupt(digitalPinToInterrupt(BUTTON), ISR_action, RISING);
    attachInterrupt(digitalPinToInterrupt(PIR), ISR_action2, RISING);

    pinMode(4, INPUT);

    //16MHz
    //Prescaler 
    //Timer1


    PCICR |= B00000100;   
    PCMSK2 |= B00010000;
  
  //Reset Tuner1 Cibtrik Reg A
  //TCCR1A and 1B to 0 to make sure everything is clear
  	TCCR1A = 0;
    TCCR1B = 0;

  //SET CTC mode
  // that occur in one second. It is measured in Hertz (Hz).
  //CTC (Clear Timer on Compare Match) Mode: The timer counts up until it matches the value in the compare register, then resets to zero. This mode is often used for generating accurate timing intervals.
 	TCCR1B &= ~(1 << WGM13);
  	TCCR1B |= (1 << WGM12);  //shifts the value 1 left by the number of positions indicated by the value of the CS11 bit
  
  //Set prescaler to 1024
   	TCCR1B |= (1 << CS12);
	TCCR1B &= ~(1 << CS11);
   	TCCR1B |= (1 << CS10);
  //Notes to help me understand:
  ///1 << CS12) performs a left shift of the
  //value 1 by the number of positions indicated by the value of 
  //CS12. This sets the bit at the position specified
  //by CS12 to 1, while other bits remain unchanged.
  
  
  	TCNT1 = 0;
  	OCR1A = tl_comp;
  
  //Enable Timer1 Compare interrupts  
    //TIMSK register - Timer/Counter Interrupt Mask Register,
  	TIMSK1 = (1 << OCIE1A);
  
  //Enable global interrupts
  	sei();
  

  //Timer0 = 8 bits
  //Timer1 =  16 bits = 2^16 = 650000
  //Timer2 = 8 bits
  
}

void loop()
{
 // Serial.println("Temperature is:" + String(analogRead(TMP)) + "\n");
  tl_comp = ((65000)-(180*analogRead(TMP)));
  OCR1A = tl_comp;
  Serial.println(analogRead(TMP));
  delay(500);
}


void ISR_action(){
  	analogRead(TMP)>200 ? digitalWrite(LED, HIGH) : digitalWrite(LED, LOW);
  	analogRead(TMP)>200 ? Serial.println("BLUE LED ON!") : Serial.println("BLUE LED OFF!");
}

void ISR_action2(){
  pirState = !pirState;
  pirState ? Serial.println("GREEN LED ON!") : Serial.println("GREEN LED OFF!");
  digitalWrite(LED_G, pirState);
}

ISR(PCINT2_vect){ 
  ledState = !ledState;
  digitalWrite(LED_R, ledState);
  ledState ? Serial.println("RED1 LED ON!") : Serial.println("RED1 LED OFF!");
}

ISR(TIMER1_COMPA_vect){
  ledState2 = !ledState2;
  digitalWrite(LED_R2, ledState2);
}