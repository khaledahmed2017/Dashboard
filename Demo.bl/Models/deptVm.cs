using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoKhaled.BL.Models
{
    public class deptVm
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="required")]
        [MaxLength(50,ErrorMessage ="max 50 characters")]
        [MinLength(3,ErrorMessage ="min 3 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage ="required")]
        public string code { get; set; }
    }
}
