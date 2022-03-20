using DemoKhaled.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoKhaled.BL.Models
{
    public class DistinctVM
    {
        [Required(ErrorMessage = "Required")]
        public int Id { get; set; }
        public string Name { get; set; }

        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }
        public ICollection<Employee> Employee { get; set; }
    }
}
