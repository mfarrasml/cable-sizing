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
    public partial class Form2 : Form
    {
        double k1, k2, kt;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string insul = Form1.insulation;
            string install = Form1.installation;

            if (insul == "XLPE")
            {
                DataTable agdt = new DataTable();
                agdt.Clear();

                agdt.Columns.Add("Ground Temperature");
                agdt.Columns.Add("XLPE Insulation Correction Factor (Kg1)");

                agdt.Rows.Add("10", "1,07");
                agdt.Rows.Add("15", "1,04");
                agdt.Rows.Add("20", "1");
                agdt.Rows.Add("25", "0,96");
                agdt.Rows.Add("30", "0,93");
                agdt.Rows.Add("35", "0,89");
                agdt.Rows.Add("40", "0,85");
                agdt.Rows.Add("45", "0,80");
                agdt.Rows.Add("50", "0,76");
                agdt.Rows.Add("55", "0,71");
                agdt.Rows.Add("60", "0,65");
                agdt.Rows.Add("65", "0,60");
                agdt.Rows.Add("70", "0,53");
                agdt.Rows.Add("75", "0,46");
                agdt.Rows.Add("80", "0,38");

                dataGridView1.DataSource = agdt;
            }
            else if (insul == "PVC")
            {
                DataTable agdt = new DataTable();
                agdt.Clear();

                agdt.Columns.Add("Ground Temperature");
                agdt.Columns.Add("PVC Insulation Correction Factor (Kg1)");
                agdt.Rows.Add("10", "1,1");
                agdt.Rows.Add("15", "1,05");
                agdt.Rows.Add("20", "1");
                agdt.Rows.Add("25", "0,95");
                agdt.Rows.Add("30", "0,89");
                agdt.Rows.Add("35", "0,84");
                agdt.Rows.Add("40", "0,77");
                agdt.Rows.Add("45", "0,71");
                agdt.Rows.Add("50", "0,63");
                agdt.Rows.Add("55", "0,55");
                agdt.Rows.Add("60", "0,45");

                dataGridView1.DataSource = agdt;
            }

            // data table 2
            DataTable kg2dt = new DataTable();
            kg2dt.Clear();

            kg2dt.Columns.Add("Number of Circuits");
            kg2dt.Columns.Add("Cable Touching Correction Factor (Kg2)");

            kg2dt.Rows.Add("1", "1");
            kg2dt.Rows.Add("2", "0,75");
            kg2dt.Rows.Add("3", "0,65");
            kg2dt.Rows.Add("4", "0,6");
            kg2dt.Rows.Add("5", "0,55");
            kg2dt.Rows.Add("6", "0,5");
            kg2dt.Rows.Add("7", "0,45");
            kg2dt.Rows.Add("8", "0,43");
            kg2dt.Rows.Add("9", "0,41");
            kg2dt.Rows.Add("12", "0,36");
            kg2dt.Rows.Add("16", "0,32");
            kg2dt.Rows.Add("20", "0,29");

            dataGridView2.DataSource = kg2dt;

            // properties table 1
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.RowHeadersDefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;

            dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //properties table 2
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.White;

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView2.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(20, 25, 72);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView2.RowHeadersDefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;

            dataGridView2.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView2.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
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
            if (e.RowIndex != -1)
            {
                dataGridView1.CurrentRow.Selected = true;
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                k1 = double.Parse(textBox1.Text);

                kt_check();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                Form1.k1main = k1;
                Form1.k2main = k2;
                Form1.ktmain = kt;

                Form1.ok_clicked = true;

                this.Close();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if ((dataGridView1.CurrentRow != null) && (dataGridView1.CurrentRow.Selected))
            {
                dataGridView1.CurrentRow.Selected = true;
                textBox1.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].FormattedValue.ToString();
                k1 = double.Parse(textBox1.Text);

                kt_check();
            }
        }

        private void DataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if ((dataGridView2.CurrentRow != null) && (dataGridView2.CurrentRow.Selected))
            {
                dataGridView2.CurrentRow.Selected = true;
                textBox2.Text = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[1].FormattedValue.ToString();
                k2 = double.Parse(textBox2.Text);

                kt_check();
            }
        }

        private void DataGridView2_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                dataGridView2.CurrentRow.Selected = true;
                textBox2.Text = dataGridView2.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                k2 = double.Parse(textBox2.Text);

                kt_check();
            }
        }

        private void kt_check()
        {
            if ((textBox1.Text != "") && (textBox2.Text != ""))
            {
                kt = k1 * k2;
                textBox3.Text = kt.ToString("0.##");
            }
            else
            {
                textBox3.Text = "";
            }
        }
    }
}
