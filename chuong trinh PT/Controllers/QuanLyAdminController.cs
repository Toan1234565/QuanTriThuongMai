using chuong_trinh_PT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace chuong_trinh_PT.Controllers
{
    public class QuanLyAdminController : Controller
    {
        // GET: QuanLyAdmin
        private QuanLyDuLieuEntities db = new QuanLyDuLieuEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult dsAdmin()
        {
            List<admins> ds = db.admins.ToList();
            return View(ds);
        }
    }
}