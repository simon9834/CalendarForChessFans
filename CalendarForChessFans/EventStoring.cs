using Newtonsoft.Json;

namespace CalendarForChessFans
{
    public class EventStoring
    {
        private const string filepath = "events.json";
        public void SaveEvents(List<Event> events)
        {
            var json = JsonConvert.SerializeObject(events, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filepath, json);
        }
        public List<Event> LoadEvents()
        {
            try
            {
                string json = File.ReadAllText(filepath);
                List<Event> objs = JsonConvert.DeserializeObject<List<Event>>(json);
                return objs;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading events: {ex.Message}");
                return new List<Event>();
            }
        }
    }
}
