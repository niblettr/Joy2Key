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

        // P/Invoke declarations for the Windows API functions
        [DllImport("user32.dll", SetLastError = true)]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        // Virtual Key codes
        public const byte VK_A = 0x41; // A key
        public const byte VK_UP = 0x26; // Up Arrow key
        public const byte VK_DOWN = 0x28; // Down Arrow key

        public const byte VK_6 = 0x36; // 6 key

        // Key event flags
        public const uint KEYEVENTF_KEYDOWN = 0x0000; // Key down event
        public const uint KEYEVENTF_KEYUP = 0x0002;   // Key up event




        public void SendKey_InputSimulator(VirtualKeyCode keyCode)
        {
            //inputSimulator = new InputSimulator();
            //inputSimulator.Keyboard.KeyDown(keyCode);
            //int delay = Convert.ToInt32(KeyHoldTime_TextBox.Text);
            //Thread.Sleep(GetKeyHoldTime());
            //inputSimulator.Keyboard.KeyUp(keyCode);

            // Simulate pressing and releasing the 'A' key
            SimulateKeyPress(VK_6);
            Thread.Sleep(100); // Sleep for 100ms
            SimulateKeyRelease(VK_6);

            // Simulate pressing the Up Arrow key
            //SimulateKeyPress(VK_UP);
            //Thread.Sleep(100);
            //SimulateKeyRelease(VK_UP);

            // Simulate pressing the Down Arrow key
            //SimulateKeyPress(VK_DOWN);
            //Thread.Sleep(100);
            //SimulateKeyRelease(VK_DOWN);


        }

        // Method to simulate a key press
        public static void SimulateKeyPress(byte virtualKey)
        {
            keybd_event(virtualKey, 0, KEYEVENTF_KEYDOWN, 0);
        }

        // Method to simulate a key release
        public static void SimulateKeyRelease(byte virtualKey)
        {
            keybd_event(virtualKey, 0, KEYEVENTF_KEYUP, 0);
        }
    }
}
