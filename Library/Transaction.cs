using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public enum TransactionType { issueBook, returnBook }

    public struct TransactionShort
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ReaderId { get; set; }
        public string ReaderFullName { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public TransactionType TransactionType { get; set; }
    }
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Reader Reader { get; set; }
        public Book Book { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
