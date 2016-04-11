using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace PicSim.ViewModels {
  class OperationViewModel : PropertyChangedBase {

    #region Fields

    private string _index;
    private bool _breakPoint;
    private string _operationName;
    private string _operationArg1;
    private string _operationArg2;

    #endregion //Fields

    #region Properties

    public bool BreakPoint
    {
      get
      {
        return _breakPoint;
      }

      set
      {
        _breakPoint = value;
        NotifyOfPropertyChange(() => BreakPoint);
      }
    }

    public string OperationName
    {
      get
      {
        return _operationName;
      }

      set
      {
        _operationName = value;
        NotifyOfPropertyChange(() => OperationName);
      }
    }

    public string OperationArg1
    {
      get
      {
        return _operationArg1;
      }

      set
      {
        if (value != "True" && value != "False") {
          _operationArg1 = ConvertHelper.ToHexString(value);
        }
        else {
          _operationArg1 = value;
        }
        NotifyOfPropertyChange(() => OperationArg1);
      }
    }

    public string OperationArg2
    {
      get
      {
        return _operationArg2;
      }

      set
      {
        _operationArg2 = ConvertHelper.ToHexString(value);
        NotifyOfPropertyChange(() => OperationArg2);
      }
    }

    public string Index
    {
      get
      {
        return _index;
      }

      set
      {
        _index = ConvertHelper.ToHexString(value);
        NotifyOfPropertyChange(() => Index);
      }
    }

    #endregion //Properties

    #region Constructors

    public OperationViewModel(string index, string opName) {
      Index = index;
      OperationName = opName;
    }

    public OperationViewModel(string index, string opName, string arg) {
      Index = index;
      OperationName = opName;
      OperationArg1 = arg;
    }

    public OperationViewModel(string index, string opName, string arg1, string arg2) {
      Index = index;
      OperationName = opName;
      OperationArg1 = arg1;
      OperationArg2 = arg2;
    }

    #endregion //Constructors

    #region Methods

    #endregion //Methods

  }
}
