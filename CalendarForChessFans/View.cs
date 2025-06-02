
using Spectre.Console;

namespace CalendarForChessFans
{
    public class View
    {
        private EventStoring es = new EventStoring();
        private EventHandler eh = new EventHandler();
        private List<Event> events = new List<Event>();
        public void loadEventsIntoCalendar()
        {
            events = es.LoadEvents();
        }
        public void UpdateEvents(List<Event> events, Calendar cr)
        {
            this.events = events;
            eh.HighlightEvents(events, cr);
        }
        public void Event(Event ev)
        {
            eh.DisplayEvent(ev);
        }

        public void Month(Calendar cr)
        {
            AnsiConsole.Write(cr);
        }

        public void Day(DateTime date)
        {
            Day d = new Day();
            d.CreateDayScheduleWEvents(date.DayOfWeek.ToString(), events, date);
        }
    }
}
