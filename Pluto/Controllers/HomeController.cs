using Pluto.Models;
using Pluto.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Pluto.Models.Tempon;

namespace Pluto.Controllers
{
    public class HomeController : Controller
    {
        private IAuthentificationRepository _authentificationRepository;   
        public HomeController(IAuthentificationRepository authentificationRepository)
        {
            _authentificationRepository = authentificationRepository;

        }
        public ActionResult Index()
        {
            Session["token"] = null;
            Session["id"] = null;
            Session["structureID"] = null;
            Session["fullName"] = null;
            Session["name_logo"] = null;
            return View();
        }
        [HttpPost]
        public JsonResult activation(string code)
        {

            try
            {

                Reponse reponse = _authentificationRepository.activationCode(code);
                if (reponse.code == 200)
                {
                    var userGot = Utils.ToObject<User>(reponse.result);
                    Session["token"] = userGot.monToken;
                    Session["id"] = userGot.id;
                    Session["structureID"] = userGot.structureID;
                    Session["fullName"] = userGot.prenom +" "+ userGot.nom;
                    Session["name_logo"] = userGot.name_logo;
                    return Json(new { code = 200, result = reponse.result, message = reponse.message});
                }
                else
                {
                    return Json(new { code = reponse.code, result = false, message = reponse.message});
                }

            }
            catch (Exception)
            {
                return Json(new { code = 500, result = false, message = "une erreur interne est survenue"});


            } 
        }

        [HttpPost]
        public JsonResult Seconnecter(string phone, string pwd)
        {

          try
            {

                Reponse reponse = _authentificationRepository.Seconnecter(phone, pwd);
                if (reponse.code == 200)
                {
					var userGot = Utils.ToObject<User>(reponse.result);
					Session["token"] = userGot.monToken;
					Session["id"] = userGot.id;
					Session["structureID"] = userGot.structureID;
					Session["fullName"] = userGot.prenom + " " + userGot.nom;
					Session["name_logo"] = userGot.name_logo;

					return Json(new { code = 200,result = reponse.result, message=reponse.message });
                }
                else
                {
                    return Json(new { code = reponse.code, result = false, message = reponse.message });
                }

           }
            catch (Exception)
            {
                return Json(new { code = 500, result = false, message = "une erreur interne est survenue  coté client" });


            }  
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}