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
    /// Interaction logic for ThaoTacSuKienNgay.xaml
    /// </summary>
    public partial class ThaoTacSuKienNgay : Window
    {
        private List<SuKien> _listSuKienING;
        public List<SuKien> ListSuKienING
        {
            get { return _listSuKienING; }
            set
            {
                _listSuKienING = value;
                dataGirdDSSuKien.ItemsSource = ListSuKienING;
            }
        }
        public NguoiDung NguoiDungING { get; set; }
        public DateTime NgayING { get; set; }

        public ThaoTacSuKienNgay(NguoiDung nd, List<SuKien> lsk, DateTime ngay)
        {
            InitializeComponent();
            NguoiDungING = nd;
            ListSuKienING = lsk;
            NgayING = ngay;

        }

        public ThaoTacSuKienNgay()
        {
            InitializeComponent();
        }

        private void Click_btnSua(object sender, RoutedEventArgs e)
        {
            if (dataGirdDSSuKien.SelectedIndex >= 0)
            {
                SuKien sk = (SuKien)dataGirdDSSuKien.SelectedItem;
                SuKien skSua;
                using (QuanLyDuLieu db = new QuanLyDuLieu())
                {
                    skSua = db.SuKien.ToList().Find(m => m.SuKienID == sk.SuKienID);
                }

                ThaoTacSuKien ttsk = new ThaoTacSuKien(NguoiDungING, skSua);
                ttsk.ShowDialog();
                ListSuKienING = DateSuKien(NgayING);
            }
        }

        private void Click_btnXoa(object sender, RoutedEventArgs e)
        {
            if (dataGirdDSSuKien.SelectedIndex >= 0)
            {
                SuKien sk = (SuKien)dataGirdDSSuKien.SelectedItem;
                using (QuanLyDuLieu db = new QuanLyDuLieu())
                {
                    SuKien skXoa = db.SuKien.ToList().Find(m => m.SuKienID == sk.SuKienID);
                    db.SuKien.Remove(skXoa);
                    db.SaveChanges();
                    ListSuKienING = DateSuKien(NgayING);
                    MessageBox.Show("Xoá sự kiện thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
        }

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
                                    while (dateKetThuc < Ngay)
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
    }
}
