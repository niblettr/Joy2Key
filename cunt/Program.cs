using System;
using SharpDX.XInput;
using System.Threading;
using System.Runtime.InteropServices;


namespace cunt
{
    internal class Program
    {
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        static void Main(string[] args)
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
                Console.WriteLine("Found a XInput controller available");
                Console.WriteLine("Press buttons on the controller to display events or escape key to exit... ");

                // Poll events from joystick
                var previousState = MyController.GetState();

                while (MyController.IsConnected)
                {
                    if (IsKeyPressed(ConsoleKey.Escape))
                    {
                        break;
                    }
                    var state = MyController.GetState();

                    if (previousState.PacketNumber != state.PacketNumber)
                    {
                        //Console.WriteLine(state.Gamepad); // prints everything

                        Console.WriteLine(state.Gamepad.Buttons);

                        IntPtr calcWindow = FindWindow(null, "Calculator");

                        if (SetForegroundWindow(calcWindow))
                        {
                            Console.WriteLine("calcWindow found");
                            //SendKeys.Send("10{+}10=");   //<<<<<<<<<<<<<<<<<<<<<<<< CUNT CUNT CUNT CUNT PRICKS.
                        }
                        else
                        {
                            Console.WriteLine("calcWindow NOT found");
                        }

                    }

                    Thread.Sleep(10);
                    previousState = state;
                }
            }
            Console.WriteLine("End XGamepadApp");
        }

        public static bool IsKeyPressed(ConsoleKey key)
        {
            return false;
            //return Console.KeyAvailable && Console.ReadKey(true).Key == key;
        }
    }
}
