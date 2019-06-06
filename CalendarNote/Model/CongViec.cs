using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarNote.Model
{
    public class CongViec
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CongViecID { get; set; }
        public string TieuDe { get; set; }
        public DateTime ThoiGianHoanThanh { get; set; }
        public string NoiDung { get; set; }
        public string PhanLoaiCongViec { get; set; }// phân loại: hoàn thành, chưa hoàn thành, đang thực hiện
        [ForeignKey("NguoiDung")]
        public string NguoiDungID { get; set; }
        public virtual NguoiDung NguoiDung { get; set; }
    }
}
