﻿using System;
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
    public class LichSuXesController : Controller
    {
        private QLBaiGiuXeEntities db = new QLBaiGiuXeEntities();

        // GET: LichSuXes
        public ActionResult Index()
        {
            var lichSuXes = db.LichSuXes.Include(l => l.ThanhToan).Include(l => l.TheXeNgay).Include(l => l.TheXeThang);
            return View(lichSuXes.ToList());
        }

        // GET: LichSuXes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichSuXe lichSuXe = db.LichSuXes.Find(id);
            if (lichSuXe == null)
            {
                return HttpNotFound();
            }
            return View(lichSuXe);
        }

        // GET: LichSuXes/Create
        public ActionResult Create()
        {
            ViewBag.MaThanhToan = new SelectList(db.ThanhToans, "MaThanhToan", "MaThanhToan");
            ViewBag.MaTheXeNgay = new SelectList(db.TheXeNgays, "MaTheXeNgay", "MaTheXeNgay");
            ViewBag.MaTheXeThang = new SelectList(db.TheXeThangs, "MaTheXeThang", "MaTheXeThang");
            return View();
        }

        // POST: LichSuXes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLichSu,LuotVao,LuotRa,MaTheXeNgay,MaTheXeThang,MaThanhToan")] LichSuXe lichSuXe)
        {
            if (ModelState.IsValid)
            {
                db.LichSuXes.Add(lichSuXe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaThanhToan = new SelectList(db.ThanhToans, "MaThanhToan", "MaThanhToan", lichSuXe.MaThanhToan);
            ViewBag.MaTheXeNgay = new SelectList(db.TheXeNgays, "MaTheXeNgay", "MaTheXeNgay", lichSuXe.MaTheXeNgay);
            ViewBag.MaTheXeThang = new SelectList(db.TheXeThangs, "MaTheXeThang", "MaTheXeThang", lichSuXe.MaTheXeThang);
            return View(lichSuXe);
        }

        // GET: LichSuXes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichSuXe lichSuXe = db.LichSuXes.Find(id);
            if (lichSuXe == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaThanhToan = new SelectList(db.ThanhToans, "MaThanhToan", "MaThanhToan", lichSuXe.MaThanhToan);
            ViewBag.MaTheXeNgay = new SelectList(db.TheXeNgays, "MaTheXeNgay", "MaTheXeNgay", lichSuXe.MaTheXeNgay);
            ViewBag.MaTheXeThang = new SelectList(db.TheXeThangs, "MaTheXeThang", "MaTheXeThang", lichSuXe.MaTheXeThang);
            return View(lichSuXe);
        }

        // POST: LichSuXes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLichSu,LuotVao,LuotRa,MaTheXeNgay,MaTheXeThang,MaThanhToan")] LichSuXe lichSuXe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lichSuXe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaThanhToan = new SelectList(db.ThanhToans, "MaThanhToan", "MaThanhToan", lichSuXe.MaThanhToan);
            ViewBag.MaTheXeNgay = new SelectList(db.TheXeNgays, "MaTheXeNgay", "MaTheXeNgay", lichSuXe.MaTheXeNgay);
            ViewBag.MaTheXeThang = new SelectList(db.TheXeThangs, "MaTheXeThang", "MaTheXeThang", lichSuXe.MaTheXeThang);
            return View(lichSuXe);
        }

        // GET: LichSuXes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichSuXe lichSuXe = db.LichSuXes.Find(id);
            if (lichSuXe == null)
            {
                return HttpNotFound();
            }
            return View(lichSuXe);
        }

        // POST: LichSuXes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LichSuXe lichSuXe = db.LichSuXes.Find(id);
            db.LichSuXes.Remove(lichSuXe);
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
