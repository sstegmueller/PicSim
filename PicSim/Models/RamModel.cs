using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Models {
  class RamModel {

		#region Fields

		private byte[] _ramArray;

		#endregion //Fields

		#region Properties
	
		public byte[] RamArray {
			get {
				return _ramArray;
			}
		}

		#endregion //Properties

		#region Constructors

		public RamModel() {
			_ramArray = new byte[0xFF];
		}

		#endregion //Constructors

		#region Methods

		public void SetRegisterValue(int adress, int value) {
			_ramArray[adress] = Convert.ToByte(value);
		}

		public int GetRegisterValue(int adress) {
			return Convert.ToInt32(_ramArray[adress]);
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

		#endregion //Methods
	}
}
