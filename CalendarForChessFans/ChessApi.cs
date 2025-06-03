
using HtmlAgilityPack;
using System.Text.Json;


namespace CalendarForChessFans
{
    public class ChessApi
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task PrintMostRecentGameAsync(TxtFormating tf)
        {
            client.DefaultRequestHeaders.UserAgent.ParseAdd("MyChessApp/1.0 (+https://example.com)");
            string username = "magnuscarlsen"; // fixed user

            try
            {
                string archivesUrl = $"https://api.chess.com/pub/player/{username}/games/archives";
                var archivesResponse = await client.GetStringAsync(archivesUrl);
                using var archivesDoc = JsonDocument.Parse(archivesResponse);
                var archives = archivesDoc.RootElement.GetProperty("archives");

                if (archives.GetArrayLength() == 0)
                {
                    Console.WriteLine("No archives found.");
                    return;
                }

                string latestArchiveUrl = archives[archives.GetArrayLength() - 1].GetString();

                var gamesResponse = await client.GetStringAsync(latestArchiveUrl);
                using var gamesDoc = JsonDocument.Parse(gamesResponse);
                var games = gamesDoc.RootElement.GetProperty("games");

                if (games.GetArrayLength() == 0)
                {
                    Console.WriteLine("No games found in the latest archive.");
                    return;
                }

                var recentGame = games[games.GetArrayLength() - 1];

                var white = recentGame.GetProperty("white");
                var black = recentGame.GetProperty("black");
                var whiteUsername = white.GetProperty("username").GetString();
                var blackUsername = black.GetProperty("username").GetString();

                string result = white.GetProperty("result").GetString();
                string timeControl = recentGame.GetProperty("time_control").GetString();

                string addedSec = "";
                string[] parts = timeControl.Split('+');
                int time = int.Parse(parts[0]);
                time /= 60;
                if (parts.Length != 1)
                {
                    addedSec = " + " + parts[1] + " seconds";
                }

                Console.SetCursorPosition(0, Console.CursorTop + 20);
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;

                Console.WriteLine(tf.CenterText($"Most recent game for {username}:"));
                Console.WriteLine(tf.CenterText($"White: {whiteUsername}"));
                Console.WriteLine(tf.CenterText($"Black: {blackUsername}"));
                Console.WriteLine(tf.CenterText($"Result (white's perspective): {result}"));
                Console.WriteLine(tf.CenterText($"Time control: {time} minutes {addedSec}"));

                Console.SetCursorPosition(0, Console.CursorTop - 23);
                tf.resetColors();
            }
            catch (Exception ex)
            {
                tf.warning($"Error fetching data: {ex.Message}");
            }
        }
    }
}
