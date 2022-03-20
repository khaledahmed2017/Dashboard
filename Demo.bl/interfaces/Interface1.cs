using DemoKhaled.BL.Models;
using DemoKhaled.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoKhaled.BL.interfaces
{
    public interface Idepartment
    {
         IEnumerable<Department> get();
        Department GetById(int id);
        void create(Department obj);

        void update(Department obj);
        void delete(Department obj);
    }
}
