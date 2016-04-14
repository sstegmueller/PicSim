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
          Ram.Columns.Add("0" + ActionHelper.ToHexString(i - 1), typeof(double));
        }
      }
    }

    public void SetRamField(int column, int row, string value) {
      Ram.Rows[row].ItemArray[column + 1] = value;
    }

    #endregion //Methods

  }
}
