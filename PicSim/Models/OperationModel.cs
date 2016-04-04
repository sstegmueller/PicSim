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

      set
      {
        _index = value;
      }
    }

    internal Operation Operation
    {
      get
      {
        return _operation;
      }

      set
      {
        _operation = value;
      }
    }

    internal Arguments Args
    {
      get
      {
        return _args;
      }

      set
      {
        _args = value;
      }
    }

    #endregion //Properties

    #region Constructors

    public OperationModel(int index, Operation operation) {
      Index = index;
      Operation = operation;
    }

    #endregion //Constructors

    #region Methods

    #endregion //Methods

  }
}
