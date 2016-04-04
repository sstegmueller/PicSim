using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using PicSim.Models;

namespace PicSim.ViewModels {
  class MainViewModel : Caliburn.Micro.Screen {

    #region Fields

    private string _openFileContent;
    private string _fileNameContent;

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

    #endregion //Properties

    #region Constructors

    public MainViewModel() {
      OpenFileContent = "Open File";
    }

    #endregion //Constructors

    #region Methods

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
        ProgramModel progModel = new ProgramModel(dlg.FileName);
      }      
    }

    #endregion //Methods

  }
}
