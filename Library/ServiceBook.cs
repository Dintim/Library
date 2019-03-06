using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class ServiceBook
    {
        public bool AddBookToDB(Book book, out string message)
        {
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var books = db.GetCollection<Book>("Books");
                    books.Insert(book);
                }
                message = string.Format("Книга {0} в базу добавлена успешно", book.Id);
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        public bool DeleteBookFromDB(Book book, out string message)
        {
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var books = db.GetCollection<Book>("Books");
                    books.Delete(d => d.Equals(book));
                }
                message = string.Format("Книга {0} из базы удалена успешно", book.Id);
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        public Book FindBookById(int id, out string message)
        {
            Book book = null;
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var books = db.GetCollection<Book>("Books");
                    var result = books.Find(f => f.Id.Equals(id));
                    if (result.Any())
                    {
                        message = "";
                        return result.FirstOrDefault();
                    }
                    else
                    {
                        message = "Книги с таким ID в базе нет";
                        return book;
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return book;
            }
        }

        public Book FindBookByISDN(string isdn, out string message)
        {
            Book book = null;
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var books = db.GetCollection<Book>("Books");
                    var result = books.Find(f => f.ISDN.Equals(isdn));
                    if (result.Any())
                    {
                        message = "";
                        return result.FirstOrDefault();
                    }
                    else
                    {
                        message = "Книги с таким ISDN в базе нет";
                        return book;
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return book;
            }
        }

        public Book FindBookByNameAuthor(string name, string author, out string message)
        {
            Book book = null;
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var books = db.GetCollection<Book>("Books");
                    var result = books.Find(f => f.Name.Equals(name) && f.Author.Equals(author));
                    if (result.Any())
                    {
                        message = "";
                        return result.FirstOrDefault();
                    }
                    else
                    {
                        message = "Книги с таким названием и такого автора в базе нет";
                        return book;
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return book;
            }
        }

        public bool UpdateBookStatus(Book book, BookStatus status, out string message)
        {
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var books = db.GetCollection<Book>("Books");
                    book.BookStatus = status;
                    books.Update(book);
                }
                message = string.Format("Статус книги {0} изменен на {1}", book.Id, status);
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        public List<Book> GetBooks(out string message)
        {
            List<Book> books = null;
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var tmp = db.GetCollection<Book>("Books");
                    books = tmp.FindAll().ToList();
                    message = "";
                    return books;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return books;
            }
        }
    }
}
