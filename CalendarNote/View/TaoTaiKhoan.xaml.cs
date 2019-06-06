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
    /// Interaction logic for TaoTaiKhoan.xaml
    /// </summary>
    public partial class TaoTaiKhoan : Window
    {
        public TaoTaiKhoan()
        {
            InitializeComponent();
        }

        private void Click_btnTaoMoi(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txbTenTaiKhoan.Text == "") throw new Exception("TaiKhoanRong");
                if (!KiemTraChuanTenTaiKhoan(txbTenTaiKhoan.Text.ToUpper())) throw new Exception("TaiKhoanChuaDungChuan");
                if (!KiemTraTaiKhoanTonTai(txbTenTaiKhoan.Text.ToUpper())) throw new Exception("TaiKhoanDaTonTai");
                if (txbTenHienThi.Text == "") throw new Exception("TenHienThiRong");
                if (txbMatKhau.Password == "") throw new Exception("MatKhauRong");
                if (txbNhapLaiMatKhau.Password == "") throw new Exception("NhapLaiMatKhauRong");
                if (txbMatKhau.Password.Length < 6) throw new Exception("MatKhauNgan");
                if (txbMatKhau.Password != txbNhapLaiMatKhau.Password) throw new Exception("MatKhauKhacNhau");
                NguoiDung nd = new NguoiDung
                {
                    NguoiDungID = txbTenTaiKhoan.Text,
                    TenHienThi = txbTenHienThi.Text,
                    MatKhau = txbMatKhau.Password
                };
                using (QuanLyDuLieu db = new QuanLyDuLieu())
                {
                    db.NguoiDung.Add(nd);
                    db.SaveChanges();
                    MessageBox.Show("Tạo mới tài khoản thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    DangNhap dn = new DangNhap();
                    dn.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "TaiKhoanRong")
                {
                    textThongBao.Text = "* Bắt buộc nhập tên tài khoản";
                    txbTenTaiKhoan.Focus();
                }
                else if (ex.Message == "TaiKhoanChuaDungChuan")
                {
                    textThongBao.Text = "* Tài khoản chỉ nhập a-zA-Z0-9 và gạch chân ''_''";
                    txbTenTaiKhoan.Focus();
                }
                else if (ex.Message == "TaiKhoanDaTonTai") 
                {
                    textThongBao.Text = "* Tài khoản đã tồn tại";
                    txbTenTaiKhoan.Focus();
                }
                else if (ex.Message == "TenHienThiRong")
                {
                    textThongBao.Text = "* Bắt buộc nhập tên hiển thị";
                    txbTenHienThi.Focus();
                }
                else if (ex.Message == "MatKhauRong")
                {
                    textThongBao.Text = "* Bắt buộc nhập mật khẩu";
                    txbMatKhau.Focus();
                }
                else if (ex.Message == "NhapLaiMatKhauRong")
                {
                    textThongBao.Text = "* Bắt buộc phải nhập lại mật khẩu";
                    txbNhapLaiMatKhau.Focus();
                }
                else if (ex.Message == "MatKhauNgan")
                {
                    textThongBao.Text = "* Mật khẩu phải nhập 6 ký tự trở lên";
                    txbMatKhau.Focus();
                }
                else if (ex.Message == "MatKhauKhacNhau")
                {
                    textThongBao.Text = "* Mật khẩu nhập vào khác nhau";
                    txbNhapLaiMatKhau.Focus();
                }
                else
                {
                    MessageBox.Show(ex.Message, "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private bool KiemTraChuanTenTaiKhoan(string tenTaiKhoan)
        {
            foreach (char c in tenTaiKhoan)
                if (!(('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z') || ('0' <= c && c <= '9') || (c == '_')))
                    return false;
            return true;
        }
        private bool KiemTraTaiKhoanTonTai(string tenTaiKhoan)
        {

            using(QuanLyDuLieu db = new QuanLyDuLieu())
            {
                NguoiDung nd = db.NguoiDung.ToList().Find(m => m.NguoiDungID.ToUpper() == tenTaiKhoan.ToUpper());
                if (nd != null)
                    return false;
            }
            return true;
        }
    }
}
