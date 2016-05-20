using System.Collections.Generic;
using Caliburn.Micro;

namespace PicSim.ViewModels {
  class StackViewModel : PropertyChangedBase{

    #region Fields

    private BindableCollection<string> _stack;

    #endregion //Fields

    #region Properties

    public BindableCollection<string> Stack {
      get {
        return _stack;
      }

      set {
        _stack = value;
        NotifyOfPropertyChange(() => Stack);
      }
    }

    #endregion //Properties

    #region Constructors

    public StackViewModel() {
      Stack = new BindableCollection<string>();
    }

    #endregion //Constructors

    #region Methods

    public void RefreshStack(Stack<int> stackModel) {
      Stack.Clear();
      foreach(var item in stackModel) {
        int correctData = item;
        Stack.Add(correctData.ToString());
      }
    }

    #endregion //Methods

  }
}
