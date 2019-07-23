using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test1
{

    public partial class FSettings : Form
    {
        CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
        char currentDecimalSeparator;
        public static int tempDecimalSeparator;

        public FSettings()
        {
            InitializeComponent();
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton2.Checked = false;
                radioButton1.Checked = false;
            }
            else
            {
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                if (Convert.ToChar(CultureInfo.InstalledUICulture.NumberFormat.NumberDecimalSeparator) == '.')
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }
            }
        }

        private void FSettings_Load(object sender, EventArgs e)
        {
            currentDecimalSeparator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            if (tempDecimalSeparator == 0)
            {
                checkBox1.Checked = true;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton2.Checked = false;
                radioButton1.Checked = false;
            }
            else if (tempDecimalSeparator == 1)
            {
                checkBox1.Checked = false;
                radioButton1.Checked = true;
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
            }
            else if (tempDecimalSeparator == 2)
            {
                checkBox1.Checked = false;
                radioButton2.Checked = true;
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //update chosen decimalseparator settings
            if (checkBox1.Checked)
            {
                culture.NumberFormat.NumberDecimalSeparator = CultureInfo.InstalledUICulture.NumberFormat.NumberDecimalSeparator;
                Thread.CurrentThread.CurrentCulture = culture;
                tempDecimalSeparator = 0;
            }
            else if ((radioButton1.Checked) && (!checkBox1.Checked))
            {
                culture.NumberFormat.NumberDecimalSeparator = ".";
                Thread.CurrentThread.CurrentCulture = culture;
                tempDecimalSeparator = 1;
            }
            else if ((radioButton2.Checked) && (!checkBox1.Checked))
            {
                culture.NumberFormat.NumberDecimalSeparator = ",";
                Thread.CurrentThread.CurrentCulture = culture;
                tempDecimalSeparator = 2;
            }
            Properties.Settings.Default.DecimalSeparator = tempDecimalSeparator;

            // write current decimal separator to decimalseparator variable in Form1
            Form1.decimalseparator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);


            if (Form1.decimalseparator != currentDecimalSeparator)
            {
                Form1.decimalSeparatorChanged = true;
            }

            Form1.okSetClicked = true;
            Close();
        }


        private void FSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                cancelSave();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            cancelSave();
            Close();
        }

        private void cancelSave()
        {
            if (!Form1.okSetClicked)
            {
                if (tempDecimalSeparator == 0)
                {
                    checkBox1.Checked = true;
                }
                else if (tempDecimalSeparator == 1)
                {
                    checkBox1.Checked = false;
                    radioButton1.Checked = true;
                }
                else if (tempDecimalSeparator == 2)
                {
                    checkBox1.Checked = false;
                    radioButton2.Checked = true;
                }
            }
        }
    }
}
