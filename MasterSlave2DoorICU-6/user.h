#ifndef USER_H
#define USER_H

#include "types.h"
/******************************************************************************/
/* User Level #define Macros                                                  */
/******************************************************************************/

/* TODO Application specific user parameters used in user.c may go here */

/******************************************************************************/
/* User Function Prototypes                                                   */
/******************************************************************************/

/* TODO User level functions prototypes (i.e. InitApp) go here */

void InitApp(void);         /* I/O and Peripheral Initialization */


// define the TRIS bits
#define OUT1_TRIS        (TRISBbits.TRISB8)
#define OUT2_TRIS        (TRISBbits.TRISB9)
#define IN1_TRIS         (TRISBbits.TRISB4)
#define IN2_TRIS         (TRISBbits.TRISB5)
#define IN3_TRIS         (TRISBbits.TRISB6)
#define IN4_TRIS         (TRISBbits.TRISB7)
#define LED1_TRIS        (TRISEbits.TRISE1)
#define LED2_TRIS        (TRISEbits.TRISE0)

#define TESTPIN_TRIS     (TRISBbits.TRISB3)


#define SW0_TRIS         (TRISDbits.TRISD11)
#define SW1_TRIS         (TRISDbits.TRISD10)
#define SW2_TRIS         (TRISDbits.TRISD9)
#define SW3_TRIS         (TRISDbits.TRISD8)
#define SW4_TRIS         (TRISDbits.TRISD7)
#define SW5_TRIS         (TRISDbits.TRISD6)

////////////
//#define MODEM_TX_TRIS      (TRISFbits.TRISF1)
//#define MODEM_RX_TRIS      (TRISCbits.TRISC14)
//#define MODEM_UART_EN_TRIS (TRISFbits.TRISF4)
//#define INVALID_TRIS       (TRISFbits.TRISF0)

/////////////

#define LORA_TX_TRIS     (TRISFbits.TRISF1)
#define LORA_RX_TRIS     (TRISCbits.TRISC14)
#define LORA_RESET_TRIS  (TRISFbits.TRISF4)
#define INVALID_TRIS       (TRISFbits.TRISF0)
//#define LORA_RTSOUT_TRIS (TRISBbits.TRISB15)
//#define LORA_CTS_TRIS    (TRISDbits.TRISD2)

//define the OUTPUT bits
#define OUT1            (LATBbits.LATB8)
#define OUT2            (LATBbits.LATB9)
#define CAN_LED         (LATEbits.LATE1)
#define LORA_LED        (LATEbits.LATE0)
//#define MODEM_UART_EN   (LATFbits.LATF4)
#define LORA_RESET      (LATFbits.LATF4)




// define the INPUT bits
#define IN1           (PORTDbits.RD11)
#define IN2           (PORTDbits.RD10)
#define IN3           (PORTDbits.RD9)
#define IN4           (PORTDbits.RD8)
#define IN5           (PORTDbits.RD7)
#define IN6           (PORTDbits.RD6)


extern void DebounceInputs( void );

extern boolean Input1Pressed;
extern boolean Input2Pressed;
extern boolean Input3Pressed;
extern boolean Input4Pressed;
extern boolean Input5Pressed;
extern boolean Input6Pressed;
#endif