using DoctorWorkDayManager.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DoctorWorkDayManager.Views
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        LoginViewModel _loginViewModel;
        public Login()
        {
            InitializeComponent();
            _loginViewModel = new LoginViewModel();
            DataContext = _loginViewModel;
        }

        private void LoginGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Grid)
            {
                this.DragMove();
            }
        }

        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_Min_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
