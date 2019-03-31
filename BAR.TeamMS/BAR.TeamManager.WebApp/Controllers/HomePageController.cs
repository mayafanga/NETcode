using BAR.TeamManager.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Newtonsoft.Json;
using BAR.TeamManager.Model;
using BAR.TeamManager.Common;
using Newtonsoft.Json.Converters;

namespace BAR.TeamManager.WebApp.Controllers
{
    public class HomePageController : Controller
    {
        StringBuilder sbHeaderContent = new StringBuilder();
        StringBuilder sbSliderContent = new StringBuilder();
        StringBuilder sbFooterContent = new StringBuilder();
        StringBuilder sbTeamContent = new StringBuilder();
        StringBuilder sbHonorContent = new StringBuilder();
        StringBuilder sbActivityContent = new StringBuilder();
        // IndexPageCommon indexContent = new IndexPageCommon();//使用Common
        string sbAllContent = null;
        string navContent = null;
        string sliderContent = null;
        string footerContent = null;
        string teamContents = null;
        string honorContents = null;
        string activiesContents = null;
        string loginUserInfoId = "null";//登录用户的ID
        string loginIdentity = "null";//登录用户的标识
        string status = null;//状态码
        string errorMsg = null;//错误信息
        //IndexContent indexContent = null;
        IwebmasterService webmasterService { get; set; }//站长属性
        IteamService teamService { get; set; }//团队属性
        IhonorService honorService { get; set; }//荣耀属性
        IactivityService activityService { get; set; }
        IregisterloginService registerloginService { get; set; }
        // GET: HomePage
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HomePageContent()
        {
            HttpCookie loginUserCookie = Request.Cookies["loginUserInfo"];//登录用户的Cookie
            int loginUserIndentity = 0;//身份标识
            if (loginUserCookie != null)//当传过来的cookie的值不为null
            {
                string loginAccount = loginUserCookie.Values["loginUserAccount"];//用户的账号
                if (LoginController.cookieList.Contains(loginAccount))//判断账号是否存在
                {
                    loginUserInfoId = loginUserCookie.Values["loginUserId"];//用户的id
                    loginIdentity = loginUserCookie.Values["loginUserIdentity"];//拿到身份标识
                    int loginUserId = Convert.ToInt32(loginUserInfoId);
                    try
                    {
                        if (loginIdentity != null)
                        {
                            loginUserIndentity = Convert.ToInt32(loginIdentity);
                            //得到首页的头部尾部和轮播图的所有数据
                            try
                            {
                                var homePageContent = webmasterService.LoadEntities(webmaster => true).ToList();
                                navContent = IndexPageCommon.GetIndexNav(homePageContent, loginUserIndentity, loginUserCookie);//得到导航
                                sliderContent = IndexPageCommon.GetIndexSlider(homePageContent, "IndexSlider");
                                footerContent = IndexPageCommon.GetFooter(homePageContent);

                            }
                            catch (Exception)
                            {
                                status = "fail";
                                errorMsg = "网络连接不稳定，获取数据错误";
                            }
                            //头部尾部和轮播图的所有数据结束
                        }
                        else
                        {
                            //如果身份标识不存在，则按普通学生处理
                            //得到首页的头部尾部和轮播图的所有数据
                            try
                            {
                                var homePageContent = webmasterService.LoadEntities(webmaster => true).ToList();
                                navContent = IndexPageCommon.GetIndexNav(homePageContent, loginUserIndentity, loginUserCookie);//导航栏
                                sliderContent = IndexPageCommon.GetIndexSlider(homePageContent, "IndexSlider");
                                footerContent = IndexPageCommon.GetFooter(homePageContent);

                            }
                            catch (Exception)
                            {
                                status = "fail";
                                errorMsg = "网络连接不稳定，获取数据错误";
                            }
                            //头部尾部和轮播图的所有数据结束
                        }
                        //}
                    }
                    catch (Exception)
                    {
                        status = "fail";
                        errorMsg = "网络连接不稳定，获取数据错误";
                    }
                }
                else
                {
                    try
                    {
                        var homePageContent = webmasterService.LoadEntities(webmaster => true).ToList();
                        navContent = IndexPageCommon.GetIndexNav(homePageContent, loginUserIndentity, loginUserCookie);//得到导航
                        sliderContent = IndexPageCommon.GetIndexSlider(homePageContent, "IndexSlider");//首页的轮播图
                        footerContent = IndexPageCommon.GetFooter(homePageContent);//底部
                    }
                    catch (Exception)
                    {
                        status = "fail";
                        errorMsg = "网络连接不稳定，获取数据错误";
                    }
                }

            }
            else
            {
                try
                {
                    var homePageContent = webmasterService.LoadEntities(webmaster => true).ToList();
                    navContent = IndexPageCommon.GetIndexNav(homePageContent, loginUserIndentity, loginUserCookie);//得到导航
                    sliderContent = IndexPageCommon.GetIndexSlider(homePageContent, "IndexSlider");//首页的轮播图
                    footerContent = IndexPageCommon.GetFooter(homePageContent);//底部
                }
                catch (Exception)
                {
                    status = "fail";
                    errorMsg = "网络连接不稳定，获取数据错误";
                }
            }
            //得到团队信息数据
            try
            {
                var teamContent = teamService.LoadEntities(teamInfo => true).ToList();
                sbTeamContent.Append("\"" + "team" + "\"" + ":" + "[");
                foreach (var team in teamContent)
                {
                    if (team != null)
                    {
                        string teamTime = team.dTeamSetupTime.ToString();
                        if (team.dTeamSetupTime.ToString().Contains(' '))
                        {
                            string[] teamTimes = team.dTeamSetupTime.ToString().Split(' ');
                            teamTime = teamTimes[0];
                        }
                        sbTeamContent.Append("{" + "\"" + "id" + "\"" + ":" + "\"" + team.ID + "\"" + "," + "\"" + "teamName" + "\"" + ":" + "\"" + team.vcTeamName + "\"" + "," + "\"" + "TeamSetupTime" + "\"" + ":" + "\"" + teamTime + "\"" + "," + "\"" + "TeamLogoAddress" + "\"" + ":" + "\"" + team.vcTeamLogoAddress + "\"" + "},");
                    }
                }
                sbTeamContent.Append("]");
                teamContents = sbTeamContent.ToString().Remove(sbTeamContent.ToString().LastIndexOf(','), 1);//移除最后一个逗号
            }
            catch (Exception)
            {
                status = "fail";
                errorMsg = "网络连接不稳定，获取数据错误";
            }
            //首页的团队信息完毕

            //荣耀信息
            try
            {
                var honorContent = honorService.LoadEntities(honorInfo => true).ToList();
                sbHonorContent.Append("\"" + "honor" + "\"" + ":" + "[");
                foreach (var honor in honorContent)
                {
                    if (honor != null)
                    {
                        string honorTime = honor.dSubmitTime.ToString();
                        if (honor.dSubmitTime.ToString().Contains(' '))
                        {
                            string[] honorTimes = honorTime.Split(' ');
                            honorTime = honorTimes[0];
                        }
                        sbHonorContent.Append("{" + "\"" + "id" + "\"" + ":" + "\"" + honor.ID + "\"" + "," + "\"" + "honorName" + "\"" + ":" + "\"" + honor.vcHonorName + "\"" + "," + "\"" + "HonorSubmitTime" + "\"" + ":" + "\"" + honorTime + "\"" + "," + "\"" + "HonorLogoAddress" + "\"" + ":" + "\"" + honor.vcHonorSubmitAddress + "\"" + "},");
                    }
                }
                sbHonorContent.Append("]");
                honorContents = sbHonorContent.ToString().Remove(sbHonorContent.ToString().LastIndexOf(','), 1);//移除最后一个逗号
            }
            catch (Exception)
            {
                status = "fail";
                errorMsg = "网络连接不稳定，获取数据错误";
            }
            //荣耀信息完毕

            //活动信息
            try
            {
                var activityContent = activityService.LoadEntities(activityInfo => true).ToList();
                sbActivityContent.Append("\"" + "activity" + "\"" + ":" + "[");
                foreach (var activity in activityContent)
                {
                    if (activity != null)
                    {
                        string activityTime = activity.dHostTime.ToString();
                        if (activity.dHostTime.ToString().Contains(' '))
                        {
                            string[] activityTimes = activityTime.Split(' ');
                            activityTime = activityTimes[0];
                        }
                        sbActivityContent.Append("{" + "\"" + "id" + "\"" + ":" + "\"" + activity.ID + "\"" + "," + "\"" + "activityName" + "\"" + ":" + "\"" + activity.vcActivityName + "\"" + "," + "\"" + "ActivityHostTime" + "\"" + ":" + "\"" + activityTime + "\"" + "," + "\"" + "ActivityLogoAddress" + "\"" + ":" + "\"" + activity.vcThumbnailAddress + "\"" + "},");
                    }
                }
                sbActivityContent.Append("]");
                activiesContents = sbActivityContent.ToString().Remove(sbActivityContent.ToString().LastIndexOf(','), 1);//移除最后一个逗号
            }
            catch (Exception)
            {
                status = "fail";
                errorMsg = "网络连接不稳定，获取数据错误";
            }
            //活动信息结束

            if (status == "fail")
            {
                string errorData = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + errorMsg + "\"" + "}";
                return Content(errorData);
            }
            else
            {
                //sbAllContent = "{" + "\"" + "Index" + "\"" + ":" + "{" + "\"" + "loginId" + "\"" + ":" + "\"" + loginUserInfoId + "\"" + "," + "\"" + "nav" + "\"" + ":" + navContent + "," + "\"" + "IndexSlider" + "\"" + ":" + sliderContent + "," + teamContents + "," + honorContents + "," + activiesContents + "," + "\"" + "footer" + "\"" + ":" + footerContent + "}" + "}";
                sbAllContent = "{" + "\"" + "Index" + "\"" + ":" + "{" + "\"" + "loginId" + "\"" + ":" + "\"" + loginUserInfoId + "\"" + "," + "\"" + "loginIdentity" + "\"" + ":" + "\"" + loginIdentity + "\"" + "," + "\"" + "nav" + "\"" + ":" + navContent + "," + "\"" + "IndexSlider" + "\"" + ":" + sliderContent + "," + teamContents + "," + honorContents + "," + activiesContents + "," + "\"" + "footer" + "\"" + ":" + footerContent + "}" + "}";
                //总的返回数据
                return Content(sbAllContent);
            }
        }
        /// <summary>
        /// 导航栏
        /// </summary>
        /// <returns></returns>
        public ActionResult GetIndexNavPage()
        {
            HttpCookie loginUserCookie = Request.Cookies["loginUserInfo"];//登录用户的Cookie
            int loginUserIndentity = 0;//身份标识
            if (loginUserCookie != null)
            {
                loginUserInfoId = loginUserCookie.Values["loginUserId"];//用户的id
                loginIdentity = loginUserCookie.Values["loginUserIdentity"];//拿到身份标识
                if (loginIdentity != null)
                {
                    loginUserIndentity = Convert.ToInt32(loginIdentity);
                    try
                    {
                        var homePageContent = webmasterService.LoadEntities(webmaster => true).ToList();
                        navContent = IndexPageCommon.GetIndexNav(homePageContent, loginUserIndentity, loginUserCookie);//得到导航
                    }
                    catch (Exception)
                    {
                        status = "fail";
                        errorMsg = "网络连接不稳定，获取数据错误";
                    }

                }
                else
                {
                    status = "fail";
                    errorMsg = "查询不到对应的身份";
                }
                

            }
            else
            {
                try
                {
                    var homePageContent = webmasterService.LoadEntities(webmaster => true).ToList();
                    navContent = IndexPageCommon.GetIndexNav(homePageContent, loginUserIndentity, loginUserCookie);//得到导航
                }
                catch (Exception)
                {
                    status = "fail";
                    errorMsg = "网络连接不稳定，获取数据错误";
                }
            }
            if (status == "fail")
            {
                string errorData = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + errorMsg + "\"" + "}";
                return Content(errorData);
            }
            else
            {
                string navs = "{"+"\"" + "loginId" + "\"" + ":" + "\"" + loginUserInfoId + "\"" + ","+"\"" + "nav" + "\"" + ":" + navContent+"}";
                return Content(navs);
            }
        }
        /// <summary>
        /// 得到登录者的身份标识
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLoginUserIdentity()
        {
            string mess = null;
            HttpCookie loginUserCookie = Request.Cookies["loginUserInfo"];//登录用户的Cookie
            if (loginUserCookie!=null)
            {
                loginIdentity = loginUserCookie.Values["loginUserIdentity"];//拿到身份标识
                status = "ok";
            }
            else
            {
                status = "fail";
            }
            mess= "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "identity" + "\"" + ":" + "\"" + loginIdentity + "\"" + "}";
            return Content(mess);
        }
    }
}