using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private ServiceBookType serviceBookType = new ServiceBookType();
        private ServiceTransaction serviceTransaction = new ServiceTransaction();

        public void MainMenu() //+
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Добро пожаловать в библиотеку ХХХХ!");
                Console.WriteLine("1. Регистрация");
                Console.WriteLine("2. Вход");
                Console.WriteLine("3. Выход");
                Console.Write("Ваш выбор: ");
                string choice = Console.ReadLine();
                if (Char.IsNumber(choice[0]) && choice.Length == 1)
                {
                    if (choice[0] == '1')
                        RegisterMenu();
                    else if (choice[0] == '2')
                        LogOnMenu();
                    else if (choice[0] == '3')
                        break;
                    else                    
                        Console.WriteLine("Некорректный ввод. Попробуйте еще раз");                        
                }
                else
                    Console.WriteLine("Некорректный ввод. Попробуйте еще раз");                    
                Thread.Sleep(1000);
            }
        }

        public void RegisterMenu() //+
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
                string choice = Console.ReadLine();
                if (Char.IsNumber(choice[0]) && choice.Length == 1)
                {
                    if (choice[0] == '1')
                        RegisterReaderMenu();
                    else if (choice[0] == '2')
                        RegisterAdministratorMenu();
                    else if (choice[0] == '3')
                        break;
                    else
                        Console.WriteLine("Некорректный ввод. Попробуйте еще раз");
                }
                else
                    Console.WriteLine("Некорректный ввод. Попробуйте еще раз");
                Thread.Sleep(1000);               
            }
        }

        public void LogOnMenu() //+
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
                string choice = Console.ReadLine();
                if (Char.IsNumber(choice[0]) && choice.Length == 1)
                {
                    if (choice[0] == '1')
                        LogOnReaderMenu();
                    else if (choice[0] == '2')
                        LogOnAdministratorMenu();
                    else if (choice[0] == '3')
                        break;
                    else
                        Console.WriteLine("Некорректный ввод. Попробуйте еще раз");
                }
                else
                    Console.WriteLine("Некорректный ввод. Попробуйте еще раз");
                Thread.Sleep(1000);
            }
        }

        public void ReaderMenu() //+
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
                string choice = Console.ReadLine();
                if (Char.IsNumber(choice[0]) && choice.Length == 1)
                {
                    if (choice[0] == '1')
                    {
                        FindBook(book);
                        IssueBook(book);
                    }
                    else if (choice[0] == '2')
                        ReturnBook();
                    else if (choice[0] == '3')
                        ChangeReaderPassword();
                    else if (choice[0] == '4')
                        break;
                    else
                        Console.WriteLine("Некорректный ввод. Попробуйте еще раз");
                }
                else
                    Console.WriteLine("Некорректный ввод. Попробуйте еще раз");
                Thread.Sleep(1000);
            }
        }
        public void AdministratorMenu() //+
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
                string choice = Console.ReadLine();
                if (Char.IsNumber(choice[0]) && choice.Length == 1)
                {
                    if (choice[0] == '1')
                        CreateOrChangeReader();
                    else if (choice[0] == '2')
                        CreateOrChangeBook();
                    else if (choice[0] == '3')
                        ReportsMenu();
                    else if (choice[0] == '4')
                        ChangeAdministratorPassword();
                    else if (choice[0] == '5')
                        break;
                    else
                        Console.WriteLine("Некорректный ввод. Попробуйте еще раз");
                }
                else
                    Console.WriteLine("Некорректный ввод. Попробуйте еще раз");
                Thread.Sleep(1000);
            }
        }
    }
}
