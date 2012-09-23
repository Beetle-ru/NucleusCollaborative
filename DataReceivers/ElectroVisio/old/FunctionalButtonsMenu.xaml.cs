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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConverterVisio
{
    /// <summary>
    /// Interaction logic for FunctionalButtonsMenu.xaml
    /// </summary>
    ///    

    public partial class FunctionalButtonsMenu : UserControl
    {
        public struct fMenuItem
        {
            public string Text;
            public string Uri;
        }

        public Dictionary<int, fMenuItem> MenuItems = new Dictionary<int, fMenuItem>();

        public Page parentPage;


        public FunctionalButtonsMenu()
        {
            InitializeComponent();
        }

        public FunctionalButtonsMenu(Page page)
        {
            parentPage = page;
            InitializeComponent();
        }

        public void AddItem(int index, string text, string uri)
        {
            if (index < 0 || index > 11)
                return;
            fMenuItem menuItem = new fMenuItem();
            menuItem.Text = text;
            menuItem.Uri = uri;
            MenuItems.Add(index, menuItem);
            Label label = (this.FindName(string.Format("lMenu{0}", index)) as Label);
            if (label != null)
            {
                label.Content = text;
                label.Tag = uri;
            }
 
        }

        public void UpdateMenu()
        {
            for (int i = 0; i < 12; i++)
            {
                Label label = (this.FindName(string.Format("lMenu{0}", i)) as Label);
                if (label != null)
                {
                    label.Content = MenuItems[i].Text;
                    label.Tag = MenuItems[i].Uri;
                }
            }
        }


        private void lMenu_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService ns;
            ns = NavigationService.GetNavigationService(parentPage);
            ns.Navigate(new Uri((sender as Label).Tag.ToString(), UriKind.RelativeOrAbsolute));
        }


    }
}
