using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAR.TeamManager.DAL;
using BAR.TeamManager.IDAL;
using BAR.TeamManager.Model;
using System.Data.Entity;
using System.Data.SqlClient;

namespace BAR.TeamManager.DALFactory
{
    /// <summary>
    /// 数据会话层
    /// </summary>
    public partial class DBSession:IDBSession
    {
        public DbContext Db
        {
            get
            {
                return DBContextFactory.CreateDbContext();
            }
        }



        /// <summary>
        /// 工作单元模式
        /// 一个业务中经常涉及到对多张表的操作，我们希望连接一次数据库，完成对数据库的操作，提高性能。
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges()
        {
            return Db.SaveChanges() > 0;
        }
        /// <summary>
        /// 对SQL语句进行操作  对数据的insert、update、delete
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public int ExecuteSql(string sql,params SqlParameter[] pars)
        {
            return Db.Database.ExecuteSqlCommand(sql, pars);
        }

    }
}
