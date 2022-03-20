using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoKhaled.DAL.Extensions
{
   public class UserApplication:IdentityUser
    {// here we must add migration to add new colum named IsAgree
        public bool IsAgree { get; set; }

    }
}
