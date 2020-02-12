using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebRBAC.Models;

namespace WebRBAC.Controllers
{
    public class sysClassManageController : Controller
    {
        RBACContext db = new RBACContext();

        // GET: sysClassManage
        public ActionResult Index()
        {
            return View(new ClassHelper().getAllClass);
        }

        
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(sysClass model)
        {
            if(model.ClassID == 0)
            {
                model.ParentID = 0;
                model.ParentPath = "0";
                model.Depth = 0;
            }
            else
            {
                int classid = model.ClassID;
                var parent = db.sysClass.First(m => m.ClassID == classid);
                model.ParentID = classid;
                model.ParentPath = $"{parent.ParentPath},{parent.ClassID}";
                model.Depth = parent.Depth + 1;
            }

            db.sysClass.Add(model);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}