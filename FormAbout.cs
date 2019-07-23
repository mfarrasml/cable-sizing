using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

namespace Test1
{
    public partial class FormAbout : Form
    {
        string version;
        string description;
        string copyright;

        public FormAbout()
        {
            InitializeComponent();
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

            //write app version beside title
            version = fvi.FileVersion;
            labelTitle.Text += version;

            //write app description (NOTE: NEED MANUAL INPUT FOR DESCRIPTION)
            description = "IEC && NEC Cable Sizing Application";
            labelDescription.Text = description;

            //write app copyright
            copyright = fvi.LegalCopyright;
            labelCopyright.Text = copyright;
        }
    }
}
