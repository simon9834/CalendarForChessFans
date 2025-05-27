using CalendarForChessFans;
using Spectre.Console;

var Calendar = new Calendar(2024, 1);

Color color = Color.Blue3;
Calendar.HighlightStyle(Style.Parse(color.ToString()));
Calendar.AddCalendarEvent(2024, 1, 1);
color = Color.Red;
Calendar.HighlightStyle(Style.Parse(color.ToString()));
Calendar.AddCalendarEvent(2024, 1, 1);
AnsiConsole.Write(Calendar);
Console.WriteLine("aha");

Console.WriteLine();
Console.WriteLine();

TextAnimation ta = new TextAnimation();
await Task.Run(() => ta.TextToAnimateWave("SSS", ConsoleColor.Magenta, ConsoleColor.Red));

Day day = new Day();
day.CreateDaySchedule("tuesday 13");

TxtFormating tf = new TxtFormating();
Console.WriteLine(tf.CenterText("hey m"));


