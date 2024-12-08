using System.Runtime.InteropServices;
using System.Diagnostics;
using SharpDX.XInput;
using static System.Windows.Forms.AxHost;

namespace Prick
{
    public partial class Form1 : Form
    {
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        int GearPosition;
        bool connected = false;
        Controller MyController;
        public Form1()
        {
            InitializeComponent();
            InitXInputs();
            GearPosition = 1;

            Thread loopThread = new Thread(PollJoystickXinput);
            loopThread.Start();
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
                        Debug.WriteLine("escape pressed");
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

                Debug.WriteLine("Controller disconnected");
                Thread.Sleep(1000); // Wait before trying to reconnect
            }
        }
        void HandleGamepadDataIn(SharpDX.XInput.State MyControllerState)
        {
            //Debug.WriteLine($"PacketNumber : {MyControllerState.PacketNumber.ToString()}");
            //Debug.WriteLine(MyControllerState.Gamepad); // prints everything

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
            Debug.WriteLine("Start XGamepadApp");

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
                Debug.WriteLine("No XInput controller installed");
            }
            else
            {
                Debug.WriteLine("XInput controller found..");
            }

            Debug.WriteLine("Poll events from controller..");
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
            Debug.WriteLine($"Gear position : {GearPosition.ToString()}");
        }

        public static bool IsKeyPressed(ConsoleKey key)
        {
            return false;
            //return Console.KeyAvailable && Console.ReadKey(true).Key == key;
        }

        //public void loop()
        //{
        //    Poll events from joystick
        //    while (MyController.IsConnected)
        //    {
        //        var previousState = MyController.GetState();

        //        if (IsKeyPressed(ConsoleKey.Escape))
        //        {
        //            Console.WriteLine("escape pressed");
        //            break;
        //        }

        //        var state = MyController.GetState();

        //        if (previousState.PacketNumber != state.PacketNumber)
        //        {
        //            Console.WriteLine($"PacketNumber : {state.PacketNumber.ToString()}");
        //            Console.WriteLine(state.Gamepad); // prints everything

        //            Console.WriteLine(state.Gamepad.Buttons);

        //            IntPtr Model2Emu = FindWindow(null, "Model 2 Emulator"); // emulator

        //            if (SetForegroundWindow(Model2Emu))
        //            {
        //                Console.WriteLine("emulator found");
        //                SendKeys.SendWait("10{+}10=");   //<<<<<<<<<<<<<<<<<<<<<<<< CUNT CUNT CUNT CUNT PRICKS.
        //            }
        //            else
        //            {
        //                Console.WriteLine("emulator NOT found");
        //            }

        //        }

        //        Thread.Sleep(100);
        //        previousState = state;
        //    }
        //}
    }

}
