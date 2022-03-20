using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoKhaled.Controllers
{
    public class LangugeController : Controller
    {
        [HttpGet]//it is here HttpGet because we get the languege from the Buttons then go to the server by this action and get the required languge
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }// after one year the langue will expired from cookies
            );

            return LocalRedirect(returnUrl);//return to the same page
        }

    }
}
