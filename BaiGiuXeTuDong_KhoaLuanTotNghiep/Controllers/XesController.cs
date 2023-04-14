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
    public class XesController : Controller
    {
        private QLBaiGiuXeEntities db = new QLBaiGiuXeEntities();

        // GET: Xes
        public ActionResult Index()
        {
            var xes = db.Xes.Include(x => x.LoaiXe).Include(x => x.TheXeNgay);
            return View(xes.ToList());
        }

        // GET: Xes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xe xe = db.Xes.Find(id);
            if (xe == null)
            {
                return HttpNotFound();
            }
            return View(xe);
        }

        // GET: Xes/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiXe = new SelectList(db.LoaiXes, "MaLoaiXe", "TenLoaiXe");
            ViewBag.MaTheXe = new SelectList(db.TheXeNgays, "MaTheXeNgay", "MaTheXeNgay");
            return View();
        }

        // POST: Xes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BienSo,MaTheXe,MaLoaiXe,HinhAnh")] Xe xe)
        {
            if (ModelState.IsValid)
            {
                db.Xes.Add(xe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoaiXe = new SelectList(db.LoaiXes, "MaLoaiXe", "TenLoaiXe", xe.MaLoaiXe);
            ViewBag.MaTheXe = new SelectList(db.TheXeNgays, "MaTheXeNgay", "MaTheXeNgay", xe.MaTheXe);
            return View(xe);
        }

        // GET: Xes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xe xe = db.Xes.Find(id);
            if (xe == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiXe = new SelectList(db.LoaiXes, "MaLoaiXe", "TenLoaiXe", xe.MaLoaiXe);
            ViewBag.MaTheXe = new SelectList(db.TheXeNgays, "MaTheXeNgay", "MaTheXeNgay", xe.MaTheXe);
            return View(xe);
        }

        // POST: Xes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BienSo,MaTheXe,MaLoaiXe,HinhAnh")] Xe xe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(xe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiXe = new SelectList(db.LoaiXes, "MaLoaiXe", "TenLoaiXe", xe.MaLoaiXe);
            ViewBag.MaTheXe = new SelectList(db.TheXeNgays, "MaTheXeNgay", "MaTheXeNgay", xe.MaTheXe);
            return View(xe);
        }

        // GET: Xes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xe xe = db.Xes.Find(id);
            if (xe == null)
            {
                return HttpNotFound();
            }
            return View(xe);
        }

        // POST: Xes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Xe xe = db.Xes.Find(id);
            db.Xes.Remove(xe);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
