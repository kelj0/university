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
        public Team team;
        public string lng;
        ChooseLanguage form2;
        ChooseFavoritePlayers favoritePlayersForm;
        RangLists rangLists;
        public string q;
        public string a;
        private Rectangle dragBoxFromMouseDown;
        private int rowIndexFromMouseDown;
        private int rowIndexOfItemUnderMouseToDrop;

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
            rangLists = new RangLists(this);

            try
            {
                List<string> config = Data.ReadConfigFile(); 
                //0 lng
                //1 name
                //2 fav players(split with ,)
                List<string> favPlayers = config[2].Split(',').ToList();
                if (config[0]=="CRO"){changeLanguageToCRO();}
                else{changeLanguageToENG();}
                form2.first = false;
                fifa_id = await Data.GetCountryCode(config[1]);
                team = new Team(config[1], fifa_id);
                await Task.Run(()=>team.SetUp());
                foreach (var p in favPlayers)
                {
                    if (p == "") { continue; }  
                    team.players[p].favorite = true;
                }
                await Task.Run(()=>prepareMain());
                showMain();
            }
            catch(Exception e)
            {
                if(e is FileNotFoundException)
                {
                    form2.first = true;
                    await Task.Run(()=>form2.BringToFront());
                    form2.Show();
                }
            }
            
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

        public bool confirmationBox()
            => MessageBox.Show(q, a, MessageBoxButtons.YesNo) == DialogResult.Yes ? true : false;

        public async void showMain()
        {
            showLoading();
            pnl_favoritePlayers.Show();
            pnl_notFavoritePlayers.Show();
            btn_rangLists.Show();
            btn_settings.Show();
            hideLoading();
        }

        public async void prepareMain()
        {
            Image i = Image.FromFile(@"..\..\..\static\tux.png");

            foreach (var p in team.players)
            {
                if (p.Value.favorite)
                {
                    dgv_favPlayers.Rows.Add(
                        new object[] {
                        i,
                        p.Value.name,
                        p.Value.shirt_number,
                        p.Value.position,
                        p.Value.captain?"C":""
                        });
                }
                else
                {
                    dgv_notFavPlayers.Rows.Add(
                    new object[] {
                        i,
                        p.Value.name,
                        p.Value.shirt_number,
                        p.Value.position,
                        p.Value.captain?"C":""
                    });
                }
            }
        }


        public void showLoading() { img_loading.Show(); }
        public void hideLoading() { img_loading.Hide(); }


        public void changeLanguageToENG()
        {
            lng = "ENG";
            lbl_chooseTeam.Text = "Choose favorite team";
            btn_teamApply.Text = "Apply";
            btn_settings.Text = "Settings";
            rangLists.lbl_firstEleven.Text = "First eleven";
            rangLists.lbl_substitutes.Text = "Substitues";
            rangLists.dgv_substitues.Columns[1].HeaderText = "Cpt";
            rangLists.dgv_substitues.Columns[2].HeaderText = "Favorite";
            rangLists.dgv_substitues.Columns[3].HeaderText = "Name";
            rangLists.dgv_substitues.Columns[4].HeaderText = "Cards";
            rangLists.dgv_substitues.Columns[5].HeaderText = "Goals";
            rangLists.dgv_StartingElevenYellowCardsGoals.Columns[1].HeaderText = "Cpt";
            rangLists.dgv_StartingElevenYellowCardsGoals.Columns[2].HeaderText = "Favorite";
            rangLists.dgv_StartingElevenYellowCardsGoals.Columns[3].HeaderText = "Name";
            rangLists.dgv_StartingElevenYellowCardsGoals.Columns[4].HeaderText = "Cards";
            rangLists.dgv_StartingElevenYellowCardsGoals.Columns[5].HeaderText = "Goals";
            favoritePlayersForm.lbl_chooseFavoritePlayers.Text = "Choose favorite players";
            favoritePlayersForm.btn_chooseFavoritePlayers.Text = "Choose";
            btn_rangLists.Text = "Rang lists";
            q = "Are you sure?";
            a = "Confirm Cancel";
            dgv_favPlayers.Columns[1].HeaderText = "Name";
            dgv_favPlayers.Columns[2].HeaderText = "Number";
            dgv_favPlayers.Columns[3].HeaderText = "Position";
            dgv_favPlayers.Columns[4].HeaderText = "Cpt";
            dgv_notFavPlayers.Columns[1].HeaderText = "Name";
            dgv_notFavPlayers.Columns[2].HeaderText = "Number";
            dgv_notFavPlayers.Columns[3].HeaderText = "Position";
            dgv_notFavPlayers.Columns[4].HeaderText = "Cpt";
            lbl_favorites.Text = "Favorite players";
            rangLists.dgv_matches.Columns[0].HeaderText = "Location";
            rangLists.dgv_matches.Columns[1].HeaderText = "Visitors";
            rangLists.dgv_matches.Columns[2].HeaderText = "Home team";
            rangLists.dgv_matches.Columns[3].HeaderText = "Away team";
        }

        public void changeLanguageToCRO()
        {
            lng = "CRO";
            lbl_chooseTeam.Text = "Odaberite omiljeni tim";
            btn_teamApply.Text = "Potvrdi";
            btn_settings.Text = "Postavke";
            rangLists.lbl_firstEleven.Text = "Prvih jedanaest";
            rangLists.lbl_substitutes.Text = "Zamjene";
            rangLists.dgv_substitues.Columns[1].HeaderText = "Kap";
            rangLists.dgv_substitues.Columns[2].HeaderText = "Omiljeni";
            rangLists.dgv_substitues.Columns[3].HeaderText = "Ime";
            rangLists.dgv_substitues.Columns[4].HeaderText = "Kartoni";
            rangLists.dgv_substitues.Columns[5].HeaderText = "Golovi";
            rangLists.dgv_StartingElevenYellowCardsGoals.Columns[1].HeaderText = "Kap";
            rangLists.dgv_StartingElevenYellowCardsGoals.Columns[2].HeaderText = "Omiljeni";
            rangLists.dgv_StartingElevenYellowCardsGoals.Columns[3].HeaderText = "Ime";
            rangLists.dgv_StartingElevenYellowCardsGoals.Columns[4].HeaderText = "Kartoni";
            rangLists.dgv_StartingElevenYellowCardsGoals.Columns[5].HeaderText = "Golovi";
            favoritePlayersForm.lbl_chooseFavoritePlayers.Text = "Odaberite svoje omiljene igrace";
            favoritePlayersForm.btn_chooseFavoritePlayers.Text = "Odaberi";
            btn_rangLists.Text = "Rang liste";
            q = "Jeste li sigurni?";
            a = "Potvrdi Otkazi";
            dgv_favPlayers.Columns[1].HeaderText = "Ime";
            dgv_favPlayers.Columns[2].HeaderText = "Broj";
            dgv_favPlayers.Columns[3].HeaderText = "Pozicija";
            dgv_favPlayers.Columns[4].HeaderText = "Kap";
            dgv_notFavPlayers.Columns[1].HeaderText = "Ime";
            dgv_notFavPlayers.Columns[2].HeaderText = "Broj";
            dgv_notFavPlayers.Columns[3].HeaderText = "Pozicija";
            dgv_notFavPlayers.Columns[4].HeaderText = "Kap";
            lbl_favorites.Text = "Omiljeni igraci";
            rangLists.dgv_matches.Columns[0].HeaderText = "Lokacija";
            rangLists.dgv_matches.Columns[1].HeaderText = "Posjetitelji";
            rangLists.dgv_matches.Columns[2].HeaderText = "Domacin";
            rangLists.dgv_matches.Columns[3].HeaderText = "Gost";
        }


        public async void btn_teamApply_Click(object sender, EventArgs e)
        {
            string fifa_code = await Task.Run(() => Data.GetCountryCode(cb_teamChooser.Text));
            team = new Team(cb_teamChooser.Text, fifa_code);
            lbl_teamName.Text = cb_teamChooser.Text;
            lbl_teamName.Show();
            await Task.Run(()=>hideTeamChooser());
            await Task.Run(()=>showLoading());

            await Task.Run(()=>team.SetUp());

            favoritePlayersForm.populateCheckBox(team.players);
            favoritePlayersForm.Show();
            favoritePlayersForm.BringToFront();
        
        }
        
        private void btn_settings_Click(object sender, EventArgs e)
        {
            form2.Show();
        }

        private void btn_rangLists_Click(object sender, EventArgs e)
        {
            rangLists.prepareRankedPlayersAndMatches();
            rangLists.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Data.SaveDataToFile(team, lng);
        }

        /* Drag n drop favorite players events */
        private void dgv_favPlayers_MouseDown(object sender, MouseEventArgs e)
        {
            rowIndexFromMouseDown = dgv_favPlayers.HitTest(e.X, e.Y).RowIndex;
            if (rowIndexFromMouseDown != -1)
            {
                Size dragSize = SystemInformation.DragSize;
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                                                               e.Y - (dragSize.Height / 2)),
                                                               dragSize);
            }
            else
            {
                dragBoxFromMouseDown = Rectangle.Empty;
            }
            
        }

        private void dgv_favPlayers_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if (dragBoxFromMouseDown != Rectangle.Empty &&
                    !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    DragDropEffects dropEffect = dgv_favPlayers.DoDragDrop(
                    dgv_favPlayers.Rows[rowIndexFromMouseDown],
                    DragDropEffects.Move);
                }
            }
        }

        private void dgv_favPlayers_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dgv_favPlayers_DragDrop(object sender, DragEventArgs e)
        {
            Point clientPoint = dgv_notFavPlayers.PointToClient(new Point(e.X, e.Y));

            rowIndexOfItemUnderMouseToDrop =
                dgv_notFavPlayers.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            if (e.Effect == DragDropEffects.Move)
            {
                DataGridViewRow rowToMove = e.Data.GetData(
                    typeof(DataGridViewRow)) as DataGridViewRow;
                dgv_notFavPlayers.Rows.RemoveAt(rowIndexFromMouseDown);
                dgv_favPlayers.Rows.Add(rowToMove);
                team.players[(string)rowToMove.Cells[1].Value].favorite = true;
            }

        }

        private void dgv_notFavPlayers_MouseDown(object sender, MouseEventArgs e)
        {
            rowIndexFromMouseDown = dgv_notFavPlayers.HitTest(e.X, e.Y).RowIndex;
            if (rowIndexFromMouseDown != -1)
            {
                Size dragSize = SystemInformation.DragSize;
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                                                               e.Y - (dragSize.Height / 2)),
                                                               dragSize);
            }
            else
            {
                dragBoxFromMouseDown = Rectangle.Empty;
            }

        }

        private void dgv_notFavPlayers_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if (dragBoxFromMouseDown != Rectangle.Empty &&
                    !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    DragDropEffects dropEffect = dgv_notFavPlayers.DoDragDrop(
                    dgv_notFavPlayers.Rows[rowIndexFromMouseDown],
                    DragDropEffects.Move);
                }
            }
        }

        private void dgv_notFavPlayers_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dgv_notFavPlayers_DragDrop(object sender, DragEventArgs e)
        {
            Point clientPoint = dgv_favPlayers.PointToClient(new Point(e.X, e.Y));

            rowIndexOfItemUnderMouseToDrop =
                dgv_favPlayers.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            if (e.Effect == DragDropEffects.Move)
            {
                DataGridViewRow rowToMove = e.Data.GetData(
                    typeof(DataGridViewRow)) as DataGridViewRow;
                dgv_favPlayers.Rows.RemoveAt(rowIndexFromMouseDown);
                dgv_notFavPlayers.Rows.Add(rowToMove);
                team.players[(string)rowToMove.Cells[1].Value].favorite = false;
            }
        }
    }
}
