using DoctorWorkDayManager.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace DoctorWorkDayManager.Views.PatientViews
{
    /// <summary>
    /// PatientManageView.xaml 的交互逻辑
    /// </summary>
    public partial class PatientManageView : UserControl
    {
        public PatientManageView()
        {
            InitializeComponent();
            this.DataContext = new PatientManageViewModel();
        }
    }
}
