using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SqlClient;

namespace BAR.TeamManager.IDAL
{
    /// <summary>
    /// 业务层调用数据会话层的接口。
    /// </summary>
    public partial interface IDBSession
    {
        DbContext Db { get; }
        bool SaveChanges();
        int ExecuteSql(string sql, params SqlParameter[] pars);
    }
}
