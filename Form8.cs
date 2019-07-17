using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test1
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        string tagno;
        string from;
        string fromdesc;
        string to;
        string todesc;
        string phase;
        string loadtype;
        string voltagesystem;
        string ratedvoltage;
        string material;
        string insulation;
        string armour;
        string outersheath;
        string installation;
        string breakertype;

        double power;
        double powerkva;
        double powerhp;
        double voltage;
        double eff;
        double pf;
        double pfstart;
        double current;
        double currentstart;
        double multiplier;
        double length;
        double lengthmax;
        double vdrunmax;
        double vdstartmax;
        double vdrun;
        double vdstart;
        double n;
        double cores;
        double Rac, Rdc, X, Irated, Iderated;
        double wirearea;
        double initialTemp;
        double finalTemp;
        double diameter;
        double breakcurrent;

        int kttextboxX, kttextboxY;
        int ktlabelX, ktlabelY;

        bool calculated = false;

        Form5 f5 = new Form5();
        Form6 f6 = new Form6();

        private void TextBox13_TextChanged(object sender, EventArgs e)
        {
            if (textBox13.Text != "")
            {
                tagno = textBox13.Text;
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
            else
            {
                tagno = "";
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

            enable_vd_btn();
            enable_result_btn();
        }

        private void TextBox26_TextChanged(object sender, EventArgs e)
        {
            if (textBox26.Text != "")
            {
                from = textBox26.Text;
            }
            else
            {
                from = "";
            }
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


            if (comboBox2.Text == "Motor")
            {
                if (textBox14.Text == "")
                {
                    panel10.BackColor = Color.Red;
                }
                if (textBox25.Text == "")
                {
                    panel12.BackColor = Color.Red;
                }
                if (textBox11.Text == "")
                {
                    panel27.BackColor = Color.Red;
                }
            }
            else
            {
                panel10.BackColor = Color.Transparent;
                panel12.BackColor = Color.Transparent;
                panel27.BackColor = Color.Transparent;
            }



            if (comboBox2.Text == "Motor")
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
            enable_vd_btn();
            enable_result_btn();
        }

        private void TextBox16_TextChanged(object sender, EventArgs e)
        {
            if (textBox16.Text != "")
            {
                fromdesc = textBox16.Text;
            }
            else
            {
                fromdesc = "";
            }
        }

        private void TextBox27_TextChanged(object sender, EventArgs e)
        {
            if (textBox27.Text != "")
            {
                to = textBox27.Text;
            }
            else
            {
                to = "";
            }
        }

        private void TextBox31_TextChanged(object sender, EventArgs e)
        {
            if (textBox31.Text != "")
            {
                todesc = textBox31.Text;
            }
            else
            {
                todesc = "";
            }
        }

        private void ComboBox13_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox13.Text != "")
            {
                voltagesystem = comboBox13.Text;
                panel15.BackColor = Color.Transparent;
            }
            else
            {
                panel15.BackColor = Color.Red;
            }
            voltagesystem = comboBox13.Text;
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
            if (voltagesystem == "MV")
            {
                MessageBox.Show("Vendor data for Medium Voltage (MV) cable is not available.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBox13.SelectedIndex = 0;
            }
            enable_vd_btn();
            enable_result_btn();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

            if (TextBox1.Text != "")
            {
                if (cbPower.Text == "HP")
                {
                    powerhp = double.Parse(TextBox1.Text);
                    power = 0.746 * powerhp;
                }
                else if (cbPower.Text == "kVA")
                {
                    powerkva = double.Parse(TextBox1.Text);
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

            calc_current();
            enable_vd_btn();
            enable_result_btn();
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
            calc_current();
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
            calc_current();
            enable_vd_btn();
            enable_result_btn();
        }

        private void TextBox14_TextChanged(object sender, EventArgs e)
        {
            if (textBox14.Text != "")
            {
                pfstart = double.Parse(textBox14.Text);
                panel10.BackColor = Color.Transparent;
            }
            else if ((textBox14.Text == "") && (comboBox2.Text == "Motor"))
            {
                pfstart = 0;
                panel10.BackColor = Color.Red;
            }
            else
            {
                pfstart = 0;
                panel10.BackColor = Color.Transparent;
            }
            enable_vd_btn();
            enable_result_btn();
        }

        private void RadioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton8.Checked)
            {
                TextBox1.Text = "";
                power = 0;

                TextBox1.ReadOnly = true;
                cbPower.Enabled = false;

                textBox3.ReadOnly = false;
                panel14.BackColor = Color.Transparent;
                if (textBox3.Text == "")
                {
                    panel11.BackColor = Color.Red;
                }

            }
            else
            {
                textBox3.Text = "";
                current = 0;
                TextBox1.ReadOnly = false;
                cbPower.Enabled = true;

                textBox3.ReadOnly = true;
                panel11.BackColor = Color.Transparent;
                if (textBox2.Text == "")
                {
                    panel14.BackColor = Color.Red;
                }
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
                enable_vd_btn();
                enable_result_btn();
            }
        }

        private void TextBox25_TextChanged(object sender, EventArgs e)
        {
            if (textBox25.Text != "")
            {
                multiplier = double.Parse(textBox25.Text);
                panel12.BackColor = Color.Transparent;
            }
            else if ((textBox25.Text == "") && (comboBox2.Text == "Motor"))
            {
                multiplier = 0;
                panel12.BackColor = Color.Red;
            }
            else
            {
                panel12.BackColor = Color.Transparent;
            }
            calc_current();
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
            enable_vd_btn();
            enable_result_btn();
        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
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
            }
            else if ((insulation == "XLPE") || (insulation == "EPR"))
            {
                textBox15.Text = "90";
                textBox24.Text = "250";
                panel20.BackColor = Color.Transparent;
            }
            else
            {
                textBox15.Text = "";
                textBox24.Text = "";
                textBox21.Text = "";
                panel20.BackColor = Color.Red;
            }

            reset_correction();
            enable_correction();
            enable_vd_btn();
            enable_result_btn();
        }
        
        private void Calc_k()
        {

        }

        private void reset_correction()
        {

        }

        private void enable_correction()
        {

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
            enable_vd_btn();
            enable_result_btn();
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
            enable_vd_btn();
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
            enable_vd_btn();
            enable_result_btn();
        }

        private void TextBox11_TextChanged(object sender, EventArgs e)
        {
            if (textBox11.Text != "")
            {
                vdstartmax = double.Parse(textBox11.Text);
                panel27.BackColor = Color.Transparent;
            }
            else if ((textBox11.Text == "") && (comboBox2.Text == "Motor"))
            {
                vdstartmax = 0;
                panel27.BackColor = Color.Red;
            }
            else
            {
                vdstartmax = 0;
                panel27.BackColor = Color.Transparent;
            }
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

            enable_vd_btn();
            enable_result_btn();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            calculated = false;
            if ((voltage == 0) || (eff == 0) || (length == 0) || ((power == 0) && (!radioButton8.Checked)) || (current == 0) ||
                ((comboBox2.Text == "Motor") && (multiplier == 0)) || (pf > 1) || (pfstart > 1) ||
                (vdrunmax > 100) || (vdrunmax <= 0) || ((comboBox2.Text == "Motor") && ((vdstartmax > 100) || (vdstartmax <= 0))))
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
                if ((comboBox2.Text == "Motor") && (multiplier == 0))
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
                if ((comboBox2.Text == "Motor") && ((vdstartmax > 100) || (vdstartmax <= 0)))
                {
                    msgbox += "\n- Vd Start Max";
                }
                MessageBox.Show(msgbox, "Input Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                vd_size_calc();
            }
        }

        private void ComboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            installation = comboBox9.Text;
            if (installation == "E (Above Ground)")
            {
                textBox36.Enabled = false;
                textBox36.Text = "";
                //k3main = 0;
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

            reset_correction();
            enable_correction();
            enable_vd_btn();
            enable_result_btn();
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


            comboBox5.Enabled = true;
            textBox12.ReadOnly = false;
            comboBox15.SelectedIndex = -1;
            comboBox15.Items.Clear();
            comboBox11.SelectedIndex = -1;
            comboBox12.SelectedIndex = -1;

            panel4.Enabled = false;
            button8.Enabled = false;
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

        private void TextBox13_Leave(object sender, EventArgs e)
        {
            tagno = textBox13.Text;
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TextBox4_Leave(object sender, EventArgs e)
        {
            if (pf > 1)
            {
                MessageBox.Show("Power Factor can't be greater than 1!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TextBox14_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TextBox14_Leave(object sender, EventArgs e)
        {
            if (pfstart > 1)
            {
                MessageBox.Show("Starting Power Factor can't be greater than 1!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void TextBox8_TextChanged(object sender, EventArgs e)
        {
            if (textBox8.Text != "")
            {
                vdrun = double.Parse(textBox8.Text);
            }
        }

        private void TextBox10_TextChanged(object sender, EventArgs e)
        {
            if (textBox10.Text != "")
            {
                vdstart = double.Parse(textBox10.Text);
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

        private void TextBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            calculated = false;
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
            textBox12.ReadOnly = false;
            textBox6.Text = "";
            textBox15.Text = "";
            textBox24.Text = "";
            textBox17.Text = "";
            textBox36.Text = "";
            textBox18.Text = "";
            textBox19.Text = "";
            //textBox23.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            //textBox20.Text = "";
            textBox21.Text = "";
            //textBox22.Text = "";
            tbResult.Text = "";
            textBox10.Text = "";
            //textBox28.Text = "";
            textBox29.Text = "";
            //textBox30.Text = "";
            textBox16.Text = "";
            textBox31.Text = "";
            textBox32.Text = "";
            textBox33.Text = "";
            textBox34.Text = "";
            textBox35.Text = "";
            textBox37.Text = "";



            cbPower.Text = "kW";
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            comboBox5.Enabled = true;
            comboBox6.SelectedIndex = -1;
            comboBox7.SelectedIndex = -1;
            comboBox8.SelectedIndex = -1;
            comboBox9.SelectedIndex = -1;
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
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            OpenDataTable();
        }

        private void OpenDataTable()
        {
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

        private void Button4_Click(object sender, EventArgs e)
        {
            DataRow dtr = Form1.dtdiameter.NewRow();
            f5.dataGridView1.RowCount++;
            Form1.j++;
            f5.dataGridView1.Rows[Form1.j].Cells[0].Value = Form1.j + 1;

            for (int k = 0; k < 38; k++)
            {
                f5.dataGridView1.Rows[Form1.j].Cells[k + 1].Value = Form1.results[k];
            }

            OpenDataTable();

            //cable OD
            dtr[0] = diameter;

            Form1.dtdiameter.Rows.Add(dtr);

            f5.Update_summary();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label86.Visible = false;
            timer1.Enabled = false;
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            label87.Visible = false;
            timer2.Enabled = false;
        }

        private void ComboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            breakertype = comboBox12.Text;
            SCLTEchanged();
            enable_result_btn();
        }

        private void RadioButton7_CheckedChanged(object sender, EventArgs e)
        {
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
                kttextboxX = textBox19.Location.X;
                kttextboxY = textBox19.Location.Y;
                ktlabelX = label43.Location.X;
                ktlabelY = label43.Location.Y;

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

        private void ComboBox10_TextChanged(object sender, EventArgs e)
        {
            SCLTEchanged();
            breaker_fill();
            enable_result_btn();
        }

        private void ComboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            SCLTEchanged();
            breaker_fill();
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
            enable_result_btn();
        }

        private void RadioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                comboBox11.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            else if (radioButton6.Checked)
            {
                comboBox11.DropDownStyle = ComboBoxStyle.DropDown;
            }
        }

        private void RadioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                comboBox11.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            else if (radioButton6.Checked)
            {
                comboBox11.DropDownStyle = ComboBoxStyle.DropDown;
            }
        }

        private void enable_result_btn()
        {

        }

        private void enable_vd_btn()
        {

        }

        private void calc_current()
        {

        }

        private void breaker_fill()
        {

        }

        private void vd_size_calc()
        {

        }

        private void SCLTEchanged()
        {

        }

    }
}
