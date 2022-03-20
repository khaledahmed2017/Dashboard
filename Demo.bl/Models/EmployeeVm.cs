using DemoKhaled.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoKhaled.BL.Models
{
    public class EmployeeVm
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "required")]
        [MaxLength(50, ErrorMessage = "max 50 characters")]
        [MinLength(3, ErrorMessage = "min3 characters")]
        public string Name { get; set; }
        [Required]
        [Range(2000,10000,ErrorMessage ="Range 2000:10000")]
        public double salary { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
        [EmailAddress(ErrorMessage ="Mail invalid")]
        public string Emial { get; set; }
        //12Strit-city-country
        [RegularExpression("[0-9]{2,8}[a-zA-z]{2,8}-[a-zA-z]{2,8}-[a-zA-z]{2,8}",ErrorMessage ="Address must be as that 12street-city-country")]
        public string Address { get; set; }
        [Required(ErrorMessage ="Choose Department")]
        public int DepartmentId { get; set; }

        public int DistrictId { get; set; }
        [ForeignKey("DistrictId")]
        public District Distinct { get; set; }
        public string PhotoName { get; set; }
        public string CvName { get; set; }
        public IFormFile CV { get; set; }
        public IFormFile Photo { get; set; }
    }
}
