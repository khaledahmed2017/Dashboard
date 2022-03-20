using DemoKhaled.BL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using DemoKhaled.BL.Helper;

namespace DemoKhaled.Controllers
{
    public class MailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(MailMV model)
        {
            try {
                TempData["message"]= MailSender.SendMail(model);
                return View();
            }catch(Exception ex)

            {
                TempData["message"]= MailSender.SendMail(model);
                return View();
            }
           
           
        }

    }
}
