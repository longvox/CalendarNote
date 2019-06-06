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
    /// Interaction logic for PhanLoaiSuKien.xaml
    /// </summary>
    public partial class PLSuKien : Window
    {
        public NguoiDung NguoiDungING { get; set; }

        public PLSuKien(NguoiDung nd)
        {
            InitializeComponent();
            NguoiDungING = nd;
            loadDBtoDataGrid();
        }

        public PLSuKien()
        {
            InitializeComponent();
            loadDBtoDataGrid();
        }

        private void Click_btnThem(object sender, RoutedEventArgs e)
        {
            using (QuanLyDuLieu db = new QuanLyDuLieu())
            {
                PhanLoaiSuKien plsk = new PhanLoaiSuKien
                {
                    TieuDe = txbTieuDe.Text == "" ? "(Không có tiêu đề)" : txbTieuDe.Text,
                    HienThi = true,
                };
                db.PhanLoaiSuKien.Add(plsk);
                db.SaveChanges();
                SuKien sk = new SuKien
                {
                    TieuDe = "###" + NguoiDungING.NguoiDungID + "***",
                    ThoiGianBatDau = DateTime.Now,
                    ThoiGianKetThuc = DateTime.Now,
                    LapLai = true,
                    KhungThoiGianLap = "",
                    ThongBao = true,
                    ThoiGianThongBao = 0,
                    KhungThoiGianThongBao = "",
                    Mau = "",
                    NoiDung = "",
                    NguoiDungID = NguoiDungING.NguoiDungID,
                    PhanLoaiSuKienID = plsk.PhanLoaiSuKienID,
                };
                db.SuKien.Add(sk);
                db.SaveChanges();
                txbTieuDe.Text = "";
                List<PhanLoaiSuKien> lplsk = db.PhanLoaiSuKien.ToList();
                loadDBtoDataGrid();
            }
        }

        private void Click_btnSua(object sender, RoutedEventArgs e)
        {
            if (dataGirdDSPhanLoaiSuKien.SelectedIndex >= 0)
            {
                using (QuanLyDuLieu db = new QuanLyDuLieu())
                {
                    PhanLoaiSuKien plsk = (PhanLoaiSuKien)dataGirdDSPhanLoaiSuKien.SelectedItem;
                    PhanLoaiSuKien plskSua = db.PhanLoaiSuKien.ToList().SingleOrDefault(m => m.PhanLoaiSuKienID == plsk.PhanLoaiSuKienID);
                    plskSua.TieuDe = txbTieuDe.Text;
                    MessageBox.Show("Sửa đổi thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    db.SaveChanges();
                }
                loadDBtoDataGrid();
            }
            else
                MessageBox.Show("Vui lòng chọn giá trị để sửa.", "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Click_btnXoa(object sender, RoutedEventArgs e)
        {
            if (dataGirdDSPhanLoaiSuKien.SelectedIndex >= 0)
            {
                using (QuanLyDuLieu db = new QuanLyDuLieu())
                {
                    PhanLoaiSuKien plsk = (PhanLoaiSuKien)dataGirdDSPhanLoaiSuKien.SelectedItem;
                    List<PhanLoaiSuKien> lplsk = db.PhanLoaiSuKien.ToList();
                    foreach (SuKien item in plsk.SuKien.ToList())
                    {
                        db.SuKien.ToList().Remove(item);
                    }
                    db.SaveChanges();
                    PhanLoaiSuKien plskXoa = db.PhanLoaiSuKien.ToList().Find(m => m.PhanLoaiSuKienID == plsk.PhanLoaiSuKienID);
                    db.PhanLoaiSuKien.Remove(plskXoa);
                    db.SaveChanges();
                }
                loadDBtoDataGrid();
                txbTieuDe.Text = "";
            }
        }

        private void loadDBtoDataGrid()
        {
            using (QuanLyDuLieu db = new QuanLyDuLieu())
            {
                List<SuKien> sk = db.SuKien.ToList().FindAll(m => m.NguoiDungID == NguoiDungING.NguoiDungID && m.TieuDe == ("###" + NguoiDungING.NguoiDungID + "***"));
                List<PhanLoaiSuKien> plsk = new List<PhanLoaiSuKien>();
                foreach (SuKien i in sk)
                    plsk.Add(db.PhanLoaiSuKien.ToList().Find(m => m.PhanLoaiSuKienID == i.PhanLoaiSuKienID));

                dataGirdDSPhanLoaiSuKien.ItemsSource = plsk;
            }
        }

        private void dataGirdDSPhanLoaiSuKien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGirdDSPhanLoaiSuKien.SelectedIndex >= 0)
                txbTieuDe.Text = ((PhanLoaiSuKien)dataGirdDSPhanLoaiSuKien.SelectedItem).TieuDe;
        }
    }
}
