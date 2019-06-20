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
        Team team;

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

            Image i = Image.FromFile(@"..\..\..\static\tux.png");

            foreach (var p in team.firsteleven)
            {
                dgv_StartingElevenYellowCardsGoals.Rows.Add(new object[] {i,team.players[p].name,team.players[p].cards});
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
            string fifa_code = await Task.Run(() => Data.GetCountryCode(cb_teamChooser.Text));
            team = new Team(cb_teamChooser.Text, fifa_code);
            await Task.Run(()=>showLoading());
            cb_teamChooser.Hide();
            btn_teamApply.Hide();
            label1.Hide();
            await Task.Run(()=>team.SetUp());

            showMain();
        }

        private void showLoading() {img_loading.Show(); }
        private void hideLoading() {img_loading.Hide(); }
    }
}
