using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{ 
    public class UserBs
    {
        private UserProvider userProvider;
        public UserBs()
        {
            userProvider = new UserProvider();

        }

        public List<User> GetAllUser()
        {
            return userProvider.GetAllUser();
        }

        public User GetUserByID(int id)
        {
            return userProvider.GetUserByID(id);
        }

        public void CreateUser(User user)
        {
            userProvider.CreateUser(user);
        }

        public void DeleteUser(int id)
        {
            userProvider.DeleteUser(id);
        }

        public void UpdateUser(User user)
        {
            userProvider.UpdateUser(user);
        }
    }
}
