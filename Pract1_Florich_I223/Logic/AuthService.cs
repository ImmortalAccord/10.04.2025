using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pract1_Florich_I223.dbContext;
using Pract1_Florich_I223.Model;

namespace Pract1_Florich_I223.Logic
{
    public class AuthService : IAuthService
    {
        private ShopDBEntities1 dbContext = new ShopDBEntities1();

        public bool CheckData(string Login, string Pass)
        { 

            var user = dbContext.Users.FirstOrDefault(u => u.Login == Login && u.Pass == Pass);

            if (user != null && user.Pass == Pass)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
