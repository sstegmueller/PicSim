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
    private string _carryBitValue;
    private RamModel _ram;
    private bool _rA0;
    private bool _rA1;
    private bool _rA2;
    private bool _rA3;
    private bool _rA4;
    private bool _rA5;
    private bool _rA6;
    private bool _rA7;
    private bool _rB0;
    private bool _rB1;
    private bool _rB2;
    private bool _rB3;
    private bool _rB4;
    private bool _rB5;
    private bool _rB6;
    private bool _rB7;

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

    public string CarryBitValue {
      get {
        return _carryBitValue;
      }

      set {
        _carryBitValue = value;
        NotifyOfPropertyChange(() => CarryBitValue);
      }
    }

    public bool RA0 {
      get {
        return _rA0;
      }

      set {
        _rA0 = value;
        NotifyOfPropertyChange(() => RA0);
        _ram.ToggleRegisterBit((int)SFR.PORTA, 0);
      }
    }

    public bool RA1 {
      get {
        return _rA1;
      }

      set {
        _rA1 = value;
        NotifyOfPropertyChange(() => RA1);
        _ram.ToggleRegisterBit((int)SFR.PORTA, 1);
      }
    }

    public bool RA2 {
      get {
        return _rA2;
      }

      set {
        _rA2 = value;
        NotifyOfPropertyChange(() => RA2);
        _ram.ToggleRegisterBit((int)SFR.PORTA, 2);
      }
    }

    public bool RA3 {
      get {
        return _rA3;
      }

      set {
        _rA3 = value;
        NotifyOfPropertyChange(() => RA3);
        _ram.ToggleRegisterBit((int)SFR.PORTA, 3);
      }
    }

    public bool RA4 {
      get {
        return _rA4;
      }

      set {
        _rA4 = value;
        NotifyOfPropertyChange(() => RA4);
        _ram.ToggleRegisterBit((int)SFR.PORTA, 4);
      }
    }

    public bool RA5 {
      get {
        return _rA5;
      }

      set {
        _rA5 = value;
        NotifyOfPropertyChange(() => RA5);
        _ram.ToggleRegisterBit((int)SFR.PORTA, 5);
      }
    }

    public bool RA6 {
      get {
        return _rA6;
      }

      set {
        _rA6 = value;
        NotifyOfPropertyChange(() => RA6);
        _ram.ToggleRegisterBit((int)SFR.PORTA, 6);
      }
    }

    public bool RA7 {
      get {
        return _rA7;
      }

      set {
        _rA7 = value;
        NotifyOfPropertyChange(() => RA7);
        _ram.ToggleRegisterBit((int)SFR.PORTA, 7);
      }
    }

    public bool RB0 {
      get {
        return _rB0;
      }

      set {
        _rB0 = value;
        NotifyOfPropertyChange(() => RB0);
        _ram.ToggleRegisterBit((int)SFR.PORTB, 0);
      }
    }

    public bool RB1 {
      get {
        return _rB1;
      }

      set {
        _rB1 = value;
        NotifyOfPropertyChange(() => RB1);
        _ram.ToggleRegisterBit((int)SFR.PORTB, 1);
      }
    }

    public bool RB2 {
      get {
        return _rB2;
      }

      set {
        _rB2 = value;
        NotifyOfPropertyChange(() => RB2);
        _ram.ToggleRegisterBit((int)SFR.PORTB, 2);
      }
    }

    public bool RB3 {
      get {
        return _rB3;
      }
      set {
        _rB3 = value;
        NotifyOfPropertyChange(() => RB3);
        _ram.ToggleRegisterBit((int)SFR.PORTB, 3);
      }
    }

    public bool RB4 {
      get {
        return _rB4;
      }

      set {
        _rB4 = value;
        NotifyOfPropertyChange(() => RB4);
        _ram.ToggleRegisterBit((int)SFR.PORTB, 4);
      }
    }

    public bool RB5 {
      get {
        return _rB5;
      }

      set {
        _rB5 = value;
        NotifyOfPropertyChange(() => RB5);
        _ram.ToggleRegisterBit((int)SFR.PORTB, 5);
      }
    }

    public bool RB6 {
      get {
        return _rB6;
      }

      set {
        _rB6 = value;
        NotifyOfPropertyChange(() => RB6);
        _ram.ToggleRegisterBit((int)SFR.PORTB, 6);
      }
    }

    public bool RB7 {
      get {
        return _rB7;
      }

      set {
        _rB7 = value;
        NotifyOfPropertyChange(() => RB7);
        _ram.ToggleRegisterBit((int)SFR.PORTB, 7);
      }
    }

    #endregion //Properties

    #region Constructors

    public SfrViewModel() {
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
      CarryBitValue = Tools.ToHexString(ram.GetRegisterBit((int)SFR.STATUS, 0));
    }

    public void GiveRamModel(RamModel ram) {
      _ram = ram;
    }

    #endregion //Methods
  }
}
