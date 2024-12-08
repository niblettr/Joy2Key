using System.Runtime.InteropServices;
using SharpDX.XInput;

namespace Prick
{
    public partial class Form1 : Form
    {
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        int GearPosition;
        public Form1()
        {
            InitializeComponent();

            GearPosition = 1;
            loop();
        }

        public void ChangeGear(bool dir)
        {
            if (dir) // up
            {
                if (GearPosition++ > 4)
                {
                    GearPosition = 4; // clamp
                }

            }
            else // down
            {
                if (GearPosition-- < 1)
                {
                    GearPosition = 1; // clamp
                }
            }
            Console.WriteLine($"Gear position : {GearPosition.ToString()}");
        }

        public void loop()
        {
            
            Console.WriteLine("Start XGamepadApp");

            // Initialize XInput
            var controllers = new[] { new Controller(UserIndex.One), new Controller(UserIndex.Two), new Controller(UserIndex.Three), new Controller(UserIndex.Four) };

            // Get 1st controller available
            Controller MyController = null;

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
                Console.WriteLine("No XInput controller installed");
            }
            else
            {
                Console.WriteLine("XInput controller found..");
            }

            Console.WriteLine("Poll events from controller..");


            // Poll events from joystick
            while (MyController.IsConnected)
            {
                var previousState = MyController.GetState();

                if (IsKeyPressed(ConsoleKey.Escape))
                {
                    Console.WriteLine("escape pressed");
                    break;
                }
                var state = MyController.GetState();

                if (previousState.PacketNumber != state.PacketNumber)
                {
                    Console.WriteLine($"PacketNumber : {state.PacketNumber.ToString()}");
                    Console.WriteLine(state.Gamepad); // prints everything

                    //Console.WriteLine(state.Gamepad.Buttons);

                    //IntPtr Model2Emu = FindWindow(null, "Model 2 Emulator"); // emulator

                    //if (SetForegroundWindow(Model2Emu))
                    //{
                    //    Console.WriteLine("emulator found");
                    //    SendKeys.SendWait("10{+}10=");   //<<<<<<<<<<<<<<<<<<<<<<<< CUNT CUNT CUNT CUNT PRICKS.
                    //}
                    //else
                    //{
                    //    Console.WriteLine("emulator NOT found");
                    //}

                }

                Thread.Sleep(10);
                previousState = state;
            }
        }

        public static bool IsKeyPressed(ConsoleKey key)
        {
            return Console.KeyAvailable && Console.ReadKey(true).Key == key;
        }
    }

}
