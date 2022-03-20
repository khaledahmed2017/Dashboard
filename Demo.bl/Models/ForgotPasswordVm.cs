using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoKhaled.BL.Models
{
    public class ForgotPasswordVm
    {
        [Required(ErrorMessage = "Email Required")]
        [EmailAddress(ErrorMessage = "Email")]
        public string Email { get; set; }
    }
}
