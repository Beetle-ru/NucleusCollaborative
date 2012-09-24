using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using CommonTypes;
using ConnectionProvider;
using Converter;
using ConverterUI.Models;
using ConverterUI.Util;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Heat = ConverterUI.Models.Heat;
using Lance = Converter.SteelMakingClasses.Lance;
using Implements;
using System.Configuration;

namespace ConverterUI.ViewModels.MainPage
{
    public sealed partial class MainPageViewModel : ViewModelBase, IDisposable
    {
		public delegate void LoadedEventHandler( object sender);

		public event LoadedEventHandler Loaded;


        public MainPageViewModel()
        {
            Debug.WriteLine("Starting Constructor of model");
	        _canRefreshRegimes = true;
	        LastTotalO2 = int.MinValue;
	        LastLancePosition = 0;
            HeatInProgress = false;
            _canReleaseAlConc = false;
            _canReleaseCox = false;
            _canReleaseDoloms = false;
            _canReleaseIzvest = false;
            CommandManager.InvalidateRequerySuggested();
            ShowRealO2 = Convert.ToBoolean(ConfigurationManager.AppSettings["ShowRealO2"]);
            ShowTemplateO2 = Convert.ToBoolean(ConfigurationManager.AppSettings["ShowTemplateO2"]);
            ShowC = Convert.ToBoolean(ConfigurationManager.AppSettings["ShowC"]);
            ShowSi = Convert.ToBoolean(ConfigurationManager.AppSettings["ShowSi"]);
            ShowMn = Convert.ToBoolean(ConfigurationManager.AppSettings["ShowMn"]);
            ShowP = Convert.ToBoolean(ConfigurationManager.AppSettings["ShowP"]);
            ShowFe = Convert.ToBoolean(ConfigurationManager.AppSettings["ShowFe"]);
            ShowFeO = Convert.ToBoolean(ConfigurationManager.AppSettings["ShowFeO"]);
            ShowCaO = Convert.ToBoolean(ConfigurationManager.AppSettings["ShowCaO"]);
            ShowSiO2 = Convert.ToBoolean(ConfigurationManager.AppSettings["ShowSiO2"]);
            ShowMnO = Convert.ToBoolean(ConfigurationManager.AppSettings["ShowMnO"]);
            ShowMgO = Convert.ToBoolean(ConfigurationManager.AppSettings["ShowMgO"]);
            ChugunCorrection = 0;
        }

		public void Load()
		{
			Debug.WriteLine( "Starting Load of model" );
			TractRegimeManualVisibility = Visibility.Hidden;
		    ShowCarbone = true;            
			TractRegimeAutoVisibility = Visibility.Hidden;
			LanceRegimeManualVisibility = Visibility.Hidden;
			LanceRegimeAutoVisibility = Visibility.Hidden;
			RegClapanManualVisibility = Visibility.Hidden;
			RegClapanAutoVisibility = Visibility.Hidden;
			_canColorSteps = false;
			GroupSelectingEnabled = true;

			InitialMatherialsSum = new ObservableCollection<int>();
			CurrentMatherialsSum = new ObservableCollection<int>();
			PreviousMatherialsSum = new ObservableCollection<int>();

			/************************************
			* Вычитываем список шаблонов
			* для заполнения выпадающих списков
			* выбора группы и шаблона
			***********************************/
			Groups = new ObservableCollection<HeatGroupModel>();
			for ( int i = 1; i < 6; i++ )
			{
				var group = new HeatGroupModel { Id = i, Name = "Группа " + i };
				Groups.Add( group );
			}
			try
			{
				Debug.WriteLine( DateTime.Now.TimeOfDay + " Starting database connection" );
				_ctx = new HeatModelContainer();

				IQueryable<Heat> heats = from c in _ctx.HeatSet select c;

				foreach ( Heat heat in heats )
				{
					foreach ( HeatGroupModel group in Groups )
					{
						if ( group.Id == heat.Group )
							group.Templates.Add( heat.Name );
					}
				}

				if ( _mainGate == null )
				{
					Debug.WriteLine( DateTime.Now.TimeOfDay + " Starting Gate" );
					_mainGate = new Client();
					_mainGate.Subscribe();
				}
				var T = new Thread(() =>
					                   {
						                   Debug.WriteLine(DateTime.Now.TimeOfDay + " Starting Listener");
						                   var eListener = new Listener();
						                   eListener.OnEventFired += _eListener_OnEventFired;
						                   var lGate = new Client(eListener);
						                   lGate.Subscribe();
					                   }) {IsBackground = true};
				T.Start();
				
				Debug.WriteLine( DateTime.Now.TimeOfDay + " Starting 1 second sleep" );
				Thread.Sleep( 1000 );
				var command = new OPCDirectReadEvent { EventName = typeof( ModeVerticalPathEvent ).Name };
				_mainGate.PushEvent( command );
				var command1 = new OPCDirectReadEvent { EventName = typeof( ModeLanceEvent ).Name };
				_mainGate.PushEvent( command1 );
				Debug.WriteLine( DateTime.Now.TimeOfDay + " Pushed OPCDirectReadEvent ModeVerticalPathEvent" );
				SelectedGroup = Groups[ 0 ];
				Loaded( this );
			}
			catch ( Exception e )
			{
				MessageBox.Show( e.Message + "; " + e.InnerException + e.Source );
			}
		}

		void _eListener_OnEventFired( object sender, object data )
		{
            EventsStackReact(sender, data);
		}

	    /// <summary>
        /// Загрузка данных шаблона.
        /// Вызывается при выборе шаблона.
        /// </summary>
        private void LoadTemplate(object state)
        {
			
			HGModel = new HeatGridModel { TableRows = new ObservableCollection<TableRowModel>() };
            if (RTPointsList != null)
            {
                RTPointsList.Clear();
            }

			var command = new OPCDirectReadEvent { EventName = typeof( BoundNameMaterialsEvent ).Name };
			_mainGate.PushEvent( command );
			command = new OPCDirectReadEvent { EventName = typeof( ModeVerticalPathEvent ).Name };
			_mainGate.PushEvent( command );
			command = new OPCDirectReadEvent { EventName = typeof( ModeLanceEvent ).Name };
			_mainGate.PushEvent( command );
			Debug.WriteLine( DateTime.Now.TimeOfDay + " Pushed OPCDirectReadEvent BoundNameMaterialsEvent" );
        }

        void EventsStackReact(object sender, object data)
        {
            var e = data as BaseEvent;
            if (e != null)
            {
                var eventType = e.GetType();
				#region HeatSchemaStepEvent
				if ( eventType == typeof( HeatSchemaStepEvent ) && _canColorSteps )
				{
					var T = new Thread( () =>
					                        {
					                            _canColorSteps = false;
					                            var heatSchemaStepEvent = e as HeatSchemaStepEvent;
					                            if ( heatSchemaStepEvent != null )
					                            {
					                                if ( heatSchemaStepEvent.Step >= 0 )
					                                {
					                                    CurrentStep = heatSchemaStepEvent.Step;
                                                        InstantLogger.log(DateTime.Now.ToString() + " Новый шаг, HeatSchemaStepEvent получен \r\n");
					                                    if ( _steps[ heatSchemaStepEvent.Step ].O2Volume >= 3000 )
					                                    {
					                                        _canReleaseCox = false;
					                                    }
					                                    else
					                                    {
					                                        _canReleaseCox = true;
					                                    }
                                                        if (_steps[heatSchemaStepEvent.Step].O2Volume >= 15880)
                                                        {
                                                            _canReleaseDoloms = true;
                                                        }
                                                        else
                                                        {
                                                            _canReleaseDoloms = false;
                                                        }
                                                        if (_steps[heatSchemaStepEvent.Step].O2Volume >= 19880)
                                                        {
                                                            _canReleaseAlConc = false;
                                                        }
                                                        else
                                                        {
                                                            _canReleaseAlConc = true;
                                                        }
                                                        if (_steps[heatSchemaStepEvent.Step].O2Volume >= 12000)
                                                        {
                                                            _canSetAdditions = false;
                                                        }
					                                    _canReleaseIzvest = true;

					                                    Del d = delegate
					                                                {					                                                    
					                                                    if ( CurrentStep > 0 )
                                                                            for (int i = 0; i < _steps.Count; i++)
                                                                            {
                                                                                HGModel.HeaderSteps[i].Color =
                                                                                new SolidColorBrush(Colors.White);
                                                                            }					                                                        
                                                                        HGModel.HeaderSteps[CurrentStep].Color =
                                                                            new SolidColorBrush(Colors.Aqua);
                                                                        CommandManager.InvalidateRequerySuggested();
					                                                };
					                                    Application.Current.Dispatcher.BeginInvoke( d, DispatcherPriority.Render );
					                                }
					                            }

					                            _canColorSteps = true;
					                            if ( heatSchemaStepEvent != null && heatSchemaStepEvent.Step == -1 )
					                            {
					                                Del d = delegate
					                                            {
					                                                LastLancePosition = 0;
					                                                LastTotalO2 = 0;
					                                                HGModel.HeaderSteps[ CurrentStep ].Color = new SolidColorBrush( Colors.White );
					                                                _canColorSteps = false;
					                                                _canStartHeat = true;
                                                                    HeatInProgress = false;
					                                                _canSetAdditions = true;
					                                                _canFurmaUp = true;
					                                                _canFurmaDown = true;
						                                            _canO2IntenceUp = true;
						                                            _canO2IntenceDown = true;
					                                                _canO2Plus = true;
					                                                _canO2Minus = true;
                                                                    _canReleaseAlConc = false;
                                                                    _canReleaseCox = false;
                                                                    _canReleaseDoloms = false;
                                                                    _canReleaseIzvest = false;
					                                                GroupSelectingEnabled = true;
																	if ( ReloadTemplateAfterHeat )
																	{
																		var tr = new Thread( LoadTemplate ) { IsBackground = true };
																		tr.Start();
																	}
					                                                CommandManager.InvalidateRequerySuggested();
					                                            };
					                                Application.Current.Dispatcher.BeginInvoke( d, DispatcherPriority.Render );
					                            }
					                        } ) { IsBackground = true };
					T.Start();
				} 
				#endregion
				#region BoundNameMaterialsEvent
				if ( eventType == typeof( BoundNameMaterialsEvent ) )
				{
				    ShowCarbone = true;
					Debug.WriteLine( DateTime.Now.TimeOfDay + " Incoming BoundNameMaterialsEvent" );
					var T = new Thread( () =>
					                        {
					                            Del d = delegate
					                                        {
					                                            InitialMatherialsSum = new ObservableCollection<int>();
					                                            CurrentMatherialsSum = new ObservableCollection<int>();
					                                            PreviousMatherialsSum = new ObservableCollection<int>();
					                                            var boundNameMaterialsEvent = e as BoundNameMaterialsEvent;
					                                            if ( boundNameMaterialsEvent != null )
					                                                Names = boundNameMaterialsEvent;
					                                            _ctx = new HeatModelContainer();
					                                            _ctx.StepSet.Include( "VerticalTracts" );
					                                            HGModel.AdditionsList = GetAdditionsList();
					                                            _canSetAdditions = true;
					                                            HGModel.ItemsSource = null;
					                                            LoadSteps();
					                                            if ( HGModel.ColumnsList != null )
					                                                HGModel.ColumnsList.Clear();
					                                            HGModel.ColumnsList = SetDataGridColumns();
					                                            HGModel.TableRows = FillTable();
					                                            HGModel.ItemsSource = HGModel.TableRows;
					                                            FillStages();
					                                            _canStartHeat = true;
					                                            HeatInProgress = false;
					                                            _canO2Plus = true;
					                                            _canO2Minus = true;
					                                            _canSaveTV = true;
					                                            GroupSelectingEnabled = true;
					                                        };
					                            Application.Current.Dispatcher.BeginInvoke( d, DispatcherPriority.Render );
					                            CommandManager.InvalidateRequerySuggested();
					                        } ) { IsBackground = true };
					T.Start();
				} 
				#endregion
                #region BlowingEvent
                if (eventType == typeof(BlowingEvent))
                {
                    Debug.WriteLine(DateTime.Now.TimeOfDay + " Incoming BlowingEvent");
                    var T = new Thread(() =>
                                            {
                                                Del d = delegate
                                                            {
                                                                var visBlowingEvent = e as BlowingEvent;
                                                                if (visBlowingEvent == null) return;
                                                                SumO2Value = visBlowingEvent.O2TotalVol.ToString(CultureInfo.InvariantCulture);
                                                                if (!HeatInProgress)
                                                                {
                                                                    if (visBlowingEvent.O2TotalVol > 0)
                                                                    {
                                                                        _canStartHeat = false;
                                                                        CantStartHeatMessageText =
                                                                            "Суммарный О2 больше 0";
                                                                    }
                                                                    else
                                                                    {
                                                                        CantStartHeatMessageText =
                                                                            "";
                                                                        _canStartHeat = true;
                                                                    }
                                                                    CommandManager.InvalidateRequerySuggested();
                                                                    return;
                                                                }
                                                                if (RTPointsList == null)
                                                                {
                                                                    RTPointsList = new List<Point>();
                                                                }
                                                                LastTotalO2 = visBlowingEvent.O2TotalVol;
                                                                if (LastLancePosition <= 0) return;
                                                                RTPointsList.Add(new Point(visBlowingEvent.O2TotalVol, LastLancePosition));
                                                                var ds = new RawDataSource(RTPointsList);
                                                                LineGraphRealTime.Data = ds.Data;
                                                                LineGraphRealTime.RaiseDataChanged();
                                                            };
                                                Application.Current.Dispatcher.BeginInvoke(d, DispatcherPriority.Render);
                                            }) { IsBackground = true };
                    T.Start();
                } 
                #endregion
                #region LanceEvent
                if (eventType == typeof(LanceEvent))
                {
                    Debug.WriteLine(DateTime.Now.TimeOfDay + " Incoming LanceEvent");
                    var T = new Thread(() =>
                                            {
                                                Del d = delegate
                                                            {
                                                                if (!HeatInProgress)
                                                                {
                                                                    return;
                                                                }
                                                                if (RTPointsList == null)
                                                                {
                                                                    RTPointsList = new List<Point>();
                                                                }
                                                                var lanceEvent = e as LanceEvent;
                                                                if (lanceEvent != null)
                                                                {
                                                                    LastLancePosition = lanceEvent.LanceHeight;
                                                                    if (lanceEvent.LanceHeight > 800 || LastTotalO2 < 0 || lanceEvent.LanceHeight <= 0) return;
                                                                    RTPointsList.Add(new Point(LastTotalO2, lanceEvent.LanceHeight));
                                                                }
                                                                var ds = new RawDataSource(RTPointsList);
                                                                LineGraphRealTime.Data = ds.Data;
                                                                LineGraphRealTime.RaiseDataChanged();
                                                            };
                                                Application.Current.Dispatcher.BeginInvoke(d, DispatcherPriority.Render);
                                            }) { IsBackground = true };
                    T.Start();
                } 
                #endregion
				#region ModeVerticalPathEvent
				if ( eventType == typeof( ModeVerticalPathEvent ) )
				{
					Debug.WriteLine( DateTime.Now.TimeOfDay + " Incoming ModeVerticalPathEvent" );
					var T = new Thread( () =>
					                        {
					                            var modeVerticalPathEvent = e as ModeVerticalPathEvent;
					                            if ( modeVerticalPathEvent == null )
					                            {
					                                return;
					                            }
					                            if ( modeVerticalPathEvent.VerticalPathMode == 3 )
					                            {
					                                TractRegimeAutoVisibility = Visibility.Visible;
					                                TractRegimeManualVisibility = Visibility.Hidden;
					                            }
					                            else
					                            {
					                                TractRegimeAutoVisibility = Visibility.Hidden;
					                                TractRegimeManualVisibility = Visibility.Visible;
					                            }
					                        } ) { IsBackground = true };
					T.Start();
				} 
				#endregion
				#region ModeLanceEvent
				if ( eventType == typeof( ModeLanceEvent ) )
				{
					Debug.WriteLine( DateTime.Now.TimeOfDay + " Incoming ModeLanceEvent" );
					var T = new Thread( () =>
					                        {
					                            var modeLanceEvent = e as ModeLanceEvent;
					                            if ( modeLanceEvent == null )
					                            {
					                                return;
					                            }
					                            if ( modeLanceEvent.LanceMode == 3 )
					                            {
					                                LanceRegimeAutoVisibility = Visibility.Visible;
					                                LanceRegimeManualVisibility = Visibility.Hidden;
					                            }
					                            else
					                            {
					                                LanceRegimeAutoVisibility = Visibility.Hidden;
					                                LanceRegimeManualVisibility = Visibility.Visible;
					                            }
					                            if ( modeLanceEvent.O2FlowMode == 3 )
					                            {
					                                RegClapanAutoVisibility = Visibility.Visible;
					                                RegClapanManualVisibility = Visibility.Hidden;
					                            }
					                            else
					                            {
					                                RegClapanAutoVisibility = Visibility.Hidden;
					                                RegClapanManualVisibility = Visibility.Visible;
					                            }
					                        } ) { IsBackground = true };
					T.Start();
				} 
				#endregion
                #region CalculatedCarboneEvent
                if (eventType == typeof(CalculatedCarboneEvent))
                {
                    var T = new Thread(() =>
                    {
                        var сarboneEvent = e as CalculatedCarboneEvent;
                        if (сarboneEvent == null || !ShowCarbone)
                        {
                            return;
                        }
                        CarboneMass = сarboneEvent.CarboneMass;
                        CarbonePercent = сarboneEvent.CarbonePercent;
                    }) { IsBackground = true };
                    T.Start();
                }
                #endregion
                #region FixDataMfactorModelEvent
                if (eventType == typeof(FixDataMfactorModelEvent))
                {
                    ShowCarbone = false;
                } 
                #endregion
                #region HeatChangeEvent
                if (eventType == typeof(HeatChangeEvent))
                {
                    var T = new Thread(() =>
                    {
                        var heatChangeEvent = e as HeatChangeEvent;
                        if (heatChangeEvent == null)
                        {
                            return;
                        }
                        ShowCarbone = true;
                        HeatNo = heatChangeEvent.HeatNumber;
                        ChugunCorrection = 0;
                    }) { IsBackground = true };
                    T.Start();
                } 
                #endregion
                #region FlexEvent
                if (eventType == typeof(FlexEvent))
                {
                    var T = new Thread(() =>
                    {
                        Del d = delegate
                                                            {
                        var flexEvent = e as FlexEvent;
                        if (flexEvent == null)
                        {
                            return;
                        }
                        if (flexEvent.Operation.StartsWith("Model.Dynamic.Output.PerSecond"))
                        {
                            
                            foreach (var arg in flexEvent.Arguments)
                            {
                                switch (arg.Key)
                                {
                                    case "С":
                                            {
                                                CModelEventCounter++;
                                                MC = arg.Value.ToString();
                                                if (СPointsList == null)
                                                {
                                                    СPointsList = new List<Point>();
                                                }
                                                var newList = new List<Point>();
                                                if (СPointsList.Count() > 15)
                                                {
                                                    СPointsList.RemoveAt(0);
                                                    foreach (var point in СPointsList)
                                                    {
                                                        newList.Add(new Point(point.X - 1, point.Y));
                                                    }
                                                    CModelEventCounter--;
                                                    newList.Add(new Point(CModelEventCounter, (double)arg.Value));
                                                    СPointsList = newList;
                                                    if (ShowC)
                                                    {
                                                        CGraph = new RawDataSource(СPointsList);
                                                        CGraph.RaiseDataChanged();
                                                    }
                                                    else
                                                    {
                                                        CGraph = new RawDataSource(new List<Point>());
                                                        CGraph.RaiseDataChanged();
                                                    }
                                                    
                                                }
                                                else
                                                {
                                                    СPointsList.Add(new Point(CModelEventCounter, (double)arg.Value));
                                                    if (ShowC)
                                                    {
                                                        CGraph = new RawDataSource(СPointsList);
                                                        CGraph.RaiseDataChanged();
                                                    }
                                                    else
                                                    {
                                                        CGraph = new RawDataSource(new List<Point>());
                                                        CGraph.RaiseDataChanged();
                                                    }
                                                }
                                                
                                                break;
                                            }
                                    case "Т":
                                            {
                                                MT = arg.Value.ToString();
                                                break;
                                            }
                                    case "Si":
                                            {
                                                SiModelEventCounter++;
                                                MSi = arg.Value.ToString();
                                                if (SiPointsList == null)
                                                {
                                                    SiPointsList = new List<Point>();
                                                }
                                                var newList = new List<Point>();
                                                if (SiPointsList.Count() > 15)
                                                {
                                                    SiPointsList.RemoveAt(0);
                                                    foreach (var point in SiPointsList)
                                                    {
                                                        newList.Add(new Point(point.X - 1, point.Y));
                                                    }
                                                    SiModelEventCounter--;
                                                    newList.Add(new Point(SiModelEventCounter, (double)arg.Value));
                                                    SiPointsList = newList;
                                                    if (ShowSi)
                                                    {
                                                        SiGraph = new RawDataSource(SiPointsList);
                                                        SiGraph.RaiseDataChanged();
                                                    }
                                                    else
                                                    {
                                                        SiGraph = new RawDataSource(new List<Point>());
                                                        SiGraph.RaiseDataChanged();
                                                    }
                                                }
                                                else
                                                {
                                                    SiPointsList.Add(new Point(SiModelEventCounter, (double)arg.Value));
                                                    if (ShowSi)
                                                    {
                                                        SiGraph = new RawDataSource(SiPointsList);
                                                        SiGraph.RaiseDataChanged();
                                                    }
                                                    else
                                                    {
                                                        SiGraph = new RawDataSource(new List<Point>());
                                                        SiGraph.RaiseDataChanged();
                                                    }
                                                }
                                                break;
                                            }
                                    case "Mn":
                                            {
                                                MnModelEventCounter++;
                                                MMn = arg.Value.ToString();
                                                if (MnPointsList == null)
                                                {
                                                    MnPointsList = new List<Point>();
                                                }
                                                var newList = new List<Point>();
                                                if (MnPointsList.Count() > 15)
                                                {
                                                    MnPointsList.RemoveAt(0);
                                                    foreach (var point in MnPointsList)
                                                    {
                                                        newList.Add(new Point(point.X - 1, point.Y));
                                                    }
                                                    MnModelEventCounter--;
                                                    newList.Add(new Point(MnModelEventCounter, (double)arg.Value));
                                                    MnPointsList = newList;
                                                    if (ShowMn)
                                                    {
                                                        MnGraph = new RawDataSource(MnPointsList);
                                                        MnGraph.RaiseDataChanged();
                                                    }
                                                    else
                                                    {
                                                        MnGraph = new RawDataSource(new List<Point>());
                                                        MnGraph.RaiseDataChanged();
                                                    }
                                                }
                                                else
                                                {
                                                    MnPointsList.Add(new Point(MnModelEventCounter, (double)arg.Value));
                                                    if (ShowMn)
                                                    {
                                                        MnGraph = new RawDataSource(MnPointsList);
                                                        MnGraph.RaiseDataChanged();
                                                    }
                                                    else
                                                    {
                                                        MnGraph = new RawDataSource(new List<Point>());
                                                        MnGraph.RaiseDataChanged();
                                                    }
                                                }
                                                break;
                                            }
                                    case "P":
                                            {
                                                PModelEventCounter++;
                                                MP = arg.Value.ToString();
                                                if (PPointsList == null)
                                                {
                                                    PPointsList = new List<Point>();
                                                }
                                                var newList = new List<Point>();
                                                if (PPointsList.Count() > 15)
                                                {
                                                    PPointsList.RemoveAt(0);
                                                    foreach (var point in PPointsList)
                                                    {
                                                        newList.Add(new Point(point.X - 1, point.Y));
                                                    }
                                                    PModelEventCounter--;
                                                    newList.Add(new Point(PModelEventCounter, (double)arg.Value));
                                                    PPointsList = newList;
                                                    if (ShowP)
                                                    {
                                                        PGraph = new RawDataSource(PPointsList);
                                                        PGraph.RaiseDataChanged();
                                                    }
                                                    else
                                                    {
                                                        PGraph = new RawDataSource(new List<Point>());
                                                        PGraph.RaiseDataChanged();
                                                    }
                                                }
                                                else
                                                {
                                                    PPointsList.Add(new Point(PModelEventCounter, (double)arg.Value));
                                                    if (ShowP)
                                                    {
                                                        PGraph = new RawDataSource(PPointsList);
                                                        PGraph.RaiseDataChanged();
                                                    }
                                                    else
                                                    {
                                                        PGraph = new RawDataSource(new List<Point>());
                                                        PGraph.RaiseDataChanged();
                                                    }
                                                }
                                                break;
                                            }
                                    case "Fe":
                                            {
                                                FeModelEventCounter++;
                                                MFe = arg.Value.ToString();
                                                if (FePointsList == null)
                                                {
                                                    FePointsList = new List<Point>();
                                                }
                                                var newList = new List<Point>();
                                                if (FePointsList.Count() > 15)
                                                {
                                                    FePointsList.RemoveAt(0);
                                                    foreach (var point in FePointsList)
                                                    {
                                                        newList.Add(new Point(point.X - 1, point.Y));
                                                    }
                                                    FeModelEventCounter--;
                                                    newList.Add(new Point(FeModelEventCounter, (double)arg.Value));
                                                    FePointsList = newList;
                                                    if (ShowFe)
                                                    {
                                                        FeGraph = new RawDataSource(FePointsList);
                                                        FeGraph.RaiseDataChanged();
                                                    }
                                                    else
                                                    {
                                                        FeGraph = new RawDataSource(new List<Point>());
                                                        FeGraph.RaiseDataChanged();
                                                    }
                                                }
                                                else
                                                {
                                                    FePointsList.Add(new Point(FeModelEventCounter, (double)arg.Value));
                                                    if (ShowFe)
                                                    {
                                                        FeGraph = new RawDataSource(FePointsList);
                                                        FeGraph.RaiseDataChanged();
                                                    }
                                                    else
                                                    {
                                                        FeGraph = new RawDataSource(new List<Point>());
                                                        FeGraph.RaiseDataChanged();
                                                    }
                                                }
                                                break;
                                            }
                                    case "FeO":
                                            {
                                                FeOModelEventCounter++;
                                                MFeO = arg.Value.ToString();
                                                if (FeOPointsList == null)
                                                {
                                                    FeOPointsList = new List<Point>();
                                                }
                                                var newList = new List<Point>();
                                                if (FeOPointsList.Count() > 15)
                                                {
                                                    FeOPointsList.RemoveAt(0);
                                                    foreach (var point in FeOPointsList)
                                                    {
                                                        newList.Add(new Point(point.X - 1, point.Y));
                                                    }
                                                    FeOModelEventCounter--;
                                                    newList.Add(new Point(FeOModelEventCounter, (double)arg.Value));
                                                    FeOPointsList = newList;
                                                    if (ShowFeO)
                                                    {
                                                        FeOGraph = new RawDataSource(FeOPointsList);
                                                        FeOGraph.RaiseDataChanged();
                                                    }
                                                    else
                                                    {
                                                        FeOGraph = new RawDataSource(new List<Point>());
                                                        FeOGraph.RaiseDataChanged();
                                                    }
                                                }
                                                else
                                                {
                                                    FeOPointsList.Add(new Point(FeOModelEventCounter, (double)arg.Value));
                                                    if (ShowFeO)
                                                    {
                                                        FeOGraph = new RawDataSource(FeOPointsList);
                                                        FeOGraph.RaiseDataChanged();
                                                    }
                                                    else
                                                    {
                                                        FeOGraph = new RawDataSource(new List<Point>());
                                                        FeOGraph.RaiseDataChanged();
                                                    }
                                                }
                                                break;
                                            }
                                    case "CaO":
                                            {
                                                CaOModelEventCounter++;
                                                MCaO = arg.Value.ToString();
                                                if (CaOPointsList == null)
                                                {
                                                    CaOPointsList = new List<Point>();
                                                }
                                                var newList = new List<Point>();
                                                if (CaOPointsList.Count() > 15)
                                                {
                                                    CaOPointsList.RemoveAt(0);
                                                    foreach (var point in CaOPointsList)
                                                    {
                                                        newList.Add(new Point(point.X - 1, point.Y));
                                                    }
                                                    CaOModelEventCounter--;
                                                    newList.Add(new Point(CaOModelEventCounter, (double)arg.Value));
                                                    CaOPointsList = newList;
                                                    if (ShowCaO)
                                                    {
                                                        CaOGraph = new RawDataSource(CaOPointsList);
                                                        CaOGraph.RaiseDataChanged();
                                                    }
                                                    else
                                                    {
                                                        CaOGraph = new RawDataSource(new List<Point>());
                                                        CaOGraph.RaiseDataChanged();
                                                    }
                                                }
                                                else
                                                {
                                                    CaOPointsList.Add(new Point(CaOModelEventCounter, (double)arg.Value));
                                                    if (ShowCaO)
                                                    {
                                                        CaOGraph = new RawDataSource(CaOPointsList);
                                                        CaOGraph.RaiseDataChanged();
                                                    }
                                                    else
                                                    {
                                                        CaOGraph = new RawDataSource(new List<Point>());
                                                        CaOGraph.RaiseDataChanged();
                                                    }
                                                }
                                                break;
                                            }
                                    case "SiO2":
                                            {
                                                SiO2ModelEventCounter++;
                                                MSiO2 = arg.Value.ToString();
                                                if (SiO2PointsList == null)
                                                {
                                                    SiO2PointsList = new List<Point>();
                                                }
                                                var newList = new List<Point>();
                                                if (SiO2PointsList.Count() > 15)
                                                {
                                                    SiO2PointsList.RemoveAt(0);
                                                    foreach (var point in SiO2PointsList)
                                                    {
                                                        newList.Add(new Point(point.X - 1, point.Y));
                                                    }
                                                    SiO2ModelEventCounter--;
                                                    newList.Add(new Point(SiO2ModelEventCounter, (double)arg.Value));
                                                    SiO2PointsList = newList;
                                                    if (ShowSiO2)
                                                    {
                                                        SiO2Graph = new RawDataSource(SiO2PointsList);
                                                        SiO2Graph.RaiseDataChanged();
                                                    }
                                                    else
                                                    {
                                                        SiO2Graph = new RawDataSource(new List<Point>());
                                                        SiO2Graph.RaiseDataChanged();
                                                    }
                                                }
                                                else
                                                {
                                                    SiO2PointsList.Add(new Point(SiO2ModelEventCounter, (double)arg.Value));
                                                    if (ShowSiO2)
                                                    {
                                                        SiO2Graph = new RawDataSource(SiO2PointsList);
                                                        SiO2Graph.RaiseDataChanged();
                                                    }
                                                    else
                                                    {
                                                        SiO2Graph = new RawDataSource(new List<Point>());
                                                        SiO2Graph.RaiseDataChanged();
                                                    }
                                                }
                                                break;
                                            }
                                    case "MnO":
                                            {
                                                MnOModelEventCounter++;
                                                MMnO = arg.Value.ToString();
                                                if (MnOPointsList == null)
                                                {
                                                    MnOPointsList = new List<Point>();
                                                }
                                                var newList = new List<Point>();
                                                if (MnOPointsList.Count() > 15)
                                                {
                                                    MnOPointsList.RemoveAt(0);
                                                    foreach (var point in MnOPointsList)
                                                    {
                                                        newList.Add(new Point(point.X - 1, point.Y));
                                                    }
                                                    MnOModelEventCounter--;
                                                    newList.Add(new Point(MnOModelEventCounter, (double)arg.Value));
                                                    MnOPointsList = newList;
                                                    if (ShowMnO)
                                                    {
                                                        MnOGraph = new RawDataSource(MnOPointsList);
                                                        MnOGraph.RaiseDataChanged();
                                                    }
                                                    else
                                                    {
                                                        MnOGraph = new RawDataSource(new List<Point>());
                                                        MnOGraph.RaiseDataChanged();
                                                    }
                                                }
                                                else
                                                {
                                                    MnOPointsList.Add(new Point(MnOModelEventCounter, (double)arg.Value));
                                                    if (ShowMnO)
                                                    {
                                                        MnOGraph = new RawDataSource(MnOPointsList);
                                                        MnOGraph.RaiseDataChanged();
                                                    }
                                                    else
                                                    {
                                                        MnOGraph = new RawDataSource(new List<Point>());
                                                        MnOGraph.RaiseDataChanged();
                                                    }
                                                }
                                                break;
                                            }
                                    case "MgO":
                                            {
                                                MgOModelEventCounter++;
                                                MMgO = arg.Value.ToString();
                                                if (MgOPointsList == null)
                                                {
                                                    MgOPointsList = new List<Point>();
                                                }
                                                var newList = new List<Point>();
                                                if (MgOPointsList.Count() > 15)
                                                {
                                                    MgOPointsList.RemoveAt(0);
                                                    foreach (var point in MgOPointsList)
                                                    {
                                                        newList.Add(new Point(point.X - 1, point.Y));
                                                    }
                                                    MgOModelEventCounter--;
                                                    newList.Add(new Point(MgOModelEventCounter, (double)arg.Value));
                                                    MgOPointsList = newList;
                                                    if (ShowMgO)
                                                    {
                                                        MgOGraph = new RawDataSource(MgOPointsList);
                                                        MgOGraph.RaiseDataChanged();
                                                    }
                                                    else
                                                    {
                                                        MgOGraph = new RawDataSource(new List<Point>());
                                                        MgOGraph.RaiseDataChanged();
                                                    }
                                                }
                                                else
                                                {
                                                    MgOPointsList.Add(new Point(MgOModelEventCounter, (double)arg.Value));
                                                    if (ShowMgO)
                                                    {
                                                        MgOGraph = new RawDataSource(MgOPointsList);
                                                        MgOGraph.RaiseDataChanged();
                                                    }
                                                    else
                                                    {
                                                        MgOGraph = new RawDataSource(new List<Point>());
                                                        MgOGraph.RaiseDataChanged();
                                                    }
                                                }
                                                break;
                                            }
                                    default:
                                        break;
                                }
                            }
                        }
                                                            };
                        Application.Current.Dispatcher.BeginInvoke(d, DispatcherPriority.Render);
                    }) { IsBackground = true };
                    T.Start();
                }
                #endregion
				if ( eventType == typeof( OPCDirectReadEvent ) )
				{
				    var opcDirectReadEvent = e as OPCDirectReadEvent;
				    if (opcDirectReadEvent != null)
				        Debug.WriteLine( DateTime.Now.TimeOfDay + " Incoming OPCDirectReadEvent " + opcDirectReadEvent.EventName );
				}
            }
        }

        /// <summary>
        /// Получение списка шагов в шаблоне
        /// </summary>
        private void LoadSteps()
        {
            // получаем список периодов в шаблоне плавки
            IOrderedEnumerable<Period> periods = from p in _currentHeat.Periods
                                                 orderby p.Number ascending
                                                 select p;
            if (!periods.Any())
                return;
            _steps = new List<Step>();
            foreach (Period period in periods)
            {
                IOrderedEnumerable<Phase> phases = period.Phases.OrderBy(p => p.Number);
                foreach (Phase phase in phases)
                {
                    IOrderedEnumerable<Step> steps = (from s in phase.Steps
                                                      orderby s.Number
                                                      select s);
                    foreach (Step step in steps)
                        _steps.Add(step);
                }
            }
        }

        /// <summary>
        /// Заполнение списка периодов, этапов и шагов для шапки таблицы с подсчетом ширины колонок
        /// </summary>
        /// <returns></returns>
        private void FillStages()
        {
            // заполняем периоды
            HGModel.Periods = new ObservableCollection<TableHeaderItem>();
            IOrderedEnumerable<Period> periods = _currentHeat.Periods.OrderBy(p => p.Number);
            foreach (TableHeaderItem toAdd in from period in periods
                                              let stepsAmount = period.Phases.Sum(phase => phase.Steps.Count)
                                              select
                                                  new TableHeaderItem
                                                      {
                                                          Title = period.Number + " " + period.Name,
                                                          Width = stepsAmount * 35
                                                      })
            {
                TableHeaderItem add = toAdd;
                Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() => HGModel.Periods.Add(add)));
            }
            // заполняем этапы
            HGModel.Phases = new ObservableCollection<TableHeaderItem>();
            foreach (TableHeaderItem toAdd in from period in periods
                                              from phase in period.Phases
                                              select
                                                  new TableHeaderItem
                                                      {
                                                          Title = phase.Number + " " + phase.Name,
                                                          Width = phase.Steps.Count * 35
                                                      })
            {
                TableHeaderItem add = toAdd;
                Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() => HGModel.Phases.Add(add)));
            }
            // заполняем шаги
            HGModel.HeaderSteps = new ObservableCollection<TableHeaderItem>();
            foreach (TableHeaderItem toAdd in from period in periods
                                              from phase in period.Phases
                                              from step in phase.Steps
                                              select
                                                  new TableHeaderItem
                                                      {
                                                          Title = step.Number.ToString(CultureInfo.InvariantCulture),
                                                          Width = 35,
                                                          Color = new SolidColorBrush(Colors.White)
                                                      })
            {
                TableHeaderItem add = toAdd;
                Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() => HGModel.HeaderSteps.Add(add)));
            }
        }

        /// <summary>
        /// Получаем список материалов с весами и бункерами
        /// </summary>
        private ObservableCollection<AdditionModel> GetAdditionsList()
        {
            var result = new ObservableCollection<AdditionModel>();
            BoundNameMaterialsEvent ev = Names;
            _currentHeat = (from h in _ctx.HeatSet
                            where h.Name == SelectedTemplate
                            select h).FirstOrDefault();
            if (_currentHeat == null)
                return result;
            List<Material> rawList = (from a in _currentHeat.Materials
                                      orderby a.Number ascending
                                      select a).ToList();


            for (int i = 0; i < rawList.Count(); i++)
            {
                var toAdd = new AdditionModel
                                {
                                    Bunker = rawList[i].BunkerName,
                                    WeighterLine = rawList[i].WeighterLineName,
                                    Correction = 0,
                                    Added = false
                                };

                switch (rawList[i].BunkerName)
                {
                    case "РБ5":
                        {
                            toAdd.Name = Encoder(ev.Bunker5MaterialName);
                            break;
                        }
                    case "РБ6":
                        {
                            toAdd.Name = Encoder(ev.Bunker6MaterialName);
                            break;
                        }
                    case "РБ7":
                        {
                            toAdd.Name = Encoder(ev.Bunker7MaterialName);
                            break;
                        }
                    case "РБ8":
                        {
                            toAdd.Name = Encoder(ev.Bunker8MaterialName);
                            break;
                        }
                    case "РБ9":
                        {
                            toAdd.Name = Encoder(ev.Bunker9MaterialName);
                            break;
                        }
                    case "РБ10":
                        {
                            toAdd.Name = Encoder(ev.Bunker10MaterialName);
                            break;
                        }
                    case "РБ11":
                        {
                            toAdd.Name = Encoder(ev.Bunker11MaterialName);
                            break;
                        }
                    case "РБ12":
                        {
                            toAdd.Name = Encoder(ev.Bunker12MaterialName);
                            break;
                        }
                    default:
                        {
                            toAdd.Name = "Нет данных";
                            break;
                        }
                }
                result.Add(toAdd);
            }
            return result;
        }

        /// <summary>
        /// Вычитываем из базы значения для текущей плавки
        /// </summary>
        /// <returns>Список строчек для грида по порядку</returns>
        private ObservableCollection<TableRowModel> FillTable()
        {
            var result = new ObservableCollection<TableRowModel>();

            if (!_steps.Any())
                return result;

            int stepsAmount = _steps.Count();

            // создаем и заполняем список значений для фурмы
            var furma = new TableRowModel { Values = new TableCell[stepsAmount] };
            var o2Flow = new TableRowModel { Values = new TableCell[stepsAmount] };
            var o2Sum = new TableRowModel { Values = new TableCell[stepsAmount] };

            var lanceGraphData = new List<Point>();
            var lanceGraphDataRealTime = new List<Point>();

            for (int i = 0; i < stepsAmount; i++)
            {
                furma.Values[i] = new TableCell();
                if (furma.Values[i].CellValue != "-1" || furma.Values[i].CellValue != "-2")
                    furma.Values[i].CellValue = _steps[i].LancePosition.ToString(CultureInfo.InvariantCulture);
                else
                    furma.Values[i].CellValue = string.Empty;
                furma.Values[i].AllowToAdd = false;
                furma.Values[i].NotToGive = false;
                o2Flow.Values[i] = new TableCell
                                       {
                                           CellValue = _steps[i].LanceO2Flow.ToString(CultureInfo.InvariantCulture),
                                           AllowToAdd = false,
                                           NotToGive = false
                                       };
                o2Sum.Values[i] = new TableCell
                                      {
                                          CellValue = _steps[i].O2Volume.ToString(CultureInfo.InvariantCulture),
                                          AllowToAdd = false,
                                          NotToGive = false
                                      };
                if (i == 0)
                {
                    lanceGraphData.Add(new Point(double.Parse(o2Sum.Values[i].CellValue),
                                             double.Parse(furma.Values[i].CellValue)));
                }
                else
                {
                    lanceGraphData.Add(new Point(double.Parse(o2Sum.Values[i].CellValue),
                                             double.Parse(furma.Values[i - 1].CellValue)));
                    lanceGraphData.Add(new Point(double.Parse(o2Sum.Values[i].CellValue),
                                             double.Parse(furma.Values[i].CellValue)));
                }
                
            }
            LineGraph = new RawDataSource(lanceGraphData);
            LineGraphRealTime = new RawDataSource(lanceGraphDataRealTime);
            result.Add(furma);
            result.Add(o2Flow);
            result.Add(o2Sum);

            InitialMatherialsSum = new ObservableCollection<int>();


            //дальше добавляем значения материалов

            for (int i = 1; i <= _currentHeat.Materials.Count; i++)
            {
                InitialMatherialsSum.Add(0);
                CurrentMatherialsSum.Add(0);
                PreviousMatherialsSum.Add(0);
                var toAdd = new TableRowModel { Values = new TableCell[stepsAmount] };
                for (int j = 0; j < stepsAmount; j++)
                {
                    IOrderedEnumerable<VerticalTrakt> vTs = from t in _steps[j].VerticalTrakts
                                                            orderby t.Material.Number
                                                            select t;
                    toAdd.Values[j] = new TableCell();

                    /* если в шаге есть хоть какие-то значения по материалам,
					проверяем есть ли среди них значение для текущего в итерации номера материала */
                    if (vTs.Any())
                        foreach (VerticalTrakt vt in vTs)
                        {
                            //если значение для материала есть, берем, если нету = делаем пустое
                            if (vt.Material.Number == i)
                            {
                                toAdd.Values[j].CellValue = vt.Amount;
                                toAdd.Values[j].NotToGive = vt.NotToGive;
                                toAdd.Values[j].AllowToAdd = vt.AllowToAdd;
                                InitialMatherialsSum[i - 1] += int.Parse(vt.Amount);
                                CurrentMatherialsSum[i - 1] += int.Parse(vt.Amount);
                                PreviousMatherialsSum[i - 1] += int.Parse(vt.Amount);
                                break;
                            }
                            toAdd.Values[j].CellValue = string.Empty;
                            toAdd.Values[j].NotToGive = false;
                            toAdd.Values[j].AllowToAdd = false;
                        }
                    else
                    {
                        toAdd.Values[j].CellValue = string.Empty;
                        toAdd.Values[j].NotToGive = false;
                        toAdd.Values[j].AllowToAdd = false;
                    }
                }
                result.Add(toAdd);
            }

            return result;
        }

        /// <summary>
        /// Заполняем столбцы датагрида
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<DataGridColumn> SetDataGridColumns()
        {
            var result = new ObservableCollection<DataGridColumn>();
            for (int i = 0; i < _steps.Count; i++)
            {
                var col = new DataGridTextColumn();
                //Here i bind to the various indices. 
                var binding = new Binding("Values[" + i + "].CellValue")
                                  {
                                      Mode = BindingMode.OneWay,
                                      UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                                  };
                col.Binding = binding;
                result.Add(col);
            }
            return result;
        }

        /// <summary>
        /// заполнение класса события плавки и отправка сообщения
        /// </summary>
        /// <param name="initial">true - отправка шаблона обычная, false - при нажатии на досыпку во время идущей плавки</param>
        private void StartHeat(bool initial)
        {
            if (initial)
            {
                Del d = delegate
                            {
                                _canColorSteps = true;
                                _canStartHeat = false;
                                HeatInProgress = true;
                                _canFurmaUp = false;
                                _canFurmaDown = false;
								_canO2IntenceUp = false;
								_canO2IntenceDown = false;
                                _canO2Plus = false;
                                _canO2Minus = false;
	                            _canReleaseAlConc = true;
                                _canReleaseCox = true;
                                _canReleaseIzvest = true;
                                GroupSelectingEnabled = false;
                                if (RTPointsList != null) RTPointsList.Clear();
                                CommandManager.InvalidateRequerySuggested();
                            };
                Dispatcher.CurrentDispatcher.BeginInvoke(d, DispatcherPriority.Render);
            }
	        var heatEvent = new SteelMakingPatternEvent();
            for (int i = 0; i < HGModel.AdditionsList.Count; i++)
            {
                heatEvent.materialsName[i] = HGModel.AdditionsList[i].Name;
            }

            if (HGModel.ItemsSource != null && HGModel.ItemsSource.Count > 0 && _steps.Count > 0)
            {
                for (int i = 0; i < _steps.Count; i++)
                {
                    var step = new Converter.SteelMakingClasses.Step { lance = new Lance() };

                    // позиция фурмы

                    int lancePosition;
                    bool parsed = int.TryParse(HGModel.ItemsSource[0].Values[i].CellValue, out lancePosition);
                    step.lance.LancePositin = parsed ? lancePosition : -1;

                    // интенсивность О2

                    double o2Flow;
                    parsed = double.TryParse(HGModel.ItemsSource[1].Values[i].CellValue, out o2Flow);
                    step.lance.O2Flow = parsed ? o2Flow : -1;

                    // суммарный кислород

                    int o2Sum;
                    parsed = int.TryParse(HGModel.ItemsSource[2].Values[i].CellValue, out o2Sum);
                    if (_previousO2Sum > 0)
                    {
                        if (parsed && o2Sum > 0)
                        {
                            step.O2Volume = o2Sum;
                            _previousO2Sum = o2Sum;
                        }
                        else
                            step.O2Volume = _previousO2Sum;
                    }
                    else
                    {
                        if (parsed)
                            step.O2Volume = o2Sum;
                        else
                            step.O2Volume = -1;
                    }

                    // материалы

                    for (int j = 3; j < HGModel.AdditionsList.Count + 3; j++)
                    {
                        int additionValue;
                        string currentLine = "";
                        parsed = int.TryParse(HGModel.ItemsSource[j].Values[i].CellValue, out additionValue);
                        if (additionValue > 0)
                        {
                            for (int l = 1; l < HGModel.AdditionsList[j - 3].WeighterLine.Length; l++)
                            {
                                currentLine += HGModel.AdditionsList[j - 3].WeighterLine[l];
                            }
                            int currentWeighterLine = int.Parse(currentLine);


                            string bunker = "";
                            for (int k = 2; k < HGModel.AdditionsList[j - 3].Bunker.Length; k++)
                            {
                                bunker += HGModel.AdditionsList[j - 3].Bunker[k];
                            }
                            int bunkerN = int.Parse(bunker) - 5;

                            if (parsed)
                            {
                                step.weigherLines[currentWeighterLine - 3].PortionWeight = additionValue;
                            }
                            else
                                step.weigherLines[currentWeighterLine - 3].PortionWeight = -1;
                            step.weigherLines[currentWeighterLine - 3].NotToGive =
                                HGModel.ItemsSource[j].Values[i].NotToGive;
                            step.weigherLines[currentWeighterLine - 3].AllowToAdd =
                                HGModel.ItemsSource[j].Values[i].AllowToAdd;
                            step.weigherLines[currentWeighterLine - 3].BunkerId = bunkerN;
                        }
                    }

                    heatEvent.steps.Add(step);
                }
            }
            _mainGate.PushEvent(heatEvent);
        }

        /// <summary>
        /// Задание значения коррекций
        /// </summary>
        private void SetAdditions()
        {
            for (int i = 3; i < HGModel.ItemsSource.Count; i++)
            {
                if (i - 3 == 1 || i - 3 == 2 || i - 3 == 5 || i - 3 == 6)
                {
                    int last = 0;
                    for (int j = 0; j < HGModel.ItemsSource[i].Values.Count(); j++)
                    {
                        int value;
                        bool parsed = int.TryParse(HGModel.ItemsSource[i].Values[j].CellValue, out value);
                        if (parsed)
                        {
                            last = j;
                        }
                    }
                    if (last == 0)
                    {
                        HGModel.ItemsSource[i].Values[HGModel.ItemsSource[i].Values.Count() - 1].CellValue =
                            (PreviousMatherialsSum[i - 3] + CurrentMatherialsSum[i - 3]).ToString(
                                CultureInfo.InvariantCulture);
                        PreviousMatherialsSum[i - 3] = CurrentMatherialsSum[i - 3];
                    }
                    else
                    {
                        HGModel.ItemsSource[i].Values[last].CellValue =
                            (int.Parse(HGModel.ItemsSource[i].Values[last].CellValue) -
                             (PreviousMatherialsSum[i - 3] - CurrentMatherialsSum[i - 3])).ToString(
                                 CultureInfo.InvariantCulture);
                        PreviousMatherialsSum[i - 3] = CurrentMatherialsSum[i - 3];
                    }
                }

                if (i - 3 == 3 || i - 3 == 4)
                {
                    int first = 0;
                    for (int j = 0; j < HGModel.ItemsSource[i].Values.Count(); j++)
                    {
                        int value;
                        bool parsed = int.TryParse(HGModel.ItemsSource[i].Values[j].CellValue, out value);
                        if (parsed && first == 0)
                        {
                            first = j;
                        }
                    }
                    HGModel.ItemsSource[i].Values[first].CellValue =
                        (int.Parse(HGModel.ItemsSource[i].Values[first].CellValue) -
                         (PreviousMatherialsSum[i - 3] - CurrentMatherialsSum[i - 3])).ToString(
                             CultureInfo.InvariantCulture);
                    PreviousMatherialsSum[i - 3] = CurrentMatherialsSum[i - 3];
                }

                if (i - 3 == 0 || i - 3 == 7)
                {
                    int position = 0;
                    if (i - 3 == 0)
                    {
                        if (_steps.Count == 32)
                        {
                            position = 27;
                        }
                        if (_steps.Count == 40)
                        {
                            position = 35;
                        }
                    }
                    else
                    {
                        if (_steps.Count == 32)
                        {
                            position = 25;
                        }
                        if (_steps.Count == 40)
                        {
                            position = 33;
                        }
                    }
                    HGModel.ItemsSource[i].Values[position].CellValue = String.Empty;
                    HGModel.ItemsSource[i].Values[position + 1].CellValue = String.Empty;
                    HGModel.ItemsSource[i].Values[position + 2].CellValue = String.Empty;
                    HGModel.ItemsSource[i].Values[position + 3].CellValue = String.Empty;
                    int delta = CurrentMatherialsSum[i - 3] - InitialMatherialsSum[i - 3];
                    if (delta >= 1000)
                    {
                        HGModel.ItemsSource[i].Values[position].CellValue = "1000";
                        delta -= 1000;
                    }
                    else
                    {
                        if (delta != 0)
                        {
                            HGModel.ItemsSource[i].Values[position].CellValue = delta.ToString(CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            HGModel.ItemsSource[i].Values[position].CellValue = string.Empty;
                        }
                        delta = 0;
                    }

                    if (delta >= 1000)
                    {
                        HGModel.ItemsSource[i].Values[position + 1].CellValue = "1000";
                        delta -= 1000;
                    }
                    else
                    {
                        if (delta != 0)
                        {
                            HGModel.ItemsSource[i].Values[position + 1].CellValue =
                                delta.ToString(CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            HGModel.ItemsSource[i].Values[position + 1].CellValue = string.Empty;
                        }
                        delta = 0;
                    }
                    if (delta >= 1000)
                    {
                        HGModel.ItemsSource[i].Values[position + 2].CellValue = "1000";
                        delta -= 1000;
                    }
                    else
                    {
                        if (delta != 0)
                        {
                            HGModel.ItemsSource[i].Values[position + 2].CellValue =
                                delta.ToString(CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            HGModel.ItemsSource[i].Values[position + 2].CellValue = string.Empty;
                        }
                        delta = 0;
                    }
                    if (delta >= 1000) HGModel.ItemsSource[i].Values[position + 3].CellValue = "1000";
                    else
                        if (delta != 0)
                        {
                            HGModel.ItemsSource[i].Values[position + 3].CellValue = delta.ToString(
                                CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            HGModel.ItemsSource[i].Values[position + 3].CellValue = string.Empty;
                        }
                }
            }

            if (HeatInProgress)
            {
                StartHeat(false);
            }
        }

        /// <summary>
        /// Повысить значение фурмы на 5
        /// </summary>
        private void FurmaUp()
        {
            foreach (TableCell item in HGModel.ItemsSource[0].Values)
            {
                int value;
                int.TryParse(item.CellValue, out value);
                item.CellValue = (value + 5).ToString(CultureInfo.InvariantCulture);
                if (_canFurmaDown == false)
                {
                    _canFurmaDown = true;
                    CommandManager.InvalidateRequerySuggested();
                }
            }

            var lanceGraphData = new List<Point>();
            for (int i = 0; i < _steps.Count(); i++)
            {

                if (i == 0)
                {
                    lanceGraphData.Add(new Point(double.Parse(HGModel.ItemsSource[2].Values[i].CellValue),
                                             double.Parse(HGModel.ItemsSource[0].Values[i].CellValue)));
                }
                else
                {
                    lanceGraphData.Add(new Point(double.Parse(HGModel.ItemsSource[2].Values[i].CellValue),
                                             double.Parse(HGModel.ItemsSource[0].Values[i - 1].CellValue)));
                    lanceGraphData.Add(new Point(double.Parse(HGModel.ItemsSource[2].Values[i].CellValue),
                                             double.Parse(HGModel.ItemsSource[0].Values[i].CellValue)));
                }
            }
            var ds = new RawDataSource(lanceGraphData);
            LineGraph.Data = ds.Data;
            LineGraph.RaiseDataChanged();
        }

        /// <summary>
        /// Понизить значение фурмы на 5
        /// </summary>
        private void FurmaDown()
        {
            foreach (TableCell item in HGModel.ItemsSource[0].Values)
            {
                int value;
                int.TryParse(item.CellValue, out value);
                item.CellValue = (value - 5).ToString(CultureInfo.InvariantCulture);
            }

            var lanceGraphData = new List<Point>();
            for (int i = 0; i < _steps.Count(); i++)
            {

                if (i == 0)
                {
                    lanceGraphData.Add(new Point(double.Parse(HGModel.ItemsSource[2].Values[i].CellValue),
                                             double.Parse(HGModel.ItemsSource[0].Values[i].CellValue)));
                }
                else
                {
                    lanceGraphData.Add(new Point(double.Parse(HGModel.ItemsSource[2].Values[i].CellValue),
                                             double.Parse(HGModel.ItemsSource[0].Values[i - 1].CellValue)));
                    lanceGraphData.Add(new Point(double.Parse(HGModel.ItemsSource[2].Values[i].CellValue),
                                             double.Parse(HGModel.ItemsSource[0].Values[i].CellValue)));
                }
            }

            var ds = new RawDataSource(lanceGraphData);
            LineGraph.Data = ds.Data;
            LineGraph.RaiseDataChanged();
            foreach (TableCell item in HGModel.ItemsSource[0].Values)
            {
                int value;
                int.TryParse(item.CellValue, out value);
                if (value < 185)
                {
                    _canFurmaDown = false;
                    CommandManager.InvalidateRequerySuggested();
                    return;
                }
            }
        }

		/// <summary>
		/// Повысить значение интенсивности на 25
		/// </summary>
		private void O2IntenceUp()
		{
		    for (int index = 8; index < HGModel.ItemsSource[1].Values.Length; index++)
		    {
		        TableCell item = HGModel.ItemsSource[1].Values[index];
		        int value;
		        int.TryParse(item.CellValue, out value);
		        item.CellValue = (value + 25).ToString(CultureInfo.InvariantCulture);
		    }
		}

        /// <summary>
		/// Понизить значение интенсивности на 25
		/// </summary>
		private void O2IntenceDown()
        {
            for (int index = 8; index < HGModel.ItemsSource[1].Values.Length; index++)
            {
                TableCell item = HGModel.ItemsSource[1].Values[index];
                int value;
                int.TryParse(item.CellValue, out value);
                item.CellValue = (value - 25).ToString(CultureInfo.InvariantCulture);
            }
        }

        private void O2Plus()
        {
            if (_steps.Count == 32)
            {
                for (int i = 8; i < 24; i++)
                {
                    int value;
                    int.TryParse(HGModel.ItemsSource[2].Values[i].CellValue, out value);
                    HGModel.ItemsSource[2].Values[i].CellValue =
                        (value + (i - 7) * 5).ToString(CultureInfo.InvariantCulture);
                }

                var lanceGraphData = new List<Point>();
                for (int i = 0; i < _steps.Count(); i++)
                {

                    if (i == 0)
                    {
                        lanceGraphData.Add(new Point(double.Parse(HGModel.ItemsSource[2].Values[i].CellValue),
                                                 double.Parse(HGModel.ItemsSource[0].Values[i].CellValue)));
                    }
                    else
                    {
                        lanceGraphData.Add(new Point(double.Parse(HGModel.ItemsSource[2].Values[i].CellValue),
                                                 double.Parse(HGModel.ItemsSource[0].Values[i - 1].CellValue)));
                        lanceGraphData.Add(new Point(double.Parse(HGModel.ItemsSource[2].Values[i].CellValue),
                                                 double.Parse(HGModel.ItemsSource[0].Values[i].CellValue)));
                    }
                }
                var ds = new RawDataSource(lanceGraphData);
                LineGraph.Data = ds.Data;
                LineGraph.RaiseDataChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private void O2Minus()
        {
            if (_steps.Count == 32)
            {
                for (int i = 8; i < 24; i++)
                {
                    int value;
                    int.TryParse(HGModel.ItemsSource[2].Values[i].CellValue, out value);
                    HGModel.ItemsSource[2].Values[i].CellValue =
                        (value - (i - 7) * 5).ToString(CultureInfo.InvariantCulture);
                }

                var lanceGraphData = new List<Point>();
                for (int i = 0; i < _steps.Count(); i++)
                {

                    if (i == 0)
                    {
                        lanceGraphData.Add(new Point(double.Parse(HGModel.ItemsSource[2].Values[i].CellValue),
                                                 double.Parse(HGModel.ItemsSource[0].Values[i].CellValue)));
                    }
                    else
                    {
                        lanceGraphData.Add(new Point(double.Parse(HGModel.ItemsSource[2].Values[i].CellValue),
                                                 double.Parse(HGModel.ItemsSource[0].Values[i - 1].CellValue)));
                        lanceGraphData.Add(new Point(double.Parse(HGModel.ItemsSource[2].Values[i].CellValue),
                                                 double.Parse(HGModel.ItemsSource[0].Values[i].CellValue)));
                    }
                }
                var ds = new RawDataSource(lanceGraphData);
                LineGraph.Data = ds.Data;
                LineGraph.RaiseDataChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        // костыль, чтобы запрашивать режимы вручную
		private void RefreshRegimes()
		{
			var command = new OPCDirectReadEvent { EventName = typeof( ModeVerticalPathEvent ).Name };
			if ( _mainGate != null ) _mainGate.PushEvent( command );
			command = new OPCDirectReadEvent { EventName = typeof( ModeLanceEvent ).Name };
			if ( _mainGate != null ) _mainGate.PushEvent( command );
		}

        #region Сброс материалов

        private void ReleaseAlConc()
        {
            // В3 РБ6
            var toSend = new ReleaseWeigherEvent { WeigherId = 0 };
            if (_mainGate == null)
            {
                _mainGate = new Client();
                _mainGate.Subscribe();
            }
            _mainGate.PushEvent(toSend);

            Thread.Sleep(100);

            int value;
            bool parsed = int.TryParse(HGModel.ItemsSource[4].Values[CurrentStep].CellValue, out value);
            HGModel.ItemsSource[4].Values[CurrentStep].CellValue = parsed
                                                                                  ? (value + 120).ToString(
                                                                                      CultureInfo.InvariantCulture)
                                                                                  : "120";
            StartHeat(false);
        }

        private void ReleaseCox()
        {
            // В4 РБ7
            int value;
            bool parsed = int.TryParse(HGModel.ItemsSource[5].Values[CurrentStep].CellValue, out value);
            HGModel.ItemsSource[5].Values[CurrentStep].CellValue = parsed
                                                                                  ? (value + 300).ToString(
                                                                                      CultureInfo.InvariantCulture)
                                                                                  : "300";
            StartHeat(false);
            _canReleaseCox = false;
            CommandManager.InvalidateRequerySuggested();
        }

        private void ReleaseDoloms()
        {
            // В7 РБ12
            double currentCellValue;
            var parsed = Double.TryParse(HGModel.ItemsSource[10].Values[_currentStep].CellValue, out currentCellValue);
            HGModel.ItemsSource[10].Values[_currentStep].CellValue = parsed ? (currentCellValue + 500).ToString(CultureInfo.InvariantCulture) : "500";
            StartHeat(false);
            Thread.Sleep(50);
            var toSend = new ReleaseWeigherEvent { WeigherId = 4 };
            if (_mainGate == null)
            {
                _mainGate = new Client();
                _mainGate.Subscribe();
            }
            _mainGate.PushEvent(toSend);
        }

        private void ReleaseIzvest()
        {
            // В7 РБ12
            var toSend = new ReleaseWeigherEvent { WeigherId = 2 };
            var toSend2 = new ReleaseWeigherEvent { WeigherId = 3 };
            if (_mainGate == null)
            {
                _mainGate = new Client();
                _mainGate.Subscribe();
            }
            _mainGate.PushEvent(toSend);
            _mainGate.PushEvent(toSend2);
        }

        #endregion

        #region Nested type: Del

        private delegate void Del();

        #endregion

        public string Encoder(string str)
        {
            char[] charArray = str.ToCharArray();
            str = "";
            foreach (char c in charArray)
            {
                if (c > 190)
                {
                    str += (char)(c + 848);
                }
                else
                {
                    str += c;
                }
            }
            return str;
        }

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            if (_ctx != null)
            {
	            _ctx.Dispose();
            }
			_mainGate.Unsubscribe();
        }

        #endregion

        private void SaveTV()
        {
            if (_ctx != null)
            {
                TargetValues t = _ctx.TargetValues.First(i => i.GroupID == SelectedGroup.Id);
                t.C = TwC;
                t.CaO = TwCaO;
                t.CaOSiO2 = TwCaOSio2;
                t.Cr = TwCr;
                t.Cu = TwCu;
                t.FeO = TwFeO;
                t.MgO = TwMgO;
                t.Ni = TwNi;
                t.Mn = TwMn;
                t.P = TwP;
                t.S = TwS;
                t.T = TwT;
                _ctx.DetectChanges();
                _ctx.SaveChanges();
                MessageBox.Show("Целевые показатели для " + SelectedGroup.Id.ToString() + "й группы сохранены");
            }
        }
    }
}