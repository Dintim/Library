using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Cons
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write(": ");
            bool x = Convert.ToBoolean(Int32.Parse(Console.ReadLine()));
            Console.WriteLine(x);
        }
    }
}
