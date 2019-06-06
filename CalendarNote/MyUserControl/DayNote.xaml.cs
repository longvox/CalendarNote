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

namespace CalendarNote.MyUserControl
{
    /// <summary>
    /// Interaction logic for DayNote.xaml
    /// </summary>
    public partial class DayNote : UserControl
    {
        private DateTime _tDay;
        public DateTime TDay
        {
            get { return _tDay; }
            set
            {
                _tDay = value;
                txtDate.Text = TDay.Day == 1 ? TDay.Day.ToString() + " thg " + TDay.Month.ToString() : TDay.Day.ToString();
            }
        }
        private List<SuKien> _danhSachSuKien;
        public List<SuKien> DanhSachSuKien
        {
            get { return _danhSachSuKien; }
            set
            {
                if (value != null)
                {
                    _danhSachSuKien = value;
                    stDayNote.Children.Clear();
                    foreach (SuKien item in DanhSachSuKien)
                    {
                        TextBlock tb = new TextBlock();
                        tb.Text = item.TieuDe;
                        tb.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(item.Mau));
                        tb.HorizontalAlignment = HorizontalAlignment.Stretch;
                        stDayNote.Children.Add(tb);
                    }

                }
            }
        }

        public enum TypeDate
        {
            ToDay = 0,
            Dates,
            DayOutMonth
        };

        private TypeDate tDate;

        public TypeDate TDate
        {
            get { return tDate; }
            set
            {
                tDate = value;
                if (tDate == TypeDate.ToDay)
                    txtDate.Foreground = new SolidColorBrush(Color.FromRgb(255, 20, 147));
                if (tDate == TypeDate.Dates)
                    txtDate.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                if (tDate == TypeDate.DayOutMonth)
                    txtDate.Foreground = new SolidColorBrush(Color.FromRgb(75, 75, 75));
            }
        }

        public DayNote()
        {
            InitializeComponent();
            TDate = TypeDate.ToDay;
            txtDate.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        }

        public DayNote(TypeDate tDate)
        {
            InitializeComponent();
            TDate = tDate;
            if (TDate == TypeDate.ToDay)
                txtDate.Foreground = new SolidColorBrush(Color.FromRgb(39, 139, 39));
            if (TDate == TypeDate.Dates)
                txtDate.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            if (TDate == TypeDate.DayOutMonth)
                txtDate.Foreground = new SolidColorBrush(Color.FromRgb(75, 75, 75));
        }

        public void SetCusorGrid(int row, int col)
        {
            this.SetValue(Grid.RowProperty, row);
            this.SetValue(Grid.ColumnProperty, col);
        }

        public static Color FromName(String name)
        {
            var color_props = typeof(Colors).GetProperties();
            foreach (var c in color_props)
                if (name.Equals(c.Name))
                    return (Color)c.GetValue(new Color(), null);
            return Colors.Blue;
        }
    }
}
