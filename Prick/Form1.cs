using System.Runtime.InteropServices;
using System.Diagnostics;
using SharpDX.XInput;
using static System.Windows.Forms.AxHost;
using WindowsInput.Native;
using WindowsInput;
using System.Windows.Forms;

namespace Prick
{
    public partial class Form1 : Form
    {
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        private const int KEYEVENTF_KEYUP = 0x0002;

        int GearPosition;
        bool connected = false;
        Controller MyController;
        InputSimulator inputSimulator;

        public Form1()
        {
            InitializeComponent();
            InitXInputs();
            GearPosition = 1;

            Thread loopThread = new Thread(PollJoystickXinput);
            loopThread.Start();
        }

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

        public void DebugPrintLine(string text)
        {
            DebugPrint(text + "\n");
        }


        // convert gear to key
        public void SpoofKeypress(int gear)
        {
            String AppName = AppName_textBox.Text;

            IntPtr AppPtr = FindWindow(null, AppName); // Sega Rally Championship (Rev B) // Model 2 Emulator

            Process p = Process.GetProcessesByName("notepad").FirstOrDefault();

            if (p != null)
            {
                IntPtr h = p.MainWindowHandle;
                SetForegroundWindow(h);

                DebugPrintLine("\"" + AppName + "\" found/running");
                string key = string.Empty;

                switch (gear)
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

                DebugPrintLine(">>>>>>>>>>>>>>>>Send key: '" + key + "'");

                SendKeys.SendWait(key);


                //Process p = Process.GetProcessesByName("Sega Rally Championship (Rev B)").FirstOrDefault();
                //if (p != null)
                //{
                //    IntPtr h = p.MainWindowHandle;
                //    SetForegroundWindow(h);
                //    SendKeys.SendWait("6");
                //}



                //inputSimulator = new InputSimulator();
                //inputSimulator.Keyboard.KeyDown(VirtualKeyCode.VK_6);

                //  keybd_event(key, 0, 0, UIntPtr.Zero); // Key down
                //  keybd_event(key, 0, KEYEVENTF_KEYUP, UIntPtr.Zero); // Key up
            }
            else
            {
                DebugPrintLine("ERROR: \"" + AppName + "\" NOT found/running");
            }
        }





        public void PollJoystickXinput()
        {
            // Poll events from joystick
            while (MyController.IsConnected)
            {
                var previousState = MyController.GetState();

                while (MyController.IsConnected)
                {
                    if (IsKeyPressed(ConsoleKey.Escape))
                    {
                        DebugPrintLine("escape pressed");
                        return;
                    }

                    var MyControllerState = MyController.GetState();

                    if (previousState.PacketNumber != MyControllerState.PacketNumber)
                    {
                        HandleGamepadDataIn(MyControllerState);
                    }

                    Thread.Sleep(10);
                    previousState = MyControllerState;
                }

                DebugPrintLine("Controller disconnected");
                Thread.Sleep(1000); // Wait before trying to reconnect
            }
        }
        void HandleGamepadDataIn(SharpDX.XInput.State MyControllerState)
        {
            //DebugPrintLine($"PacketNumber : {MyControllerState.PacketNumber.ToString()}");
            DebugPrintLine(MyControllerState.Gamepad.ToString());

            if (MyControllerState.Gamepad.Buttons.HasFlag(GamepadButtonFlags.RightShoulder))
            {
                ChangeGear(true);
            }
            else if (MyControllerState.Gamepad.Buttons.HasFlag(GamepadButtonFlags.LeftShoulder))
            {
                ChangeGear(false);
            }
        }

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


        public void ChangeGear(bool dir)
        {
            if (dir) // up
            {
                if (++GearPosition > 4)
                {
                    GearPosition = 4; // clamp
                }

            }
            else // down
            {
                if (--GearPosition < 1)
                {
                    GearPosition = 1; // clamp
                }
            }
            DebugPrintLine($"Gear position : {GearPosition.ToString()}");

            SpoofKeypress(GearPosition);
        }



        public static bool IsKeyPressed(ConsoleKey key)
        {
            return false;
            //return Console.KeyAvailable && Console.ReadKey(true).Key == key;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }
    }

}
