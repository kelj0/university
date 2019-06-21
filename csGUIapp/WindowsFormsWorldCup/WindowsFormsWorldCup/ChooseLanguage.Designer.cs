namespace WindowsFormsWorldCup
{
    partial class ChooseLanguage
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
            this.btn_english = new System.Windows.Forms.Button();
            this.btn_croatian = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_english
            // 
            this.btn_english.Location = new System.Drawing.Point(12, 33);
            this.btn_english.Name = "btn_english";
            this.btn_english.Size = new System.Drawing.Size(75, 23);
            this.btn_english.TabIndex = 11;
            this.btn_english.Text = "ENG";
            this.btn_english.UseVisualStyleBackColor = true;
            this.btn_english.Click += new System.EventHandler(this.btn_english_Click);
            // 
            // btn_croatian
            // 
            this.btn_croatian.Location = new System.Drawing.Point(128, 33);
            this.btn_croatian.Name = "btn_croatian";
            this.btn_croatian.Size = new System.Drawing.Size(75, 23);
            this.btn_croatian.TabIndex = 10;
            this.btn_croatian.Text = "CRO";
            this.btn_croatian.UseVisualStyleBackColor = true;
            this.btn_croatian.Click += new System.EventHandler(this.btn_croatian_Click_1);
            // 
            // ChooseLanguage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(215, 105);
            this.Controls.Add(this.btn_english);
            this.Controls.Add(this.btn_croatian);
            this.Name = "ChooseLanguage";
            this.Text = "ChooseLanguage";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_english;
        private System.Windows.Forms.Button btn_croatian;
    }
}