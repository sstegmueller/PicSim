using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace PicSim.ViewModels {
  class OperationViewModel : PropertyChangedBase {

    #region Fields

    private bool _breakPoint;
    private string _operationName;
    private string _operationArgs;

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

    public string OperationArgs
    {
      get
      {
        return _operationArgs;
      }

      set
      {
        _operationArgs = value;
        NotifyOfPropertyChange(() => OperationArgs);
      }
    }

    #endregion //Properties

    #region Constructors

    public OperationViewModel(string opName, string args) {
      OperationName = opName;
      OperationArgs = args;
    }

    #endregion //Constructors

    #region Methods

    #endregion //Methods

  }
}
