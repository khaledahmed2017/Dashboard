using DemoKhaled.BL.interfaces;
using DemoKhaled.BL.Models;
using DemoKhaled.DAL.Context;
using DemoKhaled.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoKhaled.BL.Repository
{
    public class deptRepo : Idepartment
    {
        Democontext db ;//to use the department property in class context
        public deptRepo(Democontext demo)
        {
            db = demo;  
        }

        //public void create(deptVm obj)
        public void create(Department obj)
        {
           
            db.Department.Add(obj);
            db.SaveChanges();
        }

        public IEnumerable<Department> get()
        {
            var data = db.Department.Select(a=>a);
            return data;
        }

        public Department GetById(int id)
        {
            var data = db.Department.Where(a => a.Id==id).FirstOrDefault();
            return data;
        }

        public void delete(Department obj)
        {
            db.Department.Remove(obj);
            db.SaveChanges();
        }

        public void update(Department obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
