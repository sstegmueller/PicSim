using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim {
  class OperationType {

    #region Fields

    private List<Operation> _byteOrientedFD;
    private List<Operation> _byteOrientedF;
    private List<Operation> _bitOriented;
    private List<Operation> _literalControl;

    #endregion //Fields

    #region Properties

    public List<Operation> ByteOrientedFD
    {
      get
      {
        return _byteOrientedFD;
      }

      set
      {
        _byteOrientedFD = value;
      }
    }

    public List<Operation> ByteOrientedF
    {
      get
      {
        return _byteOrientedF;
      }

      set
      {
        _byteOrientedF = value;
      }
    }

    public List<Operation> BitOriented
    {
      get
      {
        return _bitOriented;
      }

      set
      {
        _bitOriented = value;
      }
    }

    public List<Operation> LiteralControl
    {
      get
      {
        return _literalControl;
      }

      set
      {
        _literalControl = value;
      }
    }

    #endregion //Properties

    #region Constructors

    public OperationType() {
      ByteOrientedFD = new List<Operation>();
      ByteOrientedF = new List<Operation>();
      BitOriented = new List<Operation>();
      LiteralControl = new List<Operation>();

      ByteOrientedFD.Add(Operation.ADDWF);
      ByteOrientedFD.Add(Operation.ANDWF);
      ByteOrientedFD.Add(Operation.COMF);
      ByteOrientedFD.Add(Operation.DECF);
      ByteOrientedFD.Add(Operation.DECFSZ);
      ByteOrientedFD.Add(Operation.INCF);
      ByteOrientedFD.Add(Operation.INCFSZ);
      ByteOrientedFD.Add(Operation.IORWF);
      ByteOrientedFD.Add(Operation.MOVF);
      ByteOrientedFD.Add(Operation.RLF);
      ByteOrientedFD.Add(Operation.RRF);
      ByteOrientedFD.Add(Operation.SUBWF);
      ByteOrientedFD.Add(Operation.SWAPF);
      ByteOrientedFD.Add(Operation.XORWF);

      ByteOrientedF.Add(Operation.CLRF);
      ByteOrientedF.Add(Operation.MOVWF);

      BitOriented.Add(Operation.BCF);
      BitOriented.Add(Operation.BSF);
      BitOriented.Add(Operation.BTFSC);
      BitOriented.Add(Operation.BTFSS);

      LiteralControl.Add(Operation.ADDLW);
      LiteralControl.Add(Operation.ANDLW);
      LiteralControl.Add(Operation.CALL);
      LiteralControl.Add(Operation.GOTO);
      LiteralControl.Add(Operation.IORLW);
      LiteralControl.Add(Operation.MOVLW);
      LiteralControl.Add(Operation.RETLW);
      LiteralControl.Add(Operation.SUBLW);
      LiteralControl.Add(Operation.XORLW);
    }

    #endregion //Constructors

    #region Methods

    #endregion //Methods

  }
}
