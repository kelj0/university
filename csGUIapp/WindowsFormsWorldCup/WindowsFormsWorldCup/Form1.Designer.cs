namespace WindowsFormsWorldCup
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cb_teamChooser = new System.Windows.Forms.ComboBox();
            this.btn_teamApply = new System.Windows.Forms.Button();
            this.lbl_chooseTeam = new System.Windows.Forms.Label();
            this.img_loading = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pnl_players = new System.Windows.Forms.Panel();
            this.lbl_substitutes = new System.Windows.Forms.Label();
            this.lbl_firstEleven = new System.Windows.Forms.Label();
            this.dgv_substitues = new System.Windows.Forms.DataGridView();
            this.dgv_StartingElevenYellowCardsGoals = new System.Windows.Forms.DataGridView();
            this.btn_settings = new System.Windows.Forms.Button();
            this._Pic = new System.Windows.Forms.DataGridViewImageColumn();
            this._Cpt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Fav = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._playerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Cards = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Goals = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pic = new System.Windows.Forms.DataGridViewImageColumn();
            this.Cpt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fav = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.playerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cards = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Goals = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.img_loading)).BeginInit();
            this.pnl_players.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_substitues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_StartingElevenYellowCardsGoals)).BeginInit();
            this.SuspendLayout();
            // 
            // cb_teamChooser
            // 
            this.cb_teamChooser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_teamChooser.FormattingEnabled = true;
            this.cb_teamChooser.Location = new System.Drawing.Point(380, 178);
            this.cb_teamChooser.Name = "cb_teamChooser";
            this.cb_teamChooser.Size = new System.Drawing.Size(157, 21);
            this.cb_teamChooser.TabIndex = 0;
            // 
            // btn_teamApply
            // 
            this.btn_teamApply.Location = new System.Drawing.Point(571, 176);
            this.btn_teamApply.Name = "btn_teamApply";
            this.btn_teamApply.Size = new System.Drawing.Size(75, 23);
            this.btn_teamApply.TabIndex = 1;
            this.btn_teamApply.Text = "button1";
            this.btn_teamApply.UseVisualStyleBackColor = true;
            this.btn_teamApply.Click += new System.EventHandler(this.btn_teamApply_Click);
            // 
            // lbl_chooseTeam
            // 
            this.lbl_chooseTeam.AutoSize = true;
            this.lbl_chooseTeam.Location = new System.Drawing.Point(410, 140);
            this.lbl_chooseTeam.Name = "lbl_chooseTeam";
            this.lbl_chooseTeam.Size = new System.Drawing.Size(107, 13);
            this.lbl_chooseTeam.TabIndex = 2;
            this.lbl_chooseTeam.Text = "Choose favorite team";
            // 
            // img_loading
            // 
            this.img_loading.Image = ((System.Drawing.Image)(resources.GetObject("img_loading.Image")));
            this.img_loading.InitialImage = ((System.Drawing.Image)(resources.GetObject("img_loading.InitialImage")));
            this.img_loading.Location = new System.Drawing.Point(251, 140);
            this.img_loading.Name = "img_loading";
            this.img_loading.Size = new System.Drawing.Size(464, 256);
            this.img_loading.TabIndex = 5;
            this.img_loading.TabStop = false;
            this.img_loading.Visible = false;
            this.img_loading.WaitOnLoad = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(398, 250);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 67);
            this.button1.TabIndex = 6;
            this.button1.Text = "Odaberite Jezik/Choose your language";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pnl_players
            // 
            this.pnl_players.Controls.Add(this.lbl_substitutes);
            this.pnl_players.Controls.Add(this.lbl_firstEleven);
            this.pnl_players.Controls.Add(this.dgv_substitues);
            this.pnl_players.Controls.Add(this.dgv_StartingElevenYellowCardsGoals);
            this.pnl_players.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_players.Location = new System.Drawing.Point(0, 0);
            this.pnl_players.Name = "pnl_players";
            this.pnl_players.Size = new System.Drawing.Size(355, 637);
            this.pnl_players.TabIndex = 7;
            this.pnl_players.Visible = false;
            // 
            // lbl_substitutes
            // 
            this.lbl_substitutes.AutoSize = true;
            this.lbl_substitutes.Location = new System.Drawing.Point(119, 304);
            this.lbl_substitutes.Name = "lbl_substitutes";
            this.lbl_substitutes.Size = new System.Drawing.Size(59, 13);
            this.lbl_substitutes.TabIndex = 7;
            this.lbl_substitutes.Text = "Substitutes";
            // 
            // lbl_firstEleven
            // 
            this.lbl_firstEleven.AutoSize = true;
            this.lbl_firstEleven.Location = new System.Drawing.Point(116, 8);
            this.lbl_firstEleven.Name = "lbl_firstEleven";
            this.lbl_firstEleven.Size = new System.Drawing.Size(62, 13);
            this.lbl_firstEleven.TabIndex = 6;
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
            this.dgv_substitues.Location = new System.Drawing.Point(0, 320);
            this.dgv_substitues.Name = "dgv_substitues";
            this.dgv_substitues.ReadOnly = true;
            this.dgv_substitues.Size = new System.Drawing.Size(352, 284);
            this.dgv_substitues.TabIndex = 5;
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
            this.dgv_StartingElevenYellowCardsGoals.Location = new System.Drawing.Point(0, 24);
            this.dgv_StartingElevenYellowCardsGoals.Name = "dgv_StartingElevenYellowCardsGoals";
            this.dgv_StartingElevenYellowCardsGoals.ReadOnly = true;
            this.dgv_StartingElevenYellowCardsGoals.Size = new System.Drawing.Size(352, 263);
            this.dgv_StartingElevenYellowCardsGoals.TabIndex = 4;
            // 
            // btn_settings
            // 
            this.btn_settings.Location = new System.Drawing.Point(820, 24);
            this.btn_settings.Name = "btn_settings";
            this.btn_settings.Size = new System.Drawing.Size(75, 23);
            this.btn_settings.TabIndex = 8;
            this.btn_settings.Text = "button2";
            this.btn_settings.UseVisualStyleBackColor = true;
            this.btn_settings.Visible = false;
            this.btn_settings.Click += new System.EventHandler(this.btn_settings_Click);
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
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(907, 637);
            this.Controls.Add(this.btn_settings);
            this.Controls.Add(this.pnl_players);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.img_loading);
            this.Controls.Add(this.btn_teamApply);
            this.Controls.Add(this.cb_teamChooser);
            this.Controls.Add(this.lbl_chooseTeam);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.img_loading)).EndInit();
            this.pnl_players.ResumeLayout(false);
            this.pnl_players.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_substitues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_StartingElevenYellowCardsGoals)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.ComboBox cb_teamChooser;
        public System.Windows.Forms.Button btn_teamApply;
        public System.Windows.Forms.Label lbl_chooseTeam;
        public System.Windows.Forms.PictureBox img_loading;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel pnl_players;
        public System.Windows.Forms.DataGridView dgv_StartingElevenYellowCardsGoals;
        private System.Windows.Forms.Label lbl_substitutes;
        private System.Windows.Forms.Label lbl_firstEleven;
        public System.Windows.Forms.DataGridView dgv_substitues;
        public System.Windows.Forms.Button btn_settings;
        private System.Windows.Forms.DataGridViewImageColumn _Pic;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Cpt;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Fav;
        private System.Windows.Forms.DataGridViewTextBoxColumn _playerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Cards;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Goals;
        private System.Windows.Forms.DataGridViewImageColumn Pic;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cpt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fav;
        private System.Windows.Forms.DataGridViewTextBoxColumn playerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cards;
        private System.Windows.Forms.DataGridViewTextBoxColumn Goals;
    }
}

