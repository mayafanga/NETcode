using BAR.TeamManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;
namespace BAR.TeamManager.Common
{
    /// <summary>
    /// 获取首页的头部，轮播图，以及尾部数据
    /// </summary>
    public class IndexPageCommon
    {
        public static string indexNavContent = null;
        /// <summary>
        /// 获取首页的方法
        /// </summary>
        /// <param name="content">list<webmaster>类型数据</param>
        /// <returns></returns>
        public static string IndexContent(List<webmaster> content)
        {
            StringBuilder sbIndexContent = new StringBuilder();
            sbIndexContent.Append("{");
            foreach (var con in content)
            {
                if (con != null)
                {
                    if (con.vcName == "title" || con.vcName == "LOGO")
                    {
                        sbIndexContent.Append("\"" + con.vcName + "\"" + ":" + "\"" + con.vcContent + "\"" + ",");
                    }
                    else
                    {
                        sbIndexContent.Append("\"" + con.vcName + "\"" + ":" + con.vcContent + ",");
                    }
                }
            }
            sbIndexContent.Remove(sbIndexContent.Length - 1, 1);
            sbIndexContent.Append("}");
            return sbIndexContent.ToString();
        }
        /// <summary>
        /// 活得首页的头部(导航栏）
        /// </summary>
        /// <param name="indexContent">头部轮播图，尾部</param>
        /// <param name="identity">身份标识</param>
        /// <param name="userLoginState">用户登录状态</param>
        /// <returns></returns>
        public static string GetIndexNav(List<webmaster> indexContent, int identity,HttpCookie userLoginState)
        {
            //string navJsonResult = null;
            StringBuilder sbIndex = new StringBuilder();
            IndexNav indexNav = new IndexNav();
            if (indexContent != null)
            {
                foreach (var item in indexContent)
                {
                    if (item != null)
                    {
                        if (item.vcName == "LOGO")
                        {
                            indexNav.LOGO = item.vcContent;
                        }
                        if (item.vcName == "title")
                        {
                            
                        }

                        if (item.vcName=="nav")
                        {
                            indexNav = GetNavByRole(indexNav, item.vcContent, identity, userLoginState);//调用获取导航栏的方法 
                            //JObject navJson = (JObject)JsonConvert.DeserializeObject(indexNavContent);//将数据进行序列化
                            indexNavContent = JsonConvert.SerializeObject(indexNav, Newtonsoft.Json.Formatting.Indented);
                            sbIndex.Append(indexNavContent);
                        }

                        //sbIndex.Append(navJson.ToString());
                        
                    }
                }
            }
            return sbIndex.ToString();//返回导航栏的内容
        }
        /// <summary>
        /// 轮播图
        /// </summary>
        /// <param name="indexContent">头部,轮播图，尾部</param>
        /// <param name="IndexName">轮播名称(如首页的轮播图)</param>
        /// <returns></returns>
        public static string GetIndexSlider(List<webmaster> indexContent, string indexName)
        {
            StringBuilder sbIndexSlider = new StringBuilder();
            //sbIndexSlider.Append("{");
            if (indexContent != null)
            {
                foreach (var item in indexContent)
                {
                    if (item.vcName.ToLower() == indexName.ToLower())
                    {
                        sbIndexSlider.Append(item.vcContent);
                    }
                }
            }
            //sbIndexSlider.Append("}");
            return sbIndexSlider.ToString();
        }
        /// <summary>
        /// 获取尾部的信息
        /// </summary>
        /// <param name="indexContent">头部,轮播图，尾部</param>
        /// <returns></returns>
        public static string GetFooter(List<webmaster> indexContent)
        {
            FooterLogo footerLogo = new FooterLogo();
            ProductService proService = new ProductService();
            AboutUs aboutUs = new AboutUs();
            CommonProblems commonProblems = new CommonProblems();
            Contact contact = new Contact();
            RemarkFooter remarkFooters = new RemarkFooter();
            List<FooterLogo> footersList = new List<FooterLogo>();
            List<ProductService> productsList = new List<ProductService>();
            List<AboutUs> aboutUsList = new List<AboutUs>();
            List<CommonProblems> commonProblemsList = new List<CommonProblems>();
            List<Contact> contactsList = new List<Contact>();
            List<RemarkFooter> remarkFootList = new List<RemarkFooter>();
            IndexAllFooter indexAllFooter = new IndexAllFooter();
            string footerJsonTxt = null;
            if (indexContent != null)
            {
                foreach (var item in indexContent)
                {
                    if (item != null)
                    {
                        if (item.vcName == "footerLogo")
                        {
                            footerLogo.Logo = item.vcContent;
                            footersList.Add(footerLogo);
                            indexAllFooter.footerLogo = footersList;
                        }
                        if (item.vcName == "nav")
                        {
                            JObject navs = (JObject)JsonConvert.DeserializeObject(item.vcContent);//将数据进行序列化
                            //检查
                            //产品服务
                            proService.productService = Convert.ToString(navs["productService"]) != null ? Convert.ToString(navs["productService"]) : "";
                            proService.teamApply = Convert.ToString(navs["teamApply"]) != null ? Convert.ToString(navs["teamApply"]) : "";
                            proService.honorApply = Convert.ToString(navs["honorApply"]) != null ? Convert.ToString(navs["honorApply"]) : "";
                            proService.activeApply = Convert.ToString(navs["activeApply"]) != null ? Convert.ToString(navs["activeApply"]) : "";
                            proService.perfectinformation = Convert.ToString(navs["perfectinformation"]) != null ? Convert.ToString(navs["perfectinformation"]) : "";
                            productsList.Add(proService);
                            indexAllFooter.proService = productsList;
                            //产品服务结束
                            //关于我们
                            aboutUs.teamIntroduce = Convert.ToString(navs["teamIntroduce"]) != null ? Convert.ToString(navs["teamIntroduce"]) : "";
                            aboutUs.joinUs = Convert.ToString(navs["joinUs"]) != null ? Convert.ToString(navs["joinUs"]) : "";
                            aboutUs.problemBack = Convert.ToString(navs["problemBack"]) != null ? Convert.ToString(navs["problemBack"]) : "";
                            aboutUsList.Add(aboutUs);
                            indexAllFooter.aboutUs = aboutUsList;
                            //关于我们结束
                            //常见问题
                            commonProblems.UnableLogin = Convert.ToString(navs["UnableLogin"]) != null ? Convert.ToString(navs["UnableLogin"]) : "";
                            commonProblems.UnableRegister = Convert.ToString(navs["UnableRegister"]) != null ? Convert.ToString(navs["UnableRegister"]) : "";
                            commonProblemsList.Add(commonProblems);
                            indexAllFooter.comProblems = commonProblemsList;
                            //常见问题结束
                            //BarQ群
                            contact.qqGroup = Convert.ToString(navs["qqGroup"]) != null ? Convert.ToString(navs["qqGroup"]) : "";
                            contactsList.Add(contact);
                            indexAllFooter.contacts = contactsList;
                            //BarQ群结束

                        }
                        if (item.vcName == "footer")
                        {
                            JObject footers = (JObject)JsonConvert.DeserializeObject(item.vcContent);//将数据进行序列化
                            //底部备注
                            remarkFooters.copyRight = Convert.ToString(footers["copyRight"]) != null ? Convert.ToString(footers["copyRight"]) : "";
                            remarkFooters.address = Convert.ToString(footers["address"]) != null ? Convert.ToString(footers["address"]) : "";
                            remarkFooters.record = Convert.ToString(footers["record"]) != null ? Convert.ToString(footers["record"]) : "";
                            remarkFooters.maintenance = Convert.ToString(footers["maintenance"]) != null ? Convert.ToString(footers["maintenance"]) : "";
                            remarkFootList.Add(remarkFooters);
                            indexAllFooter.remarkFoots = remarkFootList;
                            //底部备注结束 
                        }

                    }
                }
                footerJsonTxt = JsonConvert.SerializeObject(indexAllFooter, Newtonsoft.Json.Formatting.Indented);
                //footerJsonTxt = ClearCharacter(footerJsonTxt);
            }
            return footerJsonTxt;//返回整个底部信息
        }
        /// <summary>
        /// 通过角色获取对应的导航
        /// </summary>
        /// <param name="indexNav"></param>
        /// <param name="navIndex"></param>
        /// <param name="identity"></param>
        /// <param name="userLoginState"></param>
        /// <returns></returns>
        public static IndexNav GetNavByRole(IndexNav indexNav,string navIndex,int identity,HttpCookie userLoginState)
        {
            //用户未登录返回的数据
            JObject navs = (JObject)JsonConvert.DeserializeObject(navIndex);//将数据进行序列化  
            indexNav.homePage = Convert.ToString(navs["homePage"]) != null ? Convert.ToString(navs["homePage"]) : "";
            indexNav.teamIntroduce = Convert.ToString(navs["teamIntroduce"])!=null? Convert.ToString(navs["teamIntroduce"]):"";
            indexNav.teamHonor = Convert.ToString(navs["teamHonor"]) != null ? Convert.ToString(navs["teamHonor"]) : "";
            indexNav.activeIntroduce = Convert.ToString(navs["activeIntroduce"]) != null ? Convert.ToString(navs["activeIntroduce"]) : "";
            indexNav.baseIntroduce = Convert.ToString(navs["baseIntroduce"]) != null ? Convert.ToString(navs["baseIntroduce"]) : "";
            //判断用户是否登录
            if (userLoginState != null)
            {
                //判断登录者的身份标识
                if (identity ==Convert.ToInt32(Model.EnumType.IdentityEnumType.Teacher))//8老师
                {
                    indexNav.teamApply = Convert.ToString(navs["teamApply"]) != null ? Convert.ToString(navs["teamApply"]) : "";//老师登录 
                    indexNav.perfectinformation = Convert.ToString(navs["perfectinformation"]) != null ? Convert.ToString(navs["perfectinformation"]) : "";
                    indexNav.personalCenter = Convert.ToString(navs["personalCenter"]) != null ? Convert.ToString(navs["personalCenter"]) : "";
                    indexNav.quit = Convert.ToString(navs["quit"]) != null ? Convert.ToString(navs["quit"]) : "";
                }
                else if (identity == Convert.ToInt32(Model.EnumType.IdentityEnumType.Captain))//4队长
                {
                    indexNav.perfectinformation = Convert.ToString(navs["perfectinformation"]) != null ? Convert.ToString(navs["perfectinformation"]) : "";
                    indexNav.honorApply = Convert.ToString(navs["honorApply"]) != null ? Convert.ToString(navs["honorApply"]) : "";
                    indexNav.activityApply = Convert.ToString(navs["activeApply"]) != null ? Convert.ToString(navs["activeApply"]) : "";
                    indexNav.personalCenter = Convert.ToString(navs["personalCenter"]) != null ? Convert.ToString(navs["personalCenter"]) : "";
                    indexNav.quit = Convert.ToString(navs["quit"]) != null ? Convert.ToString(navs["quit"]) : "";
                }
                else if (identity == Convert.ToInt32(Model.EnumType.IdentityEnumType.Admin))//16管理员
                {

                }
                else if(identity == Convert.ToInt32(Model.EnumType.IdentityEnumType.Member))//2队员登录。
                {
                    indexNav.personalCenter = Convert.ToString(navs["personalCenter"]) != null ? Convert.ToString(navs["personalCenter"]) : "";
                    indexNav.quit = Convert.ToString(navs["quit"]) != null ? Convert.ToString(navs["quit"]) : "";
                }
                else//其他
                {
                    indexNav.personalCenter = Convert.ToString(navs["personalCenter"]) != null ? Convert.ToString(navs["personalCenter"]) : "";
                    indexNav.quit = Convert.ToString(navs["quit"]) != null ? Convert.ToString(navs["quit"]) : "";
                }
            }
            //未登录
            else
            {
                indexNav.login = Convert.ToString(navs["login"]) != null ? Convert.ToString(navs["login"]) : "";
                indexNav.register = Convert.ToString(navs["register"]) != null ? Convert.ToString(navs["register"]) : "";
            }
            return indexNav;
            
        }
        /// <summary>
        /// 根据sliderName获取轮播图
        /// </summary>
        /// <param name="webmaster"></param>
        /// <param name="sliderName"></param>
        /// <returns></returns>
        public static string GetSlider(List<webmaster> webmaster,string sliderName)
        {
            string slider = null;
            SliderType sliderType = new SliderType();
            if (webmaster != null && sliderName != null)
            {
                foreach(var web in webmaster)
                {
                    if (web.vcName.ToUpper() == sliderName.ToUpper())
                    {
                        sliderType.slider =  web.vcContent;
                        slider = JsonConvert.SerializeObject(sliderType, Newtonsoft.Json.Formatting.Indented);
                        break;
                    }
                }
            }
            else
            {
                slider = "no";
            }
            return slider;
        }
        class SliderType
        {
            public string slider { get; set; }
        }
        /// <summary>
        /// 普通用户首页Nav
        /// </summary>
        public class IndexNav
        {
            public string homePage { get; set; }
            public string LOGO { get; set; }
            public string teamIntroduce { get; set; }
            public string teamHonor { get; set; }
            public string activeIntroduce { get; set; }
            public string baseIntroduce { get; set; }
            public string login { get; set; }
            public string register { get; set; }
            public string teamApply { get; set; }//老师登录显示
            public string perfectinformation { get; set; }//老师队长登录显示
            public string honorApply { get; set; }//队长登录荣耀申请
            public string activityApply { get; set; }//队长活动申请
            public string personalCenter { get; set; }//用户登录后显示个人中心
            public string quit { get; set; }//用户登陆后显示退出
        }
        /// <summary>
        /// 首页的底部开始
        /// </summary>
        public class IndexAllFooter
        {
            public List<FooterLogo> footerLogo { get; set; }
            public List<ProductService> proService { get; set; }
            public List<AboutUs> aboutUs { get; set; }
            public List<CommonProblems> comProblems { get; set; }
            public List<Contact> contacts { get; set; }
            public List<RemarkFooter> remarkFoots { get; set; }
        }
        public class FooterLogo
        {
            public string Logo { get; set; }//logo
        }
        public class ProductService
        {
            public string productService { get; set; }
            public string teamApply { get; set; }//团队申请
            public string honorApply { get; set; }//荣耀申请
            public string activeApply { get; set; }//活动申请
            public string perfectinformation { get; set; }//完善信息
        }
        public class AboutUs
        {
            public string teamIntroduce { get; set; }//团队介绍
            public string joinUs { get; set; }//加入我们
            public string problemBack { get; set; }//问题反馈
        }
        public class CommonProblems
        {
            public string UnableLogin { get; set; }
            public string UnableRegister { get; set; }
        }
        public class Contact
        {
            public string qqGroup { get; set; }
        }
        public class RemarkFooter
        {
            public string copyRight { get; set; }
            public string address { get; set; }
            public string record { get; set; }
            public string maintenance { get; set; }
        }
        //首页底部信息结束
    }
}
