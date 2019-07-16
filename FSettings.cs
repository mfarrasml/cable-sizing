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
        int tempDecimalSeparator;

        public FSettings()
        {
            InitializeComponent();
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                culture.NumberFormat.NumberDecimalSeparator = CultureInfo.InstalledUICulture.NumberFormat.NumberDecimalSeparator;
                Thread.CurrentThread.CurrentCulture = culture;

                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton2.Checked = false;
                radioButton1.Checked = false;

                Form1.decimalseparator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            }
            else
            {
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                if (Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator) == '.')
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }
            }
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if ((radioButton1.Checked) && (!checkBox1.Checked))
            {
                culture.NumberFormat.NumberDecimalSeparator = ".";
                Thread.CurrentThread.CurrentCulture = culture;
                Form1.decimalseparator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            }
            else if ((radioButton2.Checked) && (!checkBox1.Checked))
            {
                culture.NumberFormat.NumberDecimalSeparator = ",";
                Thread.CurrentThread.CurrentCulture = culture;
                Form1.decimalseparator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            }
        }

        private void FSettings_Load(object sender, EventArgs e)
        {
            currentDecimalSeparator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            if (checkBox1.Checked)
            {
                tempDecimalSeparator = 0;
            }
            else if (radioButton1.Checked)
            {
                tempDecimalSeparator = 1;
            }
            else if (radioButton2.Checked)
            {
                tempDecimalSeparator = 2;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (Form1.decimalseparator != currentDecimalSeparator)
            {
                Form1.decimalSeparatorChanged = true;
            }
            Form1.okSetClicked = true;
            this.Close();
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
            this.Close();
        }

        private void cancelSave()
        {
            if (!Form1.okSetClicked)
            {
                culture.NumberFormat.NumberDecimalSeparator = currentDecimalSeparator.ToString();
                Form1.decimalseparator = currentDecimalSeparator;
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
