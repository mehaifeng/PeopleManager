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
using System.IO;
using OfficeOpenXml;
using System.Windows.Media;

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
        private void UpdateAll(DataGrid dataGrid)
        {
            try
            {
                List<UserInfoDTO> newInfos = UserInfos.Select(u => new UserInfoDTO 
                {
                    id= u.Id,
                    name= u.Name,
                    gender = u.Gender,
                    personid = u.Personid,
                    contact = u.Contact,
                    academic = u.Academic,
                    department = u.Department,
                    room = u.Room,
                    joblevel = u.Joblevel,
                    workDate = u.WorkDate,
                    userType = u.UserType
                }).ToList();
                var items = newInfos;
                db.Updateable(items).ExecuteCommand();
                MessageBox.Show("保存完毕");
            }
            catch
            {
                MessageBox.Show("发生错误，无法保存");
                return;
            }
        }
        /// <summary>
        /// 导出到Excel
        /// </summary>
        /// <param name="dataGrid"></param>
        [RelayCommand]
        private void ExportToExcel(DataGrid dataGrid)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                // 创建工作表
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

                // 将DataGrid中的列标题写入第一行
                for (int i = 0; i < dataGrid.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = dataGrid.Columns[i].Header;
                }
                // 将DataGrid中的行数据写入工作表中
                for (int i = 0; i < dataGrid.Items.Count; i++)
                {
                    for (int j = 0; j < dataGrid.Columns.Count; j++)
                    {
                        var cellValue = UserInfos[i].GetType().GetProperty(dataGrid.Columns[j].SortMemberPath).GetValue(UserInfos[i], null);
                        worksheet.Cells[i + 2, j + 1].Value = cellValue;
                    }
                }
                // 将Excel文件保存到磁盘
                var saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                if (saveFileDialog.ShowDialog() == true)
                {
                    FileInfo file = new FileInfo(saveFileDialog.FileName);
                    excelPackage.SaveAs(file);
                }
            }
        }
        /// <summary>
        /// 打印表格
        /// </summary>
        /// <param name="dataGrid"></param>
        [RelayCommand]
        private void Print(DataGrid dataGrid)
        {
            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                // 获取DataGrid的可视化对象
                var visual = new DrawingVisual();
                var drawingContext = visual.RenderOpen();
                var size = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);
                var scale = Math.Min(size.Width / dataGrid.ActualWidth, size.Height / dataGrid.ActualHeight);
                drawingContext.PushTransform(new ScaleTransform(scale, scale));
                drawingContext.DrawRectangle(new VisualBrush(dataGrid), null, new Rect(new Point(), new Size(dataGrid.ActualWidth, dataGrid.ActualHeight)));
                drawingContext.Close();

                // 打印可视化对象
                printDialog.PrintVisual(visual, "DataGrid Printing.");
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
