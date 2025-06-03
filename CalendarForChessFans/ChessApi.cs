
using HtmlAgilityPack;
using System.Text.Json;


namespace CalendarForChessFans
{
    public class ChessApi
    {

        public async Task FetchFideEventsAsync()
        {
            var url = "https://www.fide.com/calendar";
            var httpClient = new HttpClient();

            try
            {
                var html = await httpClient.GetStringAsync(url);

                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                // Find the table with events
                var rows = doc.DocumentNode.SelectNodes("//table//tr");

                if (rows != null)
                {
                    Console.WriteLine("Upcoming FIDE Chess Events:\n");

                    foreach (var row in rows)
                    {
                        var columns = row.SelectNodes("td");
                        if (columns != null && columns.Count >= 3)
                        {
                            var date = columns[0].InnerText.Trim();
                            var name = columns[1].InnerText.Trim();
                            var location = columns[2].InnerText.Trim();

                            Console.WriteLine($"📅 {date} — {name} ({location})");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Could not find event table.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching FIDE events: " + ex.Message);
            }
        }
    }
}
