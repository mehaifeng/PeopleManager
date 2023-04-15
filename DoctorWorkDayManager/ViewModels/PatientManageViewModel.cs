using CommunityToolkit.Mvvm.ComponentModel;
using DoctorWorkDayManager.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using SqlSugar;

namespace DoctorWorkDayManager.ViewModels
{
    public partial class PatientManageViewModel : ObservableObject
    {
        public PatientManageViewModel()
        {
            PatientInfos = new ObservableCollection<PatientInfoDTO>();
            PatientInfo = new PatientInfoDTO();
            query();
        }
        /// <summary>
        /// 上次搜索的内容
        /// </summary>
        private static string LastSearch;

        /// <summary>
        /// 患者信息列表
        /// </summary>
        [ObservableProperty]
        ObservableCollection<PatientInfoDTO> patientInfos;

        /// <summary>
        /// 是否显示顶部患者信息
        /// </summary>
        [ObservableProperty]
        Visibility isShowPatientHeader;

        /// <summary>
        /// 选中的患者
        /// </summary>
        [ObservableProperty]
        PatientInfoDTO selectPatient;

        /// <summary>
        /// 单个患者信息
        /// </summary>
        [ObservableProperty]
        PatientInfoDTO patientInfo;

        /// <summary>
        /// 姓名
        /// </summary>
        [ObservableProperty]
        string name;
        /// <summary>
        /// 性别
        /// </summary>
        [ObservableProperty]
        string gender;
        /// <summary>
        /// 年龄
        /// </summary>
        [ObservableProperty]
        string age;
        /// <summary>
        /// 门诊号
        /// </summary>
        [ObservableProperty]
        string outPatientNum;
        /// <summary>
        /// 诊疗卡号
        /// </summary>
        [ObservableProperty]
        string medicalCard;
        /// <summary>
        /// 联系电话
        /// </summary>
        [ObservableProperty]
        string contact;

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string connstr = ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString;

        void query()
        {
            //SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            //{
            //    ConnectionString = ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString,
            //    DbType = DbType.SqlServer,
            //    IsAutoCloseConnection = true
            //});
            //var list = db.Queryable<PatientInfoDTO>().ToList();
        }

        [RelayCommand]
        void SearchPerson(string inputText)
        {
            if (string.IsNullOrEmpty(inputText))
            {
                IsShowPatientHeader = Visibility.Collapsed;
                return;
            }
            //判断是否和上次搜寻的一样，一样则直接退出
            if(LastSearch == inputText)
            {
                return;
            }
            var sqlText = $"select * from patientinfo where medicalcard='{inputText}' or outpatientnum='{inputText}' or name='{inputText}'";


            if (SelectPatient != null)
            {
                Name = $"姓名：{SelectPatient.name}";
                Gender = $"性别：{SelectPatient.gender}";
                Age = $"年龄：{SelectPatient.age}";
                OutPatientNum = $"住院号：{SelectPatient.outpatientnum}";
                MedicalCard = $"诊疗卡号：{SelectPatient.medicalcard}";
                Contact = $"联系电话：{SelectPatient.contact}";
                IsShowPatientHeader = Visibility.Visible;
            }
            else if (PatientInfo == null)
            {
                IsShowPatientHeader = Visibility.Collapsed;
            }
            LastSearch = inputText;
        }
    }
}
