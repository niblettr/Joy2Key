using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using WindowsInput.Native;
using WindowsInput;

namespace JoyKey
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Synthesizes a keystroke.
        /// </summary>
        /// <param name="bVk">The virtual-key code of the key.</param>
        /// <param name="bScan">The hardware scan code of the key.</param>
        /// <param name="dwFlags">Controls various aspects of function operation.</param>
        /// <param name="dwExtraInfo">An additional value associated with the keystroke.</param>
        [DllImport("user32.dll", SetLastError = true)]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        // Constants for keypress
        public const uint KEYEVENTF_KEYDOWN = 0x0000;
        public const uint KEYEVENTF_KEYUP = 0x0002;

        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>

        private static readonly Dictionary<string, VirtualKeyCode> GearToKeyCodeMap = new Dictionary<string, VirtualKeyCode>
        {
            { "I", VirtualKeyCode.VK_I },
            { "K", VirtualKeyCode.VK_K },
            { "O", VirtualKeyCode.VK_O },
            { "L", VirtualKeyCode.VK_L },
            { "6", VirtualKeyCode.VK_6 }
        };

        /// <summary>
        /// Sends a keystroke to the active window.
        /// </summary>
        /// <param name="KeyStroke">The key to send.</param>
        public void HandleVirtualKeystroke(string KeyStroke) // e.g. I K O L 
        {
            String AppName = AppName_textBox.Text;

            FindRunningApp(AppName, WindowSearchOptions.BringWindowToFront); // may not find app but send key press below anyway....

            if (GearToKeyCodeMap.TryGetValue(KeyStroke, out VirtualKeyCode keyCode))
            {
                //Keys keyData = (Keys)keyCode;

                DebugPrintLine(">>>>>>>>>>>>>>>> Send keystroke: '" + KeyStroke + "'");

                // the troublesome section.....
                // why are ascci charaters being sent? Should they not be keyboard scan code?

                SendKey_InputSimulator(keyCode); // does not work
                //SendKey_kbdEventMethod(0x36); // does not work either...
                //SendKey_SendInputMethod(); // SendMethod implementation (doesn't work on directx either)
            }
            else
            {
                DebugPrintLine("VirtualKeyCode not, complete the table you lazy git");
            }
        }

        //experimental
        public void SendKey_kbdEventMethod(byte key)
        {
            // Simulate key down
           // keybd_event(key, 0, KEYEVENTF_KEYDOWN, 0); // niblett
            Thread.Sleep(GetKeyHoldTime());
            // Simulate key up
           // keybd_event(key, 0, KEYEVENTF_KEYUP, 0); // niblett
        }

        public void SendKey_InputSimulator(VirtualKeyCode keyCode)
        {
            //inputSimulator = new InputSimulator();
            //inputSimulator.Keyboard.KeyDown(keyCode);
            //int delay = Convert.ToInt32(KeyHoldTime_TextBox.Text);
            //Thread.Sleep(GetKeyHoldTime());
            //inputSimulator.Keyboard.KeyUp(keyCode);
        }

        public void SendKey_SendInputMethod()
        {
          //  SendInput_KeySimulator.SendKeyPress(SendInput_KeySimulator.VK_6, this); // just send a 6 for now (coin insert) // niblett
        }
    }
}
