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
    /// Interaction logic for QuanLyNguoiDung.xaml
    /// </summary>
    public partial class QuanLyNguoiDung : Window
    {
        private NguoiDung _nguoiDungING;
        public NguoiDung NguoiDungING
        {
            get { return _nguoiDungING; }
            set
            {
                _nguoiDungING = value;
                txbTenHienThi.Text = NguoiDungING.TenHienThi;
                txbTenTaiKhoan.Text = NguoiDungING.NguoiDungID;
            }
        }

        public QuanLyNguoiDung(NguoiDung nd)
        {
            InitializeComponent();
            NguoiDungING = nd;
        }

        public QuanLyNguoiDung()
        {
            InitializeComponent();
        }

        private void btnDoiMatKhau_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txbMatKhauCu.Password == "") throw new Exception("MatKhauCuRong");
                if (txbMatKhauMoi.Password == "") throw new Exception("MatKhauMoiRong");
                if (txbNhapLaiMatKhauMoi.Password == "") throw new Exception("NhapLaiMatKhauMoiRong");
                if (txbMatKhauMoi.Password.Length < 6) throw new Exception("MatKhauNgan");
                if (txbMatKhauMoi.Password != txbNhapLaiMatKhauMoi.Password) throw new Exception("MatKhauKhacNhau");
                using (QuanLyDuLieu db = new QuanLyDuLieu())
                {
                    NguoiDung nd = db.NguoiDung.ToList().Single(m => m.NguoiDungID == NguoiDungING.NguoiDungID);
                    if (nd.MatKhau != txbMatKhauCu.Password) throw new Exception("MatKhauCuKhongTrung");
                    nd.MatKhau = txbMatKhauMoi.Password;
                    db.SaveChanges();
                    MessageBox.Show("Đổi mật khẩu thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "MatKhauCuRong")
                {
                    textThongBao.Text = "* Xin mời nhập ở mật khẩu cũ";
                    txbMatKhauCu.Focus();
                }
                else if (ex.Message == "MatKhauMoiRong")
                {
                    textThongBao.Text = "* Xin mời nhập ở mật khẩu mới";
                    txbMatKhauMoi.Focus();
                }
                else if (ex.Message == "NhapLaiMatKhauMoiRong")
                {
                    textThongBao.Text = "* Xin mời nhập ở nhập lại mật khẩu mới";
                    txbNhapLaiMatKhauMoi.Focus();
                }
                else if (ex.Message == "MatKhauNgan")
                {
                    textThongBao.Text = "* Mật khẩu phải nhập 6 ký tự trở lên";
                    txbNhapLaiMatKhauMoi.Focus();
                }
                else if (ex.Message == "MatKhauNgan")
                {
                    textThongBao.Text = "* Mật khẩu phải nhập 6 ký tự trở lên";
                    txbMatKhauMoi.Focus();
                }
                else if (ex.Message == "MatKhauKhacNhau")
                {
                    textThongBao.Text = "* Mật khẩu nhập vào khác nhau";
                    txbNhapLaiMatKhauMoi.Focus();
                }
                else if (ex.Message == "MatKhauCuKhongTrung")
                {
                    textThongBao.Text = "* Mật khẩu nhập cũ không trùng";
                    txbMatKhauCu.Focus();
                }
                else
                {
                    MessageBox.Show(ex.Message, "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnSuaTenHienThi_Click(object sender, RoutedEventArgs e)
        {
            using (QuanLyDuLieu db = new QuanLyDuLieu())
            {
                NguoiDung nd = db.NguoiDung.ToList().Single(m => m.NguoiDungID == NguoiDungING.NguoiDungID);
                nd.TenHienThi = txbTenHienThi.Text;
                db.SaveChanges();
                MessageBox.Show("Đổi tên hiển thị thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnXoaDuLieu_Click(object sender, RoutedEventArgs e)
        {
            using (QuanLyDuLieu db = new QuanLyDuLieu())
            {
                List<SuKien> sk = db.SuKien.ToList().FindAll(m => m.NguoiDungID == NguoiDungING.NguoiDungID && m.TieuDe == ("###" + NguoiDungING.NguoiDungID + "***"));
                List<PhanLoaiSuKien> listplsk = new List<PhanLoaiSuKien>();
                foreach (SuKien i in sk)
                    listplsk.Add(db.PhanLoaiSuKien.ToList().Find(m => m.PhanLoaiSuKienID == i.PhanLoaiSuKienID));

                foreach (PhanLoaiSuKien item in listplsk)
                {
                    PhanLoaiSuKien plsk = item;
                    foreach (SuKien i in plsk.SuKien.ToList())
                    {
                        db.SuKien.ToList().Remove(i);
                    }
                    db.SaveChanges();
                    PhanLoaiSuKien plskXoa = db.PhanLoaiSuKien.ToList().Find(m => m.PhanLoaiSuKienID == plsk.PhanLoaiSuKienID);
                    db.PhanLoaiSuKien.Remove(plskXoa);
                    db.SaveChanges();
                }
                List<CongViec> lcv = db.CongViec.ToList().FindAll(m => m.NguoiDungID == NguoiDungING.NguoiDungID);
                foreach(CongViec i in lcv)
                {
                    db.CongViec.Remove(i);
                }
                db.SaveChanges();
                MessageBox.Show("Xóa dữ liệu thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private void btnXoaNguoiDung_Click(object sender, RoutedEventArgs e)
        {
            this.btnXoaDuLieu_Click(sender, e);
            using(QuanLyDuLieu db = new QuanLyDuLieu())
            {
                NguoiDung nd = db.NguoiDung.ToList().Find(m => m.NguoiDungID == NguoiDungING.NguoiDungID);
                db.NguoiDung.Remove(nd);
                db.SaveChanges();
                MessageBox.Show("Xóa người dùng thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
