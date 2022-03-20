using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoKhaled.DAL.Entities
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string code { get; set; }
        public ICollection<Employee> Employee { get; set; }
    }
}
