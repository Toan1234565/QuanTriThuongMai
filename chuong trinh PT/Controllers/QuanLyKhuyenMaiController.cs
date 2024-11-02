using chuong_trinh_PT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace chuong_trinh_PT.Controllers
{
    public class QuanLyKhuyenMaiController : Controller
    {
        // GET: QuanLyKhuyenMai
        private QuanLyDuLieuEntities db = new QuanLyDuLieuEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult dsKhuyenMai()
        {
            List<KhuyenMai> ds =db.KhuyenMai.ToList();
            return View(ds);
        }
    }
}