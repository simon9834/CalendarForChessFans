
using System.Text;

namespace CalendarForChessFans
{
    public class TxtFormating
    {
        public string CenterText(string text)
        {
            int totalWidth = Console.WindowWidth;
            int spacesBefore = Math.Max((totalWidth - text.Length) / 2, 0);
            string padded = new string(' ', spacesBefore) + text;

            if (padded.Length < totalWidth)
            {
                padded = padded.PadRight(totalWidth);
            }
            else if (padded.Length > totalWidth)
            {
                padded = padded.Substring(0, totalWidth);
            }

            return padded;
        }
        public void warning(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(CenterText(text));
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void TextToAnimateWave(string text, ConsoleColor highlightCol = ConsoleColor.DarkRed, ConsoleColor finalCol = ConsoleColor.Yellow)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(text);
            char[] charArr = text.ToCharArray();
            int indexOfFirstLetter = Array.FindIndex(charArr, c => !char.IsWhiteSpace(c));
            int maxIndex = Array.FindLastIndex(charArr, c => !char.IsWhiteSpace(c));

            for (int i = indexOfFirstLetter; i <= maxIndex; i++)
            {
                Console.ForegroundColor = highlightCol;
                Console.SetCursorPosition(i, Console.CursorTop);
                Console.Write(char.ToUpper(charArr[i]));

                Task.Delay((charArr[i] == ' ') ? 100 : 50).Wait();

                Console.ForegroundColor = finalCol;
                Console.SetCursorPosition(i, Console.CursorTop);
                Console.Write(char.ToLower(charArr[i]));
            }
            resetColors();
            Console.CursorVisible = true;
            Console.WriteLine();
        }
        public void FullyClearConsole()
        {
            Console.Clear();

            int bufferHeight = Console.BufferHeight;
            int bufferWidth = Console.BufferWidth;

            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < bufferHeight; i++)
            {
                Console.WriteLine(new string(' ', bufferWidth));
            }
            Console.SetCursorPosition(0, 0);
        }
        public void resetColors()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public string ReadCenteredInput()
        {
            try
            {
                Console.CursorVisible = false;
                StringBuilder input = new StringBuilder();
                ConsoleKeyInfo key;

                int top = Console.CursorTop;

                while (true)
                {
                    key = Console.ReadKey(intercept: true);

                    if (key.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine();
                        break;
                    }
                    else if (key.Key == ConsoleKey.Backspace)
                    {
                        if (input.Length > 0)
                        {
                            input.Remove(input.Length - 1, 1);
                        }
                    }
                    else if (!char.IsControl(key.KeyChar))
                    {
                        input.Append(key.KeyChar);
                    }

                    // Re-center the text
                    Console.SetCursorPosition(0, top);
                    Console.Write(new string(' ', Console.WindowWidth));

                    int left = Math.Max((Console.WindowWidth - input.Length) / 2, 0);
                    Console.SetCursorPosition(left, top);
                    Console.Write(input.ToString());
                }
                Console.CursorVisible = false;
                return input.ToString();

            }
            catch (Exception ex)
            {
                warning("Smth went wrong here bro: " + ex.Message);
                return string.Empty;
            }
        }
    }
}

