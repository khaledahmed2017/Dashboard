using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoKhaled.DAL.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [MaxLength(2)]
        public string two_char_counrty { get; set; }
        [MaxLength(3)]
        public string three_char_counrty { get; set; }
        public ICollection<City> City { get; set; }

    }
}
