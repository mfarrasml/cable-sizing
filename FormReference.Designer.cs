namespace Test1
{
    partial class FormReference
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReference));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageIEC = new System.Windows.Forms.TabPage();
            this.tabPageNEC = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.tabPageIEC.SuspendLayout();
            this.tabPageNEC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageIEC);
            this.tabControl1.Controls.Add(this.tabPageNEC);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(752, 634);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageIEC
            // 
            this.tabPageIEC.AutoScroll = true;
            this.tabPageIEC.Controls.Add(this.pictureBox1);
            this.tabPageIEC.Location = new System.Drawing.Point(4, 22);
            this.tabPageIEC.Name = "tabPageIEC";
            this.tabPageIEC.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageIEC.Size = new System.Drawing.Size(744, 608);
            this.tabPageIEC.TabIndex = 0;
            this.tabPageIEC.Text = "IEC Standard";
            this.tabPageIEC.UseVisualStyleBackColor = true;
            // 
            // tabPageNEC
            // 
            this.tabPageNEC.AutoScroll = true;
            this.tabPageNEC.Controls.Add(this.pictureBox2);
            this.tabPageNEC.Location = new System.Drawing.Point(4, 22);
            this.tabPageNEC.Name = "tabPageNEC";
            this.tabPageNEC.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNEC.Size = new System.Drawing.Size(744, 608);
            this.tabPageNEC.TabIndex = 1;
            this.tabPageNEC.Text = "NEC Standard";
            this.tabPageNEC.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Test1.Properties.Resources.IEC__1_;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(720, 5434);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Test1.Properties.Resources.NEC;
            this.pictureBox2.Location = new System.Drawing.Point(3, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(720, 5434);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // FormReference
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 634);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormReference";
            this.Text = "Information";
            this.tabControl1.ResumeLayout(false);
            this.tabPageIEC.ResumeLayout(false);
            this.tabPageNEC.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage tabPageIEC;
        private System.Windows.Forms.TabPage tabPageNEC;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        internal System.Windows.Forms.TabControl tabControl1;
    }
}