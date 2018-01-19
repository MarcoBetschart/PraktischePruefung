using System;
using System.Web.Mvc;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using Pruefung_Praktisch_Musterloesung.Models;
using System.Text.RegularExpressions;

namespace Pruefung_Praktisch_Musterloesung.Controllers
{
    public class Lab4Controller : Controller
    {

        /**
        * 
        * ANTWORTEN BITTE HIER
        * 
		* Weil man so potentielle Angreifer finden kann
		* 
        * */

        public ActionResult Index() {

            Lab4IntrusionLog model = new Lab4IntrusionLog();
            return View(model.getAllData());   
        }

		public ActionResult getIp()
		{
			Lab4IntrusionLog model = new Lab4IntrusionLog();
			return View(model.getIpCount());
		}

		[HttpPost]
        public ActionResult Login()
        {
            var username = Request["username"];
            var password = Request["password"];
			var model = new Lab4IntrusionLog();

			// Return true if strIn is in valid e-mail format.
			try
			{
				Regex.IsMatch(username,
					  @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
					  @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
					  RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
			}
			catch (RegexMatchTimeoutException)
			{
				model.logIntrusion(Request.UserHostAddress, Request.Browser.Platform, "Not valid E-Mail");
			}

			if (password.Length >9 && password.Length < 21 && Regex.IsMatch(password, @"[^a-zA-Z0-9\s]"))
			{
				return View(model.logIntrusion(Request.UserHostAddress, Request.Browser.Platform, "Password too long or too short"));
			}

			bool intrusion_detected = false;
        
            // Hints
            // Request.Browser.Platform;
            // Request.UserHostAddress;
			

            // Hint:
            //model.logIntrusion();

            if (intrusion_detected)
            {
                return RedirectToAction("Index", "Lab4");
            }
            else
            {
                // check username and password
                // this does not have to be implemented!
                return RedirectToAction("Index", "Lab4");
            }
        }
    }
}