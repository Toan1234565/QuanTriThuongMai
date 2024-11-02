using chuong_trinh_PT.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace chuong_trinh_PT.Controllers
{
    public class SapxepController : Controller
    {
        QuanLyDuLieuEntities db = new QuanLyDuLieuEntities();
        // GET: Sapxep
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult Danhsachsanpham()
        //{
           
        //    // truy van toan bo: select * from table
        //    var kq = (from item in db.SanPham select item).ToList();
        //    // truy van co dieu kien 
        //    var kq1 = (from item in db.SanPham where item.Soluong < 10 select item).ToList();
        //    // sap xep tang dan
        //    var kq2 = (from item in db.SanPham orderby item.Soluong select item).ToList();
        //    // ket noi giua hai bang: select * from table1 join table2 on table...
        //    var kq3 = (from sp in db.SanPham join Hang in db.Hang on sp.HangID equals Hang.HangID select sp).ToList();

        //    // loc trung: select Distinct *  from  table(VD: xem 1 hang co nhung san pham gi)
        //    var kq4 = (from sp in db.SanPham join Hang in db.Hang on sp.HangID equals Hang.HangID select Hang).Distinct().ToList();



        //    return View(kq4);
        //}
        public ActionResult sxGiaTang()
        {
            var kq2 = (from item in db.SanPham orderby item.Gia select item).ToList();
            return View(kq2);
        }
        public ActionResult sxGiaGiam()
        {
            var kq2 = (from item in db.SanPham
                       orderby item.Gia descending
                       select item).ToList();
            return View(kq2);
        }
        public ActionResult sxsoluongTang()
        {
            var kq2 = (from item in db.SanPham orderby item.Soluong select item).ToList();
            return View(kq2);
        }
        public ActionResult sxsoluongGiam()
        {
            var kq2 = (from item in db.SanPham
                       orderby item.Soluong descending
                       select item).ToList();
            return View(kq2);
        }


        public ActionResult dsgiamgiaGiam()
        {
            var kq2 = (from item in db.SanPham
                        orderby item.Soluong descending
                       select item).ToList();
            return View(kq2);
        }
        public ActionResult dsgiamgiaTang()
        {
            var kq2 = (from item in db.SanPham orderby item.KhuyenMai select item).ToList();
            return View(kq2);
        }



    }
}