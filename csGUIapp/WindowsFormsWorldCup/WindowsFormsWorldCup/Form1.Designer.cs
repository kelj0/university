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
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_StartingElevenYellowCardsGoals = new System.Windows.Forms.DataGridView();
            this.Fav = new System.Windows.Forms.DataGridViewImageColumn();
            this.Players = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YellowCards = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.img_loading = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_StartingElevenYellowCardsGoals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img_loading)).BeginInit();
            this.SuspendLayout();
            // 
            // cb_teamChooser
            // 
            this.cb_teamChooser.FormattingEnabled = true;
            this.cb_teamChooser.Location = new System.Drawing.Point(280, 116);
            this.cb_teamChooser.Name = "cb_teamChooser";
            this.cb_teamChooser.Size = new System.Drawing.Size(157, 21);
            this.cb_teamChooser.TabIndex = 0;
            // 
            // btn_teamApply
            // 
            this.btn_teamApply.Location = new System.Drawing.Point(477, 116);
            this.btn_teamApply.Name = "btn_teamApply";
            this.btn_teamApply.Size = new System.Drawing.Size(75, 23);
            this.btn_teamApply.TabIndex = 1;
            this.btn_teamApply.Text = "button1";
            this.btn_teamApply.UseVisualStyleBackColor = true;
            this.btn_teamApply.Click += new System.EventHandler(this.btn_teamApply_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(305, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Choose favorite team";
            // 
            // dgv_StartingElevenYellowCardsGoals
            // 
            this.dgv_StartingElevenYellowCardsGoals.AllowUserToAddRows = false;
            this.dgv_StartingElevenYellowCardsGoals.AllowUserToDeleteRows = false;
            this.dgv_StartingElevenYellowCardsGoals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_StartingElevenYellowCardsGoals.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Fav,
            this.Players,
            this.YellowCards});
            this.dgv_StartingElevenYellowCardsGoals.Location = new System.Drawing.Point(221, 159);
            this.dgv_StartingElevenYellowCardsGoals.Name = "dgv_StartingElevenYellowCardsGoals";
            this.dgv_StartingElevenYellowCardsGoals.ReadOnly = true;
            this.dgv_StartingElevenYellowCardsGoals.Size = new System.Drawing.Size(379, 318);
            this.dgv_StartingElevenYellowCardsGoals.TabIndex = 4;
            this.dgv_StartingElevenYellowCardsGoals.Visible = false;
            // 
            // Fav
            // 
            this.Fav.HeaderText = "";
            this.Fav.Name = "Fav";
            this.Fav.ReadOnly = true;
            // 
            // Players
            // 
            this.Players.HeaderText = "Players";
            this.Players.Name = "Players";
            this.Players.ReadOnly = true;
            // 
            // YellowCards
            // 
            this.YellowCards.HeaderText = "YellowCards";
            this.YellowCards.Name = "YellowCards";
            this.YellowCards.ReadOnly = true;
            // 
            // img_loading
            // 
            this.img_loading.Image = ((System.Drawing.Image)(resources.GetObject("img_loading.Image")));
            this.img_loading.InitialImage = ((System.Drawing.Image)(resources.GetObject("img_loading.InitialImage")));
            this.img_loading.Location = new System.Drawing.Point(167, 60);
            this.img_loading.Name = "img_loading";
            this.img_loading.Size = new System.Drawing.Size(464, 256);
            this.img_loading.TabIndex = 5;
            this.img_loading.TabStop = false;
            this.img_loading.WaitOnLoad = true;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(777, 532);
            this.Controls.Add(this.img_loading);
            this.Controls.Add(this.dgv_StartingElevenYellowCardsGoals);
            this.Controls.Add(this.btn_teamApply);
            this.Controls.Add(this.cb_teamChooser);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_StartingElevenYellowCardsGoals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img_loading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox cb_teamChooser;
        private System.Windows.Forms.Button btn_teamApply;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv_StartingElevenYellowCardsGoals;
        private System.Windows.Forms.DataGridViewImageColumn Fav;
        private System.Windows.Forms.DataGridViewTextBoxColumn Players;
        private System.Windows.Forms.DataGridViewTextBoxColumn YellowCards;
        private System.Windows.Forms.PictureBox img_loading;
    }
}

