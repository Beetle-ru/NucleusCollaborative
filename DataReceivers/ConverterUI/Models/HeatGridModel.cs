using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace ConverterUI.Models
{
    public class HeatGridModel : INPC
    {
        private ObservableCollection<AdditionModel> _additionsList;
        private ObservableCollection<DataGridColumn> _columnsList;
        private ObservableCollection<TableHeaderItem> _headerSteps;
        private ObservableCollection<TableRowModel> _itemsSource;
        private ObservableCollection<TableHeaderItem> _periods;
        private ObservableCollection<TableHeaderItem> _phases;
        private ObservableCollection<TableRowModel> _tableRows;

        /// <summary>
        /// Список материалов с весами и бункерами
        /// </summary>
        public ObservableCollection<AdditionModel> AdditionsList
        {
            get { return _additionsList; }
            set
            {
                _additionsList = value;
                OnPropertyChanged("AdditionsList");
            }
        }

        /// <summary>
        /// Список колонок для отображения корректного количества столбцов в гриде
        /// </summary>
        public ObservableCollection<DataGridColumn> ColumnsList
        {
            get { return _columnsList; }
            set
            {
                _columnsList = value;
                OnPropertyChanged("ColumnsList");
            }
        }

        /// <summary>
        /// Сюда биндится ItemsSource грида
        /// </summary>
        public ObservableCollection<TableRowModel> ItemsSource
        {
            get { return _itemsSource; }
            set
            {
                _itemsSource = value;
                OnPropertyChanged("ItemsSource");
            }
        }

        /// <summary>
        /// Значения для биндинга грида шаблона плавки.
        /// Значение присваивается свойству ItemsSource в нужный момент.
        /// </summary>
        public ObservableCollection<TableRowModel> TableRows
        {
            get { return _tableRows; }
            set
            {
                _tableRows = value;
                OnPropertyChanged("TableRows");
            }
        }

        /// <summary>
        /// Список для заполнения строки заголовка таблицы с периодами плавки
        /// </summary>
        public ObservableCollection<TableHeaderItem> Periods
        {
            get { return _periods; }
            set
            {
                _periods = value;
                OnPropertyChanged("Periods");
            }
        }

        /// <summary>
        /// Список для заполнения строки заголовка таблицы с этапами плавки
        /// </summary>
        public ObservableCollection<TableHeaderItem> Phases
        {
            get { return _phases; }
            set
            {
                _phases = value;
                OnPropertyChanged("Phases");
            }
        }

        public ObservableCollection<TableHeaderItem> HeaderSteps
        {
            get { return _headerSteps; }
            set
            {
                _headerSteps = value;
                OnPropertyChanged("HeaderSteps");
            }
        }
    }

    /// <summary>
    /// Добавка (используется для заполнения левой инф. панели грида)
    /// </summary>
    public sealed class AdditionModel : INPC
    {
        private bool _added;
        private string _bunker;
        private int _correction;
        private string _name;
        private string _weighterLine;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string WeighterLine
        {
            get { return _weighterLine; }
            set
            {
                _weighterLine = value;
                OnPropertyChanged("WeighterLine");
            }
        }

        public string Bunker
        {
            get { return _bunker; }
            set
            {
                _bunker = value;
                OnPropertyChanged("Bunker");
            }
        }

        public int Correction
        {
            get { return _correction; }
            set
            {
                _correction = value;
                OnPropertyChanged("Correction");
            }
        }

        public bool Added
        {
            get { return _added; }
            set
            {
                _added = value;
                OnPropertyChanged("Correction");
            }
        }
    }

    /// <summary>
    /// Инф. модель строки грида. К коллекции обьектов этого класса биндятся столбцы грида
    /// </summary>
    public sealed class TableRowModel : INPC
    {
        private TableCell[] _values;

        public TableCell[] Values
        {
            get { return _values; }
            set
            {
                _values = value;
                OnPropertyChanged("Values");
            }
        }
    }

    /// <summary>
    /// Инф. модель ячейки грида. К коллекции обьектов этого класса биндятся столбцы грида
    /// </summary>
    public sealed class TableCell : INPC
    {
        private bool _allowToAdd;
        private SolidColorBrush _background;
        private string _cellValue;
        private bool _notToGive;

        public TableCell()
        {
            Del d = delegate { Background = new SolidColorBrush(Colors.Red); };
            Dispatcher.CurrentDispatcher.BeginInvoke(d, DispatcherPriority.Render);
        }

        public string CellValue
        {
            get { return _cellValue; }
            set
            {
                _cellValue = value;
                OnPropertyChanged("CellValue");
            }
        }

        public bool NotToGive
        {
            get { return _notToGive; }
            set
            {
                _notToGive = value;
                if (value)
                    Background = new SolidColorBrush(Colors.Red);
                OnPropertyChanged("NotToGive");
            }
        }

        public bool AllowToAdd
        {
            get { return _allowToAdd; }
            set
            {
                _allowToAdd = value;
                if (value)
                    Background = new SolidColorBrush(Colors.Yellow);
                OnPropertyChanged("AllowToAdd");
            }
        }

        public SolidColorBrush Background
        {
            get { return _background; }
            set
            {
                _background = value;
                OnPropertyChanged("Background");
            }
        }

        #region Nested type: Del

        private delegate void Del();

        #endregion
    }

    /// <summary>
    /// Инф. модель графы заголовка грида. Используется для наполнения строк с периодами, этапами и шагами
    /// </summary>
    public sealed class TableHeaderItem : INPC
    {
        private SolidColorBrush _color;
        private string _title;
        private double _width;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
                OnPropertyChanged("Width");
            }
        }

        public SolidColorBrush Color
        {
            get { return _color; }
            set
            {
                _color = value;
                OnPropertyChanged("Color");
            }
        }
    }
}