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
      Ram = Tools.CreateTable(9, 32);
    }

    #endregion //Constructors

    #region Methods

		public void RefreshDataTable(byte[] ram) {
			int column = 0;
			int row = 0;
			for(int register = 0; register < ram.Length; register++) {				
				Ram.Rows[row].SetField(column + 1, Tools.ToHexString(ram[register]));
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
