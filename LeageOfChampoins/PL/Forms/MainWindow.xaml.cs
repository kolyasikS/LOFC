using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LOFC.PL.Forms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            TEST();
        }
        private void TEST()
        {
            //ClubsWindow clubs = new ClubsWindow();
            //clubs.ShowDialog();
            OwnersWindow owners = new OwnersWindow();
            owners.ShowDialog();
        }
        private void ClubsClick(object sender, RoutedEventArgs e)
        {
            ClubsWindow clubs = new();
            clubs.ShowDialog();
        }

        private void OwnersClick(object sender, RoutedEventArgs e)
        {
            OwnersWindow owners = new();
            owners.ShowDialog();
        }
    }
}
