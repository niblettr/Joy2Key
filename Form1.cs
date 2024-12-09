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

        public class KeySimulator
        {
            // Key constants (you can add more as needed)

            public const int VK_SPACE = 0x20;
            public const int VK_ENTER = 0x0D;
            public const int VK_6 = 0x36;  //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

            // Import the SendInput function from user32.dll
            [DllImport("user32.dll", SetLastError = true)]
            public static extern uint SendInput(uint nInputs, [In] INPUT[] pInputs, int cbSize);

            // Input structure for SendInput
            [StructLayout(LayoutKind.Sequential)]
            public struct INPUT
            {
                public uint type;
                public INPUTUNION inputUnion;
            }

            [StructLayout(LayoutKind.Explicit)]

            public struct INPUTUNION
            {
                [FieldOffset(0)] public MOUSEINPUT mi;
                [FieldOffset(0)] public KEYBDINPUT ki;
                [FieldOffset(0)] public HARDWAREINPUT hi;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct MOUSEINPUT
            {
                public int dx;
                public int dy;
                public uint mouseData;
                public uint dwFlags;
                public uint time;
                public UIntPtr dwExtraInfo;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct HARDWAREINPUT
            {
                public uint uMsg;
                public ushort wParamL;
                public ushort wParamH;
            }

            // Structure for keyboard input
            [StructLayout(LayoutKind.Sequential)]
            public struct KEYBDINPUT
            {
                public ushort wVk;
                public ushort wScan;
                public uint dwFlags;
                public uint time;
                public UIntPtr dwExtraInfo;
            }

            // Constants for keypress
            public const uint KEYEVENTF_KEYDOWN = 0x0000;
            public const uint KEYEVENTF_KEYUP = 0x0002;

            // Virtual keycodes
            public const uint INPUT_KEYBOARD = 1;

            // Simulate Key Press (Key Down and Key Up)
            public static void SendKeyPress(ushort key)
            {
                INPUT[] inputDown = new INPUT[1];
                inputDown[0].type = INPUT_KEYBOARD;
                inputDown[0].inputUnion.ki.wVk = key;
                inputDown[0].inputUnion.ki.dwFlags = KEYEVENTF_KEYDOWN;

                INPUT[] inputUp = new INPUT[1];
                inputUp[0].type = INPUT_KEYBOARD;
                inputUp[0].inputUnion.ki.wVk = key;
                inputUp[0].inputUnion.ki.dwFlags = KEYEVENTF_KEYUP;

                // Send the key press (push Down)
                SendInput(1, inputDown, Marshal.SizeOf(typeof(INPUT)));

                // Optionally, wait for a moment to simulate a longer key press
                Thread.Sleep(150);  // Adjust the sleep time as needed

                // Send the key release (release)
                SendInput(1, inputUp, Marshal.SizeOf(typeof(INPUT)));
            }
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

                DebugPrintLine(">>>>>>>>>>>>>>>> Send keystroke: '" + KeyStroke + "'");

                // the troublesome section.....
                // why are ascci charaters being sent? Should they not be keyboard scan code?
#if false
                // works for calculator but not Model2 emulator <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                inputSimulator = new InputSimulator();
                inputSimulator.Keyboard.KeyDown(keyCode);
#else
                // works for calculator but not Model2 emulator <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                //byte newkey = (byte)KeyStroke[0];
                //keybd_event(newkey, 0, 0,               UIntPtr.Zero); // Key down
                //keybd_event(newkey, 0, KEYEVENTF_KEYUP, UIntPtr.Zero); // Key up

                KeySimulator.SendKeyPress(KeySimulator.VK_6);
#endif
            }
            else
            {
                DebugPrintLine("VirtualKeyCode not, complete the table you lazy git");
            }
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