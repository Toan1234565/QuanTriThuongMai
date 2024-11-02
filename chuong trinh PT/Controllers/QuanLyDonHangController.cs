using chuong_trinh_PT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace chuong_trinh_PT.Controllers
{
    public class QuanLyDonHangController : Controller
    {
        private QuanLyDuLieuEntities db = new QuanLyDuLieuEntities();
        // GET: QuanLyDonHang
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult dsDonHang()
        {
            List<DonHang> ds =db.DonHang.ToList();

            return View(ds);
        }

    }
}