using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication9
{
    public class ArgsParse
    {
        public TakeOptions Options { get; private set; }

        public Action Action { get; set; }

        List<string> _args;

        public ArgsParse(string[] args)
        {
            _args = args.ToList();
            Parse();
        }

        private void Parse()
        {
            int index = _args.FindIndex(a => a == "-l");
            if(index == 0)
            {
                Action = Action.List;
                Options = null;
                return;
            }

            int takeIndex = _args.FindIndex(a => a == "-t");
            if (takeIndex == 0)
            {
                Action = Action.Take;
                Options = TakeOptions.Default;
                return;
            }

            Action = Action.Unknown;
        }
    }

    public class TakeOptions
    {
        public Size Size {get;set;}
        public Rect Offset { get; set; }
        public static TakeOptions Default { get { return new TakeOptions() { Size = Screen.TotalVirtualArea, Offset = Screen.VirtualBounds }; } }
    }

    public enum Action
    {
        Unknown,
        List,
        Take
    }
}
