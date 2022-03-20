using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoKhaled.DAL.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public double salary { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
        public string Emial { get; set; }
        public string Address { get; set; }
        public int DepartmentId { get; set; }// by convention will guess that is the forign key 
        //becuase it's name is Department+Id 

        public Department Department { get; set; }//navigation property
        public int DistrictId { get; set; }
        [ForeignKey("DistrictId")]
        public District Distinct { get; set; }
        public string PhotoName { get; set; }
        public string CvName { get; set; }
    }
}
