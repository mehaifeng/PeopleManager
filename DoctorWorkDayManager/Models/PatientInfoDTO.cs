using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWorkDayManager.Models
{
    public partial class PatientInfoDTO:ObservableObject
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int no { get; set; }
        /// <summary>
        /// 患者编号
        /// </summary>
        [ObservableProperty]
        public string id;
        /// <summary>
        /// 患者姓名
        /// </summary>
        [ObservableProperty]
        public string name;
        /// <summary>
        /// 患者性别
        /// </summary>
        [ObservableProperty]
        public string gender;
        /// <summary>
        /// 患者年龄
        /// </summary>
        [ObservableProperty]
        public string age;
        /// <summary>
        /// 患者过敏史
        /// </summary>
        [ObservableProperty]
        public string allergies;
        /// <summary>
        /// 诊疗卡号
        /// </summary>
        [ObservableProperty]
        public string medicalcard;
        /// <summary>
        /// 患者籍贯
        /// </summary>
        [ObservableProperty]
        public string originplace;
        /// <summary>
        /// 患者身份证
        /// </summary>
        [ObservableProperty]
        public string personid;
        /// <summary>
        /// 患者联系方式
        /// </summary>
        [ObservableProperty]
        public string contact;
        /// <summary>
        /// 患者地址
        /// </summary>
        [ObservableProperty]
        public string address;
        /// <summary>
        /// 患者工作单位
        /// </summary>
        [ObservableProperty]
        public string workplace;
        /// <summary>
        /// 患者门诊号
        /// </summary>
        [ObservableProperty]
        public string outpatientnum;
    }
}
