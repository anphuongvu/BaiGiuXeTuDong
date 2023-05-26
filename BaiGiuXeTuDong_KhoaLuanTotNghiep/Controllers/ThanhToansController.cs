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
    public class ThanhToansController : Controller
    {
        private QLBaiGiuXeEntities db = new QLBaiGiuXeEntities();


        // GET: ThanhToans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhToan thanhToan = db.ThanhToans.Find(id);
            thanhToan.TrangThai = true;
            //lấy số tiền theo vị trí 
            TheXeNgay theXeNgay = db.TheXeNgays.Find(thanhToan.MaTheXe);
            thanhToan.SoTien = theXeNgay.ViTriDauXe.DonGia;
            db.Entry(thanhToan).State = EntityState.Modified;


            // Ma lich su xe với mã thanh toán giống nhau
            // lưu lịch sử
            LichSuXe lichSuXe = db.LichSuXes.Find(thanhToan.MaThanhToan);
            lichSuXe.LuotRa = "1";
            db.Entry(lichSuXe).State = EntityState.Modified;

            db.SaveChanges();

            if (thanhToan == null)
            {
                return HttpNotFound();
            }
            return View(thanhToan);
        }
    }
}
