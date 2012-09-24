using System;
using System.Windows;
using System.Threading;
using ConverterUI.ViewModels;

namespace ConverterUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
	    public App()
        {
            bool isnew;
            M = new Mutex(true, "ConverterUI", out isnew);
            if (!isnew)
            {
                MessageBox.Show("Визуализация уже запущена");
                Environment.Exit(0);
            }

			var window = new MainWindow();
			var viewModel = new MainWindowViewModel();
			window.DataContext = viewModel;
			window.Show();
        }

	    public Mutex M { get; set; }
    }
}
