using System.Runtime.InteropServices;
using System.Diagnostics;
using SharpDX.XInput;
using static System.Windows.Forms.AxHost;
using WindowsInput.Native;
using WindowsInput;
using System.Windows.Forms;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading;

namespace Joy2Key
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
        InputSimulator inputSimulator;

        /// <summary>
        /// Represents the direction of the gear shift.
        /// </summary>
        public enum GearDirection
        {
            Up,
            Down
        }

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

        /// <summary>
        /// Sets the gear position based on the direction.
        /// </summary>
        /// <param name="direction">The direction to shift the gear.</param>
        public void SetGear(GearDirection direction)
        {
            if (direction == GearDirection.Up)
            {
                if (++GearPosition > 4)
                {
                    GearPosition = 4; // clamp
                }

            }
            else if (direction == GearDirection.Down)
            {
                if (--GearPosition < 1)
                {
                    GearPosition = 1; // clamp
                }
            }
            DebugPrintLine($"Gear position : {GearPosition.ToString()}");

            string key = string.Empty;

            switch (GearPosition)
            {
                case 1:
                    key = "I";
                    break;
                case 2:
                    key = "K";
                    break;
                case 3:
                    key = "O";
                    break;
                case 4:
                    key = "L";
                    break;
            }

            SendKeystroke(key);
        }
    }
}