using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Models {
  class ProgramModel {

    #region Fields
    
    private List<OperationModel> _operations = new List<OperationModel>();
		private RamModel _ram;
		private int _progCounter;

    #endregion //Fields

    #region Properties

    public List<OperationModel> Operations
    {
      get
      {
        return _operations;
      }

      set
      {
        _operations = value;
      }
    }

		public int ProgCounter {
			get {
				return _progCounter;
			}

			set {
				_progCounter = value;
			}
		}

		public RamModel Ram {
			get {
				return _ram;
			}
			set {
				_ram = value;
			}
		}

		#endregion //Properties

		#region Constructors

		public ProgramModel(string filePath) {
      Dictionary<int, int> opcodes = ParseFile(filePath);
      ObjectifyOPCodes(opcodes);
			ProgCounter = 0;
			_ram = new RamModel();
    }

    #endregion //Constructors

    #region Methods

    private Dictionary<int, int> ParseFile(string filePath) {
      int counter = 0;
      string line;
      Dictionary<int, int> opcodes = new Dictionary<int, int>();

      // Read the file save OP-Codes
      System.IO.StreamReader file = new System.IO.StreamReader(filePath);
      while ((line = file.ReadLine()) != null) {
        char[] chars = line.ToCharArray();
        if (char.IsNumber(chars[0])) {
          opcodes.Add(ParseOPCodeIndex(line), ParseOPCodeCommand(line));
        }
        counter++;
      }
      file.Close();
      return opcodes;
    }

    private int ParseOPCodeIndex(string line) {
      string subString;
      subString = line.Substring(0, 4);
      return Convert.ToInt32(subString, 16);
    }

    private int ParseOPCodeCommand(string line) {
      string subString = line.Substring(5, 4);
      int hex = Convert.ToInt32(subString, 16);
      return hex;
    }

    private void ObjectifyOPCodes(Dictionary<int, int> opcodes) {
      Operation[] sortedOpValues = SortEnumValues();
      foreach (KeyValuePair<int, int> opcode in opcodes) {
        ObjectifyOperation(sortedOpValues, opcode);
        ObjectifyArgs(opcode.Value);
      }
    }

    private Operation[] SortEnumValues() {
      Operation[] opValues = Enum.GetValues(typeof(Operation)).Cast<Operation>().ToArray();
      Array.Sort(opValues);
      Array.Reverse(opValues);
      return opValues;
    }

    private void ObjectifyOperation(Operation[] opValues, KeyValuePair<int, int> opcode) {
      foreach (Operation opMask in opValues) {
        if (HasMask(opcode.Value, opMask)) {
          Operations.Add(new OperationModel(opcode.Key, opMask));
          break;
        }
      }
    }

    private bool HasMask(int opcode, Operation op) {
      if ((Convert.ToInt32(op) & opcode) == Convert.ToInt32(op)) {
        return true;
      }
      else {
        return false;
      }
    }

    private void ObjectifyArgs(int opcode) {
      OperationModel opModel = Operations.Last();
      if (TypeHasFlag(new TypeList(OperationType.ByteOrientedFD).OpsOfType, opModel.Operation)) {
        ParseFDArgs(opcode, opModel);
        return;
      }
      if (TypeHasFlag(new TypeList(OperationType.ByteOrientedF).OpsOfType, opModel.Operation)) {
        ParseFArgs(opcode, opModel);
        return;
      }
      if (TypeHasFlag(new TypeList(OperationType.BitOriented).OpsOfType, opModel.Operation)) {
        ParseBFArgs(opcode, opModel);
        return;
      }
      if (TypeHasFlag(new TypeList(OperationType.LiteralControl).OpsOfType, opModel.Operation)) {
        ParseKArgs(opcode, opModel);
        return;
      }
			if (TypeHasFlag(new TypeList(OperationType.NoArgs).OpsOfType, opModel.Operation)) {
				opModel.OpType = OperationType.NoArgs;
				return;
			}
		}

    private void ParseFDArgs(int opcode, OperationModel opModel) {
      int intD = opcode & Convert.ToInt32(0x0080);
      BitArray byteD = new BitArray(new int[] { intD });
      int f = opcode & Convert.ToInt32(0x007F);
			f = CheckForFSR(f);
      opModel.SetArgs(byteD[7], f);
			opModel.OpType = OperationType.ByteOrientedFD;
    }

    private void ParseFArgs(int opcode, OperationModel opModel) {
      int f = opcode & Convert.ToInt32(0x007F);
			f = CheckForFSR(f);
			opModel.SetArgs(f);
			opModel.OpType = OperationType.ByteOrientedFD;
    }

    private void ParseBFArgs(int opcode, OperationModel opModel) {
      int b = (opcode & Convert.ToInt32(0x0380)) / 0x80;
      int f = opcode & Convert.ToInt32(0x007F);
      opModel.SetArgs(b, f);
			opModel.OpType = OperationType.BitOriented;
    }

    private void ParseKArgs(int opcode, OperationModel opModel) {
      int k;
      if (opModel.Operation == Operation.CALL || opModel.Operation == Operation.GOTO) {
        k = opcode & Convert.ToInt32(0x07FF);
      }
      else {
        k = opcode & Convert.ToInt32(0x00FF);
      }
      opModel.SetArgs(k);
			opModel.OpType = OperationType.LiteralControl;
    }

    private bool TypeHasFlag(List<Operation> ops, Operation op) {
      if (ops.Contains(op)) {
        return true;
      }
      else {
        return false;
      }
    }

		public void ExecuteCommand(int index) {
			ChooseCommand(GetOpByIndex(index));
			_progCounter++;
			_ram.SetRegisterValue((int)SFR.PCL, _progCounter);
		}

		public OperationModel GetOpByIndex(int index) {
			foreach (OperationModel opModel in Operations) {
				if (index == opModel.Index) {
					return opModel;
				}
			}
			return null;
		}

		private void ChooseCommand(OperationModel opModel) {
			switch (opModel.Operation) {
				case Operation.ADDLW:

					break;
				case Operation.ADDWF:
					ADDWFCommand(opModel);
					break;
				case Operation.ANDLW:
          ANDLWCommand(opModel);
					break;
				case Operation.ANDWF:
          ANDWFCommand(opModel);
					break;
				case Operation.BCF:
          BCFCommand(opModel);
					break;
				case Operation.BSF:
          BSFCommand(opModel);
          break;
				case Operation.BTFSC:
          BTFSCCommand(opModel);
          break;
				case Operation.BTFSS:
          BTFSSCommand(opModel);
          break;
				case Operation.CALL:
          CALLCommand(opModel);
          break;
				case Operation.CLRF:
          CLRFCommand(opModel);
					break;
				case Operation.CLRW:
          CLRWCommand(opModel);
					break;
				case Operation.CLRWDT:
          CLRWDTCommand(opModel);
					break;
				case Operation.COMF:
          COMFCommand(opModel);
					break;
				case Operation.DECF:
          DECFCommand(opModel);
					break;
				case Operation.DECFSZ:
          DECFSZCommand(opModel);
					break;
				case Operation.GOTO:
					GOTOCommand(opModel);
					break;
				case Operation.INCF:
          INCFCommand(opModel);
					break;
				case Operation.INCFSZ:
          INCFSZCommand(opModel);
					break;
				case Operation.IORLW:
          IORLWCommand(opModel);
					break;
				case Operation.IORWF:
          IORWFCommand(opModel);
					break;
				case Operation.MOVF:
          MOVFCommand(opModel);
					break;
				case Operation.MOVLW:
          MOVLWCommand(opModel);
					break;
				case Operation.MOVWF:
          MOVWFCommand(opModel);
					break;
				case Operation.NOP:
          NOPCommand(opModel);
					break;
				case Operation.RETFIE:
          RETFIECommand(opModel);
					break;
				case Operation.RETLW:
          RETLWCommand(opModel);
					break;
				case Operation.RETURN:
          RETURNCommand(opModel);
					break;
				case Operation.RLF:
          RLFCommand(opModel);
					break;
				case Operation.RRF:
          RRFCommand(opModel);
					break;
				case Operation.SLEEP:
          SLEEPCommand(opModel);
					break;
				case Operation.SUBLW:
          SUBLWCommand(opModel);
					break;
				case Operation.SUBWF:
          SUBWFCommand(opModel);
					break;
				case Operation.SWAPF:
          SWAPFCommand(opModel);
					break;
				case Operation.XORLW:
          XORLWCommand(opModel);
					break;
				case Operation.XORWF:
          XORWFCommand(opModel);
					break;
			}
		}

		private void ADDWFCommand(OperationModel opModel) {
			if (opModel.Args.Bool1) {
				Ram.SetRegisterValue(opModel.Args.Byte2, Ram.GetRegisterValue() + Ram.GetRegisterValue(opModel.Args.Byte2));
				CheckZeroBit(opModel.Args.Byte2);
			}
			else {
				Ram.SetRegisterValue(Ram.GetRegisterValue() + Ram.GetRegisterValue(opModel.Args.Byte2));
				CheckZeroBit();
			}
			CheckCarryBit(Ram.GetRegisterValue(), Ram.GetRegisterValue(opModel.Args.Byte2));
			CheckDigitCarryBit(Ram.GetRegisterValue(), Ram.GetRegisterValue(opModel.Args.Byte2));		
		}

		private void ANDWFCommand(OperationModel opModel) {
			if (opModel.Args.Bool1) {
				Ram.SetRegisterValue(opModel.Args.Byte2, Ram.GetRegisterValue() & Ram.GetRegisterValue(opModel.Args.Byte2));
				CheckZeroBit(opModel.Args.Byte2);
			}
			else {
				Ram.SetRegisterValue(Ram.GetRegisterValue() & Ram.GetRegisterValue(opModel.Args.Byte2));
				CheckZeroBit();
			}
		}

		private void CLRFCommand(OperationModel opModel) {
			Ram.SetRegisterValue(opModel.Args.Byte2, 0);
			CheckZeroBit(opModel.Args.Byte2);
		}

		private void CLRWCommand(OperationModel opModel) {
			Ram.SetRegisterValue(0);
			CheckZeroBit();
		}

		private void COMFCommand(OperationModel opModel) {
			Ram.SetRegisterValue(opModel.Args.Byte2, ~Ram.GetRegisterValue(opModel.Args.Byte2));
			CheckZeroBit(opModel.Args.Byte2);
		}

		private void DECFCommand(OperationModel opModel) {
			int dec = Ram.GetRegisterValue(opModel.Args.Byte2) - 1;
			if (opModel.Args.Bool1) {
				Ram.SetRegisterValue(opModel.Args.Byte2, dec);
				CheckZeroBit(opModel.Args.Byte2);
			}
			else {
				Ram.SetRegisterValue(dec);
				CheckZeroBit();
			}
		}

		private void DECFSZCommand(OperationModel opModel) {
			DECFCommand(opModel);
			if (Ram.GetRegisterBit((int)SFR.STATUS, 2)){
				NOPCommand(opModel);
				_progCounter++;
			}
		}

		private void INCFCommand(OperationModel opModel) {
			int inc = Ram.GetRegisterValue(opModel.Args.Byte2) + 1;
			if (opModel.Args.Bool1) {
				Ram.SetRegisterValue(opModel.Args.Byte2, inc);
				CheckZeroBit(opModel.Args.Byte2);
			}
			else {
				Ram.SetRegisterValue(inc);
				CheckZeroBit();
			}
		}

		private void INCFSZCommand(OperationModel opModel) {
			INCFCommand(opModel);
			if (Ram.GetRegisterBit((int)SFR.STATUS, 2)) {
				NOPCommand(opModel);
				_progCounter++;
			}
		}

		private void IORWFCommand(OperationModel opModel) {
			if (opModel.Args.Bool1) {
				Ram.SetRegisterValue(opModel.Args.Byte2, Ram.GetRegisterValue() | Ram.GetRegisterValue(opModel.Args.Byte2));
				CheckZeroBit(opModel.Args.Byte2);
			}
			else {
				Ram.SetRegisterValue(Ram.GetRegisterValue() | Ram.GetRegisterValue(opModel.Args.Byte2));
				CheckZeroBit();
			}
		}

		private void MOVFCommand(OperationModel opModel) {
			if (opModel.Args.Bool1) {
				Ram.SetRegisterValue(opModel.Args.Byte2, Ram.GetRegisterValue(opModel.Args.Byte2));
				CheckZeroBit(opModel.Args.Byte2);
			}
			else {
				Ram.SetRegisterValue(Ram.GetRegisterValue(opModel.Args.Byte2));
				CheckZeroBit();
			}
		}

    private void MOVWFCommand(OperationModel opModel) {
    }

    private void NOPCommand(OperationModel opModel) {

		}

		private void RLFCommand(OperationModel opModel) {
			bool msb = Ram.GetRegisterBit(opModel.Args.Byte2, 7);
			bool carry = Ram.GetRegisterBit((int)SFR.STATUS, 0);
			int shift = 2 * Ram.GetRegisterValue(opModel.Args.Byte2);
			if (carry) {
				shift++;
			}
			if (msb) {
				Ram.ToggleRegisterBit((int)SFR.STATUS, 0, true);
			}
			else {
				Ram.ToggleRegisterBit((int)SFR.STATUS, 0, false);
			}
			if (opModel.Args.Bool1) {
				Ram.SetRegisterValue(opModel.Args.Byte2, shift);
			}
			else {
				Ram.SetRegisterValue(shift);
			}
		}

		private void RRFCommand(OperationModel opModel) {
			bool lsb = Ram.GetRegisterBit(opModel.Args.Byte2, 0);
			bool carry = Ram.GetRegisterBit((int)SFR.STATUS, 0);
			int shift = 2 / Ram.GetRegisterValue(opModel.Args.Byte2);
			if (carry) {
				shift += 255;
			}
			if (lsb) {
				Ram.ToggleRegisterBit((int)SFR.STATUS, 0, true);
			}
			else {
				Ram.ToggleRegisterBit((int)SFR.STATUS, 0, false);
			}
			if (opModel.Args.Bool1) {
				Ram.SetRegisterValue(opModel.Args.Byte2, shift);
			}
			else {
				Ram.SetRegisterValue(shift);
			}
		}

		private void SUBWFCommand(OperationModel opModel) {

		}

		private void SWAPFCommand(OperationModel opModel) {

		}

		private void XORWFCommand(OperationModel opModel) {

		}

		private void BCFCommand(OperationModel opModel) {

		}

		private void BSFCommand(OperationModel opModel) {

		}

		private void BTFSCCommand(OperationModel opModel) {

		}

		private void BTFSSCommand(OperationModel opModel) {

		}

		private void ADDLWCommand(OperationModel opModel) {

		}

		private void ANDLWCommand(OperationModel opModel) {

		}

		private void CALLCommand(OperationModel opModel) {

		}

		private void CLRWDTCommand(OperationModel opModel) {

		}

		private void GOTOCommand(OperationModel opModel) {
			ProgCounter = opModel.Args.Byte1;
		}

		private void IORLWCommand(OperationModel opModel) {

		}

		private void MOVLWCommand(OperationModel opModel) {

		}

		private void RETFIECommand(OperationModel opModel) {

		}

		private void RETLWCommand(OperationModel opModel) {

		}

		private void RETURNCommand(OperationModel opModel) {

		}

		private void SLEEPCommand(OperationModel opModel) {

		}

		private void SUBLWCommand(OperationModel opModel) {

		}

		private void XORLWCommand(OperationModel opModel) {

		}

    private int CheckForFSR(int adress) {
			int resultAdress = adress;
			if(adress == 0) {
				resultAdress = (int)SFR.FSR;
			}
			return resultAdress;
		}
		
		private void CheckCarryBit(int byte1, int byte2) {
			if(byte1 + byte2 > 255) {
				Ram.ToggleRegisterBit((int)SFR.STATUS, 0, true);
			}
			else {
				Ram.ToggleRegisterBit((int)SFR.STATUS, 0, false);
			}
		}

		private void CheckDigitCarryBit(int byte1, int byte2) {
			byte mask = Convert.ToByte(15);
			byte lowValue1 = (byte)(mask & Convert.ToByte(byte1));
			byte lowValue2 = (byte)(mask & Convert.ToByte(byte2));
			if ((lowValue1 + lowValue2) > 15) {
				Ram.ToggleRegisterBit((int)SFR.STATUS, 1, true);
			}
			else {
				Ram.ToggleRegisterBit((int)SFR.STATUS, 1, false);
			}
		}

		private void CheckZeroBit(int adress) {
			if (Ram.GetRegisterValue(adress) == 0) {
				Ram.ToggleRegisterBit((int)SFR.STATUS, 2, true);
			}
			else {
				Ram.ToggleRegisterBit((int)SFR.STATUS, 2, false);
			}
		}
		private void CheckZeroBit() {
			if (Ram.GetRegisterValue() == 0) {
				Ram.ToggleRegisterBit((int)SFR.STATUS, 2, true);
			}
			else {
				Ram.ToggleRegisterBit((int)SFR.STATUS, 2, false);
			}
		}

		#endregion //Methods

	}
}
