using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAR.TeamManager.BLL;
using BAR.TeamManager.IBLL;
using BAR.TeamManager.Model;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BAR.TeamManager.Common;

namespace BAR.TeamManager.WebApp.Controllers
{
    public class HonorALLController : Controller
    {
        IhonorService HonorService { get; set; }
        IhonorparticipantmemberService HonorparticipantmemberService { get; set; }
        IteamService TeamService { get; set; }
        IpersonalinformationService PersonalinformationService { get; set; }

        // GET: HonorALL
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetHonorAll()
        {
            //获取荣耀表中所有的荣耀  （荣耀名称、上传时间、荣耀简介）
            var honorList = HonorService.LoadEntities(h => true && h.IsDel == false && h.bReviewOfWorks == true).ToList();
            string jsonTxt = HonorInfoController.GetHonorInfo(honorList, HonorService, HonorparticipantmemberService, TeamService, PersonalinformationService);      
            return Content(jsonTxt);
        }
    }
    
}