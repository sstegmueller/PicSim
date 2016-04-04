using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Models {
  class ProgramModel {

    #region Fields

    private Dictionary<int, int> _opcodes;

    #endregion //Fields

    #region Properties

    #endregion //Properties

    #region Constructors

    public ProgramModel(string filePath) {
      int counter = 0;
      string line;

      // Read the file and display it line by line.
      System.IO.StreamReader file = new System.IO.StreamReader(filePath);
      while ((line = file.ReadLine()) != null) {
        char[] chars = line.ToCharArray();
        if (char.IsNumber(chars[0])) {
          System.Console.WriteLine(line);
          _opcodes.Add(GetOPCodeIndex(line), GetOPCodeCommand(line));
        }
        counter++;
      }

      file.Close();
    }

    #endregion //Constructors

    #region Methods

    private int GetOPCodeIndex(string line) {
      string rawIndex = line.Take<char>(4).ToString();
      int index = Convert.ToInt32(rawIndex);
      return index;
    }

    private int GetOPCodeCommand(string line) {
      int command;
      char[] commandDigits = new char[4];
      for (int character = 5; character < 9; character++) {
        commandDigits[character - 5] = line[character];
      }
      command = Convert.ToInt32(commandDigits.ToString());
      return command;
    }

    #endregion //Methods

  }
}
