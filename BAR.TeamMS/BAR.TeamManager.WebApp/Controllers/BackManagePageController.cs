using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAR.TeamManager.BLL;
using BAR.TeamManager.IBLL;
using BAR.TeamManager.Common;
using BAR.TeamManager.Model;
using System.Text;

namespace BAR.TeamManager.WebApp.Controllers
{
    public class BackManagePageController : Controller
    {
        /// <summary>
        /// 后台管理页面界面 
        /// </summary>
        /// <returns></returns>

        IwebmasterService WebmasterService { get; set; }
        // GET: BackManagePage
        public ActionResult Index()
        {
            return View();
        }
        #region  先拿到表中的数据在后台页面上展示出来（get请求）
        /// <summary>
        /// 拿到webmaster表中的头部   包括头部的导航、按钮、LoGO图片
        /// </summary>
        /// <returns></returns>
        public ActionResult ManagePageHead()
        {
            string jsonTxt = null;
            //拿到管理员登陆Cookie  传参需要：webmaster、身份标识、用户登录Cookie
            HttpCookie loginUserCookie = Request.Cookies["loginUserInfo"];//登录用户的Cookie
            if (loginUserCookie != null)
            {
                string loginIdentity = loginUserCookie.Values["loginUserIdentity"];//拿到身份标识
                int loginUserIdentity = Convert.ToInt32(loginIdentity);
                if (loginUserIdentity == 0)
                {
                    try
                    {
                        var webmaster = WebmasterService.LoadEntities(w => true).ToList();
                        jsonTxt = IndexPageCommon.GetIndexNav(webmaster, loginUserIdentity, loginUserCookie);
                    }
                    catch (Exception)
                    {
                        jsonTxt = "Network instability";
                    }
                }
                else
                {
                    jsonTxt = "no";
                }
            }
            return Content(jsonTxt);
        }
        /// <summary>
        /// 获取特定轮播图的数据  根据sliderName
        /// </summary>
        /// <returns></returns>
        public ActionResult ManagePageSlider()
        {
            string jsonTxt = null;
            try
            {
                string slider = Request["sliderName"];
                if (slider != null)
                {
                    var webmaster = WebmasterService.LoadEntities(w => true).ToList();
                    jsonTxt = IndexPageCommon.GetSlider(webmaster, slider);
                }
                else
                {
                    jsonTxt = "no";
                }
            }
            catch (Exception ex)
            {
                jsonTxt = "Network instability";
            }
            return Content(jsonTxt);
        }

        /// <summary>
        /// 获取底部的数据  先不写（先不调用）
        /// </summary>
        /// <returns></returns>
        public ActionResult ManagePageFooter()
        {
            string jsonTxt = null;
            try
            {
                var webmaster = WebmasterService.LoadEntities(w => true).ToList();
                jsonTxt = IndexPageCommon.GetFooter(webmaster);
            }
            catch (Exception ex)
            {
                jsonTxt = "网络出现异常，请稍后重试。";
            }
            return Content(jsonTxt);
        }
        #endregion

        # region   当点击某个页面的某个按钮的时候再做单独的数据操作
        /// <summary>
        /// 对数据库中的logo地址进行修改
        /// </summary>
        /// <returns></returns>
        public ActionResult ModifyLogo()
        {
            string jsonTxt = null;
            string logo = Request["logoAddress"];
            string logoAddress = EditPhotos.Base64StringToImage(logo, "Logo");
            try
            {
                //对数据库中的logo地址进行修改
                if (logo != null && logoAddress != null)
                {
                    //根据vcName对数据库中的vcContent进行修改
                    webmaster web = WebmasterService.GetWebMasterByName("LOGO");
                    web.vcContent = logoAddress;
                    if (WebmasterService.EditEntity(web))
                    {
                        jsonTxt = "ok";
                    }
                    else
                    {
                        jsonTxt = "no";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonTxt = "Network instability";
            }
            return Content(jsonTxt);
        }
        /// <summary>
        /// 对数据库中的导航或按钮进行修改
        /// </summary>
        /// <returns></returns>
        public ActionResult ModifyNavBtn()
        {
            string jsonTxt = null;
            try
            {
                string nav = Request["nav"];//数据
                //向数据库中添加或删除Nav或按钮
                //不管是添加还是删除都是对数据库中的数据进行修改
                //对数据库中的导航进行修改
                webmaster web = WebmasterService.GetWebMasterByName("nav");
                web.vcContent = nav;
                if (WebmasterService.EditEntity(web))
                {
                    jsonTxt = "ok";
                }
                else
                {
                    jsonTxt = "no";
                }
            }
            catch (Exception ex)
            {
                jsonTxt = "Network instability";
            }
            return Content(jsonTxt);
        }
        /// <summary>
        /// 根据轮播名，编辑轮播
        /// </summary>
        /// <returns></returns>
        public ActionResult EditSlider()
        {
            string jsonTxt = null;
            string sliderName = Request["sliderName"];//用来判断是添加到那个slider里面
            string slider = Request["slider"];//添加的slider内容
            try
            {
                if (sliderName != null && slider != null)
                {
                    //分割base64编码
                    string sliderAddress = Common.EditSlider.EncapCode(slider, sliderName);
                    //先从数据库中拿出原来的轮播内容，然后把这个图片添加到字符串内
                    webmaster web = WebmasterService.GetWebMasterByName(sliderName);
                    web.vcContent = sliderAddress;
                    if (WebmasterService.EditEntity(web))
                    {
                        jsonTxt = "ok";
                    }
                    else
                    {
                        jsonTxt = "no";
                    }
                }
                else if (sliderName != null && slider == null)
                {
                    webmaster web = WebmasterService.GetWebMasterByName(sliderName);
                    web.vcContent = "null";
                    if (WebmasterService.EditEntity(web))
                    {
                        jsonTxt = "ok";
                    }
                    else
                    {
                        jsonTxt = "no";
                    }
                }
                else
                {
                    jsonTxt = "no";
                }
            }
            catch (Exception ex)
            {
                jsonTxt = "Network instability";
            }

            return Content(jsonTxt);
        }
        /// <summary>
        /// 对底部进行操作  （先不做）
        /// </summary>
        /// <returns></returns>

        #endregion



    }
}