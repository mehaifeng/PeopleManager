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

namespace DoctorWorkDayManager.UserControls
{
    /// <summary>
    /// TimePicker.xaml 的交互逻辑
    /// </summary>
    public partial class TimePicker : UserControl
    {
        public List<int> hours { get; set; } = new List<int>();
        public List<int> minutes { get; set; } = new List<int>();
        public List<int> seconds { get; set; } = new List<int>();

        public int hour { get; set; } = 0;

        public int minute { get; set; } = 0;

        public int second { get; set; } = 0;

        public TimeSpan time { get; set; }

        public static readonly RoutedEvent TimeChangedEvent = EventManager.RegisterRoutedEvent("TimeChanged", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(TimePicker));

        public event RoutedEventHandler TimeChanged
        {
            add
            {
                AddHandler(TimeChangedEvent, value);
            }
            remove
            {
                RemoveHandler(TimeChangedEvent, value);
            }
        }
        public TimePicker()
        {
            for (int i = 0; i < 24; i++)
            {
                hours.Add(i);
            }
            for (int i = 0; i < 60; i++)
            {
                minutes.Add(i);
                seconds.Add(i);
            }

            InitializeComponent();
            this.DataContext = this;
        }

        private void hoursChanged(object sender, SelectionChangedEventArgs e)
        {
            hour = ShoursCb.SelectedIndex;
            time = new TimeSpan(hour, minute, second);
            NotifyEvent();
        }

        private void minutesChanged(object sender, SelectionChangedEventArgs e)
        {
            minute = SminutesCb.SelectedIndex;
            time = new TimeSpan(hour, minute, second);
            NotifyEvent();
        }

        private void NotifyEvent()
        {
            if (!IsLoaded)
            {
                return;
            }
            RoutedEventArgs routedEvent = new RoutedEventArgs();
            routedEvent.RoutedEvent = TimeChangedEvent;
            RaiseEvent(routedEvent);
        }
    }
}
