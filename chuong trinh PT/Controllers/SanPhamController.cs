using chuong_trinh_PT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace chuong_trinh_PT.Controllers
{
    public class SanPhamController : Controller
    {
        private QuanLyDuLieuEntities db = new QuanLyDuLieuEntities();
        // GET: SanPham
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult danhsach()
        {
            // lay ra danh sach cac san pham

            List<SanPham> kq = db.SanPham.ToList();
            return View(kq);
            
        }
        public ActionResult danhsachchitiet()
        {
            //var kq3 = (from sp in db.SanPham
            //           join chitiet in db.ChiTietSanPham
            //           on sp.SanPhamID equals chitiet.ChiTietSanPhamID
            //           select new
            //           {
            //               sp.TenSanPham,
            //               chitiet.ChiTietSanPhamID,
            //               chitiet.ManHinh,
            //               chitiet.HeDieuHanh,
            //               chitiet.CameraSau,
            //               chitiet.CameraTruoc,
            //               chitiet.Chip,
            //               chitiet.RAM,
            //               chitiet.BoNhoTrong,
            //               chitiet.Sim,
            //               chitiet.Pin
            //           }
            //          ).ToList();
            //return View(kq3);
            List<ChiTietSanPham> kq = db.ChiTietSanPham.ToList();
            return View(kq);
            // lay ra danh sach cac san pham

        }
        public ActionResult ChiTiet(int id)
        {
            var chiTietSanPham = db.ChiTietSanPham.Where(c => c.SanPhamID == id).ToList();
            if (chiTietSanPham == null)
            {
                return HttpNotFound();
            }
            return View(chiTietSanPham);
        }


        [HttpGet]
        public ActionResult Them()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Them(SanPham sp)
        {
            if (string.IsNullOrEmpty(sp.TenSanPham))
            {
                ModelState.AddModelError("", "Tên sản phẩm không được để trống");
                return View(sp);
            }
            if (sp.Gia <= 0)
            {
                ModelState.AddModelError("", "Giá phải lớn hơn 0");
                return View(sp);
            }
            if (sp.Soluong <= 0)
            {
                ModelState.AddModelError("", "Số lượng phải lớn hơn 0");
                return View(sp);
            }

            db.SanPham.Add(sp);
            db.SaveChanges();

            return RedirectToAction("ThemChiTietSanPham", new { id = sp.SanPhamID });
        }

        [HttpGet]
        public ActionResult ThemChiTietSanPham(int id)
        {
            var sp = db.SanPham.Find(id);
            if (sp == null)
            {
                // Xử lý trường hợp không tìm thấy sản phẩm
                return HttpNotFound();
            }
            ViewBag.SanPham = sp;
            return View(new ChiTietSanPham { SanPhamID = sp.SanPhamID });
        }

        [HttpPost]
        public ActionResult ThemChiTietSanPham(ChiTietSanPham chiTietSanPham)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    // Tìm sản phẩm để gán ID sản phẩm khi thêm chi tiết sản phẩm
                    var sp = db.SanPham.Find(chiTietSanPham.SanPhamID);
                    if (sp == null)
                    {
                        ModelState.AddModelError("", "Sản phẩm không tồn tại");
                        return View(chiTietSanPham);
                    }

                    // Nếu tìm thấy thì lưu thông tin vào cơ sở dữ liệu
                    db.ChiTietSanPham.Add(chiTietSanPham);
                    db.SaveChanges();

                    // Cam kết lưu cả hai bảng
                    transaction.Commit();

                    return RedirectToAction("DanhSach");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ModelState.AddModelError("", "Có lỗi xảy ra: " + ex.Message);
                    return View(chiTietSanPham);
                }
            }
        }



        public ActionResult capnhat(int id)
        {
            // tim san pham cap nhat
         
            var sanphammoi = db.SanPham.Find(id);
            
            return View(sanphammoi);
        }
        [HttpPost]
        public ActionResult capnhat(SanPham sp)
        {
            // kiem tra 
            if (string.IsNullOrEmpty(sp.TenSanPham) == true)
            {
                ModelState.AddModelError("", "ten san pham khong duoc de trong");
                return View(sp);
            }
            if (sp.Gia <= 0)
            {
                ModelState.AddModelError("", "Gia phai lon hon 0");
                return View(sp);
            }
            if (sp.Soluong <= 0 || sp.Soluong == null)
            {
                ModelState.AddModelError("", "Gia phai lon hon 0");
                return View(sp);
            }
            // luu 
         
            // tim doi tuong can sua
            var udate = db.SanPham.Find(sp.SanPhamID);

            // gan gia tri 
            udate.TenSanPham = sp.TenSanPham;
            udate.Soluong = sp.Soluong;
            udate.MoTa = sp.MoTa;
            udate.HangID = sp.HangID;
            udate.DanhMucID = sp.DanhMucID;
            udate.HinhAnh = sp.HinhAnh;
            udate.KhuyenMai = sp.KhuyenMai;
            var id = db.SaveChanges();
            if (id > 0)
            {
                return RedirectToAction("danhsach");
            }
            else
            {
                ModelState.AddModelError("", "Khong luu duoc");
                return View(db);
            }
           
        }
        [HttpGet]
        public ActionResult Xoa(int id)
        {
            var sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        [HttpPost, ActionName("Xoa")]
        public ActionResult XacNhanXoa(int id)
        {
            var sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }

            // Retrieve and delete related product details
            var chiTietSanPhams = db.ChiTietSanPham.Where(c => c.SanPhamID == id).ToList();
            foreach (var chiTiet in chiTietSanPhams)
            {
                db.ChiTietSanPham.Remove(chiTiet);
            }

            // Delete the product itself
            db.SanPham.Remove(sanPham);
            db.SaveChanges();

            return RedirectToAction("danhsach"); // Redirect to the product list or another appropriate page
        }
        //public ActionResult dsTon()
        //{
        //    // lay ra danh sach cac san pham
        //    var kq = (from sp in db.SanPham select new
        //    {
        //        sp.SanPhamID,
        //        sp.TenSanPham,
        //        sp.Soluong,
        //        sp.NgayCap
        //    }).ToList();



        //    return View(kq);

        //}
        //public ActionResult TonKho(int id, int soluongton)
        //{
        //    var sp = db.SanPham.Find(id);
        //    if (sp != null)
        //    {
        //        sp.Soluong = soluongton;
        //        sp.NgayCap = DateTime.Now;
        //        db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
        //        db.SaveChanges();
        //        Kiemtra(sp);
        //    }
        //    return View("danhsach");
        //}
        //public void Kiemtra(SanPham sp)
        //{
        //    if(sp.Soluong <= sp.CanhBaoTonKho)
        //    {


        //        // Cảnh báo: Số lượng sản phẩm còn ít
        //        ViewBag.CanhBao = $"Cảnh báo: Sản phẩm {sp.TenSanPham} tồn kho thấp!";
        //    }

        //    if (sp.NgayCap < DateTime.Now.AddMonths(-1) && sp.Soluong > 50)
        //    {
        //        // Cảnh báo: Sản phẩm cập nhật lâu mà sản phẩm vẫn còn nhiều
        //        ViewBag.CanhBao += $" Cảnh báo: Sản phẩm {sp.TenSanPham} đã lâu không cập nhật mà tồn kho vẫn còn nhiều!";
        //    }
        //}

        //public ActionResult xoa(int id)
        //{

        //    var model = db.SanPham.Find(id);
        //    var model1 = db.ChiTietSanPham.Find(id);
        //    db.SanPham.Remove(model);
        //    db.ChiTietSanPham.Remove(model1);
        //    db.SaveChanges();
        //    return RedirectToAction("danhsach");
        //}



        //public ActionResult Danhsachsanpham()
        //{

        //    // truy van toan bo: select * from table
        //    var kq = (from item in  db.SanPham select item).ToList();
        //    // truy van co dieu kien 
        //    var kq1 = (from item in db.SanPham where item.Soluong < 10 select item).ToList();
        //    // sap xep tang dan
        //    var kq2 = (from item in db.SanPham orderby item.Soluong select item).ToList();
        //    // ket noi giua hai bang: select * from table1 join table2 on table...
        //    var kq3 = (from sp in db.SanPham join Hang in db.Hang on sp.HangID equals Hang.HangID select sp).ToList();

        //    // loc trung: select Distinct *  from  table(VD: xem 1 hang co nhung san pham gi)
        //    var kq4 = (from sp in db.SanPham join Hang in db.Hang on sp.HangID equals Hang.HangID  select Hang).Distinct().ToList();



        //    return View(kq4);
       
        public ActionResult Sosanh(string name1, string name2)
        {
            var kq = from sp in db.SanPham join Chitiet in db.ChiTietSanPham on sp.HangID equals Chitiet.SanPhamID select sp;
            if (!string.IsNullOrEmpty(name1) && !string.IsNullOrEmpty(name2))
            {
                kq = kq.Where(x => x.TenSanPham.Contains(name2) && x.TenSanPham.Contains(name1));
            } 
            return View(kq.ToList());
        }
        private readonly 

    }
}