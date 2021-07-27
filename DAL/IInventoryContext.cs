using DomainModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IInventoryContext : IDisposable
    {
        DbSet<Product> Products { get; }

        DbSet<User> Users { get; set; }

        DbSet<UserRole> UserRoles { get; set; }
        Task<int> SaveChangesAsync();
        void MarkAsModified(Product item);

    }
}
