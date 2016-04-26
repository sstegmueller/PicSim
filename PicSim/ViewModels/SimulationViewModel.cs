using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using PicSim.Models;
using System.ComponentModel;
using System.Windows.Media;

namespace PicSim.ViewModels {
  class SimulationViewModel : Screen {

    #region Fields

    private string _openFileContent;
    private string _fileNameContent;
    private string _operationIndex;
    private string _operationBreak;
    private string _operationName;
    private string _operationArg1;
    private string _operationArg2;
    private ProgramModel _progModel;
    private BindableCollection<OperationViewModel> _operations;
    private RamViewModel _ramVM;
    private SfrViewModel _sFRVM;
    private string _ramName;
    private readonly BackgroundWorker _worker = new BackgroundWorker();
    private bool _canStep;

    #endregion //Fields

    #region Properties

    public string OpenFileContent {
      get {
        return _openFileContent;
      }

      set {
        _openFileContent = value;
        NotifyOfPropertyChange(() => OpenFileContent);
      }
    }

    public string FileNameContent {
      get {
        return _fileNameContent;
      }

      set {
        _fileNameContent = value;
        NotifyOfPropertyChange(() => FileNameContent);
      }
    }
    
    public BindableCollection<OperationViewModel> Operations {
      get {
        return _operations;
      }

      set {
        _operations = value;
        NotifyOfPropertyChange(() => Operations);
      }
    }

    public string OperationIndex {
      get {
        return _operationIndex;
      }

      set {
        _operationIndex = value;
        NotifyOfPropertyChange(() => OperationIndex);
      }
    }

    public string OperationBreak {
      get {
        return _operationBreak;
      }

      set {
        _operationBreak = value;
        NotifyOfPropertyChange(() => OperationBreak);
      }
    }

    public string OperationName {
      get {
        return _operationName;
      }

      set {
        _operationName = value;
        NotifyOfPropertyChange(() => OperationName);
      }
    }

    public string OperationArg1 {
      get {
        return _operationArg1;
      }

      set {
        _operationArg1 = value;
        NotifyOfPropertyChange(() => OperationArg1);
      }
    }

    public string OperationArg2 {
      get {
        return _operationArg2;
      }

      set {
        _operationArg2 = value;
        NotifyOfPropertyChange(() => OperationArg2);
      }
    }

    public RamViewModel RamVM {
      get {
        return _ramVM;
      }

      set {
        _ramVM = value;
        NotifyOfPropertyChange(() => RamVM);
      }
    }

    public string RamName {
      get {
        return _ramName;
      }

      set {
        _ramName = value;
        NotifyOfPropertyChange(() => RamName);
      }
    }

    public bool CanStep {
      get {
        return _canStep;
      }

      set {
        _canStep = value;
        NotifyOfPropertyChange(() => CanStep);
      }
    }

    public SfrViewModel SfrVM {
      get {
        return _sFRVM;
      }

      set {
        _sFRVM = value;
        NotifyOfPropertyChange(() => SfrVM);
      }
    }

    #endregion //Properties

    #region Constructors

    public SimulationViewModel() {
      OpenFileContent = "Open File";
      RamName = "RAM";
      OperationIndex = "Index";
      OperationBreak = "Breakpoint";
      OperationName = "Operation";
      OperationArg1 = "Argument 1";
      OperationArg2 = "Argument 2";
      Operations = new BindableCollection<OperationViewModel>();
      RamVM = new RamViewModel();
      SfrVM = new SfrViewModel();
      _worker.DoWork += worker_DoWork;
      _worker.WorkerSupportsCancellation = true;
    }

    #endregion //Constructors

    #region Methods

    private void ShowOperations() {
      if (Operations.Count != 0) {
        Operations.Clear();
      }
      foreach (OperationModel op in _progModel.Operations) {
        Operations.Add(new OperationViewModel(op));
      }
    }

    private void UseCommand() {
      _progModel.ExecuteCommand(_progModel.ProgCounter);
      BrushCurrentOp();
      RefreshVMs();
    }

    private void RefreshVMs() {
      RamVM.RefreshDataTable(_progModel.Ram.RamArray);
      SfrVM.RefreshSfr(_progModel.Ram, _progModel.ProgCounter);
    }

    private void BrushCurrentOp() {
      foreach (OperationViewModel op in Operations) {
        if (Convert.ToInt32(op.Index, 16) == _progModel.ProgCounter) {
          op.Background = Brushes.LightBlue;
        }
        else {
          op.Background = Brushes.White;
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
        _sFRVM.GiveRamModel(_progModel.Ram);
        ShowOperations();
        CanStep = true;
      }
    }

    public void Start() {
      _worker.RunWorkerAsync();
    }

    private void worker_DoWork(object sender, DoWorkEventArgs e) {
      if (_progModel.GetOpByIndex(_progModel.ProgCounter).IsBreak &&
        _progModel.ProgCounter < _progModel.Operations.Last().Index) {
        UseCommand();
      }
      while (_progModel.ProgCounter < _progModel.Operations.Last().Index &&
            !_progModel.GetOpByIndex(_progModel.ProgCounter).IsBreak) {
        UseCommand();
      }
    }

    public void Step() {
      if (_progModel.ProgCounter < _progModel.Operations.Last().Index) {
        UseCommand();
      }
    }

    public void Stop() {
      _progModel.Ram = new RamModel();
      RamVM = new RamViewModel();
      _progModel.ProgCounter = 0;
      BrushCurrentOp();
      RefreshVMs();
    }
    #endregion //Methods
  }
}
