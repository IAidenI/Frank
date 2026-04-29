namespace Frydia.Utils
{
    public class ProcessWatcher
    {
        public string ProcessName { get; }
        public Action<IntPtr, Rectangle> OnWindowFound { get; }
        public Action<IntPtr> OnWindowLost { get; }

        public Dictionary<IntPtr, Rectangle> KnownWindows { get; } = new();
        public HashSet<IntPtr> PendingWindows { get; } = new();

        public ProcessWatcher(string processName, Action<IntPtr, Rectangle> onFound, Action<IntPtr> onLost)
        {
            ProcessName = processName;
            OnWindowFound = onFound;
            OnWindowLost = onLost;
        }

        public void RefreshFoundWindows()
        {
            foreach (var hWnd in KnownWindows.Keys.ToList())
            {
                OnWindowFound(hWnd, KnownWindows[hWnd]);
            }
        }

        public void AcknowledgeCurrentWindows()
        {
            foreach (var hWnd in ProcessUtils.GetAllWindows(ProcessName))
            {
                var bounds = ProcessUtils.GetBounds(hWnd);

                KnownWindows[hWnd] = bounds;
                PendingWindows.Remove(hWnd);

                OnWindowFound(hWnd, bounds);
            }
        }
    }
}
