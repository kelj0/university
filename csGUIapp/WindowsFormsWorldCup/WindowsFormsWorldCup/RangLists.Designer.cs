namespace WindowsFormsWorldCup
{
    partial class RangLists
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_substitutes = new System.Windows.Forms.Label();
            this.lbl_firstEleven = new System.Windows.Forms.Label();
            this.dgv_substitues = new System.Windows.Forms.DataGridView();
            this._Pic = new System.Windows.Forms.DataGridViewImageColumn();
            this._Cpt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Fav = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._playerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Cards = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Goals = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_StartingElevenYellowCardsGoals = new System.Windows.Forms.DataGridView();
            this.Pic = new System.Windows.Forms.DataGridViewImageColumn();
            this.Cpt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fav = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.playerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cards = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Goals = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_matches = new System.Windows.Forms.DataGridView();
            this.Location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Visitors = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Home_team = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Away_team = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_substitues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_StartingElevenYellowCardsGoals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_matches)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_substitutes
            // 
            this.lbl_substitutes.AutoSize = true;
            this.lbl_substitutes.Location = new System.Drawing.Point(118, 291);
            this.lbl_substitutes.Name = "lbl_substitutes";
            this.lbl_substitutes.Size = new System.Drawing.Size(59, 13);
            this.lbl_substitutes.TabIndex = 11;
            this.lbl_substitutes.Text = "Substitutes";
            // 
            // lbl_firstEleven
            // 
            this.lbl_firstEleven.AutoSize = true;
            this.lbl_firstEleven.Location = new System.Drawing.Point(115, 9);
            this.lbl_firstEleven.Name = "lbl_firstEleven";
            this.lbl_firstEleven.Size = new System.Drawing.Size(62, 13);
            this.lbl_firstEleven.TabIndex = 10;
            this.lbl_firstEleven.Text = "First Eleven";
            // 
            // dgv_substitues
            // 
            this.dgv_substitues.AllowUserToAddRows = false;
            this.dgv_substitues.AllowUserToDeleteRows = false;
            this.dgv_substitues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_substitues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._Pic,
            this._Cpt,
            this._Fav,
            this._playerName,
            this._Cards,
            this._Goals});
            this.dgv_substitues.Location = new System.Drawing.Point(-3, 307);
            this.dgv_substitues.Name = "dgv_substitues";
            this.dgv_substitues.ReadOnly = true;
            this.dgv_substitues.RowHeadersVisible = false;
            this.dgv_substitues.Size = new System.Drawing.Size(311, 284);
            this.dgv_substitues.TabIndex = 9;
            // 
            // _Pic
            // 
            this._Pic.HeaderText = "";
            this._Pic.Name = "_Pic";
            this._Pic.ReadOnly = true;
            this._Pic.Width = 25;
            // 
            // _Cpt
            // 
            this._Cpt.HeaderText = "Cpt";
            this._Cpt.Name = "_Cpt";
            this._Cpt.ReadOnly = true;
            this._Cpt.Width = 28;
            // 
            // _Fav
            // 
            this._Fav.HeaderText = "Favorite";
            this._Fav.Name = "_Fav";
            this._Fav.ReadOnly = true;
            this._Fav.Width = 50;
            // 
            // _playerName
            // 
            this._playerName.HeaderText = "Name";
            this._playerName.Name = "_playerName";
            this._playerName.ReadOnly = true;
            this._playerName.Width = 120;
            // 
            // _Cards
            // 
            this._Cards.HeaderText = "Cards";
            this._Cards.Name = "_Cards";
            this._Cards.ReadOnly = true;
            this._Cards.Width = 45;
            // 
            // _Goals
            // 
            this._Goals.HeaderText = "Goals";
            this._Goals.Name = "_Goals";
            this._Goals.ReadOnly = true;
            this._Goals.Width = 40;
            // 
            // dgv_StartingElevenYellowCardsGoals
            // 
            this.dgv_StartingElevenYellowCardsGoals.AllowUserToAddRows = false;
            this.dgv_StartingElevenYellowCardsGoals.AllowUserToDeleteRows = false;
            this.dgv_StartingElevenYellowCardsGoals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_StartingElevenYellowCardsGoals.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Pic,
            this.Cpt,
            this.Fav,
            this.playerName,
            this.Cards,
            this.Goals});
            this.dgv_StartingElevenYellowCardsGoals.Location = new System.Drawing.Point(-3, 25);
            this.dgv_StartingElevenYellowCardsGoals.Name = "dgv_StartingElevenYellowCardsGoals";
            this.dgv_StartingElevenYellowCardsGoals.ReadOnly = true;
            this.dgv_StartingElevenYellowCardsGoals.RowHeadersVisible = false;
            this.dgv_StartingElevenYellowCardsGoals.Size = new System.Drawing.Size(311, 263);
            this.dgv_StartingElevenYellowCardsGoals.TabIndex = 8;
            // 
            // Pic
            // 
            this.Pic.HeaderText = "";
            this.Pic.Name = "Pic";
            this.Pic.ReadOnly = true;
            this.Pic.Width = 25;
            // 
            // Cpt
            // 
            this.Cpt.HeaderText = "Cpt";
            this.Cpt.Name = "Cpt";
            this.Cpt.ReadOnly = true;
            this.Cpt.Width = 28;
            // 
            // Fav
            // 
            this.Fav.HeaderText = "Favorite";
            this.Fav.Name = "Fav";
            this.Fav.ReadOnly = true;
            this.Fav.Width = 50;
            // 
            // playerName
            // 
            this.playerName.HeaderText = "Name";
            this.playerName.Name = "playerName";
            this.playerName.ReadOnly = true;
            this.playerName.Width = 120;
            // 
            // Cards
            // 
            this.Cards.HeaderText = "Cards";
            this.Cards.Name = "Cards";
            this.Cards.ReadOnly = true;
            this.Cards.Width = 45;
            // 
            // Goals
            // 
            this.Goals.HeaderText = "Goals";
            this.Goals.Name = "Goals";
            this.Goals.ReadOnly = true;
            this.Goals.Width = 40;
            // 
            // dgv_matches
            // 
            this.dgv_matches.AllowUserToAddRows = false;
            this.dgv_matches.AllowUserToDeleteRows = false;
            this.dgv_matches.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_matches.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Location,
            this.Visitors,
            this.Home_team,
            this.Away_team});
            this.dgv_matches.Location = new System.Drawing.Point(380, 69);
            this.dgv_matches.Name = "dgv_matches";
            this.dgv_matches.ReadOnly = true;
            this.dgv_matches.RowHeadersVisible = false;
            this.dgv_matches.Size = new System.Drawing.Size(503, 357);
            this.dgv_matches.TabIndex = 12;
            // 
            // Location
            // 
            this.Location.HeaderText = "Location";
            this.Location.Name = "Location";
            this.Location.ReadOnly = true;
            this.Location.Width = 140;
            // 
            // Visitors
            // 
            this.Visitors.HeaderText = "Visitors";
            this.Visitors.Name = "Visitors";
            this.Visitors.ReadOnly = true;
            this.Visitors.Width = 120;
            // 
            // Home_team
            // 
            this.Home_team.HeaderText = "Home team";
            this.Home_team.Name = "Home_team";
            this.Home_team.ReadOnly = true;
            this.Home_team.Width = 120;
            // 
            // Away_team
            // 
            this.Away_team.HeaderText = "Away team";
            this.Away_team.Name = "Away_team";
            this.Away_team.ReadOnly = true;
            this.Away_team.Width = 120;
            // 
            // RangLists
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 591);
            this.Controls.Add(this.dgv_matches);
            this.Controls.Add(this.lbl_substitutes);
            this.Controls.Add(this.lbl_firstEleven);
            this.Controls.Add(this.dgv_substitues);
            this.Controls.Add(this.dgv_StartingElevenYellowCardsGoals);
            this.Name = "RangLists";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RangLists";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RangLists_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_substitues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_StartingElevenYellowCardsGoals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_matches)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lbl_substitutes;
        public System.Windows.Forms.Label lbl_firstEleven;
        public System.Windows.Forms.DataGridView dgv_substitues;
        private System.Windows.Forms.DataGridViewImageColumn _Pic;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Cpt;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Fav;
        private System.Windows.Forms.DataGridViewTextBoxColumn _playerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Cards;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Goals;
        public System.Windows.Forms.DataGridView dgv_StartingElevenYellowCardsGoals;
        private System.Windows.Forms.DataGridViewImageColumn Pic;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cpt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fav;
        private System.Windows.Forms.DataGridViewTextBoxColumn playerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cards;
        private System.Windows.Forms.DataGridViewTextBoxColumn Goals;
        public System.Windows.Forms.DataGridView dgv_matches;
        private System.Windows.Forms.DataGridViewTextBoxColumn Location;
        private System.Windows.Forms.DataGridViewTextBoxColumn Visitors;
        private System.Windows.Forms.DataGridViewTextBoxColumn Home_team;
        private System.Windows.Forms.DataGridViewTextBoxColumn Away_team;
    }
}