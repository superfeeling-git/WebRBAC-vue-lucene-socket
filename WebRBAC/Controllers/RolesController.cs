using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebRBAC.Models;
using System.Data.Entity;

namespace WebRBAC.Controllers
{
    public class RolesController : Controller
    {
        RBACContext db = new RBACContext();
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.sysList = new ClassHelper().getAllClass;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Roles roles,FormCollection form)
        {            
            if(ModelState.IsValid)
            {
                //var idList = form["idList"].Split(',');
                //roles.RolesClass = new List<RolesClass>();                
                db.Roles.Add(roles);
                //roles.RolesClass = new List<RolesClass> { new RolesClass { ClassID = 1 } };
                roles.RolesClass.Add(new RolesClass { ClassID = 1 });
                db.SaveChanges();
            }
            
            return RedirectToAction("Create");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(db.Roles.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Roles Roles)
        {
            if(ModelState.IsValid)
            {
                db.Entry<Roles>(Roles).State = EntityState.Added;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Test()
        {
            return View();
        }
    }
}