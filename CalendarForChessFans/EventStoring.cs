using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Xml;
using Newtonsoft.Json;

namespace CalendarForChessFans
{
    public class EventStoring
    {
        private const string filepath = "filepath mega";
        public void SaveEvents(List<Event> events)
        {
            var json = JsonConvert.SerializeObject(events, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filepath, json);
        }

        public List<Event> LoadEvents()
        {
            if (!File.Exists(filepath)) return new List<Event>();
            var json = File.ReadAllText(filepath);
            return JsonConvert.DeserializeObject<List<Event>>(json); //might be null, handle
        }
    }
}
