using DemoKhaled.BL.Models;
using DemoKhaled.BL.Repository;
using DemoKhaled.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoKhaled.BL.interfaces;
using AutoMapper;

namespace DemoKhaled.Controllers
{
    public class DepartmentController : Controller
    {
        //deptRepo repo = new deptRepo();
        private readonly Idepartment repo;// dependency inversion 
        private readonly IMapper mapper;

        public DepartmentController(Idepartment department,IMapper mapper) // this line is called inject instance
        {
            mapper = mapper;

            this.repo = department;
            this.mapper = mapper;
            //repo = new deptRepo();
        }
        public IActionResult Index()
        {
            var data = repo.get();
            //             transfered      original
            var model =mapper.Map<IEnumerable<deptVm>>(data);// this line is essenial to deal with model(deptvm) as it is in html 
            return View(model);
        }
        [HttpGet]
        public IActionResult create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult create(deptVm obj)// can be (String Name,int code) but the html input can guess deptVm as well
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data=mapper.Map<Department>(obj);
                    repo.create(data);
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
            var model = mapper.Map<deptVm>(data);


            return View(model);
        }

        public IActionResult Update(int id)
        {
            var data = repo.GetById(id);
            var model =mapper.Map<deptVm>(data);
            return View(model);
        }
        [HttpPost]
        public IActionResult Update(Department obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data =mapper.Map<Department>(obj);
                    repo.update(data);
                    return RedirectToAction("Index");
                }
                return View(obj);
            }
            catch(Exception ex)
            {
                return View(obj);

            }

            
        }
        public IActionResult Delete(deptVm obj)
        {
            var data =mapper.Map<Department>(obj);
            repo.delete(data);


            return RedirectToAction("Index");
        }

    }
}
