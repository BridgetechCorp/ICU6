/******************************************************************************/
/* Files to Include                                                           */
/******************************************************************************/

/* Device header file */
#if defined(__XC16__)
    #include <xc.h>
#elif defined(__C30__)
    #if defined(__dsPIC33E__)
    	#include <p33Exxxx.h>
    #elif defined(__dsPIC33F__)
    	#include <p33Fxxxx.h>
    #endif
#endif
#include <xc.h>
#include <string.h>
#include <stdint.h>        /* Includes uint16_t definition                    */
#include <stdbool.h>       /* Includes true/false definition                  */

#include "system.h"        /* System funct/params, like osc/peripheral config */
#include "user.h"          /* User funct/params, such as InitApp              */
#include "ecan.h"
#include "oscillator.h"
#include "CanIrma.h"
#include "timer.h"
#include "uart.h"
#include "mDot.h"


unsigned long GetCANAddressFromBuffer( void );
// DSPIC33EP256MU806 Configuration Bit Settings


// FGS
#pragma config GWRP = OFF               // General Segment Write-Protect bit (General Segment may be written)
#pragma config GSS = OFF                // General Segment Code-Protect bit (General Segment Code protect is disabled)
#pragma config GSSK = OFF               // General Segment Key bits (General Segment Write Protection and Code Protection is Disabled)

// FOSCSEL
#pragma config FNOSC = PRI              // Initial Oscillator Source Selection bits (Primary Oscillator (XT, HS, EC))
//#pragma config FNOSC = FRC              // Initial Oscillator Source Selection bits (Primary Oscillator (XT, HS, EC))
#pragma config IESO = ON                // Two-speed Oscillator Start-up Enable bit (Start up device with FRC, then switch to user-selected oscillator source)

// FOSC
#pragma config POSCMD = HS              // Primary Oscillator Mode Select bits (HS Crystal Oscillator Mode)
#pragma config OSCIOFNC = OFF           // OSC2 Pin Function bit (OSC2 is clock output)
#pragma config IOL1WAY = ON             // Peripheral pin select configuration (Allow only one reconfiguration)
#pragma config FCKSM = CSDCMD           // Clock Switching Mode bits (Both Clock switching and Fail-safe Clock Monitor are disabled)

// FWDT watchdog timeout is about 33 seconds
#pragma config WDTPOST = PS8192      // Watchdog Timer Postscaler bits (1:32,768)
#pragma config WDTPRE =  PR128  //PR128           // Watchdog Timer Prescaler bit (1:128)
#pragma config PLLKEN = ON              // PLL Lock Wait Enable bit (Clock switch to PLL source will wait until the PLL lock signal is valid.)
#pragma config WINDIS = OFF             // Watchdog Timer Window Enable bit (Watchdog Timer in Non-Window mode)
#pragma config FWDTEN = ON             // Watchdog Timer Enable bit (Watchdog timer enabled/disabled by user software)

// FPOR
#pragma config FPWRT = PWR128           // Power-on Reset Timer Value Select bits (128ms)
#pragma config BOREN = ON               // Brown-out Reset (BOR) Detection Enable bit (BOR is enabled)
#pragma config ALTI2C1 = OFF            // Alternate I2C pins for I2C1 (SDA1/SCK1 pins are selected as the I/O pins for I2C1)

// FICD
#pragma config ICS = PGD3               // ICD Communication Channel Select bits (Communicate on PGEC3 and PGED3)
#pragma config RSTPRI = PF              // Reset Target Vector Select bit (Device will obtain reset instruction from Primary flash)
#pragma config JTAGEN = OFF             // JTAG Enable bit (JTAG is disabled)

// FAS
#pragma config AWRP = OFF               // Auxiliary Segment Write-protect bit (Aux Flash may be written)
#pragma config APL = OFF                // Auxiliary Segment Code-protect bit (Aux Flash Code protect is disabled)
#pragma config APLK = OFF               // Auxiliary Segment Key bits (Aux Flash Write Protection and Code Protection is Disabled)


typedef enum
{
    INIT_STATE = 0,      
    RUNNING,
    LORA_TRANSMIT,
    LORA_WAIT
       
} my_state_t;

my_state_t state = INIT_STATE;

/******************************************************************************/
/* Global Variable Declaration                                                */
/******************************************************************************/
// declare a holder for the latest incoming CAN message

#define MAX_MESSAGES 10
mID message;
mID messages[MAX_MESSAGES];
mID txMessage;

#define MAX_SENSORS  6

bool DebugMode = FALSE;

int timeout;
int SensorCount = 0;

unsigned long SensorAddr[MAX_SENSORS] = {0,0};//,0,0,0,0,0,0};
int FuncArea[MAX_SENSORS] = {0,0};//,0,0,0,0,0,0};
uint16 PeopleCountIn[MAX_SENSORS]  = {0,0};//,0,0,0,0,0,0};
uint16 PeopleCountOut[MAX_SENSORS] = {0,0};//,0,0,0,0,0,0};
int SensorStatus[MAX_SENSORS] = {0,0};//,0,0,0,0,0,0};
int Sabotaged[MAX_SENSORS]  = {0,0};//,0,0,0,0,0,0};


extern boolean flashblink;
extern boolean slowblink;

boolean LostSensor = false;

//boolean SensorEnabled = true;
boolean NeedToSendData = false;
unsigned long sensorCanAddress;

int system_disi_level;

void ClearAllMessages( void );

int (*RunLevel5Main)(void);


void InitGlobalVariables()
{
    int i;
   
    SensorCount = 0;
    LostSensor = false;    
    NeedToSendData = false;
    sensorCanAddress = 0;
    for (i=0;i<MAX_SENSORS;i++)
    {
        SensorAddr[i] = 0;
        FuncArea[i] = 0;
        PeopleCountIn[i]  = 0;
        PeopleCountOut[i] = 0;
        SensorStatus[i] = 0;
        Sabotaged[i]  = 0;  
    }
}

int GetCanAddrIndex( unsigned long addr )
{
   int i;   
    for (i=0;i<MAX_SENSORS;i++)
    {
      if (SensorAddr[i] == addr)
          return i;  
    }
    return 0; // give something valid if we have nothing 
}

// get the index of the can address message
int GetCanIndex( void )
{
    int i;
    unsigned long ca = GetCANAddressFromBuffer();
    for (i=0;i<MAX_SENSORS;i++)
    {
      if (SensorAddr[i] == ca)
          return i;  
    }
    return 0; // give something valid if we have nothing
}


int process_can_message(mID messageType)
{  // Accepts a CAN message from the PC and organizes it for the slave to read/write
   // copy the message based on first byte in data.
  int index = 0;
  
  index = messageType.data[0];
  if (index < MAX_MESSAGES )
  { 
     memcpy(&messages[index],&messageType,sizeof(mID));
     return index; // return the last message id 
  }  
    // something went very bad, lets clear out all messages
  ClearAllMessages();
  return 0;    
}


/* i.e. uint16_t <variable_name>; */
// *** CheckForCanMessages ****************************************************************//
// return 1 if a can message was received, 0 otherwise
int CheckForCanMessages( void )
{ // Determines whether or not a CAN message is waiting in the buffer
  if (IsCanCommandAvailable())
  {   
    can_messages_rcvd++;
    //	get CAn message out of CAN buffer
    retrieve_can_message(&message);
    // put messages into array based on first byte
    return process_can_message(message);
  }
  return 0;
}




int RunLevel5MainFunc(void);

/******************************************************************************/
/* Main Program                                                               */
/******************************************************************************/


unsigned long GetCANAddressFromBuffer( void )
{  
   return (unsigned long) messages[1].id & 0x000FFFFF;
}

void FlushCanMessages( void )
{
    // flush all pending CAN message
    while (IsCanCommandAvailable())
    {
       retrieve_can_message(&message);
    }
    ClearAllMessages();
}

void ClearAllMessages( void )
{
    int i; //,j;

    for (i=0;i<MAX_MESSAGES;i++)
    {
        memset(&messages[i] ,0xFF ,sizeof(mID));      
    }
}


// if readcmd == 0 then read the door count
// if readcmd == 1 then read the sensor status,
// if readcmd == 2 then read the functional area
// if readcmd == 3 then read nothing
// return how manu CAN messages were found
void ReadCount( int readcmd )
{
//    static boolean MsgFlop = false;
    int k;//,j;
   // int functionArea;
    
 // Check to see if a status message arrived, but no status is available
    if (CheckForCanMessages() == 1)
    {
       if ((messages[1].data[4] == 'S') &&
           (messages[1].data[5] == 0x30) &&
           (messages[1].data[6] == 'G') &&
           (messages[1].data[7] == 0x00) ) // no status message waiting
       {
           // Sensor Status Query Response
               SensorStatus[GetCanIndex()] = 0; // no status is available
       }
       else if ((messages[1].data[4] == 'c') &&
                (messages[1].data[5] == 0x10) &&
                (messages[1].data[6] == 'B') )
       {           
            // this is a BROADCAST SCAN RESPONSE
           //
           sensorCanAddress = GetCANAddressFromBuffer();
           for (k=0;k<MAX_SENSORS;k++)
           {
               if (SensorAddr[k] == sensorCanAddress)
               {
                   break; // we already have this address
               }
               else if (SensorAddr[k] == 0 )
               {
                  SensorAddr[k] = sensorCanAddress;
                  FuncArea[k] = 0;
                  return;
               }
           }
       }
       else if  ((messages[1].data[4] == 'D') &&
                 (messages[1].data[5] == 0x30) &&
                 (messages[1].data[6] == 'S') )
       {
         // Door State Count Message (who requested this?)
            // this is sent when DoorOpen and DoorClose are requested
       }
       else if  ((messages[1].data[4] == 'C') &&
                 (messages[1].data[5] == 0x60) &&
                 (messages[1].data[6] == 'G') )
       {
         // Count Data Query Response
           k=3;
       }
       else if  ((messages[1].data[4] == 'C') &&
                 (messages[1].data[5] == 0x60) &&
                 (messages[1].data[6] == 'R') )
       {
         // Count Data Query Response
           k=3;
       }
       else
       {
           k=23; // what message is this
           k=4;
       }
     }


    // check to see if status arrived, and grab the
    // Sensor Status if available
    if (CheckForCanMessages() == 2)
    {
           if ((messages[1].data[4] == 'S') &&
               (messages[1].data[5] == 0x30) &&
               (messages[1].data[6] == 'G') )
           {
               SensorStatus[GetCanIndex()] = messages[2].data[1];
               if (SensorStatus[GetCanIndex()])
               {
                   // rats we have a sabotage message here
                   //
                   k=4;
               }
               //Sabotaged = 1;
               ClearAllMessages();
           }
           else if ((messages[1].data[4] == 'S') &&
               (messages[1].data[5] == 0x31) &&
               (messages[1].data[6] == 'G') )
           {               
               FuncArea[GetCanIndex()] = messages[2].data[1]; 
                         
           }
           else
           {
               // unknown 2 slot message
           }
     }

     // Check for the people count
    if (CheckForCanMessages() == 3)
    {
        if ((messages[1].data[4] == 'C') &&
            (messages[1].data[5] == 0x60) &&
            (messages[1].data[6] == 'G') )
        {
            k = GetCanIndex(); 
            // CMB: this next line is a test
            if ( FuncArea[k]==0 )
            {
                FuncArea[k] = messages[1].data[7]; 
            }
                       
            PeopleCountIn[k] =  ((uint16)messages[2].data[7])<<8 | (uint16)messages[2].data[6];
            PeopleCountOut[k] = ((uint16)messages[3].data[2])<<8 | (uint16)messages[3].data[1];            
        }  
        else
        {
             // unknown 3 slot message
            k=4;
            k=34;
        }
        ClearAllMessages();
    }
    if (CheckForCanMessages() == 4) 
        ClearAllMessages();
    
    if (CheckForCanMessages() == 5) 
        ClearAllMessages();
    
    if (CheckForCanMessages() == 6) 
        ClearAllMessages();
   
    if ( GetTimerInterrupts() > 50) // every 50 milliseconds read count
    {
        SetTimerInterrupts(0);
        
        switch (readcmd)
        {
            case 0: // read the door count
                ClearAllMessages(); // this may be bad
                SendIRMACounterStateRequest();          
                break;
            case 1: // read the sensor status
                // Update the Sensor Status, to decide if we are sabotaged or not
                //  NOTE: The Sensor Status is only updated when the door is closed
                // and was previously opened for at least 4 seconds
                ClearAllMessages(); // this may be bad
               // SendIRMASensorStateRequest();
                break;
            case 2: // read the functional area
                ClearAllMessages();
               // SendFunctionAreaStatusQuery();
                break;
            case 3: // read nothing
                ClearAllMessages();
                break; 
            case 4: // read count with new method
                ClearAllMessages(); // this may be bad
               // SendIRMACounterStateRequest2(1,2); 
                break; 
        }            
    }
}


boolean ExternalResetPressed( void )
{
    return (Input2Pressed); // || Input2Pressed);
}


boolean ExternalEnablePressed( void )
{
    return (Input1Pressed); // || Input4Pressed);
}

void EstablishLoraModuleConnection( void )
{
    // char c;
    //LORA_RESET = 0; //  Disable the LORA module to reset it
     // give 100 milliseconds for the mDot to shut down
    SetWaitDelay(100);    while (GetWaitDelay()!=0);       
   // LORA_RESET = 1; //  Enable the LORA module    
     // give 600 milliseconds for the mDot to come to life
    SetWaitDelay(600); while (GetWaitDelay()!=0);
          
    // clear any existing data in the uart
    ClearUartReceiveBuffer();
    InitMDot(); 
    return;           
}


void EstablishCanModuleConnections( void )
{
    int k;
  // establish communications to CAN bus
    while (true) 
    {
        CAN_LED = 1;
        LORA_LED = 1;
         // First Get the CAN address
        SendIRMAGetCanAddress();  
        SetWaitDelay(30);  while (GetWaitDelay()!=0);
        for (k=0;k<MAX_SENSORS;k++)
            ReadCount(3); // get CAN ids from all possiblemessages
      
        if (SensorAddr[0] == 0)
        {        
            SendNoCanErrorMessage();
            // No CAN bus was detected, keep trying forever
            CAN_LED = 0;
            LORA_LED = 1;
             SetWaitDelay(2000);  while (GetWaitDelay()!=0);
        }
        else
        {
            SensorCount = 0;
            for (k=0;k<MAX_SENSORS;k++)
            {
                if (SensorAddr[k] != 0)
                {
                    SensorCount++;                
                }
            }
            break; // Yeah we got at least one address
        }
    }   
}


  int GetFunctionArea(unsigned long addr)
  {         
    int index = GetCanAddrIndex(addr);
    int fa=0;
    
    // scan all function areas looking for a valid response to
    // establish which function area the sensor is on.
    while (FuncArea[index] == 0)
    {
        for (fa=0;fa<MAX_SENSORS+1;fa++)
        {
            SendIRMACounterStateRequest2(addr,fa);
            SetWaitDelay(20);    
            while (GetWaitDelay()!=0);       
            ReadCount(3);
            if (FuncArea[index] != 0) 
                return FuncArea[index];
        }
    }
    return 0;
  }
  
  void OpenAllDoors( void )
  {
    int addr;
    for (addr=0;addr<MAX_SENSORS;addr++)
    {
        if (SensorAddr[addr] != 0)
        {
           SendIRMASetDoorsOpen(SensorAddr[addr],FuncArea[addr]);
           SetWaitDelay(100);    
           while (GetWaitDelay()!=0);  
           ReadCount(2); // after each CAN command, we need to read response
        }
    }
  }
  
void CloseAllDoors( void )
{
   int addr;
   for (addr=0;addr<MAX_SENSORS;addr++)
   {
      if (SensorAddr[addr] != 0)
      {
         SendIRMASetDoorsClose(SensorAddr[addr],FuncArea[addr]); // Close doors to stop counting
         SetWaitDelay(100);    
         while (GetWaitDelay()!=0);  
         ReadCount(2); // after each CAN command, we need to read response
      }
   }
}  

void ReadAllDoorCounts( void )
{
   int addr;
  
   for (addr=0;addr<MAX_SENSORS;addr++)
   {
      if (SensorAddr[addr] != 0)
      {
         can_messages_rcvd = 0;
         SendIRMACounterStateRequest2(SensorAddr[addr],FuncArea[addr]);        
         SetWaitDelay(10);    
         while (GetWaitDelay()!=0); 
         ReadCount(3); // fetch the can data
         if (can_messages_rcvd == 0)
         {
             // oh oh, when I requested the Count, I did not receive
             // back a CAN message.
             // tell main loop to try to reconnect.
            LostSensor = true;
            // clear out this sensor, so we don't transmit anymore
            SensorAddr[addr]=0;
            FuncArea[addr]=0;
            SensorCount--;
         }
      }
   }
}

  
    
void GetAllFunctionAreas( void )
{
    int addr;
    for (addr=0;addr<MAX_SENSORS;addr++)
    {
        if (SensorAddr[addr] != 0)
        {          
            FuncArea[addr] = GetFunctionArea(SensorAddr[addr]);
        }              
    }  
}
  
   
 void ClearCountHistory( void )
 {
     int k;
     for (k=0;k<MAX_SENSORS;k++)
     { 
        PeopleCountIn[k]  = 0;
        PeopleCountOut[k] = 0;             
     }
 }
 
 // tell the LORA server the startup messages when we bootup.
 void SendStartupLoraMessages( void )
 {
   int addr;
   for (addr=0;addr<MAX_SENSORS;addr++)
   {
      if (SensorAddr[addr] != 0)
      {
         SendLoraPacket( 0xFF, 0, 0, FuncArea[addr] );        
         SetWaitDelay(1500);    
         while (GetWaitDelay()!=0);  
      }
   }  
 }
 
int AnyDoorCount( void )
{
   int sum = 0;
   int addr;
   for (addr=0;addr<MAX_SENSORS;addr++)
   {
      if (SensorAddr[addr] != 0)
      {
          sum += PeopleCountIn[addr];
          sum += PeopleCountOut[addr];                   
      }
   }  
   return sum; // no count changed since last time
}

bool AnyCountsGreaterThanFF( void )
{
   int addr;
   for (addr=0;addr<MAX_SENSORS;addr++)
   {
      if (SensorAddr[addr] != 0)
      {
          if ( (PeopleCountIn[addr]  >= 255) ||
               (PeopleCountOut[addr] >= 255))
          {           
            return true; // the count has changed since last time
          }         
      }
   }  
   return false; // no count changed since last time   
}

void SendAllSensorDataToModem( void )
{
    int addr;
    int index;
    int incnt[MAX_SENSORS+1];
    int outcnt[MAX_SENSORS+1];  
    int functionarea=0;  
    
    // first initialize the data
    for (addr=0;addr<MAX_SENSORS;addr++)
    {
        incnt[addr]  = -1;
        outcnt[addr] = -1;
    }
    
    // now sort the data into order
    for (addr=0;addr<MAX_SENSORS;addr++)
    {
        if (SensorAddr[addr] != 0)
        {
            index = FuncArea[addr];
            if ((PeopleCountIn[addr] > incnt[index]) || (incnt[index] == -1) )
                incnt[index]  = PeopleCountIn[addr];
            if ((PeopleCountOut[addr]>outcnt[index]) || (outcnt[index] == -1) )
                outcnt[index] = PeopleCountOut[addr];
        }
    }
    
   //ClearUartReceiveBuffer(); // clear any old data out from uart buffer
   putch(0xc5); // param id .. start of block
   putch(0xFF); 
   for (index=0;index<MAX_SENSORS;index++)
   {      
       if (incnt[index] != -1 )
       {
         functionarea = (index-1)*2+1;         
         putch(0xbf);
         putch(functionarea);
         putch(incnt[index]);
         putch(0xbf);
         putch(functionarea+1);
         putch(outcnt[index]);                
       }     
   }
}

void ClearAllDoorCounts( void )
{
    unsigned long addr;
    int attempts = 0;
        
    while (AnyDoorCount() > 0 )
    {  
        // Do not allow us to get stuck in this loop forever
        // if we cant reset one of the counts, we need to bail
        // and reset processor
       if (attempts++ > 10)
       {
            asm("reset"); // reset the processor
       }
       
       for (addr=0;addr<MAX_SENSORS;addr++)
       {        
          if (SensorAddr[addr] != 0)
          {    
              if ((PeopleCountIn[addr] > 0) || (PeopleCountOut[addr]>0))
              {
                SendIRMACountDataReset( SensorAddr[addr], (unsigned char)FuncArea[addr]  );           
                SetWaitDelay(10);  while (GetWaitDelay()!=0);                  
                ReadCount(4);
              }
          }
       }
       ReadAllDoorCounts();
       SetWaitDelay(100);  while (GetWaitDelay()!=0);
    }   
    
    
}

int GetTimeout()
{
    int time;
    
    time = 166; // start at 20 seconds
    if (!Input1Pressed) time += 41;  // add 5 seconds per switch
    if (!Input2Pressed) time += 41;
    if (!Input3Pressed) time += 41;
    if (!Input4Pressed) time += 41;
    if (!Input5Pressed) time += 41;
    if (!Input6Pressed) time += 41;   
    
    return time;       
}


// Send C5-FF-DD-BE-xx (xx is count of how many sensors are attached)
void SendHeartBeat( void )
{
   int index;
   unsigned char senscount = 0;
   putch(0xc5); // param id .. start of block
   putch(0xFF);
   putch(0xDD); 
   putch(0xBE); 
   for (index=0;index<MAX_SENSORS;index++)
   {      
       if (FuncArea[index] != 0 )
       {
           senscount++;                      
       }     
   }
   putch(senscount);
}

void SendDebugOnResponse( void )
{
   putch(0xc5); // param id .. start of block
   putch(0xFF);
   putch(0xDD); 
   putch(0xDE); 
   putch(0x00); 
}

void SendBootupMessage( void )
{
   putch(0xc5); 
   putch(0xFF);
   putch(0xDD); 
   putch(0xB0);
   if (RCONbits.SWR) { putch(0x00);  RCONbits.SWR = 0; }
   else if (RCONbits.BOR) { putch(0x01);  RCONbits.BOR = 0; }
   else if (RCONbits.WDTO) { putch(0x02);  RCONbits.WDTO = 0; }
   else if (RCONbits.EXTR) { putch(0x04);  RCONbits.EXTR = 0; }
   else if (RCONbits.POR) { putch(0x03);  RCONbits.POR = 0; }   
   else putch(0xff);   
}

void SendErrorCode( unsigned char code )
{
   putch(0xc5); 
   putch(0xFF);
   putch(0xDD); 
   putch(0xEE);
   putch(code);
   // give time to send before a reset, etc..
  SetWaitDelay(200);   // 
  while (GetWaitDelay()!=0);
}

void SendDebugMessage( char * str)
{
  if (DebugMode)
  {
    putch(0xc5); 
    putch(0xFF);
    putch(0xDD); 
    putch(0xB0);  
    while (*str!=0)
    {
        putch(*str++);       
    }
     // give time to send uart before a reset, etc..
    SetWaitDelay(200);   // 
    while (GetWaitDelay()!=0);
  }    
}


int16_t main(void)
{    
    long SuperTimeout = 0;
    int newCount = 0;
    int lastCount = 0;
    int accumulatedTime;
    int k; 
    char c;
    int heartbeat = 0;
  
    /* Configure the oscillator for the device */
    InitOscillator();
    

    ANSELB = 0x0000;
    ANSELC = 0x0000;
    ANSELD = 0x0000;
    ANSELE = 0x0000;
    ANSELG = 0x0000;

    /* Initialize IO ports and peripherals */
    InitApp();
  
    
    // give time for sensors to come on line
    for (k=0;k<20;k++)
    {
         CAN_LED = 0;  LORA_LED=1; SetWaitDelay(100);  while (GetWaitDelay()!=0);    
         CAN_LED = 1;  LORA_LED=0; SetWaitDelay(100);  while (GetWaitDelay()!=0);    
    }
    
    InitGlobalVariables();
                 
    // find out which CAN devices are on the bus
    EstablishCanModuleConnections();  
    
    // for each matrix sensor get its function area
    GetAllFunctionAreas();
    
    CloseAllDoors(); // this will reset all of the counts             
    
    ClearAllDoorCounts();
    
    OpenAllDoors();
 
    FlushCanMessages();    
    // read a few times to initialize the PeopleCounts[]
    CAN_LED = 0;  //LED2 = 1;
    LORA_LED = 0; // turn off LORA LED                
    for (k=0;k<20;k++)
    {
       ReadAllDoorCounts();
       SetWaitDelay(10);  while (GetWaitDelay()!=0);
    }    
    CAN_LED = 1;  //LED2 = 1;
    LORA_LED = 1; // turn off LORA LED                 
    
    FlushCanMessages();
    
      
    
    // tell server that we booted up with reason
    SendBootupMessage();
     
   accumulatedTime = 0; 
   while (true)     
   {      
       ClrWdt(); // kick the watchdog timer
       
        
       // see if the modem has sent us any data
       while ( getch(&c) ) 
       {          
           switch ((unsigned char)c)
           {
               case 0xf1:
                   // A request to reboot the microcontroller from server
                   asm("reset"); // reset the processor
                   break;
               case 0xf2:
                   // Heartbeat request from server
                   SendHeartBeat();
                   break;
               case 0xf3:
                   // request from server to turn debug mode on
                   DebugMode = true;
                   SendDebugOnResponse();
                   break;               
               default:
                   SendErrorCode(2); // unknown command code
                   break;               
           }
       }
       
       
       timeout = GetTimeout(); // get time between sending data
       
       ReadAllDoorCounts();  // read latest door counts from all function areas
       
       
         // ack we lost a sensor
       if (LostSensor)
       {
           SendErrorCode(0);
           SendDebugMessage("Lost Sensor");
           CAN_LED = 0;
           LORA_LED = 0;
           SetWaitDelay(200);    
           while (GetWaitDelay()!=0);
           LostSensor=false;          
           asm("reset"); // reset the processor
       }
       
       // if we have less than 6 sensors, then after 6 hours reset processor 
       if (SensorCount<1)
       {
          SuperTimeout++;
          if (SuperTimeout > 144000) // every six hours
          {
             SendDebugMessage("6 hour timeout");
             SendErrorCode(1);
             asm("reset"); // reset the processor to check for sensor  
          }
       }
       else
       {
           SuperTimeout = 0;
       }
       
       newCount = AnyDoorCount();       
       // check if any counts have changed
       if (lastCount != newCount)
       {
           lastCount = newCount;    
           // turn on led for 100 msec so we know count changed
           CAN_LED = 0; 
           
           // start the 15 second countdown before we send messages
           NeedToSendData = true;        
       }
       
       // accumulate time if we need to send data
       if (NeedToSendData == true)
       {
           LORA_LED = 0; // flash LORA_LED while waiting to send
           accumulatedTime++; // increment the time in 100msec increments  
       }
       
       // always wait 100 msec before we read sensor again
       //CAN_LED=0; // always blink can led so we know main loop is running
       SetWaitDelay(2);    
       while (GetWaitDelay()!=0);
       LORA_LED = 1; // turn off LORA_LED                             
       SetWaitDelay(98);    
       while (GetWaitDelay()!=0);
       
       if (heartbeat++>15)
       {
           heartbeat = 0;
           CAN_LED=0; // always blink can led for 2ms so we know main loop is running
           SetWaitDelay(2);    
           while (GetWaitDelay()!=0);
       }
                     
       // turn off LED
       CAN_LED = 1;
       
       // if 20 seconds elapsed or any count is > 255
       // then send all data.      
       if (AnyCountsGreaterThanFF() || accumulatedTime >= timeout )  // 2 seconds expired since count changed
       {       
           LORA_LED = 0; // turn on LORA_LED                
           SendAllSensorDataToModem();  
           
           ClearAllDoorCounts();
           lastCount = 0;
           // read a few times to make sure we have the latest data.
           SetWaitDelay(500);    
           while (GetWaitDelay()!=0);                                 
           accumulatedTime = 0;  // reset the delay timer
           NeedToSendData = false; // reset the wait
       }
       LORA_LED = 1; // turn off LORA_LED                       
    }
        
        
} // end of main


