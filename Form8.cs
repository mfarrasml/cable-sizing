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

        string[] cabledata_nec = new string[21]
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

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                label2.Text = "Table 310.16 Allowable Ampacities of Insulated Conductors Rated 0 Through 2000 Volts, 60°C Through 90°C (140°F Through 194°F)," +
                    "\n                    in Raceway, Cable, or Earth (Directly Buried), Based on AmbientT emperature of 30°C (86°F)";
                dataGridView2.Visible = true;
                dataGridView1.Visible = false;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                label2.Text = "Table 310.17 Allowable Ampacities of Single-Insulated Conductors Rated 0 Through 2000 Volts in Free Air" +
                    "\n                    Based on Ambient Air Temperature of 30°C (86°F)";
                dataGridView2.Visible = true;
                dataGridView1.Visible = false;
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                label2.Text = "Table 9 Alternating-Current Resistance and Reactance for 600-Volt Cables, 3-Phase, 60 Hz, 75°C (167°F)";
                dataGridView2.Visible = false;
                dataGridView1.Visible = true;
            }
            else
            {
                label2.Text = "";
                dataGridView2.Visible = false;
                dataGridView1.Visible = false;
            }

            fill_datagridview();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            label2.Text = "";

            dataGridView2.RowCount = 21;
            dataGridView1.RowCount = 21;

            for (int i = 0; i < 21; i++)
            {
                dataGridView2.Rows[i].Cells[0].Value = cabledata_nec[i];
                dataGridView1.Rows[i].Cells[0].Value = cabledata_nec[i];
            }

            dataGridView2.Visible = false;
            dataGridView1.Visible = false;
        }

        private void fill_datagridview()
        {
            if (comboBox1.SelectedIndex == 0)
            {
                if (comboBox2.SelectedIndex == 0)
                {
                    for (int i = 0; i < 21; i++)
                    {
                        dataGridView2.Rows[i].Cells[1].Value = data_ampacity_racewaycableearth[i, 0];
                        dataGridView2.Rows[i].Cells[2].Value = data_ampacity_racewaycableearth[i, 1];
                        dataGridView2.Rows[i].Cells[3].Value = data_ampacity_racewaycableearth[i, 2];
                    }
                }
                else if (comboBox2.SelectedIndex == 1)
                {
                    for (int i = 0; i < 21; i++)
                    {
                        dataGridView2.Rows[i].Cells[1].Value = data_ampacity_racewaycableearth[i, 3];
                        dataGridView2.Rows[i].Cells[2].Value = data_ampacity_racewaycableearth[i, 4];
                        dataGridView2.Rows[i].Cells[3].Value = data_ampacity_racewaycableearth[i, 5];
                    }
                }
                else
                {
                    for (int i = 0; i < 21; i++)
                    {
                        dataGridView2.Rows[i].Cells[1].Value = null;
                        dataGridView2.Rows[i].Cells[2].Value = null;
                        dataGridView2.Rows[i].Cells[3].Value = null;
                    }
                }
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                if (comboBox2.SelectedIndex == 0)
                {
                    for (int i = 0; i < 21; i++)
                    {
                        dataGridView2.Rows[i].Cells[1].Value = data_ampacity_freeair[i, 0];
                        dataGridView2.Rows[i].Cells[2].Value = data_ampacity_freeair[i, 1];
                        dataGridView2.Rows[i].Cells[3].Value = data_ampacity_freeair[i, 2];
                    }
                }
                else if (comboBox2.SelectedIndex == 1)
                {
                    for (int i = 0; i < 21; i++)
                    {
                        dataGridView2.Rows[i].Cells[1].Value = data_ampacity_freeair[i, 3];
                        dataGridView2.Rows[i].Cells[2].Value = data_ampacity_freeair[i, 4];
                        dataGridView2.Rows[i].Cells[3].Value = data_ampacity_freeair[i, 5];
                    }
                }
                else
                {
                    for (int i = 0; i < 21; i++)
                    {
                        dataGridView2.Rows[i].Cells[1].Value = null;
                        dataGridView2.Rows[i].Cells[2].Value = null;
                        dataGridView2.Rows[i].Cells[3].Value = null;
                    }
                }
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                if (comboBox2.SelectedIndex == 0)
                {
                    for (int i = 0; i < 21; i++)
                    {
                        dataGridView1.Rows[i].Cells[1].Value = data_electrical[i, 0];
                        dataGridView1.Rows[i].Cells[2].Value = data_electrical[i, 1];
                        dataGridView1.Rows[i].Cells[3].Value = data_electrical[i, 2];
                        dataGridView1.Rows[i].Cells[4].Value = data_electrical[i, 3];
                        dataGridView1.Rows[i].Cells[5].Value = data_electrical[i, 4];
                    }
                }
                else if (comboBox2.SelectedIndex == 1)
                {
                    for (int i = 0; i < 21; i++)
                    {
                        dataGridView1.Rows[i].Cells[1].Value = data_electrical[i, 0];
                        dataGridView1.Rows[i].Cells[2].Value = data_electrical[i, 1];
                        dataGridView1.Rows[i].Cells[3].Value = data_electrical[i, 5];
                        dataGridView1.Rows[i].Cells[4].Value = data_electrical[i, 6];
                        dataGridView1.Rows[i].Cells[5].Value = data_electrical[i, 7];
                    }
                }
                else
                {
                    for (int i = 0; i < 21; i++)
                    {
                        dataGridView1.Rows[i].Cells[1].Value = null;
                        dataGridView1.Rows[i].Cells[2].Value = null;
                        dataGridView1.Rows[i].Cells[3].Value = null;
                        dataGridView1.Rows[i].Cells[4].Value = null;
                        dataGridView1.Rows[i].Cells[5].Value = null;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 21; i++)
                {
                    dataGridView2.Rows[i].Cells[1].Value = null;
                    dataGridView2.Rows[i].Cells[2].Value = null;
                    dataGridView2.Rows[i].Cells[3].Value = null;

                    dataGridView1.Rows[i].Cells[1].Value = null;
                    dataGridView1.Rows[i].Cells[2].Value = null;
                    dataGridView1.Rows[i].Cells[3].Value = null;
                    dataGridView1.Rows[i].Cells[4].Value = null;
                    dataGridView1.Rows[i].Cells[5].Value = null;
                }
            }
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill_datagridview();
        }
    }
}
