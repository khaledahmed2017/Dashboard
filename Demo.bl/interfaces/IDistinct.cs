using DemoKhaled.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DemoKhaled.BL.interfaces
{
   public interface IDistinctRep
    {
        IEnumerable<District> get(Expression<Func<District, bool>> filter = null);

        District GetById(int id);
    }
}
