using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarForChessFans
{
    public class Day
    {
        //public string name;
        public void CreateDaySchedule(string title)
        {
            string line = new string('-', Console.WindowWidth - 6);
            Console.BufferHeight = 1000;
            spaceBarTop();
            AnsiConsole.Write(new FigletText(FigletFont.Default, title));
            spaceBarTop();

            int iHolder = 0;
            writeHours(ref iHolder, 12);
            writeLines(line, 43, false);
            writeHours(ref iHolder, 25);
            writeLines(line, 52, true);

        }
        public void writeLines(string line, int height, bool onemore)
        {
            Console.SetCursorPosition(5, Console.CursorTop - height);
            int max = 24;
            if (onemore) max++;
            for (int i = 0; i < max; i++)
            {
                if ((i % 2 == 0 || i == 0))
                {
                    Console.Write(line);
                    Console.SetCursorPosition(5, Console.CursorTop + 1);
                }
                else
                {
                    if (i != max - 1)
                    {
                        Console.SetCursorPosition(5, Console.CursorTop + 1);
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }
            }
        }
        public void writeHours(ref int iHolder, int max)
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            for (int i = iHolder; i < max; i++)
            {
                if (i > 9)
                {
                    Console.WriteLine($"{i}\n");
                }
                else
                {
                    Console.WriteLine($"0{i}\n");
                }
                if (i == (max - 1))
                {
                    iHolder = i + 1;
                }
            }
        }
        public void spaceBarTop() { for (int i = 0; i < 2; i++) Console.WriteLine(); }
    }
}
