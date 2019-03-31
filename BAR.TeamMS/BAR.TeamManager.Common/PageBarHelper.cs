using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAR.TeamManager.Common
{
    /// <summary>
    /// 给后台的展示全部页面设置分页导航
    /// </summary>
    public class PageBarHelper
    {
        /// <summary>
        /// 生成分页导航条
        /// </summary>
        /// <param name="pageSize">每页容量</param>
        /// <param name="currentPage">当前页数</param>
        /// <param name="totalPages">总页数</param>
        /// <returns>导航条的HTML字符串</returns>
        public static string CreatePageNavigator(int pageSize,int currentPage,int totalPages)
        {
            //1.判断pageSize，如果<=0，就让每页显示5条记录(容错处理)
            pageSize = pageSize <= 0 ? 5 : pageSize;
            currentPage = currentPage <= 0 ? 1 : currentPage;
            StringBuilder sbOutPut = new StringBuilder();
            if (totalPages > 1)
            {
                //生成“首页”
                if (currentPage != 1)
                {
                    sbOutPut.AppendFormat("<a href='?pageSize={0}&currentPage=1' class='pageLink first'>首页</a>",pageSize);
                }
                //3.生成“上一页”选项
                if (currentPage > 1)  //如果当前页不是第一页
                {
                    sbOutPut.AppendFormat("<a href='?pageSize={0}&currentPage={1}' class='pageLink prePage'>《上一页</a>", pageSize, currentPage - 1);
                }
                else  //如果当前页就是第1页，那么就让“上一页”不能点击
                {
                    sbOutPut.AppendFormat("<span class='prePage prePage'>《上一页</span>");
                }
                //4.显示当前页码的前3个和后三个页码
                int range = 3;
                for(int i = 0; i <= 6; i++)
                {
                    int pageNumber = currentPage + i - range;//得到本次循环的页码值
                    if (pageNumber >= 1 && pageNumber <= totalPages)//判断这个页码值是不是在正常范围之内
                    {
                        if (pageNumber == currentPage)
                        {
                            sbOutPut.AppendFormat("<span class='pageLink currentPage'>{0}</span>", currentPage);
                        }
                        else   //处理一般页选项
                        {
                            sbOutPut.AppendFormat("<a href='?pageSize={0}&currentPage={1}' class='pageLink'>{2}</a>", pageSize,pageNumber,pageNumber);
                        }
                    }
                }
                //5.生成下一页选项
                if (currentPage < totalPages)
                {
                    sbOutPut.AppendFormat("<a href='?pageSize={0}&currentPage={1}' class='pageLink nextPage'>下一页》</a>", pageSize, currentPage + 1);
                }
                else //如果没有下一页
                {
                    sbOutPut.AppendFormat("<span class='prePage'>下一页》</span>");
                }
                //6.生成“末页”选项
                if (currentPage != totalPages)
                {
                    sbOutPut.AppendFormat("<a href='?pageSize={0}&currentPage={1}' class='pageLink endPage'>末页</a>", pageSize, totalPages);
                }
            }
            //7.生成最后的提示信息，显示共多少页
            sbOutPut.AppendFormat("<span class='pageInfo'>第[{0}]页/共[{1}]页</span>", currentPage, totalPages);
            return sbOutPut.ToString();
        }
    }
}
