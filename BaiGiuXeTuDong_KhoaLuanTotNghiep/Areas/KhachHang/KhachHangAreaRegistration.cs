using System.Web.Mvc;

namespace BaiGiuXeTuDong_KhoaLuanTotNghiep.Areas.KhachHang
{
    public class KhachHangAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "KhachHang";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "KhachHang_default",
                "KhachHang/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}