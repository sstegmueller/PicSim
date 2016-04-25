using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using PicSim.Models;
using System.Windows.Media;

namespace PicSim.ViewModels {
  class OperationViewModel : PropertyChangedBase {

    #region Fields

    private string _index;
    private bool _isChecked;
    private string _operationName;
    private string _operationArg1;
    private string _operationArg2;
		private OperationModel _opModel;
		private Brush _background;

    #endregion //Fields

    #region Properties

    public bool IsChecked
    {
      get
      {
        return _isChecked;
      }

      set
      {
        _isChecked = value;
        NotifyOfPropertyChange(() => IsChecked);
				_opModel.IsBreak = value;
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
          _operationArg1 = Tools.ToHexString(value);
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
        _operationArg2 = Tools.ToHexString(value);
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
        _index = Tools.ToHexString(value);
        NotifyOfPropertyChange(() => Index);
      }
    }

		public Brush Background {
			get {
				return _background;
			}

			set {
				_background = value;
				NotifyOfPropertyChange(() => Background);
			}
		}

		#endregion //Properties

		#region Constructors

		public OperationViewModel(OperationModel opModel) {
			_opModel = opModel;
			IsChecked = false;
			if (opModel.OpType == OperationType.ByteOrientedFD) {
				Index = opModel.Index.ToString();
				OperationName = opModel.Operation.ToString();
				OperationArg1 = opModel.Args.Bool1.ToString();
				OperationArg2 = opModel.Args.Byte2.ToString();

			}
			if (opModel.OpType == OperationType.ByteOrientedF || opModel.OpType == OperationType.LiteralControl) {
				Index = opModel.Index.ToString();
				OperationName = opModel.Operation.ToString();
				OperationArg1 = opModel.Args.Byte1.ToString();
			}
			if (opModel.OpType == OperationType.BitOriented) {
				Index = opModel.Index.ToString();
				OperationName = opModel.Operation.ToString();
				OperationArg1 = opModel.Args.Byte1.ToString();
				OperationArg2 = opModel.Args.Byte2.ToString();
			}
			if (opModel.OpType == OperationType.NoArgs) {
				Index = opModel.Index.ToString();
				OperationName = opModel.Operation.ToString();
			}			
		}

    #endregion //Constructors

    #region Methods

    #endregion //Methods

  }
}
