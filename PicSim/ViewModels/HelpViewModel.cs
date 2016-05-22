using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace PicSim.ViewModels {
  class HelpViewModel : PropertyChangedBase {
        public void OpenPDFFile()
        {     
                 
            System.Diagnostics.Process.Start("..\\..\\Doku.pdf");
                       
        }
    }
}
