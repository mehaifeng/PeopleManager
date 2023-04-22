using DoctorWorkDayManager.ViewModels;
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

namespace DoctorWorkDayManager.Views.SchedulingView
{
    /// <summary>
    /// SchedulingView.xaml 的交互逻辑
    /// </summary>
    public partial class SchedulingView : UserControl
    {
        SchedulingViewModel _viewmodel;
        public SchedulingView()
        {
            InitializeComponent();
            _viewmodel = new SchedulingViewModel();
            this.DataContext = _viewmodel;
            Department_combobox.ItemsSource = new List<string>
            {
                "全部",
                "内科",
                "外科",
                "骨科",
                "眼科",
                "泌尿科",
                "妇科",
                "口腔科",
                "消化内科"
            };
            Department_combobox.SelectedValue = "全部";
        }
    }
}
