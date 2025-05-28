using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarForChessFans
{
    public class TxtFormating
    {
        public string CenterText(string text)
        {
            int spaces = 0;
            spaces = (Console.WindowWidth - text.Length) / 2;
            return text.PadLeft(spaces);
        }
        public void warning(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(CenterText(text));
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
