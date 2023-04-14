using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BaiGiuXeTuDong_KhoaLuanTotNghiep
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "BaiGiuXeTuDong_KhoaLuanTotNghiep.Controllers" }
            );

            routes.MapRoute(
                "AdminPage",
                "Admin/{controller}/{action}",
                new { area = "Admin", controller = "HomeAdmin", action = "Index" },
                new[] { typeof(Areas.Admin.Controllers.HomeAdminController).Namespace }
            );

            routes.MapRoute(
                "KhachHangPage",
                "KhachHang/{controller}/{action}",
                new { area = "KhachHang", controller = "HomeKH", action = "Index" },
                new[] { typeof(Areas.KhachHang.Controllers.HomeKHController).Namespace }
            );

            
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

        }
    }
}
