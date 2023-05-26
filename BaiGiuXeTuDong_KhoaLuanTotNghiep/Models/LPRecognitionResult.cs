using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaiGiuXeTuDong_KhoaLuanTotNghiep.Models
{
    public class LPRecognitionResult
    {
        public string pattern_id { get; set; }
        public string pattern_img { get; set; }
        public string result { get; set; }
        public string barcode { get; set; }
    }
}