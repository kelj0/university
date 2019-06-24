using DataLayer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
        public List<string> teams = new List<string>();
        public TeamInfo teaminfo;
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
            teaminfo = new TeamInfo(this);


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



        public async void ShowMain()
        {
            Show();
        }

        public async Task changeLanguageToENG()
        {
            initialSettingsForm.btn_fullscreen.Content = "Fullscreen";
            initialSettingsForm.btn_notFullscreen.Content = "Normal";
            enemyTeamChooser.lbl_chooseEnemyTeam.Content = "Choose enemy team";
            enemyTeamChooser.btn_chooseEnemyTeam.Content = "Apply";
            teamChooser.lbl_chooseTeam.Content = "Choose your favorite team";
            teamChooser.btn_applyFavoriteTeam.Content = "Apply";
            teaminfo.lbl_diference.Content = "Difference";
            teaminfo.lbl_draws.Content = "Draws";
            teaminfo.lbl_goals.Content = "Goals";
            teaminfo.lbl_goalsRecived.Content = "Recived goals";
            teaminfo.lbl_losses.Content = "Losses";
            teaminfo.lbl_played.Content = "Played";
            teaminfo.lbl_wins.Content = "Wins";
        }

        public async Task changeLanguageToCRO()
        {
            initialSettingsForm.btn_fullscreen.Content = "Puni zaslon";
            initialSettingsForm.btn_notFullscreen.Content = "Normalni prozor";
            enemyTeamChooser.lbl_chooseEnemyTeam.Content = "Odaberite protivnicki tim";
            enemyTeamChooser.btn_chooseEnemyTeam.Content = "Odaberi";
            teamChooser.lbl_chooseTeam.Content = "Odaberite omiljeni tim";
            teamChooser.btn_applyFavoriteTeam.Content = "Odaberi";
            teaminfo.lbl_diference.Content = "Razlika";
            teaminfo.lbl_draws.Content = "Nerjeseno";
            teaminfo.lbl_goals.Content = "Golovi";
            teaminfo.lbl_goalsRecived.Content = "Primljeni golovi";
            teaminfo.lbl_losses.Content = "Izgubljene";
            teaminfo.lbl_played.Content = "Odigrane";
            teaminfo.lbl_wins.Content = "Pobjede";
        }

        /* Team methods */
        public async Task fillEnemyTeamChooser(Team t)
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
            await SetHomeTeamLabels(team);
        }

        public async Task CreateEnemyTeam(string teamName)
        {
            awayTeam = new Team(teamName, enemyFifa_id);
            await awayTeam.SetUp();
            await SetAwayTeamLabels(awayTeam);
            await SetMatchLabels();
            Show();
        }

        public async Task SetHomeTeamLabels(Team homeTeam)
        {
            await showHomeTeamField();
            int defender = 0;
            int midfielder = 0;
            int forward = 0;
            List<Player> firstElevenWithMoreInfo = new List<Player>();
            foreach (var p in homeTeam.firsteleven)
            {
                firstElevenWithMoreInfo.Add(homeTeam.players[p]);
                switch (homeTeam.players[p].position)
                {
                    case "Defender": defender++; break;
                    case "Midfield": midfielder++; break;
                    case "Forward": forward++; break;
                }
            }
            int defCounter = 0;
            int midCounter = 0;
            int forCounter = 0;
            foreach (var i in firstElevenWithMoreInfo)
            {
                switch (i.position)
                {
                    case "Defender":
                        switch (defender)
                        {
                            case 3:
                                switch (defCounter++)
                                {
                                    case 0: lbl_defenderHome4.Content = i.name; break;
                                    case 1: lbl_defenderHome1.Content = i.name; break;
                                    case 2: lbl_defenderHome2.Content = i.name; break;
                                }
                                lbl_defenderHome0.Visibility = Visibility.Hidden;
                                lbl_defenderHome3.Visibility = Visibility.Hidden;
                                box_homeDefender0.Visibility = Visibility.Hidden;
                                box_homeDefender3.Visibility = Visibility.Hidden;
                                lbl_midfieldHome5.Visibility = Visibility.Hidden;
                                box_homeMidfield5.Visibility = Visibility.Hidden;
                                break;
                            case 4:
                                switch (defCounter++)
                                {
                                    case 0: lbl_defenderHome0.Content = i.name; break;
                                    case 1: lbl_defenderHome1.Content = i.name; break;
                                    case 2: lbl_defenderHome2.Content = i.name; break;
                                    case 3: lbl_defenderHome3.Content = i.name; break;
                                }
                                lbl_defenderHome4.Visibility = Visibility.Hidden;
                                box_homeDefender4.Visibility = Visibility.Hidden;
                                lbl_midfieldHome5.Visibility = Visibility.Hidden;
                                box_homeMidfield5.Visibility = Visibility.Hidden;
                                break;
                            case 5:
                                switch (defCounter++)
                                {
                                    case 0: lbl_defenderHome0.Content = i.name; break;
                                    case 1: lbl_defenderHome1.Content = i.name; break;
                                    case 2: lbl_defenderHome2.Content = i.name; break;
                                    case 3: lbl_defenderHome3.Content = i.name; break;
                                    case 4: lbl_defenderHome4.Content = i.name; break;
                                }
                                break;
                        }
                        break;
                    case "Midfield":
                        switch (midfielder)
                        {
                            case 2:
                                switch (midCounter++){
                                    case 0: lbl_midfieldHome1.Content = i.name; break;
                                    case 1: lbl_midfieldHome2.Content = i.name; break;}
                                    lbl_midfieldHome0.Visibility = Visibility.Hidden;
                                    lbl_midfieldHome3.Visibility = Visibility.Hidden;
                                    lbl_midfieldHome4.Visibility = Visibility.Hidden;
                                    box_homeMidfield0.Visibility = Visibility.Hidden;
                                    box_homeMidfield3.Visibility = Visibility.Hidden;
                                    box_homeMidfield4.Visibility = Visibility.Hidden;
                                    lbl_midfieldHome5.Visibility = Visibility.Hidden;
                                    box_homeMidfield5.Visibility = Visibility.Hidden;
                                break;
                            case 3:
                                switch (midCounter++){
                                    case 0: lbl_midfieldHome4.Content = i.name; break;
                                    case 1: lbl_midfieldHome1.Content = i.name; break;
                                    case 2: lbl_midfieldHome2.Content = i.name; break;}
                                lbl_midfieldHome0.Visibility = Visibility.Hidden;
                                lbl_midfieldHome3.Visibility = Visibility.Hidden;
                                box_homeMidfield0.Visibility = Visibility.Hidden;
                                box_homeMidfield3.Visibility = Visibility.Hidden;
                                lbl_midfieldHome5.Visibility = Visibility.Hidden;
                                box_homeMidfield5.Visibility = Visibility.Hidden;
                                break;
                            case 4:
                                switch (midCounter++)
                                {
                                    case 0: lbl_midfieldHome0.Content = i.name; break;
                                    case 1: lbl_midfieldHome1.Content = i.name; break;
                                    case 2: lbl_midfieldHome2.Content = i.name; break;
                                    case 3: lbl_midfieldHome3.Content = i.name; break;
                                }
                                lbl_midfieldHome4.Visibility = Visibility.Hidden;
                                box_homeMidfield4.Visibility = Visibility.Hidden;
                                lbl_midfieldHome5.Visibility = Visibility.Hidden;
                                box_homeMidfield5.Visibility = Visibility.Hidden;
                                break;
                            case 5:
                                switch (midCounter++){
                                    case 0: lbl_midfieldHome0.Content = i.name; break;
                                    case 1: lbl_midfieldHome1.Content = i.name; break;
                                    case 2: lbl_midfieldHome2.Content = i.name; break;
                                    case 3: lbl_midfieldHome3.Content = i.name; break;
                                    case 4: lbl_midfieldHome4.Content = i.name; break;}
                                lbl_midfieldHome5.Visibility = Visibility.Hidden;
                                box_homeMidfield5.Visibility = Visibility.Hidden;
                                break;
                            case 6:
                                switch (midCounter++)
                                {
                                    case 0: lbl_midfieldHome0.Content = i.name; break;
                                    case 1: lbl_midfieldHome1.Content = i.name; break;
                                    case 2: lbl_midfieldHome2.Content = i.name; break;
                                    case 3: lbl_midfieldHome3.Content = i.name; break;
                                    case 4: lbl_midfieldHome4.Content = i.name; break;
                                    case 5: lbl_midfieldHome5.Content = i.name; break;
                                }
                                break;
                        }
                        break;
                    case "Forward":
                        switch (forward)
                        {
                            case 1:
                                lbl_forwardHome0.Content = i.name;
                                lbl_forwardHome1.Visibility = Visibility.Hidden;
                                lbl_forwardHome2.Visibility = Visibility.Hidden;
                                lbl_forwardHome3.Visibility = Visibility.Hidden;
                                box_homeForward1.Visibility = Visibility.Hidden;
                                box_homeForward2.Visibility = Visibility.Hidden;
                                box_homeForward3.Visibility = Visibility.Hidden;
                                break;
                            case 2:
                                switch (forCounter++){
                                    case 0: lbl_forwardHome2.Content = i.name; break;
                                    case 1: lbl_forwardHome1.Content = i.name; break;}
                                lbl_forwardHome0.Visibility = Visibility.Hidden;
                                box_homeForward0.Visibility = Visibility.Hidden;
                                lbl_forwardHome3.Visibility = Visibility.Hidden;
                                box_homeForward3.Visibility = Visibility.Hidden;
                                break;
                            case 3:
                                switch (forCounter++){
                                    case 0: lbl_forwardHome0.Content = i.name; break;
                                    case 1: lbl_forwardHome1.Content = i.name; break;
                                    case 2: lbl_forwardHome2.Content = i.name; break;}
                                lbl_forwardHome3.Visibility = Visibility.Hidden;
                                box_homeForward3.Visibility = Visibility.Hidden;
                                break;
                            case 4:
                                switch (forCounter++){
                                    case 0: lbl_forwardHome0.Content = i.name; break;
                                    case 1: lbl_forwardHome1.Content = i.name; break;
                                    case 2: lbl_forwardHome2.Content = i.name; break;
                                    case 3: lbl_forwardHome3.Content = i.name; break;}
                                break;
                        }
                        break;
                    case "Goalie": lbl_goalieHome.Content = i.name; break;
                }
            }
        }

        public async Task SetAwayTeamLabels(Team awayTeam)
        {
            await showAwayTeamField();
            int defender = 0;
            int midfielder = 0;
            int forward = 0;
            List<Player> firstElevenWithMoreInfo = new List<Player>();

            foreach (var p in awayTeam.firsteleven)
            {
                firstElevenWithMoreInfo.Add(awayTeam.players[p]);
                switch (awayTeam.players[p].position)
                {
                    case "Defender": defender++; break;
                    case "Midfield": midfielder++; break;
                    case "Forward": forward++; break;
                }
            }
            int defCounter = 0;
            int midCounter = 0;
            int forCounter = 0;
            foreach (var i in firstElevenWithMoreInfo)
            {
                switch (i.position)
                {
                    case "Defender":
                        switch (defender)
                        {
                            case 3:
                                switch (defCounter++)
                                {
                                    case 0: lbl_defenderAway4.Content = i.name; break;
                                    case 1: lbl_defenderAway1.Content = i.name; break;
                                    case 2: lbl_defenderAway2.Content = i.name; break;
                                }
                                lbl_defenderAway0.Visibility = Visibility.Hidden;
                                lbl_defenderAway3.Visibility = Visibility.Hidden;
                                box_awayDefender0.Visibility = Visibility.Hidden;
                                box_awayDefender3.Visibility = Visibility.Hidden;
                                break;
                            case 4:
                                switch (defCounter++)
                                {
                                    case 0: lbl_defenderAway0.Content = i.name; break;
                                    case 1: lbl_defenderAway1.Content = i.name; break;
                                    case 2: lbl_defenderAway2.Content = i.name; break;
                                    case 3: lbl_defenderAway3.Content = i.name; break;
                                }
                                lbl_defenderAway4.Visibility = Visibility.Hidden;
                                box_awayDefender4.Visibility = Visibility.Hidden;
                                break;
                            case 5:
                                switch (defCounter++)
                                {
                                    case 0: lbl_defenderAway0.Content = i.name; break;
                                    case 1: lbl_defenderAway1.Content = i.name; break;
                                    case 2: lbl_defenderAway2.Content = i.name; break;
                                    case 3: lbl_defenderAway3.Content = i.name; break;
                                    case 4: lbl_defenderAway4.Content = i.name; break;
                                }
                                break;
                        }
                        break;
                    case "Midfield":
                        switch (midfielder)
                        {
                            case 2:
                                switch (midCounter++){
                                    case 0: lbl_midfieldHome1.Content = i.name; break;
                                    case 1: lbl_midfieldHome2.Content = i.name; break;}
                                    lbl_midfieldAway0.Visibility = Visibility.Hidden;
                                    lbl_midfieldAway3.Visibility = Visibility.Hidden;
                                    lbl_midfieldAway4.Visibility = Visibility.Hidden;
                                    box_awayMidfield0.Visibility = Visibility.Hidden;
                                    box_awayMidfield3.Visibility = Visibility.Hidden;
                                    box_awayMidfield4.Visibility = Visibility.Hidden;
                                    lbl_midfieldAway5.Visibility = Visibility.Hidden;
                                    box_awayMidfield5.Visibility = Visibility.Hidden;
                                break;
                            case 3:
                                switch (midCounter++)
                                {
                                    case 0: lbl_midfieldAway4.Content = i.name; break;
                                    case 1: lbl_midfieldAway1.Content = i.name; break;
                                    case 2: lbl_midfieldAway2.Content = i.name; break;
                                }
                                lbl_midfieldAway0.Visibility = Visibility.Hidden;
                                lbl_midfieldAway3.Visibility = Visibility.Hidden;
                                box_awayMidfield0.Visibility = Visibility.Hidden;
                                box_awayMidfield3.Visibility = Visibility.Hidden;
                                lbl_midfieldAway5.Visibility = Visibility.Hidden;
                                box_awayMidfield5.Visibility = Visibility.Hidden;
                                break;
                            case 4:
                                switch (midCounter++)
                                {
                                    case 0: lbl_midfieldAway0.Content = i.name; break;
                                    case 1: lbl_midfieldAway1.Content = i.name; break;
                                    case 2: lbl_midfieldAway2.Content = i.name; break;
                                    case 3: lbl_midfieldAway3.Content = i.name; break;
                                }
                                lbl_midfieldAway4.Visibility = Visibility.Hidden;
                                box_awayMidfield4.Visibility = Visibility.Hidden;
                                lbl_midfieldAway5.Visibility = Visibility.Hidden;
                                box_awayMidfield5.Visibility = Visibility.Hidden;
                                break;
                            case 5:
                                switch (midCounter++){
                                    case 0: lbl_midfieldAway0.Content = i.name; break;
                                    case 1: lbl_midfieldAway1.Content = i.name; break;
                                    case 2: lbl_midfieldAway2.Content = i.name; break;
                                    case 3: lbl_midfieldAway3.Content = i.name; break;
                                    case 4: lbl_midfieldAway4.Content = i.name; break;}
                                lbl_midfieldAway5.Visibility = Visibility.Hidden;
                                box_awayMidfield5.Visibility = Visibility.Hidden;
                                break;
                            case 6:
                                switch (midCounter++){
                                    case 0: lbl_midfieldAway0.Content = i.name; break;
                                    case 1: lbl_midfieldAway1.Content = i.name; break;
                                    case 2: lbl_midfieldAway2.Content = i.name; break;
                                    case 3: lbl_midfieldAway3.Content = i.name; break;
                                    case 4: lbl_midfieldAway4.Content = i.name; break;
                                    case 5: lbl_midfieldAway5.Content = i.name; break;}
                                break;
                        }
                        break;
                    case "Forward":
                        switch (forward)
                        {
                            case 1:
                                lbl_forwardAway0.Content = i.name;
                                lbl_forwardAway1.Visibility = Visibility.Hidden;
                                lbl_forwardAway2.Visibility = Visibility.Hidden;
                                lbl_forwardAway3.Visibility = Visibility.Hidden;
                                box_awayForward1.Visibility = Visibility.Hidden;
                                box_awayForward2.Visibility = Visibility.Hidden;
                                box_awayForward3.Visibility = Visibility.Hidden;
                                break;
                            case 2:
                                switch (forCounter++){
                                    case 0: lbl_forwardAway2.Content = i.name; break;
                                    case 1: lbl_forwardAway1.Content = i.name; break;}
                                lbl_forwardAway0.Visibility = Visibility.Hidden;
                                lbl_forwardAway3.Visibility = Visibility.Hidden;
                                box_awayForward0.Visibility = Visibility.Hidden;
                                box_awayForward3.Visibility = Visibility.Hidden;
                                break;
                            case 3:
                                switch (forCounter++){
                                    case 0: lbl_forwardAway0.Content = i.name; break;
                                    case 1: lbl_forwardAway1.Content = i.name; break;
                                    case 2: lbl_forwardAway2.Content = i.name; break;}
                                    lbl_forwardAway3.Visibility = Visibility.Hidden;
                                    box_awayForward3.Visibility = Visibility.Hidden;
                                break;
                            case 4:
                                switch (forCounter++){
                                    case 0: lbl_forwardAway0.Content = i.name; break;
                                    case 1: lbl_forwardAway1.Content = i.name; break;
                                    case 2: lbl_forwardAway2.Content = i.name; break;
                                    case 3: lbl_forwardAway3.Content = i.name; break;}
                                break;

                        }
                        break;
                    case "Goalie": lbl_goalieAway.Content = i.name; break;
                }
            }
        }

        public async Task SetMatchLabels()
        {
            Match match = Data.GetMatch(team, awayTeam.teamName);
            if (match.home_team == team.teamName){
                lbl_homeTeam.Content = match.home_team;
                lbl_homeTeamScore.Content = match.score.Split(':')[0];
                lbl_awayTeam.Content = match.away_team;
                lbl_awayTeamScore.Content = match.score.Split(':')[1];
            }
            else{
                lbl_homeTeam.Content = match.away_team;
                lbl_homeTeamScore.Content = match.score.Split(':')[1];
                lbl_awayTeam.Content = match.home_team;
                lbl_awayTeamScore.Content = match.score.Split(':')[0];
            }
        }

        private async Task showAwayTeamField()
        {
            /*labels forward*/
            lbl_forwardAway0.Visibility = Visibility.Visible;
            lbl_forwardAway1.Visibility = Visibility.Visible;
            lbl_forwardAway2.Visibility = Visibility.Visible;
            /*labels midfield*/
            lbl_midfieldAway0.Visibility = Visibility.Visible;
            lbl_midfieldAway1.Visibility = Visibility.Visible;
            lbl_midfieldAway2.Visibility = Visibility.Visible;
            lbl_midfieldAway3.Visibility = Visibility.Visible;
            lbl_midfieldAway4.Visibility = Visibility.Visible;
            /*labels defender*/
            lbl_defenderAway0.Visibility = Visibility.Visible;
            lbl_defenderAway1.Visibility = Visibility.Visible;
            lbl_defenderAway2.Visibility = Visibility.Visible;
            lbl_defenderAway3.Visibility = Visibility.Visible;
            lbl_defenderAway4.Visibility = Visibility.Visible;
            /*box forward*/
            box_awayForward0.Visibility = Visibility.Visible;
            box_awayForward1.Visibility = Visibility.Visible;
            box_awayForward2.Visibility = Visibility.Visible;
            /*box midfield*/
            box_awayMidfield0.Visibility = Visibility.Visible;
            box_awayMidfield1.Visibility = Visibility.Visible;
            box_awayMidfield2.Visibility = Visibility.Visible;
            box_awayMidfield3.Visibility = Visibility.Visible;
            box_awayMidfield4.Visibility = Visibility.Visible;
            /*box defender*/
            box_awayDefender0.Visibility = Visibility.Visible;
            box_awayDefender1.Visibility = Visibility.Visible;
            box_awayDefender2.Visibility = Visibility.Visible;
            box_awayDefender3.Visibility = Visibility.Visible;
            box_awayDefender4.Visibility = Visibility.Visible;
        }

        private async Task showHomeTeamField()
        {
            /*labels forward*/
            lbl_forwardHome0.Visibility = Visibility.Visible;
            lbl_forwardHome1.Visibility = Visibility.Visible;
            lbl_forwardHome2.Visibility = Visibility.Visible;
            /*labels midfield*/
            lbl_midfieldHome0.Visibility = Visibility.Visible;
            lbl_midfieldHome1.Visibility = Visibility.Visible;
            lbl_midfieldHome2.Visibility = Visibility.Visible;
            lbl_midfieldHome3.Visibility = Visibility.Visible;
            lbl_midfieldHome4.Visibility = Visibility.Visible;
            /*labels defender*/
            lbl_defenderHome0.Visibility = Visibility.Visible;
            lbl_defenderHome1.Visibility = Visibility.Visible;
            lbl_defenderHome2.Visibility = Visibility.Visible;
            lbl_defenderHome3.Visibility = Visibility.Visible;
            lbl_defenderHome4.Visibility = Visibility.Visible;
            /*box forward*/
            box_homeForward0.Visibility = Visibility.Visible;
            box_homeForward1.Visibility = Visibility.Visible;
            box_homeForward2.Visibility = Visibility.Visible;
            /*box midfield*/
            box_homeMidfield0.Visibility = Visibility.Visible;
            box_homeMidfield1.Visibility = Visibility.Visible;
            box_homeMidfield2.Visibility = Visibility.Visible;
            box_homeMidfield3.Visibility = Visibility.Visible;
            box_homeMidfield4.Visibility = Visibility.Visible;
            /*box defender*/
            box_homeDefender0.Visibility = Visibility.Visible;
            box_homeDefender1.Visibility = Visibility.Visible;
            box_homeDefender2.Visibility = Visibility.Visible;
            box_homeDefender3.Visibility = Visibility.Visible;
            box_homeDefender4.Visibility = Visibility.Visible;
        }
        

        /* Loading methods */
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
