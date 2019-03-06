using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class ServiceAdministrator
    {
        public bool AddAdminToDB(Administrator admin, out string message)
        {
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var administrators = db.GetCollection<Administrator>("Administrators");
                    administrators.Insert(admin);
                }
                message = string.Format("Администратор {0} добавлен в базу успешно", admin.Id);
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        public bool DeleteAdminFromDB(Administrator admin, out string message)
        {
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var administrators = db.GetCollection<Administrator>("Administrators");
                    administrators.Delete(d => d.Equals(admin));
                }
                message = string.Format("Администратор {0} удален из базы успешно", admin.Id);
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        public Administrator LogOnAdmin(string login, string password, out string message)
        {
            Administrator admin = null;
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var administrators = db.GetCollection<Administrator>("Administrators");
                    var results = administrators.Find(f => f.Login.Equals(login) && f.Password.Equals(password));
                    if (results.Any())
                    {
                        message = "";
                        return results.FirstOrDefault();
                    }
                    else
                    {
                        message = "Неправильный логин или пароль";
                        return admin;
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return admin;
            }
        }

        public List<Administrator> GetAdministrators(out string message)
        {
            List<Administrator> admins = null;
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var tmp = db.GetCollection<Administrator>("Administrators");
                    admins = tmp.FindAll().ToList();
                    message = "";
                    return admins;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return admins;
            }
        }
    }
}
