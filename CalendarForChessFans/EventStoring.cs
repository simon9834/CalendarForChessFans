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
            string afterWrite = File.ReadAllText(filepath);
        }
        public List<Event> LoadEvents()
        {
            try
            {
                if (!File.Exists(filepath))
                {
                    File.WriteAllText(filepath, "[]");
                }
                string json = File.ReadAllText(filepath);
                List<Event> objs = JsonConvert.DeserializeObject<List<Event>>(json);
                return objs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Event>();
            }
        }
    }
}
