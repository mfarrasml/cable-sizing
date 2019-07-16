using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Test1
{
    public class DoubleTextBox : TextBox
    {
        string Input;
        double Number;
        string tempText;

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if ((SelectionLength > 0) && (e.KeyChar == Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator)))
            {
                tempText = Text.Remove(SelectionStart, SelectionLength) + e.KeyChar;
                if (!double.TryParse(tempText, out Number))
                {
                    e.Handled = true;
                }
            }
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator)))
            {
                e.Handled = true;
            }

            if ((Text == "") && (e.KeyChar == Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator)))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator) &&
                (Text.IndexOf(Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator)) > -1)))
            {
                e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (!double.TryParse(Text, out Number) )
            {
                Text = "";
            }
            base.OnTextChanged(e);
        }

        protected override void WndProc(ref Message m)
        {
            // Trap WM_PASTE:
            if(m.Msg == 0x302 && Clipboard.ContainsText()) {

                if (Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator) == '.')
                {
                    Input = Clipboard.GetText().Replace(',', '.');
                }
                else if (Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator) == ',')
                {
                    Input = Clipboard.GetText().Replace('.', ',');
                }
                else
                {
                    Input = Clipboard.GetText();
                }

                if (!double.TryParse(Input, out Number))
                {
                    this.SelectedText = "";
                }
                else
                {
                    
                    Text = "";
                    this.SelectedText = Input;
                }
                
                return;
            }
            base.WndProc(ref m);
        }
    }
}
