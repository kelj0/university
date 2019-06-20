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

        public string favoriteTeam;
        public string favoriteTeamcode;
        public List<List<string>> firstEleven = new List<List<string>>();
        public string fifa_id;
        public dynamic matches;


        public Form1()
        {
            InitializeComponent();
            Task.Run(()=>SetTeams());
            //Task.Run(()=>TestFunctions());
            
        }

        private async void TestFunctions()
        {
            //Console.WriteLine($"Testing...\nNumber of yellow cards: {await Task.Run(() => Data.GetPlayerYellowCardNumber("Eiji KAWASHIMA", "JPN",mat))}");
            List<string> players = await Task.Run(()=> Data.GetMatchFirstEleven("300331550","JPN"));
            Console.WriteLine("Starting eleven from japan: ");
            players.ForEach(Console.WriteLine);
            Console.WriteLine("---------------------------");
        }

        private async void SetTeams()
        {
            showLoading();
            List<string> countries = await Task.Run(() => Data.GetCountryNames());
            hideLoading();
            label1.Show();
            cb_teamChooser.Show();
            btn_teamApply.Show();
            cb_teamChooser.DataSource = countries;
            cb_teamChooser.SelectedItem = countries[0];
        }


        private void showSettings()
        {
            //TODO
        }

        private void hideSettings()
        {
            //TODO
        }

        private async void showMain()
        {
            showLoading();

            dynamic fifa_id = await Data.GetCountryMatches(favoriteTeamcode);
            List<string> p = await Data.GetMatchFirstEleven((string)fifa_id[0].fifa_id, favoriteTeamcode);
            Image i = Image.FromFile(@"..\..\..\static\tux.png");

            foreach (var item in firstEleven)
            {
                dgv_StartingElevenYellowCardsGoals.Rows.Add(new object[] {i,item[0],item[1]});
            }            
            dgv_StartingElevenYellowCardsGoals.Show();

            hideLoading();

        }
        private async void hideMain()
        {
            dgv_StartingElevenYellowCardsGoals.Hide();

        }

        private async void btn_teamApply_Click(object sender, EventArgs e)
        {
            favoriteTeam = cb_teamChooser.Text;
            cb_teamChooser.Hide();
            btn_teamApply.Hide();
            label1.Hide();

            showLoading();

            favoriteTeamcode = await Task.Run(() => Data.GetCountryCode(favoriteTeam));
            fifa_id = (string)(await Data.GetCountryMatches(favoriteTeamcode))[0].fifa_id;

            
            setFirstEleven();
            showMain();

            hideLoading();
        }

        private async void setFirstEleven()
        {
            List<string> players = await Data.GetMatchFirstEleven(fifa_id, favoriteTeamcode);
            dynamic match = (await Data.GetCountryMatches(favoriteTeamcode))[0];
            int s = 0;
            foreach (var player in players)
            {
                int c = 0;
                if (match.home_team.code == favoriteTeamcode)
                {
                    foreach (var e in match.home_team_events)
                    {
                        if (e.type_of_event == "yellow-card" && e.player == player)
                            c++;
                    }
                }
                else
                {
                    foreach (var e in match.away_team_events)
                    {
                        if (e.type_of_event == "yellow-card" && e.player == player)
                            c++;
                    }
                }
                firstEleven.Add(new List<string>());
                firstEleven[s].Add(player);
                firstEleven[s++].Add(c.ToString());

            }    
        }

        private async void showLoading() {await Task.Run(()=>img_loading.Show()); }
        private async void hideLoading() {await Task.Run(() => img_loading.Hide()); }
    }
}
