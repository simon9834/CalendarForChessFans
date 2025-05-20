using CalendarForChessFans;
using Spectre.Console;

var Calendar = new Calendar(2024, 1);
AnsiConsole.Write(Calendar);
Console.WriteLine("aha");

Console.WriteLine();
Console.WriteLine();

TextAnimation ta = new TextAnimation();
await Task.Run(() => ta.TextToAnimateWave("SSS", ConsoleColor.Magenta, ConsoleColor.Red));


