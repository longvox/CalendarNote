using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarNote.Model
{
    public class PhanLoaiSuKien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhanLoaiSuKienID { get; set; }
        public string TieuDe { get; set; }
        public bool HienThi { get; set; }
        public ICollection<SuKien> SuKien { get; set; }
    }
}
