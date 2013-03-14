using System;
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
using System.Windows.Shapes;

namespace Charge5UI.PatternEditor {
    /// <summary>
    /// Логика взаимодействия для CreatePattern.xaml
    /// </summary>
    public partial class CreatePattern : Window {
        public CreatePattern() {
            InitializeComponent();
            lblOldPatternName.Content = String.Format("\"{0}\"", Pointer.PPatternEditor.PatternLoadedName);
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e) {
            Pointer.PPatternEditor.PatternLoadedName = tbName.Text;
            Pointer.PPatternEditor.StatusChange(String.Format("Паттерн \"{0}\" создан", tbName.Text));
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}