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
    /// Interaction logic for DangNhap.xaml
    /// </summary>
    public partial class DangNhap : Window
    {

        public DangNhap()
        {
            InitializeComponent();
        }

        private void Click_btnTaoMoi(object sender, RoutedEventArgs e)
        {
            TaoTaiKhoan taoTaiKhoan = new TaoTaiKhoan();
            taoTaiKhoan.Show();
            this.Close();
        }

        private void Click_btnDangNhap(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txbTenNguoiDung.Text == "") throw new Exception("TaiKhoanRong");
                if (txbMatKhau.Password == "") throw new Exception("MatKhauRong");
                using (QuanLyDuLieu db = new QuanLyDuLieu())
                {
                    NguoiDung nd = db.NguoiDung.ToList().Find(m => m.NguoiDungID.ToUpper() == txbTenNguoiDung.Text.ToUpper());
                    if (nd == null) throw new Exception("TaiKhoanKhongTonTai");
                    if (nd.MatKhau != txbMatKhau.Password) throw new Exception("MatKhauKhongDung");
                    MainWindow main = new MainWindow(nd);
                    main.Show();
                    this.Close();

                }
        }
            catch (Exception ex)
            {
                if (ex.Message == "TaiKhoanRong")
                {
                    textThongBao.Text = "* Bạn chưa nhập tên người dùng";
                    txbTenNguoiDung.Focus();
                }
                else if (ex.Message == "MatKhauRong")
                {
                    textThongBao.Text = "* Bạn phải nhập mật khẩu";
                    txbMatKhau.Focus();
                }
                else if (ex.Message == "TaiKhoanKhongTonTai")
                {
                    textThongBao.Text = "* Tài khoản không tồn tại";
                    txbTenNguoiDung.Focus();
                }
                else if (ex.Message == "MatKhauKhongDung")
                {
                    textThongBao.Text = "* Mật khẩu sai";
                    txbMatKhau.Focus();
                }
                else
                {
                    MessageBox.Show(ex.Message, "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
