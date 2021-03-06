﻿using System;
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
        public string Code { get; set; }
        public string Name { get; set; }        
        public BookType BookType { get; set; }
        public string Author { get; set; }
        public int PublishDate { get; set; }
        public int Edition { get; set; }
        public BookStatus BookStatus { get; set; }        

        public Book()
        {
            BookStatus = BookStatus.free;
        }

        public override string ToString()
        {
            return string.Format("ID: {0}\tISDN: {1}\tНазвание: {2}\tАвтор: {3}\tДата публикации: {4}\tЖанр: {5}\tСтатус: {6}",
                Id, ISDN, Name, Author, PublishDate, BookType, BookStatus);
        }
    }
}
