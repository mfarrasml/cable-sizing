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
    public partial class Form10 : Form
    {
        //global variables
        public static string[,] cabledata_nec = new string[21,6];
        public static string[,] prevcabledata_nec = new string[21, 6];
        public static string[,] confirmedcabledata_nec = new string[21, 6];

        public static double[,] cabledata = new double[17, 6];
        public static double[,] prevcabledata = new double[17, 6];
        public static double[,] confirmedcabledata = new double[17, 6];
        public static bool okclicked = false;

        //local variables
        int cablecount;
        bool[] datAvailable = new bool[21]; 
        int nMax = 21;
        bool inValid;
        double maxTemp;
        int currentrow;

        double k_material;

        public Form10()
        {
            InitializeComponent();
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            okclicked = false;

            cabledata_nec[0, 0] = "14";
            cabledata_nec[1, 0] = "12";
            cabledata_nec[2, 0] = "10";
            cabledata_nec[3, 0] = "8";
            cabledata_nec[4, 0] = "6";
            cabledata_nec[5, 0] = "4";
            cabledata_nec[6, 0] = "3";
            cabledata_nec[7, 0] = "2";
            cabledata_nec[8, 0] = "1";
            cabledata_nec[9, 0] = "1/0";
            cabledata_nec[10, 0] = "2/0";
            cabledata_nec[11, 0] = "3/0";
            cabledata_nec[12, 0] = "4/0";
            cabledata_nec[13, 0] = "250";
            cabledata_nec[14, 0] = "300";
            cabledata_nec[15, 0] = "350";
            cabledata_nec[16, 0] = "400";
            cabledata_nec[17, 0] = "500";
            cabledata_nec[18, 0] = "600";
            cabledata_nec[19, 0] = "750";
            cabledata_nec[20, 0] = "1000";

            /*
            cabledata[0, 0] = 1.5;
            cabledata[1, 0] = 2.5;
            cabledata[2, 0] = 4;
            cabledata[3, 0] = 6;
            cabledata[4, 0] = 10;
            cabledata[5, 0] = 16;
            cabledata[6, 0] = 25;
            cabledata[7, 0] = 35;
            cabledata[8, 0] = 50;
            cabledata[9, 0] = 70;
            cabledata[10, 0] = 95;
            cabledata[11, 0] = 120;
            cabledata[12, 0] = 150;
            cabledata[13, 0] = 185;
            cabledata[14, 0] = 240;
            cabledata[15, 0] = 300;
            cabledata[16, 0] = 400;
            */

            dataGridView1.RowCount = 21;

            for (int i = 0; i <21; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = cabledata_nec[i, 0];
                dataGridView1.Rows[i].Cells[0].ReadOnly = true;
            }

            if (Form9.initialTemp != 0)
            {
                comboBox1.Text = Form9.initialTemp.ToString();
            }

            if (Form9.material == "Copper")
            {
                k_material = 234.5;
            }
            else if (Form9.material == "Aluminium")
            {
                k_material = 228.1;
            }
            else
            {
                k_material = 0;
            }

        }

        private void DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.KeyPress += new KeyPressEventHandler(DataGridView1_KeyPress);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }

        private void DataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex > 0)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Form1.decimalseparator))
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if ((e.KeyChar == Form1.decimalseparator) && ((sender as TextBox).Text.IndexOf(Form1.decimalseparator) > -1))
                {
                    e.Handled = true;
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            tableToArray();
            inValid = cekValidasiTable();
            if (!inValid)
            {
                finalCableData();
                Form9.cableCount = cablecount;
                okclicked = true;
                PrevCable();
                Close();
            }
            else
            {
                MessageBox.Show("Invalid: All cells in a row must be either filled entirely or left empty!", "Input Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void DataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void tableToArray()
        {
            bool terisi;
            for (int i = 0; i < nMax; i++)
            {
                int j = 0;
                terisi = true;
                while (j < 6)
                {
                    cabledata_nec[i, j] = Convert.ToString(dataGridView1.Rows[i].Cells[j].Value);
                    if (cabledata_nec[i, j] == "")
                    {
                        terisi = false;
                    }
                    j++;
                }
                if (terisi)
                {
                    datAvailable[i] = true;
                }
                else
                {
                    datAvailable[i] = false;
                }
            }
        }
        private bool cekValidasiTable()
        {
            bool inVal;
            int count;
            inVal = false;
            int i = 0;
            while ((i <nMax) && !inVal)
            {
                int j = 1;
                count = 0;
                while (j < 6)
                {
                    if(cabledata_nec[i, j] == "")
                    {
                        count++;
                    }
                    j++;
                }
                if ((count < 5) && (count > 0))
                {
                    inVal = true;
                }
                i++;
            }
            return inVal;
        }

        private void finalCableData()
        {
            cablecount = 0;
            for (int i = 0; i <nMax; i++)
            {
                if (datAvailable[i])
                {
                    for (int j = 0; j < 6; j++)
                    {
                        confirmedcabledata_nec[cablecount, j] = cabledata_nec[i, j];
                    }
                    cablecount++;
                }
            } 
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form10_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!okclicked)
            {
                dataGridView1.CurrentCell.Selected = false;
                for (int i = 0; i < nMax; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (prevcabledata_nec[i, j] == "")
                        {
                            dataGridView1.Rows[i].Cells[j].Value =  null;
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells[j].Value = prevcabledata_nec[i, j];
                        }
                    }

                }
            }
        }

        private void PrevCable()
        {
            for (int i = 0; i < nMax; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    prevcabledata_nec[i, j] = cabledata_nec[i, j];
                }
            }
        }

        private void ComboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Form1.decimalseparator))
            {
                e.Handled = true;
            }

            if ((comboBox1.Text == "") && (e.KeyChar == Form1.decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == Form1.decimalseparator) && ((sender as TextBox).Text.IndexOf(Form1.decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void ComboBox1_TextChanged(object sender, EventArgs e) //note : sudah diubah
        {
            if (comboBox1.Text != "")
            {
                maxTemp = double.Parse(comboBox1.Text);

                for (int i = 0; i < nMax; i++)
                {
                    if (Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value) != 0)
                    {
                        dataGridView1.Rows[i].Cells[2].Value = Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value) * (k_material + maxTemp) / (k_material + 20);
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[2].Value = null;
                    }
                }
            }
            else
            {
                for (int i = 0; i < nMax; i++)
                {
                    dataGridView1.Rows[i].Cells[2].Value = null;
                }
            }
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            bool rowNotEmpty = false;

            for (int i = 1; i <6; i++)
            {
                if(dataGridView1.CurrentRow.Cells[i].Value != null)
                {
                    rowNotEmpty = true;
                }
            }
            //activate delete button
            if ((rowNotEmpty) && (dataGridView1.CurrentCell.Selected))
            {
                button4.Enabled = true;
                currentrow = dataGridView1.CurrentCell.RowIndex;
            }
            else
            {
                button4.Enabled = false;
            }

            //calculate and update R at initial temperature
            if (comboBox1.Text != "") // note : sudah diubah
            {

                maxTemp = double.Parse(comboBox1.Text);

                for (int i = 0; i < nMax; i++)
                {
                    if (Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value) != 0)
                    {
                        dataGridView1.Rows[i].Cells[2].Value = Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value) * (k_material + maxTemp) / (k_material + 20);
                    }
                    else
                    {
                       
                        dataGridView1.Rows[i].Cells[2].Value = null;
                        
                    }
                }
            }
            else
            {
                for (int i = 0; i < nMax; i++)
                {
                    dataGridView1.Rows[i].Cells[2].Value = null;
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.Selected)
            {
                for (int i = 1; i < 6; i++)
                {
                    dataGridView1.Rows[currentrow].Cells[i].Value = null;
                }
            }
            button4.Enabled = false;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Delete all data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                for (int i = 0; i < nMax; i++)
                {
                    for (int k = 1; k < 6; k++)
                    {
                        dataGridView1.Rows[i].Cells[k].Value = null;
                    }
                }
            }
        }
    }
}
