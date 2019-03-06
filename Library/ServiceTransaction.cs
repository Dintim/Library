using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class ServiceTransaction
    {
        public bool AddTransactionToDB(Transaction trans, out string message)
        {
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var transactions = db.GetCollection<Transaction>("Transactions");
                    transactions.Insert(trans);
                }
                message = string.Format("Транзакция {0} {1} записана в базу успешно", trans.Id, trans.TransactionType);
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        public List<Transaction> GetIssueTransactions(out string messsage)
        {
            List<Transaction> trans = null;
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var transactions = db.GetCollection<Transaction>("Transactions");
                    var results = transactions.Find(f => f.TransactionType.Equals(TransactionType.issueBook));
                    if (results.Any())
                    {
                        messsage = "";
                        return (List<Transaction>)results;
                    }
                    else
                    {
                        messsage = string.Format("Транзакций с таким статусом  {0} нет", TransactionType.issueBook);
                        return trans;
                    }
                }
            }
            catch (Exception ex)
            {
                messsage = ex.Message;
                return trans;
            }
        }

        public List<Transaction> GetReturnTransactions(out string messsage)
        {
            List<Transaction> trans = null;
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var transactions = db.GetCollection<Transaction>("Transactions");
                    var results = transactions.Find(f => f.TransactionType.Equals(TransactionType.returnBook));
                    if (results.Any())
                    {
                        messsage = "";
                        return (List<Transaction>)results;
                    }
                    else
                    {
                        messsage = string.Format("Транзакций с таким статусом  {0} нет", TransactionType.returnBook);
                        return trans;
                    }
                }
            }
            catch (Exception ex)
            {
                messsage = ex.Message;
                return trans;
            }
        }

        public List<Transaction> GetTransactionsByReaderId(int readerId, out string messsage)
        {
            List<Transaction> trans = null;
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var transactions = db.GetCollection<Transaction>("Transactions");
                    var results = transactions.Find(f => f.Reader.Id.Equals(readerId));
                    if (results.Any())
                    {
                        messsage = "";
                        return (List<Transaction>)results;
                    }
                    else
                    {
                        messsage = string.Format("Транзакций читателя с таким ID {0} нет", readerId);
                        return trans;
                    }
                }
            }
            catch (Exception ex)
            {
                messsage = ex.Message;
                return trans;
            }
        }

        public List<Transaction> GetTransactionsByReaderFullName(string name, string surname, out string messsage)
        {
            List<Transaction> trans = null;
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var transactions = db.GetCollection<Transaction>("Transactions");
                    var results = transactions.Find(f => f.Reader.Name.Equals(name) && f.Reader.Surname.Equals(surname));
                    if (results.Any())
                    {
                        messsage = "";
                        return (List<Transaction>)results;
                    }
                    else
                    {
                        messsage = string.Format("Транзакций читателя с такими именем и фамилией ({0} {1}) нет", name, surname);
                        return trans;
                    }
                }
            }
            catch (Exception ex)
            {
                messsage = ex.Message;
                return trans;
            }
        }

        public List<Transaction> GetTransactionsByBookId(int bookId, out string messsage)
        {
            List<Transaction> trans = null;
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var transactions = db.GetCollection<Transaction>("Transactions");
                    var results = transactions.Find(f => f.Book.Id.Equals(bookId));
                    if (results.Any())
                    {
                        messsage = "";
                        return (List<Transaction>)results;
                    }
                    else
                    {
                        messsage = string.Format("Транзакций по книге с таким ID ({0}) нет", bookId);
                        return trans;
                    }
                }
            }
            catch (Exception ex)
            {
                messsage = ex.Message;
                return trans;
            }
        }

        public List<Transaction> GetTransactionsByBookISDN(string isdn, out string messsage)
        {
            List<Transaction> trans = null;
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var transactions = db.GetCollection<Transaction>("Transactions");
                    var results = transactions.Find(f => f.Book.ISDN.Equals(isdn));
                    if (results.Any())
                    {
                        messsage = "";
                        return (List<Transaction>)results;
                    }
                    else
                    {
                        messsage = string.Format("Транзакций по книге с таким ISDN ({0}) нет", isdn);
                        return trans;
                    }
                }
            }
            catch (Exception ex)
            {
                messsage = ex.Message;
                return trans;
            }
        }

        public List<Transaction> GetTransactionsByBookNameAuthor(string name, string author, out string messsage)
        {
            List<Transaction> trans = null;
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var transactions = db.GetCollection<Transaction>("Transactions");
                    var results = transactions.Find(f => f.Book.Name.Equals(name) && f.Book.Author.Equals(author));
                    if (results.Any())
                    {
                        messsage = "";
                        return (List<Transaction>)results;
                    }
                    else
                    {
                        messsage = string.Format("Транзакций по такой книге ({0}, авт. {1}) нет", name, author);
                        return trans;
                    }
                }
            }
            catch (Exception ex)
            {
                messsage = ex.Message;
                return trans;
            }
        }

        public List<Transaction> GetTransactions(out string message)
        {
            List<Transaction> transactions = null;
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var tmp = db.GetCollection<Transaction>("Transactions");
                    transactions = tmp.FindAll().ToList();
                    message = "";
                    return transactions;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return transactions;
            }
        }
    }
}
