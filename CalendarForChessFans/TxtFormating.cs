using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarForChessFans
{
    public class TxtFormating
    {
        public string CenterText(string text)
        {
            int spaces = 0;
            spaces = (Console.WindowWidth - text.Length) / 2;
            return text.PadLeft(spaces);
        }
        public void warning(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(CenterText(text));
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void TextToAnimateWave(string text, ConsoleColor highlightCol, ConsoleColor finalCol)
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
                    Task.Delay(100).Wait();
                }
                else
                {
                    Task.Delay(50).Wait();
                }

                Console.ForegroundColor = finalCol;
                Console.SetCursorPosition(i, Console.CursorTop);
                Console.Write(char.ToLower(charArr[i]));
            }
            Console.ForegroundColor = ConsoleColor.White;
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
    }
}

