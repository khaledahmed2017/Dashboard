using DemoKhaled.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace DemoKhaled.BL.Models
{
    public class CityVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Required")]
        public int CountryId { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
        //public ICollection<District> District { get; set; }
        public ICollection<District> District { get; set; }
    }
}
