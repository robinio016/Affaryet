using BOL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserProvider
    {
        public UserProvider()
        {
        }

        public List<User> GetAllUser()
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                var res = db.Users.ToList();
                return res;
            }
        }

        public User GetUserByID(int id)
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                return db.Users.Find(id);
            }
        }

        public void CreateUser(User user)
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public void DeleteUser(int id)
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }

        public void UpdateUser(User user)
        {
            using (VientVendreDBContext db = new VientVendreDBContext())
            {
                try
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch(Exception ex)
                {
                    throw;
                }
            }
        }


    }
}
