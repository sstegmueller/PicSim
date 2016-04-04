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

    private Dictionary<int, BitArray> _opcodes = new Dictionary<int, BitArray>();

    #endregion //Fields

    #region Properties

    #endregion //Properties

    #region Constructors

    public ProgramModel(string filePath) {
      ParseFile(filePath);
      ObjectifyOPCodes();
    }

    #endregion //Constructors

    #region Methods

    private void ParseFile(string filePath) {
      int counter = 0;
      string line;

      // Read the file and display it line by line.
      System.IO.StreamReader file = new System.IO.StreamReader(filePath);
      while ((line = file.ReadLine()) != null) {
        char[] chars = line.ToCharArray();
        if (char.IsNumber(chars[0])) {
          _opcodes.Add(ParseOPCodeIndex(line), ParseOPCodeCommand(line));
        }
        counter++;
      }
      file.Close();
    }

    private int ParseOPCodeIndex(string line) {
      string subString;
      subString = line.Substring(0, 4);
      return Convert.ToInt32(subString);
    }

    private BitArray ParseOPCodeCommand(string line) {
      string subString = line.Substring(5, 8);
      long int64 = Int64.Parse(subString, NumberStyles.HexNumber);
      byte[] bytes = BitConverter.GetBytes(int64);
      return new BitArray(bytes);
    }

    private void ObjectifyOPCodes() {
      foreach (KeyValuePair<int, BitArray> opcode in _opcodes) {
        
      }
    }

    #endregion //Methods

  }
}
