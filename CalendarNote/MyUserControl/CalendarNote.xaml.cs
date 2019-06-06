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
    /// Interaction logic for CalendarNote.xaml
    /// </summary>
    public partial class CalendarNote : UserControl
    {

        private const int NUMOFDAY = 7;
        private const int NUMOFWEEK = 6;
        public DateTime[][] arr_Calendar = new DateTime[NUMOFDAY][];
        public DayNote[][] dnDate = new DayNote[NUMOFDAY][];
        private DateTime _dateTimecurrent;
        public List<List<SuKien>> ListSuKien;
        public DateTime DateTimeCurrent
        {
            get { return _dateTimecurrent; }
            set
            {
                if (_dateTimecurrent != value)
                {
                    _dateTimecurrent = value;
                    
                    for (int i = 0; i < NUMOFWEEK; i++)
                        for (int j = 0; j < NUMOFDAY; j++)
                            if (dnDate[i][j].TDay.Day == DateTimeCurrent.Day
                            && dnDate[i][j].TDay.Month == DateTimeCurrent.Month
                            && dnDate[i][j].TDay.Year == DateTimeCurrent.Year)
                                dnDate[i][j].khung.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EEAEEE"));
                            else dnDate[i][j].khung.Background = new SolidColorBrush(Colors.Transparent);
                }
            }
        }

        public event RoutedEventHandler ClickNextMonth;
        public event RoutedEventHandler ClickPreviousMonth;
        public event RoutedEventHandler ClickToDay;
        public event RoutedEventHandler DoubleClickDate;
        public event RoutedEventHandler ClickDate;

        public CalendarNote()
        {
            InitializeComponent();

            for (int i = 0; i < NUMOFWEEK; i++)
            {
                arr_Calendar[i] = new DateTime[NUMOFDAY];
                dnDate[i] = new DayNote[NUMOFDAY];
            }

            for (int i = 0; i < NUMOFWEEK; i++)
                grCalendar.RowDefinitions.Add(new RowDefinition());
            for (int i = 0; i < NUMOFDAY; i++)
                grCalendar.ColumnDefinitions.Add(new ColumnDefinition());

            for (int i = 0; i < NUMOFWEEK; i++)
                for (int j = 0; j < NUMOFDAY; j++)
                {
                    Border borLine = new Border();
                    borLine.BorderBrush = new SolidColorBrush(Color.FromRgb(75, 75, 75));
                    borLine.BorderThickness = new Thickness(0, 1, 1, 0);
                    borLine.Background = Brushes.Transparent;
                    borLine.SetValue(Grid.RowProperty, i);
                    borLine.SetValue(Grid.ColumnProperty, j);
                    grCalendar.Children.Add(borLine);

                    dnDate[i][j] = new DayNote();

                    dnDate[i][j].SetCusorGrid(i, j);
                    grCalendar.Children.Add(dnDate[i][j]);
                }
                    Buil_Calendar(DateTime.Now);
        }

        public void Buil_Calendar(DateTime date)
        {
            AlgTimePicker.ConvertListtoArray(AlgTimePicker.calendar(date), ref arr_Calendar);

            TxbMonthCurrent.Text = "Tháng " + date.Month.ToString() + ", " + date.Year.ToString();
            for (int i = 0; i < NUMOFWEEK; i++)
                for (int j = 0; j < NUMOFDAY; j++)
                {
                    dnDate[i][j].TDay = arr_Calendar[i][j];
                    dnDate[i][j].TDate = DayNote.TypeDate.Dates;
                    if (DateTime.Now.Year == date.Year &&
                        DateTime.Now.Month == date.Month &&
                        DateTime.Now.Day == arr_Calendar[i][j].Day)
                        dnDate[i][j].TDate = DayNote.TypeDate.ToDay;

                    if (date.Month != arr_Calendar[i][j].Month)
                        dnDate[i][j].TDate = DayNote.TypeDate.DayOutMonth;
                    
                }

            DateTimeCurrent = date;
        }

        private void click_btnPreviousMonth(object sender, RoutedEventArgs e)
        {
            Buil_Calendar(DateTimeCurrent.AddMonths(-1));
            if (ClickPreviousMonth != null)
            {
                ClickPreviousMonth(this, new RoutedEventArgs());
            }
        }

        private void click_btnNextMonth(object sender, RoutedEventArgs e)
        {
            Buil_Calendar(DateTimeCurrent.AddMonths(1));
            if (ClickNextMonth != null)
            {
                ClickNextMonth(this, new RoutedEventArgs());
            }
        }

        private void click_btnToday(object sender, RoutedEventArgs e)
        {
            Buil_Calendar(DateTime.Now);
            if (ClickToDay != null)
            {
                ClickToDay(this, new RoutedEventArgs());
            }
        }

        public void ShowDateEvent(List<List<SuKien>> danhSachSuKien)
        {
            ListSuKien = danhSachSuKien;
            for (int i = 0; i < NUMOFWEEK; i++)
                for (int j = 0; j < NUMOFDAY; j++)
                {
                    dnDate[i][j].DanhSachSuKien = danhSachSuKien[i * 7 + j];
                }
        }

        private void OnPreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int row = 0;
            int col = 0;
            if (e.ClickCount == 1) // for double-click, remove this condition if only want single click
            {
                GetCursorMouse(ref row, ref col);
                
                DateTimeCurrent = arr_Calendar[row][col];
                if (ClickDate != null)
                {
                    ClickDate(this, new RoutedEventArgs());
                }
            }
            else if (e.ClickCount == 2) // for double-click, remove this condition if only want single click
            {
                GetCursorMouse(ref row, ref col); 

                DateTimeCurrent = arr_Calendar[row][col];
                if (DoubleClickDate != null)
                {
                    DoubleClickDate(this, new RoutedEventArgs());
                }
            }
        }

        public void GetCursorMouse(ref int rows, ref int cols)
        {
            var point = Mouse.GetPosition(grCalendar);

            int row = 0;
            int col = 0;

            double accumulatedHeight = 0.0;
            double accumulatedWidth = 0.0;

            // calc row mouse was over
            foreach (var rowDefinition in grCalendar.RowDefinitions)
            {
                accumulatedHeight += rowDefinition.ActualHeight;
                if (accumulatedHeight >= point.Y)
                    break;
                row++;
            }

            // calc col mouse was over
            foreach (var columnDefinition in grCalendar.ColumnDefinitions)
            {
                accumulatedWidth += columnDefinition.ActualWidth;
                if (accumulatedWidth >= point.X)
                    break;
                col++;
            }

            rows = row;
            cols = col;
        }
    }
}
