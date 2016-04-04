using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using PicSim.ViewModels;

namespace PicSim {
  class AppBootstrapper : BootstrapperBase {
    public AppBootstrapper() {
      Initialize();
    }

    protected override void OnStartup(object sender, StartupEventArgs e) {
      DisplayRootViewFor<MainViewModel>();
    }
  }
}
