using DataLayer;
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
    /// Interaction logic for TeamChooser.xaml
    /// </summary>
    public partial class TeamChooser : Window
    {
        MainWindow f;
        
        public TeamChooser(MainWindow mainWindow)
        {
            InitializeComponent();
            f = mainWindow;

            FillComboBox();
        }

        public async void FillComboBox()
        {
            f.ShowLoading();
            List<string> teams = await Task.Run(() => Data.GetCountryNames());

            f.HideLoading();
            foreach (var i in teams)
            {
                cb_teams.Items.Add(i);
            }
                cb_teams.SelectedItem = teams[0];
        }

        private async void Btn_applyFavoriteTeam_Click(object sender, RoutedEventArgs e)
        {
            f.ShowLoading();
            f.fifa_id = await Data.GetCountryCode(cb_teams.Text);
            f.HideLoading();
            f.favTeamName = cb_teams.Text;
            await Task.Run(()=>f.CreateTeam(f.favTeamName));
            Hide();
            f.fillEnemyTeamChooser(f.team);
        }
    }
}
