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






//        public class KeyboardHook
//        {
//            #region Imports

//            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
//            private static extern IntPtr SetWindowsHookEx(int idHook,
//                LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

//            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
//            [return: MarshalAs(UnmanagedType.Bool)]
//            private static extern bool UnhookWindowsHookEx(IntPtr hhk);

//            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
//            private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
//                IntPtr wParam, IntPtr lParam);

//            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
//            private static extern IntPtr GetModuleHandle(string lpModuleName);

//            #endregion

//            const int WH_KEYBOARD_LL = 13;
//            const int WM_KEYDOWN = 0x0100;
//            static LowLevelKeyboardProc m_proc = HookCallback;
//            static IntPtr m_hookID = IntPtr.Zero;
//            static Screenshot m_screen;

//            public static void Hook(Screenshot screenshotManager)
//            {
//                m_screen = screenshotManager;
//                m_hookID = SetHook(m_proc);
//            }

//            public static void Unhook()
//            {
//                UnhookWindowsHookEx(m_hookID);
//            }

//            private static IntPtr SetHook(LowLevelKeyboardProc proc)
//            {
//                using (Process curProcess = Process.GetCurrentProcess())
//                using (ProcessModule curModule = curProcess.MainModule)
//                {
//                    return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
//                        GetModuleHandle(curModule.ModuleName), 0);
//                }
//            }

//            private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

//            private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
//            {
//                if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
//                {
//                    int vkCode = Marshal.ReadInt32(lParam);

//#if DEBUG
//                    Debug.WriteLine(((System.Windows.Forms.Keys)vkCode).ToString());
//#endif
//                    if ((System.Windows.Forms.Keys)vkCode == m_screen.CaptureKey)
//                    {
//                        m_screen.CaptureSimple();
//                    }
//                }

//                return CallNextHookEx(m_hookID, nCode, wParam, lParam);
//            }
//        }













        /// <summary>
        /// Synthesizes a keystroke.
        /// </summary>
        /// <param name="bVk">The virtual-key code of the key.</param>
        /// <param name="bScan">The hardware scan code of the key.</param>
        /// <param name="dwFlags">Controls various aspects of function operation.</param>
        /// <param name="dwExtraInfo">An additional value associated with the keystroke.</param>
        [DllImport("user32.dll", SetLastError = true)]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        InputSimulator inputSimulator;

        // Constants for keypress
        public const uint KEYEVENTF_KEYDOWN = 0x0000;
        public const uint KEYEVENTF_KEYUP = 0x0002;

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
            bool BringToFront = BringToFront_CheckBox.Checked;
            if (BringToFront)
            {
                FindRunningApp(AppName, WindowSearchOptions.BringWindowToFront); // may not find app but send key press below anyway....
            }
            else
            {
                FindRunningApp(AppName, WindowSearchOptions.none); // may not find app but send key press below anyway....
            }

            if (GearToKeyCodeMap.TryGetValue(KeyStroke, out VirtualKeyCode keyCode))
            {
                DebugPrintLine(">>>>>>>>>>>>>>>> Send keystroke: '" + KeyStroke + "'");
                SendKey_InputSimulator(keyCode); // does not work
            }
            else
            {
                DebugPrintLine("VirtualKeyCode not, complete the table you lazy git");
            }
        }

        public void SendKey_InputSimulator(VirtualKeyCode keyCode)
        {
            inputSimulator = new InputSimulator();
            inputSimulator.Keyboard.KeyDown(keyCode);
            int delay = Convert.ToInt32(KeyHoldTime_TextBox.Text);
            Thread.Sleep(GetKeyHoldTime());
            inputSimulator.Keyboard.KeyUp(keyCode);
        }
    }
}
