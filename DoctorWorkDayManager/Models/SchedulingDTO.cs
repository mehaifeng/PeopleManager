using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace DoctorWorkDayManager.Models
{
    [SugarTable("Scheduling")]
    public partial class SchedulingDTO : ObservableObject
    {
        /// <summary>
        /// 序号
        /// </summary>
        string no;
        /// <summary>
        /// 序号
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string No
        {
            get { return no; }
            set
            {
                SetProperty(ref no, value);
                OnPropertyChanged(nameof(No));
            }
        }

        /// <summary>
        /// 医生姓名
        /// </summary>
        string userName;
        /// <summary>
        /// 医生姓名
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string UserName
        {
            get { return userName; }
            set
            {
                SetProperty(ref userName, value);
                OnPropertyChanged(nameof(No));
            }
        }

        /// <summary>
        /// 医生科室
        /// </summary>
        string userDepartment;
        /// <summary>
        /// 医生科室
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string UserDepartment
        {
            get { return userDepartment; }
            set
            {
                SetProperty(ref userDepartment, value);
                OnPropertyChanged(nameof(UserDepartment));
            }
        }

        [ObservableProperty]
        string id;

        [ObservableProperty]
        string dateFrom;

        [ObservableProperty]
        string dateTo;

        [ObservableProperty]
        string monday;

        [ObservableProperty]
        string tuesday;

        [ObservableProperty]
        string wednesday;

        [ObservableProperty]
        string thursday;

        [ObservableProperty]
        string friday;

        [ObservableProperty]
        string saturday;

        [ObservableProperty]
        string sunday;
    }
}
