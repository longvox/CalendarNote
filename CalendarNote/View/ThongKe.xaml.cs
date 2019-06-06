using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PieControls;
using System.Collections.ObjectModel;
using CalendarNote.Model;

namespace CalendarNote.View
{
    /// <summary>
    /// Interaction logic for ThongKe.xaml
    /// </summary>
    public partial class ThongKe : Window
    {
        public NguoiDung NguoiDungING { get; set; }
        public ThongKe(NguoiDung nd)
        {
            InitializeComponent();
            NguoiDungING = nd;
            using (QuanLyDuLieu db = new QuanLyDuLieu())
            {
                int dangThucHien = db.CongViec.ToList().FindAll(m => m.NguoiDungID == NguoiDungING.NguoiDungID && m.PhanLoaiCongViec == "DangThucHien").Count;
                int daHoanThanh = db.CongViec.ToList().FindAll(m => m.NguoiDungID == NguoiDungING.NguoiDungID && m.PhanLoaiCongViec == "DaHoanThanh").Count;
                int chuaHoanThanh = db.CongViec.ToList().FindAll(m => m.NguoiDungID == NguoiDungING.NguoiDungID && m.PhanLoaiCongViec == "ChuaHoanThanh").Count;

                ObservableCollection<PieSegment> pieCollection = new ObservableCollection<PieSegment>();
                pieCollection.Add(new PieSegment { Color = Colors.Green, Value = dangThucHien, Name = "Công việc đang thực hiện" });
                pieCollection.Add(new PieSegment { Color = Colors.Yellow, Value = chuaHoanThanh, Name = "công việc chưa thực hiện" });
                pieCollection.Add(new PieSegment { Color = Colors.DarkCyan, Value = daHoanThanh, Name = "Công việc đã làm" });
                chartThongKe.Data = pieCollection;
            }
        }
        public ThongKe()
        {
            InitializeComponent();
        }
    }
}
