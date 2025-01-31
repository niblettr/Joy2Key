using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

class Program
{
    // Import necessary methods
    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool RegisterRawInputDevices(RAWINPUTDEVICE[] pRawInputDevices, uint uiNumDevices, uint cbSize);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern uint GetRawInputData(IntPtr hRawInput, uint uiCommand, IntPtr pData, ref uint pcbSize, uint cbSizeHeader);

    [DllImport("user32.dll")]
    public static extern IntPtr GetMessageExtraInfo();

    // Constants for Raw Input
    const uint RIM_TYPEKEYBOARD = 1;
    const uint RID_INPUT = 0x10000003;
    const uint RIM_INPUT = 0x10000003;

    // Define RawInput structure
    [StructLayout(LayoutKind.Sequential)]
    public struct RAWINPUTDEVICE
    {
        public ushort usUsagePage;
        public ushort usUsage;
        public uint dwFlags;
        public IntPtr hwndTarget;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RAWINPUT
    {
        public uint dwType;
        public RAWKEYBOARD keyboard;
        // Other members, not used here
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RAWKEYBOARD
    {
        public ushort MakeCode;
        public ushort Flags;
        public ushort VKey;
        public uint Message;
        public uint ExtraInformation;
    }

    // Main form to receive RawInput events
    public class RawInputForm : Form
    {
        public RawInputForm()
        {
            RegisterDevices();
        }

        // Register devices to capture raw keyboard input
        private void RegisterDevices()
        {
            RAWINPUTDEVICE[] devices = new RAWINPUTDEVICE[1];
            devices[0].usUsagePage = 0x01;  // Keyboard
            devices[0].usUsage = 0x06;      // Keyboard
            devices[0].dwFlags = 0;
            devices[0].hwndTarget = this.Handle;

            RegisterRawInputDevices(devices, (uint)devices.Length, (uint)Marshal.SizeOf(typeof(RAWINPUTDEVICE)));
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x00FF) // WM_INPUT message
            {
                ProcessRawInput(m.LParam);
            }

            base.WndProc(ref m);
        }

        // Process raw input data
        private void ProcessRawInput(IntPtr lParam)
        {
            uint dwSize = 0;
            GetRawInputData(lParam, RID_INPUT, IntPtr.Zero, ref dwSize, (uint)Marshal.SizeOf(typeof(RAWINPUT)));
            IntPtr buffer = Marshal.AllocHGlobal((int)dwSize);
            GetRawInputData(lParam, RID_INPUT, buffer, ref dwSize, (uint)Marshal.SizeOf(typeof(RAWINPUT)));

            RAWINPUT rawInput = (RAWINPUT)Marshal.PtrToStructure(buffer, typeof(RAWINPUT));
            if (rawInput.dwType == RIM_TYPEKEYBOARD)
            {
                // Handle raw keyboard input
                Console.WriteLine($"Key: {rawInput.keyboard.VKey}");
            }

            Marshal.FreeHGlobal(buffer);
        }

        static void Main()
        {
            Application.Run(new RawInputForm());
        }
    }
}
