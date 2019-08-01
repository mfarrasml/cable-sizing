using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Test1
{

    public partial class Form1 : GradientForm
    {

        //variables
        public static double current;
        public static double currentstart;
        public static double voltage;
        public static double power;
        public static double pf, eff;
        public static string phase;
        public static string loadtype;
        public static double hp;
        public static double pfstart;
        public static double multiplier;
        public static double sccurrent;
        public static double cplxpower;
        public static string materialname;
        public static double initialTemp, finalTemp;
        public static double diameter;

        public static double vdrunmax, vdstartmax, vdrun, vdstart;
        public static double length;


        public static int n = 1;
        public static string volSys, powerUnit;
        public static string tagno, from, to, fromdesc, todesc;
        public static string material, armour, innersheath, outersheath;
        public static string breakertype;
        public static string remarks;
        public static string ratedvoltage;
        public static string insulation, installation;
        public static int cores;
        public static double breakcurrent;

        public static double k1main, k2main, k3main, ktmain;
        public static bool ok_clicked, okSetClicked;

        bool complete, inputValid;
        int i = 0;

        string readtemp;
        public static double Rac;
        public static double X;
        public static double Rdc;
        public static double Irated;
        public static double wirearea;
        public static double iderated;

        public static double tbreaker;
        public static double bLTE;
        public static double cLTE;
        public static double k;
        double sc_wiremin;
        public static double lmax;
        double smin;
        bool calculated = false;
        public static double scrating;

        private bool ConsiderVdStart;

        int m;

        public static string voltageLv;

        public static int j = -1;
        Form5 f5 = new Form5();
        Form6 f6 = new Form6();
        Form2 f2 =  new Form2();
        Form3 f3  = new Form3();
        FormAbout fAbout = new FormAbout();
        FSettings fSettings = new FSettings();
        FormAddCableDatabase faddcable;

        public static string[] results = new string[39];

        public static char decimalseparator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
        public static bool decimalSeparatorChanged = false;

        public static double[,] inputCableData;
        public static int cableCount;

        public static DataTable dtdiameter;

        bool EditingState = false;
        int tempCurrentRow;

        int kttextboxX, kttextboxY, ktlabelX, ktlabelY;

        public static bool OpenFromMain;
        
        //dtr is dtdiameter DataRow used to handle current input/edited data that's gonna be saved to dtdiameter
        DataRow dtr;

        //Directory & database variables
        DirectoryInfo di;

        //number of custom database found
        int IECDatabaseFiles;
        //currently selected database file
        string SelectedDatabase;
        DataSet cableDS; //currently selected database data

        //currently selected database data based on insulation and cores
        DataTable dtXLPE2DB;
        DataTable dtXLPE3DB;
        DataTable dtXLPE4DB;
        DataTable dtPVC2DB;
        DataTable dtPVC3DB;
        DataTable dtPVC4DB;

        //
        double[,] xlpe2coreDB;
        double[,] xlpe3coreDB;
        double[,] xlpe4coreDB;
        double[,] pvc2coreDB;
        double[,] pvc3coreDB;
        double[,] pvc4coreDB;

        double xlpe2coreLength;
        double xlpe3coreLength;
        double xlpe4coreLength;
        double pvc2coreLength;
        double pvc3coreLength;
        double pvc4coreLength;

        double currentDataLength;

        public static bool FileOpened;

        //Datatable to save and reload data before reset
        DataTable dtReset;

        public Form1()
        {

            InitializeComponent();

        }

        public static double[,] xlpe2core = new double[17, 6]
        {
            { 1.5, 15.4300, 0.0999, 15.4287, 26, 26 },
            { 2.5, 9.4500, 0.0961, 9.4485, 34, 36 },
            { 4, 5.8800, 0.0899, 5.8782, 44, 49},
            { 6, 3.9300, 0.0853, 3.9273, 56, 63 },
            { 10, 2.3400, 0.0802, 2.3334, 73, 86 },
            { 16, 1.4700, 0.0781, 1.4664, 95, 115 },
            { 25, 0.9270, 0.0779, 0.9270, 121, 149},
            { 35, 0.6690, 0.0753, 0.6682, 146, 185 },
            { 50, 0.4940, 0.0748, 0.4935, 173, 225 },
            { 70, 0.3430, 0.0734, 0.3417, 213, 289 },
            { 95, 0.2470, 0.0711, 0.2461, 252, 352 },
            { 120, 0.1970, 0.0708, 0.1951, 287, 410 },
            { 150, 0.1600, 0.0712, 0.1581, 324, 473 },
            { 185, 0.1281, 0.0720, 0.1264, 363, 542 },
            { 240, 0.0984, 0.0711, 0.0961, 419, 641 },
            { 300, 0.0799, 0.0704, 0.0766, 474, 741 },
            { 400, 0.0633, 0.0701, 0.0599, 555, 892 }
        };

        public static double[,] xlpe3core = new double[17, 6]
        {
            {1.5, 15.4300, 0.0999, 15.4281, 22, 23 },
            {2.5,    9.4500,  0.0961,  9.4481,  29,  32 },
            {4,  5.8800,  0.0899,  5.8780,  37,  42 },
            {6,  3.9300,  0.0853,  3.9272,  46, 54 },
            {10, 2.3400,  0.0802,  2.3333,  61,  75 },
            {16, 1.4700,  0.0781,  1.4663,  79,  100 },
            {25, 0.9270,  0.0779,  0.9270,  101, 127 },
            {35, 0.6690,  0.0753,  0.6681,  122, 158 },
            {50, 0.4940,  0.0748,  0.4934,  144, 192 },
            {70, 0.3430,  0.0734,  0.3417,  178, 246 },
            {95, 0.2470,  0.0711,  0.2461,  211, 298 },
            {120, 0.1970,  0.0708,  0.1951,  240, 346 },
            {150, 0.1600,  0.0712,  0.1581,  271, 399 },
            {185, 0.1281,  0.0720,  0.1264,  304, 456 },
            {240, 0.0984,  0.0711,  0.0961,  351, 538 },
            {300, 0.0799,  0.0704,  0.0766,  396, 621 },
            {400, 0.0633,  0.0701,  0.0599,  464, 745 }
        };

        public static double[,] xlpe4core = new double[17, 6]
        {
            { 1.5, 15.4300, 0.1125,  15.4287, 22,  23 },
            { 2.5, 9.4500,  0.1086,  9.4485,  29,  32 },
            { 4,  5.8800,  0.1025,  5.8782,  37, 42 },
            { 6, 3.93, 0.098, 3.927308, 46, 54 },
            { 10, 2.34, 0.093, 2.333433, 61, 75 },
            { 16, 1.47, 0.091, 1.466365, 79, 100 },
            { 25, 0.927, 0.0908, 0.9269977, 101, 127 },
            { 35, 0.669, 0.0883, 0.6681524, 122, 158 },
            { 50, 0.494, 0.0877, 0.4934637, 144, 192 },
            { 70, 0.343, 0.0864, 0.3417268, 178, 246 },
            { 95, 0.247, 0.0842, 0.2460943, 211, 298 },
            { 120, 0.197, 0.0839, 0.1950903, 240, 346 },
            { 150, 0.16, 0.0843, 0.1581124, 271, 399 },
            { 185, 0.1281, 0.0851, 0.12636241, 304, 456 },
            { 240, 0.0984, 0.0842, 0.09614254, 351, 538 },
            { 300, 0.0799, 0.0835, 0.07663351, 396, 621 },
            { 400, 0.0633, 0.0832, 0.0599297, 464, 745 }
        };

        public static double[,] pvc2core = new double[16, 6]
        {
            { 1.5, 14.4777, 0.1075, 14.47765, 22, 22 },
            { 2.5, 8.8661, 0.0994, 8.866065, 29, 30 },
            { 4, 5.5159, 0.0983, 5.515865, 38, 40 },
            { 6, 3.6853, 0.0928, 3.68522, 47, 51 },
            { 10, 2.1897, 0.0865, 2.189595, 63, 70 },
            { 16, 1.3761, 0.0818, 1.375975, 81, 94 },
            { 25, 0.8701, 0.0807, 0.8698555, 104, 119 },
            { 35, 0.6273, 0.0778, 0.626966, 125, 148 },
            { 50, 0.4635, 0.0822, 0.4630455, 148, 180 },
            { 70, 0.3213, 0.0789, 0.320662, 183, 232 },
            { 95, 0.2318, 0.0791, 0.2309245, 216, 282 },
            { 120, 0.1842, 0.0772, 0.1830645, 246, 328 },
            { 150, 0.15, 0.0775, 0.148366, 278, 379 },
            { 185, 0.1204, 0.0769, 0.11857315, 312, 434 },
            { 240, 0.0926, 0.0759, 0.0902161, 361, 514 },
            { 300, 0.0749, 0.0756, 0.07190965, 408, 593 }
        };

        public static double[,] pvc3core = new double[16, 6]
        {
            { 1.5, 14.4777, 0.1075, 14.47721021611, 18, 18.5 },
            { 2.5, 8.8661, 0.0994, 8.86579567779961, 24, 25 },
            { 4, 5.5159, 0.0983, 5.5156974459725, 31, 34 },
            { 6, 3.6853, 0.0928, 3.68510805500982, 39, 43 },
            { 10, 2.1897, 0.0865, 2.18952848722986, 52, 60 },
            { 16, 1.3761, 0.0818, 1.37593320235756, 67, 80 },
            { 25, 0.8701, 0.0807, 0.869829076620825, 86, 101 },
            { 35, 0.6273, 0.0778, 0.62694695481336, 103, 126 },
            { 50, 0.4635, 0.0822, 0.463031434184676, 122, 153 },
            { 70, 0.3213, 0.0789, 0.320652259332024, 151, 196 },
            { 95, 0.2318, 0.0791, 0.230917485265226, 179, 238 },
            { 120, 0.1842, 0.0772, 0.183058939096267, 203, 276 },
            { 150, 0.15, 0.0775, 0.148361493123772, 230, 319 },
            { 185, 0.1204, 0.0769, 0.118569548133595, 258, 364 },
            { 240, 0.0926, 0.0759, 0.0902133595284872, 297, 430 },
            { 300, 0.0749, 0.0756, 0.0719074656188605, 336, 497 }
        };

        public static double[,] pvc4core = new double[16, 6]
        {
            { 1.5, 14.4777, 0.1199, 14.47765, 18, 18.5 },
            { 2.5, 8.8661, 0.112, 8.866065, 24, 25 },
            { 4, 5.5159, 0.1109, 5.515865, 31, 34 },
            { 6, 3.6853, 0.1054, 3.68522, 39, 43 },
            { 10, 2.1897, 0.0993, 2.189595, 52, 60 },
            { 16, 1.3761, 0.0946, 1.375975, 67, 80 },
            { 25, 0.8701, 0.0936, 0.8698555, 86, 101 },
            { 35, 0.6273, 0.0907, 0.626966, 103, 126 },
            { 50, 0.4635, 0.095, 0.4630455, 122, 153 },
            { 70, 0.3213, 0.0917, 0.320662, 151, 196 },
            { 95, 0.2318, 0.092, 0.2309245, 179, 238 },
            { 120, 0.1842, 0.0901, 0.1830645, 203, 276 },
            { 150, 0.15, 0.0904, 0.148366, 230, 319 },
            { 185, 0.1204, 0.0898, 0.11857315, 258, 364 },
            { 240, 0.0926, 0.0889, 0.0902161, 297, 430 },
            { 300, 0.0749, 0.0886, 0.07190965, 336, 497 }
        };

        public static double[,] ODxlpe2core = new double[17, 4]
        {
            { 1.5, 10.5, 14, 13 },
            { 2.5, 11.5, 15, 13.5 },
            { 4, 12.5, 16, 15 },
            { 6, 13.5, 17, 16 },
            { 10, 15.5, 20, 18 },
            { 16, 17, 21, 19 },
            { 25, 20, 25, 23 },
            { 35, 22.5, 27.5, 24.5 },
            { 50, 25, 30, 27 },
            { 70, 28.5, 34, 30.5 },
            { 95, 32.5, 39, 35 },
            { 120, 36, 42, 39.5 },
            { 150, 39.5, 46, 43.5 },
            { 185, 44.5, 52, 48.5 },
            { 240, 50, 57.5, 54 },
            { 300, 55.5, 63, 59.5 },
            { 400, 62, 0, 0 }
        };

        public static double[,] ODxlpe3core = new double[17, 4]
        {
            { 1.5, 11, 14.5, 13.5 },
            { 2.5, 12, 15.5, 14 },
            { 4, 13.5, 17, 15.5 },
            { 6, 14.5, 18, 16.5 },
            { 10, 16.5, 20.5, 18.5 },
            { 16, 18, 22, 20 },
            { 25, 21, 26.5, 23.5 },
            { 35, 24, 29, 26 },
            { 50, 26.5, 31.5, 28.5 },
            { 70, 30.5, 37, 33 },
            { 95, 35, 41.5, 37.5 },
            { 120, 38, 45, 42 },
            { 150, 42.5, 50, 46.5 },
            { 185, 47.5, 55, 51.5 },
            { 240, 53.5, 61.5, 58 },
            { 300, 59.5, 67.5, 64 },
            { 400, 67, 0, 0 }
        };

        public static double[,] ODxlpe4core = new double[17, 4]
        {
            { 1.5, 12, 15.5, 14 },
            { 2.5, 13, 16.5, 15 },
            { 4, 14.5, 18, 16.5 },
            { 6, 15.5, 19.5, 17.5 },
            { 10, 18, 22, 20 },
            { 16, 19.5, 23.5, 21.5 },
            { 25, 23.5, 28.5, 25.5 },
            { 35, 26, 31.5, 28.5 },
            { 50, 29.5, 34.5, 31.5 },
            { 70, 34, 40.5, 36 },
            { 95, 38.5, 45, 42.5 },
            { 120, 42.5, 50, 46.5 },
            { 150, 47, 55, 51 },
            { 185, 53, 60.5, 57 },
            { 240, 59.5, 67.5, 64 },
            { 300, 66.5, 74, 70.5 },
            { 400, 0, 0, 0 }
        };

        public static double[,] ODpvc2core = new double[16, 4]
        {
            { 1.5, 11, 14.5, 13 },
            { 2.5, 12, 15.5, 14 },
            { 4, 14, 17.5, 16 },
            { 6, 15, 19, 17 },
            { 10, 17, 21, 19 },
            { 16, 19, 23, 21 },
            { 25, 22, 27.5, 24.5 },
            { 35, 24.5, 30, 27 },
            { 50, 28.5, 34, 31 },
            { 70, 32, 38, 34 },
            { 95, 37, 43.5, 39 },
            { 120, 40.5, 47, 43.5 },
            { 150, 45, 52, 48 },
            { 185, 49.5, 57, 52.5 },
            { 240, 56, 63.5, 58.5 },
            { 300, 62, 70, 64.5 }
        };

        public static double[,] ODpvc3core = new double[16, 4]
        {
            { 1.5, 11.5, 15, 13.5 },
            { 2.5, 12.5, 16, 14.5 },
            { 4, 14.5, 18.5, 16.5 },
            { 6, 15.5, 20, 18 },
            { 10, 18, 22, 20 },
            { 16, 20, 24, 22 },
            { 25, 23.5, 29, 26 },
            { 35, 26, 31.5, 28.5 },
            { 50, 25, 30.5, 27 },
            { 70, 28, 34.5, 30.5 },
            { 95, 33, 39, 36.5 },
            { 120, 35.5, 42, 39.5 },
            { 150, 38.5, 46, 42.5 },
            { 185, 43.5, 51, 47.5 },
            { 240, 49, 57, 53.5 },
            { 300, 54, 62, 58 }
        };

        public static double[,] ODpvc4core = new double[16, 4]
        {
            { 1.5, 12.5, 16, 14.5 },
            { 2.5, 13.5, 17, 15.5 },
            { 4, 16, 20, 18 },
            { 6, 17, 21, 19 },
            { 10, 19.5, 23.5, 21.5 },
            { 16, 22, 27, 24 },
            { 25, 26, 31, 28 },
            { 35, 29, 34, 31 },
            { 50, 28.5, 35, 31 },
            { 70, 32, 38.5, 36 },
            { 95, 37.5, 44.5, 41 },
            { 120, 41, 48.5, 44.5 },
            { 150, 45, 52.5, 49 },
            { 185, 50, 58, 54 },
            { 240, 56, 63.5, 60 },
            { 300, 62.5, 70, 66.5 }
        };



        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void TextBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((TextBox1.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox2.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox3.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox6.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox8.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }
        private void TextBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox9.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox10.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox11.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }


        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

            if (TextBox1.Text != "") 
            {
                if (cbPower.Text == "HP")
                {
                    hp = double.Parse(TextBox1.Text);
                    power = 0.746 * hp;
                }
                else if (cbPower.Text == "kVA")
                {
                    cplxpower = double.Parse(TextBox1.Text);
                }
                else
                {
                    power = double.Parse(TextBox1.Text);
                }
                panel14.BackColor = Color.Transparent;
            }
            else if ((TextBox1.Text == "") && (!radioButton8.Checked))
            {
                power = 0;
                panel14.BackColor = Color.Red;
            }
            else
            {
                power = 0;
                panel14.BackColor = Color.Transparent;
            }

            DisabeUndoReset();
            calc_current();
            enable_vd_btn();
            enable_result_btn();
        }


        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((comboBox3.Text == "DC") && (comboBox2.Text == "Feeder"))
            {
                pf = 1;
                textBox4.Text = pf.ToString("R");
                textBox4.ReadOnly = true;
            }
            else
            {
                textBox4.ReadOnly = false;
            }

            if (comboBox3.Text != "")
            {
                phase = comboBox3.Text;
                panel17.BackColor = Color.Transparent;
            }
            else
            {
                panel17.BackColor = Color.Red;
            }


            comboBox2.Items.Clear();
            comboBox5.Items.Clear();
            if (comboBox3.Text == "Three-Phase AC")
            {
                comboBox5.Items.Insert(0, 3);
                comboBox5.Items.Insert(1, 4);

                comboBox2.Items.Insert(0, "Feeder");
                comboBox2.Items.Insert(1, "Motor");
            }
            else if (comboBox3.Text == "Single-Phase AC")
            {
                comboBox5.Items.Insert(0, 2);

                comboBox2.Items.Insert(0, "Feeder");
                comboBox2.Items.Insert(1, "Motor");
            }
            else if (comboBox3.Text == "DC")
            {
                comboBox5.Items.Insert(0, 2);

                comboBox2.Items.Insert(0, "Feeder");
            }

            DisabeUndoReset();
            calc_current();
            enable_vd_btn();
            enable_result_btn();
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((comboBox3.Text == "DC") && (comboBox2.Text == "Feeder"))
            {
                pf = 1;
                textBox4.Text = pf.ToString("R");
                textBox4.ReadOnly = true;
            }
            else
            {
                textBox4.ReadOnly = false;
            }

            if (comboBox2.Text != "")
            {
                loadtype = comboBox2.Text;
                panel18.BackColor = Color.Transparent;
            }
            else
            {
                panel18.BackColor = Color.Red;
            }

            if ((comboBox2.Text == "Motor") && !ConsiderVdStart)
            {
                label18.Enabled = true;
                label19.Enabled = true;
                textBox10.Enabled = true;
                textBox11.Enabled = true;
                label24.Enabled = true;
                label25.Enabled = true;
                label45.Enabled = true;
                label46.Enabled = true;
                textBox14.Enabled = true;
                label28.Enabled = false;
                label29.Enabled = false;
                textBox7.Enabled = false;
                textBox25.Enabled = false;
                label59.Enabled = false;
                ConsiderVdStart = false;

            }
            else if (comboBox2.Text != "Motor")
            {
                label18.Enabled = false;
                label19.Enabled = false;
                textBox10.Enabled = false;
                textBox11.Enabled = false;
                label24.Enabled = false;
                label25.Enabled = false;
                label45.Enabled = false;
                label46.Enabled = false;
                textBox14.Enabled = false;
                label28.Enabled = false;
                label29.Enabled = false;
                textBox7.Enabled = false;
                textBox25.Enabled = false;
                label59.Enabled = false;

                textBox14.Text = "";
                textBox10.Text = "";
                textBox25.Text = "";
                textBox11.Text = "";
                textBox7.Text = "";
                vdstart = 0;
                vdstartmax = 0;
                pfstart = 0;
                currentstart = 0;
                multiplier = 0;

                panel10.BackColor = Color.Transparent;
                panel27.BackColor = Color.Transparent;
                panel12.BackColor = Color.Transparent;


                ConsiderVdStart = false;
            }

            DisabeUndoReset();
            calc_current();
            enable_vd_btn();
            enable_result_btn();
        }

        private void CbPower_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPower.Text != "")
            {
                powerUnit = cbPower.Text;
            }

            if (TextBox1.Text != "")
            {
                if (cbPower.Text == "HP")
                {
                    hp = double.Parse(TextBox1.Text);
                    power = 0.746 * hp;
                }
                else if (cbPower.Text == "kVA")
                {
                    cplxpower = double.Parse(TextBox1.Text);
                }
                else
                {
                    power = double.Parse(TextBox1.Text);
                }
                panel14.BackColor = Color.Transparent;
            }
            else if ((TextBox1.Text == "") && (!radioButton8.Checked))
            {
                power = 0;
                panel14.BackColor = Color.Red;
            }
            else
            {
                power = 0;
                panel14.BackColor = Color.Transparent;
            }
            DisabeUndoReset();
            calc_current();
        }


        private void TextBox4_Leave(object sender, EventArgs e)
        {
            calc_current();
        }

        private void TextBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text != "")
            {
                length = double.Parse(textBox6.Text);
                panel25.BackColor = Color.Transparent;
            }
            else
            {
                length = 0;
                panel25.BackColor = Color.Red;
            }
            DisabeUndoReset();
            enable_vd_btn();
            enable_result_btn();
        }

        private void TextBox12_TextChanged(object sender, EventArgs e)
        {
            if (textBox12.Text != "")
            {
                n = int.Parse(textBox12.Text);
                panel31.BackColor = Color.Transparent;
            }
            else
            {
                panel31.BackColor = Color.Red;
            }
            DisabeUndoReset();
            enable_vd_btn();
            enable_result_btn();
        }

        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.Text != "")
            {
                cores = int.Parse(comboBox5.Text);
                panel30.BackColor = Color.Transparent;
            }
            else
            {
                panel30.BackColor = Color.Red;
            }

            DisabeUndoReset();
            enable_vd_btn();
            enable_result_btn();
        }

        internal void SaveAllColor()
        {
            //Form 5
            if (Properties.Settings.Default.ColorTheme == 0) //default theme
            {
                f5.BackColor = Color.Gray;
                f5.BackColor = Color.White;
                f5.TopColor = Color.Transparent;
                f5.BottomColor = Color.Transparent;
                f5.Angle = 0;

                f5.dataGridView1.BackgroundColor = Color.White; 

                f5.ForeColor = SystemColors.ControlText;
                f5.dataGridView1.ForeColor = SystemColors.ControlText;
            }
            else if (Properties.Settings.Default.ColorTheme == 1) //Skyblue theme
            {
                f5.BackColor = Color.Gray;
                f5.BackColor = Color.White;
                f5.TopColor = Color.Azure;
                f5.BottomColor = Color.LightCyan;
                f5.Angle = 300;

                f5.dataGridView1.BackgroundColor = Color.White;

                f5.ForeColor = SystemColors.ControlText;
                f5.dataGridView1.ForeColor = SystemColors.ControlText;

            }
            else if (Properties.Settings.Default.ColorTheme == 2) //Dark theme
            {
                f5.BackColor = Color.FromArgb(45, 46, 51);
                f5.TopColor = Color.Transparent;
                f5.BottomColor = Color.Transparent;
                f5.Angle = 0;

                f5.dataGridView1.BackgroundColor = Color.FromArgb(58, 59, 66);

                f5.ForeColor = SystemColors.ControlLightLight;
                f5.dataGridView1.ForeColor = SystemColors.ControlText;

            }
            else if (Properties.Settings.Default.ColorTheme == 3) //Pinky salmon theme
            {
                f5.BackColor = Color.Gray;
                f5.BackColor = Color.White;
                f5.TopColor = Color.Salmon;
                f5.BottomColor = Color.HotPink;
                f5.Angle = 0;

                f5.dataGridView1.BackgroundColor = Color.White;

                f5.ForeColor = SystemColors.ControlText;
                f5.dataGridView1.ForeColor = SystemColors.ControlText;

            }
            else if (Properties.Settings.Default.ColorTheme == 4) //Cable sizing theme
            {
                f5.BackColor = Color.Gray;
                f5.BackColor = Color.White;
                f5.TopColor = Color.Turquoise;
                f5.BottomColor = Color.DodgerBlue;
                f5.Angle = 60;

                f5.dataGridView1.BackgroundColor = Color.White;

                f5.ForeColor = SystemColors.ControlLightLight;
                f5.dataGridView1.ForeColor = SystemColors.ControlText;
            }
            else if (Properties.Settings.Default.ColorTheme == 5) //Visual Studio theme
            {
                f5.BackColor = Color.FromArgb(93, 107, 153);
                f5.TopColor = Color.Transparent;
                f5.BottomColor = Color.Transparent;
                f5.Angle = 0;

                f5.dataGridView1.BackgroundColor = Color.AliceBlue;

                f5.ForeColor = SystemColors.ControlLightLight;
                f5.dataGridView1.ForeColor = SystemColors.ControlText;
            }

            //Form 7 (form summary)
            if (Properties.Settings.Default.ColorTheme == 0) //default theme
            {
                Form5.f7.BackColor = Color.Gray;
                Form5.f7.BackColor = Color.White;
                Form5.f7.TopColor = Color.Transparent;
                Form5.f7.BottomColor = Color.Transparent;
                Form5.f7.Angle = 0;

                Form5.f7.dataGridView1.BackgroundColor = Color.White;

                Form5.f7.ForeColor = SystemColors.ControlText;
                Form5.f7.dataGridView1.ForeColor = SystemColors.ControlText;
            }
            else if (Properties.Settings.Default.ColorTheme == 1) //Skyblue theme
            {
                Form5.f7.BackColor = Color.Gray;
                Form5.f7.BackColor = Color.White;
                Form5.f7.TopColor = Color.Azure;
                Form5.f7.BottomColor = Color.LightCyan;
                Form5.f7.Angle = 300;

                Form5.f7.dataGridView1.BackgroundColor = Color.White;

                Form5.f7.ForeColor = SystemColors.ControlText;
                Form5.f7.dataGridView1.ForeColor = SystemColors.ControlText;

            }
            else if (Properties.Settings.Default.ColorTheme == 2) //Dark theme
            {
                Form5.f7.BackColor = Color.FromArgb(45, 46, 51);
                Form5.f7.TopColor = Color.Transparent;
                Form5.f7.BottomColor = Color.Transparent;
                Form5.f7.Angle = 0;

                Form5.f7.dataGridView1.BackgroundColor = Color.FromArgb(58, 59, 66);

                Form5.f7.ForeColor = SystemColors.ControlLightLight;
                Form5.f7.dataGridView1.ForeColor = SystemColors.ControlText;

            }
            else if (Properties.Settings.Default.ColorTheme == 3) //Pinky salmon theme
            {
                Form5.f7.BackColor = Color.Gray;
                Form5.f7.BackColor = Color.White;
                Form5.f7.TopColor = Color.Salmon;
                Form5.f7.BottomColor = Color.HotPink;
                Form5.f7.Angle = 0;

                Form5.f7.dataGridView1.BackgroundColor = Color.White;

                Form5.f7.ForeColor = SystemColors.ControlText;
                Form5.f7.dataGridView1.ForeColor = SystemColors.ControlText;

            }
            else if (Properties.Settings.Default.ColorTheme == 4) //Cable sizing theme
            {
                Form5.f7.BackColor = Color.Gray;
                Form5.f7.BackColor = Color.White;
                Form5.f7.TopColor = Color.Turquoise;
                Form5.f7.BottomColor = Color.DodgerBlue;
                Form5.f7.Angle = 60;

                Form5.f7.dataGridView1.BackgroundColor = Color.White;

                Form5.f7.ForeColor = SystemColors.ControlLightLight;
                Form5.f7.dataGridView1.ForeColor = SystemColors.ControlText;
            }
            else if (Properties.Settings.Default.ColorTheme == 5) //Visual Studio theme
            {
                Form5.f7.BackColor = Color.FromArgb(93, 107, 153);
                Form5.f7.TopColor = Color.Transparent;
                Form5.f7.BottomColor = Color.Transparent;
                Form5.f7.Angle = 0;

                Form5.f7.dataGridView1.BackgroundColor = Color.AliceBlue;

                Form5.f7.ForeColor = SystemColors.ControlLightLight;
                Form5.f7.dataGridView1.ForeColor = SystemColors.ControlText;
            }

            //Form 2 (UG Derating Form)
            if (Properties.Settings.Default.ColorTheme == 0) //default theme
            {

                f2.BackColor = Color.Gray;
                f2.BackColor = Color.White;
                f2.TopColor = Color.Transparent;
                f2.BottomColor = Color.Transparent;
                f2.Angle = 0;


                f2.dataGridView1.ForeColor = SystemColors.ControlText;
                f2.dataGridView2.ForeColor = SystemColors.ControlText;
                f2.dataGridView3.ForeColor = SystemColors.ControlText;
                f2.ForeColor = SystemColors.ControlText;
            }
            else if (Properties.Settings.Default.ColorTheme == 1) //Skyblue theme
            {
                f2.BackColor = Color.Gray;
                f2.BackColor = Color.White;
                f2.TopColor = Color.Azure;
                f2.BottomColor = Color.LightCyan;
                f2.Angle = 300;


                f2.dataGridView1.ForeColor = SystemColors.ControlText;
                f2.dataGridView2.ForeColor = SystemColors.ControlText;
                f2.dataGridView3.ForeColor = SystemColors.ControlText;
                f2.ForeColor = SystemColors.ControlText;

            }
            else if (Properties.Settings.Default.ColorTheme == 2) //Dark theme
            {
                f2.BackColor = Color.FromArgb(45, 46, 51);
                f2.TopColor = Color.Transparent;
                f2.BottomColor = Color.Transparent;
                f2.Angle = 0;


                f2.dataGridView1.ForeColor = SystemColors.ControlText;
                f2.dataGridView2.ForeColor = SystemColors.ControlText;
                f2.dataGridView3.ForeColor = SystemColors.ControlText;
                f2.ForeColor = SystemColors.ControlLightLight;

            }
            else if (Properties.Settings.Default.ColorTheme == 3) //Pinky salmon theme
            {
                f2.BackColor = Color.Gray;
                f2.BackColor = Color.White;
                f2.TopColor = Color.Salmon;
                f2.BottomColor = Color.HotPink;
                f2.Angle = 0;


                f2.dataGridView1.ForeColor = SystemColors.ControlText;
                f2.dataGridView2.ForeColor = SystemColors.ControlText;
                f2.dataGridView3.ForeColor = SystemColors.ControlText;
                f2.ForeColor = SystemColors.ControlText;

            }
            else if (Properties.Settings.Default.ColorTheme == 4) //Cable sizing theme
            {
                f2.BackColor = Color.Gray;
                f2.BackColor = Color.White;
                f2.TopColor = Color.Turquoise;
                f2.BottomColor = Color.DodgerBlue;
                f2.Angle = 60;


                f2.dataGridView1.ForeColor = SystemColors.ControlText;
                f2.dataGridView2.ForeColor = SystemColors.ControlText;
                f2.dataGridView3.ForeColor = SystemColors.ControlText;
                f2.ForeColor = SystemColors.ControlLightLight;
            }
            else if (Properties.Settings.Default.ColorTheme == 5) //Visual Studio theme
            {
                f2.BackColor = Color.FromArgb(93, 107, 153);
                f2.TopColor = Color.Transparent;
                f2.BottomColor = Color.Transparent;
                f2.Angle = 0;


                f2.dataGridView1.ForeColor = SystemColors.ControlText;
                f2.dataGridView2.ForeColor = SystemColors.ControlText;
                f2.dataGridView3.ForeColor = SystemColors.ControlText;
                f2.ForeColor = SystemColors.ControlLightLight;
            }


            //Form 3 (AG Derating Form)
            if (Properties.Settings.Default.ColorTheme == 0) //default theme
            {
                f3.BackColor = Color.Gray;
                f3.BackColor = Color.White;
                f3.TopColor = Color.Transparent;
                f3.BottomColor = Color.Transparent;
                f3.Angle = 0;


                f3.dataGridView1.ForeColor = SystemColors.ControlText;
                f3.dataGridView2.ForeColor = SystemColors.ControlText;
                f3.ForeColor = SystemColors.ControlText;
            }
            else if (Properties.Settings.Default.ColorTheme == 1) //Skyblue theme
            {
                f3.BackColor = Color.Gray;
                f3.BackColor = Color.White;
                f3.TopColor = Color.Azure;
                f3.BottomColor = Color.LightCyan;
                f3.Angle = 300;


                f3.dataGridView1.ForeColor = SystemColors.ControlText;
                f3.dataGridView2.ForeColor = SystemColors.ControlText;
                f3.ForeColor = SystemColors.ControlText;

            }
            else if (Properties.Settings.Default.ColorTheme == 2) //Dark theme
            {
                f3.BackColor = Color.FromArgb(45, 46, 51);
                f3.TopColor = Color.Transparent;
                f3.BottomColor = Color.Transparent;
                f3.Angle = 0;


                f3.dataGridView1.ForeColor = SystemColors.ControlText;
                f3.dataGridView2.ForeColor = SystemColors.ControlText;
                f3.ForeColor = SystemColors.ControlLightLight;

            }
            else if (Properties.Settings.Default.ColorTheme == 3) //Pinky salmon theme
            {
                f3.BackColor = Color.Gray;
                f3.BackColor = Color.White;
                f3.TopColor = Color.Salmon;
                f3.BottomColor = Color.HotPink;
                f3.Angle = 0;


                f3.dataGridView1.ForeColor = SystemColors.ControlText;
                f3.dataGridView2.ForeColor = SystemColors.ControlText;
                f3.ForeColor = SystemColors.ControlText;

            }
            else if (Properties.Settings.Default.ColorTheme == 4) //Cable sizing theme
            {
                f3.BackColor = Color.Gray;
                f3.BackColor = Color.White;
                f3.TopColor = Color.Turquoise;
                f3.BottomColor = Color.DodgerBlue;
                f3.Angle = 60;


                f3.dataGridView1.ForeColor = SystemColors.ControlText;
                f3.dataGridView2.ForeColor = SystemColors.ControlText;
                f3.ForeColor = SystemColors.ControlLightLight;
            }
            else if (Properties.Settings.Default.ColorTheme == 5) //Visual Studio theme
            {
                f3.BackColor = Color.FromArgb(93, 107, 153);
                f3.TopColor = Color.Transparent;
                f3.BottomColor = Color.Transparent;
                f3.Angle = 0;


                f3.dataGridView1.ForeColor = SystemColors.ControlText;
                f3.dataGridView2.ForeColor = SystemColors.ControlText;
                f3.ForeColor = SystemColors.ControlLightLight;
            }

            //Form 6 (IEC Manual Input)
            if (Properties.Settings.Default.ColorTheme == 0) //default theme
            {
                f6.BackColor = Color.Gray;
                f6.BackColor = Color.White;
                f6.TopColor = Color.Transparent;
                f6.BottomColor = Color.Transparent;
                f6.Angle = 0;

                f6.dataGridView1.BackgroundColor = Color.White;

                f6.ForeColor = SystemColors.ControlText;
                f6.dataGridView1.ForeColor = SystemColors.ControlText;
            }
            else if (Properties.Settings.Default.ColorTheme == 1) //Skyblue theme
            {
                f6.BackColor = Color.Gray;
                f6.BackColor = Color.White;
                f6.TopColor = Color.Azure;
                f6.BottomColor = Color.LightCyan;
                f6.Angle = 300;

                f6.dataGridView1.BackgroundColor = Color.White;

                f6.ForeColor = SystemColors.ControlText;
                f6.dataGridView1.ForeColor = SystemColors.ControlText;

            }
            else if (Properties.Settings.Default.ColorTheme == 2) //Dark theme
            {
                f6.BackColor = Color.FromArgb(45, 46, 51);
                f6.TopColor = Color.Transparent;
                f6.BottomColor = Color.Transparent;
                f6.Angle = 0;

                f6.dataGridView1.BackgroundColor = Color.FromArgb(58, 59, 66);

                f6.ForeColor = SystemColors.ControlLightLight;
                f6.dataGridView1.ForeColor = SystemColors.ControlText;

            }
            else if (Properties.Settings.Default.ColorTheme == 3) //Pinky salmon theme
            {
                f6.BackColor = Color.Gray;
                f6.BackColor = Color.White;
                f6.TopColor = Color.Salmon;
                f6.BottomColor = Color.HotPink;
                f6.Angle = 0;

                f6.dataGridView1.BackgroundColor = Color.White;

                f6.ForeColor = SystemColors.ControlText;
                f6.dataGridView1.ForeColor = SystemColors.ControlText;

            }
            else if (Properties.Settings.Default.ColorTheme == 4) //Cable sizing theme
            {
                f6.BackColor = Color.Gray;
                f6.BackColor = Color.White;
                f6.TopColor = Color.Turquoise;
                f6.BottomColor = Color.DodgerBlue;
                f6.Angle = 60;

                f6.dataGridView1.BackgroundColor = Color.White;

                f6.ForeColor = SystemColors.ControlLightLight;
                f6.dataGridView1.ForeColor = SystemColors.ControlText;
            }
            else if (Properties.Settings.Default.ColorTheme == 5) //Visual Studio theme
            {
                f6.BackColor = Color.FromArgb(93, 107, 153);
                f6.TopColor = Color.Transparent;
                f6.BottomColor = Color.Transparent;
                f6.Angle = 0;

                f6.dataGridView1.BackgroundColor = Color.AliceBlue;

                f6.ForeColor = SystemColors.ControlLightLight;
                f6.dataGridView1.ForeColor = SystemColors.ControlText;
            }

            //Form About
            if (Properties.Settings.Default.ColorTheme == 0) //default theme
            {
                fAbout.BackColor = SystemColors.Control;
                fAbout.TopColor = Color.Transparent;
                fAbout.BottomColor = Color.Transparent;
                fAbout.Angle = 0;

                fAbout.panel1.BackColor = Color.Transparent;

                fAbout.ForeColor = SystemColors.ControlText;
                fAbout.panel1.ForeColor = SystemColors.ControlText;
            }
            else if (Properties.Settings.Default.ColorTheme == 1) //Skyblue theme
            {
                fAbout.BackColor = Color.Gray;
                fAbout.BackColor = Color.White;
                fAbout.TopColor = Color.Azure;
                fAbout.BottomColor = Color.LightCyan;
                fAbout.Angle = 300;

                fAbout.panel1.BackColor = Color.Transparent;

                fAbout.ForeColor = SystemColors.ControlText;
                fAbout.panel1.ForeColor = SystemColors.ControlText;

            }
            else if (Properties.Settings.Default.ColorTheme == 2) //Dark theme
            {
                fAbout.BackColor = Color.FromArgb(45, 46, 51);
                fAbout.TopColor = Color.Transparent;
                fAbout.BottomColor = Color.Transparent;
                fAbout.Angle = 0;

                fAbout.panel1.BackColor = Color.FromArgb(58, 59, 66);

                fAbout.ForeColor = SystemColors.ControlLightLight;
                fAbout.panel1.ForeColor = SystemColors.ControlLightLight;

            }
            else if (Properties.Settings.Default.ColorTheme == 3) //Pinky salmon theme
            {
                fAbout.BackColor = Color.Gray;
                fAbout.BackColor = Color.White;
                fAbout.TopColor = Color.Salmon;
                fAbout.BottomColor = Color.HotPink;
                fAbout.Angle = 0;

                fAbout.panel1.BackColor = Color.Transparent;

                fAbout.ForeColor = SystemColors.ControlText;
                fAbout.panel1.ForeColor = SystemColors.ControlText;

            }
            else if (Properties.Settings.Default.ColorTheme == 4) //Cable sizing theme
            {
                fAbout.BackColor = Color.Gray;
                fAbout.BackColor = Color.White;
                fAbout.TopColor = Color.Turquoise;
                fAbout.BottomColor = Color.DodgerBlue;
                fAbout.Angle = 60;

                fAbout.panel1.BackColor = Color.Transparent;

                fAbout.ForeColor = SystemColors.ControlLightLight;
                fAbout.panel1.ForeColor = SystemColors.ControlText;
            }
            else if (Properties.Settings.Default.ColorTheme == 5) //Visual Studio theme
            {
                fAbout.BackColor = Color.FromArgb(93, 107, 153);
                fAbout.TopColor = Color.Transparent;
                fAbout.BottomColor = Color.Transparent;
                fAbout.Angle = 0;

                fAbout.panel1.BackColor = Color.AliceBlue;

                fAbout.ForeColor = SystemColors.ControlLightLight;
                fAbout.panel1.ForeColor = SystemColors.ControlText;
            }
        }

        internal void LoadColor()
        {
            if (Properties.Settings.Default.ColorTheme == 0) //default theme
            {
                BackColor = SystemColors.Control;
                TopColor = Color.Transparent;
                BottomColor = Color.Transparent;
                Angle = 0;

                panel2.BackColor = Color.White;

                ForeColor = SystemColors.ControlText;


                label21.ForeColor = SystemColors.ControlText;
            }
            else if (Properties.Settings.Default.ColorTheme == 1) //Skyblue theme
            {
                BackColor = Color.Gray;
                BackColor = Color.White;
                TopColor = Color.Azure;
                BottomColor = Color.LightCyan;
                Angle = 300;

                panel2.BackColor = Color.Transparent;

                ForeColor = SystemColors.ControlText;

                label21.ForeColor = SystemColors.ControlText;
            }
            else if (Properties.Settings.Default.ColorTheme == 2) //Dark theme
            {
                BackColor = Color.FromArgb(45, 46, 51);
                TopColor = Color.Transparent;
                BottomColor = Color.Transparent;
                Angle = 0;

                panel2.BackColor = Color.FromArgb(58, 59, 66);

                ForeColor = SystemColors.ControlLightLight;

                label21.ForeColor = SystemColors.ControlLightLight;
            }
            else if (Properties.Settings.Default.ColorTheme == 3) //Pinky salmon theme
            {
                BackColor = Color.Gray;
                BackColor = Color.White;
                TopColor = Color.Salmon;
                BottomColor = Color.HotPink;
                Angle = 0;

                panel2.BackColor = Color.Transparent;

                ForeColor = SystemColors.ControlText;

                label21.ForeColor = SystemColors.ControlText;
            }
            else if (Properties.Settings.Default.ColorTheme == 4) //Cable sizing theme
            {
                BackColor = Color.Gray;
                BackColor = Color.White;
                TopColor = Color.Turquoise;
                BottomColor = Color.DodgerBlue;
                Angle = 60;

                panel2.BackColor = Color.Transparent;

                ForeColor = SystemColors.ControlLightLight;

                label21.ForeColor = SystemColors.ControlLightLight;
            }
            else if (Properties.Settings.Default.ColorTheme == 5) //Visual Studio theme
            {
                BackColor = Color.FromArgb(93, 107, 153);
                TopColor = Color.Transparent;
                BottomColor = Color.Transparent;
                Angle = 0;

                panel2.BackColor = Color.AliceBlue;

                ForeColor = SystemColors.ControlText;

                label21.ForeColor = SystemColors.ControlLightLight;

            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            LoadColor();
            SaveAllColor();

            OpenFromMain = false;
            FileOpened = false;
            
            OpenForm.formMainClose = false;

            comboBox8.Text = "PVC";

            if (Form5.f7.IsDisposed)
            {
                Form5.f7 = new Form7();
            }

            //set new dtdiameter everytime this form is loaded
            dtdiameter = new DataTable();
            dtdiameter.TableName = "Data";

            //set dtdiameter columns
            dtdiameter.Columns.Add("Diameter");
            dtdiameter.Columns.Add("TagNo");
            dtdiameter.Columns.Add("From");
            dtdiameter.Columns.Add("FromDesc");
            dtdiameter.Columns.Add("To");
            dtdiameter.Columns.Add("ToDesc");
            dtdiameter.Columns.Add("Phase");
            dtdiameter.Columns.Add("LoadType");
            dtdiameter.Columns.Add("VoltageSystem");
            dtdiameter.Columns.Add("CurrentButton");
            dtdiameter.Columns.Add("cbPower");
            dtdiameter.Columns.Add("Power");
            dtdiameter.Columns.Add("Current");
            dtdiameter.Columns.Add("Voltage");
            dtdiameter.Columns.Add("Efficiency");
            dtdiameter.Columns.Add("PF");
            dtdiameter.Columns.Add("PFStart");
            dtdiameter.Columns.Add("Multiplier");
            dtdiameter.Columns.Add("RatedVoltage");
            dtdiameter.Columns.Add("Material");
            dtdiameter.Columns.Add("Insulation");
            dtdiameter.Columns.Add("Armour");
            dtdiameter.Columns.Add("OutherSheath");
            dtdiameter.Columns.Add("Installation");
            dtdiameter.Columns.Add("DeratingButton");
            dtdiameter.Columns.Add("K1");
            dtdiameter.Columns.Add("K2");
            dtdiameter.Columns.Add("K3");
            dtdiameter.Columns.Add("Kt");
            dtdiameter.Columns.Add("Length");
            dtdiameter.Columns.Add("VdRunMax");
            dtdiameter.Columns.Add("VdStartMax");
            dtdiameter.Columns.Add("CablePropButton");
            dtdiameter.Columns.Add("VdRun");
            dtdiameter.Columns.Add("VdStart");
            dtdiameter.Columns.Add("Lmax");
            dtdiameter.Columns.Add("N");
            dtdiameter.Columns.Add("Cores");
            dtdiameter.Columns.Add("WireArea");
            dtdiameter.Columns.Add("Rac");
            dtdiameter.Columns.Add("X");
            dtdiameter.Columns.Add("Irated");
            dtdiameter.Columns.Add("Iderated");
            dtdiameter.Columns.Add("SCOrLTE");
            dtdiameter.Columns.Add("SCCurrent");
            dtdiameter.Columns.Add("TBreak");
            dtdiameter.Columns.Add("InitialTemp");
            dtdiameter.Columns.Add("FinalTemp");
            dtdiameter.Columns.Add("CLTE");
            dtdiameter.Columns.Add("BreakerType");
            dtdiameter.Columns.Add("BreakerBtn");
            dtdiameter.Columns.Add("SCRating");
            dtdiameter.Columns.Add("BreakNominalCurrent");
            dtdiameter.Columns.Add("BLTE");
            dtdiameter.Columns.Add("i");
            dtdiameter.Columns.Add("Database");


            cbPower.Text = "kW";

            //load saved/default settings
            LoadIECDatabase();
            comboBoxVendor.Text = "Sumi Indo Cable (Default)";
            xlpe2coreLength = 17;
            xlpe3coreLength = 17;
            xlpe4coreLength = 17;
            pvc2coreLength = 16;
            pvc3coreLength = 16;
            pvc4coreLength = 16;

            decimalseparator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);

            //load ktmain textbox location
            kttextboxX = textBox19.Location.X;
            kttextboxY = textBox19.Location.Y;
            ktlabelX = label43.Location.X;
            ktlabelY = label43.Location.Y;

            Form5.f7.dataGridView1.Columns[2].HeaderText = "Cable Total Length (m)";


            Form5.Standard = 1;

            //default efficiency
            eff = 1;
        }

        private void TextBox15_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox15.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if ((comboBox9.Text == "D1 (Under Ground)") || (comboBox9.Text == "D2 (Under Ground)"))
            {
                if (comboBox9.Text == "D1 (Under Ground)")
                {
                    f2.label8.Visible = true;
                    f2.label7.Visible = false;
                }
                else if (comboBox9.Text == "D2 (Under Ground)")
                {
                    f2.label8.Visible = false;
                    f2.label7.Visible = true;
                }
                f2.FormClosed += F2_FormClosed;
                f2.ShowDialog();
            }
            else if (comboBox9.Text == "E (Above Ground)")
            {
                f3.FormClosed += F3_FormClosed;
                f3.ShowDialog();
            }
        }

        private void F2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ok_clicked)
            {
                textBox17.Text = k1main.ToString("R");
                textBox18.Text = k2main.ToString("R");
                textBox36.Text = k3main.ToString("R");
                textBox19.Text = ktmain.ToString("0.##");
            }
            ok_clicked = false;
        }

        private void F3_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ok_clicked)
            {
                textBox17.Text = k1main.ToString("R");
                textBox18.Text = k2main.ToString("R");
                textBox19.Text = ktmain.ToString("0.##");
            }
            ok_clicked = false;
        }

        private void ComboBox4_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            material = comboBox4.Text;
            if (comboBox4.Text != "")
            {
                panel19.BackColor = Color.Transparent;
            }
            else
            {
                panel19.BackColor = Color.Red;
            }
            DisabeUndoReset();
            Calc_k();
            enable_vd_btn();
            enable_result_btn();
        }

        private void ComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            insulation = comboBox6.Text;
            if (insulation == "PVC")
            {
                textBox15.Text = "70";
                textBox24.Text = "160";
                panel20.BackColor = Color.Transparent;
                label75.Text = "Ω/km at 70°C";
            }
            else if ((insulation == "XLPE") || (insulation == "EPR"))
            {
                textBox15.Text = "90";
                textBox24.Text = "250";
                panel20.BackColor = Color.Transparent;
                label75.Text = "Ω/km at 90°C";
            }
            else
            {
                textBox15.Text = "";
                textBox24.Text = "";
                textBox21.Text = "";
                label75.Text = "Ω/km ";
                panel20.BackColor = Color.Red;
            }

            DisabeUndoReset();
            reset_correction();
            enable_correction();
            enable_vd_btn();
            enable_result_btn();
        }

        private void ComboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            armour = comboBox7.Text;
            if (comboBox7.Text != "")
            {
                panel21.BackColor = Color.Transparent;
            }
            else
            {
                panel21.BackColor = Color.Red;
            }
            DisabeUndoReset();
            enable_vd_btn();
            enable_result_btn();
        }

        private void ComboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            installation = comboBox9.Text;
            if (installation == "E (Above Ground)")
            {
                textBox36.Enabled = false;
                textBox36.Text = "";
                k3main = 0;
                label80.Enabled = false;
            }
            else
            {
                textBox36.Enabled = true;
                label80.Enabled = true;
            }
            if (comboBox9.Text != "")
            {
                panel22.BackColor = Color.Transparent;
            }
            else
            {
                panel22.BackColor = Color.Red;
            }

            DisabeUndoReset();
            reset_correction();
            enable_correction();
            enable_vd_btn();
            enable_result_btn();
        }

        // calculate wire size based on vd and fl current
        private void vd_size_calc()
        {
            n = int.Parse(textBox12.Text);
            complete = false;

            while (!complete)
            {
                i = 0;
                if (radioButton4.Checked)
                {
                    if (insulation == "XLPE")
                    {
                        if (cores == 2)
                        {
                            if (xlpe2coreLength > 0)
                            {
                                while ((!complete) && (i < xlpe2coreLength))
                                {
                                    wirearea = xlpe2coreDB[i, 0];
                                    if (phase == "DC")
                                    {
                                        Rdc = xlpe2coreDB[i, 3];
                                        vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                    }
                                    else //AC
                                    {
                                        Rac = xlpe2coreDB[i, 1];
                                        X = xlpe2coreDB[i, 2];

                                        if (phase == "Single-Phase AC")
                                        {
                                            vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                            / (n * 1000 * voltage);

                                            lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                                (X * Math.Sqrt(1 - pf * pf))) * 100);

                                            if ((loadtype == "Motor") && ConsiderVdStart)
                                            {
                                                vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                / (n * 1000 * voltage);
                                            }
                                            else
                                            {
                                                vdstart = 0;
                                            }
                                        }
                                        else if (phase == "Three-Phase AC")
                                        {
                                            vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                            / (n * 1000 * voltage);

                                            lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                                (X * Math.Sqrt(1 - pf * pf))) * 100);

                                            if ((loadtype == "Motor") && ConsiderVdStart)
                                            {
                                                vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                / (n * 1000 * voltage);
                                            }
                                            else
                                            {
                                                vdstart = 0;
                                            }
                                        }
                                    }

                                    if (armour == "Non Armoured")
                                    {
                                        diameter = ODxlpe2core[i, 1];
                                    }
                                    else if (armour == "SWA")
                                    {
                                        diameter = ODxlpe2core[i, 2];
                                    }
                                    else if (armour == "DSTA")
                                    {
                                        diameter = ODxlpe2core[i, 3];
                                    }


                                    if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                                    {
                                        Irated = xlpe2coreDB[i, 4] * n;
                                    }
                                    else //above ground
                                    {
                                        Irated = xlpe2coreDB[i, 5] * n;
                                    }

                                    iderated = Irated * ktmain;

                                    cable_lte();

                                    // Validasi
                                    validasi_vd();

                                    i++;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Selected cable data specification for 2 Core XLPE insulation doesn't exist!",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                complete = true;
                                inputValid = false;
                            }

                        }
                        else if (cores == 3)
                        {
                            if (xlpe3coreLength > 0)
                            {
                                while ((!complete) && (i < xlpe3coreLength))
                                {
                                    wirearea = xlpe3coreDB[i, 0];
                                    if (phase == "DC")
                                    {
                                        Rdc = xlpe3coreDB[i, 3];
                                        vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                    }
                                    else //AC
                                    {
                                        Rac = xlpe3coreDB[i, 1];
                                        X = xlpe3coreDB[i, 2];

                                        if (phase == "Single-Phase AC")
                                        {
                                            vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                            / (n * 1000 * voltage);

                                            lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                                (X * Math.Sqrt(1 - pf * pf))) * 100);

                                            if ((loadtype == "Motor") && ConsiderVdStart)
                                            {
                                                vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                / (n * 1000 * voltage);
                                            }
                                            else
                                            {
                                                vdstart = 0;
                                            }
                                        }
                                        else if (phase == "Three-Phase AC")
                                        {
                                            vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                            / (n * 1000 * voltage);

                                            lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                                (X * Math.Sqrt(1 - pf * pf))) * 100);

                                            if ((loadtype == "Motor") && ConsiderVdStart)
                                            {
                                                vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                / (n * 1000 * voltage);
                                            }
                                            else
                                            {
                                                vdstart = 0;
                                            }
                                        }
                                    }

                                    if (armour == "Non Armoured")
                                    {
                                        diameter = ODxlpe3core[i, 1];
                                    }
                                    else if (armour == "SWA")
                                    {
                                        diameter = ODxlpe3core[i, 2];
                                    }
                                    else if (armour == "DSTA")
                                    {
                                        diameter = ODxlpe3core[i, 3];
                                    }

                                    if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                                    {
                                        Irated = xlpe3coreDB[i, 4] * n;
                                    }
                                    else //above ground
                                    {
                                        Irated = xlpe3coreDB[i, 5] * n;
                                    }

                                    iderated = Irated * ktmain;
                                    cable_lte();

                                    // Validasi
                                    validasi_vd();

                                    i++;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Selected cable data specification for 3 Core XLPE insulation doesn't exist!",
                                 "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                complete = true;
                                inputValid = false;
                            }
                        }
                        else if (cores == 4)
                        {
                            if (xlpe4coreLength > 0)
                            {
                                while ((!complete) && (i < xlpe4coreLength))
                                {
                                    wirearea = xlpe4coreDB[i, 0];
                                    if (phase == "DC")
                                    {
                                        Rdc = xlpe4coreDB[i, 3];
                                        vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                    }
                                    else //AC
                                    {
                                        Rac = xlpe4coreDB[i, 1];
                                        X = xlpe4coreDB[i, 2];

                                        if (phase == "Single-Phase AC")
                                        {
                                            vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                            / (n * 1000 * voltage);

                                            lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                                (X * Math.Sqrt(1 - pf * pf))) * 100);

                                            if ((loadtype == "Motor") && ConsiderVdStart)
                                            {
                                                vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                / (n * 1000 * voltage);
                                            }
                                            else
                                            {
                                                vdstart = 0;
                                            }
                                        }
                                        else if (phase == "Three-Phase AC")
                                        {
                                            vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                            / (n * 1000 * voltage);

                                            lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                                (X * Math.Sqrt(1 - pf * pf))) * 100);

                                            if ((loadtype == "Motor") && ConsiderVdStart)
                                            {
                                                vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                / (n * 1000 * voltage);
                                            }
                                            else
                                            {
                                                vdstart = 0;
                                            }
                                        }
                                    }

                                    if (armour == "Non Armoured")
                                    {
                                        diameter = ODxlpe4core[i, 1];
                                    }
                                    else if (armour == "SWA")
                                    {
                                        diameter = ODxlpe4core[i, 2];
                                    }
                                    else if (armour == "DSTA")
                                    {
                                        diameter = ODxlpe4core[i, 3];
                                    }


                                    if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                                    {
                                        Irated = xlpe4coreDB[i, 4] * n;
                                    }
                                    else //above ground
                                    {
                                        Irated = xlpe4coreDB[i, 5] * n;
                                    }

                                    iderated = Irated * ktmain;
                                    cable_lte();

                                    // Validasi
                                    validasi_vd();

                                    i++;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Selected cable data specification for 4 Core XLPE insulation doesn't exist!",
                                 "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                complete = true;
                                inputValid = false;
                            }
                        }
                    }
                    else if (insulation == "PVC")
                    {
                        if (cores == 2)
                        {
                            if (pvc2coreLength > 0)
                            {
                                while ((!complete) && (i < pvc2coreLength))
                                {
                                    wirearea = pvc2coreDB[i, 0];
                                    if (phase == "DC")
                                    {
                                        Rdc = pvc2coreDB[i, 3];
                                        vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                    }
                                    else //AC
                                    {
                                        Rac = pvc2coreDB[i, 1];
                                        X = pvc2coreDB[i, 2];

                                        if (phase == "Single-Phase AC")
                                        {
                                            vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                            / (n * 1000 * voltage);

                                            lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                                (X * Math.Sqrt(1 - pf * pf))) * 100);

                                            if ((loadtype == "Motor") && ConsiderVdStart)
                                            {
                                                vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                / (n * 1000 * voltage);
                                            }
                                            else
                                            {
                                                vdstart = 0;
                                            }
                                        }
                                        else if (phase == "Three-Phase AC")
                                        {
                                            vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                            / (n * 1000 * voltage);

                                            lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                                (X * Math.Sqrt(1 - pf * pf))) * 100);

                                            if ((loadtype == "Motor") && ConsiderVdStart)
                                            {
                                                vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                / (n * 1000 * voltage);
                                            }
                                            else
                                            {
                                                vdstart = 0;
                                            }
                                        }
                                    }

                                    if (armour == "Non Armoured")
                                    {
                                        diameter = ODpvc2core[i, 1];
                                    }
                                    else if (armour == "SWA")
                                    {
                                        diameter = ODpvc2core[i, 2];
                                    }
                                    else if (armour == "DSTA")
                                    {
                                        diameter = ODpvc2core[i, 3];
                                    }

                                    if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                                    {
                                        Irated = pvc2coreDB[i, 4] * n;
                                    }
                                    else //above ground
                                    {
                                        Irated = pvc2coreDB[i, 5] * n;
                                    }

                                    iderated = Irated * ktmain;
                                    cable_lte();

                                    // Validasi
                                    validasi_vd();

                                    i++;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Selected cable data specification for 2 Core PVC insulation doesn't exist!",
                                 "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                complete = true;
                                inputValid = false;
                            }
                        }
                        else if (cores == 3)
                        {
                            if (pvc3coreLength > 0)
                            {
                                while ((!complete) && (i < pvc3coreLength))
                                {
                                    wirearea = pvc3coreDB[i, 0];
                                    if (phase == "DC")
                                    {
                                        Rdc = pvc3coreDB[i, 3];
                                        vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                    }
                                    else //AC
                                    {
                                        Rac = pvc3coreDB[i, 1];
                                        X = pvc3coreDB[i, 2];

                                        if (phase == "Single-Phase AC")
                                        {
                                            vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                            / (n * 1000 * voltage);

                                            lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                                (X * Math.Sqrt(1 - pf * pf))) * 100);

                                            if ((loadtype == "Motor") && ConsiderVdStart)
                                            {
                                                vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                / (n * 1000 * voltage);
                                            }
                                            else
                                            {
                                                vdstart = 0;
                                            }
                                        }
                                        else if (phase == "Three-Phase AC")
                                        {
                                            vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                            / (n * 1000 * voltage);

                                            lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                                (X * Math.Sqrt(1 - pf * pf))) * 100);

                                            if ((loadtype == "Motor") && ConsiderVdStart)
                                            {
                                                vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                / (n * 1000 * voltage);
                                            }
                                            else
                                            {
                                                vdstart = 0;
                                            }
                                        }
                                    }

                                    if (armour == "Non Armoured")
                                    {
                                        diameter = ODpvc3core[i, 1];
                                    }
                                    else if (armour == "SWA")
                                    {
                                        diameter = ODpvc3core[i, 2];
                                    }
                                    else if (armour == "DSTA")
                                    {
                                        diameter = ODpvc3core[i, 3];
                                    }

                                    if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                                    {
                                        Irated = pvc3coreDB[i, 4] * n;
                                    }
                                    else //above ground
                                    {
                                        Irated = pvc3coreDB[i, 5] * n;
                                    }

                                    iderated = Irated * ktmain;
                                    cable_lte();

                                    // Validasi
                                    validasi_vd();

                                    i++;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Selected cable data specification for 3 Core PVC insulation doesn't exist!",
                                 "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                complete = true;
                                inputValid = false;
                            }
                        }
                        else if (cores == 4)
                        {
                            if (pvc4coreLength > 0)
                            {
                                while ((!complete) && (i < pvc4coreLength))
                                {
                                    wirearea = pvc4coreDB[i, 0];
                                    if (phase == "DC")
                                    {
                                        Rdc = pvc4coreDB[i, 3];
                                        vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);

                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                    }
                                    else //AC
                                    {
                                        Rac = pvc4coreDB[i, 1];
                                        X = pvc4coreDB[i, 2];

                                        if (phase == "Single-Phase AC")
                                        {
                                            vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                            / (n * 1000 * voltage);

                                            lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                                (X * Math.Sqrt(1 - pf * pf))) * 100);

                                            if ((loadtype == "Motor") && ConsiderVdStart)
                                            {
                                                vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                / (n * 1000 * voltage);
                                            }
                                            else
                                            {
                                                vdstart = 0;
                                            }
                                        }
                                        else if (phase == "Three-Phase AC")
                                        {
                                            vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                            / (n * 1000 * voltage);

                                            lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                                (X * Math.Sqrt(1 - pf * pf))) * 100);


                                            if ((loadtype == "Motor") && ConsiderVdStart)
                                            {
                                                vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                / (n * 1000 * voltage);
                                            }
                                            else
                                            {
                                                vdstart = 0;
                                            }
                                        }
                                    }

                                    if (armour == "Non Armoured")
                                    {
                                        diameter = ODpvc4core[i, 1];
                                    }
                                    else if (armour == "SWA")
                                    {
                                        diameter = ODpvc4core[i, 2];
                                    }
                                    else if (armour == "DSTA")
                                    {
                                        diameter = ODpvc4core[i, 3];
                                    }

                                    if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                                    {
                                        Irated = pvc4coreDB[i, 4] * n;
                                    }
                                    else //above ground
                                    {
                                        Irated = pvc4coreDB[i, 5] * n;
                                    }

                                    iderated = Irated * ktmain;
                                    cable_lte();

                                    // Validasi
                                    validasi_vd();

                                    i++;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Selected cable data specification for 4 Core PVC insulation doesn't exist!",
                                 "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                complete = true;
                                inputValid = false;
                            }
                        }
                    }
                    else if (insulation == "EPR")
                    {
                        MessageBox.Show("Vendor data cable specification for EPR insulation doesn't exist at the time, please insert cable specification data manually!",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        complete = true;
                        inputValid = false;
                    }
                    if (!complete)
                    {
                        solvableOrNPlus_Vd();
                    }
                }
                else if (radioButton3.Checked) //manual cable database input
                {
                    while ((!complete) && (i < cableCount))
                    {
                        wirearea = inputCableData[i, 0];

                        if (phase == "DC")
                        {
                            Rdc = inputCableData[i, 2];
                            Rac = 0;

                            vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                            lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                        }
                        else
                        {
                            Rac = inputCableData[i, 2];
                            Rdc = 0;
                            X = inputCableData[i, 3];

                            if (phase == "Single-Phase AC")
                            {
                                vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                / (n * 1000 * voltage);

                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                    (X * Math.Sqrt(1 - pf * pf))) * 100);

                                if ((loadtype == "Motor") && ConsiderVdStart)
                                {
                                    vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                    / (n * 1000 * voltage);
                                }
                                else
                                {
                                    vdstart = 0;
                                }
                            }
                            else if (phase == "Three-Phase AC")
                            {
                                vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                / (n * 1000 * voltage);

                                lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                    (X * Math.Sqrt(1 - pf * pf))) * 100);

                                if ((loadtype == "Motor") && ConsiderVdStart)
                                {
                                    vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                    / (n * 1000 * voltage);
                                }
                                else
                                {
                                    vdstart = 0;
                                }
                            }
                        }

                        Irated = inputCableData[i, 4];
                        diameter = inputCableData[i, 5];

                        iderated = Irated * ktmain;
                        cable_lte();

                        // Validasi
                        validasi_vd();

                        i++;
                    }
                    if (!complete)
                    {
                        solvableOrNPlus_Vd();
                    }
                }

            }

            if (inputValid)
            {
                textBox12.Text = n.ToString();
                textBox8.Text = vdrun.ToString("0.##");
                textBox29.Text = lmax.ToString("0.##");
                textBox22.Text = cLTE.ToString("0.##");

                if (material == "Copper")
                {
                    materialname = "Cu";
                }

                if ((loadtype == "Motor") && ConsiderVdStart)
                {
                    textBox10.Text = vdstart.ToString("0.##");
                }

                readtemp = "";

                readtemp += n.ToString() + "  ×  " + cores.ToString("0.##") + "/C  #  " + wirearea.ToString() +
                    " mm²    " + ratedvoltage + " / " + materialname + " / " + insulation;

                if (armour != "Non Armoured")
                {
                    readtemp += " / " + armour;
                }

                readtemp += " / " + outersheath;

                tbResult.Text = readtemp;

                save_vd_result(); //MUNGKIN DUIBAH

                SelectedDataLength();

                Update_size();

                enable_save();
                if (phase == "DC")
                {
                    textBox34.Text = Rdc.ToString("0.####");
                    textBox33.Text = "";
                }
                else //AC
                {
                    textBox34.Text = Rac.ToString("0.####");
                    textBox33.Text = X.ToString("0.####");
                }
                textBox32.Text = Irated.ToString("0.##");

                label86.Visible = true;
                timer1.Enabled = true;

                //enable sc/lte panel
                panel4.Enabled = true;

                // reset s.c & breaker
                textBox28.Text = "";
                textBox23.Text = "";
                textBox30.Text = "";
                textBox20.Text = "";

                comboBox12.SelectedIndex = -1;
                comboBox10.SelectedIndex = -1;
                comboBox11.SelectedIndex = -1;
                comboBox10.Text = "";
                comboBox11.Text = "";
                comboBox5.Enabled = false;
                textBox12.ReadOnly = true;

                button8.Enabled = true;
                panel5.Enabled = false;
                panel6.Enabled = false;

                label87.Text = "Since Vd run is lower than Vd run max,\ntherefore cable size of " + wirearea + " mm²\nis acceptable";
                label87.Visible = true;
                timer2.Enabled = true;
            }
            else
            {
                tbResult.Text = "Failed to get a suitable cable size";
                disable_save();
                textBox8.Text = "";
                textBox10.Text = "";
                textBox29.Text = "";
                textBox22.Text = "";
            }
        }

        //validate vd and fl current
        private void validasi_vd()
        {
            if ((current < iderated) && (vdrun < vdrunmax))
            {
                if ((loadtype == "Motor") && ConsiderVdStart)
                {
                    if (vdstart < vdstartmax)
                    {
                        complete = true;
                        inputValid = true;

                    }
                    else
                    {
                        complete = false;
                        inputValid = false;
                    }
                }
                else
                {
                    complete = true;
                    inputValid = true;
                }

            }
            else
            {
                complete = false;
                inputValid = false;
            }
        }

        //calculate wire size based on all data
        public void area_calc()
        {
            if (finalTemp < initialTemp)
            {
                MessageBox.Show("Invalid value on following input: \n- Final temperature must be greated than the initial temperature",
                    "Input Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                n = int.Parse(textBox12.Text);
                complete = false;
                while (!complete)
                {
                    i = m;
                    if (radioButton4.Checked)
                    {
                        if (insulation == "XLPE")
                        {
                            if (cores == 2)
                            {
                                if (xlpe2coreLength > 0)
                                {
                                    while ((!complete) && (i < xlpe2coreLength))
                                    {
                                        wirearea = xlpe2coreDB[i, 0];
                                        if (phase == "DC")
                                        {
                                            Rdc = xlpe2coreDB[i, 3];
                                            vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                            lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                        }
                                        else //AC
                                        {
                                            Rac = xlpe2coreDB[i, 1];
                                            X = xlpe2coreDB[i, 2];

                                            if (phase == "Single-Phase AC")
                                            {
                                                vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                                / (n * 1000 * voltage);

                                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                                    (X * Math.Sqrt(1 - pf * pf))) * 100);

                                                if ((loadtype == "Motor") && ConsiderVdStart)
                                                {
                                                    vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                    / (n * 1000 * voltage);
                                                }
                                                else
                                                {
                                                    vdstart = 0;
                                                }
                                            }
                                            else if (phase == "Three-Phase AC")
                                            {
                                                vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                                / (n * 1000 * voltage);

                                                lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                                    (X * Math.Sqrt(1 - pf * pf))) * 100);

                                                if ((loadtype == "Motor") && ConsiderVdStart)
                                                {
                                                    vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                    / (n * 1000 * voltage);
                                                }
                                                else
                                                {
                                                    vdstart = 0;
                                                }
                                            }
                                        }

                                        if (armour == "Non Armoured")
                                        {
                                            diameter = ODxlpe2core[i, 1];
                                        }
                                        else if (armour == "SWA")
                                        {
                                            diameter = ODxlpe2core[i, 2];
                                        }
                                        else if (armour == "DSTA")
                                        {
                                            diameter = ODxlpe2core[i, 3];
                                        }

                                        if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                                        {
                                            Irated = xlpe2coreDB[i, 4] * n;
                                        }
                                        else //above ground
                                        {
                                            Irated = xlpe2coreDB[i, 5] * n;
                                        }

                                        iderated = Irated * ktmain;

                                        cable_lte();

                                        // Validasi
                                        validasi();

                                        i++;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Selected cable data specification for 2 Core XLPE insulation doesn't exist!",
                                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    complete = true;
                                    inputValid = false;
                                }
                            }
                            else if (cores == 3)
                            {
                                if (xlpe3coreLength > 0)
                                {
                                    while ((!complete) && (i < xlpe3coreLength))
                                    {
                                        wirearea = xlpe3coreDB[i, 0];
                                        if (phase == "DC")
                                        {
                                            Rdc = xlpe3coreDB[i, 3];
                                            vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                            lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                        }
                                        else //AC
                                        {
                                            Rac = xlpe3coreDB[i, 1];
                                            X = xlpe3coreDB[i, 2];

                                            if (phase == "Single-Phase AC")
                                            {
                                                vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                                / (n * 1000 * voltage);

                                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                                    (X * Math.Sqrt(1 - pf * pf))) * 100);

                                                if ((loadtype == "Motor") && ConsiderVdStart)
                                                {
                                                    vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                    / (n * 1000 * voltage);
                                                }
                                                else
                                                {
                                                    vdstart = 0;
                                                }
                                            }
                                            else if (phase == "Three-Phase AC")
                                            {
                                                vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                                / (n * 1000 * voltage);

                                                lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                                    (X * Math.Sqrt(1 - pf * pf))) * 100);

                                                if ((loadtype == "Motor") && ConsiderVdStart)
                                                {
                                                    vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                    / (n * 1000 * voltage);
                                                }
                                                else
                                                {
                                                    vdstart = 0;
                                                }
                                            }
                                        }

                                        if (armour == "Non Armoured")
                                        {
                                            diameter = ODxlpe3core[i, 1];
                                        }
                                        else if (armour == "SWA")
                                        {
                                            diameter = ODxlpe3core[i, 2];
                                        }
                                        else if (armour == "DSTA")
                                        {
                                            diameter = ODxlpe3core[i, 3];
                                        }

                                        if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                                        {
                                            Irated = xlpe3coreDB[i, 4] * n;
                                        }
                                        else //above ground
                                        {
                                            Irated = xlpe3coreDB[i, 5] * n;
                                        }

                                        iderated = Irated * ktmain;
                                        cable_lte();

                                        // Validasi
                                        validasi();

                                        i++;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Selected cable data specification for 3 Core XLPE insulation doesn't exist!",
                                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    complete = true;
                                    inputValid = false;
                                }
                            }
                            else if (cores == 4)
                            {
                                if (xlpe4coreLength > 0)
                                {
                                    while ((!complete) && (i < xlpe4coreLength))
                                    {
                                        wirearea = xlpe4coreDB[i, 0];
                                        if (phase == "DC")
                                        {
                                            Rdc = xlpe4coreDB[i, 3];
                                            vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                            lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                        }
                                        else //AC
                                        {
                                            Rac = xlpe4coreDB[i, 1];
                                            X = xlpe4coreDB[i, 2];

                                            if (phase == "Single-Phase AC")
                                            {
                                                vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                                / (n * 1000 * voltage);

                                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                                    (X * Math.Sqrt(1 - pf * pf))) * 100);

                                                if ((loadtype == "Motor") && ConsiderVdStart)
                                                {
                                                    vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                    / (n * 1000 * voltage);
                                                }
                                                else
                                                {
                                                    vdstart = 0;
                                                }
                                            }
                                            else if (phase == "Three-Phase AC")
                                            {
                                                vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                                / (n * 1000 * voltage);

                                                lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                                    (X * Math.Sqrt(1 - pf * pf))) * 100);

                                                if ((loadtype == "Motor") && ConsiderVdStart)
                                                {
                                                    vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                    / (n * 1000 * voltage);
                                                }
                                                else
                                                {
                                                    vdstart = 0;
                                                }
                                            }
                                        }

                                        if (armour == "Non Armoured")
                                        {
                                            diameter = ODxlpe4core[i, 1];
                                        }
                                        else if (armour == "SWA")
                                        {
                                            diameter = ODxlpe4core[i, 2];
                                        }
                                        else if (armour == "DSTA")
                                        {
                                            diameter = ODxlpe4core[i, 3];
                                        }

                                        if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                                        {
                                            Irated = xlpe4coreDB[i, 4] * n;
                                        }
                                        else //above ground
                                        {
                                            Irated = xlpe4coreDB[i, 5] * n;
                                        }

                                        iderated = Irated * ktmain;
                                        cable_lte();

                                        // Validasi
                                        validasi();

                                        i++;
                                    }
                                }

                                else
                                {
                                    MessageBox.Show("Selected cable data specification for 4 Core XLPE insulation doesn't exist!",
                                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    complete = true;
                                    inputValid = false;
                                }
                            }
                        }
                        else if (insulation == "PVC")
                        {
                            if (cores == 2)
                            {
                                if (pvc2coreLength > 0)
                                {
                                    while ((!complete) && (i < pvc2coreLength))
                                    {
                                        wirearea = pvc2coreDB[i, 0];
                                        if (phase == "DC")
                                        {
                                            Rdc = pvc2coreDB[i, 3];
                                            vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                            lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                        }
                                        else //AC
                                        {
                                            Rac = pvc2coreDB[i, 1];
                                            X = pvc2coreDB[i, 2];

                                            if (phase == "Single-Phase AC")
                                            {
                                                vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                                / (n * 1000 * voltage);

                                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                                    (X * Math.Sqrt(1 - pf * pf))) * 100);

                                                if ((loadtype == "Motor") && ConsiderVdStart)
                                                {
                                                    vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                    / (n * 1000 * voltage);
                                                }
                                                else
                                                {
                                                    vdstart = 0;
                                                }
                                            }
                                            else if (phase == "Three-Phase AC")
                                            {
                                                vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                                / (n * 1000 * voltage);

                                                lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                                    (X * Math.Sqrt(1 - pf * pf))) * 100);

                                                if ((loadtype == "Motor") && ConsiderVdStart)
                                                {
                                                    vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                    / (n * 1000 * voltage);
                                                }
                                                else
                                                {
                                                    vdstart = 0;
                                                }
                                            }
                                        }

                                        if (armour == "Non Armoured")
                                        {
                                            diameter = ODpvc2core[i, 1];
                                        }
                                        else if (armour == "SWA")
                                        {
                                            diameter = ODpvc2core[i, 2];
                                        }
                                        else if (armour == "DSTA")
                                        {
                                            diameter = ODpvc2core[i, 3];
                                        }

                                        if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                                        {
                                            Irated = pvc2coreDB[i, 4] * n;
                                        }
                                        else //above ground
                                        {
                                            Irated = pvc2coreDB[i, 5] * n;
                                        }

                                        iderated = Irated * ktmain;
                                        cable_lte();

                                        // Validasi
                                        validasi();

                                        i++;
                                    }
                                }

                                else
                                {
                                    MessageBox.Show("Selected cable data specification for 2 Core PVC insulation doesn't exist!",
                                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    complete = true;
                                    inputValid = false;
                                }
                            }
                            else if (cores == 3)
                            {
                                if (pvc3coreLength > 0)
                                {
                                    while ((!complete) && (i < pvc3coreLength))
                                    {
                                        wirearea = pvc3coreDB[i, 0];
                                        if (phase == "DC")
                                        {
                                            Rdc = pvc3coreDB[i, 3];
                                            vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                            lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                        }
                                        else //AC
                                        {
                                            Rac = pvc3coreDB[i, 1];
                                            X = pvc3coreDB[i, 2];

                                            if (phase == "Single-Phase AC")
                                            {
                                                vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                                / (n * 1000 * voltage);

                                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                                    (X * Math.Sqrt(1 - pf * pf))) * 100);

                                                if ((loadtype == "Motor") && ConsiderVdStart)
                                                {
                                                    vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                    / (n * 1000 * voltage);
                                                }
                                                else
                                                {
                                                    vdstart = 0;
                                                }
                                            }
                                            else if (phase == "Three-Phase AC")
                                            {
                                                vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                                / (n * 1000 * voltage);

                                                lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                                    (X * Math.Sqrt(1 - pf * pf))) * 100);

                                                if ((loadtype == "Motor") && ConsiderVdStart)
                                                {
                                                    vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                    / (n * 1000 * voltage);
                                                }
                                                else
                                                {
                                                    vdstart = 0;
                                                }
                                            }
                                        }

                                        if (armour == "Non Armoured")
                                        {
                                            diameter = ODpvc3core[i, 1];
                                        }
                                        else if (armour == "SWA")
                                        {
                                            diameter = ODpvc3core[i, 2];
                                        }
                                        else if (armour == "DSTA")
                                        {
                                            diameter = ODpvc3core[i, 3];
                                        }

                                        if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                                        {
                                            Irated = pvc3coreDB[i, 4] * n;
                                        }
                                        else //above ground
                                        {
                                            Irated = pvc3coreDB[i, 5] * n;
                                        }

                                        iderated = Irated * ktmain;
                                        cable_lte();

                                        // Validasi
                                        validasi();

                                        i++;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Selected cable data specification for 3 Core PVC insulation doesn't exist!",
                                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    complete = true;
                                    inputValid = false;
                                }
                            }

                            else if (cores == 4)
                            {
                                if (pvc4coreLength > 0)
                                {
                                    while ((!complete) && (i < pvc4coreLength))
                                    {
                                        wirearea = pvc4coreDB[i, 0];
                                        if (phase == "DC")
                                        {
                                            Rdc = pvc4coreDB[i, 3];
                                            vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);

                                            lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                        }
                                        else //AC
                                        {
                                            Rac = pvc4coreDB[i, 1];
                                            X = pvc4coreDB[i, 2];

                                            if (phase == "Single-Phase AC")
                                            {
                                                vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                                / (n * 1000 * voltage);

                                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                                    (X * Math.Sqrt(1 - pf * pf))) * 100);

                                                if ((loadtype == "Motor") && ConsiderVdStart)
                                                {
                                                    vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                    / (n * 1000 * voltage);
                                                }
                                                else
                                                {
                                                    vdstart = 0;
                                                }
                                            }
                                            else if (phase == "Three-Phase AC")
                                            {
                                                vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                                / (n * 1000 * voltage);

                                                lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                                    (X * Math.Sqrt(1 - pf * pf))) * 100);


                                                if ((loadtype == "Motor") && ConsiderVdStart)
                                                {
                                                    vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                    / (n * 1000 * voltage);
                                                }
                                                else
                                                {
                                                    vdstart = 0;
                                                }
                                            }
                                        }

                                        if (armour == "Non Armoured")
                                        {
                                            diameter = ODpvc4core[i, 1];
                                        }
                                        else if (armour == "SWA")
                                        {
                                            diameter = ODpvc4core[i, 2];
                                        }
                                        else if (armour == "DSTA")
                                        {
                                            diameter = ODpvc4core[i, 3];
                                        }

                                        if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                                        {
                                            Irated = pvc4coreDB[i, 4] * n;
                                        }
                                        else //above ground
                                        {
                                            Irated = pvc4coreDB[i, 5] * n;
                                        }

                                        iderated = Irated * ktmain;
                                        cable_lte();

                                        // Validasi
                                        validasi();

                                        i++;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Selected cable data specification for 2 Core PVC insulation doesn't exist!",
                                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    complete = true;
                                    inputValid = false;
                                }
                            }
                        }
                        else if (insulation == "EPR")
                        {
                            MessageBox.Show("Vendor data cable specification for EPR insulation doesn't exist at the time, please insert cable specification data manually!",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            complete = true;
                        }
                        if (!complete)
                        {
                            solvableOrNPlus();
                        }
                    }
                    else if (radioButton3.Checked) //manual cable database input
                    {
                        while ((!complete) && (i < cableCount))
                        {
                            wirearea = inputCableData[i, 0];

                            if (phase == "DC")
                            {
                                Rdc = inputCableData[i, 2];
                                Rac = 0;

                                vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                            }
                            else
                            {
                                Rac = inputCableData[i, 2];
                                Rdc = 0;
                                X = inputCableData[i, 3];

                                if (phase == "Single-Phase AC")
                                {
                                    vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                    / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                        (X * Math.Sqrt(1 - pf * pf))) * 100);

                                    if ((loadtype == "Motor") && ConsiderVdStart)
                                    {
                                        vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                        / (n * 1000 * voltage);
                                    }
                                    else
                                    {
                                        vdstart = 0;
                                    }
                                }
                                else if (phase == "Three-Phase AC")
                                {
                                    vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                    / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                        (X * Math.Sqrt(1 - pf * pf))) * 100);

                                    if ((loadtype == "Motor") && ConsiderVdStart)
                                    {
                                        vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                        / (n * 1000 * voltage);
                                    }
                                    else
                                    {
                                        vdstart = 0;
                                    }
                                }
                            }

                            Irated = inputCableData[i, 4];
                            diameter = inputCableData[i, 5];

                            iderated = Irated * ktmain;
                            cable_lte();

                            // Validasi
                            validasi();

                            i++;
                        }
                        if (!complete)
                        {
                            solvableOrNPlus();
                        }
                    }

                }
                if (inputValid)
                {
                    calculated = true;
                    textBox12.Text = n.ToString();
                    textBox8.Text = vdrun.ToString("0.##");
                    textBox29.Text = lmax.ToString("0.##");

                    if (material == "Copper")
                    {
                        materialname = "Cu";
                    }

                    if ((loadtype == "Motor") && ConsiderVdStart)
                    {
                        textBox10.Text = vdstart.ToString("0.##");
                    }

                    readtemp = "";

                    readtemp += n.ToString() + "  ×  " + cores.ToString("0.##") + "/C  #  " + wirearea.ToString() +
                        " mm²    " + ratedvoltage + " / " + materialname + " / " + insulation;

                    if (armour != "Non Armoured")
                    {
                        readtemp += " / " + armour;
                    }

                    readtemp += " / " + outersheath;

                    tbResult.Text = readtemp;
                    cable_lte();
                    textBox22.Text = cLTE.ToString("0.##");
                    save_result();
                    SelectedDataLength();
                    Update_size();
                    enable_save();
                    if (phase == "DC")
                    {
                        textBox34.Text = Rdc.ToString("0.####");
                        textBox33.Text = "";
                    }
                    else //AC
                    {
                        textBox34.Text = Rac.ToString("0.####");
                        textBox33.Text = X.ToString("0.####");
                    }
                    textBox32.Text = Irated.ToString("0.##");

                    label86.Visible = true;
                    timer1.Enabled = true;


                    if (radioButton1.Checked)
                    {
                        label88.Text = "Since withstand energy level of cable is larger than the LTE of the \nprotection device," +
                            " therefore cable size of" + wirearea + " mm²  is acceptable";
                    }
                    else if (radioButton2.Checked)
                    {
                        label88.Text = "Since the minimum cable size due to S.C. is lower than the \nselected cable size," +
                            " therefore cable size of " + wirearea + " mm²  is acceptable";
                    }

                    label88.Visible = true;
                    timer3.Enabled = true;

                }
                else
                {
                    tbResult.Text = "Failed to get a suitable cable size";
                    disable_save();
                    i = m;
                }
            }
        }

        private void solvableOrNPlus_Vd()
        {
            if (n > 2147482)
            {
                MessageBox.Show("Failed to get a suitable cable: Maximum number of run exeeded (2147483)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                complete = true;
                inputValid = false;
            }
            else
            {
                n++;
            }
        }

        // show selected size + 2 size above
        private void Update_size()
        {
            textBox37.Text = wirearea.ToString();
            i--;
            comboBox15.Items.Clear();
            comboBox15.Items.Insert(0, "Update Size");
            if (radioButton4.Checked)
            {
                if (insulation == "XLPE")
                {
                    if (cores == 2)
                    {
                        comboBox15.Items.Insert(1, xlpe2coreDB[i, 0]);
                        if (i < currentDataLength - 1)
                        {
                            comboBox15.Items.Insert(2, xlpe2coreDB[i + 1, 0]);
                        }
                        if (i < currentDataLength - 2)
                        {
                            comboBox15.Items.Insert(3, xlpe2coreDB[i + 2, 0]);
                        }
                    }
                    else if (cores == 3)
                    {
                        comboBox15.Items.Insert(1, xlpe3coreDB[i, 0]);
                        if (i < currentDataLength - 1)
                        {
                            comboBox15.Items.Insert(2, xlpe3coreDB[i + 1, 0]);
                        }
                        if (i < currentDataLength - 2)
                        {
                            comboBox15.Items.Insert(3, xlpe3coreDB[i + 2, 0]);
                        }
                    }
                    else if (cores == 4)
                    {
                        comboBox15.Items.Insert(1, xlpe4coreDB[i, 0]);
                        if (i < currentDataLength - 1)
                        {
                            comboBox15.Items.Insert(2, xlpe4coreDB[i + 1, 0]);
                        }
                        if (i < currentDataLength - 2)
                        {
                            comboBox15.Items.Insert(3, xlpe4coreDB[i + 2, 0]);
                        }
                    }
                }
                else if (insulation == "PVC")
                {
                    if (cores == 2)
                    {
                        comboBox15.Items.Insert(1, pvc2coreDB[i, 0]);
                        if (i < currentDataLength - 1)
                        {
                            comboBox15.Items.Insert(2, pvc2coreDB[i + 1, 0]);
                        }
                        if (i < currentDataLength - 2)
                        {
                            comboBox15.Items.Insert(3, pvc2coreDB[i + 2, 0]);
                        }
                    }
                    else if (cores == 3)
                    {
                        comboBox15.Items.Insert(1, pvc3coreDB[i, 0]);
                        if (i < currentDataLength - 1)
                        {
                            comboBox15.Items.Insert(2, pvc3coreDB[i + 1, 0]);
                        }
                        if (i < currentDataLength - 2)
                        {
                            comboBox15.Items.Insert(3, pvc3coreDB[i + 2, 0]);
                        }
                    }
                    else if (cores == 4)
                    {
                        comboBox15.Items.Insert(1, pvc4coreDB[i, 0]);
                        if (i < currentDataLength - 1)
                        {
                            comboBox15.Items.Insert(2, pvc4coreDB[i + 1, 0]);
                        }
                        if (i < currentDataLength - 2)
                        {
                            comboBox15.Items.Insert(3, pvc4coreDB[i + 2, 0]);
                        }
                    }
                }
            }
            else if (radioButton3.Checked)
            {
                comboBox15.Items.Insert(1, inputCableData[i, 0]);
                if (i < cableCount - 1)
                {
                    comboBox15.Items.Insert(2, inputCableData[i + 1, 0]);

                }
                if (i < cableCount - 2)
                {
                    comboBox15.Items.Insert(3, inputCableData[i + 2, 0]);
                }
            }
            comboBox15.SelectedIndex = 0;
            m = i;
        }

        private void solvableOrNPlus()
        {
            if (breakcurrent < current)
            {
                MessageBox.Show("Failed to get a suitable cable size: Breaker current value must be greater than the full load current value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                complete = true;
                inputValid = false;
            }
            else if ((radioButton2.Enabled) && (smin > wirearea))
            {
                MessageBox.Show("Failed to get a suitable cable size: Minimum cable size exceeds the maximum available cable size!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                complete = true;
                inputValid = false;
            }
            else if ((radioButton1.Enabled) && (cLTE < bLTE))
            {
                MessageBox.Show("Failed to get a suitable cable size: Breaker LTE exceeds the maximum cable LTE!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                complete = true;
                inputValid = false;
            }
            else if (n > 2147482)
            {
                MessageBox.Show("Failed to get a suitable cable: Maximum number of run exeeded (214783)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                complete = true;
                inputValid = false;
            }
            else
            {
                n++;
            }
        }

        //validate i fl < i break < i derated, validate vd < vdmax, validate sizemin sc < selected size, validate cLTE < bLTE
        private void validasi()
        {
            if ((current < breakcurrent) && (breakcurrent < iderated) && (vdrun < vdrunmax) &&
                (((radioButton1.Checked) && (cLTE > bLTE)) || ((radioButton2.Checked) && (wirearea > smin))))
            {
                if ((loadtype == "Motor") && ConsiderVdStart)
                {
                    if (vdstart < vdstartmax)
                    {
                        complete = true;
                        inputValid = true;

                    }
                    else
                    {
                        complete = false;
                        inputValid = false;
                    }
                }
                else
                {
                    complete = true;
                    inputValid = true;
                }

            }
            else
            {
                complete = false;
                inputValid = false;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            area_calc();
        }

        private void TextBox5_Leave(object sender, EventArgs e)
        {
            calc_current();
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                voltage = double.Parse(textBox2.Text);
                panel7.BackColor = Color.Transparent;
            }
            else
            {
                voltage = 0;
                panel7.BackColor = Color.Red;
            }
            breaker_fill();
            if (!radioButton8.Checked)
            {
                calc_current();
            }
            else
            {
                calc_power();
            }
            DisabeUndoReset();
            enable_vd_btn();
            enable_result_btn();
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                pf = double.Parse(textBox4.Text);
                panel9.BackColor = Color.Transparent;
            }
            else
            {
                pf = 0;
                panel9.BackColor = Color.Red;
            }
            if (!radioButton8.Checked)
            {
                calc_current();
            }
            else
            {
                calc_power();
            }
            DisabeUndoReset();
            calc_current();
            enable_vd_btn();
            enable_result_btn();
        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text != "")
            {
                eff = double.Parse(textBox5.Text);
                panel8.BackColor = Color.Transparent;
            }
            else
            {
                eff = 0;
                panel8.BackColor = Color.Red;
            }

            if (!radioButton8.Checked)
            {
                calc_current();
            }
            else
            {
                calc_power();
            }
            DisabeUndoReset();
            enable_vd_btn();
            enable_result_btn();
        }

        private void TextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox4.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox14_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox14.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox5.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }
        private void TextBox14_TextChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Motor")
            {
                if ((textBox14.Text == "") && (textBox11.Text == ""))
                {
                    panel10.BackColor = Color.Transparent;
                    panel27.BackColor = Color.Transparent;
                    panel12.BackColor = Color.Transparent;
                    textBox25.Enabled = false;
                    textBox25.Text = "";
                    label59.Enabled = false;
                    label29.Enabled = false;
                    label28.Enabled = false;
                    textBox7.Enabled = false;
                    ConsiderVdStart = false;
                }
                else if ((textBox14.Text != "") && (textBox11.Text == "") && !ConsiderVdStart)
                {
                    panel10.BackColor = Color.Transparent;
                    panel27.BackColor = Color.Red;
                    panel12.BackColor = Color.Transparent;
                    label59.Enabled = true;
                    label29.Enabled = true;
                    label28.Enabled = true;
                    textBox7.Enabled = true;
                    textBox25.Enabled = true;
                    textBox25.Text = "1";
                    ConsiderVdStart = true;
                }
                else if (textBox14.Text == "")
                {
                    panel10.BackColor = Color.Red;
                }
                else if (textBox14.Text != "")
                {
                    panel10.BackColor = Color.Transparent;
                }
            }

            if (textBox14.Text != "")
            {
                pfstart = double.Parse(textBox14.Text);
            }
            else
            {
                pfstart = 0;
            }
            DisabeUndoReset();
            enable_vd_btn();
            enable_result_btn();

        }

        private void PfVDStartChange()
        {
            if (comboBox2.Text != "Motor")
            {
                panel10.BackColor = Color.Transparent;
                panel27.BackColor = Color.Transparent;
                panel12.BackColor = Color.Transparent;
                textBox25.Enabled = false;
                label59.Enabled = false;
                textBox25.Text = "";
                multiplier = 0;
            }
            else if ((textBox14.Text == "") && (textBox11.Text == ""))
            {
                panel10.BackColor = Color.Transparent;
                panel27.BackColor = Color.Transparent;
                panel12.BackColor = Color.Transparent;
                textBox25.Enabled = false;
                label59.Enabled = false;
                textBox25.Text = "";
                multiplier = 0;
            }
            else if ((textBox11.Text != "") && (textBox14.Text == ""))
            {
                panel10.BackColor = Color.Red;
                panel27.BackColor = Color.Transparent;
                textBox7.Enabled = true;
                label28.Enabled = true;
                label29.Enabled = true;
                panel12.BackColor = Color.Transparent;
                textBox25.Enabled = true;
                multiplier = 1;
                textBox25.Text = "1";
            }
            else if ((textBox14.Text != "") && (textBox11.Text == ""))
            {
                panel10.BackColor = Color.Transparent;
                panel27.BackColor = Color.Red;
                textBox7.Enabled = true;
                label28.Enabled = true;
                label29.Enabled = true;
                panel12.BackColor = Color.Transparent;
                textBox25.Enabled = true;
                multiplier = 1;
                textBox25.Text = "1";
            }
            else
            {
                panel10.BackColor = Color.Transparent;
                panel27.BackColor = Color.Transparent;
                textBox7.Enabled = true;
                label28.Enabled = true;
                label29.Enabled = true;
            }

        }

        private void TextBox14_Leave_1(object sender, EventArgs e)
        {
            pfstart = double.Parse(textBox14.Text);
        }

        private void TextBox23_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox23.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if ((radioButton1.Checked))
            {
                label53.Enabled = false;
                label54.Enabled = false;
                textBox23.Enabled = false;
                textBox28.Enabled = false;
                label63.Enabled = false;
                label64.Enabled = false;
                label67.Enabled = false;
                label68.Enabled = false;
                textBox30.Enabled = false;
                textBox20.ReadOnly = false;
                label48.Enabled = true;
                label49.Enabled = true;
                label69.Enabled = true;
                label70.Enabled = true;
                textBox20.Enabled = true;
                textBox22.Enabled = true;

                textBox28.Text = "";
                textBox23.Text = "";

                if (radioButton6.Checked)
                {
                    textBox20.ReadOnly = false;
                }
                else if (radioButton5.Checked)
                {
                    textBox20.ReadOnly = true;
                }
            }
            else if ((radioButton2.Checked))
            {
                label53.Enabled = true;
                label54.Enabled = true;
                textBox23.Enabled = true;
                textBox28.Enabled = true;
                label63.Enabled = true;
                label64.Enabled = true;
                label67.Enabled = true;
                label68.Enabled = true;
                textBox30.Enabled = true;
                textBox20.ReadOnly = true;
                label48.Enabled = false;
                label49.Enabled = false;
                label69.Enabled = false;
                label70.Enabled = false;
                textBox20.Enabled = false;
                textBox22.Enabled = false;
            }

            break_lte();
            enable_result_btn();
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if ((radioButton1.Checked))
            {
                label53.Enabled = false;
                label54.Enabled = false;
                textBox23.Enabled = false;
                textBox28.Enabled = false;
                label63.Enabled = false;
                label64.Enabled = false;
                label67.Enabled = false;
                label68.Enabled = false;
                textBox30.Enabled = false;
                textBox20.ReadOnly = false;
                label48.Enabled = true;
                label49.Enabled = true;
                label69.Enabled = true;
                label70.Enabled = true;
                textBox20.Enabled = true;
                textBox22.Enabled = true;
                textBox28.Text = "";
                textBox23.Text = "";

                if (radioButton6.Checked)
                {
                    textBox20.ReadOnly = false;
                }
                else if (radioButton5.Checked)
                {
                    textBox20.ReadOnly = true;
                }
            }
            else if ((radioButton2.Checked))
            {
                label53.Enabled = true;
                label54.Enabled = true;
                textBox23.Enabled = true;
                textBox28.Enabled = true;
                label63.Enabled = true;
                label64.Enabled = true;
                label67.Enabled = true;
                label68.Enabled = true;
                textBox30.Enabled = true;
                textBox20.ReadOnly = true;
                label48.Enabled = false;
                label49.Enabled = false;
                label69.Enabled = false;
                label70.Enabled = false;
                textBox20.Enabled = false;
                textBox22.Enabled = false;
            }

            break_lte();
            enable_result_btn();
        }

        private void ComboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            breakcurrent = double.Parse(comboBox11.Text);
            SCLTEchanged();
            break_lte();
            enable_result_btn();
        }

        private void ComboBox11_TextChanged(object sender, EventArgs e)
        {
            if (comboBox11.Text != "")
            {
                breakcurrent = double.Parse(comboBox11.Text);
            }
            else
            {
                breakcurrent = 0;
            }
            SCLTEchanged();
            break_lte();
            enable_result_btn();
        }

        private void ComboBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((comboBox11.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as ComboBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox13_Leave(object sender, EventArgs e)
        {
            tagno = textBox13.Text;
        }


        private void ComboBox10_TextChanged(object sender, EventArgs e)
        {
            if (comboBox10.Text != "")
            {
                scrating = double.Parse(comboBox10.Text);
            }
            else
            {
                scrating = 0;
            }
            SCLTEchanged();
            breaker_fill();
            enable_result_btn();
        }

        private void ComboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox10.Text != "")
            {
                scrating = double.Parse(comboBox10.Text);
            }
            else
            {
                scrating = 0;
            }
            SCLTEchanged();
            breaker_fill();
            enable_result_btn();
        }

        private void TextBox16_TextChanged(object sender, EventArgs e)
        {
            breaker_fill();
        }

        private void TextBox23_TextChanged(object sender, EventArgs e)
        {
            if (textBox23.Text != "")
            {
                tbreaker = double.Parse(textBox23.Text);
            }
            else
            {
                tbreaker = 0;
            }
            SCLTEchanged();
            calc_smin();
            break_lte();
            enable_result_btn();
        }

        private void TextBox9_TextChanged(object sender, EventArgs e)
        {
            if (textBox9.Text != "")
            {
                vdrunmax = double.Parse(textBox9.Text);
                panel26.BackColor = Color.Transparent;
            }
            else
            {
                vdrunmax = 0;
                panel26.BackColor = Color.Red;
            }
            DisabeUndoReset();
            enable_vd_btn();
            enable_result_btn();
        }


        private void ComboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            outersheath = comboBox8.Text;
            if (comboBox8.Text != "")
            {
                panel23.BackColor = Color.Transparent;
            }
            else
            {
                panel23.BackColor = Color.Red;
            }
            DisabeUndoReset();
            enable_vd_btn();
            enable_result_btn();
        }

        private void ResetData()
        {
            LoadIECDatabase();

            disable_save();

            calculated = false;
            textBox13.Text = "";
            textBox26.Text = "";
            textBox27.Text = "";
            TextBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox5.Text = "1"; //default efficiency = 1
            eff = 1;
            textBox25.Text = "";
            textBox14.Text = "";
            textBox9.Text = "";
            textBox11.Text = "";
            textBox12.Text = "1"; //no of run default = 1
            textBox12.ReadOnly = false;
            textBox6.Text = "";
            textBox15.Text = "";
            textBox24.Text = "";
            textBox17.Text = "";
            textBox36.Text = "";
            textBox18.Text = "";
            textBox19.Text = "";
            textBox23.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox20.Text = "";
            textBox21.Text = "";
            textBox22.Text = "";
            tbResult.Text = "";
            textBox10.Text = "";
            textBox28.Text = "";
            textBox29.Text = "";
            textBox30.Text = "";
            textBox16.Text = "";
            textBox31.Text = "";
            textBox32.Text = "";
            textBox33.Text = "";
            textBox34.Text = "";
            textBox35.Text = "";
            textBox37.Text = "";



            cbPower.SelectedIndex = 0;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            comboBox5.Enabled = true;
            comboBox6.SelectedIndex = -1;
            comboBox7.SelectedIndex = -1;
            comboBox8.SelectedIndex = -1;
            comboBox9.SelectedIndex = -1;
            comboBox10.SelectedIndex = -1;
            comboBox10.Text = "";
            comboBox11.SelectedIndex = -1;
            comboBox12.SelectedIndex = -1;
            comboBox11.Text = "";
            comboBox14.SelectedIndex = -1;
            comboBox13.SelectedIndex = -1;
            comboBox15.SelectedIndex = -1;
            comboBox15.Items.Clear();
            panel4.Enabled = false;
            panel5.Enabled = true;
            panel6.Enabled = true;
            panel30.BackColor = Color.Red;

            ConsiderVdStart = false;
        }

        private void TextBox25_TextChanged(object sender, EventArgs e)
        {
            if ((textBox25.Text == "") && ((textBox14.Text != "") || (textBox11.Text != "")))
            {
                panel12.BackColor = Color.Red;
            }
            else
            {
                panel12.BackColor = Color.Transparent; 
            }


            if (textBox25.Text != "")
            {
                multiplier = double.Parse(textBox25.Text);
            }
            else
            {
                multiplier = 0;
            }

            if (radioButton8.Checked)
            {
                calc_currentstart();
            }
            else
            {
                calc_current();
            }
            DisabeUndoReset();
            enable_vd_btn();
            enable_result_btn();
        }

        private void TextBox25_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox25.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox28_TextChanged(object sender, EventArgs e)
        {
            if (textBox28.Text != "")
            {
                sccurrent = double.Parse(textBox28.Text);
            }
            else
            {
                sccurrent = 0;
            }
            SCLTEchanged();
            calc_smin();
            break_lte();
            enable_result_btn();
        }

        //disable add when SC / LTE data changed after being calculated
        private void SCLTEchanged()
        {
            if (calculated)
            {
                disable_save();
            }
        }


        private void TextBox28_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox28.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox17_TextChanged(object sender, EventArgs e)
        {
            if (textBox17.Text != "")
            {
                k1main = double.Parse(textBox17.Text);
            }
            else
            {
                k1main = 0;
            }

            if ((textBox17.Text != "") && (textBox18.Text != ""))
            {
                textBox19.Location = new Point(kttextboxX, kttextboxY);
                label43.Location = new Point(ktlabelX, ktlabelY);
                panel24.Location = new Point(textBox19.Location.X - 4, textBox19.Location.Y);
            }
            else if ((textBox17.Text != "") && (radioButton7.Checked) && (k1main != 0))
            {
                label43.Location = label80.Location;
                textBox19.Location = textBox36.Location;

                panel24.Location = new Point(textBox36.Location.X - 4, textBox36.Location.Y);

                textBox18.Visible = true;
                label42.Visible = true;
            }
            else if ((k1main == 0) && (radioButton7.Checked))
            {
                k1main = 0;
                k2main = 0;
                k3main = 0;
                ktmain = 0;

                textBox18.Text = "";
                textBox36.Text = "";
                textBox19.Text = "";

                label43.Location = label42.Location;
                textBox19.Location = textBox18.Location;
                panel24.Location = new Point(textBox18.Location.X - 4, textBox18.Location.Y);

                textBox18.Visible = false;
                label42.Visible = false;
            }
            DisabeUndoReset();
            Updatekt();
            enable_result_btn();
        }

        private void TextBox18_TextChanged(object sender, EventArgs e)
        {
            if ((textBox18.Text != "") && (radioButton7.Checked))
            {
                k2main = double.Parse(textBox18.Text);
                textBox19.Location = new Point(kttextboxX, kttextboxY);
                label43.Location = new Point(ktlabelX, ktlabelY);

                textBox36.Visible = true;
                label80.Visible = true;

            }
            else if ((textBox18.Text == "") && (radioButton7.Checked))
            {
                k2main = 0;
                k3main = 0;

                textBox36.Text = "";

                label43.Location = label80.Location;
                textBox19.Location = textBox36.Location;

                textBox36.Visible = false;
                label80.Visible = false;


            }
            DisabeUndoReset();
            Updatekt();
            enable_result_btn();
        }

        private void TextBox19_TextChanged(object sender, EventArgs e)
        {
            if (textBox19.Text != "")
            {
                panel24.BackColor = Color.Transparent;
            }
            else
            {
                panel24.BackColor = Color.Red;
            }
            DisabeUndoReset();
            enable_vd_btn();
            enable_result_btn();
        }

        private void Updatekt()
        {
            if (radioButton7.Checked)
            {
                if ((textBox17.Text != "") && (textBox18.Text != "") && (textBox36.Text != ""))
                {
                    ktmain = k1main * k2main * k3main;
                }
                else if ((textBox17.Text != "") && (textBox18.Text != "") && (textBox36.Text == ""))
                {
                    ktmain = k1main * k2main;
                }
                else if ((textBox17.Text != "") && (textBox18.Text == "") && (textBox36.Text == ""))
                {
                    ktmain = k1main;
                }
                else
                {
                    textBox19.Text = "";
                }

                if (ktmain != 0)
                {
                    textBox19.Text = ktmain.ToString("0.##");
                }

            }
        }

        private void TextBox26_TextChanged(object sender, EventArgs e)
        {
            from = textBox26.Text;
            DisabeUndoReset();
        }

        private void ComboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            breakertype = comboBox12.Text;
            SCLTEchanged();
            breaker_fill();
            break_lte();
            enable_result_btn();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            AddToResult();
        }

        private void AddToResult()
        {
            if (!EditingState)
            {
                f5.dataGridView1.RowCount++;
                j++;
                f5.dataGridView1.Rows[j].Cells[0].Value = j + 1;

                for (int k = 0; k < 39; k++)
                {
                    f5.dataGridView1.Rows[j].Cells[k + 1].Value = results[k];
                }

                DataRow dtrTemp = dtdiameter.NewRow();
                dtrTemp.ItemArray = dtr.ItemArray.Clone() as object[];
                dtdiameter.Rows.Add(dtrTemp);

                OpenDataTable();

                f5.Update_summary();
            }
            else
            {

                for (int k = 0; k < 39; k++)
                {
                    f5.dataGridView1.Rows[tempCurrentRow].Cells[k + 1].Value = results[k];
                }
                EditingState = false;
                f5.Enabled = true;
                dtdiameter.Rows.RemoveAt(tempCurrentRow);
                dtdiameter.Rows.InsertAt(dtr, tempCurrentRow);
                OpenDataTable();
                f5.Update_summary();
                button4.Text = "Add to Result";
                addToolStripMenuItem.Text = "Add to Result";
                button3.Text = "Add and Save";
                button5.Text = "Open Calculation Result";

            }
        }

        private void TextBox16_TextChanged_1(object sender, EventArgs e)
        {
            fromdesc = textBox16.Text;
            DisabeUndoReset();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            OpenForm.formMainClose = true;
            if (OpenForm.ReturnToTitle) //return to title menu is clicked --> return to title form (OpenForm)
            {
                f5.Close();
            }
            else //exit/close button clicked --> close Entire App
            {
                Application.Exit();
            }

            if (Form5.cancelexit)
            {
                e.Cancel = true;
                Form5.cancelexit = false;
                OpenForm.ReturnToTitle = false;
            }
            else
            {
                Properties.Settings.Default.Save();
                if (EditingState)
                {
                    f5.Enabled = true;
                }
            }
            OpenForm.formMainClose = false;
        }

        private void TextBox31_TextChanged(object sender, EventArgs e)
        {
            todesc = textBox31.Text;
            DisabeUndoReset();
        }

        private void TextBox30_TextChanged(object sender, EventArgs e)
        {
            if (textBox30.Text != "")
            {
                smin = double.Parse(textBox30.Text);
            }
            else
            {
                smin = 0;
            }
        }

        private void Button5_Click_1(object sender, EventArgs e)
        {
            if (!EditingState)
            {
                OpenDataTable();
            }
            else
            {
                EditingState = false;
                f5.Enabled = true;
                OpenDataTable();
                button4.Text = "Add to Result";
                addToolStripMenuItem.Text = "Add to Result";
                button3.Text = "Add and Save";
                button5.Text = "Open Calculation Result";
            }
        }

        private void OpenDataTable()
        {
            f5.editRow.Click += EditRowClicked;
            f5.editRowDataToolStripMenuItem.Click += EditRowClicked;
            if (!f5.Visible)
            {
                f5.Show();
            }
            else if (f5.WindowState == FormWindowState.Minimized)
            {
                f5.WindowState = FormWindowState.Normal;
            }
            else
            {
                f5.BringToFront();
            }
        }

        private void EditRowClicked(object sender, EventArgs e)
        {
            tempCurrentRow = Form5.currentrow;
            f5.Enabled = false;
            EditingState = true;
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }
            else
            {
                BringToFront();
            }


            DataRow dtx = dtdiameter.Rows[Form5.currentrow];
            bool noDatabase = false;
            if (Convert.ToString(dtx[32]) == "vendor")
            {
                radioButton4.Checked = true;
                if (comboBoxVendor.Items.Contains(Convert.ToString(dtx[55])))
                {
                    comboBoxVendor.SelectedItem = Convert.ToString(dtx[55]);
                }
                else
                {
                    MessageBox.Show("Error: Cable Database \"" + Convert.ToString(dtx[55]) + "\" not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    EditingState = false;
                    f5.Enabled = true;

                    if (!f5.Visible)
                    {
                        f5.Show();
                    }
                    else if (f5.WindowState == FormWindowState.Minimized)
                    {
                        f5.WindowState = FormWindowState.Normal;
                    }
                    else
                    {
                        f5.BringToFront();
                    }

                    button4.Text = "Add to Result";
                    addToolStripMenuItem.Text = "Add to Result";
                    button3.Text = "Add and Save";
                    button5.Text = "Open Calculation Result";
                    EditingState = false;
                    noDatabase = true;
                }
            }
            else if (Convert.ToString(dtx[32]) == "manual")
            {
                radioButton3.Checked = true;
                comboBoxVendor.SelectedIndex = -1;
            }

            if (!noDatabase)
            {
                textBox13.Text = Convert.ToString(dtx[1]); //tagno
                textBox26.Text = Convert.ToString(dtx[2]); //from
                textBox16.Text = Convert.ToString(dtx[3]); //fromdesc
                textBox27.Text = Convert.ToString(dtx[4]); //to
                textBox31.Text = Convert.ToString(dtx[5]); //todesc
                comboBox3.Text = Convert.ToString(dtx[6]); //phase
                comboBox2.Text = Convert.ToString(dtx[7]); //loadtype
                comboBox13.Text = Convert.ToString(dtx[8]); //voltage system
                if (Convert.ToString(dtx[9]) == "True")
                {
                    radioButton8.Checked = true;
                }
                else
                {
                    radioButton8.Checked = false;
                }
                cbPower.Text = Convert.ToString(dtx[10]); // power unit
                TextBox1.Text = DtrToDoubleText(dtx, 11); //Power
                current = Convert.ToDouble(dtx[12]);
                textBox3.Text = current.ToString("0.##"); // FL Current
                textBox2.Text = DtrToDoubleText(dtx, 13); //Voltage
                textBox5.Text = DtrToDoubleText(dtx, 14); //eff
                textBox4.Text = DtrToDoubleText(dtx, 15); //pf
                textBox14.Text = DtrToDoubleText(dtx, 16); //pfstart
                textBox25.Text = DtrToDoubleText(dtx, 17); //multiplier
                comboBox14.Text = DtrToDoubleText(dtx, 18);//ratedvoltage
                comboBox4.Text = Convert.ToString(dtx[19]);//material
                comboBox6.Text = Convert.ToString(dtx[20]); //insulation
                comboBox7.Text = Convert.ToString(dtx[21]); //armour
                comboBox8.Text = Convert.ToString(dtx[22]); //outer Sheath
                comboBox9.Text = Convert.ToString(dtx[23]); //installation
                if (Convert.ToString(dtx[24]) == "True") //manual input correction factor
                {
                    radioButton7.Checked = true;
                }
                else
                {
                    radioButton7.Checked = false;
                }
                textBox17.Text = DtrToDoubleText(dtx, 25); //k1main
                textBox18.Text = DtrToDoubleText(dtx, 26); //k2main
                textBox36.Text = DtrToDoubleText(dtx, 27); //k3main
                textBox19.Text = DtrToDoubleText(dtx, 28); //ktmain
                ktmain = Convert.ToDouble(dtx[28]);
                textBox6.Text = DtrToDoubleText(dtx, 29); //length
                textBox9.Text = DtrToDoubleText(dtx, 30); //vdrunmax
                textBox11.Text = DtrToDoubleText(dtx, 31); //vdstartmax
                vdrun = Convert.ToDouble(dtx[33]);
                textBox8.Text = vdrun.ToString("0.##"); //vdrun
                if (DtrToDoubleText(dtx,34) != "")
                {
                    vdstart = Convert.ToDouble(dtx[34]);
                    textBox10.Text = vdstart.ToString("0.##"); //vdstart
                }
                else
                {
                    textBox10.Text = "";
                }
                lmax = Convert.ToDouble(dtx[35]);
                textBox29.Text = lmax.ToString("0.##");  //lmax
                textBox12.Text = Convert.ToString(dtx[36]); //no of run
                comboBox5.Text = Convert.ToString(dtx[37]); //no of cores
                textBox37.Text = DtrToDoubleText(dtx, 38); //wirearea
                wirearea = Convert.ToDouble(dtx[38]);
                if (Convert.ToString(dtx[6]) == "DC")
                {
                    Rdc = Convert.ToDouble(dtx[39]);
                    textBox34.Text = Rac.ToString("0.####"); //Rdc
                }
                else
                {
                    Rac = Convert.ToDouble(dtx[39]);
                    textBox34.Text = Rac.ToString("0.####"); //Rac
                }
                X = Convert.ToDouble(dtx[40]);
                textBox33.Text = X.ToString("0.####"); //X
                textBox32.Text = Convert.ToString(dtx[41]); //irated
                Irated = Convert.ToDouble(dtx[41]);
                iderated = Convert.ToDouble(dtx[42]); //iderated
                if (Convert.ToString(dtx[43]) == "sc")
                {
                    radioButton2.Checked = true;
                }
                else if (Convert.ToString(dtx[43]) == "lte")
                {
                    radioButton1.Checked = true;
                }
                textBox28.Text = DtrToDoubleText(dtx, 44); //sccurrent
                textBox23.Text = DtrToDoubleText(dtx, 45); //tbreak
                textBox20.Text = DtrToDoubleText(dtx, 53); //blte
                textBox15.Text = DtrToDoubleText(dtx, 46); //initialtemp
                textBox24.Text = DtrToDoubleText(dtx, 47); //finaltemp
                cLTE = Convert.ToDouble(dtx[48]);
                textBox22.Text = cLTE.ToString("0.##"); //CLTE
                if (Convert.ToString(dtx[49]) != "")
                {
                    comboBox12.Text = Convert.ToString(dtx[49]); //protection type
                    if (Convert.ToString(dtx[50]) == "vendor")
                    {
                        radioButton5.Checked = true;
                    }
                    else if (Convert.ToString(dtx[50]) == "manual")
                    {
                        radioButton6.Checked = true;
                    }
                    comboBox10.Text = DtrToDoubleText(dtx, 51); //breaker rating
                    comboBox11.Text = DtrToDoubleText(dtx, 52); //nominal current
                }
                else
                {
                    comboBox12.SelectedIndex = -1; //protection type
                    if (Convert.ToString(dtx[50]) == "vendor")
                    {
                        radioButton5.Checked = true;
                    }
                    else if (Convert.ToString(dtx[50]) == "manual")
                    {
                        radioButton6.Checked = true;
                    }
                    comboBox10.Text = DtrToDoubleText(dtx, 51); //breaker rating
                    comboBox10.SelectedIndex = -1;
                    comboBox11.Text = DtrToDoubleText(dtx, 52); //nominal current
                    comboBox11.SelectedIndex = -1;
                }
                textBox35.Text = Convert.ToString(f5.dataGridView1.Rows[Form5.currentrow].Cells[39].Value); //remarks
                tbResult.Text = Convert.ToString(f5.dataGridView1.Rows[Form5.currentrow].Cells[38].Value);
                i = Convert.ToInt32(dtx[54]);
                Update_size();

                if ((textBox11.Text != "") || (textBox14.Text != ""))
                {
                    ConsiderVdStart = true;
                }
                else
                {
                    ConsiderVdStart = false;
                }

                if (comboBox11.Text == "") //data being edited is vd calculated only
                {
                    button2.Enabled = false;
                }
                else //data being edited is complete with SC/LTE consideration
                {
                    button2.Enabled = false;
                }
                //Disable all inputs before vd calculation
                panel4.Enabled = true;
                panel5.Enabled = false;
                panel6.Enabled = false;

                //enable vd calculate & edit data buttons
                button7.Enabled = true;
                button8.Enabled = true;


                button4.Text = "Confirm Edit";
                addToolStripMenuItem.Text = "Confirm Edit";
                button3.Text = "Confirm Edit and Save";
                disable_save();
                enable_result_btn();
                button5.Text = "Cancel";
            }
        }

        private string DtrToDoubleText(DataRow datRow, int num)
        {
            double Number;
            if (!(Convert.ToString(datRow[num]) == "0") && (double.TryParse(Convert.ToString(datRow[num]), out Number)))
            {
                return Convert.ToString(datRow[num]);
            }
            else
            {
                return "";
            }
        }


        private void RadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                if (voltageLv == "MV")
                {
                    MessageBox.Show("Vendor data for Medium Voltage (MV) cable is not available, please input cable data manually.",
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    radioButton3.Checked = true;
                }
                else
                {
                    button6.Enabled = false;
                    label78.Enabled = false;
                    button6.Visible = false;
                    label78.Visible = false;

                    comboBoxVendor.Visible = true;
                    labelVendor.Visible = true;
                }
            }
            else
            {
                button6.Enabled = true;
                label78.Enabled = true;
                button6.Visible = true;
                label78.Visible = true;

                comboBoxVendor.Visible = false;
                labelVendor.Visible = false;
            }
            DisabeUndoReset();
            enable_vd_btn();
            enable_result_btn();
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                button6.Enabled = true;
                label78.Enabled = true;
                button6.Visible = true;
                label78.Visible = true;

                comboBoxVendor.Visible = false;
                labelVendor.Visible = false;
            }
            else
            {
                button6.Enabled = false;
                label78.Enabled = false;
                button6.Visible = false;
                label78.Visible = false;

                comboBoxVendor.Visible = true;
                labelVendor.Visible = true;
            }
            DisabeUndoReset();
            enable_vd_btn();
            enable_result_btn();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            f6.FormClosed += F6_FormClosed;
            f6.ShowDialog();

        }
        private void F6_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Form6.okclicked)
            {
                enable_result_btn();
                inputCableData = new double[cableCount, 6];
                for (int i = 0; i < cableCount; i++)
                {
                    for (int q = 0; q < 6; q++)
                    {
                        inputCableData[i, q] = Form6.confirmedcabledata[i, q];
                    }
                }
            }
        }


        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fSettings = new FSettings();
            fSettings.FormClosed += fSettings_FormClosed;
            fSettings.ShowDialog();
        }
        private void fSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (okSetClicked)
            {
                //decimalseparator update
                if (decimalSeparatorChanged && okSetClicked)
                {
                    refreshInputData();
                    refreshDataTable();
                    decimalSeparatorChanged = false;
                }
                //colortheme update
                LoadColor();
                SaveAllColor();
            }
            okSetClicked = false;
        }

        //change decimal separator of data in data table based on the new selected decimal separator
        private void refreshDataTable()
        {
            for (int i = 0; i < j + 1; i++)
            {
                for (int k = 0; k < 39; k++)
                {
                    if (((k == 8) || ((k >= 10) && (k <= 16)) || ((k >= 18) && (k <= 24)) ||
                       ((k >= 26) && (k <= 37))))
                    {
                        if (decimalseparator == '.')
                        {
                            f5.dataGridView1.Rows[i].Cells[k].Value = Convert.ToString(f5.dataGridView1.Rows[i].Cells[k].Value).Replace(',', '.');
                        }
                        else //decimalseparator == ','
                        {
                            f5.dataGridView1.Rows[i].Cells[k].Value = Convert.ToString(f5.dataGridView1.Rows[i].Cells[k].Value).Replace('.', ',');
                        }
                    }
                }
            }
            //NOTE: ALL '.' and ',' still got changed, TODO: Change decimal separator replacing to only decimal data
            foreach (DataRow row in dtdiameter.Rows)
            {
                if (decimalseparator == ',')
                {
                    for (int i = 0; i < Form1.dtdiameter.Columns.Count; i++)
                    {
                        row[i] = Convert.ToString(row[i]).Replace('.', ',');
                    }
                }
                else
                {
                    for (int i = 0; i < Form1.dtdiameter.Columns.Count; i++)
                    {
                        row[i] = Convert.ToString(row[i]).Replace(',', '.');
                    }
                }
            }
            f5.Update_summary();
        }

        //change decimal separator of data input to the new selected decimal separator
        private void refreshInputData()
        {
            if (power != 0)
            {
                TextBox1.Text = power.ToString();
            }
            if (voltage != 0)
            {
                textBox2.Text = voltage.ToString();
            }
            if (multiplier != 0)
            {
                textBox25.Text = multiplier.ToString();
            }
            if ((current != 0) && (radioButton8.Checked))
            {
                textBox3.Text = current.ToString();
            }
            if (eff != 0)
            {
                textBox5.Text = eff.ToString();
            }
            if (pf != 0)
            {
                textBox4.Text = pf.ToString();
            }
            if (pfstart != 0)
            {
                textBox14.Text = pfstart.ToString();
            }
            if (k1main != 0)
            {
                textBox17.Text = k1main.ToString();
            }
            if (k2main != 0)
            {
                textBox18.Text = k2main.ToString();
            }
            if (k3main != 0)
            {
                textBox36.Text = k3main.ToString();
            }
            if (ktmain != 0)
            {
                textBox19.Text = ktmain.ToString("0.##");
            }
            if (sccurrent != 0)
            {
                textBox28.Text = sccurrent.ToString();
            }
            if (tbreaker != 0)
            {
                textBox23.Text = tbreaker.ToString();
            }
            if (smin != 0)
            {
                textBox30.Text = smin.ToString("0.##");
            }
            if (vdrun != 0)
            {
                textBox8.Text = vdrun.ToString("0.##");
            }
            if (vdrunmax != 0)
            {
                textBox9.Text = vdrunmax.ToString();
            }
            if (vdstart != 0)
            {
                textBox10.Text = vdstart.ToString("0.##");
            }
            if (vdstartmax != 0)
            {
                textBox11.Text = vdstartmax.ToString();
            }
            if (length != 0)
            {
                textBox6.Text = length.ToString();
            }
            if (lmax != 0)
            {
                textBox29.Text = lmax.ToString("0.##");
            }
            if (initialTemp != 0)
            {
                textBox15.Text = initialTemp.ToString();
            }
            if (finalTemp != 0)
            {
                textBox24.Text = finalTemp.ToString();
            }
            if (k != 0)
            {
                textBox21.Text = k.ToString("0.###");
            }
            if (Rac != 0)
            {
                textBox34.Text = Rac.ToString("0.####");
            }
            else if (Rdc != 0)
            {
                textBox34.Text = Rdc.ToString("0.####");
            }
            if (X != 0)
            {
                textBox33.Text = X.ToString("0.####");
            }
            if (Irated != 0)
            {
                textBox32.Text = Irated.ToString("0.##");
            }
            if (bLTE != 0)
            {
                textBox20.Text = bLTE.ToString();
            }
            if (cLTE != 0)
            {
                textBox22.Text = cLTE.ToString("0.##");
            }
            if (breakcurrent != 0)
            {
                comboBox11.Text = breakcurrent.ToString();
            }

        }

        private void TextBox35_TextChanged(object sender, EventArgs e)
        {
            remarks = textBox35.Text;
            if (remarks != "")
            {
                results[38] = remarks;
            }
            else
            {
                results[38] = "N/A";
            }

        }

        private void TextBox20_TextChanged(object sender, EventArgs e)
        {
            if (textBox20.Text != "")
            {
                bLTE = double.Parse(textBox20.Text);
            }
            else
            {
                bLTE = 0;
            }
            SCLTEchanged();
            enable_result_btn();
        }

        private void TextBox20_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox20.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void ComboBox13_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox13.Text != "")
            {
                volSys = comboBox13.Text;
                panel15.BackColor = Color.Transparent;
            }
            else
            {
                panel15.BackColor = Color.Red;
            }
            voltageLv = comboBox13.Text;
            comboBox14.Items.Clear();
            if (comboBox13.Text == "MV")
            {
                comboBox14.Items.Insert(0, "3.6/6kV");
                comboBox14.Items.Insert(1, "6/10kV");
                comboBox14.Items.Insert(2, "8.7/15kV");
                comboBox14.Items.Insert(3, "12/20kV");
                comboBox14.Items.Insert(4, "18/30kV");

                toolTip1.SetToolTip(comboBox14, null);
            }
            else if (comboBox13.Text == "LV")
            {
                comboBox14.Items.Insert(0, "0.6/1kV");
                comboBox14.Text = "0.6/1kV";

                toolTip1.SetToolTip(comboBox14, null);
            }
            else
            {
                panel15.BackColor = Color.Red;
                toolTip1.SetToolTip(comboBox14, "Voltage system needs to be chosen first");
            }
            if ((radioButton4.Checked) && (voltageLv == "MV"))
            {
                MessageBox.Show("Vendor data for Medium Voltage (MV) cable is not available, please input cable data manually.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                radioButton3.Checked = true;
            }
            enable_vd_btn();
            enable_result_btn();
        }

        private void ComboBox14_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox14.Text != "")
            {
                panel16.BackColor = Color.Transparent;
            }
            else
            {
                panel16.BackColor = Color.Red;
            }
            ratedvoltage = comboBox14.Text;
            DisabeUndoReset();
            enable_vd_btn();
            enable_result_btn();
        }

        private void TextBox10_TextChanged(object sender, EventArgs e)
        {
            if (textBox10.Text != "")
            {
                vdstart = double.Parse(textBox10.Text);
            }
        }

        private void TextBox8_TextChanged_1(object sender, EventArgs e)
        {
            if (textBox8.Text != "")
            {
                vdrun = double.Parse(textBox8.Text);
            }
        }

        private void RadioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                comboBox11.DropDownStyle = ComboBoxStyle.DropDownList;
                textBox20.ReadOnly = true;
            }
            else if (radioButton6.Checked)
            {
                comboBox11.DropDownStyle = ComboBoxStyle.DropDown;
                textBox20.ReadOnly = false;
            }
        }

        private void RadioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                comboBox11.DropDownStyle = ComboBoxStyle.DropDownList;
                textBox20.ReadOnly = true;
                comboBox10.DropDownStyle = ComboBoxStyle.DropDownList;

            }
            else if (radioButton6.Checked)
            {
                comboBox11.DropDownStyle = ComboBoxStyle.DropDown;
                textBox20.ReadOnly = false;
                comboBox10.DropDownStyle = ComboBoxStyle.DropDown;
            }
            comboBox10.Text = "";
            comboBox10.SelectedIndex = -1;
            breakcurrent = 0;
            scrating = 0;
            comboBox11.Text = "";
            comboBox11.SelectedIndex = -1;
        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        

        private void TextBox24_TextChanged(object sender, EventArgs e)
        {
            if (textBox24.Text != "")
            {
                finalTemp = double.Parse(textBox24.Text);
            }
            else
            {
                finalTemp = 0;
            }
            SCLTEchanged();
            Calc_k();
            enable_result_btn();

        }

        private void Button7_Click(object sender, EventArgs e)
        {
            calculated = false;
            if ((voltage == 0) || (eff == 0) || (length == 0) || ((power == 0) && (!radioButton8.Checked)) || (current == 0) ||
                ((comboBox2.Text == "Motor") && ConsiderVdStart && (multiplier == 0)) || (pf > 1) || (pfstart > 1) || 
                (vdrunmax > 100) || (vdrunmax <= 0) || ((comboBox2.Text == "Motor") && ConsiderVdStart && ((vdstartmax > 100) || (vdstartmax <= 0)))) 
            {
                string msgbox;
                msgbox = "Invalid value on following input: ";
                if (voltage == 0)
                {
                    msgbox += "\n- Voltage: voltage input can't be 0";
                }
                if (power == 0)
                {
                    msgbox += "\n- Power";
                }
                if (eff == 0)
                {
                    msgbox += "\n- Efficiency";
                }
                if (current == 0)
                {
                    msgbox += "\n- Full load Current";
                }
                if ((comboBox2.Text == "Motor") && ConsiderVdStart && (multiplier == 0))
                {
                    msgbox += "\n- Multiplier";
                }
                    if (pf > 1)
                {
                    msgbox += "\n- P.F. full load";
                }
                if (pfstart > 1)
                {
                    msgbox += "\n- P.F. start";
                }
                if (length == 0)
                {
                    msgbox += "\n- Length: cable length can't be 0";
                }
                if ((vdrunmax > 100) || (vdrunmax <= 0))
                {
                    msgbox += "\n- Vd Run Max";
                }
                if ((comboBox2.Text == "Motor") && ConsiderVdStart && ((vdstartmax > 100) || (vdstartmax <= 0)))
                {
                    msgbox += "\n- Vd Start Max";
                }
                MessageBox.Show(msgbox, "Input Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DisabeUndoReset();
                vd_size_calc();
            }
        }

        private void SelectedDataLength()
        {
            if (material == "Copper")
            {
                if (insulation == "XLPE")
                {
                    switch (cores)
                    {
                        case 2:
                            currentDataLength = xlpe2coreLength;
                            break;
                        case 3:
                            currentDataLength = xlpe3coreLength;
                            break;
                        case 4:
                            currentDataLength = xlpe4coreLength;
                            break;
                    }
                }
                else if (insulation == "PVC")
                {

                    switch (cores)
                    {
                        case 2:
                            currentDataLength = pvc2coreLength;
                            break;
                        case 3:
                            currentDataLength = pvc3coreLength;
                            break;
                        case 4:
                            currentDataLength = pvc4coreLength;
                            break;
                    }
                }
            }
        }

        private void TextBox36_TextChanged(object sender, EventArgs e)
        {
            if((textBox36.Text != "") && (radioButton7.Checked))
            {
                k3main = double.Parse(textBox36.Text);
            }

            DisabeUndoReset();
            Updatekt();
            enable_result_btn();
        }


        private void TextBox17_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox17.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox18_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox18.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox36_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox36.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox15_TextChanged(object sender, EventArgs e)
        {
            if (textBox15.Text != "")
            {
                initialTemp = double.Parse(textBox15.Text);
            }
            else
            {
                initialTemp = 0;
            }
            SCLTEchanged();
            Calc_k();
            enable_result_btn();
        }

        private void ComboBox15_SelectedIndexChanged(object sender, EventArgs e)
        {
            enable_result_btn();
            if (!((comboBox15.Text == "Update Size") || (comboBox15.Text == "")))
            {
                textBox37.Text = comboBox15.Text;
                if (comboBox15.SelectedIndex == 1)
                {
                    m = i;
                }
                else if (comboBox15.SelectedIndex == 2)
                {
                    m = i + 1;
                }
                else if (comboBox15.SelectedIndex == 3)
                {
                    m = i + 2;
                }
            }

            if (!((textBox37.Text == wirearea.ToString()) || (comboBox15.Text == "Update Size") || (textBox37.Text == "") || 
                (comboBox15.Text == "")))
            {
                if (radioButton4.Checked)
                {
                    if (insulation == "XLPE")
                    {
                        if (cores == 2)
                        {
                            wirearea = xlpe2coreDB[m, 0];
                            if (phase == "DC")
                            {
                                Rdc = xlpe2coreDB[m, 3];
                                vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                            }
                            else //AC
                            {
                                Rac = xlpe2coreDB[m, 1];
                                X = xlpe2coreDB[m, 2];

                                if (phase == "Single-Phase AC")
                                {
                                    vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                    / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                        (X * Math.Sqrt(1 - pf * pf))) * 100);

                                    if ((loadtype == "Motor") && ConsiderVdStart)
                                    {
                                        vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                        / (n * 1000 * voltage);
                                    }
                                    else
                                    {
                                        vdstart = 0;
                                    }
                                }
                                else if (phase == "Three-Phase AC")
                                {
                                    vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                    / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                        (X * Math.Sqrt(1 - pf * pf))) * 100);

                                    if ((loadtype == "Motor") && ConsiderVdStart)
                                    {
                                        vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                        / (n * 1000 * voltage);
                                    }
                                    else
                                    {
                                        vdstart = 0;
                                    }
                                }
                            }

                            if (armour == "Non Armoured")
                            {
                                diameter = ODxlpe2core[m, 1];
                            }
                            else if (armour == "SWA")
                            {
                                diameter = ODxlpe2core[m, 2];
                            }
                            else if (armour == "DSTA")
                            {
                                diameter = ODxlpe2core[m, 3];
                            }

                            if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                            {
                                Irated = xlpe2coreDB[m, 4] * n;
                            }
                            else //above ground
                            {
                                Irated = xlpe2coreDB[m, 5] * n;
                            }

                            iderated = Irated * ktmain;

                            cable_lte();

                            // Validasi
                            validasi_vd();

                        }
                        else if (cores == 3)
                        {
                            
                            wirearea = xlpe3coreDB[m, 0];
                            if (phase == "DC")
                            {
                                Rdc = xlpe3coreDB[m, 3];
                                vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                            }
                            else //AC
                            {
                                Rac = xlpe3coreDB[m, 1];
                                X = xlpe3coreDB[m, 2];

                                if (phase == "Single-Phase AC")
                                {
                                    vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                    / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                        (X * Math.Sqrt(1 - pf * pf))) * 100);

                                    if ((loadtype == "Motor") && ConsiderVdStart)
                                    {
                                        vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                        / (n * 1000 * voltage);
                                    }
                                    else
                                    {
                                        vdstart = 0;
                                    }
                                }
                                else if (phase == "Three-Phase AC")
                                {
                                    vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                    / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                        (X * Math.Sqrt(1 - pf * pf))) * 100);

                                    if ((loadtype == "Motor") && ConsiderVdStart)
                                    {
                                        vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                        / (n * 1000 * voltage);
                                    }
                                    else
                                    {
                                        vdstart = 0;
                                    }
                                }
                            }

                            if (armour == "Non Armoured")
                            {
                                diameter = ODxlpe3core[m, 1];
                            }
                            else if (armour == "SWA")
                            {
                                diameter = ODxlpe3core[m, 2];
                            }
                            else if (armour == "DSTA")
                            {
                                diameter = ODxlpe3core[m, 3];
                            }


                            if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                            {
                                Irated = xlpe3coreDB[m, 4] * n;
                            }
                            else //above ground
                            {
                                Irated = xlpe3coreDB[m, 5] * n;
                            }

                            iderated = Irated * ktmain;
                            cable_lte();

                            // Validasi
                            validasi_vd();

                        }
                        else if (cores == 4)
                        {
                           
                            wirearea = xlpe4coreDB[m, 0];
                            if (phase == "DC")
                            {
                                Rdc = xlpe4coreDB[m, 3];
                                vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                            }
                            else //AC
                            {
                                Rac = xlpe4coreDB[m, 1];
                                X = xlpe4coreDB[m, 2];

                                if (phase == "Single-Phase AC")
                                {
                                    vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                    / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                        (X * Math.Sqrt(1 - pf * pf))) * 100);

                                    if ((loadtype == "Motor") && ConsiderVdStart)
                                    {
                                        vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                        / (n * 1000 * voltage);
                                    }
                                    else
                                    {
                                        vdstart = 0;
                                    }
                                }
                                else if (phase == "Three-Phase AC")
                                {
                                    vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                    / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                        (X * Math.Sqrt(1 - pf * pf))) * 100);

                                    if ((loadtype == "Motor") && ConsiderVdStart)
                                    {
                                        vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                        / (n * 1000 * voltage);
                                    }
                                    else
                                    {
                                        vdstart = 0;
                                    }
                                }
                            }

                            if (armour == "Non Armoured")
                            {
                                diameter = ODxlpe4core[m, 1];
                            }
                            else if (armour == "SWA")
                            {
                                diameter = ODxlpe4core[m, 2];
                            }
                            else if (armour == "DSTA")
                            {
                                diameter = ODxlpe4core[m, 3];
                            }

                            if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                            {
                                Irated = xlpe4coreDB[m, 4] * n;
                            }
                            else //above ground
                            {
                                Irated = xlpe4coreDB[m, 5] * n;
                            }

                            iderated = Irated * ktmain;
                            cable_lte();

                            // Validasi
                            validasi_vd();

                            
                        }
                    }
                    else if (insulation == "PVC")
                    {
                        if (cores == 2)
                        {
                            wirearea = pvc2coreDB[m, 0];
                            if (phase == "DC")
                            {
                                Rdc = pvc2coreDB[m, 3];
                                vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                            }
                            else //AC
                            {
                                Rac = pvc2coreDB[m, 1];
                                X = pvc2coreDB[m, 2];

                                if (phase == "Single-Phase AC")
                                {
                                    vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                    / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                        (X * Math.Sqrt(1 - pf * pf))) * 100);

                                    if ((loadtype == "Motor") && ConsiderVdStart)
                                    {
                                        vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                        / (n * 1000 * voltage);
                                    }
                                    else
                                    {
                                        vdstart = 0;
                                    }
                                }
                                else if (phase == "Three-Phase AC")
                                {
                                    vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                    / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                        (X * Math.Sqrt(1 - pf * pf))) * 100);

                                    if ((loadtype == "Motor") && ConsiderVdStart)
                                    {
                                        vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                        / (n * 1000 * voltage);
                                    }
                                    else
                                    {
                                        vdstart = 0;
                                    }
                                }
                            }

                            if (armour == "Non Armoured")
                            {
                                diameter = ODpvc2core[m, 1];
                            }
                            else if (armour == "SWA")
                            {
                                diameter = ODpvc2core[m, 2];
                            }
                            else if (armour == "DSTA")
                            {
                                diameter = ODpvc2core[m, 3];
                            }

                            if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                            {
                                Irated = pvc2coreDB[m, 4] * n;
                            }
                            else //above ground
                            {
                                Irated = pvc2coreDB[m, 5] * n;
                            }

                            iderated = Irated * ktmain;
                            cable_lte();

                        }
                        else if (cores == 3)
                        {
                            wirearea = pvc3coreDB[m, 0];
                            if (phase == "DC")
                            {
                                Rdc = pvc3coreDB[m, 3];
                                vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                            }
                            else //AC
                            {
                                Rac = pvc3coreDB[m, 1];
                                X = pvc3coreDB[m, 2];

                                if (phase == "Single-Phase AC")
                                {
                                    vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                    / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                        (X * Math.Sqrt(1 - pf * pf))) * 100);

                                    if ((loadtype == "Motor") && ConsiderVdStart)
                                    {
                                        vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                        / (n * 1000 * voltage);
                                    }
                                    else
                                    {
                                        vdstart = 0;
                                    }
                                }
                                else if (phase == "Three-Phase AC")
                                {
                                    vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                    / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                        (X * Math.Sqrt(1 - pf * pf))) * 100);

                                    if ((loadtype == "Motor") && ConsiderVdStart)
                                    {
                                        vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                        / (n * 1000 * voltage);
                                    }
                                    else
                                    {
                                        vdstart = 0;
                                    }
                                }
                            }

                            if (armour == "Non Armoured")
                            {
                                diameter = ODpvc3core[m, 1];
                            }
                            else if (armour == "SWA")
                            {
                                diameter = ODpvc3core[m, 2];
                            }
                            else if (armour == "DSTA")
                            {
                                diameter = ODpvc3core[m, 3];
                            }

                            if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                            {
                                Irated = pvc3coreDB[m, 4] * n;
                            }
                            else //above ground
                            {
                                Irated = pvc3coreDB[m, 5] * n;
                            }

                            iderated = Irated * ktmain;
                            cable_lte();
                            
                        }
                        else if (cores == 4)
                        {
                            wirearea = pvc4coreDB[m, 0];
                            if (phase == "DC")
                            {
                                Rdc = pvc4coreDB[m, 3];
                                vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);

                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                            }
                            else //AC
                            {
                                Rac = pvc4coreDB[m, 1];
                                X = pvc4coreDB[m, 2];

                                if (phase == "Single-Phase AC")
                                {
                                    vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                    / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                        (X * Math.Sqrt(1 - pf * pf))) * 100);

                                    if ((loadtype == "Motor") && ConsiderVdStart)
                                    {
                                        vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                        / (n * 1000 * voltage);
                                    }
                                    else
                                    {
                                        vdstart = 0;
                                    }
                                }
                                else if (phase == "Three-Phase AC")
                                {
                                    vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                    / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                        (X * Math.Sqrt(1 - pf * pf))) * 100);


                                    if ((loadtype == "Motor") && ConsiderVdStart)
                                    {
                                        vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                        / (n * 1000 * voltage);
                                    }
                                    else
                                    {
                                        vdstart = 0;
                                    }
                                }
                            }

                            if (armour == "Non Armoured")
                            {
                                diameter = ODpvc4core[m, 1];
                            }
                            else if (armour == "SWA")
                            {
                                diameter = ODpvc4core[m, 2];
                            }
                            else if (armour == "DSTA")
                            {
                                diameter = ODpvc4core[m, 3];
                            }

                            if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                            {
                                Irated = pvc4coreDB[m, 4] * n;
                            }
                            else //above ground
                            {
                                Irated = pvc4coreDB[m, 5] * n;
                            }

                            iderated = Irated * ktmain;
                            cable_lte();
                            
                        }
                    }
                }
                else if (radioButton3.Checked) //manual cable database input
                {
                    wirearea = inputCableData[m, 0];

                    if (phase == "DC")
                    {
                        Rdc = inputCableData[m, 2];
                        Rac = 0;

                        vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                    }
                    else
                    {
                        Rac = inputCableData[m, 2];
                        Rdc = 0;
                        X = inputCableData[m, 3];

                        if (phase == "Single-Phase AC")
                        {
                            vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                            / (n * 1000 * voltage);

                            lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                (X * Math.Sqrt(1 - pf * pf))) * 100);

                            if ((loadtype == "Motor") && ConsiderVdStart)
                            {
                                vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                / (n * 1000 * voltage);
                            }
                            else
                            {
                                vdstart = 0;
                            }
                        }
                        else if (phase == "Three-Phase AC")
                        {
                            vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                            / (n * 1000 * voltage);

                            lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                (X * Math.Sqrt(1 - pf * pf))) * 100);

                            if ((loadtype == "Motor") && ConsiderVdStart)
                            {
                                vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                / (n * 1000 * voltage);
                            }
                            else
                            {
                                vdstart = 0;
                            }
                        }
                    }

                    Irated = inputCableData[m, 4];
                    diameter = inputCableData[m, 5];

                    iderated = Irated * ktmain;
                    cable_lte();
                }

                textBox12.Text = n.ToString();
                textBox8.Text = vdrun.ToString("0.##");
                textBox29.Text = lmax.ToString("0.##");
                textBox22.Text = cLTE.ToString("0.##");

                if (material == "Copper")
                {
                    materialname = "Cu";
                }

                if ((loadtype == "Motor") && ConsiderVdStart)
                {
                    textBox10.Text = vdstart.ToString("0.##");
                }

                readtemp = "";

                readtemp += n.ToString() + "  ×  " + cores.ToString("0.##") + "/C  #  " + wirearea.ToString() +
                    " mm²    " + ratedvoltage + " / " + materialname + " / " + insulation;

                if (armour != "Non Armoured")
                {
                    readtemp += " / " + armour;
                }

                readtemp += " / " + outersheath;

                tbResult.Text = readtemp;

                save_vd_result();

                enable_save();
                if (phase == "DC")
                {
                    textBox34.Text = Rdc.ToString("0.####");
                    textBox33.Text = "";
                }
                else //AC
                {
                    textBox34.Text = Rac.ToString("0.####");
                    textBox33.Text = X.ToString("0.####");
                }
                textBox32.Text = Irated.ToString("0.##");
                if (IsCalculateNeeded())
                {
                    disable_save();
                }
            }
            
        }


        private void Button8_Click(object sender, EventArgs e)
        {
            calculated = false;
            panel6.Enabled = true;
            panel5.Enabled = true;

            textBox8.Text = "";
            textBox10.Text = "";
            textBox29.Text = "";
            textBox12.Text = "1";
            textBox37.Text = "";
            textBox34.Text = "";
            textBox33.Text = "";
            textBox32.Text = "";
            textBox28.Text = "";
            textBox23.Text = "";
            textBox30.Text = "";
            textBox22.Text = "";
            textBox20.Text = "";
            comboBox11.Text = "";
            comboBox10.Text = "";


            comboBox5.Enabled = true;
            textBox12.ReadOnly = false;
            comboBox15.SelectedIndex = -1;
            comboBox15.Items.Clear();
            comboBox10.SelectedIndex = -1;
            comboBox11.SelectedIndex = -1;
            comboBox12.SelectedIndex = -1;

            panel4.Enabled = false;
            button8.Enabled = false;

            disable_save();
        }

        private void TextBox4_Leave_1(object sender, EventArgs e)
        {
            if (pf > 1)
            {
                MessageBox.Show("Power Factor can't be greater than 1!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label86.Visible = false;
            timer1.Enabled = false;
        }

        private void RadioButton7_CheckedChanged(object sender, EventArgs e)
        {
            DisabeUndoReset();
            enable_correction();
            reset_correction();
            if (!radioButton7.Checked)
            {
                textBox17.ReadOnly = true;
                textBox18.ReadOnly = true;
                textBox36.ReadOnly = true;

                label42.Visible = true;
                label80.Visible = true;
                textBox18.Visible = true;
                textBox36.Visible = true;

                textBox19.Location = new Point(kttextboxX, kttextboxY);
                label43.Location = new Point(ktlabelX, ktlabelY);
                panel24.Location = new Point(textBox19.Location.X - 4, textBox19.Location.Y);
            }
            else if (radioButton7.Checked)
            {
                textBox17.ReadOnly = false;
                textBox18.ReadOnly = false;
                textBox36.ReadOnly = false;

                if (textBox17.Text == "")
                {
                    label42.Visible = false;
                    label80.Visible = false;
                    textBox18.Visible = false;
                    textBox36.Visible = false;
                    textBox19.Location = textBox18.Location;
                    label43.Location = label42.Location;
                    panel24.Location = new Point(textBox18.Location.X - 4, textBox18.Location.Y);

                }
                else if ((textBox17.Text != "") && (textBox18.Text == ""))
                {
                    textBox36.Visible = false;
                    label80.Visible = false;

                    textBox19.Location = textBox36.Location;
                    label43.Location = label80.Location;
                }

            }
        }

        private void RadioButton7_Click(object sender, EventArgs e)
        {
            if (!radioButton7.Checked)
            {
                radioButton7.Checked = true;
            }
            else if (radioButton7.Checked)
            {
                radioButton7.Checked = false;
            }
        }

        private void RadioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton8.Checked)
            {
                cbPower.SelectedIndex = 0;
                TextBox1.ReadOnly = true;
                cbPower.Enabled = false;

                textBox3.ReadOnly = false;
                panel14.BackColor = Color.Transparent;
                if (textBox3.Text == "")
                {
                    panel11.BackColor = Color.Red;
                }
                calc_power();

            }
            else
            {
                TextBox1.ReadOnly = false;
                cbPower.Enabled = true;

                textBox3.ReadOnly = true;
                panel11.BackColor = Color.Transparent;
                if (textBox2.Text == "")
                {
                    panel14.BackColor = Color.Red;
                }
                calc_current();
            }
            DisabeUndoReset();
        }

        private void RadioButton8_Click(object sender, EventArgs e)
        {
            if (radioButton8.Checked)
            {
                radioButton8.Checked = false;
            }
            else
            {
                radioButton8.Checked = true;
            }
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                current = double.Parse(textBox3.Text);
                panel11.BackColor = Color.Transparent;
            }
            else if ((textBox3.Text == "") && (radioButton8.Checked))
            {
                current = 0;
                panel11.BackColor = Color.Red;
            }

            if (radioButton8.Checked)
            {
                calc_currentstart();
                calc_power();
                enable_vd_btn();
                enable_result_btn();
            }
            DisabeUndoReset();
        }

        private void calc_currentstart()
        {

            if (radioButton8.Checked)
            {
                if ((phase == "DC") && (loadtype == "Feeder"))
                {
                    textBox7.Text = "";
                    currentstart = 0;
                }
                else if (((phase == "Single-Phase AC") || (phase == "Three-Phase AC")) && (loadtype == "Motor") && (textBox25.Text != ""))
                {
                    currentstart = current * multiplier;
                    textBox7.Text = currentstart.ToString("0.##");
                }
                else
                {
                    textBox7.Text = "";
                    currentstart = 0;
                }
            }
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            label87.Visible = false;
            timer2.Enabled = false;
        }

        private void Timer3_Tick(object sender, EventArgs e)
        {
            label88.Visible = false;
            timer3.Enabled = false;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TextBox14_Leave(object sender, EventArgs e)
        {
            if (pfstart > 1)
            {
                MessageBox.Show("Starting Power Factor can't be greater than 1!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CableDataTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDataTable();
        }

        private void CableSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5.OpenSummary();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void ReturnToTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm.ReturnToTitle = true;
            Close();
        }

        private void ResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtReset = new DataTable();

            dtReset.Columns.Add("Diameter");
            dtReset.Columns.Add("TagNo");
            dtReset.Columns.Add("From");
            dtReset.Columns.Add("FromDesc");
            dtReset.Columns.Add("To");
            dtReset.Columns.Add("ToDesc");
            dtReset.Columns.Add("Phase");
            dtReset.Columns.Add("LoadType");
            dtReset.Columns.Add("VoltageSystem");
            dtReset.Columns.Add("CurrentButton");
            dtReset.Columns.Add("cbPower");
            dtReset.Columns.Add("Power");
            dtReset.Columns.Add("Current");
            dtReset.Columns.Add("Voltage");
            dtReset.Columns.Add("Efficiency");
            dtReset.Columns.Add("PF");
            dtReset.Columns.Add("PFStart");
            dtReset.Columns.Add("Multiplier");
            dtReset.Columns.Add("RatedVoltage");
            dtReset.Columns.Add("Material");
            dtReset.Columns.Add("Insulation");
            dtReset.Columns.Add("Armour");
            dtReset.Columns.Add("OutherSheath");
            dtReset.Columns.Add("Installation");
            dtReset.Columns.Add("DeratingButton");
            dtReset.Columns.Add("K1");
            dtReset.Columns.Add("K2");
            dtReset.Columns.Add("K3");
            dtReset.Columns.Add("Kt");
            dtReset.Columns.Add("Length");
            dtReset.Columns.Add("VdRunMax");
            dtReset.Columns.Add("VdStartMax");
            dtReset.Columns.Add("CablePropButton");
            dtReset.Columns.Add("VdRun");
            dtReset.Columns.Add("VdStart");
            dtReset.Columns.Add("Lmax");
            dtReset.Columns.Add("N");
            dtReset.Columns.Add("Cores");
            dtReset.Columns.Add("WireArea");
            dtReset.Columns.Add("Rac");
            dtReset.Columns.Add("X");
            dtReset.Columns.Add("Irated");
            dtReset.Columns.Add("Iderated");
            dtReset.Columns.Add("SCOrLTE");
            dtReset.Columns.Add("SCCurrent");
            dtReset.Columns.Add("TBreak");
            dtReset.Columns.Add("InitialTemp");
            dtReset.Columns.Add("FinalTemp");
            dtReset.Columns.Add("CLTE");
            dtReset.Columns.Add("BreakerType");
            dtReset.Columns.Add("BreakerBtn");
            dtReset.Columns.Add("SCRating");
            dtReset.Columns.Add("BreakNominalCurrent");
            dtReset.Columns.Add("BLTE");
            dtReset.Columns.Add("i");
            dtReset.Columns.Add("Database");
            dtReset.Columns.Add("Remarks");
            dtReset.Columns.Add("Result");

            dtr = dtReset.NewRow();

            //full data
            dtr[1] = textBox13.Text;
            dtr[2] = textBox26.Text;
            dtr[3] = textBox16.Text;
            dtr[4] = textBox27.Text;
            dtr[5] = textBox31.Text;
            dtr[6] = comboBox3.Text;
            dtr[7] = comboBox2.Text;
            dtr[8] = comboBox13.Text;
            if (!radioButton8.Checked) //Manual current input or not
            {
                dtr[9] = false;
                dtr[10] = cbPower.Text; //powerdata
                if (Convert.ToString(dtr[10]) == "kW") //power
                {
                    dtr[11] = TextBox1.Text;
                }
                else if (Convert.ToString(dtr[10]) == "kVA")
                {
                    dtr[11] = TextBox1.Text;
                }
                else if (Convert.ToString(dtr[10]) == "HP")
                {
                    dtr[11] = TextBox1.Text;
                }
                dtr[12] = textBox3.Text;
            }
            else
            {
                dtr[9] = true;
                dtr[10] = cbPower.Text;
                dtr[11] = TextBox1.Text;
                dtr[12] = textBox3.Text;
            }
            dtr[13] = textBox2.Text;
            dtr[14] = textBox5.Text;
            dtr[15] = textBox4.Text;

            if ((loadtype == "Motor") && ConsiderVdStart)
            {
                dtr[16] = textBox14.Text;
                dtr[17] = textBox25.Text;
                dtr[31] = textBox11.Text;
                dtr[34] = textBox10.Text;
            }
            else
            {
                dtr[16] = "";
                dtr[17] = "";
                dtr[31] = "";
                dtr[34] = "";
            }

            dtr[18] = comboBox14.Text;
            dtr[19] = comboBox4.Text;
            dtr[20] = comboBox6.Text;
            dtr[21] = comboBox7.Text;
            dtr[22] = comboBox8.Text;
            dtr[23] = comboBox9.Text;
            if (radioButton7.Checked) //Manual derating input or not
            {
                dtr[24] = true;
            }
            else
            {
                dtr[24] = false;
            }
            dtr[25] = textBox17.Text;
            dtr[26] = textBox18.Text;
            dtr[27] = textBox36.Text;
            dtr[28] = textBox19.Text;
            dtr[29] = textBox6.Text;
            dtr[30] = textBox9.Text;
            if (radioButton4.Checked) //vendor/manual cable properties input
            {
                dtr[32] = "vendor";
                dtr[55] = comboBoxVendor.Text;
            }
            else if (radioButton3.Checked)
            {
                dtr[32] = "manual";
                dtr[55] = "";
            }
            dtr[33] = textBox8.Text;
            dtr[35] = textBox29.Text;
            dtr[36] = textBox12.Text;
            dtr[37] = comboBox5.Text;
            dtr[38] = textBox37.Text;
            if (loadtype == "DC")
            {
                dtr[39] = textBox34.Text;
                dtr[40] = textBox33.Text;
            }
            else
            {
                dtr[39] = textBox34.Text;
                dtr[40] = textBox33.Text;
            }
            dtr[41] = textBox32.Text;
            if ( Convert.ToString(dtr[41]) != "")
            {
                dtr[42] = Convert.ToDouble(dtr[41]) * Convert.ToDouble(dtr[28]);
            }
            else
            {
                dtr[42] = "";
            }
            
            if (radioButton2.Checked) //using S.C. current or LTE
            {
                dtr[43] = "sc";
                dtr[44] = textBox28.Text;
                dtr[45] = textBox23.Text;
                dtr[53] = "";
            }
            else
            {
                dtr[43] = "lte";
                dtr[44] = "";
                dtr[45] = "";
                dtr[53] = textBox20.Text;
            }
            dtr[46] = textBox15.Text;
            dtr[47] = textBox24.Text;
            dtr[48] = textBox22.Text;
            dtr[49] = comboBox12.Text;
            if (radioButton5.Checked)
            {
                dtr[50] = "vendor";
            }
            else if (radioButton6.Checked)
            {
                dtr[50] = "manual";
            }
            dtr[51] = comboBox10.Text;
            dtr[52] = comboBox11.Text;
            dtr[54] = i;
            dtr[56] = textBox35.Text;
            dtr[57] = tbResult.Text;

            dtReset.Rows.Add(dtr);

            
            //reset all the  data
            ResetData();

            //enable the undo reset button
            undoResetToolStripMenuItem.Enabled = true;
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAbout.ShowDialog();
        }

        private void CableDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            faddcable = new FormAddCableDatabase();
            faddcable.FormClosed += AddDatabaseClosed;
            faddcable.tabControl1.SelectedIndex = 0;
            faddcable.ShowDialog();
        }

        private void AddDatabaseClosed(object sender, FormClosedEventArgs e)
        {
            if (FormAddCableDatabase.databaseEdited)
            {
                LoadIECDatabase();
                FormAddCableDatabase.databaseEdited = false;
            }
        }

        private void ComboBoxVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBoxVendor, comboBoxVendor.Text);

            SelectedDatabase = comboBoxVendor.Text;
            //Gather cable specification of the selected database
            if ((SelectedDatabase != "Sumi Indo Cable (Default)") && (SelectedDatabase != ""))
            {
                ReadIECDatabase();
            }
            else if (SelectedDatabase == "Sumi Indo Cable (Default)")
            {
                xlpe2coreDB = new double[17,6];
                Array.Copy(xlpe2core, xlpe2coreDB, xlpe2core.Length);

                xlpe3coreDB = new double[17, 6];
                Array.Copy(xlpe3core, xlpe3coreDB, xlpe3core.Length);

                xlpe4coreDB = new double[17, 6];
                Array.Copy(xlpe4core, xlpe4coreDB, xlpe4core.Length);

                pvc2coreDB = new double[16, 6];
                Array.Copy(pvc2core, pvc2coreDB, pvc2core.Length);

                pvc3coreDB = new double[16, 6];
                Array.Copy(pvc3core, pvc3coreDB, pvc3core.Length);

                pvc4coreDB = new double[16, 6];
                Array.Copy(pvc4core, pvc4coreDB, pvc4core.Length);

                xlpe2coreLength = 17;
                xlpe3coreLength = 17;
                xlpe4coreLength = 17;
                pvc2coreLength = 16;
                pvc3coreLength = 16;
                pvc4coreLength = 16;
            }
            else if (SelectedDatabase != "")
            {
                xlpe2coreDB = null;
                xlpe3coreDB = null;
                xlpe4coreDB = null;
                pvc2coreDB = null;
                pvc3coreDB = null;
                pvc4coreDB = null;
            }

            DisabeUndoReset();
        }

        private void TextBox9_Leave(object sender, EventArgs e)
        {
            if (vdrunmax > 100)
            {
                MessageBox.Show("Vd run can't be greater than 100%!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if ((vdrunmax <= 0) && (textBox9.Text != ""))
            {
                MessageBox.Show("Vd run can't be 0%!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TextBox11_Leave(object sender, EventArgs e)
        {
            if (vdstartmax > 100)
            {
                MessageBox.Show("Vd start can't be greater than 100%!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if ((vdstartmax <= 0) && (textBox11.Text != ""))
            {
                MessageBox.Show("Vd start can't be 0%!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TextBox24_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox24.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void AddCableDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            faddcable = new FormAddCableDatabase();
            faddcable.FormClosed += AddDatabaseClosed;
            faddcable.tabControl1.SelectedIndex = 1;
            faddcable.ShowDialog();
        }

        private void Panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Panel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Panel14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Panel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label39_Click(object sender, EventArgs e)
        {

        }

        private void Panel24_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label43_Click(object sender, EventArgs e)
        {

        }

        private void Label40_Click(object sender, EventArgs e)
        {

        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Form5.savefile == "")
            {
                f5.ExportDgvToXML();
            }
            else //safefile == last savepath
            {
                f5.saveExportDgvToXML();
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f5.ExportDgvToXML();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFromMain = true;
            f5.OpenFileDecide();
            if (FileOpened)
            {
                OpenDataTable();
                FileOpened = false;
            }
            OpenFromMain = false;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            AddToResult();
            if (Form5.savefile == "")
            {
                f5.ExportDgvToXML();
            }
            else //safefile == last savepath
            {
                f5.saveExportDgvToXML();
            }
        }

        private void AddToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddToResult();
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            MessageBox.Show(breakcurrent.ToString()); ;
        }

        private void Calc_k()
        {
            if (material == "Copper")
            {
                k = 226 * Math.Sqrt(Math.Log(1 + (finalTemp - initialTemp)/(234.5 + initialTemp)));
            }
            else if (material == "Alumunium")
            {
                k = 148 * Math.Sqrt(Math.Log(1 + (finalTemp - initialTemp) / (228 + initialTemp)));
            }

            if (k != 0)
            {
                textBox21.Text = k.ToString("0.###");
            }
            else
            {
                textBox21.Text = "";
            }
        }

        private void UndoResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UndoReset();
            DisabeUndoReset();
        }

        private void UndoReset()
        {
            DataRow dtx = dtReset.Rows[0];
            bool noDatabase = false;
            if (Convert.ToString(dtx[32]) == "vendor")
            {
                radioButton4.Checked = true;
                if (comboBoxVendor.Items.Contains(Convert.ToString(dtx[55])))
                {
                    comboBoxVendor.SelectedItem = Convert.ToString(dtx[55]);
                }
                else
                {
                    MessageBox.Show("Error: Cable Database \"" + Convert.ToString(dtx[55]) + "\" not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    EditingState = false;
                    f5.Enabled = true;

                    if (!f5.Visible)
                    {
                        f5.Show();
                    }
                    else if (f5.WindowState == FormWindowState.Minimized)
                    {
                        f5.WindowState = FormWindowState.Normal;
                    }
                    else
                    {
                        f5.BringToFront();
                    }

                    button4.Text = "Add to Result";
                    addToolStripMenuItem.Text = "Add to Result";
                    button3.Text = "Add and Save";
                    button5.Text = "Open Calculation Result";
                    EditingState = false;
                    noDatabase = true;
                }
            }
            else if (Convert.ToString(dtx[32]) == "manual")
            {
                radioButton3.Checked = true;
                comboBoxVendor.SelectedIndex = -1;
            }

            if (!noDatabase)
            {
                textBox13.Text = Convert.ToString(dtx[1]); //tagno
                textBox26.Text = Convert.ToString(dtx[2]); //from
                textBox16.Text = Convert.ToString(dtx[3]); //fromdesc
                textBox27.Text = Convert.ToString(dtx[4]); //to
                textBox31.Text = Convert.ToString(dtx[5]); //todesc
                comboBox3.Text = Convert.ToString(dtx[6]); //phase
                comboBox2.Text = Convert.ToString(dtx[7]); //loadtype
                comboBox13.Text = Convert.ToString(dtx[8]); //voltage system
                if (Convert.ToString(dtx[9]) == "True")
                {
                    radioButton8.Checked = true;
                }
                else
                {
                    radioButton8.Checked = false;
                }
                cbPower.Text = Convert.ToString(dtx[10]); // power unit
                TextBox1.Text = DtrToDoubleText(dtx, 11); //Power
                textBox3.Text = DtrToDoubleText(dtx, 12); // FL Current
                if(DtrToDoubleText(dtx, 12) != "")
                {

                    current = Convert.ToDouble(dtx[12]);
                }
                textBox2.Text = DtrToDoubleText(dtx, 13); //Voltage
                textBox5.Text = DtrToDoubleText(dtx, 14); //eff
                textBox4.Text = DtrToDoubleText(dtx, 15); //pf
                textBox14.Text = DtrToDoubleText(dtx, 16); //pfstart
                textBox25.Text = DtrToDoubleText(dtx, 17); //multiplier
                comboBox14.Text = DtrToDoubleText(dtx, 18);//ratedvoltage
                comboBox4.Text = Convert.ToString(dtx[19]);//material
                comboBox6.Text = Convert.ToString(dtx[20]); //insulation
                comboBox7.Text = Convert.ToString(dtx[21]); //armour
                comboBox8.Text = Convert.ToString(dtx[22]); //outer Sheath
                comboBox9.Text = Convert.ToString(dtx[23]); //installation
                if (Convert.ToString(dtx[24]) == "True") //manual input correction factor
                {
                    radioButton7.Checked = true;
                }
                else
                {
                    radioButton7.Checked = false;
                }
                textBox17.Text = DtrToDoubleText(dtx, 25); //k1main
                textBox18.Text = DtrToDoubleText(dtx, 26); //k2main
                textBox36.Text = DtrToDoubleText(dtx, 27); //k3main
                textBox19.Text = DtrToDoubleText(dtx, 28); //ktmain
                if (DtrToDoubleText(dtx, 28) != "")
                {

                    ktmain = Convert.ToDouble(dtx[28]);
                }
                textBox6.Text = DtrToDoubleText(dtx, 29); //length
                textBox9.Text = DtrToDoubleText(dtx, 30); //vdrunmax
                textBox11.Text = DtrToDoubleText(dtx, 31); //vdstartmax
                
                textBox8.Text = DtrToDoubleText(dtx, 33); //vdrun
                if (DtrToDoubleText(dtx, 33) != "")
                {
                    vdrun = Convert.ToDouble(dtx[33]);
                }
                textBox10.Text = DtrToDoubleText(dtx, 34); //vdstart
                if (textBox10.Text != "")
                {
                    vdstart = Convert.ToDouble(dtx[34]);
                }
                else
                {
                    textBox10.Text = "";
                }
                textBox29.Text = DtrToDoubleText(dtx, 35);  //lmax
                if (DtrToDoubleText(dtx, 35) != "")
                {
                    lmax = Convert.ToDouble(dtx[35]);
                }
                textBox12.Text = Convert.ToString(dtx[36]); //no of run
                comboBox5.Text = Convert.ToString(dtx[37]); //no of cores
                textBox37.Text = DtrToDoubleText(dtx, 38); //wirearea
                if (DtrToDoubleText(dtx, 38) != "")
                {

                    wirearea = Convert.ToDouble(dtx[38]);
                    //Disable all inputs before vd calculation
                    panel4.Enabled = true;
                    panel5.Enabled = false;
                    panel6.Enabled = false;

                    //enable vd calculate & edit data buttons
                    button7.Enabled = true;
                    button8.Enabled = true;

                    i = Convert.ToInt32(dtx[54]);
                    Update_size();
                }
                if ((Convert.ToString(dtx[6]) == "DC") && (DtrToDoubleText(dtx,39) != ""))
                {
                    Rdc = Convert.ToDouble(dtx[39]);
                    textBox34.Text = Rac.ToString("0.####"); //Rdc
                }
                else if (((Convert.ToString(dtx[6]) == "Single-Phase AC") || (Convert.ToString(dtx[6]) == "Three-Phase AC")) && (DtrToDoubleText(dtx, 39) != ""))
                {
                    Rac = Convert.ToDouble(dtx[39]);
                    textBox34.Text = Rac.ToString("0.####"); //Rac
                    X = Convert.ToDouble(dtx[40]);
                    textBox33.Text = X.ToString("0.####"); //X
                }
                textBox32.Text = Convert.ToString(dtx[41]); //irated
                if (DtrToDoubleText(dtx,41) != "")
                {
                    Irated = Convert.ToDouble(dtx[41]);
                    iderated = Convert.ToDouble(dtx[42]); //iderated
                }
                if (Convert.ToString(dtx[43]) == "sc")
                {
                    radioButton2.Checked = true;
                }
                else if (Convert.ToString(dtx[43]) == "lte")
                {
                    radioButton1.Checked = true;
                }
                textBox28.Text = DtrToDoubleText(dtx, 44); //sccurrent
                textBox23.Text = DtrToDoubleText(dtx, 45); //tbreak
                textBox20.Text = DtrToDoubleText(dtx, 53); //blte
                textBox15.Text = DtrToDoubleText(dtx, 46); //initialtemp
                textBox24.Text = DtrToDoubleText(dtx, 47); //finaltemp
                textBox22.Text = DtrToDoubleText(dtx,48); //CLTE
                if (DtrToDoubleText(dtx, 48) != "")
                {
                    cLTE = Convert.ToDouble(dtx[48]);
                }
                if (Convert.ToString(dtx[49]) != "")
                {
                    comboBox12.Text = Convert.ToString(dtx[49]); //protection type
                    if (Convert.ToString(dtx[50]) == "vendor")
                    {
                        radioButton5.Checked = true;
                    }
                    else if (Convert.ToString(dtx[50]) == "manual")
                    {
                        radioButton6.Checked = true;
                    }
                    comboBox10.Text = DtrToDoubleText(dtx, 51); //breaker rating
                    comboBox11.Text = DtrToDoubleText(dtx, 52); //nominal current
                }
                else
                {
                    comboBox12.SelectedIndex = -1; //protection type
                    if (Convert.ToString(dtx[50]) == "vendor")
                    {
                        radioButton5.Checked = true;
                    }
                    else if (Convert.ToString(dtx[50]) == "manual")
                    {
                        radioButton6.Checked = true;
                    }
                    comboBox10.Text = DtrToDoubleText(dtx, 51); //breaker rating
                    comboBox10.SelectedIndex = -1;
                    comboBox11.Text = DtrToDoubleText(dtx, 52); //nominal current
                    comboBox11.SelectedIndex = -1;
                }
                textBox35.Text = Convert.ToString(dtx[56]); //remarks
                tbResult.Text = Convert.ToString(dtx[57]);

                if (comboBox11.Text == "") //data being edited is vd calculated only
                {
                    button2.Enabled = false;
                }
                else //data being edited is complete with SC/LTE consideration
                {
                    button2.Enabled = false;
                }

                disable_save();
                enable_result_btn();
            }
        }

        private void DisabeUndoReset()
        {
            undoResetToolStripMenuItem.Enabled = false;
        }

        private void TextBox27_TextChanged(object sender, EventArgs e)
        {
            to = textBox27.Text;
            DisabeUndoReset();
        }

        private void TextBox13_TextChanged(object sender, EventArgs e)
        {
            tagno = textBox13.Text;
            if (textBox13.Text == "")
            {
                textBox26.Enabled = false;
                textBox27.Enabled = false;
                label61.Enabled = false;
                label62.Enabled = false;
                textBox16.Enabled = false;
                textBox31.Enabled = false;
                label26.Enabled = false;
                label60.Enabled = false;
                panel13.BackColor = Color.Red;
            }
            else
            {
                textBox26.Enabled = true;
                textBox27.Enabled = true;
                label61.Enabled = true;
                label62.Enabled = true;
                textBox16.Enabled = true;
                textBox31.Enabled = true;
                label26.Enabled = true;
                label60.Enabled = true;
                panel13.BackColor = Color.Transparent;
            }
            DisabeUndoReset();
            enable_vd_btn();
            enable_result_btn();
        }

        private void TextBox21_TextChanged(object sender, EventArgs e)
        {
            if (textBox21.Text != "")
            {
                k = double.Parse(textBox21.Text);
            }
            calc_smin();
        }

        private void TextBox11_TextChanged(object sender, EventArgs e)
        {

            if (comboBox2.Text == "Motor")
            {
                if ((textBox14.Text == "") && (textBox11.Text == ""))
                {
                    panel10.BackColor = Color.Transparent;
                    panel27.BackColor = Color.Transparent;
                    panel12.BackColor = Color.Transparent;
                    textBox25.Enabled = false;
                    textBox25.Text = "";
                    label59.Enabled = false;
                    label29.Enabled = false;
                    label28.Enabled = false;
                    textBox7.Enabled = false;
                    ConsiderVdStart = false;
                }
                else if ((textBox14.Text == "") && (textBox11.Text != "") && !ConsiderVdStart)
                {
                    panel10.BackColor = Color.Red;
                    panel27.BackColor = Color.Transparent;
                    panel12.BackColor = Color.Transparent;
                    textBox25.Enabled = true;
                    textBox25.Text = "1";
                    label59.Enabled = true;
                    label29.Enabled = true;
                    label28.Enabled = true;
                    textBox7.Enabled = true;
                    ConsiderVdStart = true;
                }
                else if (textBox11.Text == "")
                {
                    panel27.BackColor = Color.Red;
                }
                else if (textBox11.Text != "")
                {
                    panel27.BackColor = Color.Transparent;
                }
            }

            if (textBox11.Text != "")
            {
                vdstartmax = double.Parse(textBox11.Text);
            }
            else
            {
                vdstartmax = 0;
            }
            DisabeUndoReset();
            enable_vd_btn();
            enable_result_btn();
        }

        private void calc_current()
        {
            if ((TextBox1.Text != "") && (textBox2.Text != "") && (comboBox3.Text != "")
            && (comboBox2.Text != "") && (textBox4.Text != "") && (textBox5.Text != "") && (!radioButton8.Checked))
            {
                if ((phase == "DC") && (loadtype == "Feeder"))
                {
                    current = power * 1000 / voltage;
                    textBox3.Text = current.ToString("0.##");
                    textBox7.Text = "";
                }
                else if (phase == "Single-Phase AC")
                {
                    if (cbPower.Text == "kVA")
                    {
                        power = cplxpower * pf;
                        current = power * 1000 / (voltage * eff * pf);
                    }
                    else
                    {
                        current = power * 1000 / (voltage * eff * pf);
                    }

                    if ((loadtype == "Motor") && (textBox25.Text != ""))
                    {
                        currentstart = current * multiplier;
                        textBox7.Text = currentstart.ToString("0.##");
                    }
                    else
                    {
                        textBox7.Text = "";
                        currentstart = 0;
                    }
                }
                else if (phase == "Three-Phase AC")
                {
                    if (cbPower.Text == "kVA")
                    {
                        current = power * 1000 / (Math.Sqrt(3) * voltage * eff);
                    }
                    else
                    {
                        current = power * 1000 / (Math.Sqrt(3) * voltage * eff * pf);
                    }
                    textBox3.Text = current.ToString("0.##");

                    if ((loadtype == "Motor") && (textBox25.Text != ""))
                    {
                        currentstart = current * multiplier;
                        textBox7.Text = currentstart.ToString("0.##");
                    }
                    else
                    {
                        textBox7.Text = "";
                        currentstart = 0;
                    }
                }
                else
                {
                    textBox3.Text = "";
                    textBox7.Text = "";
                    currentstart = 0;
                }

                if (current != 0)
                {
                    textBox3.Text = current.ToString("0.##");
                }
                else
                {
                    textBox3.Text = "";
                }
            }
            else if (!radioButton8.Checked)
            {
                textBox3.Text = "";
            }
            else if (radioButton8.Checked)
            {
                if ((phase == "DC") && (loadtype == "Feeder"))
                {
                    textBox7.Text = "";
                    currentstart = 0;
                }
                else if (((phase == "Single-Phase AC") || (phase == "Three-Phase AC")) && (loadtype == "Motor") && (textBox25.Text != ""))
                {
                    currentstart = current * multiplier;
                    textBox7.Text = currentstart.ToString("0.##");
                }
                else
                {
                    textBox7.Text = "";
                    currentstart = 0;
                }
            }
            
        }

        private void calc_power()
        {
            if ((textBox3.Text != "") && (textBox2.Text != "") && (comboBox3.Text != "")
            && (comboBox2.Text != "") && (textBox4.Text != "") && (textBox5.Text != "") && (radioButton8.Checked))
            {
                if ((phase == "DC") && (loadtype == "Feeder"))
                {
                    power = current * voltage / 1000;
                    textBox7.Text = "";
                }
                else if (phase == "Single-Phase AC")
                {
                    power = current * voltage * eff * pf / 1000;
                }
                else if (phase == "Three-Phase AC")
                {
                    power = current * Math.Sqrt(3) * voltage * eff * pf / 1000;
                }
                else
                {
                    power = 0;
                }
                if (power != 0)
                {
                    TextBox1.Text = power.ToString("0.##");
                }
                else
                {
                    TextBox1.Text = "";
                }
            }
            else if (radioButton8.Checked)
            {
                TextBox1.Text = "";
                power = 0;
            }
        }

        private void enable_correction()
        {
            if (((comboBox9.Text == "E (Above Ground)") || (comboBox9.Text == "D1 (Under Ground)") || (comboBox9.Text == "D2 (Under Ground)")) && 
                ((comboBox6.Text == "PVC") || (comboBox6.Text == "XLPE") || (comboBox6.Text == "EPR")) && (!radioButton7.Checked))
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void reset_correction()
        {
            if (!radioButton7.Checked)
            {
                k1main = 0;
                k2main = 0;
                k3main = 0;
                ktmain = 0;

                textBox17.Text = "";
                textBox18.Text = "";
                textBox36.Text = "";
                textBox19.Text = "";
            }
        }

        private void enable_vd_btn()
        {
            if ((comboBox2.Text != "") && (comboBox3.Text != "") && (comboBox13.Text != "")&& 
                (textBox2.Text != "") && (textBox3.Text != "") && (textBox4.Text != "") && (textBox5.Text != "") && 
                (comboBox6.Text != "") &&(comboBox7.Text != "") && (comboBox8.Text != "") && (textBox9.Text != "") && 
                (comboBox14.Text != "") &&(comboBox9.Text != "") && (textBox19.Text != "") && (textBox6.Text != "") && 
                (textBox12.Text != "") && (comboBox5.Text != "")  && (comboBox4.Text != "") && (textBox13.Text != ""))
            {
                if (((radioButton3.Checked) && (cableCount > 0)) || (radioButton4.Checked))
                {
                    if ((loadtype == "Motor") && ConsiderVdStart)
                    {
                        if ((textBox7.Text != "") && (textBox14.Text != "") && (textBox11.Text != "") && (textBox25.Text != ""))
                        {
                            button7.Enabled = true;
                        }
                        else
                        {
                            button7.Enabled = false;
                        }
                    }
                    else
                    {
                        button7.Enabled = true;
                    }
                }
                else
                {
                    button7.Enabled = false;
                }
            }
            else
            {
                button7.Enabled = false;
            }
        }
        
        private void enable_result_btn()
        {
            if ((comboBox2.Text != "") && (comboBox3.Text != "")&& (textBox2.Text != "") && 
                (textBox3.Text != "") && (textBox4.Text != "") &&
                (textBox5.Text != "") && (textBox9.Text != "") && (textBox6.Text != "") && (textBox12.Text != "") && 
                (comboBox5.Text != "") && (comboBox6.Text != "") && (comboBox9.Text != "") && (textBox19.Text != "") && 
                (comboBox11.Text != "") && (comboBox4.Text != "") && (comboBox7.Text != "") && (comboBox8.Text != "") && 
                (comboBox10.Text != "") && (comboBox12.Text != "")&& (comboBox13.Text != "") && (comboBox14.Text != "") && 
                (textBox13.Text!= "") && (textBox15.Text != "") && (textBox24.Text != ""))
                {
                    if (((radioButton3.Checked) && (cableCount > 0)) || (radioButton4.Checked))
                    {
                        if (((radioButton2.Checked) && (textBox23.Text != "") && (textBox28.Text != "")) ||
                                ((radioButton1.Checked) && (textBox20.Text != "")))
                        {
                            if ((loadtype == "Motor") && ConsiderVdStart)
                        {
                                if ((textBox7.Text != "") && (textBox14.Text != "") && (textBox11.Text != "") && (textBox25.Text != ""))
                                {
                                    button2.Enabled = true;
                                }
                                else
                                {
                                    button2.Enabled = false;
                                }
                            }
                            else
                            {
                                button2.Enabled = true;
                            }
                        }
                        else
                        {
                            button2.Enabled = false;
                        }
                    }
                    else
                    {
                        button2.Enabled = false;
                    }
                }
                else
                {
                    button2.Enabled = false;
                }
        }

        //function to determine the need of calculation, when all SC/LTE data is empty, calculation is not needed
        private bool IsCalculateNeeded()
        {
            if ((textBox23.Text != "") || (textBox28.Text != "") || (textBox20.Text != ""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void breaker_fill()
        {
            comboBox11.Items.Clear();
            if (comboBox12.Text == "MCB")
            {
                if ((comboBox10.Text == "10") && ((voltage == 230) || (voltage == 400)))
                {
                    comboBox11.Items.Insert(0, "2");
                    comboBox11.Items.Insert(1, "4");
                    comboBox11.Items.Insert(2, "6");
                    comboBox11.Items.Insert(3, "10");
                    comboBox11.Items.Insert(4, "16");
                    comboBox11.Items.Insert(5, "25");
                    comboBox11.Items.Insert(6, "32");
                    comboBox11.Items.Insert(7, "40");
                    comboBox11.Items.Insert(8, "50");
                    comboBox11.Items.Insert(9, "63");
                }
                else if ((comboBox10.Text == "25") && ((voltage == 230) || (voltage == 400)))
                {
                    comboBox11.Items.Insert(0, "2");
                    comboBox11.Items.Insert(1, "4");
                    comboBox11.Items.Insert(2, "6");
                    comboBox11.Items.Insert(3, "10");
                    comboBox11.Items.Insert(4, "16");
                    comboBox11.Items.Insert(5, "25");
                    comboBox11.Items.Insert(6, "32");
                    comboBox11.Items.Insert(7, "40");
                    comboBox11.Items.Insert(8, "50");
                    comboBox11.Items.Insert(9, "63");
                }
            }
            else if (comboBox12.Text == "MCCB")
            {
                if ((comboBox10.Text == "10") && (voltage == 230))
                {
                    comboBox11.Items.Insert(0, "4");
                    comboBox11.Items.Insert(1, "6");
                    comboBox11.Items.Insert(2, "10");
                    comboBox11.Items.Insert(3, "16");
                    comboBox11.Items.Insert(4, "25");
                    comboBox11.Items.Insert(5, "32");
                    comboBox11.Items.Insert(6, "40");
                    comboBox11.Items.Insert(7, "50");
                    comboBox11.Items.Insert(8, "63");
                    comboBox11.Items.Insert(9, "80");
                    comboBox11.Items.Insert(10, "100");
                    comboBox11.Items.Insert(11, "125");
                    comboBox11.Items.Insert(12, "160");
                    comboBox11.Items.Insert(13, "250");
                    comboBox11.Items.Insert(14, "400");
                    comboBox11.Items.Insert(15, "630");
                }
                else if ((comboBox10.Text == "25") && (voltage == 400))
                {
                    comboBox11.Items.Insert(0, "4");
                    comboBox11.Items.Insert(1, "6");
                    comboBox11.Items.Insert(2, "10");
                    comboBox11.Items.Insert(3, "16");
                    comboBox11.Items.Insert(4, "20");
                    comboBox11.Items.Insert(5, "25");
                    comboBox11.Items.Insert(6, "32");
                    comboBox11.Items.Insert(7, "40");
                    comboBox11.Items.Insert(8, "50");
                    comboBox11.Items.Insert(9, "63");
                    comboBox11.Items.Insert(10, "80");
                    comboBox11.Items.Insert(11, "100");
                    comboBox11.Items.Insert(12, "125");
                    comboBox11.Items.Insert(13, "160");
                }
            }
        }


        private void break_lte()
        {
            if ((radioButton1.Checked) && (radioButton5.Checked) && (comboBox11.Text != ""))
            {
                if (comboBox12.Text == "MCB")
                {
                    if ((comboBox10.Text == "10") && ((voltage == 230) || (voltage == 400)))
                    {
                        switch (breakcurrent)
                        {
                            case 2:
                                bLTE = 800;
                                break;
                            case 4:
                                bLTE = 6800;
                                break;
                            case 6:
                                bLTE = 13000;
                                break;
                            case 10:
                                bLTE = 35000;
                                break;
                            case 16:
                                bLTE = 40000;
                                break;
                            case 25:
                                bLTE = 46000;
                                break;
                            case 32:
                                bLTE = 56000;
                                break;
                            case 40:
                                bLTE = 56000;
                                break;
                            case 50:
                                bLTE = 88000;
                                break;
                            case 63:
                                bLTE = 88000;
                                break;
                        }
                        sc_wiremin = 2.5;
                        textBox20.Text = bLTE.ToString();
                    }
                    else if ((comboBox10.Text == "25") && ((voltage == 230) || (voltage == 400)))
                    {
                        switch (breakcurrent)
                        {
                            case 2:
                                bLTE = 800;
                                sc_wiremin = 2.5;
                                break;
                            case 4:
                                bLTE = 8500;
                                sc_wiremin = 2.5;
                                break;
                            case 6:
                                bLTE = 15000;
                                sc_wiremin = 2.5;
                                break;
                            case 10:
                                bLTE = 51000;
                                sc_wiremin = 2.5;
                                break;
                            case 16:
                                bLTE = 54000;
                                sc_wiremin = 2.5;
                                break;
                            case 25:
                                bLTE = 66000;
                                sc_wiremin = 2.5;
                                break;
                            case 32:
                                bLTE = 130000;
                                sc_wiremin = 4;
                                break;
                            case 40:
                                bLTE = 130000;
                                sc_wiremin = 4;
                                break;
                            case 50:
                                bLTE = 160000;
                                sc_wiremin = 4;
                                break;
                            case 63:
                                bLTE = 160000;
                                sc_wiremin = 4;
                                break;
                        }
                        textBox20.Text = bLTE.ToString();
                    }
                    else
                    {
                        textBox20.Text = "";
                    }
                }
                else if (comboBox12.Text == "MCCB")
                {
                    if ((comboBox10.Text == "10") && (voltage == 230))
                    {
                        switch (breakcurrent)
                        {
                            case 4:
                                bLTE = 5300;
                                sc_wiremin = 2.5;
                                break;
                            case 6:
                                bLTE = 9400;
                                sc_wiremin = 2.5;
                                break;
                            case 10:
                                bLTE = 18000;
                                sc_wiremin = 2.5;
                                break;
                            case 16:
                                bLTE = 44000;
                                sc_wiremin = 2.5;
                                break;
                            case 25:
                                bLTE = 50000;
                                sc_wiremin = 2.5;
                                break;
                            case 32:
                                bLTE = 50000;
                                sc_wiremin = 2.5;
                                break;
                            case 40:
                                bLTE = 55000;
                                sc_wiremin = 2.5;
                                break;
                            case 50:
                                bLTE = 55000;
                                sc_wiremin = 2.5;
                                break;
                            case 63:
                                bLTE = 55000;
                                sc_wiremin = 2.5;
                                break;
                            case 80:
                                bLTE = 130000;
                                sc_wiremin = 4;
                                break;
                            case 100:
                                bLTE = 130000;
                                sc_wiremin = 4;
                                break;
                            case 125:
                                bLTE = 130000;
                                sc_wiremin = 4;
                                break;
                            case 160:
                                bLTE = 130000;
                                sc_wiremin = 4;
                                break;
                            case 250:
                                bLTE = 1300000;
                                sc_wiremin = 6;
                                break;
                            case 400:
                                bLTE = 1300000;
                                sc_wiremin = 6;
                                break;
                            case 630:
                                bLTE = 1300000;
                                sc_wiremin = 6;
                                break;
                        }
                        textBox20.Text = bLTE.ToString();
                    }
                    else if ((comboBox10.Text == "25") && (voltage == 400))
                    {
                        switch (breakcurrent)
                        {
                            case 4:
                                bLTE = 18000;
                                sc_wiremin = 2.5;
                                break;
                            case 6:
                                bLTE = 30000;
                                sc_wiremin = 2.5;
                                break;
                            case 10:
                                bLTE = 53000;
                                sc_wiremin = 2.5;
                                break;
                            case 16:
                                bLTE = 130000;
                                sc_wiremin = 4;
                                break;
                            case 20:
                                bLTE = 140000;
                                sc_wiremin = 4;
                                break;
                            case 25:
                                bLTE = 150000;
                                sc_wiremin = 4;
                                break;
                            case 32:
                                bLTE = 150000;
                                sc_wiremin = 4;
                                break;
                            case 40:
                                bLTE = 160000;
                                sc_wiremin = 4;
                                break;
                            case 50:
                                bLTE = 160000;
                                sc_wiremin = 4;
                                break;
                            case 63:
                                bLTE = 160000;
                                sc_wiremin = 4;
                                break;
                            case 80:
                                bLTE = 330000;
                                sc_wiremin = 4;
                                break;
                            case 100:
                                bLTE = 330000;
                                sc_wiremin = 4;
                                break;
                            case 125:
                                bLTE = 330000;
                                sc_wiremin = 4;
                                break;
                            case 160:
                                bLTE = 330000;
                                sc_wiremin = 4;
                                break;
                        }
                        textBox20.Text = bLTE.ToString();
                    }
                    else
                    {
                        textBox20.Text = "";
                    }
                }
                else
                {
                    textBox20.Text = "";
                }
            }
            else if ((radioButton1.Checked) && (radioButton6.Checked))
            {
                if (bLTE != 0)
                {
                    textBox20.Text = bLTE.ToString();
                }
            }
            else
            {
                textBox20.Text = "";
            }
        }


        private void cable_lte()
        {
            cLTE = wirearea * wirearea * k * k;
        }

        private void calc_smin()
        {
            if ((textBox28.Text != "") && (textBox23.Text != "") && (textBox21.Text != ""))
            {
                smin = sccurrent * 1000 * Math.Sqrt(tbreaker) / k;
                textBox30.Text = smin.ToString("0.##");
            }
            else
            {
                smin = 0;
                textBox30.Text = "";
            }
        }
        
        private void save_vd_result()
        {
            results[0] = tagno;
            results[1] = from;
            results[2] = fromdesc;
            results[3] = to;
            results[4] = todesc;
            results[5] = phase;
            results[6] = loadtype;
            results[7] = voltage.ToString("0.##");
            results[8] = installation;
            results[9] = power.ToString("0.##");
            results[10] = eff.ToString("0.##");
            results[11] = pf.ToString("0.##");
            results[12] = Math.Sqrt(1 - pf * pf).ToString("0.##");
            results[13] = pfstart.ToString("0.##");
            results[14] = current.ToString("0.##");
            results[15] = currentstart.ToString("0.##");
            results[16] = n.ToString("0.##");
            results[17] = wirearea.ToString("0.##");
            if (phase == "DC")
            {
                results[18] = "";
                results[19] = Rdc.ToString("0.####");
                results[20] = "";
            }
            else
            {
                results[18] = Rac.ToString("0.####");
                results[19] = "";
                results[20] = X.ToString("0.####");
            }
            results[21] = Irated.ToString("0.##");
            results[22] = ktmain.ToString("0.##");
            results[23] = iderated.ToString("0.##");
            results[24] = "";
            results[25] = "";
            results[26] = length.ToString("0.##");
            results[27] = lmax.ToString("0.##");
            results[28] = vdrun.ToString("0.##");
            results[29] = vdrunmax.ToString("0.##");
            results[30] = vdstart.ToString("0.##");
            results[31] = vdstartmax.ToString("0.##");
            results[32] = "";
            results[33] = "";
            results[34] = "";
            results[35] = "";
            results[36] = cLTE.ToString("0.##");
            results[37] = readtemp;
            results[38] = remarks;

            for (int i = 0; i < 38; i++)
            {
                if ((results[i] == "0") || (results[i] == null) || (results[i] == ""))
                {
                    results[i] = "N/A";
                }
            }

            //update data
            dtr = dtdiameter.NewRow();
            //cable OD
            if (diameter != 0)
            {
                dtr[0] = diameter;
            }
            else
            {
                dtr[0] = "N/A";
            }
            //full data
            dtr[1] = tagno;
            dtr[2] = from;
            dtr[3] = fromdesc;
            dtr[4] = to;
            dtr[5] = todesc;
            dtr[6] = phase;
            dtr[7] = loadtype;
            dtr[8] = volSys;
            if (!radioButton8.Checked) //Manual current input or not
            {
                dtr[9] = false;
                dtr[10] = cbPower.Text; //powerdata
                if (Convert.ToString(dtr[10]) == "kW") //power
                {
                    dtr[11] = power;
                }
                else if (Convert.ToString(dtr[10]) == "kV")
                {
                    dtr[11] = cplxpower;
                }
                else if (Convert.ToString(dtr[10]) == "HP")
                {
                    dtr[11] = hp;
                }
                dtr[12] = current;
            }
            else
            {
                dtr[9] = true;
                dtr[10] = cbPower.Text;
                dtr[11] = TextBox1.Text;
                dtr[12] = current;
            }
            dtr[13] = voltage;
            dtr[14] = eff;
            dtr[15] = pf;

            if ((loadtype == "Motor") && ConsiderVdStart)
            {
                dtr[16] = pfstart;
                dtr[17] = multiplier;
                dtr[31] = vdstartmax;
                dtr[34] = vdstart;
            }
            else
            {
                dtr[16] = "";
                dtr[17] = "";
                dtr[31] = "";
                dtr[34] = "";
            }

            dtr[18] = ratedvoltage;
            dtr[19] = material;
            dtr[20] = insulation;
            dtr[21] = armour;
            dtr[22] = outersheath;
            dtr[23] = installation;
            if (radioButton7.Checked) //Manual derating input or not
            {
                dtr[24] = true;
            }
            else
            {
                dtr[24] = false;
            }
            dtr[25] = k1main;
            dtr[26] = k2main;
            dtr[27] = k3main;
            dtr[28] = ktmain;
            dtr[29] = length;
            dtr[30] = vdrunmax;
            if (radioButton4.Checked) //vendor/manual cable properties input
            {
                dtr[32] = "vendor";
                dtr[55] = comboBoxVendor.Text;
            }
            else if (radioButton3.Checked)
            {
                dtr[32] = "manual";
                dtr[55] = "";
            }
            dtr[33] = vdrun;
            dtr[35] = lmax;
            dtr[36] = n;
            dtr[37] = cores;
            dtr[38] = wirearea;
            if (loadtype == "DC")
            {
                dtr[39] = Rdc;
                dtr[40] = "";
            }
            else
            {
                dtr[39] = Rac;
                dtr[40] = X;
            }
            dtr[41] = Irated;
            dtr[42] = iderated;
            dtr[43] = "";
            dtr[44] = "";
            dtr[45] = "";
            dtr[46] = initialTemp;
            dtr[47] = finalTemp;
            dtr[48] = cLTE;
            dtr[54] = i;


            for (int i = 49; i <54; i++)
            {
                dtr[i] = "";
            }

        }

        private void save_result()
        {
            results[0] = tagno;
            results[1] = from;
            results[2] = fromdesc;
            results[3] = to;
            results[4] = todesc;
            results[5] = phase;
            results[6] = loadtype;
            results[7] = voltage.ToString("0.##");
            results[8] = installation;
            results[9] = power.ToString("0.##");
            results[10] = eff.ToString("0.##");
            results[11] = pf.ToString("0.##");
            results[12] = Math.Sqrt(1 - pf * pf).ToString("0.##");
            results[13] = pfstart.ToString("0.##");
            results[14] = current.ToString("0.##");
            results[15] = currentstart.ToString("0.##");
            results[16] = n.ToString("0.##");
            results[17] = wirearea.ToString("0.##");
            if (phase == "DC")
            {
                results[18] = "";
                results[19] = Rdc.ToString("0.####");
                results[20] ="";
            }
            else
            {
                results[18] = Rac.ToString("0.####");
                results[19] = "";
                results[20] = X.ToString("0.####");
            }
            results[21] = Irated.ToString("0.##");
            results[22] = ktmain.ToString("0.##");
            results[23] = iderated.ToString("0.##");
            results[24] = breakertype;
            results[25] = breakcurrent.ToString("0.##");
            results[26] = length.ToString("0.##");
            results[27] = lmax.ToString("0.##");
            results[28] = vdrun.ToString("0.##");
            results[29] = vdrunmax.ToString("0.##");
            results[30] = vdstart.ToString("0.##");
            results[31] = vdstartmax.ToString("0.##");
            if (radioButton2.Checked)
            {
                results[32] = sccurrent.ToString("0.##");
                results[33] = tbreaker.ToString("0.##");
            }
            else
            {
                results[32] = "";
                results[33] = "";
            }
            results[34] = smin.ToString("0.##");
            if (radioButton1.Checked)
            {
                results[35] = bLTE.ToString("0.##");
            }
            else
            {
                results[35] = "";
            }
            results[36] = cLTE.ToString("0.##");
            results[37] = readtemp;
            results[38] = remarks;

            for (int i = 0; i < 38; i++)
            {
                if ((results[i] == "0") || (results[i] == null) || (results[i] == ""))
                {
                    results[i] = "N/A";
                }
            }

            dtr = dtdiameter.NewRow();
            //cable OD
            if (diameter != 0)
            {
                dtr[0] = diameter;
            }
            else
            {
                dtr[0] = "N/A";
            }
            //full data
            dtr[1] = tagno;
            dtr[2] = from;
            dtr[3] = fromdesc;
            dtr[4] = to;
            dtr[5] = todesc;
            dtr[6] = phase;
            dtr[7] = loadtype;
            dtr[8] = volSys;
            if (!radioButton8.Checked) //Manual current input or not
            {
                dtr[9] = false;
                dtr[10] = cbPower.Text; //powerdata
                if (Convert.ToString(dtr[10]) == "kW") //power
                {
                    dtr[11] = power;
                }
                else if (Convert.ToString(dtr[10]) == "kV")
                {
                    dtr[11] = cplxpower;
                }
                else if (Convert.ToString(dtr[10]) == "HP")
                {
                    dtr[11] = hp;
                }
                dtr[12] = current;
            }
            else
            {
                dtr[9] = true;
                dtr[10] = cbPower.Text;
                dtr[11] = TextBox1.Text;
                dtr[12] = current;
            }
            dtr[13] = voltage;
            dtr[14] = eff;
            dtr[15] = pf;

            if ((loadtype == "Motor") && ConsiderVdStart)
            {
                dtr[16] = pfstart;
                dtr[17] = multiplier;
                dtr[31] = vdstartmax;
                dtr[34] = vdstart;
            }
            else
            {
                dtr[16] = "";
                dtr[17] = "";
                dtr[31] = "";
                dtr[34] = "";
            }

            dtr[18] = ratedvoltage;
            dtr[19] = material;
            dtr[20] = insulation;
            dtr[21] = armour;
            dtr[22] = outersheath;
            dtr[23] = installation;
            if (radioButton7.Checked) //Manual derating input or not
            {
                dtr[24] = true;
            }
            else
            {
                dtr[24] = false;
            }
            dtr[25] = k1main;
            dtr[26] = k2main;
            dtr[27] = k3main;
            dtr[28] = ktmain; 
            dtr[29] = length;
            dtr[30] = vdrunmax;
            if (radioButton4.Checked) //vendor/manual cable properties input
            {
                dtr[32] = "vendor";
                dtr[55] = comboBoxVendor.Text;
            }
            else if (radioButton3.Checked)
            {
                dtr[32] = "manual";
                dtr[55] = "";
            }
            dtr[33] = vdrun;
            dtr[35] = lmax;
            dtr[36] = n;
            dtr[37] = cores;
            dtr[38] = wirearea;
            if (loadtype == "DC")
            {
                dtr[39] = Rdc;
                dtr[40] = "";
            }
            else
            {
                dtr[39] = Rac;
                dtr[40] = X;
            }
            dtr[41] = Irated;
            dtr[42] = iderated;
            if (radioButton2.Checked) //using S.C. current or LTE
            {
                dtr[43] = "sc";
                dtr[44] = sccurrent;
                dtr[45] = tbreaker;
                dtr[53] = "";
            }
            else
            {
                dtr[43] = "lte";
                dtr[44] = "";
                dtr[45] = "";
                dtr[53] = bLTE;
            }
            dtr[46] = initialTemp;
            dtr[47] = finalTemp;
            dtr[48] = cLTE;
            dtr[49] = breakertype;
            if (radioButton5.Checked)
            {
                dtr[50] = "vendor";
            }
            else if (radioButton6.Checked)
            {
                dtr[50] = "manual";
            }
            dtr[51] = scrating;
            dtr[52] = breakcurrent;
            dtr[54] = i;

        }

        private void enable_save()
        {
            button4.Enabled = true;
            addToolStripMenuItem.Enabled = true;
            button3.Enabled = true;
            toolTip1.SetToolTip(button4, "Add calculated cable size data to data table");
            toolTip1.SetToolTip(button3, "Add calculated cable size data to data table and save the data table");
        }

        private void disable_save()
        {
            button4.Enabled = false;
            addToolStripMenuItem.Enabled = false;
            button3.Enabled = false;
            toolTip1.SetToolTip(button4, null);
            toolTip1.SetToolTip(button3, null);
        }

        internal void LoadIECDatabase()
        {
            //read all file in database directory
            string systemPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Directory.CreateDirectory(systemPath + "/Cable Sizing/IEC_database");
            string saveDir = (systemPath + "/Cable Sizing/IEC_database");

            di = new DirectoryInfo(saveDir);
            //get all file name in database directory
            FileInfo[] files = di.GetFiles("*.xml");
            IECDatabaseFiles = files.Length;
            //fill vendor data from database
            comboBoxVendor.Items.Clear();
            comboBoxVendor.Items.Insert(0, "Sumi Indo Cable (Default)"); //default, hardcoded-to-program database
            comboBoxVendor.SelectedIndex = 0;
            //fill all saved database created by user
            for (int z = 0; z < IECDatabaseFiles; z++)
            {
                string tempstring;
                tempstring = files[z].ToString();
                tempstring = tempstring.Replace(".xml", "");
                comboBoxVendor.Items.Insert(z + 1, tempstring);
            }
        }

        internal void ReadIECDatabase()
        {
            string systemPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Directory.CreateDirectory(systemPath + "/Cable Sizing/IEC_database");
            string saveDir = Path.Combine(systemPath + "/Cable Sizing/IEC_database", SelectedDatabase + ".xml");

            //save database to a new dataset
            cableDS = new DataSet();
            cableDS.ReadXml(saveDir);

            //save each cable data table to their respective datatable
            if (cableDS.Tables.Contains("XLPE_2CORE"))
            {
                dtXLPE2DB = cableDS.Tables["XLPE_2CORE"].Copy();
                xlpe2coreDB = new double[dtXLPE2DB.Rows.Count, dtXLPE2DB.Columns.Count];
                DTToArrayDouble(dtXLPE2DB, xlpe2coreDB);
                xlpe2coreLength = dtXLPE2DB.Rows.Count;
            }
            else
            {
                xlpe2coreLength = 0;
            }
            if (cableDS.Tables.Contains("XLPE_3CORE"))
            {
                dtXLPE3DB = cableDS.Tables["XLPE_3CORE"].Copy();
                xlpe3coreDB = new double[dtXLPE3DB.Rows.Count, dtXLPE3DB.Columns.Count];
                DTToArrayDouble(dtXLPE3DB, xlpe3coreDB);
                xlpe3coreLength = dtXLPE3DB.Rows.Count;
            }
            else
            {
                xlpe3coreLength = 0;
            }
            if (cableDS.Tables.Contains("XLPE_4CORE"))
            {
                dtXLPE4DB = cableDS.Tables["XLPE_4CORE"].Copy();
                xlpe4coreDB = new double[dtXLPE4DB.Rows.Count, dtXLPE4DB.Columns.Count];
                DTToArrayDouble(dtXLPE4DB, xlpe4coreDB);
                xlpe4coreLength = dtXLPE4DB.Rows.Count;
            }
            else
            {
                xlpe4coreLength = 0;
            }
            if (cableDS.Tables.Contains("PVC_2CORE"))
            {
                dtPVC2DB = cableDS.Tables["PVC_2CORE"].Copy();
                pvc2coreDB = new double[dtPVC2DB.Rows.Count, dtPVC2DB.Columns.Count];
                DTToArrayDouble(dtPVC2DB,pvc2coreDB);
                pvc2coreLength = dtPVC2DB.Rows.Count;
            }
            else
            {
                pvc2coreLength = 0;
            }
            if (cableDS.Tables.Contains("PVC_3CORE"))
            {
                dtPVC3DB = cableDS.Tables["PVC_3CORE"].Copy();
                pvc3coreDB = new double[dtPVC3DB.Rows.Count, dtPVC3DB.Columns.Count];
                DTToArrayDouble(dtPVC3DB, pvc3coreDB);
                pvc3coreLength = dtPVC3DB.Rows.Count;
            }
            else
            {
                pvc3coreLength = 0;
            }
            if (cableDS.Tables.Contains("PVC_4CORE"))
            {
                dtPVC4DB = cableDS.Tables["PVC_4CORE"].Copy();
                pvc4coreDB = new double[dtPVC4DB.Rows.Count, dtPVC4DB.Columns.Count];
                DTToArrayDouble(dtPVC4DB, pvc4coreDB);
                pvc4coreLength = dtPVC4DB.Rows.Count;
            }
            else
            {
                pvc4coreLength = 0;
            }
        }

        private void DTToArrayDouble(DataTable dt, double[,] arr)
        {
            int dtRow = dt.Rows.Count;
            int dtColumn = dt.Columns.Count;

            int row = 0;
            foreach (DataRow dr in dt.Rows)
            {
                for (int col = 0; col < dtColumn; col++)
                {
                    arr[row, col] = Convert.ToDouble(dr[col]);
                }
                row++;
            }
        }

    }
}
