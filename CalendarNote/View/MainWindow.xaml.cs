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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalendarNote.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private NguoiDung _nguoiDungING;
        public NguoiDung NguoiDungING
        {
            get
            {
                return _nguoiDungING;
            }
            set
            {
                _nguoiDungING = value;
                textTenHienThi.Text = NguoiDungING.TenHienThi;
            }
        }
        private DateTime ngayDangChon;
        private DateTime NgayCu;

        public DateTime NgayDangChon
        {
            get { return ngayDangChon; }
            set
            {
                ngayDangChon = value;
                cnLich.Buil_Calendar(NgayDangChon);
                LoadSuKienToLich();
            }
        }

        public MainWindow(NguoiDung nd)
        {
            InitializeComponent();
            NguoiDungING = nd;
            LoadDefault();
        }

        public MainWindow()
        {
            InitializeComponent();
            LoadDefault();
        }
        #region Loading
        private void LoadDefault()
        {
            using (QuanLyDuLieu db = new QuanLyDuLieu())
            {

                List<NguoiDung> NguoiDung = db.NguoiDung.ToList();
                List<PhanLoaiSuKien> PhanLoaiSuKien = db.PhanLoaiSuKien.ToList();
                List<CongViec> CongViec = db.CongViec.ToList();
                List<SuKien> SuKien = db.SuKien.ToList();
                PhanLoaiSuKien plsk = db.PhanLoaiSuKien.ToList().Find(m => m.TieuDe == "Sinh nhật");
                List<SuKien> SuKienSinhNhat = db.SuKien.ToList().FindAll(m => m.NguoiDungID == "admin" && m.PhanLoaiSuKienID == plsk.PhanLoaiSuKienID);
                LoadSuKienToLich();
                LoadCongViec();
                LoadPhanLoaiSuKien();
            }
        }
        private void LoadCongViec()
        {
            using (QuanLyDuLieu db = new QuanLyDuLieu())
            {
                List<CongViec> cv = db.CongViec.ToList().FindAll(m => m.NguoiDungID == NguoiDungING.NguoiDungID);
                List<CongViecView> cvv = new List<CongViecView>();
                foreach (CongViec i in cv)
                {
                    CongViecView v = new CongViecView(i);
                    cvv.Add(v);
                };
                lvCongViec.ItemsSource = cvv;
            }
        }
        private void LoadPhanLoaiSuKien()
        {
            using (QuanLyDuLieu db = new QuanLyDuLieu())
            {
                List<SuKien> sk = db.SuKien.ToList().FindAll(m => m.NguoiDungID == NguoiDungING.NguoiDungID && m.TieuDe == ("###" + NguoiDungING.NguoiDungID + "***"));
                List<PhanLoaiSuKien> plsk = new List<PhanLoaiSuKien>();
                foreach (SuKien i in sk)
                    plsk.Add(db.PhanLoaiSuKien.ToList().Find(m => m.PhanLoaiSuKienID == i.PhanLoaiSuKienID));

                lvPhanLoaiSuKien.ItemsSource = plsk;
            }
        }
        private void LoadSuKienToLich()
        {
            cnLich.ShowDateEvent(GetSuKien());
        }

        private void btnTaoEvent_Click(object sender, RoutedEventArgs e)
        {
            ThaoTacSuKien ttsk = new ThaoTacSuKien(NguoiDungING,cnLich.DateTimeCurrent);
            ttsk.ShowDialog();
            LoadSuKienToLich();
            LoadPhanLoaiSuKien();
        }


        private List<List<SuKien>> GetSuKien()
        {
            List<List<SuKien>> ListSuKien = new List<List<SuKien>>();
            for (int i = 0; i < 42; i++)
                ListSuKien.Add(DateSuKien(cnLich.arr_Calendar[i / 7][i % 7]));
            return ListSuKien;
        }

        #region lấy sự kiện từng ngày
        private List<SuKien> DateSuKien(DateTime Ngay)
        {
            List<SuKien> sk = new List<SuKien>();
            using (QuanLyDuLieu db = new QuanLyDuLieu())
            {
                List<SuKien> skloc = db.SuKien.ToList().FindAll(m => m.NguoiDungID == NguoiDungING.NguoiDungID && m.TieuDe == ("###" + NguoiDungING.NguoiDungID + "***"));
                List<PhanLoaiSuKien> plsk = new List<PhanLoaiSuKien>();
                foreach (SuKien i in skloc)
                {
                    List<PhanLoaiSuKien> plskk = (from m in db.PhanLoaiSuKien
                                                  where i.PhanLoaiSuKienID == m.PhanLoaiSuKienID
                                                  select m).ToList();
                    plskk.ForEach(m => plsk.Add(m));
                }

                List<SuKien> listsk = new List<SuKien>();
                foreach (PhanLoaiSuKien itemplsk in plsk)
                    if (itemplsk.HienThi == true)
                    {
                        List<SuKien> lsk = itemplsk.SuKien.ToList().FindAll(m => m.TieuDe != ("###" + NguoiDungING.NguoiDungID + "***"));
                        foreach (SuKien itemsk in lsk)
                        {
                            if (itemsk.LapLai == false)
                            {
                                if (itemsk.ThoiGianBatDau <= Ngay && itemsk.ThoiGianKetThuc >= Ngay)
                                    listsk.Add(itemsk);
                            }
                            else if (itemsk.LapLai == true)
                            {

                                DateTime dateBatDau = itemsk.ThoiGianBatDau;
                                DateTime dateKetThuc = itemsk.ThoiGianKetThuc;
                                if (itemsk.ThoiGianBatDau >= Ngay)
                                {
                                    while (dateKetThuc > Ngay)
                                    {
                                        if (dateBatDau <= Ngay && dateKetThuc >= Ngay)
                                        {
                                            listsk.Add(itemsk);
                                            break;
                                        }
                                        caculatorNgayThang(ref dateBatDau, ref dateKetThuc, itemsk.KhungThoiGianLap, -1);
                                    }
                                }
                                else if (itemsk.ThoiGianBatDau <= Ngay)
                                {
                                    while (dateBatDau <= Ngay)
                                    {
                                        if (dateBatDau <= Ngay && dateKetThuc >= Ngay)
                                        {
                                            listsk.Add(itemsk);
                                            break;
                                        }
                                        caculatorNgayThang(ref dateBatDau, ref dateKetThuc, itemsk.KhungThoiGianLap, 1);
                                    }
                                }
                            }
                        }
                    }
                sk = listsk;
            }
            return sk;
        }

        private void caculatorNgayThang(ref DateTime batDau, ref DateTime ketThuc, string khungThoiGianLap, int dau)
        {
            if (khungThoiGianLap == "ngay")
            {
                batDau = batDau.AddDays(1 * dau);
                ketThuc = ketThuc.AddDays(1 * dau);
            }
            if (khungThoiGianLap == "tuan")
            {
                batDau = batDau.AddDays(7 * dau);
                ketThuc = ketThuc.AddDays(7 * dau);
            }
            if (khungThoiGianLap == "thang")
            {
                batDau = batDau.AddMonths(1 * dau);
                ketThuc = ketThuc.AddMonths(1 * dau);
            }
            if (khungThoiGianLap == "nam")
            {
                batDau = batDau.AddYears(1 * dau);
                ketThuc = ketThuc.AddYears(1 * dau);
            }
        }
        #endregion
        #endregion

        #region Menu
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "btnNguoiDung":
                    QuanLyNguoiDung qlnd = new QuanLyNguoiDung(NguoiDungING);
                    qlnd.ShowDialog();
                    using (QuanLyDuLieu db = new QuanLyDuLieu())
                    {
                        NguoiDung nd = db.NguoiDung.ToList().Find(m => m.NguoiDungID == NguoiDungING.NguoiDungID);

                        if (nd == null)
                        {
                            DangNhap dnc = new DangNhap();
                            dnc.Show();
                            this.Close();
                        }
                        if (nd.MatKhau != NguoiDungING.MatKhau)
                        {
                            DangNhap dnc = new DangNhap();
                            dnc.Show();
                            this.Close();
                        }
                        if (nd.TenHienThi != NguoiDungING.TenHienThi)
                            NguoiDungING = nd;
                        LoadSuKienToLich();
                        LoadPhanLoaiSuKien();
                        LoadCongViec();
                    }
                    break;
                case "btnPLSuKien":
                    PLSuKien plsk = new PLSuKien(NguoiDungING);
                    plsk.ShowDialog();
                    LoadSuKienToLich();
                    LoadPhanLoaiSuKien();
                    break;
                //case "btnSaoLuuKhoiPhuc":
                //    break;
                case "btnThongKe":
                    ThongKe tk = new ThongKe(NguoiDungING);
                    tk.ShowDialog();
                    break;
                case "btnDangXuat":
                    DangNhap dn = new DangNhap();
                    dn.Show();
                    this.Close();
                    break;
                case "btnThoat":
                    this.Close();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Phần lịch hiển thị
        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            NgayDangChon = calendarPicker.DisplayDate;
            cnLich.DateTimeCurrent = calendarPicker.DisplayDate;
        }

        private void calendarPicker_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {

        }

        private void cnLich_ClickNextMonth(object sender, RoutedEventArgs e)
        {
            LoadSuKienToLich();
           // calendarPicker.SelectedDate = cnLich.DateTimeCurrent;
        }

        private void cnLich_ClickPreviousMonth(object sender, RoutedEventArgs e)
        {
            LoadSuKienToLich();
        //    calendarPicker.SelectedDate = cnLich.DateTimeCurrent;
        }

        private void cnLich_ClickToDay(object sender, RoutedEventArgs e)
        {
            LoadSuKienToLich();
           // calendarPicker.SelectedDate = cnLich.DateTimeCurrent;
        }


        #endregion

        private void btnHienCongViec_Click(object sender, RoutedEventArgs e)
        {
            dsCongViec.Width = iconAnHien.Kind == MaterialDesignThemes.Wpf.PackIconKind.ChevronDoubleLeft ? new GridLength(300) : new GridLength(0);
            iconAnHien.Kind = iconAnHien.Kind == MaterialDesignThemes.Wpf.PackIconKind.ChevronDoubleLeft ?
                MaterialDesignThemes.Wpf.PackIconKind.ChevronDoubleRight : MaterialDesignThemes.Wpf.PackIconKind.ChevronDoubleLeft;
        }

        private void btnCongViec_Click(object sender, RoutedEventArgs e)
        {
            using (QuanLyDuLieu db = new QuanLyDuLieu())
            {
                CongViec cv = new CongViec
                {
                    TieuDe = txbTieuDeCongViec.Text,
                    ThoiGianHoanThanh = DateTime.Now,
                    NoiDung = "",
                    PhanLoaiCongViec = "DangThucHien",
                    NguoiDungID = NguoiDungING.NguoiDungID
                };
                txbTieuDeCongViec.Text = "";
                db.CongViec.Add(cv);
                db.SaveChanges();
                LoadCongViec();
            }
        }

        private void lvCongViec_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CongViecView cvv = (CongViecView)lvCongViec.SelectedItem;
            if (cvv != null)
                using (QuanLyDuLieu db = new QuanLyDuLieu())
                {
                    CongViec cv = db.CongViec.ToList().Find(m => m.CongViecID == cvv.CongViecID);
                    ThaoTacCongViec ttcv = new ThaoTacCongViec(cv);
                    ttcv.ShowDialog();
                    LoadCongViec();
                }
        }

        private void cnLich_DoubleClickDate(object sender, RoutedEventArgs e)
        {
            int row = 0;
            int col = 0;
            cnLich.GetCursorMouse(ref row, ref col);
            ThaoTacSuKienNgay ttskn = new ThaoTacSuKienNgay(NguoiDungING, cnLich.ListSuKien[row * 7 + col], cnLich.arr_Calendar[row][col]);
            ttskn.ShowDialog();
            LoadSuKienToLich();
        }


        private void CheckBox_Check(object sender, RoutedEventArgs e)
        {
            CheckBox chkBox = sender as CheckBox;

            PhanLoaiSuKien plsk = (PhanLoaiSuKien)chkBox.DataContext;
            using (QuanLyDuLieu db = new QuanLyDuLieu())
            {
                PhanLoaiSuKien plskThayDoi = db.PhanLoaiSuKien.ToList().Single(m => m.PhanLoaiSuKienID == plsk.PhanLoaiSuKienID);
                plskThayDoi.HienThi = chkBox.IsChecked ?? false;
                db.SaveChanges();
            }
            LoadPhanLoaiSuKien();
            LoadSuKienToLich();
        }

        private void CheckBoxCongViec_Check(object sender, RoutedEventArgs e)
        {
            CheckBox chkBox = sender as CheckBox;

            CongViecView cvv = (CongViecView)chkBox.DataContext;
            using (QuanLyDuLieu db = new QuanLyDuLieu())
            {
                CongViec cvThayDoi = db.CongViec.ToList().Single(m => m.CongViecID == cvv.CongViecID);
                cvThayDoi.PhanLoaiCongViec = chkBox.IsChecked == null ? cvThayDoi.PhanLoaiCongViec : ((bool)chkBox.IsChecked ? "DaHoanThanh" : "DangThucHien");
                db.SaveChanges();
            }
            LoadCongViec();
        }
    }
}
