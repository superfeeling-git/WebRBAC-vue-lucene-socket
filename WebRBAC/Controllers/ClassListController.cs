using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebRBAC.Models;

namespace WebRBAC.Controllers
{
    public class ClassListController : Controller
    {
        RBACContext db = new RBACContext();
        dechengEntities db_dc = new dechengEntities();

        List<sysClass> syslist = new List<sysClass>();

        public ClassListController()
        {
            //查找所有的一级分类
            var classlist = db.sysClass.Where(m => m.ParentID == 0);
            foreach(var p in classlist)
            {
                syslist.Add(p);
                //调用递归
                getClass(p.ClassID);
            }
        }


        // GET: ClassList
        [ChildActionOnly]
        public ActionResult Index(int? ClassId,string firstText = "请选择分类")
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = firstText, Value= "0" });
            foreach(var p in syslist)
            {
                string nbsp = string.Empty;
                for(int i = 0; i < p.Depth; i++)
                {
                    nbsp += HttpUtility.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;");
                }
                nbsp += p.ClassName;

                SelectListItem item = new SelectListItem { Text = nbsp, Value = p.ClassID.ToString() };
                if (p.ClassID == ClassId)
                {
                    item.Selected = true;
                }

                items.Add(item);
            }

            ViewBag.firstText = firstText;

            ViewBag.items = items;
            return View();
        }

        /// <summary>
        /// 通过递归遍历所有子分类
        /// </summary>
        /// <param name="ParentId"></param>
        public void getClass(int ParentId)
        {
            var list = db.sysClass.Where(m => m.ParentID == ParentId).ToList();
            foreach(var p in list)
            {
                syslist.Add(p);
                getClass(p.ClassID);
            }
        }
    }
}