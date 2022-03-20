using DemoKhaled.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoKhaled.BL.Models
{
    public class CountryVm
    {
        [Required(ErrorMessage = "Required")]
        public int Id { get; set; }
        public string Name { get; set; }
        [MaxLength(2,ErrorMessage ="max length 2")]
        public string two_char_counrty { get; set; }
        [MaxLength(3,ErrorMessage = "max length 3")]
        public string three_char_counrty { get; set; }
        public ICollection<City> City { get; set; }

    }
}
