using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Threading;
using ElectroVisio.ViewModels;

namespace ElectroVisio
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        Mutex m;
        public App()
        {
            bool isnew;
            m = new Mutex(true, "ElectroVisio", out isnew);
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
    }
}
