using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoyKey
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Prints debug text to the rich text box.
        /// </summary>
        /// <param name="text">The text to print.</param>
        public void DebugPrint(string text)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new Action(() => richTextBox1.AppendText(text)));
                richTextBox1.Invoke(new Action(() => richTextBox1.ScrollToCaret()));
            }
            else
            {
                richTextBox1.AppendText(text);
                richTextBox1.ScrollToCaret();
            }
        }

        /// <summary>
        /// Prints debug text with a newline to the rich text box.
        /// </summary>
        /// <param name="text">The text to print.</param>
        public void DebugPrintLine(string text)
        {
            DebugPrint(text + "\n");
        }
    }
}