using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsWorldCup;
using DataLayer;
using System.IO;
using System.Net;
using System.Threading;

namespace WindowsFormsWorldCup
{
    public partial class Form1 : Form
    {
        public string fifa_id;
        public dynamic matches;
        public Team team;
        public string lng;
        ChooseLanguage form2;
        ChooseFavoritePlayers favoritePlayersForm;
        public Form1()
        {
            InitializeComponent();
            run();
            //Task.Run(()=>TestFunctions());
        }

        public async void run()
        {
            hideTeamChooser();
            form2 = new ChooseLanguage(this);
            favoritePlayersForm = new ChooseFavoritePlayers(this);
        }

        public async void TestFunctions()
        {
            //Console.WriteLine($"Testing...\nNumber of yellow cards: {await Task.Run(() => Data.GetPlayerYellowCardNumber("Eiji KAWASHIMA", "JPN",mat))}");
            List<string> players = await Task.Run(()=> Data.GetMatchFirstEleven("300331550","JPN"));
            Console.WriteLine("Starting eleven from japan: ");
            players.ForEach(Console.WriteLine);
            Console.WriteLine("---------------------------");
        }

        public async void SetTeams()
        {
            
            button1.Hide();
            showLoading();
            List<string> countries = await Task.Run(() => Data.GetCountryNames());

            showTeamChooser();
            hideLoading();

            cb_teamChooser.DataSource = countries;
            cb_teamChooser.SelectedItem = countries[0];
        }

        public async void showTeamChooser()
        {
            lbl_chooseTeam.Show();
            cb_teamChooser.Show();
            btn_teamApply.Show();
            btn_settings.Show();
        }

        public async void hideTeamChooser()
        {
            lbl_chooseTeam.Hide();
            cb_teamChooser.Hide();
            btn_teamApply.Hide();
        }

        public void showSettings()
        {
            //TODO
        }

        public void hideSettings()
        {
            //TODO
        }

        public async void showMain()
        {
            showLoading();

            Image i = Image.FromFile(@"..\..\..\static\tux.png");

            foreach (var p in team.firsteleven)
            {
                dgv_StartingElevenYellowCardsGoals.Rows.Add(
                    new object[] {
                        i,
                        (team.players[p].captain?"C":""),
                        (team.players[p].favorite?"★":""),
                        team.players[p].name,
                        team.players[p].cards,
                        team.players[p].goals,
                    });
            }
            foreach (var p in team.substitutions)
            {
                dgv_substitues.Rows.Add(
                    new object[] {
                        i,
                        (team.players[p].captain?"C":""),
                        (team.players[p].favorite?"★":""),
                        team.players[p].name,
                        team.players[p].cards,
                        team.players[p].goals,
                    });
            }
            pnl_players.Show();

            hideLoading();
        }

        public async void hideMain()
        {
            dgv_StartingElevenYellowCardsGoals.Hide();

        }

        public void showLoading() { img_loading.Show(); }
        public void hideLoading() { img_loading.Hide(); }


        public void changeLanguageToENG()
        {
            lng = "ENG";
            lbl_chooseTeam.Text = "Choose favorite team";
            btn_teamApply.Text = "Apply";
            btn_settings.Text = "Settings";
            lbl_firstEleven.Text = "First eleven";
            lbl_substitutes.Text = "Substitues";
            dgv_substitues.Columns[1].HeaderText = "Cpt";
            dgv_substitues.Columns[2].HeaderText = "Favorite";
            dgv_substitues.Columns[3].HeaderText = "Name";
            dgv_substitues.Columns[4].HeaderText = "Cards";
            dgv_substitues.Columns[5].HeaderText = "Goals";
            dgv_StartingElevenYellowCardsGoals.Columns[1].HeaderText = "Cpt";
            dgv_StartingElevenYellowCardsGoals.Columns[2].HeaderText = "Favorite";
            dgv_StartingElevenYellowCardsGoals.Columns[3].HeaderText = "Name";
            dgv_StartingElevenYellowCardsGoals.Columns[4].HeaderText = "Cards";
            dgv_StartingElevenYellowCardsGoals.Columns[5].HeaderText = "Goals";
            favoritePlayersForm.lbl_chooseFavoritePlayers.Text = "Choose favorite players";
            favoritePlayersForm.btn_chooseFavoritePlayers.Text = "Choose";
        }

        public void changeLanguageToCRO()
        {
            lng = "CRO";
            lbl_chooseTeam.Text = "Odaberite omiljeni tim";
            btn_teamApply.Text = "Potvrdi";
            btn_settings.Text = "Postavke";
            lbl_firstEleven.Text = "Prvih jedanaest";
            lbl_substitutes.Text = "Zamjene";
            dgv_substitues.Columns[1].HeaderText = "Kap";
            dgv_substitues.Columns[2].HeaderText = "Omiljeni";
            dgv_substitues.Columns[3].HeaderText = "Ime";
            dgv_substitues.Columns[4].HeaderText = "Kartoni";
            dgv_substitues.Columns[5].HeaderText = "Golovi";
            dgv_StartingElevenYellowCardsGoals.Columns[1].HeaderText = "Kap";
            dgv_StartingElevenYellowCardsGoals.Columns[2].HeaderText = "Omiljeni";
            dgv_StartingElevenYellowCardsGoals.Columns[3].HeaderText = "Ime";
            dgv_StartingElevenYellowCardsGoals.Columns[4].HeaderText = "Kartoni";
            dgv_StartingElevenYellowCardsGoals.Columns[5].HeaderText = "Golovi";
            favoritePlayersForm.lbl_chooseFavoritePlayers.Text = "Odaberite svoje omiljene igrace";
            favoritePlayersForm.btn_chooseFavoritePlayers.Text = "Odaberi";
        }


        public async void btn_teamApply_Click(object sender, EventArgs e)
        {
            string fifa_code = await Task.Run(() => Data.GetCountryCode(cb_teamChooser.Text));
            team = new Team(cb_teamChooser.Text, fifa_code);

            await Task.Run(()=>hideTeamChooser());
            await Task.Run(()=>showLoading());


            await Task.Run(()=>team.SetUp());

            favoritePlayersForm.populateCheckBox(team.players);
            favoritePlayersForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            form2.Show();
        }

        private void btn_settings_Click(object sender, EventArgs e)
        {
            form2.Show();
        }
    }
}
