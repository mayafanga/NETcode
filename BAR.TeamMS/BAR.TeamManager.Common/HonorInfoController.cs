using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAR.TeamManager.BLL;
using BAR.TeamManager.IBLL;
using BAR.TeamManager.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BAR.TeamManager.Common
{
    public class HonorInfoController
    {
        public static string GetHonorInfo(List<honor> honorList, IhonorService HonorService, IhonorparticipantmemberService HonorparticipantmemberService, IteamService TeamService, IpersonalinformationService PersonalinformationService)
      {
            try
            {
                //var honorList = HonorService.LoadEntities(h => true).ToList();
                List<Model.ViewModel.HonorStripModel.Honor> HonorList = new List<Model.ViewModel.HonorStripModel.Honor>();
                //获取荣耀表中所有的荣耀  （荣耀名称、上传时间、荣耀简介）
                foreach (var honor in honorList)
                {
                    List<int> honorMemberId = new List<int>();
                    List<Model.ViewModel.HonorStripModel.Person> personList = new List<Model.ViewModel.HonorStripModel.Person>();
                    Model.ViewModel.HonorStripModel.Honor honors = new Model.ViewModel.HonorStripModel.Honor();
                    honors.honorId = honor.ID;  //荣耀ID
                    honors.honorLogo = honor.vcPreviewAddress;//荣耀LOGO
                    honors.honorName = honor.vcHonorName;//荣耀名称
                    honors.honorSubmit = (System.DateTime)honor.dSubmitTime;//荣耀上传时间
                    honors.guidTeacher = honor.vcGuideTeacher;//荣耀指导老师
                    honors.honorIntroduce = honor.vcHonorIntroduce;//荣耀简介
                    var team = TeamService.LoadEntities(t => t.ID == honor.iTeamID).FirstOrDefault();
                    honors.honorTeam = team.vcTeamName;  //所属团队
                    var honorMemberList = HonorparticipantmemberService.LoadEntities(hm => hm.iHonorID == honor.ID).ToList();
                    foreach (var honorMember in honorMemberList)
                    {
                        honorMemberId.Add((int)honorMember.iUserID);
                        honors.unperHonorName = honorMember.vcNonTeamMember;
                    }
                    var perInfomation = PersonalinformationService.LoadPersonalInformationList(honorMemberId).ToList();
                    foreach (var perInfo in perInfomation)
                    {
                        Model.ViewModel.HonorStripModel.Person person = new Model.ViewModel.HonorStripModel.Person();
                        person.Name = perInfo.vcName;
                        personList.Add(person);
                    }
                    honors.personList = personList;
                    HonorList.Add(honors);
                }
                var timerConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" };
                string jsonTxt = JsonConvert.SerializeObject(HonorList, Newtonsoft.Json.Formatting.Indented);
                return jsonTxt;
            }
            catch (Exception)
            {
              return "网络不稳定，请稍后重试";
            }
        }
    }
}
