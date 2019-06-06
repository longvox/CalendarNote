using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarNote.Model
{
    public class NguoiDung
    {
        [Key]
        public string NguoiDungID { get; set; }
        public string TenHienThi { get; set; }
        public string MatKhau { get; set; }
        public ICollection<CongViec> CongViec { get; set; }
        public ICollection<SuKien> SuKien { get; set; }
    }
}
