using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Frydia.Utils
{
    public class ProcessUtils
    {
        [DllImport("user32.dll")] static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
        [DllImport("user32.dll")] static extern bool IsWindowVisible(IntPtr hWnd);
        [DllImport("user32.dll")] static extern bool GetWindowRect(IntPtr hWnd, out RECT rect);
        [DllImport("dwmapi.dll")] static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out RECT pvAttribute, int cbAttribute);
        [DllImport("user32.dll")] static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);
        [DllImport("shcore.dll")] static extern int GetDpiForMonitor(IntPtr hmonitor, int dpiType, out uint dpiX, out uint dpiY);
        [DllImport("user32.dll")] public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        private const int DWMWA_EXTENDED_FRAME_BOUNDS = 9;

        public struct RECT { public int Left, Top, Right, Bottom; }

        // Retourne { hWnd -> Rectangle } pour toutes les fenêtres d'un process
        public static HashSet<IntPtr> GetAllWindows(string processName)
        {
            var result = new HashSet<IntPtr>();
            var pids = Process.GetProcessesByName(processName).Select(p => (uint)p.Id).ToHashSet();

            EnumWindows((hWnd, _) =>
            {
                GetWindowThreadProcessId(hWnd, out uint pid);
                if (pids.Contains(pid) && IsWindowVisible(hWnd)) result.Add(hWnd);
                return true;
            }, IntPtr.Zero);

            return result;
        }

        public static Rectangle GetBounds(IntPtr hWnd)
        {
            int hr = DwmGetWindowAttribute(hWnd, DWMWA_EXTENDED_FRAME_BOUNDS, out RECT r, Marshal.SizeOf<RECT>());

            if (hr != 0) GetWindowRect(hWnd, out r);

            int azfs = DwmGetWindowAttribute(hWnd, DWMWA_EXTENDED_FRAME_BOUNDS, out RECT raw, Marshal.SizeOf<RECT>());
            GetWindowRect(hWnd, out raw);

            // Récupère le DPI du monitor où est la fenêtre
            IntPtr monitor = MonitorFromWindow(hWnd, 0x2); // MONITOR_DEFAULTTONEAREST
            GetDpiForMonitor(monitor, 0, out uint dpiX, out uint dpiY);

            float scale = dpiX / 96f;
            return new Rectangle((int)(r.Left / scale), (int)(r.Top / scale), (int)((r.Right - r.Left) / scale), (int)((r.Bottom - r.Top) / scale));
        }
    }
}
