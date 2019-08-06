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

    public partial class Form9 : GradientForm
    {
        double current;
        double currentstart;
        double voltage;
        double power;
        double pf, eff;
        string phase;
        string loadtype;
        double hp;
        double pfstart;
        double multiplier;
        double sccurrent;
        double cplxpower;
        string materialname;
        public static double initialTemp = 0;
        double finalTemp;
        double diameter = 0;
        double temperature;
        double scrating;

        double wirearea;

        double vdrunmax, vdstartmax, vdrun, vdstart;
        double length;
        double maxtemp;

        double number;
        double cmil;

        double n_cmil;

        int n = 1;
        string volSys, powerUnit;
        string tagno, from, to, fromdesc, todesc;
        public static string material;
        string innersheath;
        string outersheath = "-";
        string armour = "-";
        string breakertype;
        string remarks;
        string ratedvoltage;
        string groupconductor;
        public static string insulation, installation;
        string lengthunit;
        string conduit;
        string wirearea_unit;
        string wirearea_nec;
        string smin_nec;
        string cablesizemin_nec;

        int cores;
        double breakcurrent;

        public static double k1main, k2main, k3main, ktmain;

        bool ConsiderVdStart;

        bool EditingState = false;

        bool complete, inputValid;
        int i = 0;
        int insulindex = 0;
        int tempindex = 0;

        int tempCurrentRow;

        string readtemp;
        double Rac;
        double X;
        double Rdc;
        double Irated;
        double iderated;

        double tbreaker;
        double bLTE;
        double cLTE;
        double k;
        double sc_wiremin;
        double lmax;
        double smin;
        double cablesizemin;
        double wirearea_metric;

        bool calculated = false;

        int m;

        string voltageLv;

        DirectoryInfo di;
        int NECDatabaseFiles;
        string SelectedDatabase;
        DataSet cableDS;

        DataTable NEC00DB;
        DataTable NEC10DB;
        DataTable NEC20DB;
        DataTable NEC01DB;
        DataTable NEC11DB;
        DataTable NEC21DB;

        //selected custom database type
        double[,] nec_selected_data_electrical;
        string[] nec_selected_wirearea;
        string[] nec_selected_wirearea_unit;
        double[] nec_selected_wirearea_metric;

        //save custom NEC database electrical data
        double[,] nec00DB_data_electrical;
        double[,] nec10DB_data_electrical;
        double[,] nec20DB_data_electrical;
        double[,] nec01DB_data_electrical;
        double[,] nec11DB_data_electrical;
        double[,] nec21DB_data_electrical;

        //save custom NEC database wirearea in AWG/kcmil
        string[] nec00DB_wirearea;
        string[] nec10DB_wirearea;
        string[] nec20DB_wirearea;
        string[] nec01DB_wirearea;
        string[] nec11DB_wirearea;
        string[] nec21DB_wirearea;

        //save custom NEC database wire area units(AWG/kcmil)
        string[] nec00DB_wirearea_unit;
        string[] nec10DB_wirearea_unit;
        string[] nec20DB_wirearea_unit;
        string[] nec01DB_wirearea_unit;
        string[] nec11DB_wirearea_unit;
        string[] nec21DB_wirearea_unit;


        //save custom NEC database wirearea in mm²
        double[] nec00DB_wirearea_metric;
        double[] nec10DB_wirearea_metric;
        double[] nec20DB_wirearea_metric;
        double[] nec01DB_wirearea_metric;
        double[] nec11DB_wirearea_metric;
        double[] nec21DB_wirearea_metric;




        int nec00Length;
        int nec10Length;
        int nec20Length;
        int nec01Length;
        int nec11Length;
        int nec21Length;

        //store currently used datalength for size updating purpose while using custom database
        int currentDataLength;

        //public static int j = -1;
        Form5 f5 = new Form5();
        Form6 f6 = new Form6();
        Form10 f10 = new Form10();
        Form8 f8;
        FSettings fSettings = new FSettings();
        FormAbout fAbout = new FormAbout();
        FormAddCableDatabase faddcable;

        public static string[] results = new string[39];

        public static string[,] inputCableData_nec;
        public static double[] inputCableData_nec_metric;
        public static string[] inputCableData_nec_unit;
        public static double[,] inputCableData;
        public static int cableCount;

        int index_temperature;
        int index_groupedconductor;
        int index_distanceaboveroof;
        int index_cablecover;
        int index_conduit;

        DataRow dtr;

        DataTable dtReset;

        //public static DataTable dtdiameter = new DataTable();


        public Form9()
        {

            InitializeComponent();

        }

        string[] data_wirearea = new string[21]
        {
            "14",
            "12",
            "10",
            "8",
            "6",
            "4",
            "3",
            "2",
            "1",
            "1/0",
            "2/0",
            "3/0",
            "4/0",
            "250",
            "300",
            "350",
            "400",
            "500",
            "600",
            "750",
            "1000"
        };

        double[] data_wirearea_metric = new double[21]
        {
            2.08,
            3.31,
            5.26,
            8.36,
            13.3,
            21.2,
            26.7,
            33.6,
            42.4,
            53.5,
            67.4,
            85,
            107,
            127,
            152,
            177,
            203,
            253,
            304,
            380,
            507
        };

        string[] data_wirearea_unit = new string[21]
        {
            "AWG",
            "AWG",
            "AWG",
            "AWG",
            "AWG",
            "AWG",
            "AWG",
            "AWG",
            "AWG",
            "AWG",
            "AWG",
            "AWG",
            "AWG",
            "kcmil",
            "kcmil",
            "kcmil",
            "kcmil",
            "kcmil",
            "kcmil",
            "kcmil",
            "kcmil"
        };

        double[,] data_ampacity_racewaycableearth = new double[21, 6]
        {
            { 20, 20, 25, 0, 0, 0 },
            { 25, 25, 30, 20, 20, 25 },
            { 30, 35, 40, 25, 30, 35 },
            { 40, 50, 55, 30, 40, 45 },
            { 55, 65, 75, 40, 50, 60 },
            { 70, 85, 95, 55, 65, 75 },
            { 85, 100, 110, 65, 75, 85 },
            { 95, 115, 130, 75, 90, 100 },
            { 110, 130, 150, 85, 100, 115 },
            { 125, 150, 170, 100, 120, 135 },
            { 145, 175, 195, 115, 135, 150 },
            { 165, 200, 225, 130, 155, 175 },
            { 195, 230, 260, 150, 180, 205 },
            { 215, 255, 290, 170, 205, 230 },
            { 240, 285, 320, 190, 230, 255 },
            { 260, 310, 350, 210, 250, 280 },
            { 280, 335, 380, 225, 270, 305 },
            { 320, 380, 430, 260, 310, 350 },
            { 355, 420, 475, 285, 340, 385 },
            { 400, 475, 535, 320, 385, 435 },
            { 455, 545, 615, 375, 445, 500 }
        };

        double[,] data_ampacity_freeair = new double[21, 6]
        {
            { 25, 30, 35, 0, 0, 0 },
            { 30, 35, 40, 25, 30, 35 },
            { 40, 50, 55, 35, 40, 40 },
            { 60, 70, 80, 45, 55, 60 },
            { 80, 95, 105, 60, 75, 80 },
            { 105, 125, 140, 80, 100, 110 },
            { 120, 145, 165, 95, 115, 130 },
            { 140, 170, 190, 110, 135, 150 },
            { 165, 195, 220, 130, 155, 175 },
            { 195, 230, 260, 150, 180, 205 },
            { 225, 265, 300, 175, 210, 235 },
            { 260, 310, 350, 200, 240, 275 },
            { 300, 360, 405, 235, 280, 315 },
            { 340, 405, 455, 265, 315, 355 },
            { 375, 445, 505, 290, 350, 395 },
            { 420, 505, 570, 330, 395, 445 },
            { 455, 545, 615, 355, 425, 480 },
            { 515, 620, 700, 405, 485, 545 },
            { 575, 690, 780, 455, 540, 615 },
            { 655, 785, 885, 515, 620, 700 },
            { 780, 935, 1055, 625, 750, 845 }
        };


        double[,] data_electrical = new double[21, 14]
        {
            { 0.058, 0.073, 3.1, 3.1, 3.1, 0, 0, 0, 2.7, 2.7, 2.7, 0, 0, 0 },
            { 0.054, 0.068, 2, 2, 2, 3.2, 3.2, 3.2, 1.7, 1.7, 1.7, 2.8, 2.8, 2.8 },
            { 0.05, 0.063, 1.2, 1.2, 1.2, 2, 2, 2, 1.1, 1.1, 1.1, 1.8, 1.8, 1.8 },
            { 0.052, 0.065, 0.78, 0.78, 0.78, 1.3, 1.3, 1.3, 0.69, 0.69, 0.7, 1.1, 1.1, 1.1 },
            { 0.051, 0.064, 0.49, 0.49, 0.49, 0.81, 0.81, 0.81, 0.44, 0.45, 0.45, 0.71, 0.72, 0.72 },
            { 0.048, 0.06, 0.31, 0.31, 0.31, 0.51, 0.51, 0.51, 0.29, 0.29, 0.3, 0.46, 0.46, 0.46 },
            { 0.047, 0.059, 0.25, 0.25, 0.25, 0.4, 0.41, 0.4, 0.23, 0.24, 0.24, 0.37, 0.37, 0.37 },
            { 0.045, 0.057, 0.19, 0.2, 0.2, 0.32, 0.32, 0.32, 0.19, 0.19, 0.2, 0.3, 0.3, 0.3 },
            { 0.046, 0.057, 0.15, 0.16, 0.16, 0.25, 0.26, 0.25, 0.16, 0.16, 0.16, 0.24, 0.24, 0.25 },
            { 0.044, 0.055, 0.12, 0.13, 0.12, 0.2, 0.21, 0.2, 0.13, 0.13, 0.13, 0.19, 0.2, 0.2 },
            { 0.043, 0.054, 0.1, 0.1, 0.1, 0.16, 0.16, 0.16, 0.11, 0.11, 0.11, 0.16, 0.16, 0.16 },
            { 0.042, 0.052, 0.077, 0.082, 0.079, 0.13, 0.13, 0.13, 0.088, 0.092, 0.094, 0.13, 0.13, 0.14 },
            { 0.041, 0.051, 0.062, 0.067, 0.063, 0.1, 0.11, 0.1, 0.074, 0.078, 0.08, 0.11, 0.11, 0.11 },
            { 0.041, 0.052, 0.052, 0.057, 0.054, 0.085, 0.09, 0.086, 0.066, 0.07, 0.073, 0.094, 0.098, 0.1 },
            { 0.041, 0.051, 0.044, 0.049, 0.045, 0.071, 0.076, 0.072, 0.059, 0.063, 0.065, 0.082, 0.086, 0.088 },
            { 0.04, 0.05, 0.038, 0.043, 0.039, 0.061, 0.066, 0.063, 0.053, 0.058, 0.06, 0.073, 0.077, 0.08 },
            { 0.04, 0.049, 0.033, 0.038, 0.035, 0.054, 0.059, 0.055, 0.049, 0.053, 0.056, 0.066, 0.071, 0.073 },
            { 0.039, 0.048, 0.027, 0.032, 0.029, 0.043, 0.048, 0.045, 0.043, 0.048, 0.05, 0.057, 0.061, 0.064 },
            { 0.039, 0.048, 0.023, 0.028, 0.025, 0.036, 0.041, 0.038, 0.04, 0.044, 0.047, 0.051, 0.055, 0.058 },
            { 0.038, 0.048, 0.019, 0.024, 0.021, 0.029, 0.034, 0.031, 0.036, 0.04, 0.043, 0.045, 0.049, 0.052 },
            { 0.037, 0.046, 0.015, 0.019, 0.018, 0.023, 0.027, 0.025, 0.032, 0.036, 0.04, 0.039, 0.042, 0.046 }
        };


        double[,] correctionfactor_temperature = new double[10, 6]
        {
            { 1.08, 1.05, 1.04, 1.08, 1.05, 1.04 },
            { 1, 1, 1, 1, 1, 1 },
            { 0.91, 0.94, 0.96, 0.91, 0.94, 0.96 },
            { 0.82, 0.88, 0.91, 0.82, 0.88, 0.91 },
            { 0.71, 0.82, 0.87, 0.71, 0.82, 0.87 },
            { 0.58, 0.75, 0.82, 0.58, 0.75, 0.82 },
            { 0.41, 0.67, 0.76, 0.41, 0.67, 0.76 },
            { 0, 0.58, 0.71, 0, 0.58, 0.71 },
            { 0, 0.33, 0.58, 0, 0.33, 0.58 },
            { 0, 0, 0.41, 0, 0, 0.41 }
        };


        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void TextBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Form1.decimalseparator))
            {
                e.Handled = true;
            }

            if ((TextBox1.Text == "") && (e.KeyChar == Form1.decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == Form1.decimalseparator) && ((sender as TextBox).Text.IndexOf(Form1.decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Form1.decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox2.Text == "") && (e.KeyChar == Form1.decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == Form1.decimalseparator) && ((sender as TextBox).Text.IndexOf(Form1.decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Form1.decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox3.Text == "") && (e.KeyChar == Form1.decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == Form1.decimalseparator) && ((sender as TextBox).Text.IndexOf(Form1.decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Form1.decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox6.Text == "") && (e.KeyChar == Form1.decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == Form1.decimalseparator) && ((sender as TextBox).Text.IndexOf(Form1.decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Form1.decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox8.Text == "") && (e.KeyChar == Form1.decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == Form1.decimalseparator) && ((sender as TextBox).Text.IndexOf(Form1.decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }
        private void TextBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Form1.decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox9.Text == "") && (e.KeyChar == Form1.decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == Form1.decimalseparator) && ((sender as TextBox).Text.IndexOf(Form1.decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Form1.decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox10.Text == "") && (e.KeyChar == Form1.decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == Form1.decimalseparator) && ((sender as TextBox).Text.IndexOf(Form1.decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Form1.decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox11.Text == "") && (e.KeyChar == Form1.decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == Form1.decimalseparator) && ((sender as TextBox).Text.IndexOf(Form1.decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Input_Enter(object sender, EventArgs e)
        {
            calc_current();
        }

        private void TextBox1_Leave(object sender, EventArgs e)
        {
            calc_current();

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

            DisableUndoReset();
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

            DisableUndoReset();
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

            DisableUndoReset();
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
            DisableUndoReset();
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
                if (lengthunit == "m")
                {
                    length = 3.28084 * double.Parse(textBox6.Text);
                }
                else
                {
                    length = double.Parse(textBox6.Text);
                }
                panel25.BackColor = Color.Transparent;
            }
            else
            {
                length = 0;
                panel25.BackColor = Color.Red;
            }

            DisableUndoReset();
            Updatek3();

            Updatekt();

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
            DisableUndoReset();
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

            DisableUndoReset();
            enable_vd_btn();
            enable_result_btn();
        }


        private void Form9_Load(object sender, EventArgs e)
        {

            Form1.OpenFromMain = false;
            Form1.FileOpened = false;

            //Load and apply color theme
            LoadColor();
            SaveAllColor();

            Form1.j = -1;
            eff = 1;

            OpenForm.formMainClose = false;
            if (Form5.f7.IsDisposed)
            {
                Form5.f7 = new Form7();
            }

            //set new dtdiameter everytime this form is loaded
            Form1.dtdiameter = new DataTable();
            Form1.dtdiameter.TableName = "Data";

            //set dtdiameter columns
            Form1.dtdiameter.Columns.Add("Diameter");
            Form1.dtdiameter.Columns.Add("TagNo");
            Form1.dtdiameter.Columns.Add("From");
            Form1.dtdiameter.Columns.Add("FromDesc");
            Form1.dtdiameter.Columns.Add("To");
            Form1.dtdiameter.Columns.Add("ToDesc");
            Form1.dtdiameter.Columns.Add("Phase");
            Form1.dtdiameter.Columns.Add("LoadType");
            Form1.dtdiameter.Columns.Add("VoltageSystem");
            Form1.dtdiameter.Columns.Add("CurrentButton");
            Form1.dtdiameter.Columns.Add("cbPower");
            Form1.dtdiameter.Columns.Add("Power");
            Form1.dtdiameter.Columns.Add("Current");
            Form1.dtdiameter.Columns.Add("Voltage");
            Form1.dtdiameter.Columns.Add("Efficiency");
            Form1.dtdiameter.Columns.Add("PF");
            Form1.dtdiameter.Columns.Add("PFStart");
            Form1.dtdiameter.Columns.Add("Multiplier");
            Form1.dtdiameter.Columns.Add("RatedVoltage");
            Form1.dtdiameter.Columns.Add("Material");
            Form1.dtdiameter.Columns.Add("Insulation");
            Form1.dtdiameter.Columns.Add("Armour");
            Form1.dtdiameter.Columns.Add("OutherSheath");
            Form1.dtdiameter.Columns.Add("Installation");
            Form1.dtdiameter.Columns.Add("DeratingButton");
            Form1.dtdiameter.Columns.Add("K1");
            Form1.dtdiameter.Columns.Add("K2");
            Form1.dtdiameter.Columns.Add("K3");
            Form1.dtdiameter.Columns.Add("Kt");
            Form1.dtdiameter.Columns.Add("Length");
            Form1.dtdiameter.Columns.Add("VdRunMax");
            Form1.dtdiameter.Columns.Add("VdStartMax");
            Form1.dtdiameter.Columns.Add("CablePropButton");
            Form1.dtdiameter.Columns.Add("VdRun");
            Form1.dtdiameter.Columns.Add("VdStart");
            Form1.dtdiameter.Columns.Add("Lmax");
            Form1.dtdiameter.Columns.Add("N");
            Form1.dtdiameter.Columns.Add("Cores");
            Form1.dtdiameter.Columns.Add("WireArea");
            Form1.dtdiameter.Columns.Add("Rac");
            Form1.dtdiameter.Columns.Add("X");
            Form1.dtdiameter.Columns.Add("Irated");
            Form1.dtdiameter.Columns.Add("Iderated");
            Form1.dtdiameter.Columns.Add("SCOrLTE");
            Form1.dtdiameter.Columns.Add("SCCurrent");
            Form1.dtdiameter.Columns.Add("TBreak");
            Form1.dtdiameter.Columns.Add("InitialTemp");
            Form1.dtdiameter.Columns.Add("FinalTemp");
            Form1.dtdiameter.Columns.Add("CLTE");
            Form1.dtdiameter.Columns.Add("BreakerType");
            Form1.dtdiameter.Columns.Add("BreakerBtn");
            Form1.dtdiameter.Columns.Add("SCRating");
            Form1.dtdiameter.Columns.Add("BreakNominalCurrent");
            Form1.dtdiameter.Columns.Add("BLTE");
            Form1.dtdiameter.Columns.Add("i");
            Form1.dtdiameter.Columns.Add("Temperature");
            Form1.dtdiameter.Columns.Add("IndexTemperature");
            Form1.dtdiameter.Columns.Add("IndexGroupedConductor");
            Form1.dtdiameter.Columns.Add("IndexDistanceAboveRoof");
            Form1.dtdiameter.Columns.Add("IndexCableCover");
            Form1.dtdiameter.Columns.Add("Conduit");
            Form1.dtdiameter.Columns.Add("IndexConduit");
            Form1.dtdiameter.Columns.Add("IndexLength");
            Form1.dtdiameter.Columns.Add("Remarks");
            Form1.dtdiameter.Columns.Add("Result");
            Form1.dtdiameter.Columns.Add("CustomDatabase");


            //load saved/default settings
            Form1.decimalseparator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);



            //Set datatable column header
            f5.dataGridView1.Columns[18].HeaderText = "Size (AWG or kcmil)";
            f5.dataGridView1.Columns[20].HeaderText = "Rdc (Ω/kft)";
            f5.dataGridView1.Columns[19].HeaderText = "Rac (Ω/kft)";
            f5.dataGridView1.Columns[21].HeaderText = "X (Ω/kft)";
            f5.dataGridView1.Columns[27].HeaderText = "Length (ft)";
            f5.dataGridView1.Columns[28].HeaderText = "Lmax (ft)";
            f5.dataGridView1.Columns[27].HeaderText = "Length (ft)";
            f5.dataGridView1.Columns[35].HeaderText = "Minimum Cable Size Due to S.C. (AWG or kcmil)";

            Form5.f7.dataGridView1.Columns[2].HeaderText = "Cable Total Length (ft)";

            //Load Custom NEC Database
           LoadNECDatabase();

            cbPower.Text = "kW";
            comboBox1.SelectedIndex = 1;

            Form5.Standard = 2;

            //save default ktmain position

            kttextboxX = textBox19.Location.X;
            kttextboxY = textBox19.Location.Y;
            ktlabelX = label43.Location.X;
            ktlabelY = label43.Location.Y;
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
            DisableUndoReset();
            enable_vd_btn();
            Calc_k();
            enable_result_btn();
        }

        private void ComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            insulation = comboBox6.Text;
            comboBox16.SelectedIndex = -1;
            comboBox17.SelectedIndex = -1;
            k1main = 0;
            k2main = 0;
            k3main = 0;

            if ((insulation == "TW") || (insulation == "UF"))
            {
                insulindex = 1;
                comboBox17.Items.Clear();
                comboBox17.Items.Insert(0, "60");
                comboBox17.SelectedIndex = 0;

                panel20.BackColor = Color.Transparent;
            }
            else if ((insulation == "RHW") || (insulation == "THW") || (insulation == "THWN")
                || (insulation == "USE") || (insulation == "ZW"))
            {
                insulindex = 2;
                comboBox17.Items.Clear();
                comboBox17.Items.Insert(0, "75");
                comboBox17.SelectedIndex = 0;

                panel20.BackColor = Color.Transparent;
            }
            else if ((insulation == "TBS") || (insulation == "SA") || (insulation == "SIS") || (insulation == "FEP")
                || (insulation == "FEPB") || (insulation == "MI") || (insulation == "RHH")
                || (insulation == "RHW-2") || (insulation == "THHN") || (insulation == "THW-2")
                || (insulation == "THWN-2") || (insulation == "USE-2") || (insulation == "XHH")
                || (insulation == "XHHW-2") || (insulation == "ZW-2"))
            {
                insulindex = 3;
                comboBox17.Items.Clear();
                comboBox17.Items.Insert(0, "90");
                comboBox17.SelectedIndex = 0;

                panel20.BackColor = Color.Transparent;
            }
            else if ((insulation == "XHHW") || (insulation == "THHW"))
            {
                insulindex = 4;
                comboBox17.SelectedIndex = -1;
                comboBox17.Items.Clear();
                comboBox17.Items.Insert(0, "75");
                comboBox17.Items.Insert(1, "90");
                panel20.BackColor = Color.Transparent;
            }
            else
            {
                insulindex = 0;
                comboBox17.SelectedIndex = -1;
                comboBox17.Items.Clear();

                textBox21.Text = "";
                panel20.BackColor = Color.Red;
            }

            if (comboBox17.Text != "")
            {
                initialTemp = double.Parse(comboBox17.Text);
            }
            else
            {
                initialTemp = 0;
            }

            if (comboBox16.SelectedIndex != -1)
            {
                panel32.BackColor = Color.Transparent;
            }
            else
            {
                panel32.BackColor = Color.Red;
            }

            if (comboBox17.SelectedIndex != -1)
            {
                panel33.BackColor = Color.Transparent;
            }
            else
            {
                panel33.BackColor = Color.Red;
            }

            DisableUndoReset();
            temp_fill();

            maxtemp_calc();

            Updatek1();
            Updatekt();

            SCLTEchanged();

            enable_vd_btn();
            enable_result_btn();
        }

        private void temp_fill()
        {
            if (insulindex == 1)
            {
                comboBox16.Items.Clear();
                comboBox16.Items.Insert(0, "25");
                comboBox16.Items.Insert(1, "30");
                comboBox16.Items.Insert(2, "35");
                comboBox16.Items.Insert(3, "40");
                comboBox16.Items.Insert(4, "45");
                comboBox16.Items.Insert(5, "50");
                comboBox16.Items.Insert(6, "55");
            }
            else if (insulindex == 2)
            {
                comboBox16.Items.Clear();
                comboBox16.Items.Insert(0, "25");
                comboBox16.Items.Insert(1, "30");
                comboBox16.Items.Insert(2, "35");
                comboBox16.Items.Insert(3, "40");
                comboBox16.Items.Insert(4, "45");
                comboBox16.Items.Insert(5, "50");
                comboBox16.Items.Insert(6, "55");
                comboBox16.Items.Insert(7, "60");
                comboBox16.Items.Insert(8, "70");
            }
            else if (insulindex == 3)
            {
                comboBox16.Items.Clear();
                comboBox16.Items.Insert(0, "25");
                comboBox16.Items.Insert(1, "30");
                comboBox16.Items.Insert(2, "35");
                comboBox16.Items.Insert(3, "40");
                comboBox16.Items.Insert(4, "45");
                comboBox16.Items.Insert(5, "50");
                comboBox16.Items.Insert(6, "55");
                comboBox16.Items.Insert(7, "60");
                comboBox16.Items.Insert(8, "70");
                comboBox16.Items.Insert(9, "80");
            }
            else
            {
                comboBox16.Items.Clear();
            }
        }

        private void maxtemp_calc()
        {
            if (insulindex == 1)
            {
                maxtemp = 55;
            }
            else if (insulindex == 2)
            {
                maxtemp = 70;
            }
            else if (insulindex == 3)
            {
                maxtemp = 80;
            }
            else
            {
                maxtemp = 0;
            }
        }


        private void ComboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            installation = comboBox9.Text;

            DisableUndoReset();
            correctionfactor_fill();
            Updatek1();
            Updatekt();

            enable_vd_btn();
            enable_result_btn();
        }

        private void correctionfactor_fill()
        {
            if (!radioButton7.Checked)
            {
                if (installation == "Raceways")
                {
                    comboBox18.SelectedIndex = -1;
                    comboBox19.SelectedIndex = -1;
                    comboBox20.SelectedIndex = -1;
                    index_groupedconductor = -1;
                    index_distanceaboveroof = -1;
                    index_cablecover = -1;
                    comboBox18.Enabled = true;
                    comboBox19.Enabled = true;
                    comboBox20.Enabled = false;
                    label93.Text = "Ambient Temperature\nCorrection Factor";
                    label94.Text = "No. of Grouped Conductor\nCorrection Factor";
                    label95.Text = "";
                    label93.Visible = true;
                    label94.Visible = true;
                    label95.Visible = false;
                    panel22.BackColor = Color.Transparent;
                }
                else if (installation == "Cable Tray / Ladder")
                {
                    comboBox18.SelectedIndex = -1;
                    comboBox19.SelectedIndex = -1;
                    comboBox20.SelectedIndex = -1;
                    index_groupedconductor = -1;
                    index_distanceaboveroof = -1;
                    index_cablecover = -1;
                    comboBox18.Enabled = true;
                    comboBox19.Enabled = false;
                    comboBox20.Enabled = true;
                    label93.Text = "Ambient Temperature\nCorrection Factor";
                    label94.Text = "No. of Grouped Conductor\nCorrection Factor";
                    label95.Text = "Cable Tray/Ladder Cover\nCorrection Factor";
                    label93.Visible = true;
                    label94.Visible = true;
                    label95.Visible = true;
                    panel22.BackColor = Color.Transparent;
                }
                else if (installation == "Earth (Direct Buried)")
                {
                    comboBox18.SelectedIndex = -1;
                    comboBox19.SelectedIndex = -1;
                    comboBox20.SelectedIndex = -1;
                    index_groupedconductor = -1;
                    index_distanceaboveroof = -1;
                    index_cablecover = -1;
                    comboBox18.Enabled = true;
                    comboBox19.Enabled = false;
                    comboBox20.Enabled = false;
                    label93.Text = "Ambient Temperature\nCorrection Factor";
                    label94.Text = "No. of Grouped Conductor\nCorrection Factor";
                    label95.Text = "";
                    label93.Visible = true;
                    label94.Visible = true;
                    label95.Visible = false;
                    panel22.BackColor = Color.Transparent;
                }
                else if (installation == "Free Air")
                {
                    comboBox18.SelectedIndex = -1;
                    comboBox19.SelectedIndex = -1;
                    comboBox20.SelectedIndex = -1;
                    index_groupedconductor = -1;
                    index_distanceaboveroof = -1;
                    index_cablecover = -1;
                    comboBox18.Enabled = true;
                    comboBox19.Enabled = false;
                    comboBox20.Enabled = false;
                    label93.Text = "Ambient Temperature\nCorrection Factor";
                    label94.Text = "No. of Grouped Conductor\nCorrection Factor";
                    label95.Text = "";
                    label93.Visible = true;
                    label94.Visible = true;
                    label95.Visible = false;
                    panel22.BackColor = Color.Transparent;
                }
                else
                {
                    comboBox18.SelectedIndex = -1;
                    comboBox19.SelectedIndex = -1;
                    comboBox20.SelectedIndex = -1;
                    index_groupedconductor = -1;
                    index_distanceaboveroof = -1;
                    index_cablecover = -1;
                    comboBox18.Enabled = false;
                    comboBox19.Enabled = false;
                    comboBox20.Enabled = false;
                    label93.Text = "";
                    label94.Text = "";
                    label95.Text = "";
                    label93.Visible = false;
                    label94.Visible = false;
                    label95.Visible = false;
                    panel22.BackColor = Color.Red;
                }
            }
            else if (radioButton7.Checked)
            {
                if (installation == "Raceways")
                {
                    comboBox18.SelectedIndex = -1;
                    comboBox19.SelectedIndex = -1;
                    comboBox20.SelectedIndex = -1;
                    index_groupedconductor = -1;
                    index_distanceaboveroof = -1;
                    index_cablecover = -1;
                    comboBox18.Enabled = true;
                    comboBox19.Enabled = true;
                    comboBox20.Enabled = false;

                    panel22.BackColor = Color.Transparent;
                }
                else if (installation == "Cable Tray / Ladder")
                {
                    comboBox18.SelectedIndex = -1;
                    comboBox19.SelectedIndex = -1;
                    comboBox20.SelectedIndex = -1;
                    index_groupedconductor = -1;
                    index_distanceaboveroof = -1;
                    index_cablecover = -1;
                    comboBox18.Enabled = true;
                    comboBox19.Enabled = false;
                    comboBox20.Enabled = true;

                    panel22.BackColor = Color.Transparent;
                }
                else if (installation == "Earth (Direct Buried)")
                {
                    comboBox18.SelectedIndex = -1;
                    comboBox19.SelectedIndex = -1;
                    comboBox20.SelectedIndex = -1;
                    index_groupedconductor = -1;
                    index_distanceaboveroof = -1;
                    index_cablecover = -1;
                    comboBox18.Enabled = true;
                    comboBox19.Enabled = false;
                    comboBox20.Enabled = false;

                    panel22.BackColor = Color.Transparent;
                }
                else if (installation == "Free Air")
                {
                    comboBox18.SelectedIndex = -1;
                    comboBox19.SelectedIndex = -1;
                    comboBox20.SelectedIndex = -1;
                    index_groupedconductor = -1;
                    index_distanceaboveroof = -1;
                    index_cablecover = -1;
                    comboBox18.Enabled = true;
                    comboBox19.Enabled = false;
                    comboBox20.Enabled = false;

                    panel22.BackColor = Color.Transparent;
                }
                else
                {
                    comboBox18.SelectedIndex = -1;
                    comboBox19.SelectedIndex = -1;
                    comboBox20.SelectedIndex = -1;
                    index_groupedconductor = -1;
                    index_distanceaboveroof = -1;
                    index_cablecover = -1;
                    comboBox18.Enabled = false;
                    comboBox19.Enabled = false;
                    comboBox20.Enabled = false;

                    panel22.BackColor = Color.Red;
                }
            }

            if (comboBox18.Enabled == true)
            {
                label89.Enabled = true;
                panel34.BackColor = Color.Red;
            }
            else
            {
                label89.Enabled = false;
                panel34.BackColor = Color.Transparent;
            }

            if (comboBox19.Enabled == true)
            {
                label90.Enabled = true;
                panel35.BackColor = Color.Red;
            }
            else
            {
                label90.Enabled = false;
                panel35.BackColor = Color.Transparent;
            }

            if (comboBox20.Enabled == true)
            {
                label91.Enabled = true;
                panel36.BackColor = Color.Red;
            }
            else
            {
                label91.Enabled = false;
                panel36.BackColor = Color.Transparent;
            }

        }

        private void calc_cmil()
        {
            if ((double.TryParse(wirearea_nec, out number)) && (wirearea_unit == "AWG"))
            {
                n_cmil = Convert.ToDouble(wirearea_nec);
                cmil = Math.Pow((5 * Math.Pow(92.00, ((36 - n_cmil) / 39))), 2);
            }
            else if ((double.TryParse(wirearea_nec, out number)) && (wirearea_unit == "kcmil"))
            {
                cmil = Convert.ToDouble(wirearea_nec) * 1000;
            }
            else
            {
                switch (wirearea_nec)
                {
                    case "1/0":
                        {
                            n_cmil = 0;
                            break;
                        }
                    case "2/0":
                        {
                            n_cmil = -1;
                            break;
                        }
                    case "3/0":
                        {
                            n_cmil = -2;
                            break;
                        }
                    case "4/0":
                        {
                            n_cmil = -3;
                            break;
                        }
                };
                cmil = Math.Pow((5 * Math.Pow(92.00, ((36 - n_cmil) / 39))), 2);
            }
        }

        private void calc_vd()
        {

            n = int.Parse(textBox12.Text);
            complete = false;

            if (initialTemp == 60)
            {
                insulindex = 1;
            }
            else if (initialTemp == 75)
            {
                insulindex = 2;
            }
            else if (initialTemp == 90)
            {
                insulindex = 3;
            }
            else
            {
                insulindex = 0;
            }

            while (!complete)
            {
                i = 0;
                if (radioButton4.Checked) //vendor data
                {
                    if (material == "Copper")
                    {
                        if (conduit == "PVC")
                        {
                            while ((!complete) && (i < 21))
                            {
                                wirearea_nec = data_wirearea[i];
                                wirearea_unit = data_wirearea_unit[i];
                                wirearea_metric = data_wirearea_metric[i];

                                calc_cmil();


                                if (phase == "DC")
                                {
                                    Rdc = data_electrical[i, 2];
                                    vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                }
                                else //AC
                                {
                                    Rac = data_electrical[i, 2] * (234.5 + initialTemp) / (234.5 + 75);
                                    X = data_electrical[i, 0];

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


                                if ((installation == "Raceways") || (installation == "Cable Tray / Ladder") || (installation == "Earth (Direct Buried)"))
                                {
                                    Irated = data_ampacity_racewaycableearth[i, (insulindex - 1)] * n;
                                }
                                else if (installation == "Free Air")
                                {
                                    Irated = data_ampacity_freeair[i, (insulindex - 1)] * n;
                                }

                                iderated = Irated * ktmain;

                                //cLTE = data_wirearea_metric[i] * data_wirearea_metric[i] * k * k;
                                cable_lte();

                                // Validasi
                                validasi_vd();

                                i++;
                            }
                        }
                        else if (conduit == "Aluminium")
                        {
                            while ((!complete) && (i < 21))
                            {
                                wirearea_nec = data_wirearea[i];
                                wirearea_unit = data_wirearea_unit[i];
                                wirearea_metric = data_wirearea_metric[i];

                                calc_cmil();

                                if (phase == "DC")
                                {
                                    Rdc = data_electrical[i, 3];
                                    vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                }
                                else //AC
                                {
                                    Rac = data_electrical[i, 3] * (234.5 + initialTemp) / (234.5 + 75);
                                    X = data_electrical[i, 0];

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


                                if ((installation == "Raceways") || (installation == "Cable Tray / Ladder") || (installation == "Earth (Direct Buried)"))
                                {
                                    Irated = data_ampacity_racewaycableearth[i, (insulindex - 1)] * n;
                                }
                                else if (installation == "Free Air")
                                {
                                    Irated = data_ampacity_freeair[i, (insulindex - 1)] * n;
                                }

                                iderated = Irated * ktmain;

                                //cLTE = data_wirearea_metric[i] * data_wirearea_metric[i] * k * k;
                                cable_lte();

                                // Validasi
                                validasi_vd();

                                i++;
                            }
                        }
                        else if (conduit == "Steel")
                        {
                            while ((!complete) && (i < 21))
                            {
                                wirearea_nec = data_wirearea[i];
                                wirearea_unit = data_wirearea_unit[i];
                                wirearea_metric = data_wirearea_metric[i];
                                calc_cmil();

                                if (phase == "DC")
                                {
                                    Rdc = data_electrical[i, 4];
                                    vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                }
                                else //AC
                                {
                                    Rac = data_electrical[i, 4] * (234.5 + initialTemp) / (234.5 + 75);
                                    X = data_electrical[i, 1];

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


                                if ((installation == "Raceways") || (installation == "Cable Tray / Ladder") || (installation == "Earth (Direct Buried)"))
                                {
                                    Irated = data_ampacity_racewaycableearth[i, (insulindex - 1)] * n;
                                }
                                else if (installation == "Free Air")
                                {
                                    Irated = data_ampacity_freeair[i, (insulindex - 1)] * n;
                                }

                                iderated = Irated * ktmain;

                                //cLTE = data_wirearea_metric[i] * data_wirearea_metric[i] * k * k;
                                cable_lte();

                                // Validasi
                                validasi_vd();

                                i++;
                            }
                        }
                    }
                    else if (material == "Aluminium")
                    {
                        if (insulindex == 1)
                        {
                            insulindex = 4;
                        }
                        else if (insulindex == 2)
                        {
                            insulindex = 5;
                        }
                        else if (insulindex == 3)
                        {
                            insulindex = 6;
                        }

                        if (conduit == "PVC")
                        {
                            while ((!complete) && (i < 21))
                            {
                                wirearea_nec = data_wirearea[i];
                                wirearea_unit = data_wirearea_unit[i];
                                wirearea_metric = data_wirearea_metric[i];
                                calc_cmil();

                                if (phase == "DC")
                                {
                                    Rdc = data_electrical[i, 5];
                                    vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                }
                                else //AC
                                {
                                    Rac = data_electrical[i, 5] * (228.1 + initialTemp) / (228.1 + 75);
                                    X = data_electrical[i, 0];

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


                                if ((installation == "Raceways") || (installation == "Cable Tray / Ladder") || (installation == "Earth (Direct Buried)"))
                                {
                                    Irated = data_ampacity_racewaycableearth[i, (insulindex - 1)] * n;
                                }
                                else if (installation == "Free Air")
                                {
                                    Irated = data_ampacity_freeair[i, (insulindex - 1)] * n;
                                }

                                iderated = Irated * ktmain;

                                //cLTE = data_wirearea_metric[i] * data_wirearea_metric[i] * k * k;
                                cable_lte();

                                // Validasi
                                validasi_vd();

                                i++;
                            }
                        }
                        else if (conduit == "Aluminium")
                        {
                            while ((!complete) && (i < 21))
                            {
                                wirearea_nec = data_wirearea[i];
                                wirearea_unit = data_wirearea_unit[i];
                                wirearea_metric = data_wirearea_metric[i];
                                calc_cmil();

                                if (phase == "DC")
                                {
                                    Rdc = data_electrical[i, 6];
                                    vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                }
                                else //AC
                                {
                                    Rac = data_electrical[i, 6] * (228.1 + initialTemp) / (228.1 + 75);
                                    X = data_electrical[i, 0];

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


                                if ((installation == "Raceways") || (installation == "Cable Tray / Ladder") || (installation == "Earth (Direct Buried)"))
                                {
                                    Irated = data_ampacity_racewaycableearth[i, (insulindex - 1)] * n;
                                }
                                else if (installation == "Free Air")
                                {
                                    Irated = data_ampacity_freeair[i, (insulindex - 1)] * n;
                                }

                                iderated = Irated * ktmain;

                                //cLTE = data_wirearea_metric[i] * data_wirearea_metric[i] * k * k;
                                cable_lte();

                                // Validasi
                                validasi_vd();

                                i++;
                            }
                        }
                        else if (conduit == "Steel")
                        {
                            while ((!complete) && (i < 21))
                            {
                                wirearea_nec = data_wirearea[i];
                                wirearea_unit = data_wirearea_unit[i];
                                wirearea_metric = data_wirearea_metric[i];
                                calc_cmil();

                                if (phase == "DC")
                                {
                                    Rdc = data_electrical[i, 7];
                                    vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                }
                                else //AC
                                {
                                    Rac = data_electrical[i, 7] * (228.1 + initialTemp) / (228.1 + 75);
                                    X = data_electrical[i, 1];

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


                                if ((installation == "Raceways") || (installation == "Cable Tray / Ladder") || (installation == "Earth (Direct Buried)"))
                                {
                                    Irated = data_ampacity_racewaycableearth[i, (insulindex - 1)] * n;
                                }
                                else if (installation == "Free Air")
                                {
                                    Irated = data_ampacity_freeair[i, (insulindex - 1)] * n;
                                }

                                iderated = Irated * ktmain;

                                //cLTE = data_wirearea_metric[i] * data_wirearea_metric[i] * k * k;
                                cable_lte();

                                // Validasi
                                validasi_vd();

                                i++;
                            }
                        }
                    }

                    if (!complete)
                    {
                        solvableOrNPlus_Vd();
                    }
                }
                else if (radioButton3.Checked) //manual data
                {
                    while ((!complete) && (i < cableCount))
                    {
                        wirearea_nec = inputCableData_nec[i, 0];
                        wirearea_unit = inputCableData_nec_unit[i];
                        wirearea_metric = inputCableData_nec_metric[i];
                        calc_cmil();

                        if (phase == "DC")
                        {
                            Rdc = Convert.ToDouble(inputCableData_nec[i, 2]);
                            Rac = 0;

                            vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                            lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                        }
                        else
                        {
                            Rac = Convert.ToDouble(inputCableData_nec[i, 2]);
                            Rdc = 0;
                            X = Convert.ToDouble(inputCableData_nec[i, 3]);

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

                        Irated = Convert.ToDouble(inputCableData_nec[i, 4]) * n;
                        diameter = Convert.ToDouble(inputCableData_nec[i, 5]);

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

                label85.Text = wirearea_unit;
            }

            if (inputValid)
            {
                textBox12.Text = n.ToString();
                textBox8.Text = vdrun.ToString("0.##");
                textBox29.Text = lmax.ToString("0.##");
                //textBox22.Text = cLTE.ToString("0.##");

                if (material == "Copper")
                {
                    materialname = "Cu";
                }
                else if (material == "Aluminium")
                {
                    materialname = "Al";
                }

                if ((loadtype == "Motor") && ConsiderVdStart)
                {
                    textBox10.Text = vdstart.ToString("0.##");
                }

                readtemp = "";

                readtemp += n.ToString() + "  ×  " + cores.ToString("0.##") + "/C  #  " + wirearea_nec +
                    " " + wirearea_unit + "    " + ratedvoltage + " / " + materialname + " / " + insulation;



                tbResult.Text = readtemp;

                save_vd_result();

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
                bLTE = 0;
                textBox20.Text = "";

                comboBox12.SelectedIndex = -1;
                comboBox10.SelectedIndex = -1;
                comboBox11.SelectedIndex = -1;
                comboBox10.Text = "";
                comboBox11.Text = "";
                comboBox5.Enabled = false;
                comboBox21.Enabled = false;
                textBox12.ReadOnly = true;

                button8.Enabled = true;
                panel5.Enabled = false;
                panel6.Enabled = false;

                label87.Text = "Since Vd run is lower than Vd run max,\ntherefore cable size of " + wirearea_nec + " " + wirearea_unit + "  is acceptable";
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

        public void calc_area()
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

                if (initialTemp == 60)
                {
                    insulindex = 1;
                }
                else if (initialTemp == 75)
                {
                    insulindex = 2;
                }
                else if (initialTemp == 90)
                {
                    insulindex = 3;
                }
                else
                {
                    insulindex = 0;
                }

                while (!complete)
                {
                    i = m;
                    if (radioButton4.Checked)
                    {
                        //yg baru
                        if (material == "Copper")
                        {
                            if (conduit == "PVC")
                            {
                                while ((!complete) && (i < 21))
                                {
                                    wirearea_nec = data_wirearea[i];
                                    wirearea_unit = data_wirearea_unit[i];
                                    wirearea_metric = data_wirearea_metric[i];
                                    calc_cmil();

                                    if (phase == "DC")
                                    {
                                        Rdc = data_electrical[i, 2];
                                        vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                    }
                                    else //AC
                                    {
                                        Rac = data_electrical[i, 2] * (234.5 + initialTemp) / (234.5 + 75);
                                        X = data_electrical[i, 0];

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


                                    if ((installation == "Raceways") || (installation == "Cable Tray / Ladder") || (installation == "Earth (Direct Buried)"))
                                    {
                                        Irated = data_ampacity_racewaycableearth[i, (insulindex - 1)] * n;
                                    }
                                    else if (installation == "Free Air")
                                    {
                                        Irated = data_ampacity_freeair[i, (insulindex - 1)] * n;
                                    }

                                    iderated = Irated * ktmain;

                                    //cLTE = data_wirearea_metric[i] * data_wirearea_metric[i] * k * k;
                                    cable_lte();

                                    // Validasi
                                    validasi();

                                    i++;
                                }
                            }
                            else if (conduit == "Aluminium")
                            {
                                while ((!complete) && (i < 21))
                                {
                                    wirearea_nec = data_wirearea[i];
                                    wirearea_unit = data_wirearea_unit[i];
                                    wirearea_metric = data_wirearea_metric[i];
                                    calc_cmil();

                                    if (phase == "DC")
                                    {
                                        Rdc = data_electrical[i, 3];
                                        vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                    }
                                    else //AC
                                    {
                                        Rac = data_electrical[i, 3] * (234.5 + initialTemp) / (234.5 + 75);
                                        X = data_electrical[i, 0];

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


                                    if ((installation == "Raceways") || (installation == "Cable Tray / Ladder") || (installation == "Earth (Direct Buried)"))
                                    {
                                        Irated = data_ampacity_racewaycableearth[i, (insulindex - 1)] * n;
                                    }
                                    else if (installation == "Free Air")
                                    {
                                        Irated = data_ampacity_freeair[i, (insulindex - 1)] * n;
                                    }

                                    iderated = Irated * ktmain;

                                    //cLTE = data_wirearea_metric[i] * data_wirearea_metric[i] * k * k;
                                    cable_lte();

                                    // Validasi
                                    validasi();

                                    i++;
                                }
                            }
                            else if (conduit == "Steel")
                            {
                                while ((!complete) && (i < 21))
                                {
                                    wirearea_nec = data_wirearea[i];
                                    wirearea_unit = data_wirearea_unit[i];
                                    wirearea_metric = data_wirearea_metric[i];
                                    calc_cmil();

                                    if (phase == "DC")
                                    {
                                        Rdc = data_electrical[i, 4];
                                        vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                    }
                                    else //AC
                                    {
                                        Rac = data_electrical[i, 4] * (234.5 + initialTemp) / (234.5 + 75);
                                        X = data_electrical[i, 1];

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


                                    if ((installation == "Raceways") || (installation == "Cable Tray / Ladder") || (installation == "Earth (Direct Buried)"))
                                    {
                                        Irated = data_ampacity_racewaycableearth[i, (insulindex - 1)] * n;
                                    }
                                    else if (installation == "Free Air")
                                    {
                                        Irated = data_ampacity_freeair[i, (insulindex - 1)] * n;
                                    }

                                    iderated = Irated * ktmain;

                                    //cLTE = data_wirearea_metric[i] * data_wirearea_metric[i] * k * k;
                                    cable_lte();

                                    // Validasi
                                    validasi();

                                    i++;
                                }
                            }
                        }
                        else if (material == "Aluminium")
                        {
                            if (insulindex == 1)
                            {
                                insulindex = 4;
                            }
                            else if (insulindex == 2)
                            {
                                insulindex = 5;
                            }
                            else if (insulindex == 3)
                            {
                                insulindex = 6;
                            }

                            if (conduit == "PVC")
                            {
                                while ((!complete) && (i < 21))
                                {
                                    wirearea_nec = data_wirearea[i];
                                    wirearea_unit = data_wirearea_unit[i];
                                    wirearea_metric = data_wirearea_metric[i];

                                    calc_cmil();

                                    if (phase == "DC")
                                    {
                                        Rdc = data_electrical[i, 5];
                                        vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                    }
                                    else //AC
                                    {
                                        Rac = data_electrical[i, 5] * (228.1 + initialTemp) / (228.1 + 75);
                                        X = data_electrical[i, 0];

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


                                    if ((installation == "Raceways") || (installation == "Cable Tray / Ladder") || (installation == "Earth (Direct Buried)"))
                                    {
                                        Irated = data_ampacity_racewaycableearth[i, (insulindex - 1)] * n;
                                    }
                                    else if (installation == "Free Air")
                                    {
                                        Irated = data_ampacity_freeair[i, (insulindex - 1)] * n;
                                    }

                                    iderated = Irated * ktmain;

                                    //cLTE = data_wirearea_metric[i] * data_wirearea_metric[i] * k * k;
                                    cable_lte();

                                    // Validasi
                                    validasi();

                                    i++;
                                }
                            }
                            else if (conduit == "Aluminium")
                            {
                                while ((!complete) && (i < 21))
                                {
                                    wirearea_nec = data_wirearea[i];
                                    wirearea_unit = data_wirearea_unit[i];
                                    wirearea_metric = data_wirearea_metric[i];
                                    calc_cmil();

                                    if (phase == "DC")
                                    {
                                        Rdc = data_electrical[i, 6];
                                        vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                    }
                                    else //AC
                                    {
                                        Rac = data_electrical[i, 6] * (228.1 + initialTemp) / (228.1 + 75);
                                        X = data_electrical[i, 0];

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


                                    if ((installation == "Raceways") || (installation == "Cable Tray / Ladder") || (installation == "Earth (Direct Buried)"))
                                    {
                                        Irated = data_ampacity_racewaycableearth[i, (insulindex - 1)] * n;
                                    }
                                    else if (installation == "Free Air")
                                    {
                                        Irated = data_ampacity_freeair[i, (insulindex - 1)] * n;
                                    }

                                    iderated = Irated * ktmain;

                                    //cLTE = data_wirearea_metric[i] * data_wirearea_metric[i] * k * k;
                                    cable_lte();

                                    // Validasi
                                    validasi();

                                    i++;
                                }
                            }
                            else if (conduit == "Steel")
                            {
                                while ((!complete) && (i < 21))
                                {
                                    wirearea_nec = data_wirearea[i];
                                    wirearea_unit = data_wirearea_unit[i];
                                    wirearea_metric = data_wirearea_metric[i];

                                    calc_cmil();

                                    if (phase == "DC")
                                    {
                                        Rdc = data_electrical[i, 7];
                                        vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                    }
                                    else //AC
                                    {
                                        Rac = data_electrical[i, 7] * (228.1 + initialTemp) / (228.1 + 75);
                                        X = data_electrical[i, 1];

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


                                    if ((installation == "Raceways") || (installation == "Cable Tray / Ladder") || (installation == "Earth (Direct Buried)"))
                                    {
                                        Irated = data_ampacity_racewaycableearth[i, (insulindex - 1)] * n;
                                    }
                                    else if (installation == "Free Air")
                                    {
                                        Irated = data_ampacity_freeair[i, (insulindex - 1)] * n;
                                    }

                                    iderated = Irated * ktmain;

                                    //cLTE = data_wirearea_metric[i] * data_wirearea_metric[i] * k * k;
                                    cable_lte();

                                    // Validasi
                                    validasi();

                                    i++;
                                }
                            }
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
                            wirearea_nec = inputCableData_nec[i, 0];
                            wirearea_unit = inputCableData_nec_unit[i];
                            wirearea_metric = inputCableData_nec_metric[i];

                            if (phase == "DC")
                            {
                                Rdc = Convert.ToDouble(inputCableData_nec[i, 2]);
                                Rac = 0;

                                vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                            }
                            else
                            {
                                Rac = Convert.ToDouble(inputCableData_nec[i, 2]);
                                Rdc = 0;
                                X = Convert.ToDouble(inputCableData_nec[i, 3]);

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

                            Irated = Convert.ToDouble(inputCableData_nec[i, 4]) * n;
                            diameter = Convert.ToDouble(inputCableData_nec[i, 5]);

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

                    label85.Text = wirearea_unit;

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
                    else if (material == "Aluminium")
                    {
                        materialname = "Al";
                    }

                    if ((loadtype == "Motor") && ConsiderVdStart)
                    {
                        textBox10.Text = vdstart.ToString("0.##");
                    }

                    readtemp = "";

                    readtemp += n.ToString() + "  ×  " + cores.ToString("0.##") + "/C  #  " + wirearea_nec +
                    " " + wirearea_unit + "    " + ratedvoltage + " / " + materialname + " / " + insulation;



                    tbResult.Text = readtemp;
                    cable_lte();
                    save_result();
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
                            " therefore cable size of" + wirearea_nec + " " + wirearea_unit + " is acceptable";
                    }
                    else if (radioButton2.Checked)
                    {
                        label88.Text = "Since the minimum cable size due to S.C. is lower than the \nselected cable size," +
                            " therefore cable size of " + wirearea_nec + " " + wirearea_unit + " is acceptable";
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

        // vd calculation if custom NEC database is selected
        private void calc_vd_custom()
        {

            n = int.Parse(textBox12.Text);
            complete = false;

            if (initialTemp == 60)
            {
                insulindex = 1;
            }
            else if (initialTemp == 75)
            {
                insulindex = 2;
            }
            else if (initialTemp == 90)
            {
                insulindex = 3;
            }
            else
            {
                insulindex = 0;
            }

            //get the database length
            SelectedDataLength();
            if (currentDataLength > 0)
            {
                //get selected database base on its insulation and material type
                SelectedDatabaseType();

                while (!complete)
                {
                    i = 0;
                    while ((!complete) && (i < currentDataLength))
                    {
                        wirearea_nec = nec_selected_wirearea[i];
                        wirearea_unit = nec_selected_wirearea_unit[i];
                        wirearea_metric = nec_selected_wirearea_metric[i];

                        calc_cmil();

                        if (phase == "DC")
                        {
                            Rdc = nec_selected_data_electrical[i, 0];
                            vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                            lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                        }
                        else //AC
                        {
                            Rac = nec_selected_data_electrical[i, 0];
                            X = nec_selected_data_electrical[i, 1];

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

                        //get irated value
                        Irated = nec_selected_data_electrical[i, 2] * n;


                        iderated = Irated * ktmain;

                        //cLTE = data_wirearea_metric[i] * data_wirearea_metric[i] * k * k;
                        cable_lte();

                        // Validasi
                        validasi_vd();

                        i++;
                    }

                    if (!complete)
                    {
                        solvableOrNPlus_Vd();
                    }
                    label85.Text = wirearea_unit;
                }
            }
            else
            {
                MessageBox.Show("Selected cable data specification for cable with " + material + " cable material and " + insulation + " insulation doesn't exist!",
                 "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                complete = true;
                inputValid = false;
            }


            if (inputValid)
            {
                textBox12.Text = n.ToString();
                textBox8.Text = vdrun.ToString("0.##");
                textBox29.Text = lmax.ToString("0.##");
                //textBox22.Text = cLTE.ToString("0.##");

                if (material == "Copper")
                {
                    materialname = "Cu";
                }
                else if (material == "Aluminium")
                {
                    materialname = "Al";
                }

                if ((loadtype == "Motor") && ConsiderVdStart)
                {
                    textBox10.Text = vdstart.ToString("0.##");
                }

                readtemp = "";

                readtemp += n.ToString() + "  ×  " + cores.ToString("0.##") + "/C  #  " + wirearea_nec +
                    " " + wirearea_unit + "    " + ratedvoltage + " / " + materialname + " / " + insulation;



                tbResult.Text = readtemp;

                save_vd_result();

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
                bLTE = 0;
                textBox20.Text = "";

                comboBox12.SelectedIndex = -1;
                comboBox10.SelectedIndex = -1;
                comboBox11.SelectedIndex = -1;
                comboBox10.Text = "";
                comboBox11.Text = "";
                comboBox5.Enabled = false;
                comboBox21.Enabled = false;
                textBox12.ReadOnly = true;

                button8.Enabled = true;
                panel5.Enabled = false;
                panel6.Enabled = false;

                label87.Text = "Since Vd run is lower than Vd run max,\ntherefore cable size of " + wirearea_nec + " " + wirearea_unit + "  is acceptable";
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

        public void calc_area_custom()
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

                if (initialTemp == 60)
                {
                    insulindex = 1;
                }
                else if (initialTemp == 75)
                {
                    insulindex = 2;
                }
                else if (initialTemp == 90)
                {
                    insulindex = 3;
                }
                else
                {
                    insulindex = 0;
                }

                //get the database length
                SelectedDataLength();

                if (currentDataLength > 0)
                {
                    //get selected database base on its insulation and material type
                    SelectedDatabaseType();
                    while (!complete)
                    {
                        i = m;
                        while ((!complete) && (i < currentDataLength))
                        {
                            wirearea_nec = nec_selected_wirearea[i];
                            wirearea_unit = nec_selected_wirearea_unit[i];
                            wirearea_metric = nec_selected_wirearea_metric[i];

                            calc_cmil();

                            if (phase == "DC")
                            {
                                Rdc = nec_selected_data_electrical[i, 0];
                                vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                            }
                            else //AC
                            {
                                Rac = nec_selected_data_electrical[i, 0];
                                X = nec_selected_data_electrical[i, 1];

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

                            //get irated
                            Irated = nec_selected_data_electrical[i, 2] * n;

                            iderated = Irated * ktmain;

                            //cLTE = data_wirearea_metric[i] * data_wirearea_metric[i] * k * k;
                            cable_lte();

                            // Validasi
                            validasi();

                            i++;
                        }


                        if (!complete)
                        {
                            solvableOrNPlus();
                        }

                        label85.Text = wirearea_unit;

                    }
                }
                else
                {
                    MessageBox.Show("Selected cable data specification for cable with " + material + " cable material and " + insulation + " insulation doesn't exist!",
                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    complete = true;
                    inputValid = false;
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
                    else if (material == "Aluminium")
                    {
                        materialname = "Al";
                    }

                    if ((loadtype == "Motor") && ConsiderVdStart)
                    {
                        textBox10.Text = vdstart.ToString("0.##");
                    }

                    readtemp = "";

                    readtemp += n.ToString() + "  ×  " + cores.ToString("0.##") + "/C  #  " + wirearea_nec +
                    " " + wirearea_unit + "    " + ratedvoltage + " / " + materialname + " / " + insulation;



                    tbResult.Text = readtemp;
                    cable_lte();
                    save_result();
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
                            " therefore cable size of" + wirearea_nec + " " + wirearea_unit + " is acceptable";
                    }
                    else if (radioButton2.Checked)
                    {
                        label88.Text = "Since the minimum cable size due to S.C. is lower than the \nselected cable size," +
                            " therefore cable size of " + wirearea_nec + " " + wirearea_unit + " is acceptable";
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

        private void SelectedDatabaseType()
        {
            if (material == "Copper")
            {
                switch (insulindex)
                {
                    case 1:
                        if (nec00Length > 0)
                        {
                            nec_selected_data_electrical = new double[nec00Length, 3];
                            nec_selected_wirearea = new string[nec00Length];
                            nec_selected_wirearea_unit = new string[nec00Length];
                            nec_selected_wirearea_metric = new double[nec00Length];

                            Array.Copy(nec00DB_data_electrical, nec_selected_data_electrical, nec00DB_data_electrical.Length);
                            Array.Copy(nec00DB_wirearea, nec_selected_wirearea, nec00DB_wirearea.Length);
                            Array.Copy(nec00DB_wirearea_unit, nec_selected_wirearea_unit, nec00DB_wirearea_unit.Length);
                            Array.Copy(nec00DB_wirearea_metric, nec_selected_wirearea_metric, nec00DB_wirearea_metric.Length);
                        }
                        break;
                    case 2:
                        if (nec10Length > 0)
                        {
                            nec_selected_data_electrical = new double[nec10Length, 3];
                            nec_selected_wirearea = new string[nec10Length];
                            nec_selected_wirearea_unit = new string[nec10Length];
                            nec_selected_wirearea_metric = new double[nec10Length];

                            Array.Copy(nec10DB_data_electrical, nec_selected_data_electrical, nec10DB_data_electrical.Length);
                            Array.Copy(nec10DB_wirearea, nec_selected_wirearea, nec10DB_wirearea.Length);
                            Array.Copy(nec10DB_wirearea_unit, nec_selected_wirearea_unit, nec10DB_wirearea_unit.Length);
                            Array.Copy(nec10DB_wirearea_metric, nec_selected_wirearea_metric, nec10DB_wirearea_metric.Length);
                        }
                        break;
                    case 3:
                        if (nec20Length > 0)
                        {
                            nec_selected_data_electrical = new double[nec20Length, 3];
                            nec_selected_wirearea = new string[nec20Length];
                            nec_selected_wirearea_unit = new string[nec20Length];
                            nec_selected_wirearea_metric = new double[nec20Length];

                            Array.Copy(nec20DB_data_electrical, nec_selected_data_electrical, nec20DB_data_electrical.Length);
                            Array.Copy(nec20DB_wirearea, nec_selected_wirearea, nec20DB_wirearea.Length);
                            Array.Copy(nec20DB_wirearea_unit, nec_selected_wirearea_unit, nec20DB_wirearea_unit.Length);
                            Array.Copy(nec20DB_wirearea_metric, nec_selected_wirearea_metric, nec20DB_wirearea_metric.Length);
                        }
                        break;
                }
            }
            else if (material == "Aluminium")
            {

                switch (insulindex)
                {
                    case 1:
                        if (nec01Length > 0)
                        {
                            nec_selected_data_electrical = new double[nec01Length, 3];
                            nec_selected_wirearea = new string[nec01Length];
                            nec_selected_wirearea_unit = new string[nec01Length];
                            nec_selected_wirearea_metric = new double[nec01Length];

                            Array.Copy(nec01DB_data_electrical, nec_selected_data_electrical, nec01DB_data_electrical.Length);
                            Array.Copy(nec01DB_wirearea, nec_selected_wirearea, nec01DB_wirearea.Length);
                            Array.Copy(nec01DB_wirearea_unit, nec_selected_wirearea_unit, nec01DB_wirearea_unit.Length);
                            Array.Copy(nec01DB_wirearea_metric, nec_selected_wirearea_metric, nec01DB_wirearea_metric.Length);
                        }
                        break;
                    case 2:
                        if (nec11Length > 0)
                        {
                            nec_selected_data_electrical = new double[nec11Length, 3];
                            nec_selected_wirearea = new string[nec11Length];
                            nec_selected_wirearea_unit = new string[nec11Length];
                            nec_selected_wirearea_metric = new double[nec11Length];

                            Array.Copy(nec11DB_data_electrical, nec_selected_data_electrical, nec11DB_data_electrical.Length);
                            Array.Copy(nec11DB_wirearea, nec_selected_wirearea, nec11DB_wirearea.Length);
                            Array.Copy(nec11DB_wirearea_unit, nec_selected_wirearea_unit, nec11DB_wirearea_unit.Length);
                            Array.Copy(nec11DB_wirearea_metric, nec_selected_wirearea_metric, nec11DB_wirearea_metric.Length);
                        }
                        break;
                    case 3:
                        if (nec21Length > 0)
                        {
                            nec_selected_data_electrical = new double[nec21Length, 3];
                            nec_selected_wirearea = new string[nec21Length];
                            nec_selected_wirearea_unit = new string[nec21Length];
                            nec_selected_wirearea_metric = new double[nec21Length];

                            Array.Copy(nec21DB_data_electrical, nec_selected_data_electrical, nec21DB_data_electrical.Length);
                            Array.Copy(nec21DB_wirearea, nec_selected_wirearea, nec21DB_wirearea.Length);
                            Array.Copy(nec21DB_wirearea_unit, nec_selected_wirearea_unit, nec21DB_wirearea_unit.Length);
                            Array.Copy(nec21DB_wirearea_metric, nec_selected_wirearea_metric, nec21DB_wirearea_metric.Length);
                        }
                        break;
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
        private void Update_size() //update buat custom database
        {
            string size_temp_update = "";

            textBox37.Text = wirearea_nec;
            textBox15.Text = cmil.ToString("0.##");

            int c = i - 1;
            comboBox15.Items.Clear();
            comboBox15.Items.Insert(0, "Update Size");
            if (radioButton4.Checked)
            {
                size_temp_update = data_wirearea[c] + " " + data_wirearea_unit[c];
                comboBox15.Items.Insert(1, size_temp_update);
                if (c < 20)
                {
                    size_temp_update = data_wirearea[c + 1] + " " + data_wirearea_unit[c + 1];
                    comboBox15.Items.Insert(2, size_temp_update);
                }
                if (c < 19)
                {
                    size_temp_update = data_wirearea[c + 2] + " " + data_wirearea_unit[c + 2];
                    comboBox15.Items.Insert(3, size_temp_update);
                }
            }
            else if (radioButton3.Checked)
            {
                comboBox15.Items.Insert(1, inputCableData_nec[c, 0]);
                if (c + 1 < cableCount)
                {
                    comboBox15.Items.Insert(2, inputCableData_nec[c + 1, 0]);

                }
                if (c + 2 < cableCount)
                {
                    comboBox15.Items.Insert(3, inputCableData_nec[c + 2, 0]);
                }
            }
            else if (radioButtonVendor.Checked)
            {

                comboBox15.Items.Insert(1, nec_selected_wirearea[c]);
                if (c + 1 < currentDataLength)
                {
                    comboBox15.Items.Insert(2, nec_selected_wirearea[c + 1]);

                }
                if (c + 2 < currentDataLength)
                {
                    comboBox15.Items.Insert(3, nec_selected_wirearea[c + 2]);
                }
            }
            comboBox15.SelectedIndex = 0;
            m = c;
        }

        private void solvableOrNPlus()
        {
            if (breakcurrent < current)
            {
                MessageBox.Show("Failed to get a suitable cable size: Breaker current value must be greater than the full load current value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                complete = true;
                inputValid = false;
            }
            else if (cablesizemin > cmil)
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
                (((radioButton1.Checked) && (cLTE > bLTE)) ||
                ((radioButton2.Checked) &&
                (((radioButton4.Checked) && /*(data_wirearea_metric[i] > cablesizemin))*/ (cmil > smin)) || ((radioButton3.Checked) && /*(inputCableData_nec_metric[i] > cablesizemin)*/ (cmil > smin)) ||
                ((radioButtonVendor.Checked) && /*(nec_selected_wirearea_metric[i] > cablesizemin)*/(cmil > smin))))))
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
            if (radioButtonVendor.Checked)
            {
                calc_area_custom();
            }
            else
            {
                calc_area();
            }
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
            DisableUndoReset();
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
            DisableUndoReset();
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
            DisableUndoReset();
            enable_vd_btn();
            enable_result_btn();
        }

        private void TextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Form1.decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox4.Text == "") && (e.KeyChar == Form1.decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == Form1.decimalseparator) && ((sender as TextBox).Text.IndexOf(Form1.decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox14_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Form1.decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox14.Text == "") && (e.KeyChar == Form1.decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == Form1.decimalseparator) && ((sender as TextBox).Text.IndexOf(Form1.decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Form1.decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox5.Text == "") && (e.KeyChar == Form1.decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == Form1.decimalseparator) && ((sender as TextBox).Text.IndexOf(Form1.decimalseparator) > -1))
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
            DisableUndoReset();
            enable_vd_btn();
            enable_result_btn();

        }

        private void TextBox14_Leave_1(object sender, EventArgs e)
        {
            pfstart = double.Parse(textBox14.Text);
        }

        private void TextBox23_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Form1.decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox23.Text == "") && (e.KeyChar == Form1.decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == Form1.decimalseparator) && ((sender as TextBox).Text.IndexOf(Form1.decimalseparator) > -1))
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
                comboBox11.DropDownStyle = ComboBoxStyle.DropDown;
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
            enable_result_btn();
        }

        private void ComboBox11_TextChanged(object sender, EventArgs e)
        {
            if (comboBox11.Text != "")
            {
                breakcurrent = double.Parse(comboBox11.Text);
            }
            SCLTEchanged();
            break_lte();
            enable_result_btn();
        }

        private void ComboBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Form1.decimalseparator))
            {
                e.Handled = true;
            }

            if ((comboBox11.Text == "") && (e.KeyChar == Form1.decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == Form1.decimalseparator) && ((sender as ComboBox).Text.IndexOf(Form1.decimalseparator) > -1))
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
            DisableUndoReset();
            enable_vd_btn();
            enable_result_btn();
        }


        private void ResetData()
        {
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
            //textBox15.Text = "";
            textBox24.Text = "";
            textBox17.Text = "";
            textBox36.Text = "";
            textBox18.Text = "";
            textBox19.Text = "";
            textBox23.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            bLTE = 0;
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
            textBox15.Text = "";


            cbPower.Text = "kW";
            comboBox1.SelectedIndex = 1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            comboBox5.Enabled = true;
            comboBox6.SelectedIndex = -1;
            comboBox9.SelectedIndex = -1;
            comboBox10.SelectedIndex = -1;
            comboBox11.SelectedIndex = -1;
            comboBox12.SelectedIndex = -1;
            comboBox11.Text = "";
            comboBox14.SelectedIndex = -1;
            comboBox13.SelectedIndex = -1;
            comboBox15.SelectedIndex = -1;
            comboBox15.Items.Clear();
            comboBox16.SelectedIndex = -1;
            comboBox17.SelectedIndex = -1;
            comboBox18.SelectedIndex = -1;
            comboBox19.SelectedIndex = -1;
            comboBox20.SelectedIndex = -1;
            comboBox21.SelectedIndex = -1;
            comboBox21.Enabled = true;

            panel4.Enabled = false;
            panel5.Enabled = true;
            panel6.Enabled = true;
            panel30.BackColor = Color.Red;

            label93.Text = "";
            label94.Text = "";
            label95.Text = "";

            label93.Visible = false;
            label94.Visible = false;
            label95.Visible = false;

            radioButton7.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = true;

            insulindex = 0;
            button8.Enabled = false;
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

            DisableUndoReset();
            enable_vd_btn();
            enable_result_btn();
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

        private void TextBox25_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Form1.decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox25.Text == "") && (e.KeyChar == Form1.decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == Form1.decimalseparator) && ((sender as TextBox).Text.IndexOf(Form1.decimalseparator) > -1))
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
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Form1.decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox28.Text == "") && (e.KeyChar == Form1.decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == Form1.decimalseparator) && ((sender as TextBox).Text.IndexOf(Form1.decimalseparator) > -1))
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

            /*
            if (radioButton7.Checked)
            {
                if ((textBox17.Text != "") && (textBox18.Text != ""))
                {
                    textBox19.Location = new Point(kttextboxX, kttextboxY);
                    label43.Location = new Point(ktlabelX, ktlabelY);
                    panel24.Location = new Point(textBox19.Location.X - 4, textBox19.Location.Y);
                }
                else if ((textBox17.Text != "") && (k1main != 0))
                {
                    label43.Location = label80.Location;
                    textBox19.Location = textBox36.Location;

                    panel24.Location = new Point(textBox36.Location.X - 4, textBox36.Location.Y);

                    textBox18.Visible = true;
                    label42.Visible = true;
                }
                else if (k1main == 0)
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
            }
            */

            DisableUndoReset();
            UpdatePosition();

            Updatekt();
            enable_result_btn();
        }

        private void UpdatePosition()
        {
            if (radioButton7.Checked)
            {
                if ((textBox17.Text != "") && (textBox18.Text != "") && (textBox36.Text != ""))
                {
                    textBox18.Visible = true;
                    label42.Visible = true;

                    textBox36.Visible = true;
                    label80.Visible = true;

                    textBox19.Location = new Point(kttextboxX, kttextboxY);
                    label43.Location = new Point(ktlabelX, ktlabelY);
                    panel24.Location = new Point(textBox19.Location.X - 4, textBox19.Location.Y);
                }
                else if ((textBox17.Text != "") && (textBox18.Text != "") && (textBox36.Text == ""))
                {
                    k3main = 0;

                    textBox18.Visible = true;
                    label42.Visible = true;

                    textBox36.Visible = true;
                    label80.Visible = true;

                    textBox19.Location = new Point(kttextboxX, kttextboxY);
                    label43.Location = new Point(ktlabelX, ktlabelY);
                    panel24.Location = new Point(textBox19.Location.X - 4, textBox19.Location.Y);
                }
                else if ((textBox17.Text != "") && (textBox18.Text == "") && (textBox36.Text != ""))
                {
                    k2main = 0;

                    textBox18.Visible = true;
                    label42.Visible = true;

                    textBox36.Visible = true;
                    label80.Visible = true;

                    textBox19.Location = new Point(kttextboxX, kttextboxY);
                    label43.Location = new Point(ktlabelX, ktlabelY);
                    panel24.Location = new Point(textBox19.Location.X - 4, textBox19.Location.Y);
                }
                else if ((textBox17.Text != "") && (textBox18.Text == "") && (textBox36.Text == ""))
                {
                    k2main = 0;
                    k3main = 0;

                    textBox18.Visible = true;
                    label42.Visible = true;

                    textBox36.Visible = false;
                    label80.Visible = false;

                    textBox19.Location = textBox36.Location;
                    label43.Location = label80.Location;
                    panel24.Location = new Point(textBox19.Location.X - 4, textBox19.Location.Y);
                }
                else if ((textBox17.Text == "") && (textBox18.Text != "") && (textBox36.Text != ""))
                {
                    k1main = 0;

                    textBox18.Visible = true;
                    label42.Visible = true;

                    textBox36.Visible = true;
                    label80.Visible = true;

                    textBox19.Location = new Point(kttextboxX, kttextboxY);
                    label43.Location = new Point(ktlabelX, ktlabelY);
                    panel24.Location = new Point(textBox19.Location.X - 4, textBox19.Location.Y);
                }
                else if ((textBox17.Text == "") && (textBox18.Text != "") && (textBox36.Text == ""))
                {
                    k1main = 0;
                    k3main = 0;

                    textBox18.Visible = true;
                    label42.Visible = true;

                    textBox36.Visible = true;
                    label80.Visible = true;

                    textBox19.Location = new Point(kttextboxX, kttextboxY);
                    label43.Location = new Point(ktlabelX, ktlabelY);
                    panel24.Location = new Point(textBox19.Location.X - 4, textBox19.Location.Y);
                }
                else if ((textBox17.Text == "") && (textBox18.Text == "") && (textBox36.Text != ""))
                {
                    k1main = 0;
                    k2main = 0;

                    textBox18.Visible = true;
                    label42.Visible = true;

                    textBox36.Visible = true;
                    label80.Visible = true;

                    textBox19.Location = new Point(kttextboxX, kttextboxY);
                    label43.Location = new Point(ktlabelX, ktlabelY);
                    panel24.Location = new Point(textBox19.Location.X - 4, textBox19.Location.Y);
                }
                else if ((textBox17.Text == "") && (textBox18.Text == "") && (textBox36.Text == ""))
                {
                    k1main = 0;
                    k2main = 0;
                    k3main = 0;

                    textBox18.Visible = false;
                    label42.Visible = false;

                    textBox36.Visible = false;
                    label80.Visible = false;

                    textBox19.Location = textBox18.Location;
                    label43.Location = label42.Location;
                    panel24.Location = new Point(textBox19.Location.X - 4, textBox19.Location.Y);
                }
            }
        }

        private void TextBox18_TextChanged(object sender, EventArgs e)
        {
            if (textBox18.Text != "")
            {
                k2main = double.Parse(textBox18.Text);
            }
            else
            {
                k2main = 0;
            }
            DisableUndoReset();

            UpdatePosition();

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

            DisableUndoReset();
            enable_vd_btn();
            enable_result_btn();
        }

        private void Updatekt()
        {

            if (radioButton7.Checked)
            {
                label93.Text = "";
                label94.Text = "";
                label95.Text = "";

                label93.Visible = false;
                label94.Visible = false;
                label95.Visible = false;

                if ((textBox17.Text != "") && (textBox18.Text != "") && (textBox36.Text != ""))
                {
                    ktmain = k1main * k2main * k3main;
                }
                else if ((textBox17.Text != "") && (textBox18.Text != "") && (textBox36.Text == ""))
                {
                    ktmain = k1main * k2main;
                }
                else if ((textBox17.Text != "") && (textBox18.Text == "") && (textBox36.Text != ""))
                {
                    ktmain = k1main * k3main;
                }
                else if ((textBox17.Text != "") && (textBox18.Text == "") && (textBox36.Text == ""))
                {
                    ktmain = k1main;
                }
                else if ((textBox17.Text == "") && (textBox18.Text != "") && (textBox36.Text != ""))
                {
                    ktmain = k2main * k3main;
                }
                else if ((textBox17.Text == "") && (textBox18.Text != "") && (textBox36.Text == ""))
                {
                    ktmain = k2main;
                }
                else if ((textBox17.Text == "") && (textBox18.Text == "") && (textBox36.Text != ""))
                {
                    ktmain = k3main;
                }
                else if ((textBox17.Text == "") && (textBox18.Text == "") && (textBox36.Text == ""))
                {
                    ktmain = 0;
                }


                if (ktmain != 0)
                {
                    textBox19.Text = ktmain.ToString("0.##");
                }
                else
                {
                    textBox19.Text = "";
                }

            }
            else if (!radioButton7.Checked)
            {
                if (installation == "Raceways")
                {
                    ktmain = k1main * k2main;
                }
                else if (installation == "Cable Tray / Ladder")
                {
                    ktmain = k1main * k2main * k3main;
                }
                else if (installation == "Earth (Direct Buried)")
                {
                    ktmain = k1main * k2main;
                }
                else if (installation == "Free Air")
                {
                    ktmain = k1main * k2main;
                }
                else
                {
                    ktmain = 0;
                }

                if (k1main != 0)
                {
                    textBox17.Text = k1main.ToString("0.##");
                }
                else
                {
                    textBox17.Text = "";
                }

                if (k2main != 0)
                {
                    textBox18.Text = k2main.ToString("0.##");
                }
                else
                {
                    textBox18.Text = "";
                }

                if (k3main != 0)
                {
                    textBox36.Text = k3main.ToString("0.##");
                }
                else
                {
                    textBox36.Text = "";
                }

                if (ktmain != 0)
                {
                    textBox19.Text = ktmain.ToString("0.##");
                }
                else
                {
                    textBox19.Text = "";
                }
            }
        }

        private void TextBox26_TextChanged(object sender, EventArgs e)
        {
            from = textBox26.Text;
            DisableUndoReset();
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
                Form1.j++;
                f5.dataGridView1.Rows[Form1.j].Cells[0].Value = Form1.j + 1;

                for (int k = 0; k < 39; k++)
                {
                    f5.dataGridView1.Rows[Form1.j].Cells[k + 1].Value = results[k];
                }

                DataRow dtrTemp = Form1.dtdiameter.NewRow();
                dtrTemp.ItemArray = dtr.ItemArray.Clone() as object[];
                Form1.dtdiameter.Rows.Add(dtrTemp);

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
                Form1.dtdiameter.Rows.RemoveAt(tempCurrentRow);
                Form1.dtdiameter.Rows.InsertAt(dtr, tempCurrentRow);
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
            DisableUndoReset();
        }

        private void Form9_FormClosing(object sender, FormClosingEventArgs e)
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
            DisableUndoReset();
        }

        private void TextBox30_TextChanged(object sender, EventArgs e)
        {
            if (textBox30.Text != "")
            {
                cablesizemin = double.Parse(textBox30.Text);
            }
            else
            {
                cablesizemin = 0;
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
            Form5.Standard = 2;
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

            DataRow dtx = Form1.dtdiameter.Rows[Form5.currentrow];
            bool noDatabase = false;
            if (Convert.ToString(dtx[32]) == "custom")
            {
                radioButtonVendor.Checked = true;
                if (comboBoxVendor.Items.Contains(Convert.ToString(dtx[65])))
                {
                    comboBoxVendor.SelectedItem = Convert.ToString(dtx[65]);
                }
                else
                {
                    MessageBox.Show("Error: Cable Database \"" + Convert.ToString(dtx[65]) + "\" not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            else if (Convert.ToString(dtx[32]) == "vendor")
            {
                radioButton4.Checked = true;
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
                                                            //comboBox7.Text = Convert.ToString(dtx[21]); //armour
                                                            //comboBox8.Text = Convert.ToString(dtx[22]); //outer Sheath
                if (radioButtonVendor.Checked)
                {
                    SelectedDatabaseType();
                    SelectedDataLength();
                }
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
                if (DtrToDoubleText(dtx, 34) != "")
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
                textBox37.Text = Convert.ToString(dtx[38]); //wirearea
                wirearea_nec = Convert.ToString(dtx[38]);
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
                comboBox17.Text = DtrToDoubleText(dtx, 46); //initialtemp
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

                temperature = Convert.ToDouble(dtx[55]);
                comboBox16.Text = temperature.ToString();
                comboBox16.SelectedIndex = Convert.ToInt32(dtx[56]);
                comboBox18.SelectedIndex = Convert.ToInt32(dtx[57]);
                comboBox19.SelectedIndex = Convert.ToInt32(dtx[58]);
                comboBox20.SelectedIndex = Convert.ToInt32(dtx[59]);
                conduit = Convert.ToString(dtx[60]);
                comboBox21.SelectedIndex = Convert.ToInt32(dtx[61]);
                comboBox1.SelectedIndex = Convert.ToInt32(dtx[62]);
                textBox35.Text = Convert.ToString(dtx[63]);
                tbResult.Text = Convert.ToString(dtx[64]);

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
                button3.Text = "Confirm Edit and Save";
                addToolStripMenuItem.Text = "Confirm Edit";
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
                    button6.Visible = false;
                    label78.Visible = false;
                    labelVendor.Visible = false;
                    comboBoxVendor.Visible = false;
                    comboBoxVendor.SelectedIndex = -1;
                }
            }
            else if (radioButton3.Checked)
            {
                button6.Visible = true;
                label78.Visible = true;
                labelVendor.Visible = false;
                comboBoxVendor.Visible = false;
                comboBoxVendor.SelectedIndex = -1;
            }
            else
            {
                button6.Visible = false;
                label78.Visible = false;
                labelVendor.Visible = true;
                comboBoxVendor.Visible = true;
                comboBoxVendor.SelectedIndex = -1;
            }
            DisableUndoReset();
            enable_vd_btn();
            enable_result_btn();
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                button6.Visible = true;
                label78.Visible = true;
                labelVendor.Visible = false;
                comboBoxVendor.Visible = false;
                comboBoxVendor.SelectedIndex = -1;


                if ((comboBox4.Text != "") && (comboBox17.Text != ""))
                {
                    button6.Enabled = true;
                    label78.Enabled = true;
                }
                else
                {
                    button6.Enabled = false;
                    label78.Enabled = false;
                }
            }
            else if (radioButton4.Checked)
            {
                button6.Visible = false;
                label78.Visible = false;
                labelVendor.Visible = false;
                comboBoxVendor.Visible = false;
                comboBoxVendor.SelectedIndex = -1;
            }
            else
            {
                button6.Visible = false;
                label78.Visible = false;
                labelVendor.Visible = true;
                comboBoxVendor.Visible = true;
                comboBoxVendor.SelectedIndex = -1;
            }
            DisableUndoReset();
            enable_vd_btn();
            enable_result_btn();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            f10.FormClosed += F10_FormClosed;
            f10.ShowDialog();

        }
        private void F10_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Form10.okclicked)
            {
                enable_result_btn();
                inputCableData_nec = new string[cableCount, 6];
                inputCableData_nec_metric = new double[cableCount];
                inputCableData_nec_unit = new string[cableCount];
                for (int i = 0; i < cableCount; i++)
                {
                    for (int q = 0; q < 6; q++)
                    {
                        inputCableData_nec[i, q] = Form10.confirmedcabledata_nec[i, q];
                    }
                    inputCableData_nec_metric[i] = Form10.confirmedcabledata_nec_metric[i];
                    inputCableData_nec_unit[i] = Form10.confirmedcabledata_nec_unit[i];
                }
            }
        }


        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fSettings.FormClosed += fSettings_FormClosed;
            fSettings.ShowDialog();
        }
        private void fSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Form1.decimalSeparatorChanged && Form1.okSetClicked)
            {
                refreshInputData();
                refreshDataTable();
                Form1.decimalSeparatorChanged = false;
            }
            //colortheme update
            LoadColor();
            SaveAllColor();
            Form1.okSetClicked = false;
        }

        //change decimal separator of data in data table based on the new selected decimal separator
        private void refreshDataTable()
        {
            for (int i = 0; i < Form1.j + 1; i++)
            {
                for (int k = 0; k < 39; k++)
                {
                    if (((k == 8) || ((k >= 10) && (k <= 16)) || ((k >= 18) && (k <= 24)) ||
                       ((k >= 26) && (k <= 37))))
                    {
                        if (Form1.decimalseparator == '.')
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
            foreach (DataRow row in Form1.dtdiameter.Rows)
            {
                if (Form1.decimalseparator == ',')
                {
                    for (int i = 0; i < 62; i++)
                    {
                        row[i] = Convert.ToString(row[i]).Replace('.', ',');
                    }
                }
                else
                {
                    for (int i = 0; i < 55; i++)
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
                textBox19.Text = ktmain.ToString();
            }
            if (sccurrent != 0)
            {
                textBox28.Text = sccurrent.ToString();
            }
            if (tbreaker != 0)
            {
                textBox23.Text = tbreaker.ToString();
            }
            if (cablesizemin != 0)
            {
                textBox30.Text = cablesizemin.ToString("0.##");
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
                comboBox17.Text = initialTemp.ToString();
                //textBox15.Text = initialTemp.ToString();
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
        }

        private void RadioButton2_Click_1(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                radioButton2.Checked = false;
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
                radioButton1.Checked = false;
            }
        }

        private void RadioButton1_Click_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                radioButton1.Checked = false;
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
            }
        }

        private void TextBox20_TextChanged(object sender, EventArgs e)
        {
            if (textBox20.Text != "")
            {
                bLTE = double.Parse(textBox20.Text);
            }
            SCLTEchanged();
            enable_result_btn();
        }

        private void TextBox20_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Form1.decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox20.Text == "") && (e.KeyChar == Form1.decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == Form1.decimalseparator) && ((sender as TextBox).Text.IndexOf(Form1.decimalseparator) > -1))
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
                panel14.BackColor = Color.Red;
                toolTip1.SetToolTip(comboBox14, "Voltage system needs to be chosen first");
            }
            if ((radioButton4.Checked) && (voltageLv == "MV"))
            {
                MessageBox.Show("Vendor data for Medium Voltage (MV) cable is not available, please input cable data manually.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                radioButton3.Checked = true;
            }
            DisableUndoReset();
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
            DisableUndoReset();
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
        int kttextboxX, kttextboxY, ktlabelX, ktlabelY;


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
                if (radioButtonVendor.Checked)
                {
                    calc_vd_custom();
                }
                else
                {
                    calc_vd();
                }
            }
        }

        private void TextBox36_TextChanged(object sender, EventArgs e)
        {
            if (textBox36.Text != "")
            {
                k3main = double.Parse(textBox36.Text);
            }
            else
            {
                k3main = 0;
            }

            DisableUndoReset();
            UpdatePosition();

            Updatekt();
            enable_result_btn();
        }


        private void TextBox17_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Form1.decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox17.Text == "") && (e.KeyChar == Form1.decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == Form1.decimalseparator) && ((sender as TextBox).Text.IndexOf(Form1.decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox18_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Form1.decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox18.Text == "") && (e.KeyChar == Form1.decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == Form1.decimalseparator) && ((sender as TextBox).Text.IndexOf(Form1.decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox36_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Form1.decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox36.Text == "") && (e.KeyChar == Form1.decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == Form1.decimalseparator) && ((sender as TextBox).Text.IndexOf(Form1.decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        /*
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
        */

        private void ComboBox15_SelectedIndexChanged(object sender, EventArgs e)
        {
            enable_result_btn();

            if (!((comboBox15.Text == "Update Size") || (comboBox15.Text == "")))
            {

                if (comboBox15.SelectedIndex == 1)
                {
                    m = i - 1;
                }
                else if (comboBox15.SelectedIndex == 2)
                {
                    m = i;
                }
                else if (comboBox15.SelectedIndex == 3)
                {
                    m = i + 1;
                }

                if (radioButton4.Checked)
                {
                    label85.Text = data_wirearea_unit[m];
                    textBox37.Text = data_wirearea[m];
                    
                }
                else if (radioButton3.Checked)
                {
                    label85.Text = inputCableData_nec_unit[m];
                    textBox37.Text = inputCableData_nec[m, 0];
                }
                else if (radioButtonVendor.Checked)
                {
                    label85.Text = nec_selected_wirearea_unit[m];
                    textBox37.Text = nec_selected_wirearea[m];
                }
                textBox15.Text = cmil.ToString("0.##");
            }

            if (!((comboBox15.Text == "Update Size") || (textBox37.Text == "") ||
                (comboBox15.Text == "")))
            {
                if (initialTemp == 60)
                {
                    insulindex = 1;
                }
                else if (initialTemp == 75)
                {
                    insulindex = 2;
                }
                else if (initialTemp == 90)
                {
                    insulindex = 3;
                }
                else
                {
                    insulindex = 0;
                }

                if (radioButton4.Checked)
                {
                    if (material == "Copper")
                    {
                        if (conduit == "PVC")
                        {
                            wirearea_nec = data_wirearea[m];
                            wirearea_unit = data_wirearea_unit[m];
                            wirearea_metric = data_wirearea_metric[m];

                            calc_cmil(); 

                            if (phase == "DC")
                            {
                                Rdc = data_electrical[m, 2];
                                vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                            }
                            else //AC
                            {
                                Rac = data_electrical[m, 2] * (234.5 + initialTemp) / (234.5 + 75);
                                X = data_electrical[m, 0];

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


                            if ((installation == "Raceways") || (installation == "Cable Tray / Ladder") || (installation == "Earth (Direct Buried)"))
                            {
                                Irated = data_ampacity_racewaycableearth[m, (insulindex - 1)] * n;
                            }
                            else if (installation == "Free Air")
                            {
                                Irated = data_ampacity_freeair[m, (insulindex - 1)] * n;
                            }

                            iderated = Irated * ktmain;
                            cable_lte();
                        }
                        else if (conduit == "Aluminium")
                        {
                            wirearea_nec = data_wirearea[m];
                            wirearea_unit = data_wirearea_unit[m];
                            wirearea_metric = data_wirearea_metric[m];

                            calc_cmil();

                            if (phase == "DC")
                            {
                                Rdc = data_electrical[m, 3];
                                vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                            }
                            else //AC
                            {
                                Rac = data_electrical[m, 3] * (234.5 + initialTemp) / (234.5 + 75);
                                X = data_electrical[m, 0];

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


                            if ((installation == "Raceways") || (installation == "Cable Tray / Ladder") || (installation == "Earth (Direct Buried)"))
                            {
                                Irated = data_ampacity_racewaycableearth[m, (insulindex - 1)] * n;
                            }
                            else if (installation == "Free Air")
                            {
                                Irated = data_ampacity_freeair[m, (insulindex - 1)] * n;
                            }

                            iderated = Irated * ktmain;
                            cable_lte();
                        }
                        else if (conduit == "Steel")
                        {
                            wirearea_nec = data_wirearea[m];
                            wirearea_unit = data_wirearea_unit[m];
                            wirearea_metric = data_wirearea_metric[m];

                            calc_cmil();

                            if (phase == "DC")
                            {
                                Rdc = data_electrical[m, 4];
                                vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                            }
                            else //AC
                            {
                                Rac = data_electrical[m, 4] * (234.5 + initialTemp) / (234.5 + 75);
                                X = data_electrical[m, 1];

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


                            if ((installation == "Raceways") || (installation == "Cable Tray / Ladder") || (installation == "Earth (Direct Buried)"))
                            {
                                Irated = data_ampacity_racewaycableearth[m, (insulindex - 1)] * n;
                            }
                            else if (installation == "Free Air")
                            {
                                Irated = data_ampacity_freeair[m, (insulindex - 1)] * n;
                            }

                            iderated = Irated * ktmain;
                            cable_lte();
                        }
                    }
                    else if (material == "Aluminium")
                    {
                        if (insulindex == 1)
                        {
                            insulindex = 4;
                        }
                        else if (insulindex == 2)
                        {
                            insulindex = 5;
                        }
                        else if (insulindex == 3)
                        {
                            insulindex = 6;
                        }

                        if (conduit == "PVC")
                        {
                            wirearea_nec = data_wirearea[m];
                            wirearea_unit = data_wirearea_unit[m];
                            wirearea_metric = data_wirearea_metric[m];

                            calc_cmil();

                            if (phase == "DC")
                            {
                                Rdc = data_electrical[m, 5];
                                vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                            }
                            else //AC
                            {
                                Rac = data_electrical[m, 5] * (228.1 + initialTemp) / (228.1 + 75);
                                X = data_electrical[m, 0];

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


                            if ((installation == "Raceways") || (installation == "Cable Tray / Ladder") || (installation == "Earth (Direct Buried)"))
                            {
                                Irated = data_ampacity_racewaycableearth[m, (insulindex - 1)] * n;
                            }
                            else if (installation == "Free Air")
                            {
                                Irated = data_ampacity_freeair[m, (insulindex - 1)] * n;
                            }

                            iderated = Irated * ktmain;
                            cable_lte();
                        }
                        else if (conduit == "Aluminium")
                        {
                            wirearea_nec = data_wirearea[m];
                            wirearea_unit = data_wirearea_unit[m];
                            wirearea_metric = data_wirearea_metric[m];

                            calc_cmil();

                            if (phase == "DC")
                            {
                                Rdc = data_electrical[m, 6];
                                vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                            }
                            else //AC
                            {
                                Rac = data_electrical[m, 6] * (228.1 + initialTemp) / (228.1 + 75);
                                X = data_electrical[m, 0];

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


                            if ((installation == "Raceways") || (installation == "Cable Tray / Ladder") || (installation == "Earth (Direct Buried)"))
                            {
                                Irated = data_ampacity_racewaycableearth[m, (insulindex - 1)] * n;
                            }
                            else if (installation == "Free Air")
                            {
                                Irated = data_ampacity_freeair[m, (insulindex - 1)] * n;
                            }

                            iderated = Irated * ktmain;
                            cable_lte();
                        }
                        else if (conduit == "Steel")
                        {
                            wirearea_nec = data_wirearea[m];
                            wirearea_unit = data_wirearea_unit[m];
                            wirearea_metric = data_wirearea_metric[m];
                            calc_cmil();

                            if (phase == "DC")
                            {
                                Rdc = data_electrical[m, 7];
                                vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                            }
                            else //AC
                            {
                                Rac = data_electrical[m, 7] * (228.1 + initialTemp) / (228.1 + 75);
                                X = data_electrical[m, 1];

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


                            if ((installation == "Raceways") || (installation == "Cable Tray / Ladder") || (installation == "Earth (Direct Buried)"))
                            {
                                Irated = data_ampacity_racewaycableearth[m, (insulindex - 1)] * n;
                            }
                            else if (installation == "Free Air")
                            {
                                Irated = data_ampacity_freeair[m, (insulindex - 1)] * n;
                            }

                            iderated = Irated * ktmain;
                            cable_lte();

                        }
                    }

                }
                else if (radioButton3.Checked) //manual cable database input
                {

                    wirearea_nec = inputCableData_nec[m, 0];
                    wirearea_unit = inputCableData_nec_unit[m];
                    wirearea_metric = inputCableData_nec_metric[m];

                    calc_cmil();

                    if (phase == "DC")
                    {
                        Rdc = Convert.ToDouble(inputCableData_nec[m, 2]);
                        Rac = 0;

                        vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                    }
                    else
                    {
                        Rac = Convert.ToDouble(inputCableData_nec[m, 2]);
                        Rdc = 0;
                        X = Convert.ToDouble(inputCableData_nec[m, 3]);

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

                    Irated = Convert.ToDouble(inputCableData_nec[m, 4]) * n;
                    diameter = Convert.ToDouble(inputCableData_nec[m, 5]);

                    iderated = Irated * ktmain;
                    cable_lte();
                }
                else if (radioButtonVendor.Checked)
                {

                    //get selected database base on its insulation and material type
                    SelectedDatabaseType();
                    //get the database length
                    SelectedDataLength();
                    wirearea_nec = nec_selected_wirearea[m];
                    wirearea_unit = nec_selected_wirearea_unit[m];
                    wirearea_metric = nec_selected_wirearea_metric[m];

                    calc_cmil();

                    if (phase == "DC")
                    {
                        Rdc = nec_selected_data_electrical[m, 0];
                        vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                    }
                    else //AC
                    {
                        Rac = nec_selected_data_electrical[m, 0];
                        X = nec_selected_data_electrical[m, 1];

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

                    //get irated value
                    Irated = nec_selected_data_electrical[m, 2] * n;


                    iderated = Irated * ktmain;

                    //cLTE = data_wirearea_metric[i] * data_wirearea_metric[i] * k * k;
                    cable_lte();

                }

                textBox12.Text = n.ToString();
                textBox8.Text = vdrun.ToString("0.##");
                textBox29.Text = lmax.ToString("0.##");
                //textBox22.Text = cLTE.ToString("0.##");

                if (material == "Copper")
                {
                    materialname = "Cu";
                }
                else if (material == "Aluminium")
                {
                    materialname = "Al";
                }

                if ((loadtype == "Motor") && ConsiderVdStart)
                {
                    textBox10.Text = vdstart.ToString("0.##");
                }

                readtemp = "";

                readtemp += n.ToString() + "  ×  " + cores.ToString("0.##") + "/C  #  " + wirearea_nec +
                    " " + wirearea_unit + "    " + ratedvoltage + " / " + materialname + " / " + insulation;



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
            textBox15.Text = "";
            textBox34.Text = "";
            textBox33.Text = "";
            textBox32.Text = "";
            textBox28.Text = "";
            textBox23.Text = "";
            textBox30.Text = "";
            textBox22.Text = "";
            finalTemp = 0;
            textBox24.Text = "";
            textBox21.Text = "";

            bLTE = 0;
            textBox20.Text = "";


            comboBox5.Enabled = true;
            textBox12.ReadOnly = false;
            comboBox15.SelectedIndex = -1;
            comboBox15.Items.Clear();
            comboBox10.SelectedIndex = -1;
            comboBox11.SelectedIndex = -1;
            comboBox11.Text = "";
            comboBox12.SelectedIndex = -1;
            comboBox21.Enabled = true;

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
            reset_correction();

            if (!radioButton7.Checked)
            {
                Updatek1();
                Updatek2();
                Updatek3();

                correctionfactor_fill();

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

                label93.Text = "";
                label94.Text = "";
                label95.Text = "";

                label93.Visible = false;
                label94.Visible = false;
                label95.Visible = false;

                textBox17.ReadOnly = false;
                textBox18.ReadOnly = false;
                textBox36.ReadOnly = false;

                if (((textBox17.Text == "") && (textBox18.Text == "") && (textBox36.Text == "")) ||
                    ((textBox17.Text != "") && (textBox18.Text == "") && (textBox36.Text == "")))
                {
                    label42.Visible = false;
                    label80.Visible = false;
                    textBox18.Visible = false;
                    textBox36.Visible = false;
                    textBox19.Location = textBox18.Location;
                    label43.Location = label42.Location;
                    panel24.Location = new Point(textBox18.Location.X - 4, textBox18.Location.Y);

                }
                /*
                else if ((textBox17.Text != "") && (textBox18.Text == ""))
                {
                    textBox36.Visible = false;
                    label80.Visible = false;

                    textBox19.Location = textBox36.Location;
                    label43.Location = label80.Location;
                }
                */

            }

            DisableUndoReset();

            Updatekt();
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
            DisableUndoReset();
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
            DisableUndoReset();
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

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                lengthunit = comboBox1.Text;
            }

            if (textBox6.Text != "")
            {
                if (lengthunit == "m")
                {
                    length = 3.28084 * double.Parse(textBox6.Text);
                }
                else
                {
                    length = double.Parse(textBox6.Text);
                }
            }
            else
            {
                length = 0;
            }

            DisableUndoReset();
            Updatek3();

            Updatekt();

            enable_vd_btn();
            enable_result_btn();
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

            //Form 10 (NEC Manual Input)
            if (Properties.Settings.Default.ColorTheme == 0) //default theme
            {
                f10.BackColor = Color.Gray;
                f10.BackColor = Color.White;
                f10.TopColor = Color.Transparent;
                f10.BottomColor = Color.Transparent;
                f10.Angle = 0;

                f10.dataGridView1.BackgroundColor = Color.White;

                f10.ForeColor = SystemColors.ControlText;
                f10.dataGridView1.ForeColor = SystemColors.ControlText;
            }
            else if (Properties.Settings.Default.ColorTheme == 1) //Skyblue theme
            {
                f10.BackColor = Color.Gray;
                f10.BackColor = Color.White;
                f10.TopColor = Color.Azure;
                f10.BottomColor = Color.LightCyan;
                f10.Angle = 300;

                f10.dataGridView1.BackgroundColor = Color.White;

                f10.ForeColor = SystemColors.ControlText;
                f10.dataGridView1.ForeColor = SystemColors.ControlText;

            }
            else if (Properties.Settings.Default.ColorTheme == 2) //Dark theme
            {
                f10.BackColor = Color.FromArgb(45, 46, 51);
                f10.TopColor = Color.Transparent;
                f10.BottomColor = Color.Transparent;
                f10.Angle = 0;

                f10.dataGridView1.BackgroundColor = Color.FromArgb(58, 59, 66);

                f10.ForeColor = SystemColors.ControlLightLight;
                f10.dataGridView1.ForeColor = SystemColors.ControlText;

            }
            else if (Properties.Settings.Default.ColorTheme == 3) //Pinky salmon theme
            {
                f10.BackColor = Color.Gray;
                f10.BackColor = Color.White;
                f10.TopColor = Color.Salmon;
                f10.BottomColor = Color.HotPink;
                f10.Angle = 0;

                f10.dataGridView1.BackgroundColor = Color.White;

                f10.ForeColor = SystemColors.ControlText;
                f10.dataGridView1.ForeColor = SystemColors.ControlText;

            }
            else if (Properties.Settings.Default.ColorTheme == 4) //Cable sizing theme
            {
                f10.BackColor = Color.Gray;
                f10.BackColor = Color.White;
                f10.TopColor = Color.Turquoise;
                f10.BottomColor = Color.DodgerBlue;
                f10.Angle = 60;

                f10.dataGridView1.BackgroundColor = Color.White;

                f10.ForeColor = SystemColors.ControlLightLight;
                f10.dataGridView1.ForeColor = SystemColors.ControlText;
            }
            else if (Properties.Settings.Default.ColorTheme == 5) //Visual Studio theme
            {
                f10.BackColor = Color.FromArgb(93, 107, 153);
                f10.TopColor = Color.Transparent;
                f10.BottomColor = Color.Transparent;
                f10.Angle = 0;

                f10.dataGridView1.BackgroundColor = Color.AliceBlue;

                f10.ForeColor = SystemColors.ControlLightLight;
                f10.dataGridView1.ForeColor = SystemColors.ControlText;
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

        private void ComboBox16_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (comboBox16.SelectedIndex != -1)
            {
                panel32.BackColor = Color.Transparent;
                index_temperature = comboBox16.SelectedIndex;
            }
            else
            {
                panel32.BackColor = Color.Red;
                index_temperature = -1;
            }

            DisableUndoReset();
            Updatek1();
            Updatekt();

            SCLTEchanged();
            enable_vd_btn();
        }




        private void ComboBox17_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox17.Text != "")
            {
                initialTemp = double.Parse(comboBox17.Text);
                panel33.BackColor = Color.Transparent;
                if ((insulation == "XHHW") || (insulation == "THHW"))
                {
                    if (initialTemp == 75)
                    {
                        insulindex = 2;
                    }
                    else if (initialTemp == 90)
                    {
                        insulindex = 3;
                    }
                }
                comboBox16.Enabled = true;
            }
            else
            {
                initialTemp = 0;
                panel33.BackColor = Color.Red;
                comboBox16.Enabled = false;
            }

            if (initialTemp == 60)
            {
                label33.Text = "at 60 ᵒC";
                label33.Visible = true;
            }
            else if (initialTemp == 75)
            {
                label33.Text = "at 75 ᵒC";
                label33.Visible = true;
            }
            else if (initialTemp == 90)
            {
                label33.Text = "at 90 ᵒC";
                label33.Visible = true;
            }
            else
            {
                label33.Text = "";
                label33.Visible = false;
            }

            DisableUndoReset();
            temp_fill();

            maxtemp_calc();

            Updatek1();
            Updatekt();

            SCLTEchanged();
            Calc_k();
            enable_vd_btn();
        }

        private void ComboBox18_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox18.SelectedIndex != -1)
            {
                groupconductor = comboBox18.Text;
                panel34.BackColor = Color.Transparent;
                index_groupedconductor = comboBox18.SelectedIndex;
            }
            else
            {
                groupconductor = "";
                panel34.BackColor = Color.Red;
                index_groupedconductor = -1;
            }



            DisableUndoReset();
            Updatek2();

            Updatekt();
            enable_vd_btn();
        }

        private void Updatek2()
        {
            if (!radioButton7.Checked)
            {
                if (comboBox18.SelectedIndex == 0)
                {
                    k2main = 1;
                }
                else if (comboBox18.SelectedIndex == 1)
                {
                    k2main = 0.8;
                }
                else if (comboBox18.SelectedIndex == 2)
                {
                    k2main = 0.7;
                }
                else if (comboBox18.SelectedIndex == 3)
                {
                    k2main = 0.5;
                }
                else if (comboBox18.SelectedIndex == 4)
                {
                    k2main = 0.45;
                }
                else if (comboBox18.SelectedIndex == 5)
                {
                    k2main = 0.4;
                }
                else if (comboBox18.SelectedIndex == 6)
                {
                    k2main = 0.35;
                }
                else
                {
                    k2main = 0;
                }
            }
        }

        private void ComboBox19_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox19.SelectedIndex != -1)
            {
                panel35.BackColor = Color.Transparent;
                index_distanceaboveroof = comboBox19.SelectedIndex;
            }
            else
            {
                panel35.BackColor = Color.Red;
                index_distanceaboveroof = -1;
            }



            DisableUndoReset();
            Updatek1();
            Updatekt();
            enable_vd_btn();
        }

        private void Updatek1()
        {
            tempindex = comboBox16.SelectedIndex + 1;

            if (comboBox16.Text != "")
            {
                temperature = double.Parse(comboBox16.Text);
            }
            else
            {
                temperature = 0;
            }

            if (!radioButton7.Checked)
            {
                if (comboBox16.SelectedIndex != -1)
                {
                    if (comboBox19.SelectedIndex != -1)
                    {
                        if (comboBox19.SelectedIndex == 0)
                        {
                            temperature = temperature + 0;
                        }
                        else if (comboBox19.SelectedIndex == 1)
                        {
                            temperature = temperature + 33;
                        }
                        else if (comboBox19.SelectedIndex == 2)
                        {
                            temperature = temperature + 22;
                        }
                        else if (comboBox19.SelectedIndex == 3)
                        {
                            temperature = temperature + 17;
                        }
                        else if (comboBox19.SelectedIndex == 4)
                        {
                            temperature = temperature + 14;
                        }


                        if (temperature <= 25)
                        {
                            tempindex = 1;
                        }
                        else if (temperature <= 30)
                        {
                            tempindex = 2;
                        }
                        else if (temperature <= 35)
                        {
                            tempindex = 3;
                        }
                        else if (temperature <= 40)
                        {
                            tempindex = 4;
                        }
                        else if (temperature <= 45)
                        {
                            tempindex = 5;
                        }
                        else if (temperature <= 50)
                        {
                            tempindex = 6;
                        }
                        else if (temperature <= 55)
                        {
                            tempindex = 7;
                        }
                        else if (temperature <= 60)
                        {
                            tempindex = 8;
                        }
                        else if (temperature <= 70)
                        {
                            tempindex = 9;
                        }
                        else if (temperature <= 80)
                        {
                            tempindex = 10;
                        }

                    }
                }



                if ((insulindex != 0) && (tempindex != 0))
                {
                    if (temperature > maxtemp)
                    {
                        MessageBox.Show("Temperature + added temperature can't be greater than " + maxtemp + " °C !", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        comboBox16.SelectedIndex = -1;
                        comboBox19.SelectedIndex = -1;
                        k1main = 0;
                        tempindex = comboBox16.SelectedIndex + 1;
                    }
                    else
                    {
                        k1main = correctionfactor_temperature[(tempindex - 1), (insulindex - 1)];
                    }
                }
            }

        }

        private void ComboBox20_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox20.SelectedIndex != -1)
            {
                panel36.BackColor = Color.Transparent;
                index_cablecover = comboBox20.SelectedIndex;
            }
            else
            {
                panel36.BackColor = Color.Red;
                index_cablecover = -1;
            }



            DisableUndoReset();
            Updatek3();
            Updatekt();
            enable_vd_btn();
        }

        private void ComboBox21_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox21.SelectedIndex != -1)
            {
                conduit = comboBox21.Text;
                panel37.BackColor = Color.Transparent;
                index_conduit = comboBox21.SelectedIndex;
            }
            else
            {
                conduit = "";
                panel37.BackColor = Color.Red;
                index_conduit = -1;
            }

            DisableUndoReset();
            enable_vd_btn();
        }

        private void ReturnToTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm.ReturnToTitle = true;
            Close();
        }

        private void Updatek3()
        {
            if (!radioButton7.Checked)
            {
                if (comboBox20.SelectedIndex == 0)
                {
                    k3main = 1;
                }
                else if (comboBox20.SelectedIndex == 1)
                {
                    if (length >= 6)
                    {
                        k3main = 0.95;
                    }
                    else
                    {
                        k3main = 1;
                    }
                }
                else
                {
                    k3main = 0;
                }
            }
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
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Form1.decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox24.Text == "") && (e.KeyChar == Form1.decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == Form1.decimalseparator) && ((sender as TextBox).Text.IndexOf(Form1.decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void ComboBox4_TextChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text != "")
            {
                if ((radioButton3.Checked) && (comboBox17.Text != ""))
                {
                    button6.Enabled = true;
                    label78.Enabled = true;
                }
                else
                {
                    button6.Enabled = false;
                    label78.Enabled = false;
                }
            }
            else
            {
                button6.Enabled = false;
                label78.Enabled = false;
            }

            Calc_k();
        }

        private void Calc_k()
        {
            if (textBox24.Text != "")
            {

                if (finalTemp > initialTemp)
                {
                    if (material == "Copper")
                    {
                        k = Math.Sqrt(0.0297 * Math.Log(1 + ((finalTemp - initialTemp) / (234 + initialTemp))));
                        //k = 226 * Math.Sqrt(Math.Log(1 + ((finalTemp - initialTemp) / (234.5 + initialTemp))));
                    }
                    else if (material == "Aluminium")
                    {
                        k = Math.Sqrt(0.0125 * Math.Log(1 + ((finalTemp - initialTemp) / (228  + initialTemp))));
                    }
                }
                else
                {
                    k = 0;
                }
            }
            else
            {
                k = 0;
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

        private void ComboBox17_TextChanged(object sender, EventArgs e)
        {
            if (comboBox17.Text != "")
            {
                initialTemp = double.Parse(comboBox17.Text);
                panel33.BackColor = Color.Transparent;
                if ((insulation == "XHHW") || (insulation == "THHW"))
                {
                    if (initialTemp == 75)
                    {
                        insulindex = 2;
                    }
                    else if (initialTemp == 90)
                    {
                        insulindex = 3;
                    }
                }

                comboBox16.Enabled = true;

                if ((radioButton3.Checked) && (comboBox4.Text != ""))
                {
                    button6.Enabled = true;
                    label78.Enabled = true;
                }
                else
                {
                    button6.Enabled = false;
                    label78.Enabled = false;
                }
            }
            else
            {
                initialTemp = 0;
                panel33.BackColor = Color.Red;
                comboBox16.Enabled = false;

                button6.Enabled = false;
                label78.Enabled = false;
            }

            if (initialTemp == 60)
            {
                label33.Text = "at 60 ᵒC";
                label33.Visible = true;
            }
            else if (initialTemp == 75)
            {
                label33.Text = "at 75 ᵒC";
                label33.Visible = true;
            }
            else if (initialTemp == 90)
            {
                label33.Text = "at 90 ᵒC";
                label33.Visible = true;
            }
            else
            {
                label33.Text = "";
                label33.Visible = false;
            }

            temp_fill();

            maxtemp_calc();

            Updatek1();
            Updatekt();

            SCLTEchanged();
            Calc_k();
            enable_vd_btn();


        }


        private void ComboBox5_TextChanged(object sender, EventArgs e)
        {
            if (comboBox5.Text != "")
            {
                cores = int.Parse(comboBox5.Text);
                panel30.BackColor = Color.Transparent;
            }
            else
            {
                cores = 0;
                panel30.BackColor = Color.Red;
            }
        }

        private void ComboBox2_TextChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                loadtype = comboBox2.Text;
                panel18.BackColor = Color.Transparent;
            }
            else
            {
                loadtype = "";
                panel18.BackColor = Color.Red;
            }
        }

        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            fAbout.ShowDialog();
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
            dtReset.Columns.Add("Temperature");
            dtReset.Columns.Add("IndexTemperature");
            dtReset.Columns.Add("IndexGroupedConductor");
            dtReset.Columns.Add("IndexDistanceAboveRoof");
            dtReset.Columns.Add("IndexCableCover");
            dtReset.Columns.Add("Conduit");
            dtReset.Columns.Add("IndexConduit");
            dtReset.Columns.Add("IndexLength");
            dtReset.Columns.Add("Remarks");
            dtReset.Columns.Add("Result");
            dtReset.Columns.Add("CustomDatabase");

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
            dtr[21] = armour;
            dtr[22] = outersheath;
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
                dtr[65] = "";
            }
            else if (radioButton3.Checked)
            {
                dtr[32] = "manual";
                dtr[65] = "";
            }
            else if (radioButtonVendor.Checked)
            {
                dtr[32] = "custom";
                dtr[65] = comboBoxVendor.Text;
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
            if (Convert.ToString(dtr[41]) != "")
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
            dtr[46] = comboBox17.Text;
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
            dtr[55] = comboBox16.Text;
            dtr[56] = comboBox16.SelectedIndex;
            dtr[57] = comboBox18.SelectedIndex;
            dtr[58] = comboBox19.SelectedIndex;
            dtr[59] = comboBox20.SelectedIndex;
            dtr[60] = comboBox21.Text;
            dtr[61] = comboBox21.SelectedIndex;
            dtr[62] = comboBox1.SelectedIndex;
            dtr[63] = textBox35.Text;
            dtr[64] = tbResult.Text;

            dtReset.Rows.Add(dtr);


            //reset all the  data
            ResetData();

            //enable the undo reset button
            undoResetToolStripMenuItem.Enabled = true;
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

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1.OpenFromMain = true;
            f5.OpenFileDecide();
            if (Form1.FileOpened)
            {
                OpenDataTable();
                Form1.FileOpened = false;
            }
            Form1.OpenFromMain = false;
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

        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddToResult();
        }

        private void UndoResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UndoReset();
            DisableUndoReset();
        }

        private void UndoReset()
        {
            DataRow dtx = dtReset.Rows[0];

            bool noDatabase = false;
            if (Convert.ToString(dtx[32]) == "custom")
            {
                radioButtonVendor.Checked = true;
                if (comboBoxVendor.Items.Contains(Convert.ToString(dtx[65])))
                {
                    comboBoxVendor.SelectedItem = Convert.ToString(dtx[65]);
                }
                else
                {
                    MessageBox.Show("Error: Cable Database \"" + Convert.ToString(dtx[65]) + "\" not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            else if (Convert.ToString(dtx[32]) == "vendor")
            {
                radioButton4.Checked = true;
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
                if (DtrToDoubleText(dtx, 12) != "")
                {
                    if (radioButton8.Checked)
                    {
                        current = Convert.ToDouble(dtx[12]);
                        textBox3.Text = current.ToString("0.##"); // FL Current
                    }
                    else
                    {
                        textBox3.Text = DtrToDoubleText(dtx, 12);
                    }
                }
                textBox2.Text = DtrToDoubleText(dtx, 13); //Voltage
                textBox5.Text = DtrToDoubleText(dtx, 14); //eff
                textBox4.Text = DtrToDoubleText(dtx, 15); //pf
                textBox14.Text = DtrToDoubleText(dtx, 16); //pfstart
                textBox25.Text = DtrToDoubleText(dtx, 17); //multiplier
                comboBox14.Text = DtrToDoubleText(dtx, 18);//ratedvoltage
                comboBox4.Text = Convert.ToString(dtx[19]);//material
                comboBox6.Text = Convert.ToString(dtx[20]); //insulation
                                                            //comboBox7.Text = Convert.ToString(dtx[21]); //armour
                                                            //comboBox8.Text = Convert.ToString(dtx[22]); //outer Sheath
                comboBox9.Text = Convert.ToString(dtx[23]); //installation

                if (radioButtonVendor.Checked)
                {
                    SelectedDatabaseType();
                    SelectedDataLength();
                }
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
                if (DtrToDoubleText(dtx, 28) != "")
                {
                    ktmain = Convert.ToDouble(dtx[28]);
                    textBox19.Text = ktmain.ToString("0.##");
                }
                textBox6.Text = DtrToDoubleText(dtx, 29); //length
                textBox9.Text = DtrToDoubleText(dtx, 30); //vdrunmax
                textBox11.Text = DtrToDoubleText(dtx, 31); //vdstartmax
                if (Convert.ToString(dtx[32]) == "vendor")
                {
                    radioButton4.Checked = true;
                }
                else if (Convert.ToString(dtx[32]) == "manual")
                {
                    radioButton3.Checked = true;
                }
                else if (Convert.ToString(dtx[32]) == "custom")
                {
                    radioButtonVendor.Checked = true;
                }
                if (DtrToDoubleText(dtx, 33) != "")
                {
                    vdrun = Convert.ToDouble(dtx[33]);
                    textBox8.Text = vdrun.ToString("0.##"); //vdrun
                }
                else
                {
                    textBox8.Text = "";
                }
                if (DtrToDoubleText(dtx, 34) != "")
                {
                    vdstart = Convert.ToDouble(dtx[34]);
                    textBox10.Text = vdstart.ToString("0.##"); //vdstart
                }
                else
                {
                    textBox10.Text = "";
                }
                if (DtrToDoubleText(dtx, 35) != "")
                {
                    lmax = Convert.ToDouble(dtx[35]);
                    textBox29.Text = lmax.ToString("0.##");  //lmax
                }
                textBox12.Text = Convert.ToString(dtx[36]); //no of run
                comboBox5.Text = Convert.ToString(dtx[37]); //no of cores
                if (Convert.ToString(dtx[38]) != "")
                {
                    //Disable all inputs before vd calculation
                    panel4.Enabled = true;
                    panel5.Enabled = false;
                    panel6.Enabled = false;

                    //enable vd calculate & edit data buttons
                    button7.Enabled = true;
                    button8.Enabled = true;

                    textBox37.Text = Convert.ToString(dtx[38]); //wirearea
                    wirearea_nec = Convert.ToString(dtx[38]);
                    i = Convert.ToInt32(dtx[54]);
                    Update_size();
                }
                else
                {
                    textBox37.Text = "";
                }
                if ((Convert.ToString(dtx[6]) == "DC") && (DtrToDoubleText(dtx, 39) != ""))
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
                if (DtrToDoubleText(dtx, 41) != "")
                {
                    Irated = Convert.ToDouble(dtx[41]);
                    textBox32.Text = Irated.ToString("0.##"); //irated
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
                comboBox17.Text = DtrToDoubleText(dtx, 46); //initialtemp
                textBox24.Text = DtrToDoubleText(dtx, 47); //finaltemp
                if (DtrToDoubleText(dtx, 48) != "")
                {
                    cLTE = Convert.ToDouble(dtx[48]);
                    textBox22.Text = cLTE.ToString("0.##"); //CLTE
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

                textBox35.Text = Convert.ToString(dtx[63]);
                tbResult.Text = Convert.ToString(dtx[64]);
                if (DtrToDoubleText(dtx, 55) != "")
                {
                    temperature = Convert.ToDouble(dtx[55]);
                    comboBox16.Text = temperature.ToString();
                }
                comboBox16.SelectedIndex = Convert.ToInt32(dtx[56]);
                comboBox18.SelectedIndex = Convert.ToInt32(dtx[57]);
                comboBox19.SelectedIndex = Convert.ToInt32(dtx[58]);
                comboBox20.SelectedIndex = Convert.ToInt32(dtx[59]);
                conduit = Convert.ToString(dtx[60]);
                comboBox21.SelectedIndex = Convert.ToInt32(dtx[61]);
                comboBox1.SelectedIndex = Convert.ToInt32(dtx[62]);

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

        private void DisableUndoReset()
        {
            undoResetToolStripMenuItem.Enabled = false;
        }

        private void Panel21_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Panel37_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AddCableDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            faddcable = new FormAddCableDatabase();
            faddcable.FormClosed += AddDatabaseClosed;
            faddcable.tabControl1.SelectedIndex = 1;
            faddcable.radioButtonNECView.Checked = true;
            faddcable.radioButtonNEC.Checked = true;
            faddcable.ShowDialog();
        }
        private void AddDatabaseClosed(object sender, FormClosedEventArgs e)
        {
            if (FormAddCableDatabase.databaseEdited)
            {
                LoadNECDatabase();
                FormAddCableDatabase.databaseEdited = false;
            }
        }

        internal void LoadNECDatabase()
        {
            //read all file in database directory
            string systemPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Directory.CreateDirectory(systemPath + "/Cable Sizing/NEC_database");
            string saveDir = (systemPath + "/Cable Sizing/NEC_database");

            di = new DirectoryInfo(saveDir);
            //get all file name in database directory
            FileInfo[] files = di.GetFiles("*.xml");
            NECDatabaseFiles = files.Length;
            //fill vendor data from database
            comboBoxVendor.Items.Clear();
            //fill all saved database created by user
            for (int z = 0; z < NECDatabaseFiles; z++)
            {
                string tempstring;
                tempstring = files[z].ToString();
                tempstring = tempstring.Replace(".xml", "");
                comboBoxVendor.Items.Insert(z, tempstring);
            }
        }

        private void ComboBoxVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBoxVendor, comboBoxVendor.Text);

            SelectedDatabase = comboBoxVendor.Text;
            //Gather cable specification of the selected database
            if ((SelectedDatabase != "Sumi Indo Cable (Default)") && (SelectedDatabase != ""))
            {
                ReadNECDatabase();
            }
            else if (SelectedDatabase == "")
            {
                NEC00DB = null;
                NEC10DB = null;
                NEC20DB = null;
                NEC01DB = null;
                NEC11DB = null;
                NEC21DB = null;
            }

            DisableUndoReset();
            enable_vd_btn();
        }

        internal void ReadNECDatabase()
        {
            string systemPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Directory.CreateDirectory(systemPath + "/Cable Sizing/NEC_database");
            string saveDir = Path.Combine(systemPath + "/Cable Sizing/NEC_database", SelectedDatabase + ".xml");

            //save database to a new dataset
            cableDS = new DataSet();
            cableDS.ReadXml(saveDir);

            //save each cable data table to their respective datatable
            if (cableDS.Tables.Contains("NEC00"))
            {
                NEC00DB = cableDS.Tables["NEC00"].Copy();
                nec00DB_data_electrical = new double[NEC00DB.Rows.Count, NEC00DB.Columns.Count-1];
                nec00DB_wirearea = new string[NEC00DB.Rows.Count];
                nec00DB_wirearea_unit = new string[NEC00DB.Rows.Count];
                nec00DB_wirearea_metric = new double[NEC00DB.Rows.Count];
                DTToArrayDouble_SelectStart(NEC00DB, nec00DB_data_electrical, 1);
                DTToArrayString_SelectColumn(NEC00DB, nec00DB_wirearea, 0);
                nec00DB_wirearea_unit = new string[nec00DB_wirearea.Length];
                nec00DB_wirearea_metric = new double[nec00DB_wirearea.Length];
                SynchronizeAreaUnitAndMetric(nec00DB_wirearea, nec00DB_wirearea_unit, nec00DB_wirearea_metric);
                nec00Length = NEC00DB.Rows.Count;
            }
            else
            {
                nec00Length = 0;
            }
            if (cableDS.Tables.Contains("NEC10"))
            {
                NEC10DB = cableDS.Tables["NEC10"].Copy();
                nec10DB_data_electrical = new double[NEC10DB.Rows.Count, NEC10DB.Columns.Count-1];
                nec10DB_wirearea = new string[NEC10DB.Rows.Count];
                nec10DB_wirearea_unit = new string[NEC10DB.Rows.Count];
                nec10DB_wirearea_metric = new double[NEC10DB.Rows.Count];
                DTToArrayDouble_SelectStart(NEC10DB, nec10DB_data_electrical, 1);
                DTToArrayString_SelectColumn(NEC10DB, nec10DB_wirearea, 0);
                SynchronizeAreaUnitAndMetric(nec10DB_wirearea, nec10DB_wirearea_unit, nec10DB_wirearea_metric);
                nec10Length = NEC10DB.Rows.Count;
            }
            else
            {
                nec10Length = 0;
            }
            if (cableDS.Tables.Contains("NEC20"))
            {
                NEC20DB = cableDS.Tables["NEC20"].Copy();
                nec20DB_data_electrical = new double[NEC20DB.Rows.Count, NEC20DB.Columns.Count-1];
                nec20DB_wirearea = new string[NEC20DB.Rows.Count];
                nec20DB_wirearea_unit = new string[NEC20DB.Rows.Count];
                nec20DB_wirearea_metric = new double[NEC20DB.Rows.Count];
                DTToArrayDouble_SelectStart(NEC20DB, nec20DB_data_electrical, 1);
                DTToArrayString_SelectColumn(NEC20DB, nec20DB_wirearea, 0);
                SynchronizeAreaUnitAndMetric(nec20DB_wirearea, nec20DB_wirearea_unit, nec20DB_wirearea_metric);
                nec20Length = NEC20DB.Rows.Count;
            }
            else
            {
                nec20Length = 0;
            }
            if (cableDS.Tables.Contains("NEC01"))
            {
                NEC01DB = cableDS.Tables["NEC01"].Copy();
                nec01DB_data_electrical = new double[NEC01DB.Rows.Count, NEC01DB.Columns.Count-1];
                nec01DB_wirearea = new string[NEC01DB.Rows.Count];
                nec01DB_wirearea_unit = new string[NEC01DB.Rows.Count];
                nec01DB_wirearea_metric = new double[NEC01DB.Rows.Count];
                DTToArrayDouble_SelectStart(NEC01DB, nec01DB_data_electrical, 1);
                DTToArrayString_SelectColumn(NEC01DB, nec01DB_wirearea, 0);
                SynchronizeAreaUnitAndMetric(nec01DB_wirearea, nec01DB_wirearea_unit, nec01DB_wirearea_metric);
                nec01Length = NEC01DB.Rows.Count;
            }
            else
            {
                nec01Length = 0;
            }
            if (cableDS.Tables.Contains("NEC11"))
            {
                NEC11DB = cableDS.Tables["NEC11"].Copy();
                nec11DB_data_electrical = new double[NEC11DB.Rows.Count, NEC11DB.Columns.Count-1];
                nec11DB_wirearea = new string[NEC11DB.Rows.Count];
                nec11DB_wirearea_unit = new string[NEC11DB.Rows.Count];
                nec11DB_wirearea_metric = new double[NEC11DB.Rows.Count];
                DTToArrayDouble_SelectStart(NEC11DB, nec11DB_data_electrical, 1);
                DTToArrayString_SelectColumn(NEC11DB, nec11DB_wirearea, 0);
                SynchronizeAreaUnitAndMetric(nec11DB_wirearea, nec11DB_wirearea_unit, nec11DB_wirearea_metric);
                nec11Length = NEC11DB.Rows.Count;
            }
            else
            {
                nec11Length = 0;
            }
            if (cableDS.Tables.Contains("NEC21"))
            {
                NEC21DB = cableDS.Tables["NEC21"].Copy();
                nec21DB_data_electrical = new double[NEC21DB.Rows.Count, NEC21DB.Columns.Count-1];
                nec21DB_wirearea = new string[NEC21DB.Rows.Count];
                nec21DB_wirearea_unit = new string[NEC21DB.Rows.Count];
                nec21DB_wirearea_metric = new double[NEC21DB.Rows.Count];
                DTToArrayDouble_SelectStart(NEC21DB, nec21DB_data_electrical, 1);
                DTToArrayString_SelectColumn(NEC21DB, nec21DB_wirearea, 0);
                SynchronizeAreaUnitAndMetric(nec21DB_wirearea, nec21DB_wirearea_unit, nec21DB_wirearea_metric);
                nec21Length = NEC21DB.Rows.Count;
            }
            else
            {
                nec21Length = 0;
            }
        }

        private void SelectedDataLength()
        {
            if (material == "Copper")
            {
                switch (insulindex)
                {
                    case 1:
                        currentDataLength = nec00Length;
                        break;
                    case 2:
                        currentDataLength = nec10Length;
                        break;
                    case 3:
                        currentDataLength = nec20Length;
                        break;
                }
            }
            else if (material == "Aluminium")
            {

                switch (insulindex)
                {
                    case 1:
                        currentDataLength = nec01Length;
                        break;
                    case 2:
                        currentDataLength = nec11Length;
                        break;
                    case 3:
                        currentDataLength = nec21Length;
                        break;
                }
            }
        }

        //Fill matrix of double with data from a datatable, assuming that matrix size is at least the same size as the datatable
        private void DTToArrayDouble(DataTable dt, double[,] arr)
        {
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

        //Fill a matrix of double with data from a datatable starting from selected column index,
        //assuming that matrix size is at least the same size as the datatable
        private void DTToArrayDouble_SelectStart(DataTable dt, double[,] arr, int start)
        {
            int dtColumn = dt.Columns.Count;

            int row = 0;
            int c;
            foreach (DataRow dr in dt.Rows)
            {
                c = 0;
                for (int col = start; col < dtColumn; col++)
                {
                    arr[row, c] = Convert.ToDouble(dr[col]);
                    c++;
                }
                row++;
            }
        }

        //Fill an array of string with data from a specific column of a datatable, 
        //assuming that the matrix size is at least the same size as the datatable
        private void DTToArrayString_SelectColumn(DataTable dt, string[] arr, int selectCol)
        {
            int row = 0;
            foreach (DataRow dr in dt.Rows)
            {
                arr[row] = Convert.ToString(dr[selectCol]);
                row++;
            }
        }

        //synchronize the wire area unit and mectric wire area to the wire area of the selected custom database, 
        //asumming that wirearea_unit and wirearea_metric have suitable sizes
        private void SynchronizeAreaUnitAndMetric(string[] wirearea, string[] wirearea_unit, double[] wirearea_metric)
        {
            int length;
            int l = 0;
            int k = 0;
            length = wirearea.Length;

            while ((l < length) && (k < 21))
            {
                if (wirearea[l] == data_wirearea[k])
                {
                    wirearea_unit[l] = data_wirearea_unit[k];
                    wirearea_metric[l] = data_wirearea_metric[k];
                    l++;
                }
                k++;
            }
        }

        private void ViewEditCableDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            faddcable = new FormAddCableDatabase();
            faddcable.FormClosed += AddDatabaseClosed;
            faddcable.tabControl1.SelectedIndex = 0;
            faddcable.radioButtonNECView.Checked = true;
            faddcable.radioButtonNEC.Checked = true;
            faddcable.ShowDialog();
        }

        private void NECStandardCableDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f8 = new Form8();
            f8.Show();
        }

        private void TextBox37_TextChanged(object sender, EventArgs e)
        {
            if (textBox37.Text != "")
            {
                wirearea_nec = textBox37.Text;
                wirearea_unit = label85.Text;
                calc_cmil();
            }
            else
            {
                wirearea_nec = "";
                wirearea_unit = "";
                cmil = 0;
            }
        }

        private void TextBox27_TextChanged(object sender, EventArgs e)
        {
            to = textBox27.Text;
            DisableUndoReset();
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
            DisableUndoReset();
            enable_vd_btn();
            enable_result_btn();
        }

        private void TextBox21_TextChanged(object sender, EventArgs e)
        {
            if (textBox21.Text != "")
            {
                k = double.Parse(textBox21.Text);
            }

            cable_lte();
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
            DisableUndoReset();
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
                (comboBox6.Text != "") && (textBox9.Text != "") && (comboBox14.Text != "") &&(comboBox9.Text != "") && 
                (textBox19.Text != "") && (textBox6.Text != "") && (textBox12.Text != "") && (comboBox5.Text != "")  && 
                (comboBox4.Text != "") && (textBox13.Text != "") && (comboBox17.Text != "") && (comboBox16.Text != "") &&
                (comboBox21.Text != ""))
            {
                if (((radioButton3.Checked) && (cableCount > 0)) || (radioButton4.Checked) || (radioButtonVendor.Checked && (comboBoxVendor.Text != "")))
                {
                    if ((loadtype == "Motor") && ConsiderVdStart)
                    {
                        if ((textBox7.Text != "") && (textBox14.Text != "") && (textBox11.Text != "") && (textBox25.Text != ""))
                        {
                            if (installation == "Raceways")
                            {
                                if ((comboBox18.Text == "") || (comboBox19.Text == ""))
                                {
                                    button7.Enabled = false;
                                }
                                else
                                {
                                    button7.Enabled = true;
                                }
                            }
                            else if (installation == "Cable Tray / Ladder")
                            {
                                if ((comboBox18.Text == "") || (comboBox20.Text == ""))
                                {
                                    button7.Enabled = false;
                                }
                                else
                                {
                                    button7.Enabled = true;
                                }
                            }
                            else if ((installation == "Earth (Direct Buried)") || (installation == "Free Air"))
                            {
                                if (comboBox18.Text == "")
                                {
                                    button7.Enabled = false;
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
                    else
                    {
                        if (installation == "Raceways")
                        {
                            if ((comboBox18.Text == "") || (comboBox19.Text == ""))
                            {
                                button7.Enabled = false;
                            }
                            else
                            {
                                button7.Enabled = true;
                            }
                        }
                        else if (installation == "Cable Tray / Ladder")
                        {
                            if ((comboBox18.Text == "") || (comboBox20.Text == ""))
                            {
                                button7.Enabled = false;
                            }
                            else
                            {
                                button7.Enabled = true;
                            }
                        }
                        else if ((installation == "Earth (Direct Buried)") || (installation == "Free Air"))
                        {
                            if (comboBox18.Text == "")
                            {
                                button7.Enabled = false;
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
                (textBox3.Text != "") && (textBox4.Text != "") && (textBox5.Text != "") && 
                (textBox9.Text != "") && (textBox6.Text != "") && (textBox12.Text != "") && 
                (comboBox5.Text != "") && (comboBox6.Text != "") && (comboBox9.Text != "") && 
                (textBox19.Text != "") && (comboBox11.Text != "") && (comboBox4.Text != "") && 
                (comboBox10.Text != "") && (comboBox12.Text != "")&& (comboBox13.Text != "") && 
                (comboBox14.Text != "") && (textBox13.Text!= "") && (textBox24.Text != ""))
                {
                    if (((radioButton3.Checked) && (cableCount > 0)) || (radioButton4.Checked) || (radioButtonVendor.Checked && (comboBoxVendor.Text != "")))
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
            if (textBox21.Text != "")
            {
                cLTE = wirearea_metric * wirearea_metric * k * k;
            }
            else
            {
                cLTE = 0;
            }

            if (cLTE != 0)
            {
                textBox22.Text = cLTE.ToString("0.##");
            }
            else
            {
                textBox22.Text = "";
            }
        }

        private void calc_smin()
        {
            if ((textBox28.Text != "") && (textBox23.Text != "") && (textBox21.Text != ""))
            {
                smin = sccurrent * 1000 * Math.Sqrt(tbreaker) / k;
            }
            else
            {
                smin = 0;
            }

            if (radioButton4.Checked)
            {
                
                cablesizemin = smin;

                for (int d = 0; d < 21; d++)
                {
                    if (smin <= data_wirearea_metric[d])
                    {
                        smin_nec = data_wirearea[d];
                        label68.Text = "cmil";
                        break;
                    }
                }
                
            }
            else if (radioButton3.Checked) //manual data
            {
                
                cablesizemin = smin;

                for (int d = 0; d < cableCount; d++)
                {
                    if (smin <= inputCableData_nec_metric[d])
                    {
                        smin_nec = inputCableData_nec[d, 0];
                        label68.Text = "cmil";
                        break;
                    }
                }
                
            }
            else if (radioButtonVendor.Checked)
            {
                
                cablesizemin = smin;

                for (int d = 0; d < currentDataLength; d++)
                {
                    if (smin <= nec_selected_wirearea_metric[d])
                    {
                        smin_nec = nec_selected_wirearea[d];
                        label68.Text = "cmil";
                        break;
                    }
                }
                
            }


            if (smin != 0)
            {
                textBox30.Text = smin.ToString("0.##");
            }
            else
            {
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
            results[17] = wirearea_nec;
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
            dtr = Form1.dtdiameter.NewRow();
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
                dtr[65] = "";
            }
            else if (radioButton3.Checked)
            {
                dtr[32] = "manual";
                dtr[65] = "";
            }
            else if (radioButtonVendor.Checked)
            {
                dtr[32] = "custom";
                dtr[65] = comboBoxVendor.Text;
            }
            dtr[33] = vdrun;
            dtr[35] = lmax;
            dtr[36] = n;
            dtr[37] = cores;
            dtr[38] = wirearea_nec;
            dtr[39] = Rac;
            dtr[40] = X;
            dtr[41] = Irated;
            dtr[42] = iderated;
            dtr[43] = "";
            dtr[44] = "";
            dtr[45] = "";
            dtr[46] = initialTemp;
            dtr[47] = finalTemp;
            dtr[48] = cLTE;
            dtr[54] = i;
            dtr[55] = temperature;
            dtr[56] = comboBox16.SelectedIndex;
            dtr[57] = comboBox18.SelectedIndex;
            dtr[58] = comboBox19.SelectedIndex;
            dtr[59] = comboBox20.SelectedIndex;
            dtr[60] = conduit;
            dtr[61] = comboBox21.SelectedIndex;
            dtr[62] = comboBox1.SelectedIndex;
            dtr[63] = textBox35.Text;
            dtr[64] = tbResult.Text;

            for (int i = 49; i < 54; i++)
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
            results[17] = wirearea_nec;
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

            dtr = Form1.dtdiameter.NewRow();
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
                dtr[65] = "";
            }
            else if (radioButton3.Checked)
            {
                dtr[32] = "manual";
                dtr[65] = "";
            }
            else if (radioButtonVendor.Checked)
            {
                dtr[32] = "custom";
                dtr[65] = comboBoxVendor.Text;
            }
            dtr[33] = vdrun;
            dtr[35] = lmax;
            dtr[36] = n;
            dtr[37] = cores;
            dtr[38] = wirearea_nec;
            dtr[39] = Rac;
            dtr[40] = X;
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
            dtr[55] = temperature;
            dtr[56] = index_temperature;
            dtr[57] = index_groupedconductor;
            dtr[58] = index_distanceaboveroof;
            dtr[59] = index_cablecover;
            dtr[60] = conduit;
            dtr[61] = index_conduit;
            dtr[62] = comboBox1.SelectedIndex;
            dtr[63] = textBox35.Text;
            dtr[64] = tbResult.Text;

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

    }
}
