using System.Diagnostics;
using ConverterUI.ViewModels.MainPage;
using ConverterUI.Views;

namespace ConverterUI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private MainView _mv;
		public void Navigate( string page )
		{
			if ( page == @"\Views\MainView.xaml" )
			{
				_mv = new MainView();
				MainFrame.Navigate(_mv);
			}
			
		}

		private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            Process.Start("calc.exe");
        }

		public void Button_Click_2(object sender, System.Windows.RoutedEventArgs e)
        {
	        if (_mv != null && _mv.DataContext != null)
	        {
		        var mainPageViewModel = _mv.DataContext as MainPageViewModel;
		        if (mainPageViewModel != null) mainPageViewModel.Dispose();
	        }
			Close();
        }

        private void Button_Click_3(object sender, System.Windows.RoutedEventArgs e)
        {
            Process.Start("ShutDown", "/i");

        }

        private void Button_Click_4(object sender, System.Windows.RoutedEventArgs e)
        {
            Process.Start("Taskmgr.exe");
        }

        private void Button_Click_5(object sender, System.Windows.RoutedEventArgs e)
        {
            Process.Start("Explorer.exe", "/p");
        }
	}
}
