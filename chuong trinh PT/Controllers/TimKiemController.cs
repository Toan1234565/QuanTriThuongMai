using chuong_trinh_PT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace chuong_trinh_PT.Controllers
{
    public class TimKiemController : Controller
    {
        QuanLyDuLieuEntities db = new QuanLyDuLieuEntities();
        // GET: TimKiem
        public ActionResult Index()
        {
            return View();
        }
        // Xử lý kết quả tìm kiếm
        public ActionResult SearchResults(string name, double? from, double? to)
        {
            var kq2 = from sp in db.SanPham select sp;

            // Kiểm tra nếu tên không rỗng hoặc null
            if (!string.IsNullOrEmpty(name))
            {
                kq2 = kq2.Where(x => x.TenSanPham.Contains(name));
            }

            // Kiểm tra nếu giá từ và giá đến không null
            if (from != null && to != null)
            {
                kq2 = kq2.Where(x => x.Gia >= from && x.Gia <= to);
            }

            return View(kq2.ToList());
        }
        public ActionResult LocSanPham(int ? idHang,int ? idDM, double? to, double? from, string sapxep)
        {
            //var kq3 = (from sp in db.SanPham join Hang in db.Hang on sp.HangID equals Hang.HangID select sp).ToList();
            var kq2 = from sp in db.SanPham select sp;
            if(idHang.HasValue && idHang != 0)
            {
                kq2 = kq2.Where(sp => sp.HangID == idHang.Value);
            }
            if(idDM.HasValue && idDM != 0)
            {
                kq2 = kq2.Where(sp => sp.DanhMucID == idDM.Value);
            }
            // Kiểm tra nếu giá từ và giá đến không null
            if (from != null && to != null)
            {
                kq2 = kq2.Where(x => x.Gia >= from && x.Gia <= to);
            }
            switch (sapxep)
            {
                case "Giatang":
                    kq2 = kq2.OrderBy(x => x.Gia); break;
                case "Giagiam":
                        kq2 = kq2.OrderByDescending(x => x.Gia); break;
                default:
                    kq2 = kq2.OrderBy(x => x.Gia);
                    break;
            }
            //ViewBag.idHang = idHang;
            //ViewBag.idDM = idDM;
            //ViewBag.to = to;
            //ViewBag.from = from;
            //ViewBag.Hang = new SelectList(db.Hang, "HangID", "TenHang", idHang);
            //ViewBag.idDM = new SelectList(db.DanhMuc, "DanhMucID", "TenDanhMuc", idDM);
            var kq = kq2.ToList();
            return View(kq);
        }
    }
}