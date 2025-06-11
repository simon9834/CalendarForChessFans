using Spectre.Console;

namespace CalendarForChessFans
{
    public class Day
    {
        private TxtFormating tf = new TxtFormating();
        private List<Event> li = new List<Event>();
        private bool goes = false;
        public void CreateDayScheduleWEvents(string title, List<Event> events, DateTime date)
        {
            li = recreateListByDay(date, events);
            Console.BufferHeight = 1000;
            string line = new string('-', Console.WindowWidth - 2);

            spaceBarTop();
            AnsiConsole.Write(new FigletText(FigletFont.Default, title).Centered());
            spaceBarTop();

            int iHolder = 0;
            int index = 0;
            writeHours(ref iHolder, 12, li, ref index);
            writeLines(line, 24, false);
            writeHours(ref iHolder, 25, li, ref index);
            writeLines(line, 26, true);
            
        }
        public void writeLines(string line, int height, bool onemore)
        {
            tf.resetColors();
            Console.SetCursorPosition(2, Console.CursorTop - height);
            int max = 24;
            if (onemore) max += 2;

            for (int i = 0; i < max; i += 2)
            {
                Console.Write(line);
                Console.SetCursorPosition(2, Console.CursorTop + 2);
            }
        }
        public void writeHours(ref int iHolder, int max, List<Event> li, ref int index)
        {
            int tempIndex = 0;
            bool started = false;
            string title = "THREE DAYS GRACE";
            if (goes)
            {
                started = true;
                title = li[index].Title;
            }
            Console.SetCursorPosition(0, Console.CursorTop);
            for (int i = iHolder; i < max; i++)
            {
                tf.resetColors();
                Console.WriteLine(i > 9 ? $"{i}" : $"0{i}");

                if ((tempIndex = li.FindIndex(element => element.Start == i)) != -1)
                {
                    index = tempIndex;
                    var value = li[index];
                    title = value.Title;
                    Console.BackgroundColor = value.color;
                    Console.ForegroundColor = OppositeColorOf(value.color);
                    started = !started;
                }

                if (started)
                {
                    var currentEvent = li[index];
                    if (currentEvent.End <= i)
                    {
                        started = false;
                    }
                    else
                    {
                        Console.BackgroundColor = currentEvent.color;
                        Console.ForegroundColor = OppositeColorOf(currentEvent.color);
                    }
                }

                if (i == (max - 1))
                {
                    iHolder = i + 1;
                }
                Console.WriteLine(started ? tf.CenterText(title) : "");
                tf.resetColors();
            }
            goes = started;
        }
        public void spaceBarTop() { for (int i = 0; i < 2; i++) Console.WriteLine(); }
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
                if (e.Date == date || e.DateOptStart == date) events.Add(e);
            }
            return events;
        }
    }
}
