using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test1
{
    public partial class Form5 : Form
    {
        //variable
        int currentrow;


        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.Selected)
            {
                this.dataGridView1.Rows.RemoveAt(currentrow);
                for (int i = currentrow; i < Form1.j; i++)
                {
                    this.dataGridView1.Rows[i].Cells[0].Value = i+1;
                }
                Form1.j--;
            }
            if ((dataGridView1.CurrentCell != null) && dataGridView1.CurrentCell.Selected)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void DataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.CurrentCell.Selected)
            {
                button1.Enabled = true;
                currentrow = e.RowIndex;
            }
            else
            {
                button1.Enabled = false;
            }
            
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Delete all data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                if (this.dataGridView1.DataSource != null)
                {
                    this.dataGridView1.DataSource = null;
                }
                else
                {
                    this.dataGridView1.Rows.Clear();
                }
            }
            Form1.j = -1;
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if ((dataGridView1.CurrentCell != null) && (dataGridView1.CurrentCell.Selected))
            {
                button1.Enabled = true;
                currentrow = dataGridView1.CurrentCell.RowIndex;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (Form1.j > -1)
            {
                dataGridView1.SelectAll();
                DataObject dataObj = dataGridView1.GetClipboardContent();
                Clipboard.SetDataObject(dataObj, true);
            }
            else
            {
                Clipboard.Clear();
            }
        }
    }
}
