using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoKhaled.BL.Models
{
   public class LoginVm
    {
        [Required(ErrorMessage = "Email Required")]
        [EmailAddress(ErrorMessage = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Required")]
        [MinLength(6, ErrorMessage = "Min length is 6")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
