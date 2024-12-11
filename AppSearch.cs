
namespace Joy2Key
{
    public partial class Form1 : Form
    {

        public enum WindowSearchOptions
        {
            none,
            BringWindowToFront
        }

        public bool FindRunningApp(string windowTitle, WindowSearchOptions FocusOption) // SetGear(GearDirection.Up);
        {
            bool found = false;

            IntPtr hWnd = FindWindow(null, windowTitle); // Use the window title of the Model 2 emulator
            if (hWnd != IntPtr.Zero)
            {
                found = true;
                DebugPrintLine(windowTitle + " found");
                if (FocusOption == WindowSearchOptions.BringWindowToFront)
                {
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
