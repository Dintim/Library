using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Library.Cons.Model
{
    public partial class ServiceMenu
    {
        public void CreateOrChangeReader() //+
        {
            Reader reader = null;            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Создать/редактировать профиль читателя");
                Console.WriteLine("---------------------------------------------\n");
                Console.WriteLine("1. Зарегистрировать читателя");
                Console.WriteLine("2. Редактировать профиль читателя");
                Console.WriteLine("3. Выход");
                Console.Write("Ваш выбор: ");
                string choice = Console.ReadLine();
                if (Char.IsNumber(choice[0]) && choice.Length == 1)
                {
                    if (choice[0] == '1')
                        RegisterReaderMenu();
                    else if (choice[0] == '2')
                    {
                        FindReader(reader);
                        UpdateReader(reader);
                    }
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

        public void CreateOrChangeBook() //+
        {
            Book book = null;
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
                string choice = Console.ReadLine();
                if (Char.IsNumber(choice[0]) && choice.Length == 1)
                {
                    if (choice[0] == '1')
                        RegisterBookMenu();
                    else if (choice[0] == '2')
                    {
                        FindBook(book);
                        UpdateBook(book);
                    }
                    else if (choice[0] == '3')
                        AddNewBookType();
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

        public void ReportsMenu() //+
        {            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Отчеты");
                Console.WriteLine("---------------------------------------------\n");
                Console.WriteLine("1. Отчет за указанный день");
                Console.WriteLine("2. Список должников");
                Console.WriteLine("3. Список занятых книг");
                Console.WriteLine("4. Выход");
                Console.Write("Ваш выбор: ");
                string ch = Console.ReadLine();
                if (Char.IsNumber(ch[0]) && ch.Length == 1)
                {
                    if (ch[0] == '1')
                        DailyReport();
                    else if (ch[0] == '2')
                        DebtorsListReport();
                    else if (ch[0] == '3')
                        IssuedBooksReport();
                    else if (ch[0] == '4')
                        break;
                    else
                        Console.WriteLine("Некорректный ввод. Попробуйте еще раз");
                }
                else
                    Console.WriteLine("Некорректный ввод. Попробуйте еще раз");
                Thread.Sleep(1000);
            }
        }

        public void IssuedBooksReport() //+
        {
            string msg = "";            
            Console.Clear();
            var bList = serviceBook.GetBooks(out msg).Where(w => w.BookStatus.Equals(BookStatus.busy));            
            if (!serviceBook.GetBooks(out msg).Exists(w => w.BookStatus.Equals(BookStatus.busy)))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Все книги свободны");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.WriteLine("Список занятых книг на текущую дату");
                Console.WriteLine("ID\tISDN\tКод\tНазвание\tАвтор\tГод издания\tСтатус");
                Console.WriteLine("---------------------------------------------\n");
                foreach (Book i in bList)
                {
                    Console.WriteLine("{0}\t{0}\t{0}\t{0}\t{0}\t{0}\t{0}", i.Id, i.ISDN, i.Code, i.Name, i.Author, i.PublishDate, i.BookStatus);
                }
                Console.Write("Сохранить в XML формате? (0-нет, 1-да): ");
                string choice = Console.ReadLine();
                if (Char.IsNumber(choice[0]) && choice.Length == 1)
                {
                    if (choice[0] == '1')
                        CreateBooksXML("IssuedBooksReport", bList.ToList(), DateTime.Now);
                }
            }
            Thread.Sleep(2000);
        }

        public void CreateBooksXML(string name, List<Book> bList, DateTime ddate) //+
        {
            string path = name + ddate.Year + ddate.Month + ddate.Day + ".xml";
            SoapFormatter formatter = new SoapFormatter();
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, bList.ToArray());
            }
        }

        public void DebtorsListReport() //+
        {
            string msg = "";
            List<TransactionShort> tsList = new List<TransactionShort>();
            Console.Clear();
            var tList = serviceTransaction.GetIssueTransactions(out msg);
            var bList = serviceBook.GetBooks(out msg).Where(w => w.BookStatus.Equals(BookStatus.busy));
            if (!serviceBook.GetBooks(out msg).Exists(w => w.BookStatus.Equals(BookStatus.busy)))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Все книги свободны, должников нет");
                Console.ForegroundColor = ConsoleColor.White;                                
            }
            if (tList != null && serviceBook.GetBooks(out msg).Exists(w => w.BookStatus.Equals(BookStatus.busy)))
            {
                foreach (Transaction i in tList)
                {
                    if (bList.Contains(i.Book))
                    {
                        TransactionShort ts = new TransactionShort();
                        ts.Id = i.Id;
                        ts.Date = i.Date;
                        ts.ReaderId = i.Reader.Id;
                        ts.ReaderFullName = i.Reader.Name + " " + i.Reader.Surname;
                        ts.BookId = i.Book.Id;
                        ts.BookName = i.Book.Name;
                        ts.TransactionType = i.TransactionType;
                        tsList.Add(ts);
                    }
                }
                Console.WriteLine("Список должников на текущую дату");
                Console.WriteLine("Дата\tID читателя\tЧитатель\tID книги\tКнига");
                Console.WriteLine("---------------------------------------------\n");
                foreach (TransactionShort i in tsList)
                {
                    Console.WriteLine("{0: dd.MM.yyyy}\t{1}\t{0}\t{0}\t{0}", i.Date, i.ReaderId, i.ReaderFullName, i.BookId, i.BookName);
                }
                Console.Write("Сохранить в XML формате? (0-нет, 1-да): ");
                string choice = Console.ReadLine();
                if (Char.IsNumber(choice[0]) && choice.Length == 1)
                {
                    if (choice[0] == '1')
                        CreateTransactionsXML("DebtorsReport", tsList, DateTime.Now);
                }
            }
            Thread.Sleep(2000);
        }

        public void DailyReport() //+
        {
            string msg = "";
            List<TransactionShort> tsList = new List<TransactionShort>();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Отчет на указанную дату");
                Console.WriteLine("---------------------------------------------\n");
                Console.Write("День: ");
                string day = Console.ReadLine();
                Console.Write("Месяц: ");
                string month = Console.ReadLine();
                Console.Write("Год: ");
                string year = Console.ReadLine();
                string date = day + "." + month + "." + year;
                DateTime ddate;
                if (DateTime.TryParse(date, out ddate))
                {
                    Console.Clear();
                    Console.WriteLine("Отчет на {0: dd.MM.yyyy}", ddate);
                    Console.WriteLine("ID\tДата\tID читателя\tЧитатель\tID книги\tКнига\tТип операции");
                    Console.WriteLine("---------------------------------------------\n");
                    var tmp = serviceTransaction.GetTransactions(out msg).Where(w => w.Date.Equals(ddate));
                    foreach (Transaction i in tmp)
                    {
                        TransactionShort ts = new TransactionShort();
                        ts.Id = i.Id;
                        ts.Date = i.Date;
                        ts.ReaderId = i.Reader.Id;
                        ts.ReaderFullName = i.Reader.Name + " " + i.Reader.Surname;
                        ts.BookId = i.Book.Id;
                        ts.BookName = i.Book.Name;
                        ts.TransactionType = i.TransactionType;
                        tsList.Add(ts);
                        Console.WriteLine("{0}\t{1: dd.MM.yyyy}\t{2}\t{3}\t{4}\t{5}\t{6}", 
                            i.Id, i.Date, i.Reader.Id, i.Reader.Name+" "+i.Reader.Surname, i.Book.Id, i.Book.Name, i.TransactionType);
                    }
                    Console.Write("Сохранить в XML формате? (0-нет, 1-да): ");
                    string choice = Console.ReadLine();
                    if (Char.IsNumber(choice[0]) && choice.Length == 1)
                    {
                        if (choice[0] == '1')
                            CreateTransactionsXML("DailyReport", tsList, ddate);
                    }
                    Thread.Sleep(2000);
                    break;
                }
                else
                {
                    Console.WriteLine("Некорректная дата. Введите еще раз");
                }
            }            
        }

        public void CreateTransactionsXML(string name, List<TransactionShort> tsList, DateTime ddate) //+
        {
            string path = name + ddate.Year + ddate.Month + ddate.Day + ".xml";
            SoapFormatter formatter = new SoapFormatter();
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, tsList.ToArray());
            }
        }

        public void ChangeAdministratorPassword() //+
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

        public void RegisterBookMenu() //+
        {
            string msg = "";
            Book book = new Book();
            Console.Clear();
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

        public void AddNewBookType() //+
        {
            string msg = "";
            Console.Clear();
            Console.WriteLine("Форма регистрации жанра книги:");
            Console.WriteLine("---------------------------------------------\n");
            Console.Write("Название жанра: ");
            string bookType = Console.ReadLine();

            serviceBookType.AddBookTypeToDB(bookType, out msg);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(1000);
        }
    }
}
