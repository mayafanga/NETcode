using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAR.TeamManager.BLL;
using BAR.TeamManager.IBLL;
using BAR.TeamManager.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Text;
using BAR.TeamManager.Common;

namespace BAR.TeamManager.WebApp.Controllers
{
    public class BaseIntroduceController : Controller
    {
        IwebmasterService WebmasterService { get; set; }
        // GET: BaseIntroduce
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 基地介绍方法   (get请求)
        /// </summary>
        /// <returns></returns>
        public ActionResult GetBaseSlider()
        {
            string jsonTxt = null;
            try
            {
                Slider slider = new Slider();
                var webMaster = WebmasterService.LoadEntities(w => true).ToList();
                string indexSlider = IndexPageCommon.GetIndexSlider(webMaster, "baseSlider");
                if (indexSlider != null)
                {
                    slider.baseSlider = indexSlider;
                    jsonTxt = JsonConvert.SerializeObject(slider, Newtonsoft.Json.Formatting.Indented);
                }
                else
                {
                    jsonTxt = "无此数据";
                }

            }
            catch (Exception)
            {
                jsonTxt = "网络不畅";
            }
            return Content(jsonTxt);
        }
        class Slider
        {
            public string baseSlider { get; set; }
        }
    }
}