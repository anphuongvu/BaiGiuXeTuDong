using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BaiGiuXeTuDong_KhoaLuanTotNghiep.Models;
using PagedList.Mvc;
using PagedList;

namespace BaiGiuXeTuDong_KhoaLuanTotNghiep.Areas.Admin.Controllers
{
    public class LichSuXesController : Controller
    {
        private QLBaiGiuXeEntities db = new QLBaiGiuXeEntities();

        // GET: Admin/LichSuXes
        public ActionResult Index(int? p)
        {
            var lichSuXes = db.LichSuXes.Include(l => l.ThanhToan).Include(l => l.TheXeNgay).Include(l => l.TheXeThang);
            return View(lichSuXes.ToList().ToPagedList(p ?? 1, 3));
        }

        // GET: Admin/LichSuXes/DoanhThu
        public ActionResult DoanhThu()
        {
            var listItems = db.LichSuXes.Where(x => x.LuotVao != null && x.LuotRa != null && x.LuotVao != "" ).Include(l => l.ThanhToan).Include(l => l.TheXeNgay).Include(l => l.TheXeThang).ToList();

            // TODO doanh thu theo thẻ th
            var thongKeTien = listItems.GroupBy(x => DateTimeOffset.FromUnixTimeSeconds(int.Parse(x.LuotVao)).ToString("yyyy/MM")).Select(g => new ThongKe
            {
                index = DateTimeOffset.FromUnixTimeSeconds(int.Parse(g.FirstOrDefault().LuotVao)).ToString("yyyy/MM"),
                value = (decimal)g.Sum(s => ( s.TheXeNgay != null ? s.TheXeNgay.ThanhToan.SoTien : s.TheXeThang.ThanhToan.SoTien))
            });

            ViewBag.ttn = db.LichSuXes.Where(x => x.MaTheXeNgay != null).Count();
            ViewBag.ttt = db.LichSuXes.Where(x => x.MaTheXeThang != null).Count();

            return View(thongKeTien.ToList());
        }

        // GET: Admin/LichSuXes/ThongKeXe
        public ActionResult ThongKeXe()
        {
            var listItems = db.LichSuXes.Where(x => x.LuotVao != null && x.LuotRa != null && x.LuotVao != "" && x.LuotRa != "").Include(l => l.ThanhToan).Include(l => l.TheXeNgay).Include(l => l.TheXeThang).ToList();

            var thongKeTien = listItems.GroupBy(x => DateTimeOffset.FromUnixTimeSeconds(int.Parse(x.LuotVao)).ToString("yyyy/MM")).Select(g => new ThongKe
            {
                index = DateTimeOffset.FromUnixTimeSeconds(int.Parse(g.FirstOrDefault().LuotVao)).ToString("yyyy/MM"),
                value = (decimal)g.Count()
            });

            ViewBag.xdg = db.LichSuXes.Where(x => (x.MaTheXeNgay != null && x.LuotRa == "") || (x.MaTheXeThang != null && x.LuotRa == "")).Count();
            ViewBag.kvt = db.ViTriDauXes.Where(x => x.TrangThai == false).Count();

            return View(thongKeTien.ToList());
        }



        // GET: Admin/LichSuXes/Details/5
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

        // GET: Admin/LichSuXes/Create
        public ActionResult Create()
        {
            ViewBag.MaThanhToan = new SelectList(db.ThanhToans, "MaThanhToan", "MaThanhToan");
            ViewBag.MaTheXeNgay = new SelectList(db.TheXeNgays, "MaTheXeNgay", "MaTheXeNgay");
            ViewBag.MaTheXeThang = new SelectList(db.TheXeThangs, "MaTheXeThang", "MaTheXeThang");
            return View();
        }

        // POST: Admin/LichSuXes/Create
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

        // GET: Admin/LichSuXes/Edit/5
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

        // POST: Admin/LichSuXes/Edit/5
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

        // GET: Admin/LichSuXes/Delete/5
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

        // POST: Admin/LichSuXes/Delete/5
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
