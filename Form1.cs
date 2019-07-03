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
    
    public partial class Form1 : Form
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

        double vdrunmax, vdstartmax, vdrun, vdstart;
        double length;

        int n = 1;
        string tagno, from, to, fromdesc, todesc;
        string material, armour, innersheath, outersheath;
        string breakertype;
        string remarks;
        public static string insulation, installation;
        int cores;
        double breakcurrent;

        public static double k1main, k2main, ktmain;
        public static bool ok_clicked;

        bool complete, inputValid;
        int i= 0;

        string readtemp;
        double Rac;
        double X;
        double Rdc;
        double Irated;
        double wirearea;
        double iderated;

        double tbreaker;
        double bLTE;
        double cLTE;
        double k;
        double sc_wiremin;
        double lmax;
        double smin;
        double cablesizemin;
        


        public static int j = -1;
        Form5 f5 = new Form5();
        Form6 f6 = new Form6();
        FSettings fSettings = new FSettings();

        public static string[] results = new string[33];

        public static char decimalseparator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);

        public static double[,] inputCableData;
        public static int cableCount;

        public Form1()
        {

            InitializeComponent();
        }

        double[,] xlpe2core = new double[17, 6] 
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

        double[,] xlpe3core = new double[17, 6]
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

        double[,] xlpe4core = new double[17, 6]
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

        double[,] pvc2core = new double[16, 6]
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

        double[,] pvc3core = new double[16, 6]
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

        double[,] pvc4core = new double[16, 6]
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
            }
            calc_current();
            enable_result_btn();
        }


        private void TextBox2_Leave(object sender, EventArgs e)
        {
            calc_current();
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((comboBox3.Text == "DC") && (comboBox2.Text == "Static"))
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
            }


            comboBox2.Items.Clear();
            comboBox5.Items.Clear();
            if (comboBox3.Text == "Three-Phase AC")
            {
                comboBox5.Items.Insert(0, 3);
                comboBox5.Items.Insert(1, 4);

                comboBox2.Items.Insert(0, "Static");
                comboBox2.Items.Insert(1, "Dynamic");
            }
            else if (comboBox3.Text == "Single-Phase AC")
            {
                comboBox5.Items.Insert(0, 2);

                comboBox2.Items.Insert(0, "Static");
                comboBox2.Items.Insert(1, "Dynamic");
            }
            else if (comboBox3.Text == "DC")
            {
                comboBox5.Items.Insert(0, 2);

                comboBox2.Items.Insert(0, "Static");
            }

            calc_current();
            enable_result_btn();
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((comboBox3.Text == "DC") && (comboBox2.Text == "Static"))
            {
                pf = 1;
                textBox4.Text = pf.ToString("R");
                textBox4.ReadOnly = true;
            }
            else
            {
                textBox4.ReadOnly = false;
            }

            if(comboBox2.Text != "")
            {
                loadtype = comboBox2.Text;
            }

            

            if (comboBox2.Text == "Dynamic")
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
                label28.Enabled = true;
                label29.Enabled = true;
                textBox7.Enabled = true;
                textBox25.Enabled = true;
                label59.Enabled = true;

            }
            else
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

                textBox25.Text = "";
                textBox14.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox7.Text = "";
                vdstart = 0;
                vdstartmax = 0;
                pfstart = 0;
                currentstart = 0;
            }
            
            calc_current();
            enable_result_btn();
        }

        private void CbPower_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            }
            enable_result_btn();
        }

        private void TextBox12_TextChanged(object sender, EventArgs e)
        {
            if (textBox12.Text != "")
            {
                n = int.Parse(textBox12.Text);
            }
            enable_result_btn();
        }

        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.Text != "")
            {
                cores = int.Parse(comboBox5.Text);
            }
            
            enable_result_btn();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            cbPower.Text = "kW";
            for(int z = 0; z < 33; z++)
            {
                results[z] = "-";
            }

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
            if (comboBox9.Text == "Under Ground")
            {
                Form2 f2 = new Form2();
                f2.FormClosed += F2_FormClosed;
                f2.ShowDialog();
            }
            else if (comboBox9.Text == "Above Ground")
            {
                Form3 f3 = new Form3();
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
            enable_result_btn();
        }

        private void ComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            insulation = comboBox6.Text;
            if(insulation == "PVC")
            {
                textBox15.Text = "70";
                textBox24.Text = "160";
                textBox21.Text = "115";
            }
            else if (insulation == "XLPE")
            {
                textBox15.Text = "90";
                textBox24.Text = "250";
                textBox21.Text = "143";
            }
            else
            {
                textBox15.Text = "";
                textBox24.Text = "";
                textBox21.Text = "";
            }

            reset_correction();
            enable_correction();
            enable_result_btn();
        }

        private void ComboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            armour = comboBox7.Text;
            enable_result_btn();
        }

        private void ComboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            installation = comboBox9.Text;

            reset_correction();
            enable_correction();
            enable_result_btn();
        }

        public void area_calc()
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
                            while ((!complete) && (i < 17))
                            {
                                wirearea = xlpe2core[i, 0];
                                if (phase == "DC")
                                {
                                    Rdc = xlpe2core[i, 3];
                                    vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                }
                                else //AC
                                {
                                    Rac = xlpe2core[i, 1];
                                    X = xlpe2core[i, 2];

                                    if (phase == "Single-Phase AC")
                                    {
                                        vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                        / (n * 1000 * voltage);

                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                            (X * Math.Sqrt(1 - pf * pf))) * 100);

                                        if (loadtype == "Dynamic")
                                        {
                                            vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                            / (n * 1000 * voltage);
                                        }
                                    }
                                    else if (phase == "Three-Phase AC")
                                    {
                                        vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                        / (n * 1000 * voltage);

                                        lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                            (X * Math.Sqrt(1 - pf * pf))) * 100);

                                        if (loadtype == "Dynamic")
                                        {
                                            vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                            / (n * 1000 * voltage);
                                        }
                                    }
                                }



                                if (installation == "Under Ground")
                                {
                                    Irated = xlpe2core[i, 4] * n;
                                }
                                else //above ground
                                {
                                    Irated = xlpe2core[i, 5] * n;
                                }

                                iderated = Irated * ktmain;

                                cable_lte();

                                // Validasi
                                validasi();

                                i++;
                            }
                        }
                        else if (cores == 3)
                        {
                            while ((!complete) && (i < 17))
                            {
                                wirearea = xlpe3core[i, 0];
                                if (phase == "DC")
                                {
                                    Rdc = xlpe3core[i, 3];
                                    vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                }
                                else //AC
                                {
                                    Rac = xlpe3core[i, 1];
                                    X = xlpe3core[i, 2];

                                    if (phase == "Single-Phase AC")
                                    {
                                        vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                        / (n * 1000 * voltage);

                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                            (X * Math.Sqrt(1 - pf * pf))) * 100);

                                        if (loadtype == "Dynamic")
                                        {
                                            vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                            / (n * 1000 * voltage);
                                        }
                                    }
                                    else if (phase == "Three-Phase AC")
                                    {
                                        vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                        / (n * 1000 * voltage);

                                        lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                            (X * Math.Sqrt(1 - pf * pf))) * 100);

                                        if (loadtype == "Dynamic")
                                        {
                                            vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                            / (n * 1000 * voltage);
                                        }
                                    }
                                }


                                if (installation == "Under Ground")
                                {
                                    Irated = xlpe3core[i, 4] * n;
                                }
                                else //above ground
                                {
                                    Irated = xlpe3core[i, 5] * n;
                                }

                                iderated = Irated * ktmain;
                                cable_lte();

                                // Validasi
                                validasi();

                                i++;
                            }
                        }
                        else if (cores == 4)
                        {
                            while ((!complete) && (i < 17))
                            {
                                wirearea = xlpe4core[i, 0];
                                if (phase == "DC")
                                {
                                    Rdc = xlpe4core[i, 3];
                                    vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                }
                                else //AC
                                {
                                    Rac = xlpe4core[i, 1];
                                    X = xlpe4core[i, 2];

                                    if (phase == "Single-Phase AC")
                                    {
                                        vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                        / (n * 1000 * voltage);

                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                            (X * Math.Sqrt(1 - pf * pf))) * 100);

                                        if (loadtype == "Dynamic")
                                        {
                                            vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                            / (n * 1000 * voltage);
                                        }
                                    }
                                    else if (phase == "Three-Phase AC")
                                    {
                                        vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                        / (n * 1000 * voltage);

                                        lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                            (X * Math.Sqrt(1 - pf * pf))) * 100);

                                        if (loadtype == "Dynamic")
                                        {
                                            vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                            / (n * 1000 * voltage);
                                        }
                                    }
                                }

                                if (installation == "Under Ground")
                                {
                                    Irated = xlpe4core[i, 4] * n;
                                }
                                else //above ground
                                {
                                    Irated = xlpe4core[i, 5] * n;
                                }

                                iderated = Irated * ktmain;
                                cable_lte();

                                // Validasi
                                validasi();

                                i++;
                            }
                        }
                    }
                    else if (insulation == "PVC")
                    {
                        if (cores == 2)
                        {
                            while ((!complete) && (i < 16))
                            {
                                wirearea = pvc2core[i, 0];
                                if (phase == "DC")
                                {
                                    Rdc = pvc2core[i, 3];
                                    vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                }
                                else //AC
                                {
                                    Rac = pvc2core[i, 1];
                                    X = pvc2core[i, 2];

                                    if (phase == "Single-Phase AC")
                                    {
                                        vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                        / (n * 1000 * voltage);

                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                            (X * Math.Sqrt(1 - pf * pf))) * 100);

                                        if (loadtype == "Dynamic")
                                        {
                                            vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                            / (n * 1000 * voltage);
                                        }
                                    }
                                    else if (phase == "Three-Phase AC")
                                    {
                                        vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                        / (n * 1000 * voltage);

                                        lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                            (X * Math.Sqrt(1 - pf * pf))) * 100);

                                        if (loadtype == "Dynamic")
                                        {
                                            vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                            / (n * 1000 * voltage);
                                        }
                                    }
                                }

                                if (installation == "Under Ground")
                                {
                                    Irated = pvc2core[i, 4] * n;
                                }
                                else //above ground
                                {
                                    Irated = pvc2core[i, 5] * n;
                                }

                                iderated = Irated * ktmain;
                                cable_lte();

                                // Validasi
                                validasi();

                                i++;
                            }
                        }
                        else if (cores == 3)
                        {
                            while ((!complete) && (i < 16))
                            {
                                wirearea = pvc3core[i, 0];
                                if (phase == "DC")
                                {
                                    Rdc = pvc3core[i, 3];
                                    vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                }
                                else //AC
                                {
                                    Rac = pvc3core[i, 1];
                                    X = pvc3core[i, 2];

                                    if (phase == "Single-Phase AC")
                                    {
                                        vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                        / (n * 1000 * voltage);

                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                            (X * Math.Sqrt(1 - pf * pf))) * 100);

                                        if (loadtype == "Dynamic")
                                        {
                                            vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                            / (n * 1000 * voltage);
                                        }
                                    }
                                    else if (phase == "Three-Phase AC")
                                    {
                                        vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                        / (n * 1000 * voltage);

                                        lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                            (X * Math.Sqrt(1 - pf * pf))) * 100);

                                        if (loadtype == "Dynamic")
                                        {
                                            vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                            / (n * 1000 * voltage);
                                        }
                                    }
                                }

                                if (installation == "Under Ground")
                                {
                                    Irated = pvc3core[i, 4] * n;
                                }
                                else //above ground
                                {
                                    Irated = pvc3core[i, 5] * n;
                                }

                                iderated = Irated * ktmain;
                                cable_lte();

                                // Validasi
                                validasi();

                                i++;
                            }
                        }
                        else if (cores == 4)
                        {
                            while ((!complete) && (i < 16))
                            {
                                wirearea = pvc4core[i, 0];
                                if (phase == "DC")
                                {
                                    Rdc = pvc4core[i, 3];
                                    vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                }
                                else //AC
                                {
                                    Rac = pvc4core[i, 1];
                                    X = pvc4core[i, 2];

                                    if (phase == "Single-Phase AC")
                                    {
                                        vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                        / (n * 1000 * voltage);

                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                            (X * Math.Sqrt(1 - pf * pf))) * 100);

                                        if (loadtype == "Dynamic")
                                        {
                                            vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                            / (n * 1000 * voltage);
                                        }
                                    }
                                    else if (phase == "Three-Phase AC")
                                    {
                                        vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                        / (n * 1000 * voltage);

                                        lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                            (X * Math.Sqrt(1 - pf * pf))) * 100);


                                        if (loadtype == "Dynamic")
                                        {
                                            vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                            / (n * 1000 * voltage);
                                        }
                                    }
                                }

                                if (installation == "Under Ground")
                                {
                                    Irated = pvc4core[i, 4] * n;
                                }
                                else //above ground
                                {
                                    Irated = pvc4core[i, 5] * n;
                                }

                                iderated = Irated * ktmain;
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
                        wirearea = inputCableData[i, 0];

                        if (phase == "DC")
                        {
                            Rdc = inputCableData[i, 1];

                            vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                            lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                        }
                        else
                        {
                            Rac = inputCableData[i, 1];
                            X = inputCableData[i, 2];

                            if (phase == "Single-Phase AC")
                            {
                                vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                / (n * 1000 * voltage);

                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                    (X * Math.Sqrt(1 - pf * pf))) * 100);

                                if (loadtype == "Dynamic")
                                {
                                    vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                    / (n * 1000 * voltage);
                                }
                            }
                            else if (phase == "Three-Phase AC")
                            {
                                vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                / (n * 1000 * voltage);

                                lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                    (X * Math.Sqrt(1 - pf * pf))) * 100);

                                if (loadtype == "Dynamic")
                                {
                                    vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                    / (n * 1000 * voltage);
                                }
                            }
                        }

                        if (installation == "Under Ground")
                        {
                            Irated = inputCableData[i, 3];
                        }
                        else if (installation == "Above Ground")
                        {
                            Irated = inputCableData[i, 4];
                        }

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
                textBox12.Text = n.ToString();
                textBox8.Text = vdrun.ToString("0.##");
                textBox29.Text = lmax.ToString("0.##");

                if (material == "Copper")
                {
                    materialname = "Cu";
                }

                if (loadtype == "Dynamic")
                {
                    textBox10.Text = vdstart.ToString("0.##");
                }

                readtemp = "";
                    
                readtemp += n.ToString() + "  ×  " + cores.ToString("0.##") + "/C  #  " + wirearea.ToString() + 
                    " mm²    0,6/1kV / " + materialname + " / " + insulation;

                if (armour != "Non Armoured")
                {
                    readtemp += " / " + armour;
                }

                readtemp += " / " + outersheath;

                tbResult.Text = readtemp;
                cable_lte();
                save_result();
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
            }
            else
            {

            }
        }


        private void solvableOrNPlus()
        {
            if (breakcurrent < current)
            {
                MessageBox.Show("Breaker current value must be greater than the full load current value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                complete = true;
                inputValid = false;
            }
            else if ((radioButton2.Enabled) && (cablesizemin >= iderated) && (insulation == "PVC"))
            {
                MessageBox.Show("Minimum cable size due to short circuit current must be <300 with PVC insulation!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                complete = true;
                inputValid = false;
            }
            else if ((radioButton2.Enabled) && (cablesizemin >= iderated) && (insulation == "XLPE"))
            {
                MessageBox.Show("Minimum cable size due to short circuit current must be <400 with XLPE insulation!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                complete = true;
                inputValid = false;
            }
            else
            {
                n++;
            }
        }

        private void validasi()
        {
            if ((current < breakcurrent) && (breakcurrent < iderated) && (vdrun < vdrunmax) &&
                (((radioButton1.Checked) && (cLTE > bLTE)) ||((radioButton2.Checked) && (wirearea > smin))))
            {
                if (loadtype == "Dynamic")
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
            }
            breaker_fill();
            calc_current();
            enable_result_btn();
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                pf = double.Parse(textBox4.Text);
            }
            calc_current();
            enable_result_btn();
        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text != "")
            {
                eff = double.Parse(textBox5.Text);
            }
            calc_current();
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
            if (textBox14.Text != "")
            {
                pfstart = double.Parse(textBox14.Text);
            }
            enable_result_btn();

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
            if (radioButton2.Checked)
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
                label48.Enabled = false;
                label49.Enabled = false;
                label69.Enabled = false;
                label70.Enabled = false;
                textBox20.Enabled = false;
                textBox22.Enabled = false;
            }
            else //(!radioButton2.Checked)
            {
                comboBox11.DropDownStyle = ComboBoxStyle.DropDownList;
                label53.Enabled = false;
                label54.Enabled = false;
                textBox23.Enabled = false;
                textBox28.Enabled = false;
                label63.Enabled = false;
                label64.Enabled = false;
                label67.Enabled = false;
                label68.Enabled = false;
                textBox30.Enabled = false;
                label48.Enabled = true;
                label49.Enabled = true;
                label69.Enabled = true;
                label70.Enabled = true;
                textBox20.Enabled = true;
                textBox22.Enabled = true;
            }
            break_lte();
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                comboBox11.DropDownStyle = ComboBoxStyle.DropDownList;
                label53.Enabled = false;
                label54.Enabled = false;
                textBox23.Enabled = false;
                textBox28.Enabled = false;
                label63.Enabled = false;
                label64.Enabled = false;
                label67.Enabled = false;
                label68.Enabled = false;
                textBox30.Enabled = false;
                label48.Enabled = true;
                label49.Enabled = true;
                label69.Enabled = true;
                label70.Enabled = true;
                textBox20.Enabled = true;
                textBox22.Enabled = true;
            }
            else //(!radioButton1.Checked)
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
                label48.Enabled = false;
                label49.Enabled = false;
                label69.Enabled = false;
                label70.Enabled = false;
                textBox20.Enabled = false;
                textBox22.Enabled = false;
            }
            break_lte();
        }

        private void ComboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            breakcurrent = double.Parse(comboBox11.Text);
            enable_result_btn();
        }

        private void ComboBox11_TextChanged(object sender, EventArgs e)
        {
            if (comboBox11.Text != "")
            {
                breakcurrent = double.Parse(comboBox11.Text);
            }
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

        private void TextBox8_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void ComboBox10_TextChanged(object sender, EventArgs e)
        {
            breaker_fill();
        }

        private void ComboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            breaker_fill();
            enable_result_btn();
        }

        private void TextBox16_TextChanged(object sender, EventArgs e)
        {
            breaker_fill();
        }

        private void TextBox23_TextChanged(object sender, EventArgs e)
        {
            if(textBox23.Text != "")
            {
                tbreaker = double.Parse(textBox23.Text);
            }
            calc_smin();
            enable_result_btn();
            break_lte();
        }

        private void TextBox9_TextChanged(object sender, EventArgs e)
        {
            if (textBox9.Text != "")
            {
                vdrunmax = double.Parse(textBox9.Text);
            }
            enable_result_btn();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            innersheath = comboBox1.Text;
        }

        private void ComboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            outersheath = comboBox8.Text;
            enable_result_btn();
        }

        private void buttonReset(object sender, EventArgs e)
        {
            textBox13.Text = "";
            textBox26.Text = "";
            textBox27.Text = "";
            TextBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox25.Text = "";
            textBox14.Text = "";
            textBox9.Text = "";
            textBox11.Text = "";
            textBox12.Text = "1"; //no of run default = 1
            textBox6.Text = "";
            textBox15.Text = "";
            textBox24.Text = "";
            textBox17.Text = "";
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

            cbPower.Text = "kW";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            comboBox6.SelectedIndex = -1;
            comboBox7.SelectedIndex = -1;
            comboBox8.SelectedIndex = -1;
            comboBox9.SelectedIndex = -1;
            comboBox10.SelectedIndex = -1;
            comboBox11.SelectedIndex = -1;
            comboBox12.SelectedIndex = -1;
            comboBox11.Text = "";
        }

        private void TextBox25_TextChanged(object sender, EventArgs e)
        {
            if(textBox25.Text != "")
            {
                multiplier = double.Parse(textBox25.Text);
            }
            calc_current();
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
            calc_smin();
            enable_result_btn();
            break_lte();
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
            enable_result_btn();
        }

        private void TextBox18_TextChanged(object sender, EventArgs e)
        {
            enable_result_btn();
        }

        private void TextBox19_TextChanged(object sender, EventArgs e)
        {
            enable_result_btn();
        }

        private void TextBox26_TextChanged(object sender, EventArgs e)
        {
            from = textBox26.Text;
        }

        private void ComboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            breakertype = comboBox12.Text;
            enable_result_btn();
            breaker_fill();
            break_lte();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            f5.dataGridView1.RowCount++;
            j++;
            f5.dataGridView1.Rows[j].Cells[0].Value = j + 1;
            if (!f5.Visible)
            {
                for (int k = 0; k <33; k ++)
                {
                    f5.dataGridView1.Rows[j].Cells[k + 1].Value = results[k];
                    f5.Show();
                }
            }
            else
            {
                for (int k = 0; k < 33; k++)
                {
                    f5.dataGridView1.Rows[j].Cells[k + 1].Value = results[k];
                }
            }
        }

        private void TextBox16_TextChanged_1(object sender, EventArgs e)
        {
            fromdesc = textBox16.Text;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            f5.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
            if (Form5.cancelexit)
            {
                e.Cancel = true;
                Form5.cancelexit = false;
            }
        }

        private void TextBox31_TextChanged(object sender, EventArgs e)
        {
            todesc = textBox31.Text;
        }

        private void TextBox30_TextChanged(object sender, EventArgs e)
        {
            if (textBox30.Text != "")
            {
                cablesizemin = double.Parse(textBox30.Text);
            }
        }

        private void Button5_Click_1(object sender, EventArgs e)
        {
            f5.Show();
        }

        private void RadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                button6.Enabled = false;
                label78.Enabled = false;
            }
            else
            {
                button6.Enabled = true;
                label78.Enabled = true;
            }
            enable_result_btn();
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                button6.Enabled = true;
                label78.Enabled = true;
            }
            else
            {
                button6.Enabled = false;
                label78.Enabled = false;
            }
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
                inputCableData = new double[cableCount, 5];
                for (int i = 0; i < cableCount; i++)
                {
                    for (int q = 0; q < 5; q++)
                    {
                        inputCableData[i, q] = Form6.confirmedcabledata[i, q];
                    }
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

        }

        private void TextBox35_TextChanged(object sender, EventArgs e)
        {
            remarks = textBox35.Text; 
        }

        private void TextBox27_TextChanged(object sender, EventArgs e)
        {
            to = textBox27.Text;
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
            }
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
            if (textBox11.Text != "")
            {
                vdstartmax = double.Parse(textBox11.Text);
            }
            enable_result_btn();
        }

        private void calc_current()
        {
            if ((TextBox1.Text != "") && (textBox2.Text != "") && (comboBox3.Text != "")
            && (comboBox2.Text != "") && (textBox4.Text != "") && (textBox5.Text != ""))
            {

                if ((phase == "DC") && (loadtype == "Static"))
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
                    textBox3.Text = current.ToString("0.##");

                    if ((loadtype == "Dynamic") && (textBox25.Text != ""))
                    {
                        currentstart = current * multiplier; //6.5 mungkin (harus) diganti
                        textBox7.Text = currentstart.ToString("0.##");
                    }
                    else
                    {
                        textBox7.Text = "";
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

                    if ((loadtype == "Dynamic") && (textBox25.Text != ""))
                    {
                        currentstart = current * multiplier;
                        textBox7.Text = currentstart.ToString("0.##");
                    }
                    else
                    {
                        textBox7.Text = "";
                    }
                }
                else
                {
                    textBox3.Text = "";
                    textBox7.Text = "";
                }
            }
            else
            {
                textBox3.Text = "";
            }
            
        }

        private void enable_correction()
        {
            if (((comboBox9.Text == "Above Ground") || (comboBox9.Text == "Under Ground")) && 
                ((comboBox6.Text == "PVC") || (comboBox6.Text == "XLPE")))
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
            k1main = 0;
            k2main = 0;
            ktmain = 0;

            textBox17.Text = "";
            textBox18.Text = "";
            textBox19.Text = "";
        }

        private void enable_result_btn()
        {
            if((comboBox2.Text != "") && (comboBox3.Text != ""))
            {
                if ((TextBox1.Text != "") && (textBox2.Text != "") && (textBox3.Text != "") && (textBox4.Text != "") &&
                (textBox5.Text != "") && (textBox9.Text != "") && (textBox6.Text != "") && (textBox12.Text != "") && 
                (comboBox5.Text != "") && (comboBox6.Text != "") && (comboBox9.Text != "") && (textBox17.Text != "") && 
                (textBox18.Text != "") && (textBox19.Text != "") && (comboBox11.Text != "") && (comboBox4.Text != "") && 
                (comboBox7.Text != "") && (comboBox8.Text != "") && (comboBox10.Text != "") && (comboBox12.Text != ""))
                {
                    if (((radioButton3.Checked) && (cableCount > 0)) || (radioButton4.Checked))
                    {
                        if (radioButton2.Checked)
                        {
                            if ((textBox23.Text != "") && (textBox28.Text != ""))
                            {
                                if (loadtype == "Dynamic")
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
                        else if (loadtype == "Dynamic")
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
            if ((radioButton1.Checked) && (comboBox11.Text != ""))
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
            else
            {
                textBox20.Text = "";
            }
        }

        private void cable_lte()
        {
            if (radioButton1.Checked)
            {
                cLTE = wirearea * wirearea * k * k;
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
                textBox30.Text = smin.ToString("0.##");
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
            results[18] = Rac.ToString("0.####");
            results[19] = X.ToString("0.####");
            results[20] = Irated.ToString("0.##");
            results[21] = ktmain.ToString("0.##");
            results[22] = iderated.ToString("0.##");
            results[23] = breakertype;
            results[24] = breakcurrent.ToString("0.##");
            results[25] = length.ToString("0.##");
            results[26] = lmax.ToString("0.##");
            results[27] = vdrun.ToString("0.##");
            results[28] = vdrunmax.ToString("0.##");
            results[29] = vdstart.ToString("0.##");
            results[30] = vdstartmax.ToString("0.##");
            results[31] = readtemp;
            results[32] = remarks;

            for(int i = 0; i <33; i++)
            {
                if (results[i] == "0")
                {
                    results[i] = "";
                }
            }
        }

        private void enable_save()
        {
            button4.Enabled = true;
        }

        private void textSeparatorChange()
        {
            
        }
    }
}
