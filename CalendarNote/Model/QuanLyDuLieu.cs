using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarNote.Model
{
    public class QuanLyDuLieu : DbContext
    {
        public DbSet<NguoiDung> NguoiDung { get; set; }
        public DbSet<PhanLoaiSuKien> PhanLoaiSuKien { get; set; }
        public DbSet<SuKien> SuKien { get; set; }
        public DbSet<CongViec> CongViec { get; set; }

    }
    public class DuLieuMacDinh : DropCreateDatabaseIfModelChanges<QuanLyDuLieu>
    {
        protected override void Seed(QuanLyDuLieu db)
        {
            new List<NguoiDung>
            {
                new NguoiDung {NguoiDungID="admin",TenHienThi="admin", MatKhau="admin" },
                new NguoiDung {NguoiDungID="root", TenHienThi="root",MatKhau="root" },
            }.ForEach(m => db.NguoiDung.Add(m));
            db.SaveChanges();
            new List<PhanLoaiSuKien>
            {
                new PhanLoaiSuKien {TieuDe="Sinh nhật",HienThi=true},
                new PhanLoaiSuKien {TieuDe="Công việc quan trọng",HienThi=true},
            }.ForEach(m => db.PhanLoaiSuKien.Add(m));
            db.SaveChanges();
            new List<CongViec>
            {
                new CongViec {
                    TieuDe ="làm bài tập",
                    NoiDung ="deadline sấp mặt",
                    ThoiGianHoanThanh =DateTime.Now,
                    PhanLoaiCongViec ="DangThucHien",
                    NguoiDungID = "admin",},
                new CongViec {
                    TieuDe ="đi nhậu",
                    NoiDung ="nhậu sấp mặt",
                    ThoiGianHoanThanh =DateTime.Now,
                    PhanLoaiCongViec ="DangThucHien",
                    NguoiDungID = "root",}
            }.ForEach(m => db.CongViec.Add(m));
            db.SaveChanges();
            new List<SuKien>
            {
                new SuKien {
                    TieuDe ="sinh nhật abc",
                    ThoiGianBatDau =DateTime.Now,
                    ThoiGianKetThuc =DateTime.Now,
                    LapLai =true,
                    KhungThoiGianLap ="Ngay",
                    ThongBao =true,
                    ThoiGianThongBao =30,
                    KhungThoiGianThongBao ="Phut",
                    Mau ="Black",
                    NoiDung ="sinh nhật",
                    NguoiDungID ="admin",
                    PhanLoaiSuKienID = db.PhanLoaiSuKien.ToList().Find(m=>m.TieuDe=="Sinh nhật").PhanLoaiSuKienID,
                },
                new SuKien
                {
                    TieuDe = "sinh nhật xyz",
                    ThoiGianBatDau = DateTime.Now,
                    ThoiGianKetThuc = DateTime.Now,
                    LapLai = true,
                    KhungThoiGianLap = "Ngay",
                    ThongBao = true,
                    ThoiGianThongBao = 30,
                    KhungThoiGianThongBao = "Phut",
                    Mau = "Black",
                    NoiDung = "sinh nhật",
                    NguoiDungID = "admin",
                    PhanLoaiSuKienID = db.PhanLoaiSuKien.ToList().Find(m => m.TieuDe == "Công việc quan trọng").PhanLoaiSuKienID,
                }
            }.ForEach(m => db.SuKien.Add(m));
            db.SaveChanges();
        }
    }
}
