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
    

    public partial class OpenForm : GradientForm
    {
        //variable to indicate if application should return to title (this OpenForm) or not
        public static bool ReturnToTitle = false; 
        //variable to indicate if main application (Form1 or Form9) is closed
        public static bool formMainClose;

        public OpenForm()
        {
            InitializeComponent();
            button2.FlatAppearance.MouseOverBackColor = Color.FromArgb(178, 205, 247);
            button3.FlatAppearance.MouseOverBackColor = Color.FromArgb(178, 205, 247);
            button2.FlatAppearance.MouseDownBackColor = Color.FromArgb(153, 175, 209);
            button3.FlatAppearance.MouseDownBackColor = Color.FromArgb(153, 175, 209);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Hide();
            Form1 f1 = new Form1();
            f1.FormClosed += Form_Closed; 
            f1.Show();
        }

        private void Form_Closed(object sender, FormClosedEventArgs e)
        {
            if (ReturnToTitle)
            {
                Show();
                ReturnToTitle = false;
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Hide();
            Form9 f9 = new Form9();
            f9.FormClosed += Form_Closed;
            f9.Show();
        }

        private void OpenForm_Load(object sender, EventArgs e)
        {
            //Apply Settings

            //Apply Decimal separator setting
            CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();

            FSettings.tempDecimalSeparator = Properties.Settings.Default.DecimalSeparator;
            if (FSettings.tempDecimalSeparator == 0)
            {
                culture = CultureInfo.InstalledUICulture;
                Thread.CurrentThread.CurrentCulture = culture;
            }
            else if (FSettings.tempDecimalSeparator == 1)
            {
                culture.NumberFormat.NumberDecimalSeparator = ".";
                Thread.CurrentThread.CurrentCulture = culture;
            }
            else if (FSettings.tempDecimalSeparator == 2)
            {
                culture.NumberFormat.NumberDecimalSeparator = ",";
                Thread.CurrentThread.CurrentCulture = culture;
            }

            //Apply toolbar text activated setting
            Form5.toolbarTextActivated = Properties.Settings.Default.ToolbarTextProperties;
        }
    }
}
