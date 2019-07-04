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
    public partial class Form3 : Form
    {
        double k1, k2, kt;



        public Form3()
        {
            InitializeComponent();
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

            if ((dataGridView2.CurrentCell != null) && (dataGridView2.CurrentCell.ColumnIndex > 2))
            {
                textBox2.Text = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[dataGridView2.CurrentCell.ColumnIndex].FormattedValue.ToString();
                k2 = double.Parse(textBox2.Text);
            }

            kt_check();
        }


        private void DataGridView2_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                dataGridView2.CurrentCell.Selected = true;
                if (e.ColumnIndex >2)
                {
                    textBox2.Text = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();
                    k2 = double.Parse(textBox2.Text);
                }

                kt_check();
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            string insul = Form1.insulation;
            string install = Form1.installation;

            if ((insul == "XLPE") || (insul == "EPR"))
            {
                DataTable agdt = new DataTable();
                agdt.Clear();

                agdt.Columns.Add("Ambient Air Temperature");
                agdt.Columns.Add("XLPE Insulation Correction Factor (Ka1)");

                agdt.Rows.Add(10, 1.15);
                agdt.Rows.Add(15, 1.12);
                agdt.Rows.Add(20, 1.08);
                agdt.Rows.Add(25, 1.04);
                agdt.Rows.Add(30, 1.0);
                agdt.Rows.Add(35, 0.96);
                agdt.Rows.Add(40, 0.91);
                agdt.Rows.Add(45, 0.87);
                agdt.Rows.Add(50, 0.82);
                agdt.Rows.Add(55, 0.76);
                agdt.Rows.Add(60, 0.71);
                agdt.Rows.Add(65, 0.65);
                agdt.Rows.Add(70, 0.58);
                agdt.Rows.Add(75, 0.5);
                agdt.Rows.Add(80, 0.41);

                dataGridView1.DataSource = agdt;
            }
            else if (insul == "PVC")
            {
                DataTable agdt = new DataTable();
                agdt.Clear();

                agdt.Columns.Add("Ground Temperature");
                agdt.Columns.Add("PVC Insulation Correction Factor (Ka1)");

                agdt.Rows.Add(10, 1.22);
                agdt.Rows.Add(15, 1.17);
                agdt.Rows.Add(20, 1.12);
                agdt.Rows.Add(25, 1.06);
                agdt.Rows.Add(30, 1.0);
                agdt.Rows.Add(35, 0.94);
                agdt.Rows.Add(40, 0.87);
                agdt.Rows.Add(45, 0.79);
                agdt.Rows.Add(50, 0.71);
                agdt.Rows.Add(55, 0.61);
                agdt.Rows.Add(60, 0.5);

                dataGridView1.DataSource = agdt;
            }

            // data table 2
            DataTable ka2dt = new DataTable();

            ka2dt.Columns.Add("Method of Installation");
            ka2dt.Columns.Add("Touching/Spaced");
            ka2dt.Columns.Add("No. of Trays");
            ka2dt.Columns.Add("1 cable");
            ka2dt.Columns.Add("2 cables");
            ka2dt.Columns.Add("3 cables");
            ka2dt.Columns.Add("4 cables");
            ka2dt.Columns.Add("6 cables");
            ka2dt.Columns.Add("9 cables");

            ka2dt.Rows.Add("Perforated cable tray system", "Touching", "1", 1.0, 0.88, 0.82, 0.79, 0.76, 0.73);
            ka2dt.Rows.Add("Perforated cable tray system", "Touching", "2", 1.0, 0.87, 0.8, 0.77, 0.73, 0.68);
            ka2dt.Rows.Add("Perforated cable tray system", "Touching", "3", 1.0, 0.86, 0.79, 0.76, 0.71, 0.66);
            ka2dt.Rows.Add("Perforated cable tray system", "Touching", "6", 1.0, 0.84, 0.77, 0.73, 0.68, 0.64);
            ka2dt.Rows.Add("Perforated cable tray system", "Spaced", "1", 1.0, 1.0, 0.98, 0.95, 0.91, 0.0);
            ka2dt.Rows.Add("Perforated cable tray system", "Spaced", "2", 1.0, 0.99, 0.96, 0.92, 0.87, 0.0);
            ka2dt.Rows.Add("Perforated cable tray system", "Spaced", "3", 1.0, 0.98, 0.95, 0.91, 0.85, 0.0);

            ka2dt.Rows.Add("Vertical perforated cable tray system", "Touching", "1", 1.0, 0.88, 0.82, 0.78, 0.73, 0.72);
            ka2dt.Rows.Add("Vertical perforated cable tray system", "Touching", "2", 1.0, 0.88, 0.81, 0.76, 0.71, 0.70);
            ka2dt.Rows.Add("Vertical perforated cable tray system", "Spaced", "1", 1.0, 0.91, 0.89, 0.88, 0.87, 0.0);
            ka2dt.Rows.Add("Vertical perforated cable tray system", "Spaced", "2", 1.0, 0.91, 0.88, 0.87, 0.85, 0.0);

            ka2dt.Rows.Add("Unperforated cable tray system", "Touching", "1", 0.97, 0.84, 0.78, 0.75, 0.71, 0.68);
            ka2dt.Rows.Add("Unperforated cable tray system", "Touching", "2", 0.97, 0.83, 0.76, 0.72, 0.68, 0.63);
            ka2dt.Rows.Add("Unperforated cable tray system", "Touching", "3", 0.97, 0.82, 0.75, 0.71, 0.66, 0.61);
            ka2dt.Rows.Add("Unperforated cable tray system", "Touching", "6", 0.97, 0.81, 0.73, 0.69, 0.63, 0.58);

            ka2dt.Rows.Add("Cable ladder systems, cleats, etc.", "Touching", "1", 1.0, 0.87, 0.82, 0.80, 0.79, 0.78);
            ka2dt.Rows.Add("Cable ladder systems, cleats, etc.", "Touching", "2", 1.0, 0.86, 0.80, 0.78, 0.76, 0.73);
            ka2dt.Rows.Add("Cable ladder systems, cleats, etc.", "Touching", "3", 1.0, 0.85, 0.79, 0.76, 0.73, 0.70);
            ka2dt.Rows.Add("Cable ladder systems, cleats, etc.", "Touching", "6", 1.0, 0.84, 0.77, 0.73, 0.68, 0.64);

            dataGridView2.DataSource = ka2dt;

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
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            for (int i = 0; i<9; i++)
            {
                dataGridView2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
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

        private void Button3_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Show();
        }

        private void DataGridView2_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == dataGridView2.Rows.Count - 1)
            {
                e.AdvancedBorderStyle.Bottom = dataGridView2.AdvancedCellBorderStyle.Bottom;
            }
            else
            {
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
            }
            if (e.RowIndex < 1 || e.ColumnIndex < 0)
                return;
            if ((IsTheSameCellValue(e.ColumnIndex, e.RowIndex)) && (e.ColumnIndex < 3))
            {
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            }
            else
            {
                e.AdvancedBorderStyle.Top = dataGridView2.AdvancedCellBorderStyle.Top;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == 0)
                return;
            if ((IsTheSameCellValue(e.ColumnIndex, e.RowIndex)) && (e.ColumnIndex < 3))
            {
                e.Value = "";
                e.FormattingApplied = true;
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

        bool IsTheSameCellValue(int column, int row)
        {
            DataGridViewCell cell1 = dataGridView2[column, row];
            DataGridViewCell cell2 = dataGridView2[column, row - 1];
            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }
            return cell1.Value.ToString() == cell2.Value.ToString();
        }
    }
}
