using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaiGiuXeTuDong_KhoaLuanTotNghiep.Areas.KhachHang.Controllers
{
    public class HomeKHController : Controller
    {
        // GET: KhachHang/HomeKH
        public ActionResult Index()
        {
            return View();
        }
    }
}