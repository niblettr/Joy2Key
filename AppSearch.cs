using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joy2Key
{
    public partial class Form1 : Form
    {

        /// <summary>
        /// Finds the application window by its name.
        /// </summary>
        /// <returns>True if the application window was found; otherwise, false.</returns>
        public bool FindApp()
        {
            bool found = false;
            String AppName = AppName_textBox.Text;
#if false
            Process p = Process.GetProcessesByName(AppName).FirstOrDefault(); // Sega Rally Championship (Rev B) // Model 2 Emulator  // Calculator

            if (p != null)
#else
            IntPtr p = FindWindow(null, AppName); // Sega Rally Championship (Rev B) // Model 2 Emulator  // Calculator

            if (SetForegroundWindow(p)) // working: calculator Model 2 Emulator 
#endif
            {
                found = true;
                DebugPrintLine(AppName + " found");
            }
            else
            {
                DebugPrintLine("ERROR: " + AppName + " not found");
            }
            return found;
        }

        // AI version
        public static void BringWindowToFront(string windowTitle)
        {
            IntPtr hWnd = FindWindow(null, windowTitle); // Use the window title of the Model 2 emulator
            if (hWnd != IntPtr.Zero)
            {
                SetForegroundWindow(hWnd);
            }
        }
    }
}
