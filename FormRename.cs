using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test1
{
    public partial class FormRename : Form
    {
        internal static bool saveClicked;
        internal static string NewName;

        public FormRename()
        {
            InitializeComponent();
        }

        private void FormRename_Load(object sender, EventArgs e)
        {
            label2.Text = "Rename \"" + FormAddCableDatabase.fileName + "\".";
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            saveClicked = false;
            NewName = textBox1.Text;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ButtonOK_Click_1(object sender, EventArgs e)
        {
            if (FormAddCableDatabase.CheckFileNameValidity(NewName))
            {
                var systemPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                Directory.CreateDirectory(systemPath + "/Cable Sizing/IEC_database");
                var saveDir = Path.Combine(systemPath + "/Cable Sizing/IEC_database", NewName + ".xml");
                if (File.Exists(saveDir))
                {
                    DialogResult dr = MessageBox.Show("File with the same name already exist, want to overwrite the file?", "Cable Sizing",
                                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    if (dr == DialogResult.OK)
                    {
                        saveClicked = true;
                        Close();
                    }
                    else
                    {
                        saveClicked = false;
                    }
                }
                else
                {
                    saveClicked = true;
                    Close();
                }
            }
            else
            {
                MessageBox.Show(NewName + "\nInvalid file name", "Cable Sizing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
    }
}
