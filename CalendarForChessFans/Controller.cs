
namespace CalendarForChessFans
{
    public class Controller
    {
        private Spectre.Console.Calendar cl = new Spectre.Console.Calendar(new DateTime(2025 - 5));
        private TxtFormating tf = new TxtFormating();
        private EventStoring es = new EventStoring();
        //private ChessApi ca = new ChessApi();
        private View v = new View();
        private int year = int.MaxValue;
        private int month = int.MaxValue;
        private Dictionary<string, Action> actions;
        public List<Event> events = new List<Event>();

        public Controller()
        {
            actions = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase)
            {
                ["exit"] = exit,
                ["day"] = DayInput,
                ["change date"] = GetStarted,
                ["create event"] = createEvent,
                ["help"] = tutorial,
                ["open event"] = checkoutEvent,
            };
        }
        public void exit()
        {
            EventStoring es = new EventStoring();
            es.SaveEvents(events);
            Environment.Exit(0);
        }
        public void getOut()
        {
            Console.WriteLine(tf.CenterText("Press any key to continue."));
            Console.ReadKey();
            Console.Clear();
        }
        public void tutorial()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(tf.CenterText("Wanna change month or year? Just write 'change date'!"));
            Console.WriteLine(tf.CenterText("Wanna see a specific day? Just write 'day'!"));
            Console.WriteLine(tf.CenterText("Wanna add an event? Just write 'create event'!"));
            Console.WriteLine(tf.CenterText("Wanna revisit this tutorial? Just write 'help'!"));
            Console.WriteLine(tf.CenterText("Wanna see an event you created? Just write 'open event'!"));
            Console.WriteLine(tf.CenterText("Wanna exit from app? Just write 'exit'."));
            tf.resetColors();
            getOut();
        }
        public void checkoutEvent()
        {
            Console.Clear();
            if (events.Count > 0)
            {
                while (true)
                {
                    tf.TextToAnimateWave(tf.CenterText("Write the title of the event you want to see: "));
                    string input = tf.ReadCenteredInput();
                    if (CheckForKeyWords(input, true)) UserShowroom();
                    Console.Clear();
                    Event ev = events.FirstOrDefault(e => string.Equals(e.Title, input.Trim(), StringComparison.OrdinalIgnoreCase));
                    if (ev != null)
                    {
                        v.Event(ev);
                        break;
                    }
                    else
                    {
                        tf.warning("Event not found. Please check the title and try again.");
                    }
                    getOut();
                }
            }
            else
            {
                Console.Clear();
                tf.warning("No events available to display.");
            }
        }
        public void welcome()
        {

            try
            {
                var evs = es.LoadEvents();
                events = (evs == null) ? events : evs;
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Notifications();
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
        public void Notifications()
        {
            v.Notifications(events);
            getOut();
        }
        public async Task UserShowroom()
        {
            try
            {
                if (year != int.MaxValue && month != int.MaxValue)
                {
                    while (true)
                    {
                        tf.TextToAnimateWave(tf.CenterText("Write chosen command: "));
                        //ca.FetchFideEventsAsync().Wait();
                        CheckForKeyWords(tf.ReadCenteredInput());
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
            events.Add(v.CreateEvent());
            MonthInput(true);
        }
        public void DayInput()
        {
            try
            {
                string input;
                int day;

                Console.WriteLine(tf.CenterText("Write the days number below"));
                input = tf.ReadCenteredInput();
                if (CheckForKeyWords(input, true)) UserShowroom();
                day = int.Parse(input);
                v.UpdateEvents(events, cl);

                Console.Clear();
                int daysInMonth = DateTime.DaysInMonth(cl.Year, cl.Month);
                if (!(day > daysInMonth))
                {
                    tf.FullyClearConsole();
                    v.Day(new DateTime(cl.Year, cl.Month, day));
                }
                else
                {
                    Console.Clear();
                    tf.warning($"bruh, do you see any {day} on the month calendar??");
                    UserShowroom();
                }
                getOut();
                tf.FullyClearConsole();
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
                if (clear) Console.Clear();
                string input;
                tf.TextToAnimateWave(tf.CenterText("Please enter the year and month below in this format: year.month"));
                input = tf.ReadCenteredInput();
                if (CheckForKeyWords(input, true)) UserShowroom();
                string[] dateList = input.Trim().Split('.');

                month = int.Parse(dateList[1]);
                year = int.Parse(dateList[0]);

                if (!(month > 12))
                {
                    cl = new Spectre.Console.Calendar(year, month);
                    v.UpdateEvents(events, cl);
                    v.Month(cl);
                }
                else
                {
                    tf.warning("year has 12 months..");
                    MonthInput(true);
                }
                getOut();
            }
            catch (Exception ex)
            {
                Console.Clear();
                tf.warning("Invalid input");
                MonthInput();
            }
        }
        public bool CheckForKeyWords(string txt, bool ignoreWar = false)
        {
            bool isCalled = false;
            if (actions.TryGetValue(txt.Trim().ToLower(), out Action method))
            {
                method();
                return true;
            }
            else
            {
                if (!ignoreWar) tf.warning("Wrong spelling?");
            }
            return isCalled;
        }
    }
}
