using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BAR.TeamManager.Common
{
    public class DownLoadFile
    {
        public static string DownLoadFiles(string uri,string savePath)
        {
            //从文件路径中获取文件名
            string fileName;
            if (uri.IndexOf("\\") > -1)
            {
                fileName = uri.Substring(uri.LastIndexOf("\\") + 1);
            }
            else
            {
                fileName = uri.Substring(uri.LastIndexOf("/") + 1);
            }

            //设置文件保存路径：路径+"\"+文件名.后缀、路径+"/"+文件名.后缀
            if (!savePath.EndsWith("/") && !savePath.EndsWith("\\"))
            {
                savePath = savePath + "/"; //也可以是savePath + "\\"
            }

            savePath += fileName;   //另存为的绝对路径＋文件名
            uri = uri.Substring(2);
            string thisPath= System.Web.HttpContext.Current.Server.MapPath("../");//服务端物理路径 
            string downPath = thisPath  + "file";
            downPath = downPath + "\\" + fileName;
            //string downPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("../"), uri);
            //string downPath = thisPath + uri;
            //下载文件
            WebClient client = new WebClient();
            if (Directory.Exists(thisPath))
            {
                try
                {
                    client.DownloadFile(downPath, savePath);
                    return "ok";
                }
                catch (Exception ex)
                {
                    //Logger.Error(typeof(DownLoadFile), "下载文件失败", ex);
                    return "no";
                }
            }
            else
            {
                return "no";
            }
        }
    }
}
