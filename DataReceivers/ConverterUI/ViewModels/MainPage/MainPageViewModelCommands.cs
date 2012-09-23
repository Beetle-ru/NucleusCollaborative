using System.Windows.Input;

namespace ConverterUI.ViewModels.MainPage
{
    public sealed partial class MainPageViewModel
    {
        private bool _canFurmaDown = true;
        private bool _canFurmaUp = true;
        private bool _canO2Minus;
        private bool _canO2Plus;
        private bool _canReleaseAlConc;
        private bool _canReleaseCox;
        private bool _canReleaseDoloms;
        private bool _canReleaseIzvest;
        private bool _canSetAdditions;
        private bool _canStartHeat;
        private RelayCommand _furmaDownCommand;
        private RelayCommand _furmaUpCommand;
        private RelayCommand _o2MinusCommand;
        private RelayCommand _o2PlusCommand;
        private RelayCommand _releaseAlConcCommand;
        private RelayCommand _releaseCoxCommand;
        private RelayCommand _releaseDolomsCommand;
        private RelayCommand _releaseIzvestCommand;
        private RelayCommand _setAdditionsCommand;
        private RelayCommand _startHeatCommand;
	    private RelayCommand _refreshRegimesCommand;
	    private bool _canRefreshRegimes;
	    private RelayCommand _o2IntenceUpCommand;
	    private bool _canO2IntenceUp = true;
	    private RelayCommand _o2IntenceDownCommand;
	    private bool _canO2IntenceDown = true;
        private RelayCommand _saveTVCommand;
        private bool _canSaveTV = false;

        //RefreshRegimesCommand

		public ICommand RefreshRegimesCommand
		{
			get
			{
				if ( _refreshRegimesCommand == null )
				{
					_refreshRegimesCommand = new RelayCommand( param => RefreshRegimes(),
															 param => CanRefreshRegimes );
				}
				return _refreshRegimesCommand;
			}
		}

		private bool CanRefreshRegimes
		{
			get { return _canRefreshRegimes; }
		}


	    public ICommand ReleaseAlConcCommand
        {
            get
            {
                if (_releaseAlConcCommand == null)
                {
                    _releaseAlConcCommand = new RelayCommand(param => ReleaseAlConc(),
                                                             param => CanReleaseAlConc);
                }
                return _releaseAlConcCommand;
            }
        }

        private bool CanReleaseAlConc
        {
            get { return _canReleaseAlConc; }
        }

        public ICommand ReleaseCoxCommand
        {
            get
            {
                if (_releaseCoxCommand == null)
                {
                    _releaseCoxCommand = new RelayCommand(param => ReleaseCox(),
                                                          param => CanReleaseCox);
                }
                return _releaseCoxCommand;
            }
        }

        private bool CanReleaseCox
        {
            get { return _canReleaseCox; }
        }

        public ICommand ReleaseDolomsCommand
        {
            get
            {
                if (_releaseDolomsCommand == null)
                {
                    _releaseDolomsCommand = new RelayCommand(param => ReleaseDoloms(),
                                                             param => CanReleaseDoloms);
                }
                return _releaseDolomsCommand;
            }
        }

        private bool CanReleaseDoloms
        {
            get { return _canReleaseDoloms; }
        }

        public ICommand ReleaseIzvestCommand
        {
            get
            {
                if (_releaseIzvestCommand == null)
                {
                    _releaseIzvestCommand = new RelayCommand(param => ReleaseIzvest(),
                                                             param => CanReleaseIzvest);
                }
                return _releaseIzvestCommand;
            }
        }

        private bool CanReleaseIzvest
        {
            get { return _canReleaseIzvest; }
        }

        public ICommand StartHeatCommand
        {
            get
            {
                if (_startHeatCommand == null)
                {
                    _startHeatCommand = new RelayCommand(param => StartHeat(true),
                                                         param => CanStartHeat);
                    
                }
                return _startHeatCommand;
            }
        }

        private bool CanStartHeat
        {
            get { return _canStartHeat; }
        }

        public ICommand SetAdditionsCommand
        {
            get
            {
                if (_setAdditionsCommand == null)
                {
                    _setAdditionsCommand = new RelayCommand(param => SetAdditions(),
                                                            param => CanSetAdditions);
                }
                return _setAdditionsCommand;
            }
        }

        private bool CanSetAdditions
        {
            get { return _canSetAdditions; }
        }

        public ICommand FurmaUpCommand
        {
            get
            {
                if (_furmaUpCommand == null)
                {
                    _furmaUpCommand = new RelayCommand(param => FurmaUp(),
                                                       param => CanFurmaUp);
                }
                return _furmaUpCommand;
            }
        }

        private bool CanFurmaUp
        {
            get { return _canFurmaUp; }
        }

	    public ICommand O2IntenceUpCommand
	    {
		    get
		    {
			    if(_o2IntenceUpCommand == null)
			    {
					_o2IntenceUpCommand = new RelayCommand( param => O2IntenceUp(),
													   param => CanO2IntenceUp );
			    }
				return _o2IntenceUpCommand;
		    }
	    }

		private bool CanO2IntenceUp
	    {
			get { return _canO2IntenceUp; }
	    }

		public ICommand O2IntenceDownCommand
		{
			get
			{
				if ( _o2IntenceDownCommand == null )
				{
					_o2IntenceDownCommand = new RelayCommand( param => O2IntenceDown(),
													   param => CanO2IntenceDown );
				}
				return _o2IntenceDownCommand;
			}
		}

		private bool CanO2IntenceDown
		{
			get { return _canO2IntenceDown; }
		}

	    public ICommand FurmaDownCommand
        {
            get
            {
                if (_furmaDownCommand == null)
                {
                    _furmaDownCommand = new RelayCommand(param => FurmaDown(),
                                                         param => CanFurmaDown);
                }
                return _furmaDownCommand;
            }
        }

        private bool CanFurmaDown
        {
            get { return _canFurmaDown; }
        }

        public ICommand O2PlusCommand
        {
            get
            {
                if (_o2PlusCommand == null)
                {
                    _o2PlusCommand = new RelayCommand(param => O2Plus(),
                                                      param => CanO2Plus);
                }
                return _o2PlusCommand;
            }
        }

        private bool CanO2Plus
        {
            get { return _canO2Plus; }
        }

        public ICommand O2MinusCommand
        {
            get
            {
                if (_o2MinusCommand == null)
                {
                    _o2MinusCommand = new RelayCommand(param => O2Minus(),
                                                       param => CanO2Minus);
                }
                return _o2MinusCommand;
            }
        }

        private bool CanO2Minus
        {
            get { return _canO2Minus; }
        }

        public ICommand SaveTVCommand
        {
            get
            {
                if (_saveTVCommand == null)
                {
                    _saveTVCommand = new RelayCommand(param => SaveTV(),
                                                       param => CanSaveTV);
                }
                return _saveTVCommand;
            }
        }

        private bool CanSaveTV
        {
            get { return _canSaveTV; }
        }
    }
}
