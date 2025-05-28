
using Spectre.Console;

namespace CalendarForChessFans
{
    public class View
    {
        private EventStoring es = new EventStoring();
        private EventHandler eh = new EventHandler();
        private List<Event> events = new List<Event>();
        private Calendar cr;
        public void loadEventsIntoCalendar()
        {
            events = es.LoadEvents();
        }
        public void UpdateEvents()
        {
            eh.HighlightEvents(events, cr);
        }

        public void Month(Calendar cr)
        {
            this.cr = cr;
            UpdateEvents();
            Console.Clear();
            AnsiConsole.Write(cr);
        }

        public void Day(DateTime date)
        {
            Day d = new Day();
            d.CreateDayScheduleWEvents(date.DayOfWeek.ToString(), events, date);
        }
    }
}
