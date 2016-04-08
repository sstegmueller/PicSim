using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace PicSim.ViewModels {
  class RamViewModel : PropertyChangedBase{

    #region Fields

    private BindableCollection<string> _rowHeaders;
    private BindableCollection<string> _columnHeaders;

    #endregion //Fields

    #region Properties

    public BindableCollection<string> RowHeaders
    {
      get
      {
        return _rowHeaders;
      }

      set
      {
        _rowHeaders = value;
        NotifyOfPropertyChange(() => RowHeaders);
      }
    }

    public BindableCollection<string> ColumnHeaders
    {
      get
      {
        return _columnHeaders;
      }

      set
      {
        _columnHeaders = value;
        NotifyOfPropertyChange(() => ColumnHeaders);
      }
    }

    #endregion //Properties

    #region Constructors

    public RamViewModel() {
      RowHeaders = new BindableCollection<string>();
      AddRowHeaderValues();

      ColumnHeaders = new BindableCollection<string>();
      AddColumnHeaderValues();
    }

    #endregion //Constructors

    #region Methods

    private void AddRowHeaderValues() {
      int rowHeaderIndex = 0;
      for (int row = 0; row < 32; row++) {
        RowHeaders.Add(ConvertHelper.ToHexString(rowHeaderIndex));
        rowHeaderIndex += 8;
      }
    }

    private void AddColumnHeaderValues() {
      int columnHeaderIndex = 0;
      for (int row = 0; row < 8; row++) {
        ColumnHeaders.Add("0" + ConvertHelper.ToHexString(columnHeaderIndex));
        columnHeaderIndex++;
      }
    }

    #endregion //Methods

  }
}
