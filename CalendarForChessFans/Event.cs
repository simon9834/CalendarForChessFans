
using Newtonsoft.Json;

namespace CalendarForChessFans
{
    public class Event
    {
        public string Title { get; set; }
        public DateTime? Date { get; set; }
        public bool isMoreDays { get; set; }
        public DateTime? DateOptStart { get; set; }
        public DateTime? DateOptEnd { get; set; }
        public enum LABEL
        {
            chessMeetup,
            chessTournament,
            chessTraining,
            chessEvent,
            nonChessEvent,
        }
        public LABEL label { get; set; } = LABEL.nonChessEvent;
        public int? Start { get; set; }
        public int? End { get; set; }
        public string? Location { get; set; }
        public string? Notes { get; set; }
        public ConsoleColor color { get; set; }

        [JsonConstructor]
        public Event(string title, DateTime? date, bool isMoreDays, DateTime? dateOptStart, DateTime? dateOptEnd, int? start, int? end, string? location, string? notes,LABEL label, ConsoleColor color)
        {
            handleCtorAction(title, date, isMoreDays, dateOptStart, dateOptEnd, start, end, location, notes, label);
            this.color = color;
        }
        public Event(string title, DateTime? date, bool isMoreDays, DateTime dateOptStart, DateTime dateOptEnd, int start, int end, string? location, string? notes, LABEL label)
        {
            handleCtorAction(title, date, isMoreDays, dateOptStart, dateOptEnd, start, end, location, notes, label);
            this.color = ConsoleColor.Blue;
        }
        public void handleCtorAction(string title, DateTime? date, bool isMoreDays, DateTime? dateOptStart, DateTime? dateOptEnd, int? start, int? end, string? location, string? notes, LABEL label)
        {
            label = label;
            Title = title;
            this.isMoreDays = isMoreDays;
            if (isMoreDays)
            {
                this.DateOptStart = dateOptStart;
                this.DateOptEnd = dateOptEnd;
                this.Date = null;
                this.Start = null;
                this.End = null;
            }
            else
            {
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
                Notes = notes;
            }
        }
    }
}
