using DomainModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class InventoryContext : DbContext, IInventoryContext
    {
        public InventoryContext() : base("InventoryContext")
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public void MarkAsModified(Product item)
        {
            Entry(item).State = EntityState.Modified;
        }
    }
}
