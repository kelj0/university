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
using System.Windows.Shapes;

namespace WPFWorldCup
{
    /// <summary>
    /// Interaction logic for Initialsettings.xaml
    /// </summary>
    public partial class Initialsettings : Window
    {
        private MainWindow f;
        public Initialsettings(MainWindow mainWindow)
        {
            InitializeComponent();
            f = mainWindow;
        }

        private void Btn_fullscreen_Click(object sender, RoutedEventArgs e)
        {
            f.WindowState = WindowState.Maximized;
            f.windowsState = "fullscreen";
            Hide();
            f.showTeamChooser();
        }

        private void Btn_notFullscreen_Click(object sender, RoutedEventArgs e)
        {
            f.WindowState = WindowState.Normal;
            f.windowsState = "normal";
            Hide();
            f.showTeamChooser();
        }
    }
}
