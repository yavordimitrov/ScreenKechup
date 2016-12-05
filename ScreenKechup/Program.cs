using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication9
{
    class Program
    {

        static void Main(string[] args)
        {
            ArgsParse parse = new ArgsParse(args);
            if (parse.Action == Action.List)
            {
                List();
            }
            if (parse.Action == Action.Take)
            {
                ScreenShot.Create("random.png", parse.Options.Size, parse.Options.Offset);
            }
            Console.Read();
        }

        private static void List()
        {
            int i = 0;
            foreach (var display in Screen.Displays)
            {

                string primary = "";
                if (display.Availability == "1")
                {
                    primary = "(Defult)";
                }
                Console.WriteLine($"Display {i}{primary}, {display.ScreenWidth}x{display.ScreenHeight} at (X:{display.MonitorArea.Left}, Y:({display.MonitorArea.Top})");
                    i++;
            }
        }


    }
}
