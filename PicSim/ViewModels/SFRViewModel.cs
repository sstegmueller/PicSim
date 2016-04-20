using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using PicSim.Models;

namespace PicSim.ViewModels {
  class SfrViewModel : PropertyChangedBase {

    #region Fields

    private string _wRegValue;
    private string _fSRValue;
    private string _pCLValue;
    private string _pCLATHValue;
    private string _pCValue;
    private string _statusValue;

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

    #endregion //Properties

    #region Constructors

    public SfrViewModel() {

    }

    #endregion //Constructors

    #region Methods

    public void RefreshSfr(RamModel ram) {
      WRegValue = ActionHelper.ToHexString(ram.WReg);
      FSRValue = ActionHelper.ToHexString(ram.RamArray[(int)SFR.FSR]);
      PCLValue = ActionHelper.ToHexString(ram.RamArray[(int)SFR.PCL]);
      PCLATHValue = ActionHelper.ToHexString(ram.RamArray[(int)SFR.PCLATH]);
      StatusValue = ActionHelper.ToHexString(ram.RamArray[(int)SFR.STATUS]);
    }

    #endregion //Methods
  }
}
