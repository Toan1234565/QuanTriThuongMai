using chuong_trinh_PT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace chuong_trinh_PT.Controllers
{
    public class QuanLyKhachHangController : Controller
    {
        private QuanLyDuLieuEntities db = new QuanLyDuLieuEntities();
        // GET: QuanLyKhachHang
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult dsKhachHang()
        {
            List<KhachHang> ds = db.KhachHang.ToList();
            return View(ds);
        }
    }
}