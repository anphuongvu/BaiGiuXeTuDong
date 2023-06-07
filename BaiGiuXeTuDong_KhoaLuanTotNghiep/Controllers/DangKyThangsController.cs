using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BaiGiuXeTuDong_KhoaLuanTotNghiep.Models;


namespace BaiGiuXeTuDong_KhoaLuanTotNghiep.Controllers
{
    public class DangKyThangsController : Controller
    {
        private QLBaiGiuXeEntities db = new QLBaiGiuXeEntities();

        //GET: DangKyThangs
        //public ActionResult Index()
        //{
        //    var dangKyThangs = db.DangKyThangs.Include(d => d.LoaiThanhToan);
        //    return View(dangKyThangs.ToList());
        //}

        public ActionResult Index()
        {
            ViewBag.MaLoaiThanhToan = new SelectList(db.LoaiThanhToans, "MaLoaiThanhToan", "TenLoai");
            return View();
        }
        //POST: DangKyThangs/Create
        //To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "MaDangKy,HoTenKH,BienSoXe,ThoiGianDangKy,LoaiXe,MaLoaiThanhToan,TrangThai,SDT,Email,ThoiHan")] DangKyThang dangKyThang)
        {
            if (ModelState.IsValid)
            {
                dangKyThang.TrangThai = false;
                dangKyThang.ThoiGianDangKy = DateTime.Now;
                db.DangKyThangs.Add(dangKyThang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoaiThanhToan = new SelectList(db.LoaiThanhToans, "MaLoaiThanhToan", "TenLoai", dangKyThang.MaLoaiThanhToan);
            return View(dangKyThang);
        }

        // GET: DangKyThangs/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DangKyThang dangKyThang = db.DangKyThangs.Find(id);
        //    if (dangKyThang == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(dangKyThang);
        //}

        // GET: DangKyThangs/Create
        //public ActionResult Create()
        //{
        //    ViewBag.MaLoaiThanhToan = new SelectList(db.LoaiThanhToans, "MaLoaiThanhToan", "TenLoai");
        //    return View();
        //}

        // POST: DangKyThangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MaDangKy,HoTenKH,BienSoXe,ThoiGianDangKy,LoaiXe,MaLoaiThanhToan,TrangThai,SDT,Email,ThoiHan")] DangKyThang dangKyThang)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.DangKyThangs.Add(dangKyThang);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.MaLoaiThanhToan = new SelectList(db.LoaiThanhToans, "MaLoaiThanhToan", "TenLoai", dangKyThang.MaLoaiThanhToan);
        //    return View(dangKyThang);
        //}

        // GET: DangKyThangs/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DangKyThang dangKyThang = db.DangKyThangs.Find(id);
        //    if (dangKyThang == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.MaLoaiThanhToan = new SelectList(db.LoaiThanhToans, "MaLoaiThanhToan", "TenLoai", dangKyThang.MaLoaiThanhToan);
        //    return View(dangKyThang);
        //}

        // POST: DangKyThangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "MaDangKy,HoTenKH,BienSoXe,ThoiGianDangKy,LoaiXe,MaLoaiThanhToan,TrangThai,SDT,Email,ThoiHan")] DangKyThang dangKyThang)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(dangKyThang).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.MaLoaiThanhToan = new SelectList(db.LoaiThanhToans, "MaLoaiThanhToan", "TenLoai", dangKyThang.MaLoaiThanhToan);
        //    return View(dangKyThang);
        //}

        // GET: DangKyThangs/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DangKyThang dangKyThang = db.DangKyThangs.Find(id);
        //    if (dangKyThang == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(dangKyThang);
        //}

        // POST: DangKyThangs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    DangKyThang dangKyThang = db.DangKyThangs.Find(id);
        //    db.DangKyThangs.Remove(dangKyThang);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
