using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Drawing;
using Microsoft.SqlServer;
using System.Web.Services;
using System.Configuration;

namespace BAR.TeamManager.Common
{
    public class EditPhotos
    {
        /// <summary>
        /// Base64转图片  
        /// </summary>
        /// <param name="inputStr">图片Base64编码</param>
        /// <param name="head">文件夹名（如：学生头像地址Server.MapPath("~/HeadPortrait/" + fileName)）；head值："~/HeadPortrait/"</param>
        /// <returns></returns>
        public static string Base64StringToImage(string inputStr, string path)
        {
            //if (string.IsNullOrWhiteSpace(inputStr))
            //    return null;
            try
            {

                string fileName = Guid.NewGuid().ToString();
                fileName = fileName + ".jpg";
                ////映射出保存文件的绝对路径
                ////string savePath = Server.MapPath("~/HeadPortrait/" + fileName);
                string savePath  = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(path), fileName);
                //string savePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(path), fileName);//服务端物理路径 
                //string savePath = System.Web.HttpContext.Current.Server.MapPath(head + "-" +fileName);
                //string filePath = "E:\\bb.jpg";
                byte[] arr = Convert.FromBase64String(inputStr.Substring(inputStr.IndexOf("base64,") + 7).Trim('\0'));
                using (MemoryStream ms = new MemoryStream(arr))
                {
                    Bitmap bmp = new Bitmap(ms);
                    //新建第二个bitmap类型的bmp2变量。
                    Bitmap bmp2 = new Bitmap(bmp, bmp.Width, bmp.Height);
                    //将第一个bmp拷贝到bmp2中
                    Graphics draw = Graphics.FromImage(bmp2);
                    draw.DrawImage(bmp, 0, 0);
                    draw.Dispose();
                    bmp2.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    ms.Close();
                }
                return savePath;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

    }
}
