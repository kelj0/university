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
            this.cb_teamChooser = new System.Windows.Forms.ComboBox();
            this.btn_teamApply = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
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
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(777, 532);
            this.Controls.Add(this.btn_teamApply);
            this.Controls.Add(this.cb_teamChooser);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox cb_teamChooser;
        private System.Windows.Forms.Button btn_teamApply;
        private System.Windows.Forms.Label label1;
    }
}

