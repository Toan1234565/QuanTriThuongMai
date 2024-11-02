using chuong_trinh_PT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace chuong_trinh_PT.Controllers
{
    public class DanhMucController : Controller
    {
        QuanLyDuLieuEntities db = new QuanLyDuLieuEntities();
        // GET: DanhMuc
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult danhsach()
        {
            // lay ra danh sach cac san pham
            List<DanhMuc> kq = db.DanhMuc.ToList();
            return View(kq);
        }
        public ActionResult chiteietsp(int id)
        {
            // tim ra doi tuong nay trong co so du lieu 
           
            // tim ra id
            SanPham ketqua = db.SanPham.Find(id);

            return View(ketqua);
        }
        public ActionResult ThemDM()
        {


            return View(new DanhMuc() { DanhMucID = 2 });
        }

        [HttpPost]
        public ActionResult ThemDM(DanhMuc DM)
        {
            // luu du lieu vao csdl
            // kiem tra 
            if (string.IsNullOrEmpty(DM.TenDanhMuc) == true)
            {
                ModelState.AddModelError("", "ten san pham khong duoc de trong");
                return View(DM);
            }

            // luu 

            // ham them 
            db.DanhMuc.Add(DM);
            // ham luu du lieu vao sql
            db.SaveChanges();
            if (DM.DanhMucID > 0)
            {
                return RedirectToAction("Them", "SanPham");
            }
            else
            {
                ModelState.AddModelError("", "Khong luu duoc");
                return View(DM);
            }
        }
        public ActionResult capnhatDM(int id)
        {
            // tim san pham cap nhat
      
            var DM = db.DanhMuc.Find(id);

            return View(DM);
        }
        [HttpPost]
        public ActionResult capnhatDM(DanhMuc DM)
        {
            // Kiểm tra
            if (string.IsNullOrEmpty(DM.TenDanhMuc))
            {
                ModelState.AddModelError("", "Tên danh mục không được để trống");
                return View(DM);
            }

            // Lưu
            using (QuanLyDuLieuEntities db = new QuanLyDuLieuEntities())
            {
                // Tìm đối tượng cần sửa
                var udate = db.DanhMuc.Find(DM.DanhMucID);
                if (udate != null)
                {
                    // Gán giá trị
                    udate.TenDanhMuc = DM.TenDanhMuc;

                    var id = db.SaveChanges();
                    if (id > 0)
                    {
                        return RedirectToAction("danhsach");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Không lưu được");
                        return View(DM);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Không tìm thấy danh mục");
                    return View(DM);
                }
            }
        }

        public ActionResult xoaDM(int id)
        {
            
            var model = db.DanhMuc.Find(id);
            db.DanhMuc.Remove(model);
            db.SaveChanges();
            return RedirectToAction("danhsach");
        }
    }
}