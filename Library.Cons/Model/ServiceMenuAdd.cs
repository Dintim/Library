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
        public void RegisterReaderMenu()
        {
            string msg = "";
            Reader reader = new Reader();
            Console.WriteLine("Форма регистрации для читателя:");
            Console.WriteLine("---------------------------------------------\n");
            Console.Write("Имя: ");
            reader.Name = Console.ReadLine();
            Console.Write("Фамилия: ");
            reader.Surname = Console.ReadLine();
            while (true)
            {
                Console.Write("Email: ");
                string tmp = Console.ReadLine();
                if ((!tmp.Contains("@") && (!tmp.Contains(".kz") || !tmp.Contains(".ru") || !tmp.Contains(".com") || !tmp.Contains(".org"))) || (!tmp.Contains("@") || tmp[0] == '@'))
                    continue;
                else
                {
                    reader.Email = tmp;
                    break;
                }
            }
            while (true)
            {
                Console.Write("Логин: ");
                string tmp = Console.ReadLine();
                var readersList = serviceReader.GetReaders(out msg);
                if (readersList.Find(f => f.Login.Equals(tmp)) != null)
                {
                    Console.WriteLine("Читатель с таким логином уже существует");
                    continue;
                }
                else
                {
                    reader.Login = tmp;
                    break;
                }
            }
            Console.Write("Пароль: ");
            reader.Password = Console.ReadLine();

            serviceReader.AddReaderToDB(reader, out msg);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(1000);
        }

        public void RegisterAdministratorMenu()
        {
            string msg = "";
            Administrator admin = new Administrator();
            Console.WriteLine("Форма регистрации для администратора:");
            Console.WriteLine("---------------------------------------------\n");
            Console.Write("Имя: ");
            admin.Name = Console.ReadLine();
            Console.Write("Фамилия: ");
            admin.Surname = Console.ReadLine();
            while (true)
            {
                Console.Write("Email: ");
                string tmp = Console.ReadLine();
                if ((!tmp.Contains("@") && (!tmp.Contains(".kz") || !tmp.Contains(".ru") || !tmp.Contains(".com") || !tmp.Contains(".org"))) || (!tmp.Contains("@") || tmp[0] == '@'))
                    continue;
                else
                {
                    admin.Email = tmp;
                    break;
                }
            }
            while (true)
            {
                Console.Write("Логин: ");
                string tmp = Console.ReadLine();
                var adminsList = serviceAdministrator.GetAdministrators(out msg);
                if (adminsList.Find(f => f.Login.Equals(tmp)) != null)
                {
                    Console.WriteLine("Администратор с таким логином уже существует");
                    continue;
                }
                else
                {
                    admin.Login = tmp;
                    break;
                }
            }
            Console.Write("Пароль: ");
            admin.Password = Console.ReadLine();

            serviceAdministrator.AddAdminToDB(admin, out msg);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(1000);
        }

        public void LogOnReaderMenu()
        {
            int k = 1;
            while (k!=4)
            {
                string msg = "";
                Console.Clear();
                Console.WriteLine("Проверка входа для читателя:");
                Console.WriteLine("---------------------------------------------\n");
                Console.Write("Логин: ");
                string login = Console.ReadLine();
                Console.Write("Пароль: ");
                string password = Console.ReadLine();
                Reader reader = serviceReader.LogOnReader(login, password, out msg);
                if (reader!=null)
                {
                    AuthorReader = reader;
                    List<Transaction> tmp = serviceTransaction.GetTransactionsByReaderId(AuthorReader.Id, out msg);
                    foreach (Transaction i in tmp.Where(w=>w.Book.BookStatus.Equals(BookStatus.busy)))
                    {                        
                        AuthorReader.IssuedBooks.Add(i.Book);
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(msg);
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(1000);                    
                    ReaderMenu();
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(msg);
                    Console.WriteLine("Попытка {0}", k);
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(1000);
                }
                k++;
            }
        }

        public void LogOnAdministratorMenu()
        {
            int k = 1;
            while (k!=4)
            {
                string msg = "";
                Console.Clear();
                Console.WriteLine("Проверка входа для администратора:");
                Console.WriteLine("---------------------------------------------\n");
                Console.Write("Логин: ");
                string login = Console.ReadLine();
                Console.Write("Пароль: ");
                string password = Console.ReadLine();
                Administrator admin = serviceAdministrator.LogOnAdmin(login, password, out msg);
                if (admin != null)
                {
                    AuthorAdmin = admin;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(msg);
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(1000);                    
                    AdministratorMenu();
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(msg);
                    Console.WriteLine("Попытка {0}", k);
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(1000);
                }
                k++;
            }
        }
    }
}
