using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public enum TransactionType { issueBook, returnBook }
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Reader Reader { get; set; }
        public Book Book { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
