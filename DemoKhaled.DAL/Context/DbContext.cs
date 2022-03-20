using DemoKhaled.DAL.Entities;
using DemoKhaled.DAL.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoKhaled.DAL.Context
{
    public class Democontext:IdentityDbContext<UserApplication>
    {
        public Democontext(DbContextOptions opt):base(opt)
        {
                
        }
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<City> City { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("server=.;database=DemoDb;Integrated Security=true");
        //}
    }
}
