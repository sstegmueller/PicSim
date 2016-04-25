using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using PicSim.Models;
using System.Data;

namespace PicSim.ViewModels {
  class SfrViewModel : PropertyChangedBase {

    #region Fields

    private string _wRegValue;
    private string _fSRValue;
    private string _pCLValue;
    private string _pCLATHValue;
    private string _pCValue;
    private string _statusValue;
    private DataTable _iOPins;

    #endregion //Fields

    #region Properties

    public string WRegValue {
      get {
        return _wRegValue;
      }

      set {
        _wRegValue = value;
        NotifyOfPropertyChange(() => WRegValue);
      }
    }

    public string FSRValue {
      get {
        return _fSRValue;
      }

      set {
        _fSRValue = value;
        NotifyOfPropertyChange(() => FSRValue);
      }
    }

    public string PCLATHValue {
      get {
        return _pCLATHValue;
      }

      set {
        _pCLATHValue = value;
        NotifyOfPropertyChange(() => PCLATHValue);
      }
    }

    public string PCValue {
      get {
        return _pCValue;
      }

      set {
        _pCValue = value;
        NotifyOfPropertyChange(() => PCValue);
      }
    }

    public string StatusValue {
      get {
        return _statusValue;
      }

      set {
        _statusValue = value;
        NotifyOfPropertyChange(() => StatusValue);
      }
    }

    public string PCLValue {
      get {
        return _pCLValue;
      }

      set {
        _pCLValue = value;
        NotifyOfPropertyChange(() => PCLValue);
      }
    }

    public DataTable IOPins
    {
      get
      {
        return _iOPins;
      }

      set
      {
        _iOPins = value;
        NotifyOfPropertyChange(() => IOPins);
      }
    }

    #endregion //Properties

    #region Constructors

    public SfrViewModel() {
      IOPins = Tools.CreateTable(9, 2);
      IOPins.Rows[0].SetField(0, "PortA");
      IOPins.Rows[1].SetField(0, "PortB");
    }

    #endregion //Constructors

    #region Methods

    public void RefreshSfr(RamModel ram, int pc) {
      WRegValue = Tools.ToHexString(ram.WReg);
      FSRValue = Tools.ToHexString(ram.RamArray[(int)SFR.FSR]);
      PCLValue = Tools.ToHexString(ram.RamArray[(int)SFR.PCL]);
      PCLATHValue = Tools.ToHexString(ram.RamArray[(int)SFR.PCLATH]);
      PCValue = Tools.ToHexString(pc);
      StatusValue = Tools.ToHexString(ram.RamArray[(int)SFR.STATUS]);
    }

    public void RefreshDataTable(RamModel ram) {
      int column = 0;
      int row = 0;
      for (int register = 0; register < 2; register++) {
        if(register == 0) {
          IOPins.Rows[row].SetField(column + 1, ram.GetRegisterBit((int)SFR.PORTA, column));
        }
        else {
          IOPins.Rows[row].SetField(column + 1, ram.GetRegisterBit((int)SFR.PORTB, column));
        }        
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
