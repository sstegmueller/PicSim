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
        ObjectifyArgs(opcode);
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
        if (HasFlag(opcode, opMask)) {
          Operations.Add(new OperationModel(opcode.Key, opMask));
          break;
        }
      }
    }

    private bool HasFlag(KeyValuePair<int, int> opcode, Operation op) {
      if ((Convert.ToInt32(op) & opcode.Value) == Convert.ToInt32(op)) {
        return true;
      }
      else {
        return false;
      }
    }

    private bool HasFlag(Operation op, OperationType type) {
      if ((Convert.ToInt32(op) & Convert.ToInt32(type)) == Convert.ToInt32(type)) {
        return true;
      }
      else {
        return false;
      }
    }

    private void ObjectifyArgs(KeyValuePair<int, int> opcode) {
      OperationModel opModel = Operations.Last();
      if (OperationType.ByteOrientedFD.HasFlag((OperationType)opModel.Operation)) {
        ParseFDArgs(opcode, opModel);
      }
      if (OperationType.ByteOrientedF.HasFlag((OperationType)opModel.Operation)) {
        ParseFArgs(opcode, opModel);
      }
      if (OperationType.BitOriented.HasFlag((OperationType)opModel.Operation)) {
        ParseBFArgs(opcode, opModel);
      }
      if (OperationType.LiteralControl.HasFlag((OperationType)opModel.Operation)) {
        ParseKArgs(opcode, opModel);
      }
    }

    private void ParseFDArgs(KeyValuePair<int, int> opcode, OperationModel opModel) {
      int intD = opcode.Value & Convert.ToInt32(0x0080);
      BitArray byteD = new BitArray(new int[] { intD });
      int f = opcode.Value & Convert.ToInt32(0x007F);
      opModel.SetArgs(byteD[7], f);
    }

    private void ParseFArgs(KeyValuePair<int, int> opcode, OperationModel opModel) {
      int f = opcode.Value & Convert.ToInt32(0x007F);
      opModel.SetArgs(f);
    }

    private void ParseBFArgs(KeyValuePair<int, int> opcode, OperationModel opModel) {
      int b = opcode.Value & Convert.ToInt32(0x0380);
      int f = opcode.Value & Convert.ToInt32(0x007F);
      opModel.SetArgs(b, f);
    }

    private void ParseKArgs(KeyValuePair<int, int> opcode, OperationModel opModel) {
      int k;
      if (opModel.Operation == Operation.CALL || opModel.Operation == Operation.GOTO) {
        k = opcode.Value & Convert.ToInt32(0x07FF);
      }
      else {
        k = opcode.Value & Convert.ToInt32(0x00FF);
      }
      opModel.SetArgs(k);
    }

    #endregion //Methods

  }
}
