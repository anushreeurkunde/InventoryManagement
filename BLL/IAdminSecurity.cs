using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IAdminSecurity
    {
        bool Login(string username, string secret);
    }
}
