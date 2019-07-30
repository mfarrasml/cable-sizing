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
    public partial class Form6 : GradientForm
    {
        //global variables
        public static double[,] cabledata = new double[17, 6];
        public static double[,] prevcabledata = new double[17, 6];
        public static double[,] confirmedcabledata = new double[17, 6];
        public static bool okclicked = false;

        //local variables
        int cablecount;
        bool[] datAvailable = new bool[17]; 
        int nMax = 17;
        bool inValid;
        double maxTemp;
        int currentrow;
        

        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            okclicked = false;

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

            dataGridView1.RowCount = 17;

            for (int i = 0; i <17; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = cabledata[i, 0];
                dataGridView1.Rows[i].Cells[0].ReadOnly = true;
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
                Form1.cableCount = cablecount;
                okclicked = true;
                PrevCable();
                Close();
            }
            else
            {
                MessageBox.Show("Invalid: All cells in a row must be either filled entirely or left empty!", "Input Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
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
                    cabledata[i, j] = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                    if (cabledata[i, j] == 0)
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
                    if(cabledata[i, j] == 0)
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
                        confirmedcabledata[cablecount, j] = cabledata[i, j];
                    }
                    cablecount++;
                }
            } 
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!okclicked)
            {
                dataGridView1.CurrentCell.Selected = false;
                for (int i = 0; i < nMax; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (prevcabledata[i, j] == 0)
                        {
                            dataGridView1.Rows[i].Cells[j].Value =  null;
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells[j].Value = prevcabledata[i, j];
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
                    prevcabledata[i, j] = cabledata[i, j];
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

        private void ComboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                maxTemp = double.Parse(comboBox1.Text);

                for (int i = 0; i < 17; i++)
                {
                    if (Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value) != 0)
                    {
                        dataGridView1.Rows[i].Cells[2].Value = Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value) * (1 + (0.00393 * (maxTemp - 20)));
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[2].Value = null;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 17; i++)
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
            if (comboBox1.Text != "")
            {
                maxTemp = double.Parse(comboBox1.Text);

                for (int i = 0; i < 17; i++)
                {
                    if (Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value) != 0)
                    {
                        dataGridView1.Rows[i].Cells[2].Value = Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value) * (1 + (0.00393 * (maxTemp - 20)));
                    }
                    else
                    {
                       
                        dataGridView1.Rows[i].Cells[2].Value = null;
                        
                    }
                }
            }
            else
            {
                for (int i = 0; i < 17; i++)
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
                for (int i = 0; i < 17; i++)
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
