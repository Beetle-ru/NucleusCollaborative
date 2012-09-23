using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Threading;
using ConverterUI.ViewModels.MainPage;

namespace ConverterUI.Views
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainView
    {
		public MainView()
        {
            InitializeComponent();
        }

	    private MainPageViewModel vm;

		private void MainView_Loaded_1( object sender, System.Windows.RoutedEventArgs e )
		{
			Debug.WriteLine( DateTime.Now.TimeOfDay + " Main View Loaded" );
			var T = new Thread(LoadVm) {IsBackground = true};
			T.Start();
		}

		private void LoadVm(object state)
		{
			Debug.WriteLine( DateTime.Now.TimeOfDay + " Loading ViewModel started" );
			vm = new MainPageViewModel();
			vm.Loaded += vm_Loaded;
			vm.Load();
		}

		void vm_Loaded( object sender )
		{
			Debug.WriteLine( DateTime.Now.TimeOfDay + " ViewModel Loaded. Setting DataContext" );
			Dispatcher.BeginInvoke( DispatcherPriority.Render, new Action( () => { this.DataContext = vm; this.UpdateLayout(); Debug.WriteLine( DateTime.Now.TimeOfDay + " DataContext was set" ); } ) );
		}
    }
}
