using DemoKhaled.BL.Helper;
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
    public class AccountController : Controller
    {
        private readonly SignInManager<UserApplication> signInManager;
        private readonly UserManager<UserApplication> usermanger;

        public AccountController(SignInManager<UserApplication> signInManager, UserManager<UserApplication> usermanger)
        {
            this.signInManager = signInManager;
            this.usermanger = usermanger;
        }

        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new UserApplication()
                    {
                        Email = model.Email,
                        UserName = model.Email,
                        IsAgree = model.IsAgree
                    };
                    // We pass the password separate to make it hash
                    var result = await usermanger.CreateAsync(user, model.Password);// await is akey to Async process which means it normally to deal with the return from userManager before it be run 

                    if (result.Succeeded)// here we used result.Succeeded before it can be run 
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        foreach (var item in result.Errors)// result
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        #endregion
        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid UserName Or Password");

                    }
                }
                return View(model);
            }catch(Exception ex)
            {
                return View(model);
            }
           
           
        }
        #endregion

  
        #region Signout
        [HttpGet]
        public async Task<IActionResult> Signout()
        {
           await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        #endregion
        #region Forget password
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgotPasswordVm model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await usermanger.FindByEmailAsync(model.Email);

                    if (user != null)
                    {
                        var token = await usermanger.GeneratePasswordResetTokenAsync(user);

                        var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);
                                                                                        // combine the two field together

                        MailSender.SendMail(new MailMV() { Mail = model.Email, Title = "Reset Password", Message = passwordResetLink });

                        //logger.Log(LogLevel.Warning, passwordResetLink);

                        return RedirectToAction("ConfirmForgetPassword");
                    }
                    return View(model);
                }

            }catch(Exception ex)
            {
                return View(model);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult ConfirmForgetPassword()
        {
            // Password reset Email has been sent check your inbox!
            return View();
        }
        #endregion
        #region resetpassword
        [HttpGet]
        public IActionResult ResetPassword(string Email,string Token)
            //here we recieve both Email,Token from resetLink to be sent to post as Well 
            //but must be a hidden inputs to bind to post by form 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVm model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await usermanger.FindByEmailAsync(model.Email);

                    if (user != null)
                    {
                        var result = await usermanger.ResetPasswordAsync(user,model.Token,model.Password);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("ConfirmResetPassword");
                        }
                        else {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }

                            return View(model);
                        }

                        
                    }
                    return View(model);

                }
                return View(model);

            }
            catch (Exception ex)
            {

                return View(model);

            }
            return View();
        }
        [HttpGet]
        public IActionResult ConfirmResetPassword()
        {
            return View();
        }
        #endregion

    }
}