using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;
using WindowsInput;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Joy2Key
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
#if true
                // works for calculator but not Model2 emulator <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                SendKey_inputSimulatorMethod(keyCode);
#else

                
                SendKeybdEvent(0x36); // does not work either...

                // works for calculator but not Model2 emulator <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                //KeySimulator.SendKeyPress(KeySimulator.VK_6);
#endif
            }
            else
            {
                DebugPrintLine("VirtualKeyCode not, complete the table you lazy git");
            }
        }

        //experimental
        public static void SendKey_kbdEventMethod(byte key)
        {
            // Simulate key down
            keybd_event(key, 0, KEYEVENTF_KEYDOWN, 0);
            Thread.Sleep(150);
            // Simulate key up
            keybd_event(key, 0, KEYEVENTF_KEYUP, 0);
        }

        public void SendKey_inputSimulatorMethod(VirtualKeyCode keyCode)
        {
            inputSimulator = new InputSimulator();
            inputSimulator.Keyboard.KeyDown(keyCode);
            Thread.Sleep(150);
            inputSimulator.Keyboard.KeyUp(keyCode);
        }
    }
}
