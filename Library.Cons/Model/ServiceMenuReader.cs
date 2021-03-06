﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Cons.Model
{
    public partial class ServiceMenu
    {
        public void FindBook(out Book book) //+
        {
            string msg = "";
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Поиск книги");
                Console.WriteLine("---------------------------------------------\n");
                Console.WriteLine("1. По ID");
                Console.WriteLine("2. По ISDN");
                Console.WriteLine("3. По названию и автору");
                Console.WriteLine("4. Выход");
                Console.Write("Ваш выбор: ");
                string choice = Console.ReadLine();
                if (Char.IsNumber(choice[0]) && choice.Length == 1)
                {
                    if (choice[0] == '1')
                    {
                        Console.WriteLine("---------------------------------------------\n");
                        Console.Write("Введите ID: ");
                        int id = Int32.Parse(Console.ReadLine());
                        book = serviceBook.FindBookById(id, out msg);
                        break;
                    }
                    else if (choice[0] == '2')
                    {
                        Console.WriteLine("---------------------------------------------\n");
                        Console.Write("Введите ISDN: ");
                        string isdn = Console.ReadLine();
                        book = serviceBook.FindBookByISDN(isdn, out msg);
                        break;
                    }
                    else if (choice[0] == '3')
                    {
                        Console.WriteLine("---------------------------------------------\n");
                        Console.Write("Введите название: ");
                        string name = Console.ReadLine();
                        Console.Write("Введите автора: ");
                        string author = Console.ReadLine();
                        book = serviceBook.FindBookByNameAuthor(name, author, out msg);
                        break;
                    }
                    else if (choice[0] == '4')
                    {
                        book = null;
                        break;
                    }
                    else
                        Console.WriteLine("Некорректный ввод. Попробуйте еще раз");
                }
                else
                    Console.WriteLine("Некорректный ввод. Попробуйте еще раз");
                Thread.Sleep(1000);
            }
        }
        
        public void IssueBook(Book book) //+
        {
            string msg = "";
            if (book!=null)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Найдено");
                    Console.WriteLine("---------------------------------------------\n");
                    Console.WriteLine(book.ToString());
                    Console.WriteLine("Заказать книгу?");
                    Console.WriteLine("1. да");
                    Console.WriteLine("2. нет");
                    Console.Write("Ваш ответ: ");
                    string choice = Console.ReadLine();
                    if (Char.IsNumber(choice[0]) && choice.Length == 1)
                    {
                        if (choice[0] == '1')
                        {
                            if (book.BookStatus != BookStatus.busy)
                            {
                                AuthorReader.IssuedBooks.Add(book);
                                serviceBook.UpdateBookStatus(book, BookStatus.busy, out msg);
                                Transaction trans = new Transaction();
                                trans.Date = DateTime.Now;
                                trans.Reader = AuthorReader;
                                trans.Book = book;
                                trans.TransactionType = TransactionType.issueBook;
                                serviceTransaction.AddTransactionToDB(trans, out msg);
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Книга добавлена в ваш лист");
                                Console.ForegroundColor = ConsoleColor.White;
                                Thread.Sleep(1000);                                
                                break;
                            }
                            else
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Книга сейчас занята. Пожалуста, выберите другую книгу");
                                Console.ForegroundColor = ConsoleColor.White;
                                Thread.Sleep(1000);                                
                                break;
                            }
                        }
                        else if (choice[0] == '2')
                            break;
                        else
                            Console.WriteLine("Некорректный ввод. Попробуйте еще раз");
                    }
                    else
                        Console.WriteLine("Некорректный ввод. Попробуйте еще раз");
                    Thread.Sleep(1000);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Книга с такими параметрами не найдена");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(2000);
            }
        }

        public void ReturnBook() //+
        {
            string msg = "";
            Book book = null;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Список Ваших книг\n");
                Console.WriteLine("ID:\tISDN:\tНазвание:\tАвтор:\tДата публикации:\tЖанр:\tСтатус:");
                Console.WriteLine("---------------------------------------------\n");
                foreach (Book i in AuthorReader.IssuedBooks)
                {
                    Console.WriteLine(i.ToString());
                }
                Console.Write("Введите ID книги, которую необходимо вернуть: ");
                int id = Int32.Parse(Console.ReadLine());
                book = serviceBook.FindBookById(id, out msg);
                if (book != null)
                {
                    AuthorReader.IssuedBooks.Remove(book);
                    serviceBook.UpdateBookStatus(book, BookStatus.free, out msg);
                    Transaction trans = new Transaction();
                    trans.Date = DateTime.Now;
                    trans.Reader = AuthorReader;
                    trans.Book = book;
                    trans.TransactionType = TransactionType.returnBook;
                    serviceTransaction.AddTransactionToDB(trans, out msg);

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Книга возвращена");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Вернуть еще одну книгу?");
                    Console.WriteLine("1. да");
                    Console.WriteLine("2. нет");
                    Console.Write("Ваш ответ:");
                    int ans = Int32.Parse(Console.ReadLine());
                    if (ans == 1)
                        continue;
                    else
                        break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(msg);
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(2000);
                }
            }
        }

        public void UpdateBook(Book book) //+
        {
            string msg = "";
            if (book != null)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Редактировать профиль книги {0}, авт. {1}", book.Name, book.Author);
                    Console.WriteLine("---------------------------------------------\n");                    
                    Console.WriteLine("1. Внутренний код");
                    Console.WriteLine("2. Жанр");
                    Console.WriteLine("3. Выход");
                    Console.Write("Ваш выбор: ");
                    string choice = Console.ReadLine();
                    Console.Clear();
                    if (Char.IsNumber(choice[0]) && choice.Length == 1)
                    {
                        if (choice[0] == '1')
                        {
                            Console.WriteLine("Внутренний код: {0}", book.Code);
                            Console.Write("Новый внутренний код книги:");
                            string code = Console.ReadLine();
                            serviceBook.UpdateBookCode(book, code, out msg);
                        }
                        else if (choice[0] == '2')
                        {
                            Console.WriteLine("Жанр: {0}", book.BookType);
                            Console.WriteLine("Новый жанр книги:");
                            var tmp = serviceBookType.GetBookTypes(out msg);
                            foreach (BookType i in tmp)
                            {
                                Console.WriteLine("{0}. {1}", i.Id, i.Name);
                            }
                            while (true)
                            {
                                Console.Write("Ваш выбор жанра:");
                                int btype = Int32.Parse(Console.ReadLine());
                                if (tmp.Exists(e => e.Id.Equals(btype)))
                                {
                                    BookType bt = tmp.Find(f => f.Id.Equals(btype));
                                    serviceBook.UpdateBookBookType(book, bt, out msg);
                                    break;
                                }                                
                            }
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
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(msg);
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(2000);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Книга с такими параметрами не найдена");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(2000);
            }
        }

        public void ChangeReaderPassword() //+
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
                    serviceReader.UpdateReaderPassword(AuthorReader, pass1, out msg);
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

        public void FindReader(out Reader reader) //+
        {
            while (true)
            {
                string msg = "";
                Console.Clear();
                Console.WriteLine("Поиск читателя");
                Console.WriteLine("---------------------------------------------\n");
                Console.WriteLine("1. По ID");
                Console.WriteLine("2. По имени и фамилии");
                Console.WriteLine("3. Выход");
                Console.Write("Ваш выбор: ");
                string choice = Console.ReadLine();
                if (Char.IsNumber(choice[0]) && choice.Length == 1)
                {
                    if (choice[0] == '1')
                    {
                        Console.WriteLine("---------------------------------------------\n");
                        Console.Write("Введите ID: ");
                        int id = Int32.Parse(Console.ReadLine());
                        reader = serviceReader.FindReaderById(id, out msg);
                        break;
                    }
                    else if (choice[0] == '2')
                    {
                        Console.WriteLine("---------------------------------------------\n");
                        Console.Write("Введите имя: ");
                        string name = Console.ReadLine();
                        Console.Write("Введите фамилию: ");
                        string surname = Console.ReadLine();
                        reader = serviceReader.FindReaderByNameSurname(name, surname, out msg);
                        break;
                    }
                    else if (choice[0] == '3')
                    {
                        reader = null;
                        break;
                    }
                    else
                        Console.WriteLine("Некорректный ввод. Попробуйте еще раз");
                }
                else
                    Console.WriteLine("Некорректный ввод. Попробуйте еще раз");
                Thread.Sleep(1000);
            }
        }

        public void UpdateReader(Reader reader) //+
        {
            string msg = "";
            if (reader != null)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Редактировать профиль читателя {0} {1}", reader.Name, reader.Surname);
                    Console.WriteLine("---------------------------------------------\n");
                    Console.WriteLine("1. Имя");
                    Console.WriteLine("2. Фамилия");
                    Console.WriteLine("3. Email");
                    Console.WriteLine("4. Адрес");
                    Console.WriteLine("5. Телефон");
                    Console.WriteLine("6. Пароль");
                    Console.WriteLine("7. Доступ");
                    Console.WriteLine("8. Выход");
                    Console.Write("Ваш выбор: ");
                    string choice = Console.ReadLine();
                    Console.Clear();
                    if (Char.IsNumber(choice[0]) && choice.Length == 1)
                    {
                        if (choice[0] == '1')
                        {
                            Console.WriteLine("Имя: {0}", reader.Name);
                            Console.Write("Новое имя читателя:");
                            string name = Console.ReadLine();
                            serviceReader.UpdateReaderName(reader, name, out msg);
                        }
                        else if (choice[0] == '2')
                        {
                            Console.WriteLine("Фамилия: {0}", reader.Surname);
                            Console.Write("Новая фамилия читателя:");
                            string surname = Console.ReadLine();
                            serviceReader.UpdateReaderSurname(reader, surname, out msg);
                        }
                        else if (choice[0] == '3')
                        {
                            while (true)
                            {
                                Console.WriteLine("Email: {0}", reader.Email);
                                Console.Write("Новый email читателя:");
                                string tmp = Console.ReadLine();
                                if ((!tmp.Contains("@") && (!tmp.Contains(".kz") || !tmp.Contains(".ru") || !tmp.Contains(".com") || !tmp.Contains(".org"))) || (!tmp.Contains("@") || tmp[0] == '@'))
                                    continue;
                                else
                                {
                                    serviceReader.UpdateReaderEmail(reader, tmp, out msg);
                                    break;
                                }
                            }
                        }
                        else if (choice[0] == '4')
                        {
                            Console.WriteLine("Адрес: {0}", reader.Address);
                            Console.Write("Новый адрес читателя:");
                            string address = Console.ReadLine();
                            serviceReader.UpdateReaderAddress(reader, address, out msg);
                        }
                        else if (choice[0] == '5')
                        {
                            Console.WriteLine("Телефон: {0}", reader.Tel);
                            Console.Write("Новый телефон читателя:");
                            string tel = Console.ReadLine();
                            serviceReader.UpdateReaderTel(reader, tel, out msg);
                        }
                        else if (choice[0] == '6')
                        {
                            Console.WriteLine("Пароль: {0}", reader.Password);
                            Console.Write("Новый пароль читателя:");
                            string pass = Console.ReadLine();
                            serviceReader.UpdateReaderPassword(reader, pass, out msg);
                        }
                        else if (choice[0] == '7')
                        {
                            Console.WriteLine("Заблокирован: {0}", reader.IsBlocked);
                            Console.Write("Доступ читателя (0-свободный, 1-заблокировать):");
                            bool status = Convert.ToBoolean(Int32.Parse(Console.ReadLine()));
                            serviceReader.UpdateReaderStatus(reader, status, out msg);
                        }
                        else if (choice[0] == '8')
                            break;
                        else
                            Console.WriteLine("Некорректный ввод. Попробуйте еще раз");
                    }
                    else
                        Console.WriteLine("Некорректный ввод. Попробуйте еще раз");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(msg);
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(1000);                    
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Читатель с такими параметрами не найден");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(2000);
            }
        }
    }
}
