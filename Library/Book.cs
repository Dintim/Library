using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public struct BookType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
    public enum BookStatus { free, busy }
    public class Book
    {
        public int Id { get; set;  }
        public string ISDN { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public BookType BookType { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }
        public int Edition { get; set; }
        public BookStatus BookStatus { get; set; }
        public Guid ReaderId { get; set; }
    }
}
