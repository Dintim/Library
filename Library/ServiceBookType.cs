using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class ServiceBookType
    {
        public bool AddBookTypeToDB(string bookType, out string message)
        {
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var bt = db.GetCollection<BookType>("BookTypes");
                    if (!bt.Exists(e => e.Name.Equals(bookType)))
                    {
                        bt.Insert(new BookType() { Name = bookType });
                        message = string.Format("Жанр {0} в базу добавлен успешно", bookType);
                        return true;
                    }
                    else
                    {
                        message = "Такой жанр уже существует в базе";
                        return false;
                    }
                }                
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        public List<BookType> GetBookTypes(out string message)
        {
            List<BookType> bt = null;
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var tmp = db.GetCollection<BookType>("BookTypes");
                    bt = tmp.FindAll().ToList();
                    message = "";
                    return bt;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return bt;
            }
        }
    }
}
