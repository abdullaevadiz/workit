using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Entities;

namespace Core
{
    public static class Users
    {
        public static bool AddUser(User user)
        {
            using (var db = new WorkitContext())
            {
                var newUser = new User
                                  {
                                      Id = Guid.NewGuid(),
                                      Email = user.Email.ToLower(),
                                      IsActive = false,
                                      Password = user.Password,
                                      RegistrationDate = DateTime.Now
                                  };
                newUser.Token = newUser.Id.ToString().Replace("-", string.Empty).Take(10).ToString().ToLower();

                try
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Loggers.ExeptionLogger.AddExeption(ex, "AddUser", db);
                    return false;
                }
                
                //TODO: Отправка сообщения о подвтерждении
            }
        }

        public static bool ActivateUser(string token)
        {
            token = token.ToUpper();
            using (var db = new WorkitContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Token == token);
                if (user == null) return false;

                user.IsActive = true;
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    Loggers.ExeptionLogger.AddExeption(ex, "ActivateUser", db);
                    return false;
                }
            }
        }
    
        public static bool MailIsFree(string email)
        {
            email = email.ToLower();
            using (var db = new WorkitContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Email == email);
                return (user == null);
            }
        }
    
        public static User GetUserById(Guid userId)
        {
            using (var db = new WorkitContext())
            {
                return db.Users.FirstOrDefault(u => u.Id == userId);
            }
        }

        public static IQueryable GetAllUsers()
        {
            using (var db = new WorkitContext())
            {
                return db.Users.OrderByDescending(u => u.RegistrationDate);
            }
        }
    
        public static User ChecAkuth(User user)
        {
            using (var db = new WorkitContext())
            {
                user.Email = user.Email.ToLower();
                return db.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
            }
        }
    }
}
