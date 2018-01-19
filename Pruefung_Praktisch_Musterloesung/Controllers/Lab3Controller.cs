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
    public class Lab3Controller : Controller
    {

		/**
        * 
        * ANTWORTEN BITTE HIER
		* 
		* 1. SQL Injection und Stored XSS	
		* 
		* 2. SQL Injection: 
		* Nutzer gibt in Textbox etwas an, dass die rückgabe so angepasst wird, dass er zum Beispiel alle Einträge sieht.
		* Z.B: OR 1=1 --> gibt alle Einträge zurück
		* 
		* Stored XSS: Einschleusen und Speichern von Javascript-Code in die Datenbank z.B. via Comment function
        * Z.B ’<script> Irgendwas machen </script>
        * */

		public ActionResult Index() {

            Lab3Postcomments model = new Lab3Postcomments();

            return View(model.getAllData());
        }

        public ActionResult Backend()
        {
            return View();
        }

        [ValidateInput(false)] // -> we allow that html-tags are submitted!
        [HttpPost]
        public ActionResult Comment()
        {
            var comment = Request["comment"];
            var postid = Int32.Parse(Request["postid"]);

			var regex = new Regex("(\\<script(.+?)\\</script\\>)|(\\<style(.+?)\\</style\\>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
			comment = regex.Replace(comment, "");
			

			Lab3Postcomments model = new Lab3Postcomments();

            if (model.storeComment(postid, comment))
            {  
                return RedirectToAction("Index", "Lab3");
            }
            else
            {
                ViewBag.message = "Failed to Store Comment";
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login()
        {
            var username = Request["username"];
            var password = Request["password"];

            Lab3User model = new Lab3User();

            if (model.checkCredentials(username, password))
            {
                return RedirectToAction("Backend", "Lab3");
            }
            else
            {
                ViewBag.message = "Wrong Credentials";
                return View();
            }
        }
    }
}