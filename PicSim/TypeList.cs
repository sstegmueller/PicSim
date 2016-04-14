using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim {

  class TypeList {

    #region Fields

    private List<Operation> _opsOfType;

		#endregion //Fields

		#region Properties

		internal List<Operation> OpsOfType {
			get {
				return _opsOfType;
			}

			set {
				_opsOfType = value;
			}
		}


		#endregion //Properties

		#region Constructors

		public TypeList(OperationType type) {
			OpsOfType = new List<Operation>();
			switch (type) {
				case OperationType.ByteOrientedFD:
					OpsOfType.Add(Operation.ADDWF);
					OpsOfType.Add(Operation.ANDWF);
					OpsOfType.Add(Operation.COMF);
					OpsOfType.Add(Operation.DECF);
					OpsOfType.Add(Operation.DECFSZ);
					OpsOfType.Add(Operation.INCF);
					OpsOfType.Add(Operation.INCFSZ);
					OpsOfType.Add(Operation.IORWF);
					OpsOfType.Add(Operation.MOVF);
					OpsOfType.Add(Operation.RLF);
					OpsOfType.Add(Operation.RRF);
					OpsOfType.Add(Operation.SUBWF);
					OpsOfType.Add(Operation.SWAPF);
					OpsOfType.Add(Operation.XORWF);
					break;
				case OperationType.ByteOrientedF:
					OpsOfType.Add(Operation.CLRF);
					OpsOfType.Add(Operation.MOVWF);
					break;
				case OperationType.BitOriented:
					OpsOfType.Add(Operation.BCF);
					OpsOfType.Add(Operation.BSF);
					OpsOfType.Add(Operation.BTFSC);
					OpsOfType.Add(Operation.BTFSS);
					break;
				case OperationType.LiteralControl:
					OpsOfType.Add(Operation.ADDLW);
					OpsOfType.Add(Operation.ANDLW);
					OpsOfType.Add(Operation.CALL);
					OpsOfType.Add(Operation.GOTO);
					OpsOfType.Add(Operation.IORLW);
					OpsOfType.Add(Operation.MOVLW);
					OpsOfType.Add(Operation.RETLW);
					OpsOfType.Add(Operation.SUBLW);
					OpsOfType.Add(Operation.XORLW);
					break;
				case OperationType.NoArgs:
					OpsOfType.Add(Operation.CLRW);
					OpsOfType.Add(Operation.NOP);
					OpsOfType.Add(Operation.CLRWDT);
					OpsOfType.Add(Operation.RETFIE);
					OpsOfType.Add(Operation.RETURN);
					OpsOfType.Add(Operation.SLEEP);
					break;
			}     
    }

		#endregion //Constructors

		#region Methods

		#endregion //Methods

	}
}
