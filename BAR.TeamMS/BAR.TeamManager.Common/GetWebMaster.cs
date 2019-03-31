using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAR.TeamManager.Model;

namespace BAR.TeamManager.Common
{
    public class GetWebMaster
    {
        public static StringBuilder WebMasterJson(List<webmaster> WebMasterList)
        {
            StringBuilder jsonTxt = new StringBuilder();
            jsonTxt.Append("{");
            foreach (var webMaster in WebMasterList)
            {
                if (webMaster.vcName == "title" || webMaster.vcName == "LOGO")
                {
                    jsonTxt.Append("\"" + webMaster.vcName + "\":" + "\"" + webMaster.vcContent + "\";");
                }
                else
                {
                    jsonTxt.Append("\"" + webMaster.vcName + "\":" + webMaster.vcContent + ",");
                }
                //去掉最后一个逗号
                jsonTxt.Remove(jsonTxt.Length - 1, 1);
            }
            jsonTxt.Append("}");
            return jsonTxt;
        }
    }
}
