using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Role { get; set; }
    }
}
