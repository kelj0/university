using DataLayer;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace WPFWorldCup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string lng;
        public string favTeamName;
        public string fifa_id;
        public string enemyFifa_id;
        public string enemyTeamName;
        public Team team;
        public Team awayTeam;
        public ChooseLanguage chooseLanguageForm;
        public Initialsettings initialSettingsForm;
        public TeamChooser teamChooser;
        public EnemyTeamChooser enemyTeamChooser;
        public Loading loading = new Loading();
        public string windowsState = "";


        public MainWindow()
        {
            InitializeComponent();
            run();

        }

        public async void run()
        {
            Hide();

            chooseLanguageForm = new ChooseLanguage(this);
            initialSettingsForm = new Initialsettings(this);
            teamChooser = new TeamChooser(this);
            enemyTeamChooser = new EnemyTeamChooser(this);
            //put loading
            try
            {
                throw  new FileNotFoundException();
                //[0] language
                //[1] name 
                //[2] fav players(split with,)
                List<string> config = Data.ReadConfigFile();
                lng = config[0];
                favTeamName = config[1];
                Task.Run(() => CreateTeam(favTeamName));
            }
            catch (FileNotFoundException)
            {
                //show form to choose language
                chooseLanguageForm.Show();
                
                //show form to choose app window style
                //show form to choose your favorite team

            }

            //show form to choose team against yours

           
            //CreateOtherTeam();
            //end loading
            //ShowMain();

        }

        public void fillEnemyTeamChooser(Team t)
        {
            string n="";
            foreach (var m in t.matches)
            {    
                if (m.home_team == t.teamName)
                {
                    enemyTeamChooser.cb_enemyTeamChooser.Items.Add(m.away_team);
                    n = m.away_team;
                }
                else
                {
                    enemyTeamChooser.cb_enemyTeamChooser.Items.Add(m.home_team);
                }
            }
            enemyTeamChooser.cb_enemyTeamChooser.SelectedItem = n;
            showChooseEnemyTeam();
        }

        public async void showTeamChooser()
        {
            teamChooser.Show();
        }

        public async void showChooseEnemyTeam()
        {
            enemyTeamChooser.Show();
        }

        public async Task CreateTeam(string teamName)
        {
            team = new Team(teamName, fifa_id);
            await team.SetUp();
            SetHomeTeamLabels(team);
            return;
        }

        public async Task CreateEnemyTeam(string teamName)
        {
            awayTeam = new Team(teamName, enemyFifa_id);
            await awayTeam.SetUp();
            Console.WriteLine(team.players.Keys);
            Console.WriteLine(awayTeam.players.Keys);
            return;
        }

        public async void SetHomeTeamLabels(Team homeTeam)
        {
            
        }

        public async void SetAwayTeamLabels(Team awayTeam)
        {

        }

        public async void ShowLoading()
        {
            loading.Visibility = Visibility.Visible;
        }
        public async void HideLoading()
        {
            loading.Visibility = Visibility.Hidden;
        }

    }
}
