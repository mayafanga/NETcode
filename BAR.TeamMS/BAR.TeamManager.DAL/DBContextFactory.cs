using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;
using System.Data.Entity;
using BAR.TeamManager.Model;

namespace BAR.TeamManager.DAL
{
    /// <summary>
    /// 负责创建EF数据操作上下文实例，必须保证线程内唯一
    /// </summary>
    public class DBContextFactory
    {
        /// <summary>
        /// 线程槽
        /// </summary>
        /// <returns></returns>
        public static DbContext CreateDbContext()
        {
            DbContext dbContext = (DbContext)CallContext.GetData("DbContext");
            if (dbContext == null)
            {
                dbContext = new barteammanagedbEntities1();
                CallContext.SetData("DbContext", dbContext);
            }
            return dbContext;
        }
    }
}
