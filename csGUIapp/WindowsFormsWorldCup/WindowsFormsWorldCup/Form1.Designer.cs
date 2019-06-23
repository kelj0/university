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
            this.btn_settings = new System.Windows.Forms.Button();
            this.btn_rangLists = new System.Windows.Forms.Button();
            this.pnl_favoritePlayers = new System.Windows.Forms.Panel();
            this.lbl_favorites = new System.Windows.Forms.Label();
            this.dgv_favPlayers = new System.Windows.Forms.DataGridView();
            this.favPlayersPic = new System.Windows.Forms.DataGridViewImageColumn();
            this.favPlayersName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.favPlayersNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.favPLayersPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.favPlayersCpt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnl_notFavoritePlayers = new System.Windows.Forms.Panel();
            this.dgv_notFavPlayers = new System.Windows.Forms.DataGridView();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.notFavPlayersPic = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.notFavPlayersName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.notFavPlayersNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.notFavPLayersPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbl_teamName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.img_loading)).BeginInit();
            this.pnl_favoritePlayers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_favPlayers)).BeginInit();
            this.pnl_notFavoritePlayers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_notFavPlayers)).BeginInit();
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
            this.img_loading.Location = new System.Drawing.Point(254, 140);
            this.img_loading.Name = "img_loading";
            this.img_loading.Size = new System.Drawing.Size(464, 256);
            this.img_loading.TabIndex = 5;
            this.img_loading.TabStop = false;
            this.img_loading.Visible = false;
            this.img_loading.WaitOnLoad = true;
            // 
            // btn_settings
            // 
            this.btn_settings.Location = new System.Drawing.Point(820, 25);
            this.btn_settings.Name = "btn_settings";
            this.btn_settings.Size = new System.Drawing.Size(75, 23);
            this.btn_settings.TabIndex = 8;
            this.btn_settings.Text = "Settings";
            this.btn_settings.UseVisualStyleBackColor = true;
            this.btn_settings.Visible = false;
            this.btn_settings.Click += new System.EventHandler(this.btn_settings_Click);
            // 
            // btn_rangLists
            // 
            this.btn_rangLists.Location = new System.Drawing.Point(820, 66);
            this.btn_rangLists.Name = "btn_rangLists";
            this.btn_rangLists.Size = new System.Drawing.Size(75, 23);
            this.btn_rangLists.TabIndex = 9;
            this.btn_rangLists.Text = "Rang lists";
            this.btn_rangLists.UseVisualStyleBackColor = true;
            this.btn_rangLists.Visible = false;
            this.btn_rangLists.Click += new System.EventHandler(this.btn_rangLists_Click);
            // 
            // pnl_favoritePlayers
            // 
            this.pnl_favoritePlayers.AllowDrop = true;
            this.pnl_favoritePlayers.Controls.Add(this.lbl_favorites);
            this.pnl_favoritePlayers.Controls.Add(this.dgv_favPlayers);
            this.pnl_favoritePlayers.Location = new System.Drawing.Point(56, 25);
            this.pnl_favoritePlayers.Name = "pnl_favoritePlayers";
            this.pnl_favoritePlayers.Size = new System.Drawing.Size(284, 527);
            this.pnl_favoritePlayers.TabIndex = 10;
            this.pnl_favoritePlayers.Visible = false;
            // 
            // lbl_favorites
            // 
            this.lbl_favorites.AutoSize = true;
            this.lbl_favorites.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_favorites.Location = new System.Drawing.Point(89, 14);
            this.lbl_favorites.Name = "lbl_favorites";
            this.lbl_favorites.Size = new System.Drawing.Size(60, 24);
            this.lbl_favorites.TabIndex = 1;
            this.lbl_favorites.Text = "label1";
            // 
            // dgv_favPlayers
            // 
            this.dgv_favPlayers.AllowDrop = true;
            this.dgv_favPlayers.AllowUserToAddRows = false;
            this.dgv_favPlayers.AllowUserToDeleteRows = false;
            this.dgv_favPlayers.AllowUserToResizeRows = false;
            this.dgv_favPlayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_favPlayers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.favPlayersPic,
            this.favPlayersName,
            this.favPlayersNumber,
            this.favPLayersPosition,
            this.favPlayersCpt});
            this.dgv_favPlayers.Location = new System.Drawing.Point(0, 55);
            this.dgv_favPlayers.Name = "dgv_favPlayers";
            this.dgv_favPlayers.ReadOnly = true;
            this.dgv_favPlayers.RowHeadersVisible = false;
            this.dgv_favPlayers.Size = new System.Drawing.Size(282, 472);
            this.dgv_favPlayers.TabIndex = 0;
            this.dgv_favPlayers.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgv_favPlayers_DragDrop);
            this.dgv_favPlayers.DragEnter += new System.Windows.Forms.DragEventHandler(this.dgv_favPlayers_DragEnter);
            this.dgv_favPlayers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgv_favPlayers_MouseDown);
            this.dgv_favPlayers.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgv_favPlayers_MouseMove);
            // 
            // favPlayersPic
            // 
            this.favPlayersPic.HeaderText = "";
            this.favPlayersPic.Name = "favPlayersPic";
            this.favPlayersPic.ReadOnly = true;
            this.favPlayersPic.Width = 25;
            // 
            // favPlayersName
            // 
            this.favPlayersName.HeaderText = "Name";
            this.favPlayersName.Name = "favPlayersName";
            this.favPlayersName.ReadOnly = true;
            this.favPlayersName.Width = 120;
            // 
            // favPlayersNumber
            // 
            this.favPlayersNumber.HeaderText = "Number";
            this.favPlayersNumber.Name = "favPlayersNumber";
            this.favPlayersNumber.ReadOnly = true;
            this.favPlayersNumber.Width = 50;
            // 
            // favPLayersPosition
            // 
            this.favPLayersPosition.HeaderText = "Position";
            this.favPLayersPosition.Name = "favPLayersPosition";
            this.favPLayersPosition.ReadOnly = true;
            this.favPLayersPosition.Width = 55;
            // 
            // favPlayersCpt
            // 
            this.favPlayersCpt.HeaderText = "Cpt";
            this.favPlayersCpt.Name = "favPlayersCpt";
            this.favPlayersCpt.ReadOnly = true;
            this.favPlayersCpt.Width = 28;
            // 
            // pnl_notFavoritePlayers
            // 
            this.pnl_notFavoritePlayers.AllowDrop = true;
            this.pnl_notFavoritePlayers.Controls.Add(this.dgv_notFavPlayers);
            this.pnl_notFavoritePlayers.Location = new System.Drawing.Point(462, 25);
            this.pnl_notFavoritePlayers.Name = "pnl_notFavoritePlayers";
            this.pnl_notFavoritePlayers.Size = new System.Drawing.Size(289, 527);
            this.pnl_notFavoritePlayers.TabIndex = 11;
            this.pnl_notFavoritePlayers.Visible = false;
            // 
            // dgv_notFavPlayers
            // 
            this.dgv_notFavPlayers.AllowDrop = true;
            this.dgv_notFavPlayers.AllowUserToAddRows = false;
            this.dgv_notFavPlayers.AllowUserToDeleteRows = false;
            this.dgv_notFavPlayers.AllowUserToResizeRows = false;
            this.dgv_notFavPlayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_notFavPlayers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewImageColumn1,
            this.notFavPlayersPic,
            this.notFavPlayersName,
            this.notFavPlayersNumber,
            this.notFavPLayersPosition});
            this.dgv_notFavPlayers.Location = new System.Drawing.Point(0, 55);
            this.dgv_notFavPlayers.Name = "dgv_notFavPlayers";
            this.dgv_notFavPlayers.ReadOnly = true;
            this.dgv_notFavPlayers.RowHeadersVisible = false;
            this.dgv_notFavPlayers.Size = new System.Drawing.Size(288, 472);
            this.dgv_notFavPlayers.TabIndex = 1;
            this.dgv_notFavPlayers.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgv_notFavPlayers_DragDrop);
            this.dgv_notFavPlayers.DragEnter += new System.Windows.Forms.DragEventHandler(this.dgv_notFavPlayers_DragEnter);
            this.dgv_notFavPlayers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgv_notFavPlayers_MouseDown);
            this.dgv_notFavPlayers.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgv_notFavPlayers_MouseMove);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.Width = 25;
            // 
            // notFavPlayersPic
            // 
            this.notFavPlayersPic.HeaderText = "Name";
            this.notFavPlayersPic.Name = "notFavPlayersPic";
            this.notFavPlayersPic.ReadOnly = true;
            this.notFavPlayersPic.Width = 120;
            // 
            // notFavPlayersName
            // 
            this.notFavPlayersName.HeaderText = "Number";
            this.notFavPlayersName.Name = "notFavPlayersName";
            this.notFavPlayersName.ReadOnly = true;
            this.notFavPlayersName.Width = 50;
            // 
            // notFavPlayersNumber
            // 
            this.notFavPlayersNumber.HeaderText = "Position";
            this.notFavPlayersNumber.Name = "notFavPlayersNumber";
            this.notFavPlayersNumber.ReadOnly = true;
            this.notFavPlayersNumber.Width = 55;
            // 
            // notFavPLayersPosition
            // 
            this.notFavPLayersPosition.HeaderText = "Cpt";
            this.notFavPLayersPosition.Name = "notFavPLayersPosition";
            this.notFavPLayersPosition.ReadOnly = true;
            this.notFavPLayersPosition.Width = 28;
            // 
            // lbl_teamName
            // 
            this.lbl_teamName.AutoSize = true;
            this.lbl_teamName.Location = new System.Drawing.Point(362, 9);
            this.lbl_teamName.Name = "lbl_teamName";
            this.lbl_teamName.Size = new System.Drawing.Size(35, 13);
            this.lbl_teamName.TabIndex = 12;
            this.lbl_teamName.Text = "label1";
            this.lbl_teamName.Visible = false;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.ClientSize = new System.Drawing.Size(907, 637);
            this.Controls.Add(this.lbl_teamName);
            this.Controls.Add(this.pnl_notFavoritePlayers);
            this.Controls.Add(this.pnl_favoritePlayers);
            this.Controls.Add(this.btn_rangLists);
            this.Controls.Add(this.btn_settings);
            this.Controls.Add(this.img_loading);
            this.Controls.Add(this.btn_teamApply);
            this.Controls.Add(this.cb_teamChooser);
            this.Controls.Add(this.lbl_chooseTeam);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.img_loading)).EndInit();
            this.pnl_favoritePlayers.ResumeLayout(false);
            this.pnl_favoritePlayers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_favPlayers)).EndInit();
            this.pnl_notFavoritePlayers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_notFavPlayers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.ComboBox cb_teamChooser;
        public System.Windows.Forms.Button btn_teamApply;
        public System.Windows.Forms.Label lbl_chooseTeam;
        public System.Windows.Forms.PictureBox img_loading;
        public System.Windows.Forms.Button btn_settings;
        private System.Windows.Forms.Button btn_rangLists;
        private System.Windows.Forms.Panel pnl_favoritePlayers;
        private System.Windows.Forms.Panel pnl_notFavoritePlayers;
        private System.Windows.Forms.DataGridView dgv_favPlayers;
        private System.Windows.Forms.DataGridView dgv_notFavPlayers;
        private System.Windows.Forms.DataGridViewImageColumn favPlayersPic;
        private System.Windows.Forms.DataGridViewTextBoxColumn favPlayersName;
        private System.Windows.Forms.DataGridViewTextBoxColumn favPlayersNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn favPLayersPosition;
        private System.Windows.Forms.DataGridViewTextBoxColumn favPlayersCpt;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn notFavPlayersPic;
        private System.Windows.Forms.DataGridViewTextBoxColumn notFavPlayersName;
        private System.Windows.Forms.DataGridViewTextBoxColumn notFavPlayersNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn notFavPLayersPosition;
        private System.Windows.Forms.Label lbl_favorites;
        private System.Windows.Forms.Label lbl_teamName;
    }
}

