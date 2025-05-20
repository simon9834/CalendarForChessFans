
//using Spectre.Console;
using System.Drawing;

namespace CalendarForChessFans
{
    public class Event
    {
        private string Title { get; set; }
        private DateOnly? Date { get; set; }
        private bool isMoreDays { get; set; }
        private DateOnly? DateOptStart { get; set; }
        private DateOnly? DateOptEnd { get; set; }
        private TimeOnly? Start { get; set; }
        private TimeOnly? End { get; set; }
        private string? Location { get; set; }
        private string? Notes { get; set; }
        private string? RepeatCycle { get; set; }
        private Color color { get; set; }

        public Event(string title, DateOnly? date, bool isMoreDays, DateOnly? dateOptStart, DateOnly? dateOptEnd, TimeOnly? start, TimeOnly? end, string? location, string? notes, string? repeats, Color color)
        {
            handleCtorAction(title, date, isMoreDays, dateOptStart, dateOptEnd, start, end, location, notes, repeats);
            this.color = color;
        }
        public Event(string title, DateOnly? date, bool isMoreDays, DateOnly dateOptStart, DateOnly dateOptEnd, TimeOnly start, TimeOnly end, string location, string notes, string repeats)
        {
            handleCtorAction(title, date, isMoreDays, dateOptStart, dateOptEnd, start, end, location, notes, repeats);
            this.color = Color.Blue;
        }
        public void handleCtorAction(string title, DateOnly? date, bool isMoreDays, DateOnly? dateOptStart, DateOnly? dateOptEnd, TimeOnly? start, TimeOnly? end, string? location, string? notes, string? repeats)
        {
            Title = title;
            RepeatCycle = repeats;

            if (isMoreDays)
            {
                this.isMoreDays = true;
                this.DateOptStart = dateOptStart;
                this.DateOptEnd = dateOptEnd;
                this.Date = null;
                this.Start = null;
                this.End = null;
            }
            else
            {
                this.isMoreDays = false;
                this.DateOptStart = null;
                this.DateOptEnd = null;
                this.Date = date;
                this.Start = start;
                this.End = end;
            }
            if (location == "" || location == null)
            {
                location = null;
            }
            else
            {
                this.Location = location;
            }
            if (notes == "" || notes == null)
            {
                notes = null;
            }
            else
            {
                this.Location = location;
            }
        }
    }
}
