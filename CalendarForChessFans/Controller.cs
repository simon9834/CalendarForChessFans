

using Spectre.Console;

namespace CalendarForChessFans
{
    public class Controller
    {
        private View v = new View();
        private Calendar cl;
        private TxtFormating tf = new TxtFormating();
        private int year = int.MaxValue;
        private int month = int.MaxValue;
        private Dictionary<string, Action> actions;
        public Controller()
        {
            actions = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase)
            {
                ["exit"] = () => Environment.Exit(0),
                ["month"] = UserShowroom,
                ["change"] = GetStarted,
            };
        }

        public void welcome()
        {
            Console.WriteLine(tf.CenterText("Hello!"));
            Console.WriteLine(tf.CenterText("To exit from app, just type 'exit' and tap enter."));
            Console.WriteLine(tf.CenterText("To see month, type 'month' and tap enter."));
            Console.WriteLine(tf.CenterText("To add change month or year type 'change' and hit enter."));
            Console.WriteLine();
            GetStarted();
        }
        public void GetStarted()
        {
            try
            {
                string input;
                Console.WriteLine(tf.CenterText("To get started please enter the year and month below in this format: year, month"));
                input = Console.ReadLine();
                CheckForKeyWords(input);
                string[] dateList = input.Trim().Split(',');
                month = int.Parse(dateList[1]);
                year = int.Parse(dateList[0]);
                Console.Clear();
                UserShowroom();
            }
            catch (IndexOutOfRangeException ex)
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
                string input;
                if (year != int.MaxValue && month != int.MaxValue)
                {
                    MonthInput(month, year);
                    Console.WriteLine(tf.CenterText("Wanna see a specific day? just write the number below!"));
                    input = Console.ReadLine();
                    CheckForKeyWords(input);
                    DayInput(int.Parse(input));
                }
            }catch (IndexOutOfRangeException ex)
            {
                Console.Clear();
                tf.warning("bruh, maybe a wrong day?");
                UserShowroom();
            }
        }
        public void createEvent()
        {
            //a logic for the class eventHandler and its method to create event;
        }
        public void change()
        {
            //changes monnth or year
        }
        public void DayInput(int day)
        {
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
        public void MonthInput(int month, int year)
        {
            if (!(month > 12))
            {
                cl = new Calendar(year, month);
                v.Month(cl);
            }
            else
            {
                Console.WriteLine("bruh year has 12 months..");
                welcome();
            }
        }
        public void CheckForKeyWords(string txt)
        {
            if (actions.TryGetValue(txt, out Action method))
            {
                method();
            }
        }
    }
}
