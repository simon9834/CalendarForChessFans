using Spectre.Console;

namespace CalendarForChessFans
{
    public class Day
    {
        private List<Event> li = new List<Event>();
        public void CreateDayScheduleWEvents(string title, List<Event> events, DateTime date)
        {
            li = recreateListByDay(date, events);
            Console.Clear();
            Console.BufferHeight = 1000;
            string line = new string('-', Console.WindowWidth - 6);

            spaceBarTop();
            AnsiConsole.Write(new FigletText(FigletFont.Default, title).Centered());
            spaceBarTop();

            int iHolder = 0;
            int currTime = 0;
            writeHours(ref iHolder, 12, ref currTime, li);
            writeLines(line, 43, false);
            writeHours(ref iHolder, 25, ref currTime, li);
            writeLines(line, 52, true);
        }
        public void writeLines(string line, int height, bool onemore)
        {
            Console.SetCursorPosition(5, Console.CursorTop - height);
            int max = 24;
            if (onemore) max++;

            for (int i = 0; i < max; i++)
            {
                if (i % 2 == 0 || i == 0)
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
        public void writeHours(ref int iHolder, int max, ref int currTime, List<Event> li)
        {
            int time;
            int index;
            bool isFilled = false;
            bool started = false;
            string title = "THREE DAYS GRAZE";

            Console.SetCursorPosition(0, Console.CursorTop);
            for (int i = iHolder; i < max; i++)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = OppositeColorOf(ConsoleColor.Black);
                if (i > 9)
                {
                    Console.WriteLine($"{i}");
                }
                else
                {
                    Console.WriteLine($"0{i}");
                }

                time = currTime;

                if ((index = li.FindIndex(element => element.Start == time)) != -1)
                {
                    isFilled = true;
                    var value = li[index];
                    title = value.Title;
                    Console.BackgroundColor = value.color;
                    Console.ForegroundColor = OppositeColorOf(value.color);
                    if (started)
                    {
                        started = false;
                    }
                }

                if (started)
                {
                    Console.BackgroundColor = li[index].color;
                    Console.ForegroundColor = OppositeColorOf(li[index].color);
                    if (li[index].End == time)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = OppositeColorOf(ConsoleColor.Black);
                        started = false;
                    }
                }

                if (i == (max - 1))
                {
                    iHolder = i + 1;
                }

                if (!isFilled)
                {
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine(title);
                }

                currTime++;
            }
        }
        public void spaceBarTop() { for (int i = 0; i < 2; i++) Console.WriteLine(); }
        /*public void sortEventListByDate(List<Event> li) //completely useless but why not
        {
            li.Sort((a, b) =>
            {
                if (a == null && b == null) return 0;
                if (a == null) return 1;
                if (b == null) return -1;
                return (int)a.Start - (int)b.Start;
            });
        }*/
        public ConsoleColor OppositeColorOf(ConsoleColor color)
        {
            return color switch
            {
                ConsoleColor.Black => ConsoleColor.White,
                ConsoleColor.White => ConsoleColor.Black,
                ConsoleColor.DarkBlue => ConsoleColor.Yellow,
                ConsoleColor.Blue => ConsoleColor.Yellow,
                ConsoleColor.DarkGreen => ConsoleColor.Magenta,
                ConsoleColor.Green => ConsoleColor.Magenta,
                ConsoleColor.DarkCyan => ConsoleColor.Red,
                ConsoleColor.Cyan => ConsoleColor.Red,
                ConsoleColor.DarkRed => ConsoleColor.Cyan,
                ConsoleColor.Red => ConsoleColor.Cyan,
                ConsoleColor.DarkMagenta => ConsoleColor.Green,
                ConsoleColor.Magenta => ConsoleColor.Green,
                ConsoleColor.DarkYellow => ConsoleColor.Blue,
                ConsoleColor.Yellow => ConsoleColor.DarkBlue,
                ConsoleColor.Gray => ConsoleColor.Black,
                ConsoleColor.DarkGray => ConsoleColor.White,
                _ => ConsoleColor.Black
            };
        }
        public List<Event> recreateListByDay(DateTime date, List<Event> li)
        {
            List<Event> events = new List<Event>();
            foreach (Event e in li)
            {
                if(e.Date == date || e.DateOptStart == date) events.Add(e);
            }
            return events;
        }
    }
}
