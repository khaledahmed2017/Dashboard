using DemoKhaled.BL.interfaces;
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
    public class EmployeeRep : IEmployeeRep
    {
        Democontext db;//to use the department property in class context
        public EmployeeRep(Democontext demo)
        {
            db = demo;
        }

        //public void create(deptVm obj)
        public Employee create(Employee obj)
        {

            db.Employee.Add(obj);
            db.SaveChanges();
            return db.Employee.OrderBy(a => a.Id).FirstOrDefault();
        }

        public IEnumerable<Employee> get()
        {
            var data = db.Employee.Include("Department").Select(a => a);
            return data;
        }
        public IEnumerable<Employee> SearchByName(string name)
        {
            var data = db.Employee.Include("Department").Where(a => a.Name.Contains(name));
            return data;
        }
        public Employee GetById(int id)
        {
            var data = db.Employee.Where(a => a.Id == id).FirstOrDefault();
            return data;
        }

        public Employee delete(Employee obj)
        {
            db.Employee.Remove(obj);
            db.SaveChanges();
            return db.Employee.Find(obj.Id);

        }

        public Employee update(Employee obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            
            db.SaveChanges();
            return db.Employee.Find(obj.Id);
        }

       
    }
}
