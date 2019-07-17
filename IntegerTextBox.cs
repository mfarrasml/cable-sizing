using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test1
{
    public class IntegerTextBox : TextBox
    {
        string Input;
        int Number;

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // Allow writing digits only
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            base.OnKeyPress(e);
        }

        protected override void WndProc(ref Message m)
        {
            // Trap WM_PASTE:
            if (m.Msg == 0x302 && Clipboard.ContainsText())
            {
                Input = Clipboard.GetText();

                if (!int.TryParse(Input, out Number))
                {
                    SelectedText = "";
                }
                else
                {
                    Text = "";
                    SelectedText = Input;
                }

                return;
            }
            base.WndProc(ref m);
        }
    }
}
