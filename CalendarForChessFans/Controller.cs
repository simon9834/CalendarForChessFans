

using Spectre.Console;

namespace CalendarForChessFans
{
    public class Controller
    {
        // Debug hours and formating in events in day. Create exporting mechanics and loading back
        private Calendar cl;
        private TxtFormating tf = new TxtFormating();
        private int year = int.MaxValue;
        private int month = int.MaxValue;
        private Dictionary<string, Action> actions;
        public List<Event> events = new List<Event>();
        private View v = new View();
        public Controller()
        {
            actions = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase)
            {
                ["exit"] = () => Environment.Exit(0),
                ["day"] = DayInput,
                ["change"] = GetStarted,
                ["create event"] = createEvent,
                ["tutorial"] = tutorial,
            };
        }
        public void tutorial()
        {
            Console.WriteLine(tf.CenterText("Wanna change month or year? Just write 'change'!"));
            Console.WriteLine(tf.CenterText("Wanna see a specific day? Just write 'day'!"));
            Console.WriteLine(tf.CenterText("Wanna add an event? Just write 'create event'!"));
            Console.WriteLine(tf.CenterText("Wanna revisit this tutorial? Just write 'tutorial'!"));
            Console.WriteLine(tf.CenterText("Wanna exit from app? Just write 'exit'."));
        }
        public void welcome()
        {
            Console.WriteLine(tf.CenterText("Hello!"));
            Console.WriteLine();
            tutorial();
            Console.WriteLine();
            GetStarted();
        }
        public void GetStarted()
        {
            try
            {
                MonthInput();
                UserShowroom();
            }
            catch (Exception ex)
            {
                Console.Clear();
                tf.warning("not quite the right format");
                welcome();
            }
        }
        public void UserShowroom()
        {
            try
            {
                if (year != int.MaxValue && month != int.MaxValue)
                {
                    while (true)
                    {
                        Console.Write("Write chosen command: ");
                        CheckForKeyWords(Console.ReadLine());
                    }
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.Clear();
                tf.warning("Wrong input?");
                UserShowroom();
            }
        }
        public void createEvent()
        {
            EventHandler eh = new EventHandler();
            events.Add(eh.createEvent());
            MonthInput(true);
        }
        public void DayInput()
        {
            try
            {
                Console.Clear();
                string input;
                int day;

                Console.WriteLine(tf.CenterText("Write the days number below"));
                input = Console.ReadLine();
                day = int.Parse(input);
                v.UpdateEvents(events, cl);

                Console.Clear();
                int daysInMonth = DateTime.DaysInMonth(cl.Year, cl.Month);
                if (!(day > daysInMonth))
                {
                    v.Day(new DateTime(cl.Year, cl.Month, day));
                }
                else
                {
                    Console.Clear();
                    tf.warning($"bruh, do you see any {day} on the month calendar??");
                    UserShowroom();
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                tf.warning("Wrong spelling");
                DayInput();
            }
        }

        public void MonthInput(bool clear = false)
        {
            try
            {
                if(clear) Console.Clear();
                string input;
                Console.WriteLine(tf.CenterText("Please enter the year and month below in this format: year.month"));
                input = Console.ReadLine();
                string[] dateList = input.Trim().Split('.');

                month = int.Parse(dateList[1]);
                year = int.Parse(dateList[0]);

                if (!(month > 12))
                {
                    cl = new Calendar(year, month);
                    v.UpdateEvents(events, cl);
                    v.Month(cl);
                }
                else
                {
                    Console.WriteLine("bruh year has 12 months..");
                    MonthInput(true);
                }
            }catch (Exception ex)
            {
                Console.Clear();
                tf.warning("Invalid input");
                MonthInput(true);
            }
        }
        public void CheckForKeyWords(string txt)
        {
            if (actions.TryGetValue(txt.Trim().ToLower(), out Action method))
            {
                method();
            }
            else
            {
                tf.warning("Wrong spelling?");
            }
        }
    }
}
