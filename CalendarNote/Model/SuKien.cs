using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarNote.Model
{
    public class SuKien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SuKienID { get; set; }
        public string TieuDe { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public bool LapLai { get; set; }
        public string KhungThoiGianLap { get; set; }
        public bool ThongBao { get; set; }
        public int ThoiGianThongBao { get; set; }
        public string KhungThoiGianThongBao { get; set; }
        public string Mau { get; set; }
        public string NoiDung { get; set; }

        [ForeignKey("NguoiDung")]
        public string NguoiDungID { get; set; }
        [ForeignKey("PhanLoaiSuKien")]
        public int PhanLoaiSuKienID { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
        public virtual PhanLoaiSuKien PhanLoaiSuKien { get; set; }
    }
}
