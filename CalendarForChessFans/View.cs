
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
        public Event CreateEvent()
        {
            return eh.createEvent();
        }
        public void Event(Event ev)
        {
            eh.DisplayEvent(ev);
        }
        public void Notifications(List<Event> events)
        {
            eh.displayNotifications(events);
        }

        public void Month(Calendar cr)
        {
            AnsiConsole.Write(new Panel(cr)
                       .Border(BoxBorder.Rounded)
                       .Padding(2, 1)
                       .Expand());
        }

        public void Day(DateTime date)
        {
            Day d = new Day();
            d.CreateDayScheduleWEvents(date.DayOfWeek.ToString(), events, date);
        }
    }
}
