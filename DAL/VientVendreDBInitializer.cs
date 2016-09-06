using BOL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    //cette classe est utlisée pour recréation de la base en cas ou le modèle subit un changement (alter table)
    //on utilise généralement une ou plus parmis les strategies proposées par entityframework :ifdatabasenotexist,
    public class VientVendreDBInitializer : DropCreateDatabaseIfModelChanges<VientVendreDBContext>
    {
        //cette methode est utilisé
        protected override void Seed(VientVendreDBContext context)
        {

            //IList<User> defaultUsers = new List<User>();
            //defaultUsers.Add(new User() { UserID = 1, UserName = "Amine", Email = "robinio016@outlook.com" });
            //foreach (User std in defaultUsers)
            //    context.Users.Add(std);


            base.Seed(context);
        }
    }
}
