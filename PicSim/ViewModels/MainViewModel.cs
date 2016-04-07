using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using PicSim.Models;

namespace PicSim.ViewModels {
  class MainViewModel : Screen {

    #region Fields

    private string _windowTitle;
    private string _openFileContent;
    private string _fileNameContent;
    private string _operationIndex;
    private string _operationBreak;
    private string _operationName;
    private string _operationArg1;
    private string _operationArg2;
    private ProgramModel _progModel;
    private BindableCollection<OperationViewModel> _operations;

    #endregion //Fields

    #region Properties

    public string OpenFileContent
    {
      get
      {
        return _openFileContent;
      }

      set
      {
        _openFileContent = value;
        NotifyOfPropertyChange(() => OpenFileContent);
      }
    }

    public string FileNameContent
    {
      get
      {
        return _fileNameContent;
      }

      set
      {
        _fileNameContent = value;
        NotifyOfPropertyChange(() => FileNameContent);
      }
    }

    public string WindowTitle
    {
      get
      {
        return _windowTitle;
      }

      set
      {
        _windowTitle = value;
        NotifyOfPropertyChange(() => WindowTitle);
      }
    }

    public BindableCollection<OperationViewModel> Operations
    {
      get
      {
        return _operations;
      }

      set
      {
        _operations = value;
        NotifyOfPropertyChange(() => Operations);
      }
    }

    public string OperationIndex
    {
      get
      {
        return _operationIndex;
      }

      set
      {
        _operationIndex = value;
        NotifyOfPropertyChange(() => OperationIndex);
      }
    }

    public string OperationBreak
    {
      get
      {
        return _operationBreak;
      }

      set
      {
        _operationBreak = value;
        NotifyOfPropertyChange(() => OperationBreak);
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
        _operationArg1 = value;
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
        _operationArg2 = value;
        NotifyOfPropertyChange(() => OperationArg2);
      }
    }

    #endregion //Properties

    #region Constructors

    public MainViewModel() {
      WindowTitle = "PicSim";
      OpenFileContent = "Open File";
      OperationIndex = "Index";
      OperationBreak = "Breakpoint";
      OperationName = "Operation";  
      OperationArg1 = "Argument 1";
      OperationArg2 = "Argument 2";
      Operations = new BindableCollection<OperationViewModel>();
    }

    #endregion //Constructors

    #region Methods

    private void ShowOperations() {
      if (Operations.Count != 0) {
        Operations.Clear();
      }
      foreach (OperationModel op in _progModel.Operations) {
        if (OperationType.ByteOrientedFD.HasFlag((OperationType)op.Operation)) {
          Operations.Add(new OperationViewModel(op.Index.ToString(),
                                                op.Operation.ToString(),
                                                op.Args.Bool1.ToString(),
                                                op.Args.Byte2.ToString()));
          continue;
        }
        if (OperationType.ByteOrientedF.HasFlag((OperationType)op.Operation) || OperationType.LiteralControl.HasFlag((OperationType)op.Operation)) {
          Operations.Add(new OperationViewModel(op.Index.ToString(),
                                                op.Operation.ToString(),
                                                op.Args.Byte1.ToString()));
          continue;
        }
        if (OperationType.BitOriented.HasFlag((OperationType)op.Operation)) {
          Operations.Add(new OperationViewModel(op.Index.ToString(),
                                                op.Operation.ToString(),
                                                op.Args.Byte1.ToString(),
                                                op.Args.Byte2.ToString()));
          continue;
        }
      }
    }

    public void OpenFile() {
      // Create OpenFileDialog 
      Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



      // Set filter for file extension and default file extension 
      dlg.DefaultExt = ".lst";
      dlg.Filter = "LST Files (*.lst)|*.lst";


      // Display OpenFileDialog by calling ShowDialog method 
      Nullable<bool> result = dlg.ShowDialog();


      // Get the selected file name and display in a TextBox 
      if (result == true) {
        // Open document 
        FileNameContent = dlg.SafeFileName;
        _progModel = new ProgramModel(dlg.FileName);
        ShowOperations();
      }      
    }

    #endregion //Methods

  }
}
