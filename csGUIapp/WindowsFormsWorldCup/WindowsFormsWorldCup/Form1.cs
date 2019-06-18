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

namespace WindowsFormsWorldCup
{
    public partial class Form1 : Form
    {

        public string favoriteTeam;
        public string favoriteTeamcode;

        public Form1()
        {
            InitializeComponent();
            Task.Run(()=>SetTeams());
            Task.Run(()=>TestFunctions());
            
        }

        private async void TestFunctions()
        {
            Console.WriteLine("Number of yellow cards:" + await Task.Run(() => Data.GetPlayerYellowCardNumber("Eiji KAWASHIMA", "JPN")));
            Console.WriteLine("Starting eleven: ");
            List<string> players = await Task.Run(()=> Data.GetMatchFirstEleven("300331550","JPN"));
            players.ForEach(Console.WriteLine);
        }

        private async void SetTeams()
        {
            List<string> countries = await Task.Run(() => Data.GetCountryNames());
            cb_teamChooser.DataSource = countries;   
        }

        private void showSettings()
        {
            //TODO
        }

        private void hideSettings()
        {
            //TODO
        }

        private void showMain()
        {

        }
        private void hideMain()
        {

        }

        private async void btn_teamApply_Click(object sender, EventArgs e)
        {
            favoriteTeam = cb_teamChooser.Text;
            cb_teamChooser.Hide();
            btn_teamApply.Hide();
            label1.Hide();

            favoriteTeamcode = await Task.Run(() => Data.GetCountryCode(favoriteTeam));
            foreach (var item in await Data.GetCountryMatches(favoriteTeamcode))
            {
                Console.WriteLine(item.location);
            }
        }
    }
}
