using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using SharpDX.XInput;
using WindowsInput.Native;

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
        private NotifyIcon trayIcon;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            InitTrayIcon();
            InitXInputs();
            GearPosition = 1; // start in 1st gear

            Thread loopThread = new Thread(PollJoystickXinput);
            loopThread.Start(); // start the PollJoystickXinput thread
        }

        /// <summary>
        /// Sends a keystroke to the active window.
        /// </summary>
        /// <param name="KeyStroke">The key to send.</param>
        public void HandleVirtualKeystroke(string KeyStroke) // e.g. I K O L 
        {
            String AppName = AppName_textBox.Text;
            bool BringToFront = BringToFront_CheckBox.Checked;
            if (BringToFront)
            {
                FindRunningApp(AppName, WindowSearchOptions.BringWindowToFront); // may not find app but send key press below anyway....
            }
            else
            {
                FindRunningApp(AppName, WindowSearchOptions.none); // may not find app but send key press below anyway....
            }

            //
            //if (GearToKeyCodeMap.TryGetValue(KeyStroke, out VirtualKeyCode keyCode))
            {
                DebugPrintLine(">>>>>>>>>>>>>>>> Send keystroke: '" + KeyStroke + "'");
                SendKey_InputSimulator(0); // does not work
            }
            //else
            //{
            //    DebugPrintLine("VirtualKeyCode not, complete the table you lazy git");
            //}
        }

        public void InitTrayIcon()
        {
            trayIcon = new NotifyIcon();
            trayIcon.Text = "JoyKey";
            trayIcon.Icon = new System.Drawing.Icon("./pad.ico"); // Set your icon path here
            trayIcon.Visible = false;
            trayIcon.DoubleClick += TrayIcon_DoubleClick;
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
                DebugPrintLine("ERROR: invalid delay value, will now use default:100");
                return 100; // default
            }
        }
    }
}
