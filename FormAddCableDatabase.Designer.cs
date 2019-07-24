namespace Test1
{
    partial class FormAddCableDatabase
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
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelInsertData = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.labelInsulationType = new System.Windows.Forms.Label();
            this.comboBoxInsulation = new System.Windows.Forms.ComboBox();
            this.labelCores = new System.Windows.Forms.Label();
            this.comboBoxCores = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxConductor = new System.Windows.Forms.ComboBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelStandard = new System.Windows.Forms.Label();
            this.radioButtonIEC = new System.Windows.Forms.RadioButton();
            this.radioButtonNEC = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(15, 52);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(584, 20);
            this.textBoxName.TabIndex = 3;
            this.textBoxName.TextChanged += new System.EventHandler(this.TextBoxName_TextChanged);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 31);
            this.labelName.Margin = new System.Windows.Forms.Padding(3);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(35, 13);
            this.labelName.TabIndex = 4;
            this.labelName.Text = "Name";
            // 
            // labelInsertData
            // 
            this.labelInsertData.AutoSize = true;
            this.labelInsertData.Location = new System.Drawing.Point(12, 82);
            this.labelInsertData.Margin = new System.Windows.Forms.Padding(3);
            this.labelInsertData.Name = "labelInsertData";
            this.labelInsertData.Size = new System.Drawing.Size(89, 13);
            this.labelInsertData.TabIndex = 5;
            this.labelInsertData.Text = "Insert Cable Data";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 162);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(587, 284);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellEndEdit);
            this.dataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellLeave);
            this.dataGridView1.CurrentCellChanged += new System.EventHandler(this.DataGridView1_CurrentCellChanged);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DataGridView1_EditingControlShowing);
            this.dataGridView1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DataGridView1_KeyPress);
            // 
            // labelInsulationType
            // 
            this.labelInsulationType.AutoSize = true;
            this.labelInsulationType.Location = new System.Drawing.Point(22, 110);
            this.labelInsulationType.Margin = new System.Windows.Forms.Padding(3);
            this.labelInsulationType.Name = "labelInsulationType";
            this.labelInsulationType.Size = new System.Drawing.Size(79, 13);
            this.labelInsulationType.TabIndex = 7;
            this.labelInsulationType.Text = "Insulation Type";
            // 
            // comboBoxInsulation
            // 
            this.comboBoxInsulation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInsulation.FormattingEnabled = true;
            this.comboBoxInsulation.Items.AddRange(new object[] {
            "XLPE",
            "PVC"});
            this.comboBoxInsulation.Location = new System.Drawing.Point(107, 106);
            this.comboBoxInsulation.Name = "comboBoxInsulation";
            this.comboBoxInsulation.Size = new System.Drawing.Size(98, 21);
            this.comboBoxInsulation.TabIndex = 8;
            this.comboBoxInsulation.SelectedIndexChanged += new System.EventHandler(this.ComboBoxInsulation_SelectedIndexChanged);
            // 
            // labelCores
            // 
            this.labelCores.AutoSize = true;
            this.labelCores.Location = new System.Drawing.Point(217, 110);
            this.labelCores.Margin = new System.Windows.Forms.Padding(3);
            this.labelCores.Name = "labelCores";
            this.labelCores.Size = new System.Drawing.Size(66, 13);
            this.labelCores.TabIndex = 9;
            this.labelCores.Text = "No. of Cores";
            // 
            // comboBoxCores
            // 
            this.comboBoxCores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCores.FormattingEnabled = true;
            this.comboBoxCores.Items.AddRange(new object[] {
            "2",
            "3",
            "4"});
            this.comboBoxCores.Location = new System.Drawing.Point(289, 106);
            this.comboBoxCores.Name = "comboBoxCores";
            this.comboBoxCores.Size = new System.Drawing.Size(62, 21);
            this.comboBoxCores.TabIndex = 10;
            this.comboBoxCores.SelectedIndexChanged += new System.EventHandler(this.ComboBoxCores_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 139);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Conductor Type";
            // 
            // comboBoxConductor
            // 
            this.comboBoxConductor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxConductor.FormattingEnabled = true;
            this.comboBoxConductor.Items.AddRange(new object[] {
            "Copper"});
            this.comboBoxConductor.Location = new System.Drawing.Point(107, 135);
            this.comboBoxConductor.Name = "comboBoxConductor";
            this.comboBoxConductor.Size = new System.Drawing.Size(98, 21);
            this.comboBoxConductor.TabIndex = 12;
            this.comboBoxConductor.SelectedIndexChanged += new System.EventHandler(this.ComboBoxConductor_SelectedIndexChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonSave.Enabled = false;
            this.buttonSave.Location = new System.Drawing.Point(222, 452);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 13;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCancel.Location = new System.Drawing.Point(314, 452);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 14;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelStandard
            // 
            this.labelStandard.AutoSize = true;
            this.labelStandard.Location = new System.Drawing.Point(217, 139);
            this.labelStandard.Margin = new System.Windows.Forms.Padding(3);
            this.labelStandard.Name = "labelStandard";
            this.labelStandard.Size = new System.Drawing.Size(50, 13);
            this.labelStandard.TabIndex = 15;
            this.labelStandard.Text = "Standard";
            // 
            // radioButtonIEC
            // 
            this.radioButtonIEC.AutoSize = true;
            this.radioButtonIEC.Checked = true;
            this.radioButtonIEC.Location = new System.Drawing.Point(289, 137);
            this.radioButtonIEC.Name = "radioButtonIEC";
            this.radioButtonIEC.Size = new System.Drawing.Size(42, 17);
            this.radioButtonIEC.TabIndex = 16;
            this.radioButtonIEC.TabStop = true;
            this.radioButtonIEC.Text = "IEC";
            this.radioButtonIEC.UseVisualStyleBackColor = true;
            this.radioButtonIEC.CheckedChanged += new System.EventHandler(this.RadioButtonIEC_CheckedChanged);
            // 
            // radioButtonNEC
            // 
            this.radioButtonNEC.AutoSize = true;
            this.radioButtonNEC.Location = new System.Drawing.Point(341, 137);
            this.radioButtonNEC.Name = "radioButtonNEC";
            this.radioButtonNEC.Size = new System.Drawing.Size(47, 17);
            this.radioButtonNEC.TabIndex = 17;
            this.radioButtonNEC.TabStop = true;
            this.radioButtonNEC.Text = "NEC";
            this.radioButtonNEC.UseVisualStyleBackColor = true;
            // 
            // FormAddCableDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 482);
            this.Controls.Add(this.radioButtonNEC);
            this.Controls.Add(this.radioButtonIEC);
            this.Controls.Add(this.labelStandard);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.comboBoxConductor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxCores);
            this.Controls.Add(this.labelCores);
            this.Controls.Add(this.comboBoxInsulation);
            this.Controls.Add(this.labelInsulationType);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.labelInsertData);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.textBoxName);
            this.Name = "FormAddCableDatabase";
            this.Text = "Add Cable Database";
            this.Load += new System.EventHandler(this.FormAddCableDatabase_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelInsertData;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label labelInsulationType;
        private System.Windows.Forms.ComboBox comboBoxInsulation;
        private System.Windows.Forms.Label labelCores;
        private System.Windows.Forms.ComboBox comboBoxCores;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxConductor;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelStandard;
        private System.Windows.Forms.RadioButton radioButtonIEC;
        private System.Windows.Forms.RadioButton radioButtonNEC;
    }
}