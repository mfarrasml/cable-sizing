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
        int currenttheme;

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
            DecLoad();
            currenttheme = Properties.Settings.Default.ColorTheme;
            comboBoxTheme.SelectedIndex = currenttheme;
        }

        private void DecLoad()
        {
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

            //save color Theme settings
            Properties.Settings.Default.ColorTheme = currenttheme;

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

        private void Button3_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Resete all settings to default?", "Cable Sizing", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dr == DialogResult.OK)
            {
                //reset decimal separator
                tempDecimalSeparator = Properties.Settings.Default.DecimalSeparatorDefault;
                DecLoad();

                comboBoxTheme.SelectedIndex = 0;

                MessageBox.Show("Settings reset to default", "Cable Sizing", MessageBoxButtons.OK, MessageBoxIcon.Information);

                /* //resest toolbardesc
                Form5.toolbarDescShown = Properties.Settings.Default.ToolbarDescriptionPropertiesDefault;
                Properties.Settings.Default.ToolbarDescriptionProperties = Form5.toolbarDescShown;


                //resest toolbar activated
                Form5.toolbarTextActivated = Properties.Settings.Default.ToolbarTextPropertiesDefault;
                Properties.Settings.Default.ToolbarTextProperties = Properties.Settings.Default.ToolbarTextPropertiesDefault;
                */
            }
        }

        private void LoadColor()
        {
            if (currenttheme == 0) //default theme
            {
                gradientPanelSecondary.Visible = true;
                gradientPanelSecondary.BackColor = SystemColors.Control;

                gradientPanelPrimary.BackColor = Color.White;
                gradientPanelPrimary.TopColor = Color.Transparent;
                gradientPanelPrimary.BottomColor = Color.Transparent;
                gradientPanelPrimary.Angle = 0;

            }
            else if (currenttheme == 1) //Skyblue theme
            {
                gradientPanelPrimary.BackColor = Color.White;

                gradientPanelSecondary.Visible = false;
                gradientPanelSecondary.BackColor = Color.Transparent;

                gradientPanelPrimary.BackColor = Color.Transparent;
                gradientPanelPrimary.TopColor = Color.Azure;
                gradientPanelPrimary.BottomColor = Color.LightCyan;
                gradientPanelPrimary.Angle = 300;
            }
            else if (currenttheme == 2) //Dark theme
            {
                gradientPanelSecondary.Visible = true;
                gradientPanelSecondary.BackColor = Color.FromArgb(45, 46, 51);

                gradientPanelPrimary.BackColor = Color.FromArgb(58, 59, 66);
                gradientPanelPrimary.TopColor = Color.Transparent;
                gradientPanelPrimary.BottomColor = Color.Transparent;
                gradientPanelPrimary.Angle = 0;
            }
            else if (currenttheme == 3) //Pinky salmon theme
            {
                gradientPanelPrimary.BackColor = Color.White;

                gradientPanelSecondary.Visible = false;
                gradientPanelSecondary.BackColor = Color.Transparent;
                
                gradientPanelPrimary.BackColor = Color.Transparent;
                gradientPanelPrimary.TopColor = Color.Salmon;
                gradientPanelPrimary.BottomColor = Color.HotPink;
                gradientPanelPrimary.Angle = 0;

            }
            else if (currenttheme == 4) //Cable sizing theme
            {
                gradientPanelPrimary.BackColor = Color.White;

                gradientPanelSecondary.Visible = false;
                gradientPanelSecondary.BackColor = Color.Transparent;

                gradientPanelPrimary.BackColor = Color.Transparent;
                gradientPanelPrimary.TopColor = Color.Turquoise;
                gradientPanelPrimary.BottomColor = Color.DodgerBlue;
                gradientPanelPrimary.Angle = 60;
            }
            else if (currenttheme == 5) //Visual Studio theme
            {
                gradientPanelSecondary.Visible = true;
                gradientPanelSecondary.BackColor = Color.FromArgb(93, 107, 153);

                gradientPanelPrimary.BackColor = Color.AliceBlue;
                gradientPanelPrimary.TopColor = Color.Transparent;
                gradientPanelPrimary.BottomColor = Color.Transparent;
                gradientPanelPrimary.Angle = 0;

            }
        }

        private void ComboBoxTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            currenttheme = comboBoxTheme.SelectedIndex;
            LoadColor();
        }
    }
}
