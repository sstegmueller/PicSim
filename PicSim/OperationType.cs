using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim {
  [Flags]
  enum OperationType {
    ByteOrientedFD  = Operation.ADDWF | Operation.ANDWF | Operation.COMF | Operation.DECF | Operation.DECFSZ
                    | Operation.INCF | Operation.INCFSZ | Operation.IORWF | Operation.MOVF | Operation.RLF
                    | Operation.RRF | Operation.SUBWF | Operation.SWAPF | Operation.XORWF,
    ByteOrientedF   = Operation.CLRF | Operation.MOVWF,
    BitOriented     = Operation.BCF | Operation.BSF | Operation.BTFSC | Operation.BTFSS,
    LiteralControl  = Operation.ADDLW | Operation.ANDLW | Operation.CALL | Operation.GOTO | Operation.IORLW
                    | Operation.MOVLW | Operation.RETLW | Operation.SUBLW | Operation.XORLW
  }
}
