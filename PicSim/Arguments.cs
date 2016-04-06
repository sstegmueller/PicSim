using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim {
  public class Arguments {

    #region Fields

    private bool _bool1;
    private int _byte1;
    private int _byte2;

    #endregion //Fields

    #region Properties

    public bool Bool1
    {
      get
      {
        return _bool1;
      }
    }

    public int Byte1
    {
      get
      {
        return _byte1;
      }
    }

    public int Byte2
    {
      get
      {
        return _byte2;
      }
    }

    #endregion //Properties

    #region Constructors

    public Arguments(bool d, int f) {
      _bool1 = d;
      _byte2 = f;
    }

    public Arguments(int b, int f) {
      _byte1 = b;
      _byte2 = f;
    }

    public Arguments(int f) {
      _byte1 = f;
    }

    #endregion //Constructors
  }
}
