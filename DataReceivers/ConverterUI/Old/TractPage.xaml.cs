﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConverterUI
{
    /// <summary>
    /// Interaction logic for TractPage.xaml
    /// </summary>
    public partial class TractPage : Page
    {
        public TractPage()
        {
            InitializeComponent();
            this.DataContext = Helper.HeatInfo;
        }
    }
}
