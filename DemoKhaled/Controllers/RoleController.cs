using DemoKhaled.BL.Models;
using DemoKhaled.DAL.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoKhaled.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<UserApplication> userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<UserApplication> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            var Roles = roleManager.Roles;
            return View(Roles);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole Role)
        {
            var role = new IdentityRole()
            {
                Name = Role.Name,
                NormalizedName = Role.Name.ToUpper()
            };
            var Result = await roleManager.CreateAsync(role);
            if (Result.Succeeded)
            {
               return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in Result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View(Role);
        }
        public async Task<IActionResult> Edit(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IdentityRole model)
        {

            var role = await roleManager.FindByIdAsync(model.Id);

            role.Name = model.Name;
            role.NormalizedName = model.Name.ToUpper();

            var result = await roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }


            return View(model);
        }
        public async Task<IActionResult> AddOrRemoveUsers(string RoleId)
        {

            ViewBag.RoleId = RoleId;// we add ViewBag to use it to go back to Edit this role bag if you cancle

            var role = await roleManager.FindByIdAsync(RoleId);//get the role

            var model = new List<UserInRoleVm>();//list the all users

            foreach (var user in userManager.Users)
            {
                var userInRole = new UserInRoleVm()
                {
                    Id = user.Id,
                    Name = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))//if any user is assigned to role mark it
                {
                    userInRole.IsSelected = true;
                }
                else
                {
                    userInRole.IsSelected = false;
                }

                model.Add(userInRole);//add all users in the list one by one
            }

            return View(model);

        }



        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(List<UserInRoleVm> model, string RoleId)//RoleId for <input type="hidden" asp-for="@Model[i].Id" />

        {
            //<input type="hidden" asp-for="@Model[i].Id" /> 
            //This Line for make sure that these operations for only this instance
            var role = await roleManager.FindByIdAsync(RoleId);

            for (int i = 0; i < model.Count; i++)
            {

                var user = await userManager.FindByIdAsync(model[i].Id);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {

                    result = await userManager.AddToRoleAsync(user, role.Name);

                }
                else if (!model[i].IsSelected && (await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                //else
                //{
                //    continue;
                //}

                //if (i < model.Count)
                //    continue;


            }

            return RedirectToAction("Edit", new { id = RoleId });
        }

    }
}
