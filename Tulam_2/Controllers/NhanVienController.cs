using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tulam_2.Models;

namespace Tulam_2.Controllers
{
    public class NhanVienController : Controller
    {
        ModelDB db = new ModelDB();
        // GET: NhanVien
        public ActionResult Index()
        {
            var query = db.NhanViens.Select(p => p);
            return View(query.ToList());
        }
        [HttpGet]
        public ActionResult ChiTiet(int id)
        {
            var nv = db.NhanViens.Where(p => p.Manv == id).FirstOrDefault();
            return View(nv);
        }
        [HttpGet]
        public ActionResult Them()
        {
            ViewData["Maphong"] = new SelectList(db.Phongs, "Maphong", "Tenphong");
            return View();
        }
        //[HttpPost]
        //public ActionResult Them(FormCollection f,NhanVien nv)
        //{

        //    var ten = f["hoten"];
        //    var mk = f["matkhau"];
        //    var tuoi = f["tuoi"];
        //    var diachi = f["diachi"];
        //    var luong = f["luong"];
        //    var maphong = f["Phong"];
        //    if (String.IsNullOrEmpty(ten))
        //    {
        //        ViewData["Loi1"] = "ho ten khong duoc de trong";
        //    }
        //    else
        //    {
        //        nv.Hoten = ten;
        //        nv.Matkhau = mk;
        //        nv.Tuoi = int.Parse(tuoi);
        //        nv.Diachi = diachi;
        //        nv.Luong = int.Parse(luong);
        //        nv.Maphong = int.Parse(maphong);
        //        db.NhanViens.Add(nv);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return this.Them();
        //}

        [HttpPost]
        public ActionResult Them(NhanVien nv)
        {
            try
            {
                db.NhanViens.Add(nv);
                db.SaveChanges();
                return Json(new { result = true, JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                return Json(new { result = false, error = ex.Message});
            }
        
        }
        
        [HttpGet]
        public ActionResult Sua(int id)
        {
            ViewData["Phong"] = new SelectList(db.Phongs, "Maphong", "Tenphong");
            var nv = db.NhanViens.Where(p=>p.Manv == id).FirstOrDefault();
            return View(nv);
        }
        [HttpPost]
        public ActionResult Sua(FormCollection f,int id)
        {
            var ten = f["hoten"];
            var mk = f["matkhau"];
            var tuoi = f["tuoi"];
            var diachi = f["diachi"];
            var luong = f["luong"];
            var phong = f["Phong"];
            var nv = db.NhanViens.FirstOrDefault(p=>p.Manv == id);  
            if(String.IsNullOrEmpty(ten))
            {
                ViewData["loi1"] = "khong duoc de trong";
            }
            else
            {
                nv.Hoten = ten;
                nv.Matkhau = mk;
                nv.Tuoi = int.Parse(tuoi);
                nv.Diachi = diachi;
                nv.Luong = int.Parse(luong);
                nv.Maphong = int.Parse(phong);
             
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return this.Sua(id);
        }
        [HttpGet]
        public ActionResult Xoa(int id)
        {
            var nv = db.NhanViens.FirstOrDefault(p=>p.Manv ==id);
            return View(nv);
        }
        [HttpPost]
        public ActionResult Xoa(FormCollection f,int id)
        {
            var nv = db.NhanViens.FirstOrDefault(p=>p.Manv==id);
            db.NhanViens.Remove(nv);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string ma,string mk)
        {
            var nv = db.NhanViens.FirstOrDefault(p=> p.Manv.ToString() == ma&& p.Matkhau == mk);
            if(nv != null)
            {
                Session["ma"] = ma;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "dang nhap khong thanh cong";
                return View("Login");
            }
        }
        public ActionResult Logout()
        {
            Session["ma"] = null;
            return RedirectToAction("Index","NhanVien");
        }
        public ActionResult CategoryMenu()
        {
            var li = db.Phongs.Select(p => p);
            return PartialView(li);
        }
        [Route("NhanVienPhong/{Maphong}")]
        public ActionResult HienThiTheoPhong(int Maphong)
        {
            var nv = db.NhanViens.Where(p => p.Maphong == Maphong);
            return View(nv);
        }
    }
    
}