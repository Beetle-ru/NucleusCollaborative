using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using CommonTypes;
using ConnectionProvider;
using Converter;
using ConverterUI.Models;
using ConverterUI.Util;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Heat = ConverterUI.Models.Heat;

namespace ConverterUI.ViewModels.MainPage
{
    public sealed partial class MainPageViewModel
    {
        private Client _mainGate;
        private HeatModelContainer _ctx;
        public EventsStack EventsStack;
        private Heat _currentHeat;
        private List<Step> _steps;
        public static bool IsListening;
        private bool _reloadTemplateAfterHeat;
        private bool _canColorSteps;


        #region Листбоксы выбора шаблонов

        private bool _groupSelectingEnabled;
        private ObservableCollection<HeatGroupModel> _groups;
        private HeatGroupModel _selectedGroup;
        private string _selectedTemplate;


        public bool ReloadTemplateAfterHeat
        {
            get { return _reloadTemplateAfterHeat; }
            set { _reloadTemplateAfterHeat = value; OnPropertyChanged("ReloadTemplateAfterHeat"); }
        }

        /// <summary>
        /// Список групп шаблонов для списка выбора шаблона
        /// </summary>
        public ObservableCollection<HeatGroupModel> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                OnPropertyChanged("Groups");
            }
        }

        /// <summary>
        /// Выбранная группа шаблонов в списке
        /// </summary>
        public HeatGroupModel SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                if (_selectedGroup == value)
                {
                    return;
                }

                _selectedGroup = value;
                OnPropertyChanged("SelectedGroup");
                if (value.Templates.Count > 0)
                {
                    SelectedTemplate = value.Templates[0];

                    TargetValues t = _ctx.TargetValues.First(i => i.GroupID == SelectedGroup.Id);
                    TwC = t.C;
                    TwCaO = t.CaO;
                    TwCaOSio2 = t.CaOSiO2;
                    TwCr = t.Cr;
                    TwCu = t.Cu;
                    TwFeO = t.FeO;
                    TwMgO = t.MgO;
                    TwMn = t.Mn;
                    TwNi = t.Ni;
                    TwP = t.P;
                    TwS = t.S;
                    TwT = t.T;

                }
            }
        }

        public bool GroupSelectingEnabled
        {
            get { return _groupSelectingEnabled; }
            set
            {
                _groupSelectingEnabled = value;
                OnPropertyChanged("GroupSelectingEnabled");
            }
        }


        /// <summary>
        /// Выбранный шаблон в списке
        /// </summary>
        public string SelectedTemplate
        {
            get { return _selectedTemplate; }
            set
            {
                if (_selectedTemplate == value)
                {
                    return;
                }

                _selectedTemplate = value;
                OnPropertyChanged("SelectedTemplate");

                if (!String.IsNullOrEmpty(value))
                {
                    _canSaveTV = false;
                    var T = new Thread(LoadTemplate) { IsBackground = true };
                    T.Start();
                }
            }
        }

        #endregion

        #region Грид

        private HeatGridModel _hgModel;

        public HeatGridModel HGModel
        {
            get { return _hgModel; }
            set
            {
                _hgModel = value;
                OnPropertyChanged("HGModel");
            }
        }

        #endregion

        #region Графики, режимы

        private RawDataSource _lineGraph;
        private RawDataSource _lineGraphRealTime;
        private RawDataSource _cGraph;
        private RawDataSource _siGraph;
        private RawDataSource _mnGraph;

        private List<Point> RTPointsList;
        private List<Point> СPointsList;
        private List<Point> SiPointsList;
        private List<Point> MnPointsList;

        private int LastTotalO2;
        private int LastLancePosition;


        private Visibility _tractRegimeAutoVisibility;
        private Visibility _tractRegimeManualVisibility;
        private Visibility _lanceRegimeManualVisibility;
        private Visibility _lanceRegimeAutoVisibility;
        private Visibility _regClapanManualVisibility;
        private Visibility _regClapanAutoVisibility;

        public RawDataSource LineGraph
        {
            get { return _lineGraph; }
            set
            {
                _lineGraph = value;
                OnPropertyChanged("LineGraph");
            }
        }

        public RawDataSource CGraph
        {
            get { return _cGraph; }
            set
            {
                _cGraph = value;
                OnPropertyChanged("CGraph");
            }
        }

        public RawDataSource SiGraph
        {
            get { return _siGraph; }
            set
            {
                _siGraph = value;
                OnPropertyChanged("SiGraph");
            }
        }
        public RawDataSource MnGraph
        {
            get { return _mnGraph; }
            set
            {
                _mnGraph = value;
                OnPropertyChanged("MnGraph");
            }
        }

        private RawDataSource _pGraph;
        private List<Point> PPointsList;
        public RawDataSource PGraph
        {
            get { return _pGraph; }
            set
            {
                _pGraph = value;
                OnPropertyChanged("PGraph");
            }
        }
        private int PModelEventCounter = 0;

        private RawDataSource _feGraph;
        private List<Point> FePointsList;
        public RawDataSource FeGraph
        {
            get { return _feGraph; }
            set
            {
                _feGraph = value;
                OnPropertyChanged("FeGraph");
            }
        }
        private int FeModelEventCounter = 0;

        private RawDataSource _feOGraph;
        private List<Point> FeOPointsList;
        public RawDataSource FeOGraph
        {
            get { return _feOGraph; }
            set
            {
                _feOGraph = value;
                OnPropertyChanged("FeOGraph");
            }
        }
        private int FeOModelEventCounter = 0;

        private RawDataSource _caOGraph;
        private List<Point> CaOPointsList;
        public RawDataSource CaOGraph
        {
            get { return _caOGraph; }
            set
            {
                _caOGraph = value;
                OnPropertyChanged("CaOGraph");
            }
        }
        private int CaOModelEventCounter = 0;

        private RawDataSource _siO2Graph;
        private List<Point> SiO2PointsList;
        public RawDataSource SiO2Graph
        {
            get { return _siO2Graph; }
            set
            {
                _siO2Graph = value;
                OnPropertyChanged("SiO2Graph");
            }
        }
        private int SiO2ModelEventCounter = 0;

        private RawDataSource _mnOGraph;
        private List<Point> MnOPointsList;
        public RawDataSource MnOGraph
        {
            get { return _mnOGraph; }
            set
            {
                _mnOGraph = value;
                OnPropertyChanged("MnOGraph");
            }
        }
        private int MnOModelEventCounter = 0;

        private RawDataSource _mgOGraph;
        private List<Point> MgOPointsList;
        public RawDataSource MgOGraph
        {
            get { return _mgOGraph; }
            set
            {
                _mgOGraph = value;
                OnPropertyChanged("MgOGraph");
            }
        }
        private int MgOModelEventCounter = 0;

        private int CModelEventCounter = 0;
        private int SiModelEventCounter = 0;
        private int MnModelEventCounter = 0;


        public RawDataSource LineGraphRealTime
        {
            get { return _lineGraphRealTime; }
            set
            {
                _lineGraphRealTime = value;
                OnPropertyChanged("LineGraphRealTime");
            }
        }

        public IPointDataSource TemplateMatherialsReleaseGraph
        {
            get { return _templateMatherialsReleaseGraph; }
            set { _templateMatherialsReleaseGraph = value; OnPropertyChanged("TemplateMatherialsReleaseGraph"); }
        }

        public Visibility TractRegimeManualVisibility
        {
            get { return _tractRegimeManualVisibility; }
            set
            {
                _tractRegimeManualVisibility = value;
                OnPropertyChanged("TractRegimeManualVisibility");
            }
        }

        public Visibility TractRegimeAutoVisibility
        {
            get { return _tractRegimeAutoVisibility; }
            set
            {
                _tractRegimeAutoVisibility = value;
                OnPropertyChanged("TractRegimeAutoVisibility");
            }
        }

        public Visibility LanceRegimeManualVisibility
        {
            get { return _lanceRegimeManualVisibility; }
            set { _lanceRegimeManualVisibility = value; OnPropertyChanged("LanceRegimeManualVisibility"); }
        }

        public Visibility LanceRegimeAutoVisibility
        {
            get { return _lanceRegimeAutoVisibility; }
            set { _lanceRegimeAutoVisibility = value; OnPropertyChanged("LanceRegimeAutoVisibility"); }
        }

        public Visibility RegClapanManualVisibility
        {
            get { return _regClapanManualVisibility; }
            set { _regClapanManualVisibility = value; OnPropertyChanged("RegClapanManualVisibility"); }
        }

        public Visibility RegClapanAutoVisibility
        {
            get { return _regClapanAutoVisibility; }
            set
            {
                _regClapanAutoVisibility = value;
                OnPropertyChanged("RegClapanAutoVisibility");
            }
        }

        private string _cantStartHeatMessageText;
        public string CantStartHeatMessageText
        {
            get { return _cantStartHeatMessageText; }
            set { _cantStartHeatMessageText = value; OnPropertyChanged("CantStartHeatMessageText"); }
        }

        private bool _showRealO2;
        public bool ShowRealO2
        {
            get { return _showRealO2; }
            set
            {
                _showRealO2 = value;
                //if (value)
                //{
                //    if (LineGraphRealTime != null)
                //    {
                //        LineGraphRealTime.Data = null;
                //        LineGraphRealTime.RaiseDataChanged();
                //    }

                //}
                //else
                //{
                //    if (RTPointsList != null)
                //    {
                //        var ds = new RawDataSource(RTPointsList);
                //        LineGraphRealTime.Data = ds.Data;
                //        LineGraphRealTime.RaiseDataChanged();
                //    }
                //}
                OnPropertyChanged("ShowRealO2");
            }
        }

        private bool _showTemplateO2;
        public bool ShowTemplateO2
        {
            get { return _showTemplateO2; }
            set
            {
                _showTemplateO2 = value;
                //if (value)
                //{
                //    if (LineGraph != null)
                //    {
                //        LineGraph.Data = null;
                //        LineGraph.RaiseDataChanged();
                //    }

                //}
                //else
                //{
                //    if (RTPointsList != null)
                //    {
                //        var ds = new RawDataSource(RTPointsList);
                //        LineGraph.Data = ds.Data;
                //        LineGraph.RaiseDataChanged();
                //    }
                //}
                OnPropertyChanged("ShowTemplateO2");
            }
        }

        private bool _showC;
        public bool ShowC
        {
            get { return _showC; }
            set
            {
                _showC = value;
                if (value && СPointsList != null)
                {
                    CGraph = new RawDataSource(СPointsList);
                    CGraph.RaiseDataChanged();
                }
                else
                {
                    CGraph = new RawDataSource(new List<Point>());
                    CGraph.RaiseDataChanged();
                }
                OnPropertyChanged("ShowC");
            }
        }

        private bool _showSi;
        public bool ShowSi
        {
            get { return _showSi; }
            set
            {
                _showSi = value;
                if (value && SiPointsList != null)
                {
                    SiGraph = new RawDataSource(SiPointsList);
                    SiGraph.RaiseDataChanged();
                }
                else
                {
                    SiGraph = new RawDataSource(new List<Point>());
                    SiGraph.RaiseDataChanged();
                }
                OnPropertyChanged("ShowSi");
            }
        }

        private bool _showMn;
        public bool ShowMn
        {
            get { return _showMn; }
            set
            {
                _showMn = value;
                if (value && MnPointsList != null)
                {
                    MnGraph = new RawDataSource(MnPointsList);
                    MnGraph.RaiseDataChanged();
                }
                else
                {
                    MnGraph = new RawDataSource(new List<Point>());
                    MnGraph.RaiseDataChanged();
                }
                OnPropertyChanged("ShowMn");
            }
        }

        private bool _showP;
        public bool ShowP
        {
            get { return _showP; }
            set
            {
                _showP = value;
                if (value && PPointsList != null)
                {
                    PGraph = new RawDataSource(PPointsList);
                    PGraph.RaiseDataChanged();
                }
                else
                {
                    PGraph = new RawDataSource(new List<Point>());
                    PGraph.RaiseDataChanged();
                }
                OnPropertyChanged("ShowP");
            }
        }

        private bool _showFe;
        public bool ShowFe
        {
            get { return _showFe; }
            set
            {
                _showFe = value;
                if (value && FePointsList != null)
                {
                    FeGraph = new RawDataSource(FePointsList);
                    FeGraph.RaiseDataChanged();
                }
                else
                {
                    FeGraph = new RawDataSource(new List<Point>());
                    FeGraph.RaiseDataChanged();
                }
                OnPropertyChanged("ShowFe");
            }
        }

        private bool _showFeO;
        public bool ShowFeO
        {
            get { return _showFeO; }
            set
            {
                _showFeO = value;
                if (value && FeOPointsList != null)
                {
                    FeOGraph = new RawDataSource(FeOPointsList);
                    FeOGraph.RaiseDataChanged();
                }
                else
                {
                    FeOGraph = new RawDataSource(new List<Point>());
                    FeOGraph.RaiseDataChanged();
                }
                OnPropertyChanged("ShowFeO");
            }
        }

        private bool _showCaO;
        public bool ShowCaO
        {
            get { return _showCaO; }
            set
            {
                _showCaO = value;
                if (value && CaOPointsList != null)
                {
                    CaOGraph = new RawDataSource(CaOPointsList);
                    CaOGraph.RaiseDataChanged();
                }
                else
                {
                    CaOGraph = new RawDataSource(new List<Point>());
                    CaOGraph.RaiseDataChanged();
                }
                OnPropertyChanged("ShowCaO");
            }
        }

        private bool _showSiO2;
        public bool ShowSiO2
        {
            get { return _showSiO2; }
            set
            {
                _showSiO2 = value;
                if (value && SiO2PointsList != null)
                {
                    SiO2Graph = new RawDataSource(SiO2PointsList);
                    SiO2Graph.RaiseDataChanged();
                }
                else
                {
                    SiO2Graph = new RawDataSource(new List<Point>());
                    SiO2Graph.RaiseDataChanged();
                }
                OnPropertyChanged("ShowSiO2");
            }
        }

        private bool _showMnO;
        public bool ShowMnO
        {
            get { return _showMnO; }
            set
            {
                _showMnO = value;
                if (value && MnOPointsList != null)
                {
                    MnOGraph = new RawDataSource(MnOPointsList);
                    MnOGraph.RaiseDataChanged();
                }
                else
                {
                    MnOGraph = new RawDataSource(new List<Point>());
                    MnOGraph.RaiseDataChanged();
                }
                OnPropertyChanged("ShowMnO");
            }
        }

        private bool _showMgO;
        public bool ShowMgO
        {
            get { return _showMgO; }
            set
            {
                _showMgO = value;
                if (value && MgOPointsList != null)
                {
                    MgOGraph = new RawDataSource(MgOPointsList);
                    MgOGraph.RaiseDataChanged();
                }
                else
                {
                    MgOGraph = new RawDataSource(new List<Point>());
                    MgOGraph.RaiseDataChanged();
                }
                OnPropertyChanged("ShowMgO");
            }
        }

        #endregion

        #region Технические данные
        // если значение суммарного кислорода пустое - читаем последнее действительное значение, которое хранится в этой переменной
        private int _previousO2Sum = -1;
        public bool HeatInProgress;

        private ObservableCollection<int> _initialMatherialsSum;
        private ObservableCollection<int> _currentMatherialsSum;
        private ObservableCollection<int> _previousMatherialsSum;

        public ObservableCollection<int> InitialMatherialsSum
        {
            get { return _initialMatherialsSum; }

            set
            {
                _initialMatherialsSum = value;
                OnPropertyChanged("InitialMatherialsSum");
            }
        }

        public ObservableCollection<int> CurrentMatherialsSum
        {
            get { return _currentMatherialsSum; }

            set
            {
                _currentMatherialsSum = value;
                OnPropertyChanged("CurrentMatherialsSum");
            }
        }

        public ObservableCollection<int> PreviousMatherialsSum
        {
            get { return _previousMatherialsSum; }

            set
            {
                _previousMatherialsSum = value;
                OnPropertyChanged("PreviousMatherialsSum");
            }
        }

        private string _sumO2Value;
        public string SumO2Value
        {
            get { return _sumO2Value; }
            set { _sumO2Value = value; OnPropertyChanged("SumO2Value"); }
        }

        private int _currentStep;
        public int CurrentStep
        {
            get { return _currentStep; }
            set { _currentStep = value; OnPropertyChanged("CurrentStep"); }
        }

        private BoundNameMaterialsEvent _names;
        public BoundNameMaterialsEvent Names
        {
            get { return _names; }
            set { _names = value; OnPropertyChanged("Names"); }
        }

        #region Целевые показатели
        private string _twC;
        public string TwC
        {
            get { return _twC; }
            set { _twC = value; OnPropertyChanged("TwC"); }
        }

        private string _twP;
        public string TwP
        {
            get { return _twP; }
            set { _twP = value; OnPropertyChanged("TwP"); }
        }

        private string _twS;
        public string TwS
        {
            get { return _twS; }
            set { _twS = value; OnPropertyChanged("TwS"); }
        }

        private string _twCr;
        public string TwCr
        {
            get { return _twCr; }
            set { _twCr = value; OnPropertyChanged("TwCr"); }
        }

        private string _twNi;
        public string TwNi
        {
            get { return _twNi; }
            set { _twNi = value; OnPropertyChanged("TwNi"); }
        }

        private string _twCu;
        public string TwCu
        {
            get { return _twCu; }
            set { _twCu = value; OnPropertyChanged("TwCu"); }
        }

        private string _twMn;
        public string TwMn
        {
            get { return _twMn; }
            set { _twMn = value; OnPropertyChanged("TwMn"); }
        }

        private string _twMgO;
        public string TwMgO
        {
            get { return _twMgO; }
            set { _twMgO = value; OnPropertyChanged("TwMgO"); }
        }

        private string _twFeO;
        public string TwFeO
        {
            get { return _twFeO; }
            set { _twFeO = value; OnPropertyChanged("TwFeO"); }
        }

        private string _twCaO;
        public string TwCaO
        {
            get { return _twCaO; }
            set { _twCaO = value; OnPropertyChanged("TwCaO"); }
        }

        private string _twT;
        public string TwT
        {
            get { return _twT; }
            set { _twT = value; OnPropertyChanged("TwT"); }
        }

        private string _twCaOSio2;
        public string TwCaOSio2
        {
            get { return _twCaOSio2; }
            set { _twCaOSio2 = value; OnPropertyChanged("TwCaOSio2"); }
        }
        #endregion
        #region Углерод
        private double _carboneMass;
        public double CarboneMass
        {
            get { return _carboneMass; }
            set { _carboneMass = value; OnPropertyChanged("CarboneMass"); }
        }

        private double _carbonePercent;
        public double CarbonePercent
        {
            get { return _carbonePercent; }
            set { _carbonePercent = value; OnPropertyChanged("CarbonePercent"); }
        }

        private long _heatNo;
        public Int64 HeatNo
        {
            get { return _heatNo; }
            set { _heatNo = value; OnPropertyChanged("HeatNo"); }
        }

        public bool ShowCarbone;
        #endregion
        #region Данные модели
        private string _mC;
        public string MC
        {
            get { return _mC; }
            set { _mC = value; OnPropertyChanged("MC"); }
        }
        private string _mT;
        public string MT
        {
            get { return _mT; }
            set { _mT = value; OnPropertyChanged("MT"); }
        }
        private string _mSi;
        public string MSi
        {
            get { return _mSi; }
            set { _mSi = value; OnPropertyChanged("MSi"); }
        }
        private string _mMn;
        public string MMn
        {
            get { return _mMn; }
            set { _mMn = value; OnPropertyChanged("MMn"); }
        }
        private string _mP;
        public string MP
        {
            get { return _mP; }
            set { _mP = value; OnPropertyChanged("MP"); }
        }
        private string _mFe;
        public string MFe
        {
            get { return _mFe; }
            set { _mFe = value; OnPropertyChanged("MFe"); }
        }
        private string _mFeO;
        public string MFeO
        {
            get { return _mFeO; }
            set { _mFeO = value; OnPropertyChanged("MFeO"); }
        }
        private string _mCaO;
        public string MCaO
        {
            get { return _mCaO; }
            set { _mCaO = value; OnPropertyChanged("MCaO"); }
        }
        private string _mSiO2;
        public string MSiO2
        {
            get { return _mSiO2; }
            set { _mSiO2 = value; OnPropertyChanged("MSiO2"); }
        }
        private string _mMnO;
        public string MMnO
        {
            get { return _mMnO; }
            set { _mMnO = value; OnPropertyChanged("MMnO"); }
        }
        private string _mMgO;
        public string MMgO
        {
            get { return _mMgO; }
            set { _mMgO = value; OnPropertyChanged("MMgO"); }
        }
        #endregion

        private double _chugunCorrection;
        public double ChugunCorrection
        {
            get { return _chugunCorrection; }
            set { _chugunCorrection = value; OnPropertyChanged("ChugunCorrection");
            var fex = new ConnectionProvider.FlexHelper("CastIronCorrection");
                fex.AddArg("Correction", value);
                fex.Fire(_mainGate);
            }
        }

        #endregion

        private IPointDataSource _templateMatherialsReleaseGraph;
    }
}
