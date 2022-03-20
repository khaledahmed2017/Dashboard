using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using DemoKhaled.DAL.Extensions;

namespace DemoKhaled.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<UserApplication> userManager;

        public UserController(UserManager<UserApplication> userManager)//no identityuser
        {
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            var users = userManager.Users;
            return View(users);

        }
        public async Task<IActionResult> Edit(string id)
        {
            var model = await userManager.FindByIdAsync(id);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserApplication model)
        {       
            try
            {
                if (ModelState.IsValid)
                {

                    var user = await userManager.FindByIdAsync(model.Id);
                    user.Email = model.Email;
                    user.UserName = model.UserName;
                    // We must always Edit Normalization Email&UserName after Edit Email&UserName
                    user.NormalizedEmail = model.Email.ToUpper();
                    user.NormalizedUserName = model.UserName.ToUpper();
                    var result = await userManager.UpdateAsync(user)    ;
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("",item.Description);

                        }
                    }
                }
                return View(model);

            }catch(Exception ex)
            {
                return View(model);

            }

        }
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var user = await userManager.FindByIdAsync(id);
                var result = await userManager.DeleteAsync(user);
                return RedirectToAction("Index");

            }
            catch(Exception ex)
            {
                return RedirectToAction("Index");
            }

        }
    }
}
