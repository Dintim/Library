using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class ServiceBookType
    {
        public bool AddBookTypeToDB(BookType bookType, out string message)
        {
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var bt = db.GetCollection<BookType>("BookTypes");
                    bt.Insert(bookType);
                }
                message = string.Format("Жанр {0} в базу добавлен успешно", bookType.Name);
                return true;
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
