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

        public FSettings()
        {
            InitializeComponent();
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                culture.NumberFormat.NumberDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;

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
                if (Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator) == ',')
                {
                    radioButton2.Checked = true;
                }
                else
                {
                    radioButton1.Checked = true;

                }
            }
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                culture.NumberFormat.NumberDecimalSeparator = ".";
            }
            else
            {
                culture.NumberFormat.NumberDecimalSeparator = ",";
            }
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            Form1.decimalseparator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
        }
    }
}
