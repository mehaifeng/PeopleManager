using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoctorWorkDayManager.Views;
using System.Windows;

namespace DoctorWorkDayManager.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        public LoginViewModel()
        {
            
        }
        [RelayCommand]
        void Login(Window o)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
            o.Close();
        }
    }
}
