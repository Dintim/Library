using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Cons.Model
{
    public partial class ServiceMenu
    {
        public void ReaderRegisterMenu()
        {
            string msg = "";
            Reader reader = new Reader();
            Console.WriteLine("Форма регистрации для читателя:");
            Console.WriteLine("---------------------------------------------\n");
            Console.Write("Имя: ");
            reader.Name = Console.ReadLine();
            Console.Write("Фамилия: ");
            reader.Surname = Console.ReadLine();
            while (true)
            {
                Console.Write("Email: ");
                string tmp = Console.ReadLine();
                if ((!tmp.Contains("@") && (!tmp.Contains(".kz") || !tmp.Contains(".ru") || !tmp.Contains(".com") || !tmp.Contains(".org"))) || (!tmp.Contains("@") || tmp[0] == '@'))
                    continue;
                else
                {
                    reader.Email = tmp;
                    break;
                }
            }
            while (true)
            {
                Console.Write("Логин: ");
                string tmp = Console.ReadLine();
                var readersList = serviceReader.GetReaders(out msg);
                if (readersList.Find(f => f.Login.Equals(tmp)) != null)
                {
                    Console.WriteLine("Читатель с таким логином уже существует");
                    continue;
                }
                else
                {
                    reader.Login = tmp;
                    break;
                }
            }
            Console.Write("Пароль: ");
            reader.Password = Console.ReadLine();

            serviceReader.AddReaderToDB(reader, out msg);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void AdministratorRegisterMenu()
        {

        }
    }
}
