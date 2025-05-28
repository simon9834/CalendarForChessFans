using Microsoft.VisualBasic;
using Spectre.Console;
using System.Numerics;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CalendarForChessFans
{
    public class EventHandler
    {
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
        public void createEvent()
        {
            //Event ev = new Event();
            //implement logic for interaction w user to create an event
        }
    }
}
