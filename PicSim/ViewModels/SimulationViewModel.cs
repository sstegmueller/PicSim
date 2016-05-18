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
    private bool _openFileIsEnabled = true;
    private string _fileNameContent;
    private BindableCollection<OperationViewModel> _operations;
    private string _operationIndex;
    private string _operationBreak;
    private string _operationName;
    private string _operationArg1;
    private string _operationArg2;
    private ProgramModel _progModel;
    private RamViewModel _ramVM;
    private SfrViewModel _sFRVM;
    private StackViewModel _stackVM;
    private TimerViewModel _timerVM;
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

    public bool OpenFileIsEnabled {
      get {
        return _openFileIsEnabled;
      }

      set {
        _openFileIsEnabled = value;
        NotifyOfPropertyChange(() => OpenFileIsEnabled);
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

    public SfrViewModel SfrVM {
      get {
        return _sFRVM;
      }

      set {
        _sFRVM = value;
        NotifyOfPropertyChange(() => SfrVM);
      }
    }

    public StackViewModel StackVM {
      get {
        return _stackVM;
      }

      set {
        _stackVM = value;
        NotifyOfPropertyChange(() => StackVM);
      }
    }

    public TimerViewModel TimerVM {
      get {
        return _timerVM;
      }

      set {
        _timerVM = value;
        NotifyOfPropertyChange(() => TimerVM);
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
      StackVM = new StackViewModel();
      TimerVM = new TimerViewModel();
      _worker.DoWork += worker_RunProgram;
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
      StackVM.RefreshStack(_progModel.Ram.Stack);
      TimerVM.RefreshTime(_progModel.Cycles);
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

    private void CheckWatchdog() {
      if (_progModel.WatchdogAlert && _progModel.Sleeps) {
        TimeoutReset();
      }
      else if (_progModel.WatchdogAlert) {
        WatchdogReset();
      }
    }

    private void worker_RunProgram(object sender, DoWorkEventArgs e) {
      if (_progModel.GetOpByIndex(_progModel.ProgCounter).IsBreak &&
        _progModel.ProgCounter <= _progModel.Operations.Last().Index) {
        UseCommand();
        CheckWatchdog();
      }
      while (_progModel.ProgCounter <= _progModel.Operations.Last().Index &&
            !_progModel.GetOpByIndex(_progModel.ProgCounter).IsBreak) {
        UseCommand();
        CheckWatchdog();
        if (_worker.CancellationPending) {
          e.Cancel = true;
          return;
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
        _progModel = new ProgramModel(dlg.FileName, Convert.ToDouble(TimerVM.CrystalFrequency));
        _sFRVM.GiveRamModel(_progModel.Ram);
        ShowOperations();
        RefreshVMs();
        CanStep = true;
      }
    }

    public void Start() {
      OpenFileIsEnabled = false;
      _worker.RunWorkerAsync();
    }

    public void Step() {
      if (_progModel.ProgCounter <= _progModel.Operations.Last().Index) {
        UseCommand();
        CheckWatchdog();
      }
    }

    public void Stop() {
      OpenFileIsEnabled = true;
      ResetDevice();
      _worker.CancelAsync();
    }

    private void ResetDevice() {
      _progModel.Ram = new RamModel();
      _progModel.ProgCounter = 0;
      _progModel.Cycles = 0;
      _progModel.Watchdog = 0;
      BrushCurrentOp();
      RefreshVMs();
    }

    private void WatchdogReset() {
      _progModel.ProgCounter = 0;
      int mask = 0x07;
      int tempStatusRegister = _progModel.Ram.GetRegisterValue((int)SFR.STATUS) & mask;
      int newStatusRegister = tempStatusRegister | 0x08;
      _progModel.Ram.DirectSetRegisterValue((int)SFR.STATUS, newStatusRegister);
      _progModel.Ram.DirectSetRegisterValue((int)SFR.OPTION_REG, 0xFF);
      _progModel.Ram.DirectSetRegisterValue((int)SFR.TRISA, 0xFF);
      _progModel.Ram.DirectSetRegisterValue((int)SFR.TRISB, 0xFF);
      _progModel.Ram.DirectSetRegisterValue((int)SFR.PCLATH, 0);
      int newIntconRegister = _progModel.Ram.GetRegisterValue((int)SFR.INTCON) & 0x01;
      _progModel.Ram.DirectSetRegisterValue((int)SFR.INTCON, newIntconRegister);
      _progModel.WatchdogAlert = false;
    }

    private void TimeoutReset() {
      _progModel.Ram.DirectToggleRegisterBit((int)SFR.STATUS, 3, false);
      _progModel.Ram.DirectToggleRegisterBit((int)SFR.STATUS, 4, false);
      _progModel.Sleeps = false;
      _progModel.WatchdogAlert = false;
    }

    #endregion //Methods
  }
}
