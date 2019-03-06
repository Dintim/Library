using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Cons.Model
{
    public partial class ServiceMenu
    {
        public void CreateOrChangeReader()
        {
            Reader reader = new Reader();            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Создать/редактировать профиль читателя");
                Console.WriteLine("---------------------------------------------\n");
                Console.WriteLine("1. Зарегистрировать читателя");
                Console.WriteLine("2. Редактировать профиль читателя");
                Console.WriteLine("3. Выход");
                Console.Write("Ваш выбор: ");
                int ch = Int32.Parse(Console.ReadLine());
                if (ch == 1)
                    RegisterReaderMenu();
                else if (ch == 2)
                {
                    FindReader(reader);
                    UpdateReader(reader);
                }
                else if (ch == 3)
                    break;
                else
                    continue;
            }
        }

        public void CreateOrChangeBook()
        {

        }

        public void ReportsMenu()
        {

        }

        public void ChangeAdministratorPassword()
        {
            int k = 1;
            while (k != 4)
            {
                string msg = "";
                Console.Clear();
                Console.WriteLine("Изменение пароля");
                Console.WriteLine("---------------------------------------------\n");
                Console.Write("Новый пароль: ");
                string pass1 = Console.ReadLine();
                Console.Write("Повторите еще раз новый пароль: ");
                string pass2 = Console.ReadLine();
                if (pass1.Equals(pass2))
                {
                    serviceAdministrator.UpdateAdministratorPassword(AuthorAdmin, pass1, out msg);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(msg);
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(2000);
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Пароли не сходятся, введите заново");
                    Console.WriteLine("Попытка {0}", k);
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(2000);
                }
                k++;
            }
        }
    }
}
