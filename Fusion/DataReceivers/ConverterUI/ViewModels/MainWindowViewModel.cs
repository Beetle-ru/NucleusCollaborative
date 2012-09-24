using System;
using System.Windows.Threading;

namespace ConverterUI.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		DispatcherTimer _timer;
		string _currentTime;

		public MainWindowViewModel()
		{
			PrepareStartupState();
		}

		#region Properties

		public string CurrentTime
		{
			get { return _currentTime; }
			set { _currentTime = value; OnPropertyChanged( "CurrentTime" ); }
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

		}
	}
}
