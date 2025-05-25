using Microsoft.VisualBasic;
using Spectre.Console;

namespace CalendarForChessFans
{
    public class EventHandler
    {
        public void loadEvents(List<Event> l, Calendar cr)
        {
            foreach (Event e in l)
            {
                //cr.HighlightStyle(Style.Parse(e.color.ToString())); zmenit styl dle poctu ukolu ve dni
                if(e.Date != null)
                {
                    cr.AddCalendarEvent((DateTime)e.Date);
                }
                else if (e.DateOptStart != null && e.DateOptEnd != null)
                {
                    for (DateTime date = (DateTime)e.DateOptStart; date <= e.DateOptEnd; date = date.AddDays(1))
                    {
                        cr.AddCalendarEvent(date);
                    }
                }
            }
        }
    }
}
