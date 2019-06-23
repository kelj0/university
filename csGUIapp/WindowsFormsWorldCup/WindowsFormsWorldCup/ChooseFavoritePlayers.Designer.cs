namespace WindowsFormsWorldCup
{
    partial class ChooseFavoritePlayers
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
            this.lbl_chooseFavoritePlayers = new System.Windows.Forms.Label();
            this.clb_allPlayersToChoose = new System.Windows.Forms.CheckedListBox();
            this.btn_chooseFavoritePlayers = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_chooseFavoritePlayers
            // 
            this.lbl_chooseFavoritePlayers.AutoSize = true;
            this.lbl_chooseFavoritePlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_chooseFavoritePlayers.Location = new System.Drawing.Point(93, 9);
            this.lbl_chooseFavoritePlayers.Name = "lbl_chooseFavoritePlayers";
            this.lbl_chooseFavoritePlayers.Size = new System.Drawing.Size(70, 25);
            this.lbl_chooseFavoritePlayers.TabIndex = 0;
            this.lbl_chooseFavoritePlayers.Text = "label1";
            // 
            // clb_allPlayersToChoose
            // 
            this.clb_allPlayersToChoose.FormattingEnabled = true;
            this.clb_allPlayersToChoose.Location = new System.Drawing.Point(12, 53);
            this.clb_allPlayersToChoose.Name = "clb_allPlayersToChoose";
            this.clb_allPlayersToChoose.Size = new System.Drawing.Size(236, 409);
            this.clb_allPlayersToChoose.TabIndex = 1;
            this.clb_allPlayersToChoose.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clb_allPlayersToChoose_ItemCheck);
            // 
            // btn_chooseFavoritePlayers
            // 
            this.btn_chooseFavoritePlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_chooseFavoritePlayers.Location = new System.Drawing.Point(290, 197);
            this.btn_chooseFavoritePlayers.Name = "btn_chooseFavoritePlayers";
            this.btn_chooseFavoritePlayers.Size = new System.Drawing.Size(137, 56);
            this.btn_chooseFavoritePlayers.TabIndex = 2;
            this.btn_chooseFavoritePlayers.Text = "button1";
            this.btn_chooseFavoritePlayers.UseVisualStyleBackColor = true;
            this.btn_chooseFavoritePlayers.Click += new System.EventHandler(this.bnt_chooseFavoritePlayers_Click);
            // 
            // ChooseFavoritePlayers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 474);
            this.Controls.Add(this.btn_chooseFavoritePlayers);
            this.Controls.Add(this.clb_allPlayersToChoose);
            this.Controls.Add(this.lbl_chooseFavoritePlayers);
            this.Name = "ChooseFavoritePlayers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChooseFavoritePlayers";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lbl_chooseFavoritePlayers;
        private System.Windows.Forms.CheckedListBox clb_allPlayersToChoose;
        public System.Windows.Forms.Button btn_chooseFavoritePlayers;
    }
}