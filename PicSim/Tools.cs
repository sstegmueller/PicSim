using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim {
  static class Tools {

    #region Methods

    public static string ToHexString(string value) {
      int intAgain = int.Parse(value);
      return String.Format("{0:X}", intAgain);
    }

    public static string ToHexString(int value) {
      return String.Format("{0:X}", value);
    }

    public static string ToHexString(bool value) {
      return String.Format("{0:X}", value);
    }

    public static DataTable CreateTable(int cols, int rows) {
      DataTable ram = new DataTable();
      ram = AddColumns(cols, ram);
      ram = AddRows(cols, rows, ram);
      return ram;
    }

    public static DataTable AddRows(int nbColumns, int nbRows, DataTable ram) {
      for (int row = 0; row < nbRows; row++) {
        int rowHeaderIndex = row * 8;
        DataRow dr = ram.NewRow();

        for (int col = 0; col < nbColumns; col++) {
          if (col == 0) {
            dr[col] = Tools.ToHexString(rowHeaderIndex);
          }
          else {
            dr[col] = "0";
          }
        }
        ram.Rows.Add(dr);
      }
      return ram;
    }

    public static DataTable AddColumns(int nbColumns, DataTable ram) {
      for (int i = 0; i < nbColumns; i++) {
        if (i == 0) {
          ram.Columns.Add("Register");
        }
        else {
          ram.Columns.Add("0" + Tools.ToHexString(i - 1), typeof(string));
        }
      }
      return ram;
    }

    public static double CalculateRuntime(int cycles, double frequency) {
      return ((cycles / frequency) * 4);
    }

    #endregion //Methods

  }
}
