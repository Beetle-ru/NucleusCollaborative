using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Threading;

namespace ElectroVisio.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		DispatcherTimer _timer;
		string _currentTime;
	    private double _reactorAngle = 0.0;
        private double _dAngle = 1.0;

		public MainWindowViewModel()
		{
			PrepareStartupState();
		}

		#region Properties

		public string CurrentTime
		{
			get { return _currentTime; }
			set { _currentTime = value; OnPropertyChanged("CurrentTime"); }
		}

        public double ReactorAngle
        {
            get { return _reactorAngle; }
            set { _reactorAngle = value; OnPropertyChanged("ReactorAngle"); }
        }


		#endregion

		void PrepareStartupState()
		{
			_currentTime = "";
			_timer = new DispatcherTimer { Interval = new TimeSpan( 0, 0, 0, 1 ) };
			_timer.Tick += TimerTick;
			_timer.Start();
		}

		void TimerTick( object sender, EventArgs e )
		{
			CurrentTime = String.Format( "{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToShortDateString() );
            if (ReactorAngle > 7.0 || ReactorAngle < 4.0) _dAngle = -_dAngle;
		    ReactorAngle += _dAngle;
		}
	}
}
