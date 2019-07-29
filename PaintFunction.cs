using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test1
{
    public class GradientPanel : Panel
    {
        public Color TopColor { get; set; }
        public Color BottomColor { get; set; }
        public float Angle { get; set; }

        public GradientPanel()
        {
            DoubleBuffered = true;

            // or

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            LinearGradientBrush brush = new LinearGradientBrush(ClientRectangle, TopColor, BottomColor, Angle);
            g.FillRectangle(brush, ClientRectangle);

            base.OnPaint(e);

        }
    }

    public class GradientForm : Form
    {
        public Color TopColor { get; set; }
        public Color BottomColor { get; set; }
        public float Angle { get; set; }


        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            LinearGradientBrush brush = new LinearGradientBrush(ClientRectangle, TopColor, BottomColor, Angle);
            g.FillRectangle(brush, ClientRectangle);

            base.OnPaint(e);

        }
    }
}
