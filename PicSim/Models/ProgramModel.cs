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

    #endregion //Properties

    #region Constructors

    public ProgramModel(string filePath) {
      Dictionary<int, int> opcodes = ParseFile(filePath);
      ObjectifyOPCodes(opcodes);
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
      opModel.SetArgs(byteD[7], f);
			opModel.OpType = OperationType.ByteOrientedFD;
    }

    private void ParseFArgs(int opcode, OperationModel opModel) {
      int f = opcode & Convert.ToInt32(0x007F);
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

		public void StartProgram() {

		}

    #endregion //Methods

  }
}
