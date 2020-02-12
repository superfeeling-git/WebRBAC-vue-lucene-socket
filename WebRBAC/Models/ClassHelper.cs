using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRBAC.Models
{
    public class ClassHelper
    {
        RBACContext db = new RBACContext();
        List<sysClass> syslist = new List<sysClass>();

        public List<sysClass> getAllClass
        {
            get
            {
                //查找所有的一级分类
                var classlist = db.sysClass.Where(m => m.ParentID == 0);
                foreach (var p in classlist)
                {
                    syslist.Add(p);
                    //调用递归
                    getClass(p.ClassID);
                }

                return syslist;
            }
        }


        /// <summary>
        /// 通过递归遍历所有子分类
        /// </summary>
        /// <param name="ParentId"></param>
        private void getClass(int ParentId)
        {
            var list = db.sysClass.Where(m => m.ParentID == ParentId).ToList();
            foreach (var p in list)
            {
                syslist.Add(p);
                getClass(p.ClassID);
            }
        }
    }
}