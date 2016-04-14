using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim {
	enum SpecialFunctionRegister {
		INDF				= 0x00,
		TMR0				= 0x01,
		PCL					= 0x02,
		STATUS			= 0x03,
		FSR					= 0x04,
		PORTA				= 0x05,
		PORTB				= 0x06,
		EEDATA			= 0x08,
		EEADR				= 0x09,
		PCLATH			= 0x0A,
		INTCON			= 0x0B,

		OPTION_REG	= 0x81,
		TRISA				= 0x85,
		TRISB				= 0x86,
		EECON1			= 0x88,
		EECON2			= 0x89,
	}
}
