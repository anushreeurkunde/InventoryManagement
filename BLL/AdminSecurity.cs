using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AdminSecurity
    {
        private readonly IInventoryContext _inventoryContext;
        public AdminSecurity(IInventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }

        public AdminSecurity():this(new InventoryContext())
        {

        }

        public bool Login(string username, string secret)
        {
            var result = from user in _inventoryContext.Users
                         join userRoles in _inventoryContext.UserRoles
                         on user.Id equals userRoles.UserId
                         where userRoles.Role == "Admin" && user.Name == username && user.Secret == secret
                         select user;
            return result.Any();
        }
    }
}
