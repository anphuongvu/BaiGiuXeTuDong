using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaiGiuXeTuDong_KhoaLuanTotNghiep.Models;
using PagedList.Mvc;
using PagedList;

namespace BaiGiuXeTuDong_KhoaLuanTotNghiep.Areas.Admin.Controllers
{
    public class SearchController : Controller
    {
        // GET: Admin/Search
        public ActionResult Index(string search, int p =1)
        {
            QLBaiGiuXeEntities sd = new QLBaiGiuXeEntities();
            List<Xe> listXe = sd.Xes.ToList();
            return View(sd.Xes.Where(x => x.BienSo.StartsWith(search) || search == null).ToList().ToPagedList(p, 3));
        }
        public ActionResult SearchTraXe(string search, int p = 1)
        {
            QLBaiGiuXeEntities sd = new QLBaiGiuXeEntities();
            List<LichSuXe> listXe = sd.LichSuXes.ToList();
            return View(sd.LichSuXes.Where(x => x.LuotVao.StartsWith(search) || search == null).ToList().ToPagedList(p, 3));
        }
    }
}