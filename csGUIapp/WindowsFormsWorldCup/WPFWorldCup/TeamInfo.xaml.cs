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
    /// Interaction logic for TeamInfo.xaml
    /// </summary>
    public partial class TeamInfo : Window
    {
        private MainWindow f;
        public TeamInfo(MainWindow f)
        {
            InitializeComponent();
            this.f = f;
        }

        public async Task SetUp()
        {
            lbl_teamName.Content = f.team.teamName;
            lbl_teamTag.Content = f.team.teamCode;

            lbl_drawsV.Content = f.team.draws;
            lbl_goalsV.Content = f.team.goals_for;
            lbl_goalsRecivedV.Content = f.team.goals_against;
            lbl_diferenceV.Content = f.team.goals_difference;
            lbl_lossesV.Content = f.team.losses;
            lbl_playedV.Content = (int.Parse(f.team.losses)+ int.Parse(f.team.wins) + int.Parse(f.team.draws));
            lbl_winsV.Content = f.team.wins;
        }



    }
}
