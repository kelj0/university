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
    /// Interaction logic for EnemyTeamChooser.xaml
    /// </summary>
    public partial class EnemyTeamChooser : Window
    {
        MainWindow f;
        public EnemyTeamChooser(MainWindow f)
        {
            InitializeComponent();
            this.f = f;
        }

        private async void Btn_chooseEnemyTeam_Click(object sender, RoutedEventArgs e)
        {
            
            f.ShowLoading();
            f.enemyFifa_id = await Data.GetCountryCode(cb_enemyTeamChooser.Text);
            f.HideLoading();
            f.enemyTeamName = cb_enemyTeamChooser.Text;

            await f.CreateEnemyTeam(f.enemyTeamName);
            Hide();
        }
    }
}
