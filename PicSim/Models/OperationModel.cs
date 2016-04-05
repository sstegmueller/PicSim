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
    private OperationType _operationType;
    private Argument _args;

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

    public Argument Args
    {
      get
      {
        return _args;
      }
      set
      {
        _args = Args;
      }
    }

    public OperationType OperationType
    {
      get
      {
        return _operationType;
      }
      set
      {
        _operationType = OperationType;
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

    #endregion //Methods

  }
}
