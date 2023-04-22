using CommunityToolkit.Mvvm.ComponentModel;
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

namespace DoctorWorkDayManager.ViewModels
{
    public partial class SchedulingViewModel:ObservableObject
    {
        public SchedulingViewModel()
        {
            InitLoad();
        }
        [ObservableProperty]
        ObservableCollection<SchedulingDTO> schedulings;

        SqlSugarClient db;
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
    }
}
