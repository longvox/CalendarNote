using CalendarNote.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for ThaoTacSuKien.xaml
    /// </summary>
    public partial class ThaoTacSuKien : Window
    {
        public NguoiDung NguoiDungING { get; set; }
        public SuKien SuKienING { get; set; }
        public ThaoTacSuKien(NguoiDung nd,DateTime ngaybd)
        {
            InitializeComponent();
            NguoiDungING = nd;
            datePickerBatDau.SelectedDate = ngaybd;
            LoadDefault();
            btnSua.Visibility = Visibility.Hidden;
            btnXoa.Visibility = Visibility.Hidden;
        }

        public ThaoTacSuKien(NguoiDung nd, SuKien sk)
        {
            InitializeComponent();
            NguoiDungING = nd;
            SuKienING = sk;
            using (QuanLyDuLieu db = new QuanLyDuLieu())
            {
                txbTieuDe.Text = sk.TieuDe;
                txbNoiDung.Text = sk.NoiDung;
                datePickerBatDau.SelectedDate = sk.ThoiGianBatDau;
                timePickerBatDau.SelectedTime = sk.ThoiGianBatDau;
                datePickerKetThuc.SelectedDate = sk.ThoiGianKetThuc;
                timePickerKetThuc.SelectedTime = sk.ThoiGianKetThuc;
                comboBoxPhanLoaiSuKien.SelectedItem = (PhanLoaiSuKien)db.PhanLoaiSuKien.ToList().Find(m => m.PhanLoaiSuKienID == sk.PhanLoaiSuKienID);
                checkBoxLapLai.IsChecked = sk.LapLai;
                checkBoxThongBao.IsChecked = sk.ThongBao;
                TxbThoiGian.Text = sk.ThoiGianThongBao.ToString();
            }
            btnLuu.Visibility = Visibility.Hidden;
            LoadDefault();
        }

        public ThaoTacSuKien()
        {
            InitializeComponent();
            LoadDefault();
        }

        public void LoadDefault()
        {
            comboBoxMau.ItemsSource = typeof(Colors).GetProperties();
            comboBoxMau.SelectedIndex = 0;
            loadDBtoComboBoxPLSK();
        }

        #region Lấy thông tin
        private string GetComboBoxKhungThoiGianLap()
        {
            string text = comboBoxLapLai.Text;
            if (text == "Ngày")
                return "ngay";
            if (text == "Tuần")
                return "tuan";
            if (text == "Tháng")
                return "thang";
            if (text == "Năm")
                return "nam";
            return "";
        }

        private string GetComboBoxThongBao()
        {
            string text = comboBoxThongBao.Text;
            if (text == "phút")
                return "phut";
            if (text == "giờ")
                return "gio";
            if (text == "ngay")
                return "ngay";
            return "";
        }

        private int GetThoiGianThongBao(int thoiGian, string thongBao)
        {
            int min = 0;
            int max = 0;
            if (thongBao == "phut")
                max = 59;
            else if (thongBao == "gio")
                max = 23;
            else if (thongBao == "ngay")
                max = 30;
            else if (thongBao == "khongThongBao")
                max = 0;
            if (thoiGian < min)
                return min;
            if (thoiGian > max)
                return max;
            return thoiGian;
        }

        private void loadDBtoComboBoxPLSK()
        {
            using (QuanLyDuLieu db = new QuanLyDuLieu())
            {
                List<SuKien> sk = db.SuKien.ToList().FindAll(m => m.NguoiDungID == NguoiDungING.NguoiDungID && m.TieuDe == ("###" + NguoiDungING.NguoiDungID + "***"));
                List<PhanLoaiSuKien> plsk = new List<PhanLoaiSuKien>();
                foreach (SuKien i in sk)
                    plsk.Add(db.PhanLoaiSuKien.ToList().Find(m => m.PhanLoaiSuKienID == i.PhanLoaiSuKienID));

                comboBoxPhanLoaiSuKien.ItemsSource = plsk;
                comboBoxPhanLoaiSuKien.DisplayMemberPath = "TieuDe";
                comboBoxPhanLoaiSuKien.SelectedIndex = 0;
            }
        }
        #endregion

        #region Thao tác
        private void Click_btnluu(object sender, RoutedEventArgs e)
        {
            try
            {
                using (QuanLyDuLieu db = new QuanLyDuLieu())
                {
                    if (comboBoxPhanLoaiSuKien.Items.Count == 0) throw new Exception("khongCoPhanLoaiSuKien");
                    string tieuDe = txbTieuDe.Text == "" ? "(Không có tiêu đề)" : txbTieuDe.Text;
                    DateTime dateBatDau = datePickerBatDau.SelectedDate == null ? DateTime.Now : (DateTime)datePickerBatDau.SelectedDate;
                    DateTime timeBatDau = timePickerBatDau.SelectedTime == null ? new DateTime(1, 1, 1, 0, 0, 0) : (DateTime)timePickerBatDau.SelectedTime;
                    DateTime timeKetThuc = timePickerKetThuc.SelectedTime == null ? new DateTime(1, 1, 1, 23, 59, 0) : (DateTime)timePickerKetThuc.SelectedTime;
                    DateTime dateKetThuc = (datePickerKetThuc.SelectedDate == null || datePickerKetThuc.SelectedDate < datePickerBatDau.SelectedDate) ? (DateTime)datePickerBatDau.SelectedDate : (DateTime)datePickerKetThuc.SelectedDate;

                    DateTime thoiGianBatDau = new DateTime(dateBatDau.Year, dateBatDau.Month, dateBatDau.Day, timeBatDau.Hour, timeBatDau.Minute, timeBatDau.Second);
                    DateTime thoiGianKetThuc = new DateTime(dateKetThuc.Year, dateKetThuc.Month, dateKetThuc.Day, timeKetThuc.Hour, timeKetThuc.Minute, timeKetThuc.Second);
                    bool lapLai = checkBoxLapLai.IsChecked == null ? false : (bool)checkBoxLapLai.IsChecked;
                    string khungThoiGianLap = lapLai ? GetComboBoxKhungThoiGianLap() : "khonglap";
                    bool thongBao = checkBoxThongBao.IsChecked == null ? false : (bool)checkBoxThongBao.IsChecked;
                    string khungThoiGianThongBao = thongBao ? GetComboBoxThongBao() : "khongThongBao";
                    int thoiGianThongBao = GetThoiGianThongBao(int.Parse(TxbThoiGian.Text), khungThoiGianThongBao);

                    string Mau = ((Color)(comboBoxMau.SelectedItem as PropertyInfo).GetValue(null, null)).ToString();
                    string NoiDung = txbNoiDung.Text;

                    string nguoiDungID = NguoiDungING.NguoiDungID;
                    int phanLoaiSuKienID = ((PhanLoaiSuKien)comboBoxPhanLoaiSuKien.SelectionBoxItem).PhanLoaiSuKienID;

                    SuKien sk = new SuKien
                    {
                        TieuDe = tieuDe,
                        ThoiGianBatDau = thoiGianBatDau,
                        ThoiGianKetThuc = thoiGianKetThuc,
                        LapLai = lapLai,
                        KhungThoiGianLap = khungThoiGianLap,
                        ThongBao = thongBao,
                        ThoiGianThongBao = thoiGianThongBao,
                        KhungThoiGianThongBao = khungThoiGianThongBao,
                        Mau = Mau,
                        NoiDung = NoiDung,
                        NguoiDungID = nguoiDungID,
                        PhanLoaiSuKienID = phanLoaiSuKienID,
                    };
                    db.SuKien.Add(sk);
                    db.SaveChanges();
                    MessageBox.Show("Thêm sự kiện thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "khongCoPhanLoaiSuKien")
                    MessageBox.Show("Xin mời thêm phân loại sự kiện.", "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                    MessageBox.Show(ex.Message, "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Click_btnSua(object sender, RoutedEventArgs e)
        {
            try
            {
                using (QuanLyDuLieu db = new QuanLyDuLieu())
                {
                    if (comboBoxPhanLoaiSuKien.Items.Count == 0) throw new Exception("khongCoPhanLoaiSuKien");
                    string tieuDe = txbTieuDe.Text == "" ? "(Không có tiêu đề)" : txbTieuDe.Text;
                    DateTime dateBatDau = datePickerBatDau.SelectedDate == null ? DateTime.Now : (DateTime)datePickerBatDau.SelectedDate;
                    DateTime timeBatDau = timePickerBatDau.SelectedTime == null ? new DateTime(1, 1, 1, 0, 0, 0) : (DateTime)timePickerBatDau.SelectedTime;
                    DateTime timeKetThuc = timePickerKetThuc.SelectedTime == null ? new DateTime(1, 1, 1, 23, 59, 0) : (DateTime)timePickerKetThuc.SelectedTime;
                    DateTime dateKetThuc = (datePickerKetThuc.SelectedDate == null  || datePickerKetThuc.SelectedDate  < datePickerBatDau.SelectedDate) ? (DateTime)datePickerBatDau.SelectedDate : (DateTime)datePickerKetThuc.SelectedDate;

                    DateTime thoiGianBatDau = new DateTime(dateBatDau.Year, dateBatDau.Month, dateBatDau.Day, timeBatDau.Hour, timeBatDau.Minute, timeBatDau.Second);
                    DateTime thoiGianKetThuc = new DateTime(dateKetThuc.Year, dateKetThuc.Month, dateKetThuc.Day, timeKetThuc.Hour, timeKetThuc.Minute, timeKetThuc.Second);
                    bool lapLai = checkBoxLapLai.IsChecked == null ? false : (bool)checkBoxLapLai.IsChecked;
                    string khungThoiGianLap = lapLai ? GetComboBoxKhungThoiGianLap() : "khonglap";
                    bool thongBao = checkBoxThongBao.IsChecked == null ? false : (bool)checkBoxThongBao.IsChecked;
                    string khungThoiGianThongBao = thongBao ? GetComboBoxThongBao() : "khongThongBao";
                    int thoiGianThongBao = GetThoiGianThongBao(int.Parse(TxbThoiGian.Text), khungThoiGianThongBao);

                    string Mau = ((Color)(comboBoxMau.SelectedItem as PropertyInfo).GetValue(null, null)).ToString();
                    string NoiDung = txbNoiDung.Text;

                    string nguoiDungID = NguoiDungING.NguoiDungID;
                    int phanLoaiSuKienID = ((PhanLoaiSuKien)comboBoxPhanLoaiSuKien.SelectionBoxItem).PhanLoaiSuKienID;

                    SuKien sk = db.SuKien.ToList().Single(m => m.SuKienID == SuKienING.SuKienID);
                    sk.TieuDe = tieuDe;
                    sk.ThoiGianBatDau = thoiGianBatDau;
                    sk.ThoiGianKetThuc = thoiGianKetThuc;
                    sk.LapLai = lapLai;
                    sk.KhungThoiGianLap = khungThoiGianLap;
                    sk.ThongBao = thongBao;
                    sk.KhungThoiGianThongBao = khungThoiGianThongBao;
                    sk.ThoiGianThongBao = thoiGianThongBao;
                    sk.Mau = Mau;
                    sk.NoiDung = NoiDung;
                    sk.NguoiDungID = nguoiDungID;
                    sk.PhanLoaiSuKienID = phanLoaiSuKienID;
                    db.SaveChanges();
                    MessageBox.Show("sửa dữ liệu thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "khongCoPhanLoaiSuKien")
                    MessageBox.Show("Xin mời thêm phân loại sự kiện.", "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                    MessageBox.Show(ex.Message, "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Click_btnXoa(object sender, RoutedEventArgs e)
        {
            using (QuanLyDuLieu db = new QuanLyDuLieu())
            {
                SuKien sk = db.SuKien.ToList().Find(m => m.SuKienID == SuKienING.SuKienID);
                db.SuKien.Remove(sk);
                db.SaveChanges();
                MessageBox.Show("Xóa sự kiện thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }

        private void Click_btnThemPLSK(object sender, RoutedEventArgs e)
        {
            PLSuKien plsk = new PLSuKien(NguoiDungING);
            plsk.ShowDialog();
            loadDBtoComboBoxPLSK();
        }
        #endregion
    }
}
