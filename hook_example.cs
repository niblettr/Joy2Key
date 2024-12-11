public class KeyboardHook
{
    #region Imports

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook,
        LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
        IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    #endregion

    const int WH_KEYBOARD_LL = 13;
    const int WM_KEYDOWN = 0x0100;
    static LowLevelKeyboardProc m_proc = HookCallback;
    static IntPtr m_hookID = IntPtr.Zero;
    static Screenshot m_screen;

    public static void Hook(Screenshot screenshotManager)
    {
        m_screen = screenshotManager;
        m_hookID = SetHook(m_proc);
    }

    public static void Unhook()
    {
        UnhookWindowsHookEx(m_hookID);
    }

    private static IntPtr SetHook(LowLevelKeyboardProc proc)
    {
        using (Process curProcess = Process.GetCurrentProcess())
        using (ProcessModule curModule = curProcess.MainModule)
        {
            return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                GetModuleHandle(curModule.ModuleName), 0);
        }
    }

    private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
        {
            int vkCode = Marshal.ReadInt32(lParam);

#if DEBUG
            Debug.WriteLine(((System.Windows.Forms.Keys)vkCode).ToString());
#endif
            if ((System.Windows.Forms.Keys)vkCode == m_screen.CaptureKey)
            {
                m_screen.CaptureSimple();
            }
        }

        return CallNextHookEx(m_hookID, nCode, wParam, lParam);
    }
}    