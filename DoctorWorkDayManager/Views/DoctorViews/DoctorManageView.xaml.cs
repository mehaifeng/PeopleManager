using DoctorWorkDayManager.Models;
using DoctorWorkDayManager.ViewModels;
using System.Windows.Controls;

namespace DoctorWorkDayManager.Views.DoctorViews
{
    /// <summary>
    /// DoctorManageView.xaml 的交互逻辑
    /// </summary>
    public partial class DoctorManageView : UserControl
    {
        DoctorManageViewModel _viewModel;
        public DoctorManageView()
        {
            InitializeComponent();
            _viewModel = new DoctorManageViewModel();
            this.DataContext = _viewModel;
        }
    }
}
