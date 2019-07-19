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
    

    public partial class OpenForm : Form
    {

        public static bool ReturnToTitle = false;

        public OpenForm()
        {
            InitializeComponent();
            BackColor = Color.FromArgb(94, 215, 255);
            button2.FlatAppearance.MouseOverBackColor = Color.FromArgb(178, 205, 247);
            button3.FlatAppearance.MouseOverBackColor = Color.FromArgb(178, 205, 247);
            button2.FlatAppearance.MouseDownBackColor = Color.FromArgb(153, 175, 209);
            button3.FlatAppearance.MouseDownBackColor = Color.FromArgb(153, 175, 209);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Hide();
            Form1 f1 = new Form1();
            f1.FormClosed += Form_Closed; 
            f1.Show();
        }

        private void Form_Closed(object sender, FormClosedEventArgs e)
        {
            if (ReturnToTitle)
            {
                Show();
                ReturnToTitle = false;
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Hide();
            Form9 f9 = new Form9();
            f9.FormClosed += Form_Closed;
            f9.Show();
        }

    }
}
