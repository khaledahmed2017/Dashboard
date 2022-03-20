using DemoKhaled.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DemoKhaled.BL.interfaces
{
   public interface ICityRep
    {
        IEnumerable<City> get(Expression<Func<City, bool>> filter = null);
      
        City GetById(int id);
        
    }
}
