namespace Test1
{
    partial class FormAbout
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelSpecialThanks = new System.Windows.Forms.Label();
            this.labelDeveloper = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel1.Controls.Add(this.labelSpecialThanks);
            this.panel1.Controls.Add(this.labelDeveloper);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.labelDescription);
            this.panel1.Controls.Add(this.labelCopyright);
            this.panel1.Controls.Add(this.labelTitle);
            this.panel1.Location = new System.Drawing.Point(37, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(299, 425);
            this.panel1.TabIndex = 0;
            // 
            // labelSpecialThanks
            // 
            this.labelSpecialThanks.AutoSize = true;
            this.labelSpecialThanks.Location = new System.Drawing.Point(89, 148);
            this.labelSpecialThanks.Margin = new System.Windows.Forms.Padding(3);
            this.labelSpecialThanks.Name = "labelSpecialThanks";
            this.labelSpecialThanks.Size = new System.Drawing.Size(96, 13);
            this.labelSpecialThanks.TabIndex = 5;
            this.labelSpecialThanks.Text = "Special Thanks to:";
            // 
            // labelDeveloper
            // 
            this.labelDeveloper.AutoSize = true;
            this.labelDeveloper.Location = new System.Drawing.Point(89, 87);
            this.labelDeveloper.Margin = new System.Windows.Forms.Padding(3);
            this.labelDeveloper.Name = "labelDeveloper";
            this.labelDeveloper.Size = new System.Drawing.Size(77, 52);
            this.labelDeveloper.TabIndex = 4;
            this.labelDeveloper.Text = "Developed By:\r\n- Rauf, \r\n- Farras,\r\n- Irpan. ";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Test1.Properties.Resources.power_vector_plug_cord_4;
            this.pictureBox1.Location = new System.Drawing.Point(22, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(61, 62);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(89, 49);
            this.labelDescription.Margin = new System.Windows.Forms.Padding(3);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(79, 13);
            this.labelDescription.TabIndex = 2;
            this.labelDescription.Text = "desciption here";
            // 
            // labelCopyright
            // 
            this.labelCopyright.AutoSize = true;
            this.labelCopyright.Location = new System.Drawing.Point(89, 68);
            this.labelCopyright.Margin = new System.Windows.Forms.Padding(3);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(74, 13);
            this.labelCopyright.TabIndex = 1;
            this.labelCopyright.Text = "copyright here";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(89, 30);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(3);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(108, 13);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Cable Sizing version: ";
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 450);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAbout";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "FormAbout";
            this.Load += new System.EventHandler(this.FormAbout_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelDeveloper;
        private System.Windows.Forms.Label labelSpecialThanks;
    }
}