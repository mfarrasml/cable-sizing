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
