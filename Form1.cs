using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using SharpDX.XInput;

namespace JoyKey
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Finds a window by its class name and window name.
        /// </summary>
        /// <param name="lpClassName">The class name of the window.</param>
        /// <param name="lpWindowName">The name of the window.</param>
        /// <returns>A handle to the window.</returns>
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// Brings the specified window to the foreground.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <returns>True if the window was brought to the foreground; otherwise, false.</returns>
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        int GearPosition;
        Controller MyController;

        //InputSimulator inputSimulator; // niblett

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            InitXInputs();
            GearPosition = 1; // start in 1st gear
            Thread loopThread = new Thread(PollJoystickXinput);
            loopThread.Start(); // start the PollJoystickXinput thread
        }

        public int GetKeyHoldTime()
        {
            int delay;
            if (int.TryParse(KeyHoldTime_TextBox.Text, out delay))
            {
                return delay;
            }
            else
            {
                DebugPrintLine("ERROR: invalid delay value, using default:100");
                return 100; // default
            }
        }
    }
}
