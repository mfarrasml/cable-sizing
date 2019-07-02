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
    public partial class Form6 : Form
    {
        public static double[,] cabledata = new double[17, 5];

        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
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

            for (int i = 0; i <17; i++)
            {
                dataGridView1.RowCount++;
                dataGridView1.Rows[i].Cells[0].Value = cabledata[i, 0];
                dataGridView1.Rows[i].Cells[0].ReadOnly = true;
            }
        }
    }
}
