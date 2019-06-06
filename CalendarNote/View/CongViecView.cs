using CalendarNote.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarNote.View
{
    class CongViecView
    {
        public int CongViecID { get; set; }
        public string TieuDe { get; set; }
        public DateTime ThoiGianHoanThanh { get; set; }
        public string NoiDung { get; set; }
        public bool DanhDau { get; set; }
        public string NguoiDungID { get; set; }
        
        public CongViecView(CongViec cv)
        {
            CongViecID = cv.CongViecID;
            TieuDe = cv.TieuDe;
            ThoiGianHoanThanh = cv.ThoiGianHoanThanh;
            NoiDung = cv.NoiDung;
            NguoiDungID = cv.NguoiDungID;
            DanhDau = cv.PhanLoaiCongViec == "ChuaHoanThanh" || cv.PhanLoaiCongViec == "DangThucHien" ? false : true;
        }
    }
}
