﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoctorWorkDayManager.Models;
using DoctorWorkDayManager.Server.Dbconn;
using SqlSugar;
using SqlSugar.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DoctorWorkDayManager.ViewModels
{
    public partial class SchedulingViewModel:ObservableObject
    {
        public SchedulingViewModel()
        {
            InitLoad();
            //获取本周的周一和周日日期
            GetStartAndEndWeek();
        }
        #region 属性
        SqlSugarClient db;
        /// <summary>
        /// 数据源
        /// </summary>
        [ObservableProperty]
        ObservableCollection<SchedulingDTO> schedulings;

        /// <summary>
        /// 一周之始
        /// </summary>
        [ObservableProperty]
        DateTime weekStart;

        /// <summary>
        /// 一周之末
        /// </summary>
        [ObservableProperty]
        DateTime weekEnd;

        /// <summary>
        /// 是否显示用户详情
        /// </summary>
        [ObservableProperty]
        Visibility isShowDoctorHeader = Visibility.Collapsed;

        /// <summary>
        /// 选中行（单）
        /// </summary>
        [ObservableProperty]
        SchedulingDTO selectedItem;

        /// <summary>
        /// 头像
        /// </summary>
        [ObservableProperty]
        string avaterPath;

        /// <summary>
        /// 姓名
        /// </summary>
        [ObservableProperty]
        string top_name;

        /// <summary>
        /// 性别
        /// </summary>
        [ObservableProperty]
        string top_gender;

        /// <summary>
        /// 参加工作时间
        /// </summary>
        [ObservableProperty]
        string top_workdate;

        /// <summary>
        /// 医生编号
        /// </summary>
        [ObservableProperty]
        string top_id;
        /// <summary>
        /// 职称
        /// </summary>
        [ObservableProperty]
        string top_joblevel;

        /// <summary>
        /// 科室
        /// </summary>
        [ObservableProperty]
        string top_department;

        /// <summary>
        /// 诊室
        /// </summary>
        [ObservableProperty]
        string top_room;

        /// <summary>
        /// 联系电话
        /// </summary>
        [ObservableProperty]
        string top_contact;
        #endregion

        /// <summary>
        /// 初始化操作
        /// </summary>
        void InitLoad()
        {
            ConnectionConfig connectionConfig = new ConnectionConfig();
            connectionConfig.ConnectionString = ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString;
            connectionConfig.DbType = DbType.SqlServer;
            connectionConfig.InitKeyType = InitKeyType.SystemTable;
            db = new SqlSugarClient(connectionConfig);
            Schedulings = new ObservableCollection<SchedulingDTO>(db.Queryable<SchedulingDTO>().ToList());
            //重新排序
            NoSorting();
            // 插入用户数据
            InsertUserInfo();
        }
        /// <summary>
        /// 插入用户数据
        /// </summary>
        void InsertUserInfo()
        {
            foreach (var item in Schedulings)
            {
                item.UserName =  db.Queryable<UserInfoDTO>().Where(t => t.Id == item.Id).ToList().First().Name;
                item.UserDepartment = db.Queryable<UserInfoDTO>().Where(t => t.Id == item.Id).ToList().First().Department;
            }
        }
        /// <summary>
        /// 编号排序
        /// </summary>
        void NoSorting()
        {
            int i = 1;
            foreach (var item in Schedulings)
            {
                item.No = i.ToString();
                i++;
            }
        }

        /// <summary>
        /// 获取本周的开始和结束日期
        /// </summary>
        /// <returns></returns>
        public void GetStartAndEndWeek()
        {
            //星期一的日期
            DateTime dt = DateTime.Now;
            int weeknow = Convert.ToInt32(dt.DayOfWeek);
            int daydiff = (-1) * weeknow + 1;
            DateTime startWeek = dt.AddDays(daydiff);
            WeekStart = startWeek;
            //星期日的日期
            int daydiff2 = 7 - weeknow;
            DateTime endWeek = dt.AddDays(daydiff2);
            WeekEnd = endWeek;
        }

        [RelayCommand]
        void Selected()
        {
            if (SelectedItem != null)
            {
                UserInfoDTO temp = db.Queryable<UserInfoDTO>().Where(t => t.Id == SelectedItem.Id).ToList().First();

                IsShowDoctorHeader = Visibility.Visible;
                
                Top_name = "姓名：" + temp.Name;
                Top_id = "编号：" + temp.Id;
                Top_joblevel = "职称：" + temp.Joblevel;
                Top_department = "科室：" + temp.Department;
                Top_room = "诊室：" + temp.Room;
                Top_contact = "联系电话：" + temp.Contact;
                if (temp.Gender == "0")
                {
                    AvaterPath = "pack://application:,,,/Resources/Images/male.png";
                }
                else if (temp.Gender == "1")
                {
                    AvaterPath = "pack://application:,,,/Resources/Images/Female.png";
                }
            }
            else
            {
                IsShowDoctorHeader = Visibility.Collapsed;
            }
        }
    }
}
