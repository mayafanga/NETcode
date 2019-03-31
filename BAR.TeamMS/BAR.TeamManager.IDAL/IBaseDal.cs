using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAR.TeamManager.Model;
using System.Linq.Expressions;

namespace BAR.TeamManager.IDAL
{
    public interface IBaseDal<T>where T:class,new()
    {
        //加载
        IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda);
        //分页
        IQueryable<T> LoadPageEntities<S>(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderbyLambda, bool isAsc);
        //删除
        bool DeleteEntity(T entity);
        //修改
        bool EditEntity(T entity);
        //添加
        T AddEntity(T entity);

    }
}
