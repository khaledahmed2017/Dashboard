using AutoMapper;
using DemoKhaled.BL.Helper;
using DemoKhaled.BL.interfaces;
using DemoKhaled.BL.Models;
using DemoKhaled.BL.Repository;
using DemoKhaled.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DemoKhaled.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ICountryRep country;
        private readonly ICityRep city;
        private readonly IDistinctRep distinct;

        //deptRepo repo = new deptRepo();
        private readonly IEmployeeRep repo;// dependency inversion 
        private readonly Idepartment department;
        private readonly IMapper mapper;
        

        public EmployeeController(ICountryRep country, ICityRep city, IDistinctRep distinct, IEmployeeRep employee, Idepartment department, IMapper mapper) // this line is called inject instance
        {
            this.mapper = mapper;
            this.country = country;
            this.city = city;
            this.distinct = distinct;
            this.repo = employee;
            this.department = department;
            this.mapper = mapper;
            //repo = new deptRepo();
        }
        public IActionResult Index(string SearchValue = "")//the input field in html have name of the same name of SearchValue
        {
            if (SearchValue == "")
            {
                var data = repo.get();
                //             transfered      original
                var model = mapper.Map<IEnumerable<EmployeeVm>>(data);// this line is essenial to deal with model(deptvm) as it is in html 
                return View(model);

            }
            else
            {
                var data = repo.SearchByName(SearchValue);
                //             transfered      original
                var model = mapper.Map<IEnumerable<EmployeeVm>>(data);// this line is essenial to deal with model(deptvm) as it is in html 
                return View(model);
            }

        }
        [HttpGet]
        public IActionResult create()
        {
            ViewBag.departmentList = new SelectList(department.get(), "Id", "Name");// option must have name and value

            return View();
        }
        [HttpPost]
        public IActionResult create(EmployeeVm obj)// can be (String Name,int code) but the html input can guess deptVm as well
        {
            

            try
            {
                if (ModelState.IsValid)
                {

                    string imgName = UplaodFile.uploadfile("/wwwroot/files/imges", obj.Photo);
                    string CvName = UplaodFile.uploadfile("/wwwroot/files/cv", obj.CV);
                    obj.PhotoName = imgName;
                    obj.CvName = CvName;


                    var data = mapper.Map<Employee>(obj);
                  
                    repo.create(data);
                    ViewBag.departmentList = new SelectList(department.get(), "Id", "Name");// option must have name and value
                    return RedirectToAction("Index");
                }
                return View(obj);
            }
            catch
            {
                return View(obj);
            }
        }
        public IActionResult detailes(int id)
        {

            var data = repo.GetById(id);
            //var CountryData = country.GetById(id);
            var model = mapper.Map<EmployeeVm>(data);
            //ViewBag.distictList = new SelectList((ICollection)distinct.GetById(model.DistrictId), "Id", "Name", model.DistrictId);
            ViewBag.departmentList = new SelectList(department.get(), "Id", "Name", model.DepartmentId);// option must have name and value
            //ViewBag.DestrictList = new SelectList(distinct.get(), "Id", "Name", data.DistrictId);// option must have name and value
            //ViewBag.CityList = new SelectList(city.get(), "Id", "Name", );// option must have name and value

            return View(model);
        }

        public IActionResult Update(int id)
        {
            var data = repo.GetById(id);
            var model = mapper.Map<EmployeeVm>(data);
            ViewBag.departmentList = new SelectList(department.get(), "Id", "Name", model.DepartmentId);// option must have name and value

            return View(model);
        }
        [HttpPost]
        public IActionResult Update(EmployeeVm obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Employee>(obj);
                    repo.update(data);

                    return RedirectToAction("Index");
                }
                ViewBag.departmentList = new SelectList(department.get(), "Id", "Name", obj.DepartmentId);// option tag in html must have name and value

                return View(obj);
            }
            catch (Exception ex)
            {
                return View(obj);

            }


        }
        public IActionResult Delete(int id)// we receive an id from asp-route-id in Index View  
        {
            
            var emp = repo.GetById(id);
            //var data = mapper.Map<Employee>(emp);
           
            UplaodFile.Removefile("/wwwroot/files/imges/", emp.PhotoName);
            UplaodFile.Removefile("/wwwroot/files/cv/", emp.CvName);
            repo.delete(emp);


            return RedirectToAction("Index");
        }
        #region Ajex Requests
        [HttpPost]//we write that to be sure that when we call this action it will not write the data on the browser and will be security
        public JsonResult GetCityDataByCountryId(int ctrId)
        {
            //var data = city.get().Where(a => a.CountryId == ctrId);
            // this line is bad performance 
            var data = city.get(a => a.CountryId == ctrId);// here we make get be used like (linqo) 
            var model = mapper.Map<IEnumerable<CityVM>>(data);

            return Json(model);



        }
        [HttpPost]
        public JsonResult GetDistinctDataByCityId(int cityId)
        {
            //var data = city.get().Where(a => a.CountryId == ctrId);
            // this line is bad performance
            var data = distinct.get(a => a.CityId == cityId);
            var model = mapper.Map<IEnumerable<DistinctVM>>(data);

            return Json(model);



        }
        #endregion


    }
}
