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
    public  class CityRepo : ICityRep
    {
        Democontext db;
        public CityRepo(Democontext demo)
        {
            db = demo;
        }
        public IEnumerable<City> get(Expression<Func<City,bool>> filter =null)
        {
            if (filter == null)
            {

                var data = db.City.Select(a => a);
                return data;
            }
            else
            {
               return db.City.Where(filter);
            }
        }

        public City GetById(int id)
        {
            var data = db.City.Where(a => a.Id == id).FirstOrDefault();
            return data;
        }
    }
}
