using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication9
{
    public class Screen
    {

        public static DisplayInfoCollection Displays { get; private set; }


        static Screen()
        {
            GetDisplays();
        }
        /// <summary>
        /// Returns the number of Displays using the Win32 functions
        /// </summary>
        /// <returns>collection of Display Info</returns>
        private static void GetDisplays()
        {
            Displays = new DisplayInfoCollection();

            EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero,
                delegate (IntPtr hMonitor, IntPtr hdcMonitor, ref Rect lprcMonitor, IntPtr dwData)
                {
                    MonitorInfo mi = new MonitorInfo();
                    mi.Size = Marshal.SizeOf(mi);
                    bool success = GetMonitorInfo(hMonitor, ref mi);

                    if (success)
                    {
                        DisplayInfo di = new DisplayInfo();
                        di.ScreenWidth = (mi.Monitor.Right - mi.Monitor.Left).ToString();
                        di.ScreenHeight = (mi.Monitor.Bottom - mi.Monitor.Top).ToString();
                        di.MonitorArea = mi.Monitor;
                        di.WorkArea = mi.Work;
                        di.Availability = mi.Flags.ToString();
                        Displays.Add(di);
                    }
                    return true;
                }, IntPtr.Zero);
        }

        public static Size TotalVirtualArea
        {
            get
            {
                var bounds = VirtualBounds;
                return new Size(bounds.Right - bounds.Left, bounds.Bottom - bounds.Top);
            }
        }

        public static Rect VirtualBounds
        {
            get
            {
                int top = Displays.Min(d => d.MonitorArea.Top);
                int left = Displays.Min(d => d.MonitorArea.Left);
                int right = Displays.Max(d => d.MonitorArea.Right);
                int bottom = Displays.Max(d => d.MonitorArea.Bottom);
                return new Rect() { Top = top, Bottom = bottom, Right = right, Left = left };
            }
        }

        [DllImport("user32.dll")]
        static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumDelegate lpfnEnum, IntPtr dwData);
        [DllImport("user32.dll")]
        static extern bool GetMonitorInfo(IntPtr hMonitor, ref MonitorInfo lpmi);

        delegate bool MonitorEnumDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref Rect lprcMonitor, IntPtr dwData);
    }
}
