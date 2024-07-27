using System.Runtime.InteropServices;

class Program
{
    [DllImport("user32.dll", SetLastError = true)]
    static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs, int cbSize);

    [StructLayout(LayoutKind.Sequential)]
    struct INPUT
    {
        public int type;
        public InputUnion u;
    }

    [StructLayout(LayoutKind.Explicit)]
    struct InputUnion
    {
        [FieldOffset(0)] public MOUSEINPUT mi;
        [FieldOffset(0)] public KEYBDINPUT ki;
        [FieldOffset(0)] public HARDWAREINPUT hi;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public uint mouseData;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct KEYBDINPUT
    {
        public ushort wVk;
        public ushort wScan;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct HARDWAREINPUT
    {
        public uint uMsg;
        public ushort wParamL;
        public ushort wParamH;
    }

    const int INPUT_MOUSE = 0;
    const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
    const uint MOUSEEVENTF_LEFTUP = 0x0004;

    static void Main()
    {
        while (true)
        {
            // Sol fare tuşuna bas
            MouseClick();

            // Bir sonraki tıklamadan önce 2 saniye bekle
            Thread.Sleep(2000);
        }
    }

    static void MouseClick()
    {
        INPUT[] inputs = new INPUT[2];

        // Sol fare tuşuna bas
        inputs[0].type = INPUT_MOUSE;
        inputs[0].u.mi = new MOUSEINPUT { dwFlags = MOUSEEVENTF_LEFTDOWN };

        // Sol fare tuşunu bırak
        inputs[1].type = INPUT_MOUSE;
        inputs[1].u.mi = new MOUSEINPUT { dwFlags = MOUSEEVENTF_LEFTUP };

        SendInput(2, inputs, Marshal.SizeOf(typeof(INPUT)));
    }
}