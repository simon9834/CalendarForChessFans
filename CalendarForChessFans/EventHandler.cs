using Spectre.Console;
using System.Numerics;
using System.Reflection.Emit;

namespace CalendarForChessFans
{
    public class EventHandler
    {
        private TxtFormating tf = new TxtFormating();
        private Dictionary<DateTime, int> eventCounter = new Dictionary<DateTime, int>();
        public void HighlightEvents(List<Event> l, Calendar cr)
        {
            eventCounter = CreateDictForEventCount(l);
            foreach (Event e in l)
            {
                if (e.Date != null)
                {
                    cr.HighlightStyle = Style.Parse(NumToColor(eventCounter[(DateTime)e.Date]).ToString());
                    cr.AddCalendarEvent((DateTime)e.Date);
                }
                else if (e.DateOptStart != null && e.DateOptEnd != null)
                {
                    for (DateTime date = (DateTime)e.DateOptStart; date <= e.DateOptEnd; date = date.AddDays(1))
                    {
                        cr.HighlightStyle = Style.Parse(ConsoleColor.Green.ToString());
                        cr.AddCalendarEvent(date);
                    }
                }
            }
        }
        public Dictionary<DateTime, int> CreateDictForEventCount(List<Event> l)
        {
            Dictionary<DateTime, int> counts = new Dictionary<DateTime, int>();

            foreach (var evnt in l)
            {
                if (evnt.Date != null)
                {
                    DateTime eventDate = (DateTime)evnt.Date;

                    if (counts.ContainsKey(eventDate))
                    {
                        counts[eventDate]++;
                    }
                    else
                    {
                        counts[eventDate] = 1;
                    }
                }
            }
            return counts;
        }
        public ConsoleColor NumToColor(int num)
        {
            var ColorList = new List<ConsoleColor>
            {
                    ConsoleColor.White,
                    ConsoleColor.Gray,
                    ConsoleColor.Yellow,
                    ConsoleColor.Cyan,
                    ConsoleColor.Magenta,
                    ConsoleColor.DarkGray,
                    ConsoleColor.DarkYellow,
                    ConsoleColor.DarkCyan,
                    ConsoleColor.DarkMagenta,
                    ConsoleColor.Blue,
                    ConsoleColor.DarkBlue,
                    ConsoleColor.Black
            };

            if (num < ColorList.Count)
                return ColorList[num];
            else
                return ConsoleColor.Red;
        }
        public Event createEvent()
        {
            Event ev;
            string frequency;
            string title;
            bool isMoreDays;
            Event.LABEL label = Event.LABEL.nonChessEvent;
            DateTime date = default;
            DateTime dateOptStart = default;
            DateTime dateOptEnd = default;
            int start = int.MaxValue;
            int end = int.MaxValue;
            string location = null;
            string notes = null;
            string repeats;
            ConsoleColor color;

            title = animateAndCheck(tf, "Enter the title for the event");
            isMoreDays = yesNo(animateAndCheck(tf, "Should this event be for more days? Answers: yes, no", "yes", "no"));
            if (isMoreDays)
            {
                tryDate("Enter the starting date of the event, for example: 2025-5-1", ref dateOptStart, tf);
                tryDate("Enter the ending date of the event, for example: 2025-5-2", ref dateOptEnd, tf, dateOptStart);
            }
            else
            {
                tryDate("Enter the date of the event, for example: 2025-5-1", ref date, tf);
                tryTime("Enter the starting hour of the event, for example: 7, 22, 18", ref start, tf);
                tryTime("Enter the ending hour of the event, for example: 8, 23, 19", ref end, tf, start);
            }
            if (yesNo(animateAndCheck(tf, "Do you want to add notes? Answers: yes, no", "yes", "no")))
            {
                notes = animateAndCheck(tf, "Enter your notes");
            }
            if (yesNo(animateAndCheck(tf, "Do you want to add a location? Answers: yes, no")))
            {
                location = animateAndCheck(tf, "Enter your location");
            }
            if(yesNo(animateAndCheck(tf, "Is this a chess event? Answers: yes, no")))
            {
                label = assignLabel();
            }
            ev = new Event(title, date, isMoreDays, dateOptStart, dateOptEnd, start, end, location, notes, label);
            return ev;
        }
        public Event.LABEL assignLabel()
        {
            while (true)
            {
                Console.WriteLine(tf.TextToAnimateWave("Please choose a label that fits your event"));
                string input;
                foreach (Event.LABEL l in Enum.GetValues(typeof(Event.LABEL)))
                {
                    Console.WriteLine(tf.CenterText($"{(int)l} - {l}"));
                }
                input = tf.ReadCenteredInput();
                if (int.TryParse(input, out int selectedValue) && Enum.IsDefined(typeof(Event.LABEL), selectedValue))
                {
                    Event.LABEL selectedTitle = (Event.LABEL)selectedValue;
                    return selectedTitle;
                }
            }
        }
        public void tryDate(string textShown, ref DateTime obj, TxtFormating tf, DateTime start = default)
        {

            Console.Clear();
            string input;
            string[] parts;
            while (true)
            {
                try
                {
                    input = animateAndCheck(tf, textShown);
                    parts = input.Trim().Split('-');
                    if (parts.Length == 3)
                    {
                        int.TryParse(parts[0], out int year);
                        int.TryParse(parts[1], out int month);
                        int.TryParse(parts[2], out int day);

                        obj = new DateTime(year, month, day);
                        if (start != default && obj < start)
                        {
                            Console.Clear();
                            tf.warning("Please enter a date that is after the starting date.");
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    tf.warning("Invalid date format. Please use the format: year-month-day");
                }
            }
        }
        public void tryTime(string textShown, ref int obj, TxtFormating tf, int start = int.MinValue)
        {
            Console.Clear();
            while (true)
            {
                if (int.TryParse(animateAndCheck(tf, textShown), out obj))
                {
                    if (start != int.MinValue && obj <= start)
                    {
                        Console.Clear();
                        tf.warning($"Please enter a value greater than {start}.");
                        continue;
                    }
                    else if (obj > 23)
                    {
                        Console.Clear();
                        tf.warning($"A day has 24 hours at best...");
                        continue;
                    }
                    break;
                }
                else
                {
                    Console.Clear();
                    tf.warning("Invalid input. Please enter a number.");
                }
            }
        }
        private string animateAndCheck(TxtFormating tf, string txt, string wantedText1 = "tralalelotralala", string wantedText2 = "prprpatapim")
        {
            string outpt;
            Console.Clear();
            while (true)
            {
                tf.TextToAnimateWave(tf.CenterText(txt), ConsoleColor.Red, ConsoleColor.Yellow);
                outpt = tf.ReadCenteredInput();

                if (wantedText1 != "tralalelotralala")
                {
                    if (outpt.Trim().ToLower() != wantedText1 && outpt.Trim().ToLower() != wantedText2)
                    {
                        Console.Clear();
                        tf.warning("Please enter valid data.");
                        continue;
                    }
                }

                if (outpt == null)
                {
                    Console.Clear();
                    tf.warning("Invalid input. Please try again.");
                }
                else
                {
                    return outpt;
                }
            }
        }
        private bool yesNo(string text)
        {
            if (text.Trim().ToLower() == "yes")
            {
                return true;
            }
            if (text.Trim().ToLower() == "no")
            {
                return false;
            }
            return false;
        }
        public void DisplayEvent(Event ev)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(tf.CenterText("╔══════════════════════════════╗"));
            Console.WriteLine(tf.CenterText("║         EVENT DETAILS        ║"));
            Console.WriteLine(tf.CenterText("╚══════════════════════════════╝"));
            Console.ForegroundColor = ev.color;
            AnsiConsole.Write(new FigletText(FigletFont.Default, ev.Title).Centered());
            tf.resetColors();
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            if (ev.isMoreDays)
            {
                Console.WriteLine(tf.CenterText($"From: {ev.DateOptStart:yyyy-MM-dd}"));
                Console.WriteLine(tf.CenterText($"To:   {ev.DateOptEnd:yyyy-MM-dd}"));
            }
            else
            {
                Console.WriteLine(tf.CenterText($"Date:  {ev.Date:yyyy-MM-dd}"));
                Console.WriteLine(tf.CenterText($"Time:  {ev.Start}:00 - {ev.End}:00"));
            }
            if (!string.IsNullOrWhiteSpace(ev.Location))
            {
                Console.WriteLine(tf.CenterText($"Location: {ev.Location}"));
            }

            if (!string.IsNullOrWhiteSpace(ev.Notes))
            {
                Console.WriteLine(tf.CenterText($"Notes: {ev.Notes}"));
            }

            tf.resetColors();
            Console.WriteLine();
            Console.WriteLine(tf.CenterText("[Press any key to return]"));
            Console.ReadKey();
            Console.CursorVisible = true;
        }
        public void displayNotifications(List<Event> events)
        {
            TxtFormating tf = new TxtFormating();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            DateTime today = DateTime.Today;
            bool anyEvent = false;
            Console.WriteLine(tf.CenterText(""));
            Console.WriteLine(tf.CenterText("╔══════════════════════════════╗"));
            Console.WriteLine(tf.CenterText("║         Notifications        ║"));
            Console.WriteLine(tf.CenterText("╚══════════════════════════════╝"));
            Console.WriteLine(tf.CenterText(""));
            foreach (Event e in events)
            {
                if (today == e.Date)
                {
                    Console.WriteLine(tf.CenterText($"Upcoming event: {e.Title}! From {e.Start}{amPm((int)e.Start)} till {e.End}{amPm((int)e.End)}!"));
                    anyEvent = true;
                }
            }

            if (!anyEvent)
            {
                Console.WriteLine(tf.CenterText("No events planned for today."));
            }
            Console.WriteLine(tf.CenterText(""));
            tf.resetColors();
        }
        public string amPm(int hour)
        {
            return (hour < 12) ? "AM" : "PM";
        }
    }
}
