using DemoKhaled.BL.interfaces;
using DemoKhaled.DAL.Context;
using DemoKhaled.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DemoKhaled.BL.Repository
{
    public class DistinctRepo : IDistinctRep
    {
        Democontext db;
        public DistinctRepo(Democontext demo)
        {
            db = demo;
        }
        public IEnumerable<District> get(Expression<Func<District, bool>> filter = null)
        {
            if (filter == null)
            {

                var data = db.District.Select(a => a);
                return data;
            }
            else
            {
                return db.District.Where(filter);
            }
        }

        public District GetById(int id)
        {
            var data = db.District.Where(a => a.Id == id).FirstOrDefault();
            return data;
        }
    }
}
