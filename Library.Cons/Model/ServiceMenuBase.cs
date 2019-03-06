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
        private Reader AuthorReader = new Reader();
        private ServiceAdministrator serviceAdministrator = new ServiceAdministrator();
        private Administrator AuthorAdmin = new Administrator();
        private ServiceBook serviceBook = new ServiceBook();
        private ServiceTransaction serviceTransaction = new ServiceTransaction();

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
                    RegisterReaderMenu();
                else if (ch == 2)
                    RegisterAdministratorMenu();
                else if (ch == 3)
                    break;
                else
                    continue;
            }
        }

        public void LogOnMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Меню входа в систему");
                Console.WriteLine("---------------------------------------------\n");
                Console.WriteLine("1. Читатель");
                Console.WriteLine("2. Администратор");
                Console.WriteLine("3. Выход");
                Console.Write("Ваш выбор: ");
                int ch = Int32.Parse(Console.ReadLine());
                if (ch == 1)
                    LogOnReaderMenu();
                else if (ch == 2)
                    LogOnAdministratorMenu();
                else if (ch == 3)
                    break;
                else
                    continue;
            }
        }

        public void ReaderMenu()
        {
            Book book = null;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Меню читателя");
                Console.WriteLine("---------------------------------------------\n");
                Console.WriteLine("1. Найти книгу");
                Console.WriteLine("2. Вернуть книгу");
                Console.WriteLine("3. Изменить пароль");
                Console.WriteLine("4. Выход");
                Console.Write("Ваш выбор: ");
                int ch = Int32.Parse(Console.ReadLine());
                if (ch == 1)
                {
                    FindBook(book);
                    IssueBook(book);
                }
                else if (ch == 2)
                    ReturnBook();
                else if (ch == 3)
                    ChangeReaderPassword();
                else if (ch == 4)
                    break;
                else
                    continue;
            }
        }
        public void AdministratorMenu()
        {            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Меню администратора");
                Console.WriteLine("---------------------------------------------\n");
                Console.WriteLine("1. Читатели");
                Console.WriteLine("2. Книжный фонд");
                Console.WriteLine("3. Отчеты");
                Console.WriteLine("4. Изменить пароль");
                Console.WriteLine("5. Выход");
                Console.Write("Ваш выбор: ");
                int ch = Int32.Parse(Console.ReadLine());
                if (ch == 1)
                    CreateOrChangeReader();
                else if (ch == 2)
                    CreateOrChangeBook();
                else if (ch == 3)
                    ReportsMenu();
                else if (ch == 4)
                    ChangeAdministratorPassword();
                else if (ch == 4)
                    break;
                else
                    continue;
            }
        }
    }
}
