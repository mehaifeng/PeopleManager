using CommunityToolkit.Mvvm.ComponentModel;
using SqlSugar;
using System;

namespace DoctorWorkDayManager.Models
{
    [SugarTable("UserInfo")]
    public partial class UserInfoDTO:ObservableObject
    {
        private int _no;
        /// <summary>
        /// 序号（不在数据库）
        /// </summary>
        [SugarColumn(IsIgnore =true)]
        public int No
        {
            get => _no;
            set
            {
                SetProperty(ref _no, value);
                OnPropertyChanged(nameof(No));
            }
        }
        /// <summary>
        /// 用户编号
        /// </summary>
        [ObservableProperty]
        public string? id;
        /// <summary>
        /// 用户姓名
        /// </summary>
        [ObservableProperty]
        public string? name;
        /// <summary>
        /// 性别
        /// </summary>
        [ObservableProperty]
        public string? gender;
        /// <summary>
        /// 身份证号
        /// </summary>
        [ObservableProperty]
        public string? personid;
        /// <summary>
        /// 联系电话
        /// </summary>
        [ObservableProperty]
        public string? contact;
        /// <summary>
        /// 学历
        /// </summary>
        [ObservableProperty]
        public string? academic;
        /// <summary>
        /// 科室
        /// </summary>
        [ObservableProperty]
        public string? department;
        /// <summary>
        /// 诊室
        /// </summary>
        [ObservableProperty]
        public string? room;
        /// <summary>
        /// 职称
        /// </summary>
        [ObservableProperty]
        public string? joblevel;
        /// <summary>
        /// 工作日期
        /// </summary>
        [ObservableProperty]
        public DateOnly? workDate;
        /// <summary>
        /// 用户类型
        /// 0：管理员；1：医生:3:医生兼管理员
        /// </summary>
        [ObservableProperty]
        public string? userType;
    }
}
