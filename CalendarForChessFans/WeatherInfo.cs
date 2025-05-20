
namespace CalendarForChessFans
{
    public class WeatherInfo
    {
        private readonly string apiKey = "key";
        private readonly HttpClient httpClient = new HttpClient();

        /*public async Task<WeatherInfo> GetWeatherAsync(string location)
        {
            var response = await httpClient.GetStringAsync(
                $"https://api.openweathermap.org/data/2.5/weather?q={location}&appid={apiKey}&units=metric");
            return JsonConvert.DeserializeObject<WeatherInfo>(response);
        }*/

    }
}
