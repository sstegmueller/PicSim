using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Models {
  class RamModel {

    #region Fields

    private byte[] _ramArray;
    private byte _wReg;
    private Stack<int> _stack;

    #endregion //Fields

    #region Properties

    public byte[] RamArray {
      get {
        return _ramArray;
      }
    }

    public byte WReg {
      get {
        return _wReg;
      }
    }

    public Stack<int> Stack {
      get {
        return _stack;
      }
    }

    #endregion //Properties

    #region Constructors

    public RamModel() {
      _ramArray = new byte[0xFF];
      _stack = new Stack<int>();
    }

    #endregion //Constructors

    #region Methods

    public void SetRegisterValue(int adress, int value) {
      adress = CheckRegisterBank(adress);
      DirectSetRegisterValue(adress, value);
    }

    public void DirectSetRegisterValue(int adress, int value) {
      _ramArray[adress] = (byte)value;
      if (IsMirroredRegister(adress)) {
        _ramArray[adress + 0x80] = (byte)value;
      }
    }

    public void SetRegisterValue(int value) {
      _wReg = (byte)value;
    }

    public int GetRegisterValue(int adress) {
      adress = CheckRegisterBank(adress);
      return DirectGetRegisterValue(adress);
    }

    public int DirectGetRegisterValue(int adress) {
      return Convert.ToInt32(_ramArray[adress]);
    }

    public int GetRegisterValue() {
      return Convert.ToInt32(_wReg);
    }

    public void ToggleRegisterBit(int adress, int bit, bool set) {
      adress = CheckRegisterBank(adress);
      DirectToggleRegisterBit(adress, bit, set);
    }

    public void DirectToggleRegisterBit(int adress, int bit, bool set) {
      if (bit < 8 && bit > -1) {
        byte mask = Convert.ToByte(0x01 << bit);
        byte value = _ramArray[adress];
        if (set) {
          _ramArray[adress] = (byte)(value | mask);
          if (IsMirroredRegister(adress)) {
            _ramArray[adress + 0x80] = (byte)(value | mask);
          }
        }
        else {
          _ramArray[adress] = (byte)(_ramArray[adress] & ~(mask));
          if (IsMirroredRegister(adress)) {
            _ramArray[adress + 0x80] = (byte)(_ramArray[adress] & ~(mask));
          }
        }
      }
    }

    public void ToggleRegisterBit(int adress, int bit) {
      adress = CheckRegisterBank(adress);
      DirectToggleRegisterBit(adress, bit);
    }

    public void DirectToggleRegisterBit(int adress, int bit) {
      if (bit < 8 && bit > -1) {
        byte mask = Convert.ToByte(0x01 << bit);
        byte value = _ramArray[adress];
        _ramArray[adress] = (byte)(value ^ mask);
        if (IsMirroredRegister(adress)) {
          _ramArray[adress + 0x80] = (byte)(value ^ mask);
        }
      }
    }

    public bool GetRegisterBit(int adress, int bit) {
      adress = CheckRegisterBank(adress);
      return DirectGetRegisterBit(adress, bit);
    }

    public bool DirectGetRegisterBit(int adress, int bit) {
      if (bit < 8 && bit > -1) {
        return (_ramArray[adress] & (1 << bit)) != 0;
      }
      return false;
    }

    private int CheckRegisterBank(int adress) {
      if (DirectGetRegisterBit((int)SFR.STATUS, 5) &&
        !IsMirroredRegister(adress)) {
        adress += 0x80;
      }
      return adress;
    }

    private bool IsMirroredRegister(int adress) {
      if(adress == (int)SFR.INDF ||
          adress == (int)SFR.STATUS ||
          adress == (int)SFR.FSR ||
          adress == (int)SFR.PCL ||
          adress == (int)SFR.PCLATH ||
          adress == (int)SFR.INTCON) {
        return true;
      }
      return false;
    }

    public void PushStack(int adress) {
      if (Stack.Count == 8) {
        _stack.Clear();
      }
      _stack.Push(adress);
    }

    public int PopStack() {
      return _stack.Pop();
    }
    
    #endregion //Methods
  }
}
