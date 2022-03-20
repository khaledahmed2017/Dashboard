using DemoKhaled.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoKhaled.BL.interfaces
{
   public interface ICountryRep
    {
        IEnumerable<Country> get();

        Country GetById(int id);
    }
}
