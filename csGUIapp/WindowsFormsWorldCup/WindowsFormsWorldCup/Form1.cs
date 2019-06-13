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
        public Form1()
        {
            InitializeComponent();
            Data test = new Data();
            fullteamLB.DataSource = test.GetCountryNames();
        }

        private void fullteamLB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
