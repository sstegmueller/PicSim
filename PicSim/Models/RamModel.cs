using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Models {
  class RamModel {

		#region Fields

		private byte[] _ram;

		#endregion //Fields

		#region Properties
		public byte[] Ram {
			get {
				return _ram;
			}

			set {
				_ram = value;
			}
		}

		#endregion //Properties

		#region Constructors

		public RamModel() {
			Ram = new byte[0xFF];
		}

		#endregion //Constructors

		#region Methods

		public void SetRegisterValue(int adress, int value) {
			Ram[adress] = Convert.ToByte(value);
		}

		public int GetRegisterValue(int adress) {
			return Convert.ToInt32(Ram[adress]);
		}

		public void ToggleRegisterBit(int adress, int bit, bool set) {
			if (set && bit < 8 && bit > -1) {
				byte mask = Convert.ToByte(0x01 << bit);
				byte value = Ram[adress];
				Ram[adress] = (byte)(value | mask);
			}			
		}

		#endregion //Methods
	}
}
