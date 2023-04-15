using DoctorWorkDayManager.ViewModels;
using DoctorWorkDayManager.Views.PatientViews;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DoctorWorkDayManager.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
        }

        private void MiniBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void WindowBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Grid)
            {
                this.DragMove();
            }
        }
    }
}
