using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //apply settings
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

            Application.Run(new Form1());

            
        }
    }
}
