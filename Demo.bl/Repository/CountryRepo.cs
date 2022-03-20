using DemoKhaled.BL.interfaces;
using DemoKhaled.DAL.Context;
using DemoKhaled.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoKhaled.BL.Repository
{
    public class CountryRepo : ICountryRep
    {
        Democontext db;
        public CountryRepo(Democontext demo)
        {
            db = demo;
        }

        public IEnumerable<Country> get()
        {
            var data = db.Country.Select(a => a);
            return data;
        }

        public Country GetById(int id)
        {
            var data = db.Country.Where(a => a.Id == id).FirstOrDefault();
            return data;
        }
    }
}
