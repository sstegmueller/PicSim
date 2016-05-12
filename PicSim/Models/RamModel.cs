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
      _ramArray[adress] = (byte)value;
    }

    public void SetRegisterValue(int value) {
      _wReg = (byte)value;
    }

    public int GetRegisterValue(int adress) {
      adress = CheckRegisterBank(adress);
      return Convert.ToInt32(_ramArray[adress]);
    }

    public int GetRegisterValue() {
      return Convert.ToInt32(_wReg);
    }

    public void ToggleRegisterBit(int adress, int bit, bool set) {
      adress = CheckRegisterBank(adress);
      if (bit < 8 && bit > -1) {
        byte mask = Convert.ToByte(0x01 << bit);
        byte value = _ramArray[adress];
        if (set) {
          _ramArray[adress] = (byte)(value | mask);
        }
        else {
          _ramArray[adress] = (byte)(_ramArray[adress] & ~(mask));
        }
      }
    }

    public void ToggleRegisterBit(int adress, int bit) {
      adress = CheckRegisterBank(adress);
      if (bit < 8 && bit > -1) {
        byte mask = Convert.ToByte(0x01 << bit);
        byte value = _ramArray[adress];
        _ramArray[adress] = (byte)(value ^ mask);
      }
    }

    public bool GetRegisterBit(int adress, int bit) {
      adress = CheckRegisterBank(adress);
      if (bit < 8 && bit > -1) {
        return (_ramArray[adress] & (1 << bit)) != 0;
      }
      return false;
    }

    private int CheckRegisterBank(int adress) {
      if ((_ramArray[(int)SFR.STATUS] & (1 << 5)) != 0) {
        adress += 0x80;
      }
      return adress;
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
