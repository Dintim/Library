using Library.Cons.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Cons
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.Write(": ");
            //bool x = Convert.ToBoolean(Int32.Parse(Console.ReadLine()));
            //Console.WriteLine(x);

            //Console.WriteLine("Отчеты");
            //Console.WriteLine("---------------------------------------------\n");
            //Console.WriteLine("1. Ежедневный отчет");
            //Console.WriteLine("2. Список должников");
            //Console.WriteLine("3. Список занятых книг");
            //Console.WriteLine("4. Выход");
            //Console.Write("Ваш выбор: ");
            //string choice = Console.ReadLine();
            //if (Char.IsNumber(choice[0]) && choice.Length == 1)
            //{
            //    if (choice[0] == '1')
            //        Console.WriteLine(1);
            //}
            //else
            //    Console.WriteLine("not");

            //Console.Write("День: ");
            //string day = Console.ReadLine();
            //Console.Write("Месяц: ");
            //string month = Console.ReadLine();
            //Console.Write("Год: ");
            //string year = Console.ReadLine();
            //string date = day + "." + month + "." + year;
            //DateTime ddate;
            //if (DateTime.TryParse(date, out ddate))
            //    Console.WriteLine(ddate.ToShortDateString());
            //else
            //    Console.WriteLine("not date");

            ServiceMenu sm = new ServiceMenu();
            Book b = null;
            Reader r = new Reader();
            sm.DebtorsListReport();

        }
    }
}
