using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Models {
  class OperationModel {

    #region Fields

    private bool _isBreak;
    private int _index;
    private Operation _operation;
    private Arguments _args;

    #endregion //Fields

    #region Properties

    public bool IsBreak
    {
      get
      {
        return _isBreak;
      }

      set
      {
        _isBreak = value;
      }
    }

    public int Index
    {
      get
      {
        return _index;
      }
    }

    public Operation Operation
    {
      get
      {
        return _operation;
      }
    }

    public Arguments Args
    {
      get
      {
        return _args;
      }
    }
    
    #endregion //Properties

    #region Constructors
    
    public OperationModel(int index, Operation operation) {
      _index = index;
      _operation = operation;
    }

    #endregion //Constructors

    #region Methods
    
    public void SetArgs(bool d, int f) {
      _args = new Arguments(d, f);
    }

    public void SetArgs(int b, int f) {
      _args = new Arguments(b, f);
    }

    public void SetArgs(int f) {
      _args = new Arguments(f);
    }
 
    #endregion //Methods

  }
}
