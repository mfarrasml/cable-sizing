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
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            if (ClientRectangle.Width == 0 || ClientRectangle.Height == 0) return;
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
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
            if (ClientRectangle.Width == 0 || ClientRectangle.Height == 0) return;
            Graphics g = e.Graphics;
            LinearGradientBrush brush = new LinearGradientBrush(ClientRectangle, TopColor, BottomColor, Angle);
            g.FillRectangle(brush, ClientRectangle);

            base.OnPaint(e);

        }
    }
}
