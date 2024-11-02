using chuong_trinh_PT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace chuong_trinh_PT.Controllers
{
    public class HangController : Controller
    {
        private QuanLyDuLieuEntities db = new QuanLyDuLieuEntities();
        // GET: Hang
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult danhsach()
        {
            List<Hang> ds = db.Hang.ToList(); 
            return View(ds);
        }

        public ActionResult ThemHang()
        {


            return View(new Hang() { HangID = 2 });
        }

        [HttpPost]
        public ActionResult ThemHang(Hang hang)
        {
            // luu du lieu vao csdl
            // kiem tra 
            if (string.IsNullOrEmpty(hang.TenHang) == true)
            {
                ModelState.AddModelError("", "ten san pham khong duoc de trong");
                return View(hang);
            }

            // luu 

            // ham them 
            db.Hang.Add(hang);
            // ham luu du lieu vao sql
            db.SaveChanges();
            if (hang.HangID > 0)
            {
                return RedirectToAction("Them", "SanPham");
            }
            else
            {
                ModelState.AddModelError("", "Khong luu duoc");
                return View(hang);
            }
        }
        public ActionResult CapnhatHang(int? id)
        {
            // tim san pham cap nhat

            var Hang = db.Hang.Find(id);

            return View(Hang);
        }
        [HttpPost]
        public ActionResult CapnhatHang(Hang hang)
        {
            // Kiểm tra
            if (string.IsNullOrEmpty(hang.TenHang))
            {
                ModelState.AddModelError("", "Tên hang không được để trống");
                return View(hang);
            }
            // tim doi tuong hang can sua
            using (QuanLyDuLieuEntities db = new QuanLyDuLieuEntities())
            {
                var udate = db.Hang.Find(hang.HangID);
                if (udate != null)
                {
                    udate.TenHang = hang.TenHang;
                    var id = db.SaveChanges();
                    if (id > 0)
                    {
                        return RedirectToAction("danhsach");
                    }
                    else
                    {
                        ModelState.AddModelError("", "luu that bai");
                        return View(hang);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "khong tim thay thong tin hang de sua");
                    return View(hang);
                }
            }
            
           
            // Lưu
           
        }




        public ActionResult xoaHang(int id)
        {

            var model = db.Hang.Find(id);
            db.Hang.Remove(model);
            db.SaveChanges();
            return RedirectToAction("danhsach");
           
        }
    }
}