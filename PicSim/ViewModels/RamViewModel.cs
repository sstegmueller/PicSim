using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Data;

namespace PicSim.ViewModels {
  class RamViewModel : PropertyChangedBase{

    #region Fields
    
    private DataTable ram;

    #endregion //Fields

    #region Properties
    
    public DataTable Ram
    {
      get
      {
        return ram;
      }

      set
      {
        ram = value;
        NotifyOfPropertyChange(() => Ram);
      }
    }

    #endregion //Properties

    #region Constructors

    public RamViewModel() {
      CreateRamTable();
    }

    #endregion //Constructors

    #region Methods

    private void CreateRamTable() {
      int nbColumns = 9;
      int nbRows = 32;
      Ram = new DataTable();
      AddColumns(nbColumns);
      AddRows(nbColumns, nbRows);

    }

    private void AddRows(int nbColumns, int nbRows) {
      for (int row = 0; row < nbRows; row++) {
        int rowHeaderIndex = row * 8;
        DataRow dr = Ram.NewRow();

        for (int col = 0; col < nbColumns; col++) {
          if (col == 0) {
            dr[col] = ActionHelper.ToHexString(rowHeaderIndex);
          }
          else {
            dr[col] = "00";
          }
        }
        Ram.Rows.Add(dr);
      }
    }

    private void AddColumns(int nbColumns) {
      for (int i = 0; i < nbColumns; i++) {
        if (i == 0) {
          Ram.Columns.Add("Register");
        }
        else {
          Ram.Columns.Add("0" + ActionHelper.ToHexString(i - 1), typeof(string));
        }
      }
    }

		public void RefreshDataTable(byte[] ram) {
			int column = 0;
			int row = 0;
			for(int register = 0; register < ram.Length; register++) {				
				Ram.Rows[row].SetField(column + 1, ActionHelper.ToHexString(ram[register]));
				column++;
				if (column >= 8) {
					column = 0;
					row++;
				}
			}
		}

    #endregion //Methods

  }
}
