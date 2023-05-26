using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BaiGiuXeTuDong_KhoaLuanTotNghiep.Models;
using RestSharp;
using QRCoder;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using ZXing;

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

        // GET: Xes
        public ActionResult XeRa()
        {
            return View();
        }

        public ActionResult ThanhToan(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhToan thanhToan = db.ThanhToans.Find(int.Parse(id));
            if (thanhToan == null)
            {
                return HttpNotFound();
            }
            return View(thanhToan);
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

        [HttpPost]
        public JsonResult RecognizeBSX(String image_str)
        {
           
            var client = new RestClient("https://b4dc-165-93-111-59.ngrok-free.app");
            //client.Timeout = -1;
            var request = new RestRequest("recognize");
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", new { image_str = image_str }, ParameterType.RequestBody);

            var response = client.ExecutePost(request);

            LPRecognitionResult ret = JsonSerializer.Deserialize<LPRecognitionResult>(response.Content);
            

            // kiem tra xe ton tai chua
            var xes = db.Xes.Where(x => x.BienSo == ret.result).ToList();
            Xe xe;
            if (xes.Count  == 0 && ret.result.Length > 0 && ret.result.Length == 9)
            {
                // add new xe
                xe = new Xe();
                xe.BienSo = ret.result;
                xe.MaLoaiXe = 1;
                db.Entry(xe).State = EntityState.Added;
                db.SaveChanges();
            }
            else
            {
                xe = xes[0];
            }    

            // xe chua tồn tại và biển số xe hợp lệ
            if(xe.MaTheXe == null && ret.result.Length == 9)
            {
                var mtt = db.ThanhToans.Count();

                // the xe
                var theXeNgay = db.TheXeNgays.Where(x => x.TrangThai == false).First();
                theXeNgay.GioVao = DateTime.Now;
                theXeNgay.TrangThai = true;
                theXeNgay.MaThanhToan = mtt;
                db.Entry(theXeNgay).State = EntityState.Modified;

                // Thanh Toan

                ThanhToan thanhToan = new ThanhToan();
                thanhToan.MaThanhToan = mtt;
                thanhToan.MaTheXe = theXeNgay.MaTheXeNgay;
                thanhToan.TrangThai = false;
                thanhToan.MaLoaiThanhToan = 2; // barcode
                db.Entry(thanhToan).State = EntityState.Added;

                LichSuXe lichSuXe = new LichSuXe();
                lichSuXe.MaLichSu = thanhToan.MaThanhToan;
                lichSuXe.LuotVao = "1";
                lichSuXe.LuotRa = "0";
                lichSuXe.MaTheXeNgay = theXeNgay.MaTheXeNgay;
                lichSuXe.MaThanhToan = thanhToan.MaThanhToan;

                db.Entry(lichSuXe).State = EntityState.Added;

                // thong tin xe
                xe.MaTheXe = theXeNgay.MaTheXeNgay;
                db.Entry(xe).State = EntityState.Modified;

                // save to db
                db.SaveChanges();
            }

            var jsonRet = Json(response.Content.Remove(response.Content.Length - 2, 2) + ", \"maTheXe\": \"" + xe.MaTheXe.ToString() + "\"}");

            return jsonRet;

        }


        [HttpPost]
        public JsonResult RecognizeBSXXeRa(String image_str)
        {            
            var client = new RestClient("https://b4dc-165-93-111-59.ngrok-free.app");
            //client.Timeout = -1;
            var request = new RestRequest("recognize");
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", new { image_str = image_str }, ParameterType.RequestBody);

            var response = client.ExecutePost(request);

            LPRecognitionResult ret = JsonSerializer.Deserialize<LPRecognitionResult>(response.Content);


            // kiem tra xe ton tai chua
            var xes = db.Xes.Where(x => x.BienSo == ret.result).ToList();            

            // tim thay xe
            if (xes.Count > 0 && ret.result.Length > 0 && ret.result.Length == 9)
            {
                // add new xe
                Xe xe = xes[0];

                if (xe.MaTheXe != null)
                {
                    return Json(response.Content.Remove(response.Content.Length - 2, 2) + ", \"maTheXe\": \"" + xe.MaTheXe.ToString() + "\"}");
                }
            }

            // xe chua tồn tại và biển số xe hợp lệ
            return Json(response.Content.Remove(response.Content.Length - 2, 2) + ", \"maTheXe\": \"\"}"); 
        }

        [HttpPost]
        public JsonResult CreateQRCode(string qRCode)
        {
            QRCodeGenerator QrGenerator = new QRCodeGenerator();
            QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(qRCode, QRCodeGenerator.ECCLevel.Q);
            QRCode QrCode = new QRCode(QrCodeInfo);
            Bitmap QrBitmap = QrCode.GetGraphic(60);
            byte[] BitmapArray = QrBitmap.BitmapToByteArray();
            string QrUri = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));
            return Json(QrUri);
        }

        [HttpPost]
        public JsonResult kiemTraThangThai(string maTheXe)
        {
            TheXeNgay theXeNgay = db.TheXeNgays.Find( int.Parse(maTheXe));
            if((bool)theXeNgay.ThanhToan.TrangThai)
            {
                return Json("1");
            }

            return Json("0");
        }

        [HttpPost]
        public JsonResult ReadQRCode(string image_str, string ma_the)
        {
            // b64 string to img
            Bitmap bmpReturn = null;
            byte[] byteBuffer = Convert.FromBase64String(image_str);
            MemoryStream memoryStream = new MemoryStream(byteBuffer);
            memoryStream.Position = 0;
            bmpReturn = (Bitmap)Bitmap.FromStream(memoryStream);
            memoryStream.Close();
            memoryStream = null;
            byteBuffer = null;

            //read from barcode
            BarcodeReader reader = new BarcodeReader();
            Result result = reader.Decode(bmpReturn);
            //try
            //{
            if(result != null)
            {
                string decoded = result.ToString().Trim();
                if (decoded.Length > 0 && decoded == ma_the)
                {
                    // get mathe xe
                    TheXeNgay theXeNgay = db.TheXeNgays.Find(int.Parse(decoded));

                    QRCodeGenerator QrGenerator = new QRCodeGenerator();
                    QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(HttpContext.Request.Url.Authority + "/ThanhToans/Details/" + theXeNgay.MaThanhToan.ToString(), QRCodeGenerator.ECCLevel.Q);
                    QRCode QrCode = new QRCode(QrCodeInfo);
                    Bitmap QrBitmap = QrCode.GetGraphic(60);
                    byte[] BitmapArray = QrBitmap.BitmapToByteArray();
                    string QrUri = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));
                    return Json(QrUri);
                }
            }    
                
            //}
            //catch (Exception ex)
            //{
            //    return Json("");
            //}

            return Json("");
        }
    }
    //Extension method to convert Bitmap to Byte Array
    public static class BitmapExtension
    {
        public static byte[] BitmapToByteArray(this Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }


}
