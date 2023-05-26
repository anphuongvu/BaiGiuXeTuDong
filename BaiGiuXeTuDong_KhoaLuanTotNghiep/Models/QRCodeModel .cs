using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaiGiuXeTuDong_KhoaLuanTotNghiep.Models
{
    public class QRCodeModel
    {
        public string QRCodeText;

        public QRCodeModel(string qRCodeText)
        {
            QRCodeText = qRCodeText;
        }


    }
}