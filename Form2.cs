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
    public partial class Form2 : GradientForm
    {
        double k1, k2, k3, kt;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string insul = Form1.insulation;
            string install = Form1.installation;

            //table 1 data
            if ((insul == "XLPE") || (insul == "EPR"))
            {
                DataTable agdt = new DataTable();
                agdt.Clear();

                agdt.Columns.Add("Ground Temperature");
                agdt.Columns.Add("XLPE/EPR Insulation Correction Factor (Kg1)");

                agdt.Rows.Add(10, 1.07);
                agdt.Rows.Add(15, 1.04);
                agdt.Rows.Add(20, 1);
                agdt.Rows.Add(25, 0.96);
                agdt.Rows.Add(30, 0.93);
                agdt.Rows.Add(35, 0.89);
                agdt.Rows.Add(40, 0.85);
                agdt.Rows.Add(45, 0.80);
                agdt.Rows.Add(50, 0.76);
                agdt.Rows.Add(55, 0.71);
                agdt.Rows.Add(60, 0.65);
                agdt.Rows.Add(65, 0.60);
                agdt.Rows.Add(70, 0.53);
                agdt.Rows.Add(75, 0.46);
                agdt.Rows.Add(80, 0.38);


                dataGridView1.DataSource = agdt;
            }
            else if (insul == "PVC")
            {
                DataTable agdt = new DataTable();
                agdt.Clear();

                agdt.Columns.Add("Ground Temperature");
                agdt.Columns.Add("PVC Insulation Correction Factor (Kg1)");
                agdt.Rows.Add(10, 1.1);
                agdt.Rows.Add(15, 1.05);
                agdt.Rows.Add(20, 1);
                agdt.Rows.Add(25, 0.95);
                agdt.Rows.Add(30, 0.89);
                agdt.Rows.Add(35, 0.84);
                agdt.Rows.Add(40, 0.77);
                agdt.Rows.Add(45, 0.71);
                agdt.Rows.Add(50, 0.63);
                agdt.Rows.Add(55, 0.55);
                agdt.Rows.Add(60, 0.45);

                dataGridView1.DataSource = agdt;
            }

            // data table 2
            DataTable kg2dt = new DataTable();
            DataTable kg3dt = new DataTable();
            kg2dt.Clear();
            kg3dt.Clear();

            

            if (install == "D1 (Under Ground)")
            {
                kg2dt.Columns.Add("Number of Circuits");
                kg2dt.Columns.Add("Cable Touching Correction Factor (Kg2)");
                kg2dt.Columns.Add("0.25 m");
                kg2dt.Columns.Add("0.5 m");
                kg2dt.Columns.Add("1 m");

                kg2dt.Rows.Add(1, 1, 1, 1, 1);
                kg2dt.Rows.Add(2, 0.85, 0.9, 0.95, 0.95);
                kg2dt.Rows.Add(3, 0.75, 0.85, 0.9, 0.95);
                kg2dt.Rows.Add(4, 0.7, 0.8, 0.85, 0.9);
                kg2dt.Rows.Add(5, 0.65, 0.8, 0.85, 0.9);
                kg2dt.Rows.Add(6, 0.6, 0.8, 0.8, 0.9);
                kg2dt.Rows.Add(7, 0.57, 0.76, 0.8, 0.88);
                kg2dt.Rows.Add(8, 0.54, 0.74, 0.78, 0.88);
                kg2dt.Rows.Add(9, 0.52, 0.73, 0.77, 0.87);
                kg2dt.Rows.Add(10, 0.49, 0.72, 0.76, 0.86);
                kg2dt.Rows.Add(11, 0.47, 0.7, 0.75, 0.86);
                kg2dt.Rows.Add(12, 0.45, 0.69, 0.74, 0.85);
                kg2dt.Rows.Add(13, 0.44, 0.68, 0.73, 0.85);
                kg2dt.Rows.Add(14, 0.42, 0.68, 0.72, 0.84);
                kg2dt.Rows.Add(15, 0.41, 0.67, 0.72, 0.84);
                kg2dt.Rows.Add(16, 0.39, 0.66, 0.71, 0.83);
                kg2dt.Rows.Add(17, 0.38, 0.65, 0.7, 0.83);
                kg2dt.Rows.Add(18, 0.37, 0.65, 0.7, 0.83);
                kg2dt.Rows.Add(19, 0.35, 0.64, 0.69, 0.82);
                kg2dt.Rows.Add(20, 0.34, 0.63, 0.68, 0.82);

                dataGridView2.DataSource = kg2dt;

                for (int i = 0; i < 5; i++)
                {
                    dataGridView2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridView2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                kg3dt.Columns.Add("Thermal Resistivity (K·m/W)");
                kg3dt.Columns.Add("Correction Factor for Cables in Buried Ducts");

                kg3dt.Rows.Add(0.5, 1.28);
                kg3dt.Rows.Add(0.7, 1.2);
                kg3dt.Rows.Add(1, 1.18);
                kg3dt.Rows.Add(1.5, 1.1);
                kg3dt.Rows.Add(2, 1.05);
                kg3dt.Rows.Add(2.5, 1.0);
                kg3dt.Rows.Add(3, 0.96);

                dataGridView3.DataSource = kg3dt;
                for (int i = 0; i < 2; i++)
                {
                    dataGridView3.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridView3.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

            }
            else if (install == "D2 (Under Ground)")
            {
                kg2dt.Columns.Add("Number of Circuits");
                kg2dt.Columns.Add("Cable Touching Correction Factor (Kg2)");
                kg2dt.Columns.Add("One Cable Diameter");
                kg2dt.Columns.Add("0.125 m");
                kg2dt.Columns.Add("0.25 m");
                kg2dt.Columns.Add("0.5 m");

                kg2dt.Rows.Add(1, 1, 1, 1, 1, 1);
                kg2dt.Rows.Add(2, 0.75, 0.8, 0.85, 0.9, 0.9);
                kg2dt.Rows.Add(3, 0.65, 0.7, 0.765, 0.8, 0.85);
                kg2dt.Rows.Add(4, 0.6, 0.6, 0.7, 0.75, 0.8);
                kg2dt.Rows.Add(5, 0.55, 0.55, 0.65, 0.7, 0.8);
                kg2dt.Rows.Add(6, 0.5, 0.55, 0.6, 0.7, 0.8);
                kg2dt.Rows.Add(7, 0.45, 0.51, 0.56, 0.67, 0.76);
                kg2dt.Rows.Add(8, 0.43, 0.48, 0.57, 0.65, 0.75);
                kg2dt.Rows.Add(9, 0.41, 0.46, 0.55, 0.63, 0.74);
                kg2dt.Rows.Add(12, 0.36, 0.42, 0.51, 0.59, 0.71);
                kg2dt.Rows.Add(16, 0.32, 0.38, 0.47, 0.56, 0.68);
                kg2dt.Rows.Add(20, 0.29, 0.35, 0.44, 0.53, 0.66);

                dataGridView2.DataSource = kg2dt;

                for (int i = 0; i < 6; i++)
                {
                    dataGridView2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridView2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                kg3dt.Columns.Add("Thermal Resistivity (K·m/W)");
                kg3dt.Columns.Add("Correction Factor for Direct Buries Cables");

                kg3dt.Rows.Add(0.5, 1.88);
                kg3dt.Rows.Add(0.7, 1.62);
                kg3dt.Rows.Add(1, 1.5);
                kg3dt.Rows.Add(1.5, 1.28);
                kg3dt.Rows.Add(2, 1.12);
                kg3dt.Rows.Add(2.5, 1.0);
                kg3dt.Rows.Add(3, 0.90);

                dataGridView3.DataSource = kg3dt;
                for (int i = 0; i < 2; i++)
                {
                    dataGridView3.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridView3.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }

            

            // table layout
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

            //properties table 3
            dataGridView3.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView3.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView3.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView3.BackgroundColor = Color.White;

            dataGridView3.EnableHeadersVisualStyles = false;
            dataGridView3.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView3.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(20, 25, 72);
            dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView3.RowHeadersDefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;

            //datagridview 1
            int height = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                height += row.Height;
            }
            height += dataGridView1.ColumnHeadersHeight;
            if (height < dataGridView1.Height)
            {
                dataGridView1.ClientSize = new Size(dataGridView1.Width, height);
            }

            //datagridview 2
            height = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                height += row.Height;
            }
            height += dataGridView2.ColumnHeadersHeight;

            if (height < dataGridView2.Height)
            {
                dataGridView2.ClientSize = new Size(dataGridView2.Width, height );
            }

            //datagridview 3
            height = 0;
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                height += row.Height;
            }
            height += dataGridView3.ColumnHeadersHeight;

            if (height < dataGridView3.Height)
            dataGridView3.ClientSize = new Size(dataGridView3.Width, height);
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
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
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
                Form1.k3main = k3;
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
                textBox1.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString();
                k1 = double.Parse(textBox1.Text);

                kt_check();
            }
        }

        private void DataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if ((dataGridView2.CurrentCell.RowIndex != -1) && (dataGridView2.CurrentCell.ColumnIndex > 0))
            {
                textBox2.Text = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[dataGridView2.CurrentCell.ColumnIndex].Value.ToString();
                k2 = double.Parse(textBox2.Text);

                kt_check();
            }
        }

        private void DataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            if ((dataGridView3.CurrentRow != null) && (dataGridView3.CurrentRow.Selected))
            {
                textBox4.Text = dataGridView3.Rows[dataGridView3.CurrentCell.RowIndex].Cells[1].Value.ToString();
                k3 = double.Parse(textBox4.Text);

                kt_check();
            }
        }

        private void DataGridView2_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.RowIndex != -1) && (e.ColumnIndex > 0))
            {
                textBox2.Text = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                k2 = double.Parse(textBox2.Text);

                kt_check();
            }
        }

        private void kt_check()
        {
            if ((textBox1.Text != "") && (textBox2.Text != "") && (textBox4.Text != ""))
            {
                kt = k1 * k2 * k3;
                textBox3.Text = kt.ToString("0.##");
            }
            else
            {
                textBox3.Text = "";
            }
        }
    }
}
