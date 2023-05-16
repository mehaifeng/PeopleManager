using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlSugar;
using System.Threading.Tasks;
using DoctorWorkDayManager.ViewModels;
using DoctorWorkDayManager.Models;

namespace DoctorWorkDayManager.Server.Dbconn
{
    public class DbSugarCoreHelper
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddAsync(SqlSugarClient db, UserInfoDTO info)
        {
            return db.Insertable(info).ExecuteCommand() > 0;
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="Llist"></param>
        /// <returns></returns>
        public static async Task<bool> DeleteAsync(SqlSugarClient db,List<UserInfoDTO> Llist)
        {
            var result = true;
            db.Ado.BeginTran();
            try
            {
                foreach (var item in Llist)
                {
                    if (await db.Deleteable(item).ExecuteCommandAsync() >= 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception)
            {
                result = false;
            }

            if (result)
            {
                db.Ado.CommitTran();
            }
            else
            {
                db.Ado.RollbackTran();
            }
            return result;
        }
        /// <summary>
        /// 查询表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<UserInfoDTO> ListAsync(SqlSugarClient db)
        {
            var result = db.Queryable<UserInfoDTO>().ToList();
            return result;
        }

        /// <summary>
        /// 查询单条信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public UserInfoDTO SingleAsync(SqlSugarClient db, UserInfoDTO model)
        {
            return db.Queryable<UserInfoDTO>().Single(it => it.id == model.Id);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateAsync(SqlSugarClient db, UserInfoDTO model)
        {
            return db.Updateable(model).UpdateColumns(it => new { it.Contact, it.WorkDate, it.UserType }).Where(it => it.id == model.Id).ExecuteCommand() > 0;
        }
    }
}
