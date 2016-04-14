using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim {
  static class ActionHelper {

    #region Methods

    public static string ToHexString(string value) {
      int intAgain = int.Parse(value);
      return String.Format("{0:X}", intAgain);
    }

    public static string ToHexString(int value) {
      return String.Format("{0:X}", value);
    }

		#endregion //Methods

	}
}
