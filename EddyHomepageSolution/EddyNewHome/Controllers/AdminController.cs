using EddyNewHome.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.Mvc;

namespace EddyNewHome.Controllers
{
    public class AdminController : Controller
    {
        EddyNewHomeEntities db = new EddyNewHomeEntities();

        // GET: Admin
        public ActionResult Index()
        {
            if ((Session["user_id"] != null && Session["user_id"].ToString() == "Admin") && 
                (Session["levels"] != null && Session["levels"].ToString() == "1"))
            {
                return View("Index", "_AdminLayout");
            }
            else
            {
                return RedirectToAction("../Home/Index");
            }
        }

        [HttpGet]
        public ActionResult Members()
        {
            IEnumerable<Members> list = db.Members.ToList();
            return View("Members", "_AdminLayout", list);
        }

        [HttpGet]
        public ActionResult MemberEdit(string memberId)
        {
            Members member = db.Members.Find(memberId);
            return View("MemberEdit", "_AdminLayout", member);
        }

        [HttpPost]
        public ActionResult MemberEdit(Members member)
        {
            //Members dbMember = db.Members.Where(m => m.MemberID == member.MemberID).FirstOrDefault(); 아래와 같은 구문
            Members origin = db.Members.Find(member.MemberID);
            try
            {
                origin.MemberName = member.MemberName;
                origin.MemberPWD = member.MemberPWD;
                origin.Email = member.Email;
                origin.Telephone = member.Telephone;

                db.Entry(origin).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                ViewBag.Result = "OK";
                return RedirectToAction("Members"); //성공하면 화면전환
            }
            catch (Exception)
            {
                ViewBag.Result = "FAIL";
                return View(origin);
            }
        }

        public  ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Members member)
        {
            Members admin = db.Members.Where(m => m.MemberID == member.MemberID)
                .Where(m => m.MemberPWD == member.MemberPWD)
                .Where(m => m.Level == "1").FirstOrDefault();
                
            if(admin == null)
            {
                ViewBag.Result = "FAIL";
                return View(member);
            }
            else
            {
                Session["user_id"] = admin.MemberID;
                Session["levels"] = admin.Level;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Logout()
        {
            Session["user_id"] = String.Empty;
            Session["levels"] = String.Empty;

            return RedirectToAction("../Home/Index");
        }
    }
}