
namespace CalendarForChessFans
{
    public class TextAnimation
    {
        private readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);
        public async Task TextToAnimateWave(string text, ConsoleColor highlightCol, ConsoleColor finalCol)
        {
            await _lock.WaitAsync();
            try
            {
                Console.CursorVisible = false;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(text);
                char[] charArr = text.ToCharArray();
                for (int i = 0; i < text.Length; i++)
                {
                    Console.ForegroundColor = highlightCol;
                    Console.SetCursorPosition(i, Console.CursorTop);
                    Console.Write(char.ToUpper(charArr[i]));
                    
                    if (charArr[i] == ' ')
                    {
                        Task.Delay(200).Wait();
                    }
                    else
                    {
                        Task.Delay(50).Wait();
                    }

                        Console.ForegroundColor = finalCol;
                    Console.SetCursorPosition(i, Console.CursorTop);
                    Console.Write(char.ToLower(charArr[i]));
                }
            }
            finally
            {
                Console.ForegroundColor= ConsoleColor.White;
                Console.CursorVisible=true;
                _lock.Release();
            }
        }
    }
}
