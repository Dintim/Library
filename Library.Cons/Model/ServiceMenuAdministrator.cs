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
        public void CreateOrChangeReader()
        {
            Reader reader = new Reader();            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Создать/редактировать профиль читателя");
                Console.WriteLine("---------------------------------------------\n");
                Console.WriteLine("1. Зарегистрировать читателя");
                Console.WriteLine("2. Редактировать профиль читателя");
                Console.WriteLine("3. Выход");
                Console.Write("Ваш выбор: ");
                int ch = Int32.Parse(Console.ReadLine());
                if (ch == 1)
                    RegisterReaderMenu();
                else if (ch == 2)
                {
                    FindReader(reader);
                    UpdateReader(reader);
                }
                else if (ch == 3)
                    break;
                else
                    continue;
            }
        }

        public void CreateOrChangeBook()
        {
            Book book = new Book();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Создать/редактировать книжный фонд");
                Console.WriteLine("---------------------------------------------\n");
                Console.WriteLine("1. Зарегистрировать книгу");
                Console.WriteLine("2. Редактировать профиль книги");
                Console.WriteLine("3. Добавить новый жанр");
                Console.WriteLine("4. Выход");
                Console.Write("Ваш выбор: ");
                int ch = Int32.Parse(Console.ReadLine());
                if (ch == 1)
                    RegisterBookMenu();
                else if (ch == 2)
                {
                    FindBook(book);
                    UpdateBook(book);
                }
                else if (ch == 3)
                    Console.WriteLine();
                    //AddNewBookType();
                else if (ch == 4)
                    break;
                else
                    continue;
            }
        }

        public void ReportsMenu()
        {

        }

        public void ChangeAdministratorPassword()
        {
            int k = 1;
            while (k != 4)
            {
                string msg = "";
                Console.Clear();
                Console.WriteLine("Изменение пароля");
                Console.WriteLine("---------------------------------------------\n");
                Console.Write("Новый пароль: ");
                string pass1 = Console.ReadLine();
                Console.Write("Повторите еще раз новый пароль: ");
                string pass2 = Console.ReadLine();
                if (pass1.Equals(pass2))
                {
                    serviceAdministrator.UpdateAdministratorPassword(AuthorAdmin, pass1, out msg);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(msg);
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(2000);
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Пароли не сходятся, введите заново");
                    Console.WriteLine("Попытка {0}", k);
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(2000);
                }
                k++;
            }
        }

        public void RegisterBookMenu()
        {
            string msg = "";
            Book book = new Book();
            Console.WriteLine("Форма регистрации книги:");
            Console.WriteLine("---------------------------------------------\n");
            Console.Write("Название: ");
            book.Name = Console.ReadLine();
            Console.Write("Автор: ");
            book.Author = Console.ReadLine();
            while (true)
            {
                Console.Write("ISDN: ");
                string isdn = Console.ReadLine();
                var tmp = serviceBook.FindBookByISDN(isdn, out msg);
                if (tmp==null)
                {
                    book.ISDN = isdn;
                    break;
                }
                else
                {
                    Console.WriteLine("Книга с таким ISDN в базе уже есть");
                    continue;
                }
            }
            
            while (true)
            {
                Console.Write("Год публикации: ");
                int tmp = Int32.Parse(Console.ReadLine());
                if (tmp<=1760 && tmp>DateTime.Now.Year)
                    continue;
                else
                {
                    book.PublishDate = tmp;
                    break;
                }
            }
            Console.Write("Редакция: ");
            book.Edition = Int32.Parse(Console.ReadLine());
            Console.Write("Внутренний код: ");
            book.Code = Console.ReadLine();
            var bookTypes = serviceBookType.GetBookTypes(out msg);
            Console.WriteLine("Жанр: ");
            foreach (BookType i in bookTypes)
            {
                Console.WriteLine("{0}. {1}", i.Id, i.Name);
            }
            while (true)
            {
                Console.Write("Ваш выбор жанра: ");
                int t = Int32.Parse(Console.ReadLine());
                if (bookTypes.Exists(e => e.Id.Equals(t)))
                {
                    book.BookType = bookTypes.Find(f => f.Id.Equals(t));
                    break;
                }
                else
                    continue;
            }

            serviceBook.AddBookToDB(book, out msg);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(1000);
        }
    }
}
