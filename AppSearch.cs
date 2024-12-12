using System;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace JoyKey
{
    public partial class Form1 : Form
    {

        public enum WindowSearchOptions
        {
            none,
            BringWindowToFront
        }

        public bool FindRunningApp(string windowTitle, WindowSearchOptions FocusOption)
        {
            bool found = false;

            Process p = Process.GetProcessesByName(windowTitle).FirstOrDefault();

            IntPtr hWnd = FindWindow(null, windowTitle);
            if (hWnd != IntPtr.Zero || p != null)
            {
                found = true;
                DebugPrintLine(windowTitle + " found");
                if (FocusOption == WindowSearchOptions.BringWindowToFront)
                {
                    // niblett, fix >> Process p = ......
                    SetForegroundWindow(hWnd);
                }
            }
            else
            {
                DebugPrintLine("ERROR: " + windowTitle + " not found");
            }
            return found;
        }
    }
}
