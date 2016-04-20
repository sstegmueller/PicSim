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
  class MainViewModel : Screen {

    #region Fields

    private string _windowTitle;
    private SimulationViewModel _simulation;
    private HelpViewModel _help;

    #endregion //Fields

    #region Properties

    public string WindowTitle {
      get {
        return _windowTitle;
      }

      set {
        _windowTitle = value;
        NotifyOfPropertyChange(() => WindowTitle);
      }
    }

    public SimulationViewModel Simulation {
      get {
        return _simulation;
      }

      set {
        _simulation = value;
        NotifyOfPropertyChange(() => Simulation);
      }
    }

    internal HelpViewModel Help {
      get {
        return _help;
      }

      set {
        _help = value;
        NotifyOfPropertyChange(() => Help);
      }
    }

    #endregion //Properties

    #region Constructors

    public MainViewModel() {
      WindowTitle = "PicSim";
      Simulation = new SimulationViewModel();
      Help = new HelpViewModel();
    }

    #endregion //Constructors

    #region Methods

    #endregion //Methods

  }
}
