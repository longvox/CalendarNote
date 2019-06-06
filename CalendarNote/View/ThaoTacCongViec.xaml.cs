using CalendarNote.Model;
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

namespace CalendarNote.View
{
    /// <summary>
    /// Interaction logic for ThaoTacCongViec.xaml
    /// </summary>
    public partial class ThaoTacCongViec : Window
    {
        public CongViec CongViecING { get; set; }
        public ThaoTacCongViec(CongViec cv)
        {
            InitializeComponent();
            CongViecING = cv;
            txbTieuDe.Text = CongViecING.TieuDe;
            txbNoiDung.Text = CongViecING.NoiDung;
            datePickerHoanThanh.SelectedDate = CongViecING.ThoiGianHoanThanh;
            timePickerHoanThanh.SelectedTime = CongViecING.ThoiGianHoanThanh;
        }

        public ThaoTacCongViec()
        {
            InitializeComponent();
        }

        private void Click_btnSua(object sender, RoutedEventArgs e)
        {
            using (QuanLyDuLieu db = new QuanLyDuLieu())
            {
                CongViec svSua = db.CongViec.ToList().Single(m => m.CongViecID == CongViecING.CongViecID);
                svSua.TieuDe = txbTieuDe.Text;
                svSua.NoiDung = txbNoiDung.Text;
                DateTime dtHoanThanh = datePickerHoanThanh.SelectedDate == null ? DateTime.Now : (DateTime)datePickerHoanThanh.SelectedDate;
                DateTime ttHoanThanh = timePickerHoanThanh.SelectedTime == null ? new DateTime(1, 1, 1, 23, 59, 0) : (DateTime)timePickerHoanThanh.SelectedTime;
                svSua.ThoiGianHoanThanh = new DateTime(dtHoanThanh.Year, dtHoanThanh.Month, dtHoanThanh.Month, ttHoanThanh.Hour, ttHoanThanh.Minute, ttHoanThanh.Second);
                db.SaveChanges();
                MessageBox.Show("Sửa công việc thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }

        private void Click_btnXoa(object sender, RoutedEventArgs e)
        {
            using (QuanLyDuLieu db = new QuanLyDuLieu())
            {
                CongViec svXoa = db.CongViec.ToList().Single(m => m.CongViecID == CongViecING.CongViecID);
                db.CongViec.Remove(svXoa);
                db.SaveChanges();
                MessageBox.Show("Xóa công việc thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }
    }
}
