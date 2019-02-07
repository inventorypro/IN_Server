using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventoryDataAccess;
namespace InventoryService
{
    public class UserSecurity
    {
        public static bool Login(string username, string password)
        {

            using (InventoryDBEntities entities = new InventoryDBEntities())
            {
                return entities.UserLogins.Any(user => user.ESSUSR_Name.Equals(username,
                 StringComparison.OrdinalIgnoreCase) && user.ESSURS_Password == password);


            }

        }
    }
}