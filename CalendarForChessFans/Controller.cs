

using Spectre.Console;

namespace CalendarForChessFans
{
    public class Controller
    {
        private View v = new View();
        private Calendar cl;
        
        public void welcome()
        {

        }
        public void UserShowroom()
        {

        }

        public void DayInput(int day)
        {
            int daysInMonth = DateTime.DaysInMonth(cl.Year, cl.Month);
            if(!(day > daysInMonth)){
                v.Day(new DateTime(cl.Year, cl.Month, day));
            }
            Console.WriteLine($"bruh do you see any {day} on the month calendar??");
        }
        public void MonthInput(int month)
        {
            if (!(month > 12))
            {
                cl = new Calendar(2024, month);
                v.Month(cl);
            }
            else
            {
                Console.WriteLine("bruh year has 12 months..");
            }
        }
    }
}
