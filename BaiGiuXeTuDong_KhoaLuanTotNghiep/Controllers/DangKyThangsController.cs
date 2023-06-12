using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BaiGiuXeTuDong_KhoaLuanTotNghiep.Models;
using QRCoder;

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
                dangKyThang.MaDangKy = db.DangKyThangs.Count();
                dangKyThang.TrangThai = false;
                dangKyThang.ThoiGianDangKy = DateTime.Now;
                db.Entry(dangKyThang).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = dangKyThang.MaDangKy });
            }

            ViewBag.MaLoaiThanhToan = new SelectList(db.LoaiThanhToans, "MaLoaiThanhToan", "TenLoai", dangKyThang.MaLoaiThanhToan);
            return View(dangKyThang);
        }

        // GET: DangKyThangs/Details/5
        public ActionResult ChiTiet(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DangKyThang dangKyThang = db.DangKyThangs.Find(id);

            if (dangKyThang == null)
            {
                return HttpNotFound();
            }
            Xe xe = db.Xes.Where(x => x.BienSo == dangKyThang.BienSoXe).FirstOrDefault();
            if (xe == null)
            {
                return HttpNotFound();
            }
            
            // Ma the xe
            QRCodeGenerator QrGenerator = new QRCodeGenerator();
            QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(xe.MaTheXe.ToString(), QRCodeGenerator.ECCLevel.Q);
            QRCode QrCode = new QRCode(QrCodeInfo);
            Bitmap QrBitmap = QrCode.GetGraphic(60);
            byte[] BitmapArray = QrBitmap.BitmapToByteArray();
            ViewBag.QRCodeTheXe = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));

            // ma thanh toan
            QrGenerator = new QRCodeGenerator();
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName()); // `Dns.Resolve()` method is deprecated.
            IPAddress ipAddress = ipHostInfo.AddressList[1];
            QrCodeInfo = QrGenerator.CreateQrCode("https://" + ipAddress.ToString() + "/ThanhToans/Details/" + xe.TheXeNgay.MaThanhToan.ToString(), QRCodeGenerator.ECCLevel.Q);
            QrCode = new QRCode(QrCodeInfo);
            QrBitmap = QrCode.GetGraphic(60);
            BitmapArray = QrBitmap.BitmapToByteArray();

            ViewBag.QRCodeThanhToan = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));
            
            ViewBag.MaTheXe = xe.MaTheXe;

            return View(dangKyThang);
        }

        // GET: DangKyThangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DangKyThang dangKyThang = db.DangKyThangs.Find(id);

            if (dangKyThang == null)
            {
                return HttpNotFound();
            }

            // Số ngày
            var soNgay = - DateTime.Now.Subtract((DateTime)dangKyThang.ThoiHan).Days;

            // Mã thanh toán
            var mtt = db.ThanhToans.Count();

            //vi tri
            ViTriDauXe viTriDauXe = db.ViTriDauXes.Where(x => x.TrangThai == false).FirstOrDefault();
            db.Entry(viTriDauXe).State = EntityState.Modified;

            // Tao thẻ xe
            int mtx = db.TheXeThangs.Count() + 1001;
            TheXeThang theXeThang = new TheXeThang {
                MaTheXeThang = mtx,
                GioVao = null,
                GioRa = null,
                TrangThai = true,
                MaThanhToan = mtt,
                MaLoaiXe = dangKyThang.LoaiXe,
                ViTri = viTriDauXe.MaViTri
            };

            TheXeNgay theXeNgay = new TheXeNgay
            {
                MaTheXeNgay = mtx,
                GioVao = null,
                GioRa = null,
                TrangThai = false,
                MaThanhToan = mtt,
                MaViTri = viTriDauXe.MaViTri
            };

            db.Entry(theXeThang).State = EntityState.Added;
            db.Entry(theXeNgay).State = EntityState.Added;

            // Thanh Toan
            ThanhToan thanhToan = new ThanhToan();
            thanhToan.MaThanhToan = mtt;
            thanhToan.MaTheXe = theXeThang.MaTheXeThang;
            if(dangKyThang.LoaiThanhToan.MaLoaiThanhToan == 2)
            {
                // Thanh toan bang QR code
                thanhToan.TrangThai = false;
            }
            else
            {
                thanhToan.TrangThai = true;
            }    
            
            decimal rate = 0.7m;
            thanhToan.SoTien = viTriDauXe.DonGia * soNgay * rate;
            thanhToan.MaLoaiThanhToan = 2; // barcode
            db.Entry(thanhToan).State = EntityState.Added;

            // lich su
            LichSuXe lichSuXe = new LichSuXe();
            lichSuXe.MaLichSu = thanhToan.MaThanhToan;
            // Get the offset from current time in UTC time
            DateTimeOffset dto = new DateTimeOffset(DateTime.Now);
            lichSuXe.LuotVao = "";
            lichSuXe.LuotRa = "";
            lichSuXe.MaTheXeThang = theXeThang.MaTheXeThang;
            lichSuXe.MaThanhToan = thanhToan.MaThanhToan;
            db.Entry(lichSuXe).State = EntityState.Added;
            // save to db
            db.SaveChanges();


            // thong tin xe
            Xe xe = db.Xes.Where(x => x.BienSo == dangKyThang.BienSoXe).FirstOrDefault();
            if (xe == null)
            {
                xe = new Xe
                {
                    BienSo = dangKyThang.BienSoXe,
                    HinhAnh = "",
                    MaLoaiXe = 1,
                    MaTheXe = theXeNgay.MaTheXeNgay
                };
                db.Entry(xe).State = EntityState.Added;
            }
            else
            {
                xe.MaTheXe = theXeThang.MaTheXeThang;
                db.Entry(xe).State = EntityState.Modified;
            }

            // save to db
            db.SaveChanges();



            // Ma the xe
            QRCodeGenerator QrGenerator = new QRCodeGenerator();
            QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(theXeThang.MaTheXeThang.ToString(), QRCodeGenerator.ECCLevel.Q);
            QRCode QrCode = new QRCode(QrCodeInfo);
            Bitmap QrBitmap = QrCode.GetGraphic(60);
            byte[] BitmapArray = QrBitmap.BitmapToByteArray();
            ViewBag.QRCodeTheXe = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));
            // ma thanh toan
            QrGenerator = new QRCodeGenerator();
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName()); // `Dns.Resolve()` method is deprecated.
            IPAddress ipAddress = ipHostInfo.AddressList[1];
            QrCodeInfo = QrGenerator.CreateQrCode("https://" + ipAddress.ToString() + "/ThanhToans/Details/" + xe.TheXeNgay.MaThanhToan.ToString(), QRCodeGenerator.ECCLevel.Q);
            QrCode = new QRCode(QrCodeInfo);
            QrBitmap = QrCode.GetGraphic(60);
            BitmapArray = QrBitmap.BitmapToByteArray();

            ViewBag.QRCodeThanhToan = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));
            
            ViewBag.MaTheXe = xe.MaTheXe;

            return View(dangKyThang);
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
