using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;
using BAR.TeamManager.IDAL;
using BAR.TeamManager.Model;

namespace BAR.TeamManager.DALFactory
{
    public class DBSessionFactory
    {
        public static IDBSession CreateDBSession()
        {
            IDBSession DbSession = (IDBSession)CallContext.GetData("dbSession");
            if (DbSession == null)
            {
                DbSession = new DALFactory.DBSession();
                CallContext.SetData("dbSession", DbSession);
            }
            return DbSession;
        }
    }
}
