
//using Spectre.Console;
using Microsoft.VisualBasic;
using System.Drawing;

namespace CalendarForChessFans
{
    public class Event
    {
        public string Title { get; set; }
        public DateTime? Date { get; set; }
        public bool isMoreDays { get; set; }
        public DateTime? DateOptStart { get; set; }
        public DateTime? DateOptEnd { get; set; }
        public TimeOnly? Start { get; set; }
        public TimeOnly? End { get; set; }
        public string? Location { get; set; }
        public string? Notes { get; set; }
        public string? RepeatCycle { get; set; }
        public Color color { get; set; }

        public Event(string title, DateTime? date, bool isMoreDays, DateTime? dateOptStart, DateTime? dateOptEnd, TimeOnly? start, TimeOnly? end, string? location, string? notes, string? repeats, Color color)
        {
            handleCtorAction(title, date, isMoreDays, dateOptStart, dateOptEnd, start, end, location, notes, repeats);
            this.color = color;
        }
        public Event(string title, DateTime? date, bool isMoreDays, DateTime dateOptStart, DateTime dateOptEnd, TimeOnly start, TimeOnly end, string location, string notes, string repeats)
        {
            handleCtorAction(title, date, isMoreDays, dateOptStart, dateOptEnd, start, end, location, notes, repeats);
            this.color = Color.Blue;
        }
        public void handleCtorAction(string title, DateTime? date, bool isMoreDays, DateTime? dateOptStart, DateTime? dateOptEnd, TimeOnly? start, TimeOnly? end, string? location, string? notes, string? repeats)
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
