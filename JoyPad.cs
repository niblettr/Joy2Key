using SharpDX.XInput;
using System.Threading;
using System.Windows.Forms;

namespace JoyKey
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Initializes the XInput controllers.
        /// </summary>
        void InitXInputs()
        {
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


    }
}

