using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Cons.Model
{
    public partial class ServiceMenu
    {
        private ServiceReader serviceReader = new ServiceReader();
        public void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Добро пожаловать в библиотеку ХХХХ!");
                Console.WriteLine("1. Регистрация");
                Console.WriteLine("2. Вход");
                Console.WriteLine("3. Выход");
                Console.Write("Ваш выбор: ");
                int ch = Int32.Parse(Console.ReadLine());
                if (ch == 1)
                    RegisterMenu();
                else if (ch == 2)
                    LogOnMenu();
                else if (ch == 3)
                    break;
                else
                    continue;
            }
        }

        public void RegisterMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Меню регистрации");
                Console.WriteLine("---------------------------------------------\n");
                Console.WriteLine("1. Читатель");
                Console.WriteLine("2. Администратор");
                Console.WriteLine("3. Выход");
                Console.Write("Ваш выбор: ");
                int ch = Int32.Parse(Console.ReadLine());
                if (ch == 1)
                    ReaderRegisterMenu();
                else if (ch == 2)
                    AdministratorRegisterMenu();
                else if (ch == 3)
                    break;
                else
                    continue;
            }
        }

        public void LogOnMenu()
        {

        }
    }
}
