using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoctorWorkDayManager.Models;
using DoctorWorkDayManager.Server.Dbconn;
using SqlSugar;
using SqlSugar.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DoctorWorkDayManager.ViewModels
{
    public partial class DoctorManageViewModel : ObservableObject
    {
        public SqlSugarClient db;

        public DoctorManageViewModel()
        {
            DataInit();
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        void DataInit()
        {

            db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.SystemTable
            });
            UserInfos = new ObservableCollection <UserInfoDTO>(DbSugarCoreHelper.ListAsync(db));
            
            if (UserInfos.Count > 0)
            {
                for (int i = 0; i < UserInfos.Count; i++)
                {
                    UserInfos[i].No = i;
                }
                
            }
        }
        [RelayCommand]
        void Refresh()
        {
            DataInit();
        }
        /// <summary>
        /// 选择某行按下Enter时添加新行
        /// </summary>
        /// <param name="SelectItem"></param>
        [RelayCommand]
        void AddNewEmptyRow(UserInfoDTO SelectItem)
        {
            if(SelectItem == null)
            {
                UserInfos.Add(new UserInfoDTO { No = UserInfos.Count });
            }
            if (SelectItem?.No != null)
            {
                UserInfos.Insert(SelectItem.No + 1, new UserInfoDTO { No = SelectItem.No+1 });
                //UserInfos子集的No重新从0开始排序
                foreach (var item in UserInfos)
                {
                    item.No = UserInfos.IndexOf(item);
                }
            }
        }
        /// <summary>
        /// 移除某些行
        /// </summary>
        /// <param name="SelectItems"></param>
        [RelayCommand]
        void RemoveItem(IList SelectItems)
        {
            foreach (var item in SelectItems.Cast<UserInfoDTO>().ToList())
            {
                UserInfos.Remove(item);
            }
            //UserInfos子集的No重新从0开始排序
            foreach (var item in UserInfos)
            {
                item.No = UserInfos.IndexOf(item);
            }
        }
        /// <summary>
        /// 选中行事件
        /// </summary>
        [RelayCommand]
        void Selected()
        {
            if (SelectedItem != null)
            {
                IsShowDoctorHeader = Visibility.Visible;
                Top_name = "姓名：" + SelectedItem.Name;
                Top_id = "编号：" + SelectedItem.Id;
                Top_joblevel = "职称：" + SelectedItem.Joblevel;
                Top_department = "科室：" + SelectedItem.Department;
                Top_room = "诊室：" + SelectedItem.Room;
                Top_contact = "联系电话：" + SelectedItem.Contact;
                if(SelectedItem.Gender == "0")
                {
                    AvaterPath = "pack://application:,,,/Resources/Images/male.png";
                }
                else if(SelectedItem.Gender == "1")
                {
                    AvaterPath = "pack://application:,,,/Resources/Images/Female.png";
                }
            }
            else
            {
                IsShowDoctorHeader = Visibility.Collapsed;
            }
        }

        [RelayCommand]
        void SearchPerson(string NameOrId)
        {
            //在UserInfos中根据Name或者Id，查出用户并高亮该行
            var item = UserInfos.FirstOrDefault(t => t.Name == NameOrId || t.Id == NameOrId);
            if(item != null)
            {
                int index = UserInfos.IndexOf(item);
                SelectedItem = item;
            }
            else
            {
                MessageBox.Show("未找到该用户");
            }
        }

        /// <summary>
        /// 是否显示用户详情
        /// </summary>
        [ObservableProperty]
        Visibility isShowDoctorHeader = Visibility.Collapsed;

        /// <summary>
        /// 选中行（单）
        /// </summary>
        [ObservableProperty]
        UserInfoDTO selectedItem;

        /// <summary>
        /// 元素集合
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<UserInfoDTO> userInfos;

        /// <summary>
        /// 一页集合
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<UserInfoDTO> pageInfos;

        /// <summary>
        ///单个元素 
        /// </summary>
        [ObservableProperty]
        UserInfoDTO userInfo;

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

        

    }
}
