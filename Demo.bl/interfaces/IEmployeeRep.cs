using DemoKhaled.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoKhaled.BL.interfaces
{
   public interface IEmployeeRep
    {
        IEnumerable<Employee> get();
        IEnumerable<Employee> SearchByName(string name);
        Employee GetById(int id);
        Employee create(Employee obj);


        Employee update(Employee obj);
        Employee delete(Employee obj);
    }
}
