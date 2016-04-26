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

		#endregion //Properties

		#region Constructors

		public RamModel() {
			_ramArray = new byte[0xFF];
      _stack = new Stack<int>();
		}

		#endregion //Constructors

		#region Methods

		public void SetRegisterValue(int adress, int value) {
			_ramArray[adress] = (byte)value;
		}

		public void SetRegisterValue(int value) {
			_wReg = (byte)value;
		}

		public int GetRegisterValue(int adress) {
			return Convert.ToInt32(_ramArray[adress]);
		}

		public int GetRegisterValue() {
			return Convert.ToInt32(_wReg);
		}

		public void ToggleRegisterBit(int adress, int bit, bool set) {
			if(bit < 8 && bit > -1) {
				byte mask = Convert.ToByte(0x01 << bit);
				byte value = _ramArray[adress];
				if (set) {					
					_ramArray[adress] = (byte)(value | mask);
				}
				else {
					_ramArray[adress] = (byte)(value & mask);
				}
			}					
		}

		public bool GetRegisterBit(int adress, int bit) {
			if (bit < 8 && bit > -1) {
				return (_ramArray[adress] & (1 << bit)) != 0;				
			}
			return false;
		}

    public void PushStack(int adress) {
      _stack.Push(adress);
    }

    public int PopStack() {
      return _stack.Pop();
    }

		#endregion //Methods
	}
}
