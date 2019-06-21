using DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsWorldCup
{
    public partial class ChooseFavoritePlayers : Form
    {
        private Form1 f;
        public ChooseFavoritePlayers(Form1 f)
        {
            InitializeComponent();
            this.f = f;

        }

        public async void populateCheckBox(Dictionary<string,Player> players)
        {
            foreach (var p in players)
            {
                clb_allPlayersToChoose.Items.Add(p.Value.name);
            }
        }

        private async void bnt_chooseFavoritePlayers_Click(object sender, EventArgs e)
        {
            foreach(var p in clb_allPlayersToChoose.CheckedItems.Cast<object>().Select(x=>clb_allPlayersToChoose.GetItemText(x)))
            {
                f.team.players[p].favorite = true;
            }

            Hide();
            f.showMain();
        }
    }
}
