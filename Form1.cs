using System.Runtime.InteropServices;
using System.Diagnostics;
using SharpDX.XInput;
using static System.Windows.Forms.AxHost;
using WindowsInput.Native;
using WindowsInput;
using System.Windows.Forms;
using System.Reflection;
using System.Security.Cryptography;

namespace ControllerKeystroke
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

        /// <summary>
        /// Synthesizes a keystroke.
        /// </summary>
        /// <param name="bVk">The virtual-key code of the key.</param>
        /// <param name="bScan">The hardware scan code of the key.</param>
        /// <param name="dwFlags">Controls various aspects of function operation.</param>
        /// <param name="dwExtraInfo">An additional value associated with the keystroke.</param>
        [DllImport("user32.dll", SetLastError = true)]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        private const int KEYEVENTF_KEYUP = 0x0002;

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

        private static readonly Dictionary<string, VirtualKeyCode> GearToKeyCodeMap = new Dictionary<string, VirtualKeyCode>
        {
            { "I", VirtualKeyCode.VK_I },
            { "K", VirtualKeyCode.VK_K },
            { "O", VirtualKeyCode.VK_O },
            { "L", VirtualKeyCode.VK_L },
            { "6", VirtualKeyCode.VK_6 }
        };

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
        /// Sends a keystroke to the active window.
        /// </summary>
        /// <param name="KeyStroke">The key to send.</param>
        public void SendKeystroke(string KeyStroke) // e.g. I K O L 
        {
            String AppName = AppName_textBox.Text;
            IntPtr p = FindWindow(null, AppName);

            if (p != null && SetForegroundWindow(p))
            {
                DebugPrintLine("\"" + AppName + "\" found/running");
            }
            else
            {
                DebugPrintLine("ERROR: \"" + AppName + "\" NOT found/running");
            }

            if (GearToKeyCodeMap.TryGetValue(KeyStroke, out VirtualKeyCode keyCode))
            {
                //Keys keyData = (Keys)keyCode;

                kDebugPrintLine(">>>>>>>>>>>>>>>> Send keystroke: '" + KeyStroke + "'");

                // the troublesome section.....
                // why are ascci charaters being sent? Should they not be keyboard scan code?
#if false
                // works for calculator but not Model2 emulator <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                inputSimulator = new InputSimulator();
                inputSimulator.Keyboard.KeyDown(keyCode);
#else
                // works for calculator but not Model2 emulator <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                byte newkey = (byte)KeyStroke[0];
                keybd_event(newkey, 0, 0,               UIntPtr.Zero); // Key down
                keybd_event(newkey, 0, KEYEVENTF_KEYUP, UIntPtr.Zero); // Key up
#endif
            }
            else
            {
                DebugPrintLine("VirtualKeyCode not, complete the table you lazy git");
            }
        }

        /// <summary>
        /// Polls the joystick for input using XInput.
        /// </summary>
        public void PollJoystickXinput()
        {
            if (MyController != null)
            {
                // Poll events from joystick
                while (MyController.IsConnected)
                {
                    var previousState = MyController.GetState();

                    while (MyController.IsConnected)
                    {
                        var MyControllerState = MyController.GetState();

                        if (previousState.PacketNumber != MyControllerState.PacketNumber)
                        {
                            HandleGamepadDataIn(MyControllerState);
                        }

                        Thread.Sleep(1);
                        previousState = MyControllerState;
                    }

                    DebugPrintLine("Controller disconnected");
                    Thread.Sleep(1000); // Wait before trying to reconnect
                }
            }
        }

        /// <summary>
        /// Handles the gamepad data input.
        /// </summary>
        /// <param name="MyControllerState">The state of the controller.</param>
        void HandleGamepadDataIn(SharpDX.XInput.State MyControllerState)
        {
            // DebugPrintLine($"PacketNumber : {MyControllerState.PacketNumber.ToString()}");
            if (checkBox1.Checked)
            {
                DebugPrintLine(MyControllerState.Gamepad.ToString());
            }

            if (MyControllerState.Gamepad.Buttons.HasFlag(GamepadButtonFlags.RightShoulder))
            {
                SetGear(GearDirection.Up);
            }
            else if (MyControllerState.Gamepad.Buttons.HasFlag(GamepadButtonFlags.LeftShoulder))
            {
                SetGear(GearDirection.Down);
            }
        }

        /// <summary>
        /// Initializes the XInput controllers.
        /// </summary>
        void InitXInputs()
        {
            DebugPrintLine("Start XGamepadApp");

            // Initialize XInput
            var controllers = new[] { new Controller(UserIndex.One),
                                      new Controller(UserIndex.Two),
                                      new Controller(UserIndex.Three),
                                      new Controller(UserIndex.Four) };

            // Get 1st controller available
            MyController = null;

            foreach (var selectControler in controllers)
            {
                if (selectControler.IsConnected)
                {
                    MyController = selectControler;
                    break;
                }
            }

            if (MyController == null)
            {
                DebugPrintLine("Input controller not found");
            }
            else
            {
                DebugPrintLine("XInput controller found..");
            }

            DebugPrintLine("Poll events from controller..");
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

        /// <summary>
        /// Clears the content of the rich text box.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        /// <summary>
        /// Finds the application window.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void FindApp_button_Click(object sender, EventArgs e)
        {
            FindApp();
        }

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

        /// <summary>
        /// Handles the click event for the coin button.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void Coin_button_click(object sender, MouseEventArgs e)
        {
            SendKeystroke("6");
        }

        /// <summary>
        /// Handles the click event for the gear up button.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void GearUp_button_click(object sender, MouseEventArgs e)
        {
            SetGear(GearDirection.Up);
        }

        /// <summary>
        /// Handles the click event for the gear down button.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void GearDown_button_Click(object sender, MouseEventArgs e)
        {
            SetGear(GearDirection.Down);
        }
    }
}